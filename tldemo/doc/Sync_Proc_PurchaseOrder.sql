
/****** Object:  StoredProcedure [dbo].[Sync_Proc_PurchaseOrder]    Script Date: 09/08/2017 11:36:34 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

ALTER PROC [dbo].[Sync_Proc_PurchaseOrder]
    @ResultStr nvarchar(300)='' output, --返回值
	@voucherdate nvarchar(50),--单据日期
	@partner_name nvarchar(50)--供应商
as
begin
    --生成订单号
	declare @code nvarchar(50)
	exec Sync_GetCode 27,@code output
	select @code
	--查询表头内容
	declare @maker nvarchar(50),@idclerk int,@idpartner int
	select distinct @maker=a.maker,@idpartner=b.id,@idclerk=c.id 
	from Sync_Table_PurchaseOrder a
	left join AA_Partner b on a.partner_name=b.name
	left join AA_Person c on a.clerk_name=c.name and c.isSalesman=1
    where a.voucherdate=@voucherdate and a.partner_name=@partner_name
	--判断基础档案是否存在
	if(isnull(@idpartner,0)=0)
	begin
		set @ResultStr='导入失败，供应商基础信息不完善！'
		return 0
	end
	if(isnull(@idclerk,0)=0)
	begin
		set @ResultStr='导入失败，业务员基础信息不完善！'
		return 0
	end
	
	insert into PU_PurchaseOrder(origTotalAmount,iscarriedforwardin,makerid,createdtime,exchangeRate,auditor,--1
								 discountRate,origTotalTaxAmount,iscarriedforwardout,madedate,PrintCount,--2
								 totalAmount,voucherState,voucherdate,ismodifiedcode,--3
								 maker,collaborateVoucherCode,earnestMoney,prePaymentAmount,idbusinesstype,--4
								 updatedBy,memo,origEarnestMoney,idcurrency,code,--5
								 idmarketingOrgan,changer,reviser,idpartner,origPrePaymentAmount,--6
								 updated,acceptAddress,linkMan,payType,contractId,--7
								 totalTaxAmount,linkTelphone,idclerk) --8
	values                       (0,0,0,@voucherdate,1,--1
								  '',1,0,0,@voucherdate,--2
								  0,0,181,@voucherdate,0,--3
								  'demo','',0,NULL,1,--4
								  'demo','',0,4,@code,--5
								  1,'','',@idpartner,NULL,--6
								  NULL,'','',457,'',--7
								  0,'',@idclerk)--8
    declare @MasterID int
    set @MasterID=@@Identity
    
	insert into PU_PurchaseOrder_b(basePrice,inventoryBarCode,unitExchangeRate,subQuantity,discountRate,--1
								   freeItem0,origTaxAmount,freeItem1,arrivalTimes,countQuantity,--2
								   countQuantity2,cumInstockShrinkageQuantity,idunit,baseQuantity,idPurchaseOrderDTO,--3
								   tax,idinventory,compositionQuantity,price,stockTimes,--4
								   saleOrderCode,origDiscountPrice,taxPrice,freeItem2,discount,--5
								   origDiscount,baseDiscountPrice,countArrivalQuantity2,updatedBy,taxRate,--6
								   origTax,taxFlag,isPresent,discountPrice,idbaseunit,--7
								   code,countInQuantity,origTaxPrice,origDiscountAmount,origPrice,--8
								   quantity,idunit2,islaborcost,quantity2,partnerInventoryCode,--9
								   cumInstockShrinkageQuantity2,countInQuantity2,partnerInventoryName,baseTaxPrice,countArrivalQuantity,--10
								   discountAmount,taxAmount,idsubunit,updated,pubuserdefdecm2,--11
								   pubuserdefdecm1,pubuserdefdecm3,pubuserdefdecm4,priuserdefdecm2,priuserdefdecm3,--12
								   priuserdefdecm4)--13
	select                         NULL,'',1,NULL,1,--1
								   a.freeItem1,convert(decimal(18,2),a.amount),a.freeItem2,0,NULL,--2
								   NULL,NULL,c.id,convert(decimal(18,2),a.quantity),@MasterID,--3
								   0,b.id,dbo.get_pu_order_weight(convert(float,a.quantity)),NULL,0,--4
								   '',convert(decimal(18,4),a.price),convert(decimal(18,4),a.price),a.freeItem3,NULL,--5
								   NULL,convert(decimal(18,4),a.price),NULL,NULL,0,--6
								   0,1,0,convert(decimal(18,4),a.price),c.id,--7
								   '0000',NULL,convert(decimal(18,4),a.price),convert(decimal(18,2),a.amount),NULL,--8
								   convert(decimal(18,2),a.quantity),d.id,0,convert(decimal(18,2),a.quantity2),'',--9
								   NULL,NULL,'',convert(decimal(18,4),a.price),NULL,--10
								   convert(decimal(18,2),a.amount),convert(decimal(18,2),a.amount),2,NULL,a.pubuserdefdecm2,--11
								   a.pubuserdefdecm1,a.pubuserdefdecm3,a.pubuserdefdecm4,a.priuserdefdecm2,a.priuserdefdecm3, --12
								   a.priuserdefdecm4
	from                           Sync_Table_PurchaseOrder a
	inner join                     AA_Inventory b on a.cinvcode=b.code
	inner join                     aa_unit c on b.idunitgroup=c.idunitgroup and c.isMainUnit=1
	inner join                     aa_unit d on b.idunitgroup=d.idunitgroup and d.isMainUnit=0
	where a.voucherdate=@voucherdate and a.partner_name=@partner_name	
	
	update PU_PurchaseOrder set origTotalAmount=a.taxAmount,totalAmount=a.taxAmount,origTotalTaxAmount=a.taxAmount,totalTaxAmount=a.taxAmount
	from  (select sum(taxAmount) as taxAmount from PU_PurchaseOrder_b where idPurchaseOrderDTO=@MasterID) a
	where id=@MasterID
	
	exec Sync_SetCode 27 --采购订单
	
	set @ResultStr='同步成功'
    return 1
end
                               
                               
                               
                               