
/****** Object:  StoredProcedure [dbo].[Sync_SA_SaleDelivery]    Script Date: 07/29/2017 12:11:12 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<黄臣晓>
-- Create date: <2017-7-29 12:17:34>
-- Description:	<T+销货单同步>
-- =============================================
ALTER PROCEDURE [dbo].[Sync_SA_SaleDelivery]
	--@vh_code nvarchar(50),--单据号
    @ResultStr nvarchar(300)='' output, --返回值
    @id int
AS
BEGIN
	 SET NOCOUNT ON;
	 declare @MasterID int            --主表ID
  --   declare @count int               --判断数值
	 --set @count=(select count(*) from SA_SaleDelivery t0 where t0.code=@vh_code)
		--if	@count>0
		--begin
		--	set @ResultStr='导入失败，该单据号已经存在！'
		--	return 0
	 --end
     
  --   declare @idcustomer int=0,@maker nvarchar(50)
     
  --   select @maker=maker,@idcustomer=b.id from HM_DataImportInfo a 
  --   inner join AA_Partner b on a.ccusname=b.name
  --   where a.id=@id
  --   if @idcustomer=0
	 --begin
		--set @ResultStr='客户资料与T+不匹配'
  --      return 0
	 --end
     
     
	 DECLARE @code nvarchar(50)
	--获取销货单号
	 exec Sync_GetCode 104,@code output
	 
	 insert into [SA_SaleDelivery]
	 (isBeforeSystemInuse,thisBalanceIntegral,invoiceType,idwarehouse,amount,--1
	 createdtime,madedate,isCounteractDelivery,auditor,iscarriedforwardin,--2
	 makerid,origTaxAmount,taxAmount,PrintCount,origCancelAmount,--3
	 origAmount,updated,discountRate,isAutoGenerateSaleOut,isNoArapBookkeeping,--4
	 idmarketingOrgan,voucherState,voucherdate,origprereceiveamount,idsettlecustomer, --5
	 ismodifiedcode,changer,idcustomer,isAutoGenerateInvoice,idbusinesstype,----6
	 iscarriedforwardout,updatedBy,memo,idcurrency,OrigInvoiceTaxAmount,--7
	 code,saleInvoiceNo,isPriceTrace,origSettleAmount,settleAmount,--8
	 DataSource,reciveType,contactPhone,address,isCancel,--9
	 exchangeRate,prereceiveamount,isSaleOut,linkMan,MemberAddress,--10
	 idrdstyle,reviser,maker,auditorid,isNoModify,--11
     auditeddate,idsourcevouchertype,SourceVoucherDate,--12
     IsSeparateByWareHouse,IsAutoSaleOut,priuserdefnvc1,priuserdefnvc2,priuserdefnvc3, --13
     SaleInvoiceMedia,Allowances,origAllowances--14
     )
	 select 0 as isBeforeSystemInuse,0 as thisBalanceIntegral,281 as invoiceType,NULL as idwarehouse,0 as amount, --1
		   convert(varchar(20),getdate(),120) as createdtime,convert(varchar(10),getdate(),120)+' 00:00:00' as madedate,0 as isCounteractDelivery,'' as auditor,0 as iscarriedforwardin,--2
		   20 as makerid,0 as origTaxAmount,0 as taxAmount,0 as PrintCount,NULL as origCancelAmount,--3
		   0 as origAmount,convert(varchar(20),getdate(),120) as updated,1 as discountRate,0 as isAutoGenerateSaleOut,0 as isNoArapBookkeeping,--4
		   1 as idmarketingOrgan,181 as voucherState,convert(varchar(10),getdate(),120)+' 00:00:00' as voucherdate,0 as origprereceiveamount,b.id,--5
		   0 as ismodifiedcode,'' as changer,b.id,0 as isAutoGenerateInvoice,65 idbusinesstype,--6
		   0 as iscarriedforwardout,a.maker,a.memo,4 as idcurrency,0 as OrigInvoiceTaxAmount,--7
		   @code,'' as saleInvoiceNo,1 as isPriceTrace,0 as origSettleAmount,0 as settleAmount,--8
		   1531,7 as reciveType,a.contactPhone,a.address,428 as isCancel,--9
		   1 as exchangeRate,0 as prereceiveamount,305 as isSaleOut,a.linkMan,'' as MemberAddress,--10
		   17 as idrdstyle,'' as reviser,a.maker,NULL as auditorid,NULL as isNoModify, --11
           NULL,NULL,NULL,--12
           0 as IsSeparateByWareHouse,0 as IsAutoSaleOut,a.priuserdefnvc1,a.priuserdefnvc2,a.priuserdefnvc3,--13
           51014,0,0 
	 from HM_DataImportInfo a inner join AA_Partner b on a.ccusname=b.name
     where a.id=@id
	  set @MasterID=@@Identity
	  
   
	--发货单子表
	 insert into [SA_SaleDelivery_b]
	 (quantity,baseQuantity,discountRate,origDiscountPrice,origDiscountAmount,--1
	 origTax,origTaxPrice,origTaxAmount,discountPrice,discountAmount,--2
	 tax,taxPrice,taxAmount,taxRate,costPrice,costAmount,--3
	 isPresent,idSaleDeliveryDTO,idWarehouse,idInventory,--4
	 idbaseunit,taxflag,ismanualcost,idunit,updated,--5
	 updatedBy,priceStrategyTypeName,pricestrategyTypeID,isMemberIntegral,IsPromotionPresent,--6
	 isSaleOut,saleOrderId ,saleOrderCode ,saleOrderDetailId,code,--7
     isNoModify,--8
     sourceVoucherId,sourceVoucherDetailid,sourceVoucherCode,idsourcevouchertype,dataSource,--9
     saleoutquantity,priuserdefdecm1--10
     )
	 select convert(decimal(18,2),a.quantity),convert(decimal(18,2),a.quantity),1,convert(decimal(18,4),(a.taxamount/a.quantity)),convert(decimal(18,2),a.taxamount),--1
		   0,convert(decimal(18,4),(a.taxamount/a.quantity)),convert(decimal(18,2),a.taxamount),convert(decimal(18,4),(a.taxamount/a.quantity)),a.taxamount,--2
		   0,convert(decimal(18,4),(a.taxamount/a.quantity)),convert(decimal(18,2),a.taxamount),0,NULL,0,--前两项待确认,--3
		   0,@MasterID,NULL,b.id,--4
		   b.idunit,1,0,b.idunit,convert(varchar(20),getdate(),120),--5
		   maker,'存货最新售价',NULL,0,0,--6
		   NULL,NULL,NULL,NULL,'0000',--7
           NULL,--8
           NULL,NULL,NULL,NULL,1531, --9
           NULL,a.priuserdefdecm1
	  from HM_DataImportInfo a left join AA_Inventory b on a.idinventory=b.code
	  where a.id=@id

      --修改表头金额
      
      update SA_SaleDelivery set origAmount=a.taxAmount,amount=a.taxAmount,origTaxAmount=a.taxAmount,taxAmount=a.taxAmount
      from (select sum(taxAmount)as taxAmount from SA_SaleDelivery_b where idSaleDeliveryDTO=@MasterID)a
      where id=@MasterID
      
    if @@rowcount>0
		begin
			set @ResultStr='同步成功'
            return 1
		end
    else
		begin
		    set @ResultStr= '同步失败'
            return 0
        end
END



/****** Object:  StoredProcedure [dbo].[Sync_SaleOrder]    Script Date: 11/14/2016 14:06:14 ******/
SET ANSI_NULLS ON
