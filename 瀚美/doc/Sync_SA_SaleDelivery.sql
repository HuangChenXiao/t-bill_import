USE [UFTData242065_000001]
GO
/****** Object:  StoredProcedure [dbo].[Sync_SA_SaleDelivery]    Script Date: 08/03/2017 17:21:46 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<黄臣晓>
-- Create date: <2017-7-29 12:17:34>
-- Description:	<T+销货单同步>
-- =============================================
-- exec Sync_SA_SaleDelivery '10000001'
ALTER PROCEDURE [dbo].[Sync_SA_SaleDelivery]
    @ResultStr nvarchar(300)='' output, --返回值
	@code nvarchar(50)--单据号
AS
BEGIN
	 SET NOCOUNT ON;
	 declare @MasterID int            --主表ID
     declare @count int               --判断数值
	 set @count=(select count(*) from SA_SaleDelivery t0 where t0.code=@code)
	 if	@count>0
	 begin
		set @ResultStr='导入失败，单据号"'+@code+'"已经存在！'
		return 0
	 end
     set @count=(select count(1) from HM_DataImportInfo a where not exists(select 1 from AA_Partner where a.ccusname=name) and a.code=@code)
     if @count>0
	 begin
		set @ResultStr='导入失败，单据号"'+@code+'"T+客户信息不完善！'
		return 0
	 end
	 set @count=(select count(1) from HM_DataImportInfo a where not exists(select 1 from AA_Inventory where a.idinventory=code) and a.code=@code)
     if @count>0
	 begin
		set @ResultStr='导入失败，单据号"'+@code+'"T+存货信息不完善！'
		return 0
	 end
	-- DECLARE @code nvarchar(50)
	----获取销货单号
	-- exec Sync_GetCode 104,@code output
	 
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
	 select distinct 0 as isBeforeSystemInuse,0 as thisBalanceIntegral,281 as invoiceType,c.id,0 as amount, --1
		   convert(varchar(20),getdate(),120) as createdtime,convert(varchar(10),getdate(),120)+' 00:00:00' as madedate,0 as isCounteractDelivery,'' as auditor,0 as iscarriedforwardin,--2
		   20 as makerid,0 as origTaxAmount,0 as taxAmount,0 as PrintCount,NULL as origCancelAmount,--3
		   0 as origAmount,convert(varchar(20),getdate(),120) as updated,1 as discountRate,0 as isAutoGenerateSaleOut,0 as isNoArapBookkeeping,--4
		   1 as idmarketingOrgan,181 as voucherState,convert(varchar(10),getdate(),120)+' 00:00:00' as voucherdate,0 as origprereceiveamount,b.id,--5
		   0 as ismodifiedcode,'' as changer,b.id,0 as isAutoGenerateInvoice,65 idbusinesstype,--6
		   0 as iscarriedforwardout,a.maker,a.memo,4 as idcurrency,0 as OrigInvoiceTaxAmount,--7
		   @code,a.saleInvoiceNo,1 as isPriceTrace,0 as origSettleAmount,0 as settleAmount,--8
		   1531,7 as reciveType,a.contactPhone,a.address,428 as isCancel,--9
		   1 as exchangeRate,0 as prereceiveamount,305 as isSaleOut,a.linkMan,'' as MemberAddress,--10
		   17 as idrdstyle,'' as reviser,a.maker,NULL as auditorid,NULL as isNoModify, --11
           NULL,NULL,NULL,--12
           0 as IsSeparateByWareHouse,0 as IsAutoSaleOut,a.priuserdefnvc1,a.priuserdefnvc2,a.priuserdefnvc3,--13
           51014,0,0 
	 from HM_DataImportInfo a 
	 inner join AA_Partner b on a.ccusname=b.name
	 inner join AA_Warehouse c on a.warehouse=c.name
     where a.code=@code
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
     saleoutquantity,priuserdefdecm1,Retailprice--10
     )
	 select convert(decimal(18,2),a.quantity),convert(decimal(18,2),a.quantity),1,dbo.Sync_price(a.id),dbo.Sync_amount(a.id),--1
		   0,dbo.Sync_price(a.id),dbo.Sync_amount(a.id),dbo.Sync_price(a.id),dbo.Sync_amount(a.id),--2
		   0,dbo.Sync_price(a.id),dbo.Sync_amount(a.id),0,NULL,0,--前两项待确认,--3
		   0,@MasterID,d.id,b.id,--4
		   b.idunit,1,0,b.idunit,convert(varchar(20),getdate(),120),--5
		   maker,'存货最新售价',NULL,0,0,--6
		   NULL,NULL,NULL,NULL,'0000',--7
           NULL,--8
           NULL,NULL,NULL,NULL,1531, --9
           NULL,a.priuserdefdecm1,c.retailPriceNew
	  from HM_DataImportInfo a 
	  inner join AA_Inventory b on a.idinventory=b.code
	  left join AA_InventoryPrice c on b.id=c.idinventory
	  inner join AA_Warehouse d on a.warehouse=d.name
	  where a.code=@code

      --修改表头金额
      
      update SA_SaleDelivery set origAmount=a.taxAmount,amount=a.taxAmount,origTaxAmount=a.taxAmount,taxAmount=a.taxAmount
      from (select sum(taxAmount)as taxAmount from SA_SaleDelivery_b where idSaleDeliveryDTO=@MasterID)a
      where id=@MasterID
      
      
      --查询审批人 格式  外销仓 019蓝娇 其他021孙传强
      declare @UserID int,@UserName nvarchar(50),@UserCode nvarchar(50)
      --根据仓库查询审批人编码
      select distinct @UserCode=case when b.code='004' then '019' else '021' end from HM_DataImportInfo a 
      inner join AA_Warehouse b on a.warehouse=b.name
      where a.code=@code
      --查询审批人ID，名称
      select @UserID=id,@UserName=name from aa_person
      where code=@UserCode
      --插入审批流_操作日志表

      INSERT INTO Eap_OperationLog   
	  (OperateType,AccountID,AccountName,ModuleCode,FunctionCode,FunctionName,Operate,UserID,UserName,MachineName,
	  Time,DocNo,Status,Description,FunctionDate) 
	  VALUES(N'0',N'1',N'瀚美',N'SA',N'SA04',N'',N'新增',@UserName,@UserName,N'127.0.0.1', 
	  CONVERT(DateTime, getdate(),120),@code,N'1',N'ID='+convert(nvarchar(50),@MasterID),CONVERT(DateTime, getdate(),120)) 
      --插入审批流_审批任务
      insert into eap_wftask(BizCode, EntityCode, [Content], updatedBy, TaskStatus, UserID, AuditLogID, NodeID, PreNodeID, SolutionID,
                         EntityID, updated, CreateTime)
      values             ('SA04',@code,'',@UserName,0,@UserID,NULL,3,1,1,
                         @MasterID,CONVERT(DateTime, getdate(),120),CONVERT(DateTime, getdate(),120))
      --获取单据实体ID 
      declare @wftask_id int,@EntityID int
      set @wftask_id=@@Identity
      select @wftask_id
      select @EntityID=EntityID from eap_wftask where id=@wftask_id
      --插入审批流_审批日志                 
      insert eap_wfauditlog(updatedBy,BizCode,EntityCode,Opinion,AuditAction,
                      AuditorName,IsCurrent,AuditorID,SolutionID,EntityID,
                      updated,AuditTime)
	  values               (@UserName,'SA04',@code,'',0,
                      @UserName,1,@UserID,1,@EntityID,
                      CONVERT(DateTime, getdate(),120),CONVERT(DateTime, getdate(),120)) 
      
                         
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
