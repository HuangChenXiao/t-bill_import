USE [UFTData134085_000002]
GO
/****** Object:  StoredProcedure [dbo].[Sync_Proc_PurchaseOrder]    Script Date: 08/17/2017 16:22:00 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

ALTER PROC [dbo].[Sync_Proc_PurchaseOrder]
    @ResultStr nvarchar(300)='' output, --返回值
	@partner_code nvarchar(50)--单据号
as
begin
	declare @code nvarchar(50)
	exec Sync_GetCode 27,@code output
	select @code
	insert into PU_PurchaseOrder(origTotalAmount,iscarriedforwardin,makerid,createdtime,exchangeRate,auditor,--1
								 discountRate,origTotalTaxAmount,iscarriedforwardout,madedate,PrintCount,--2
								 totalAmount,voucherState,voucherdate,ismodifiedcode,--3
								 maker,collaborateVoucherCode,earnestMoney,prePaymentAmount,idbusinesstype,--4
								 updatedBy,memo,origEarnestMoney,idcurrency,code,--5
								 idmarketingOrgan,changer,reviser,idpartner,origPrePaymentAmount,--6
								 updated,acceptAddress,linkMan,payType,contractId,--7
								 totalTaxAmount,linkTelphone) --8
	values                       (0,0,0,getdate(),1,--1
								  '',1,0,0,getdate(),--2
								  0,0,181,getdate(),0,--3
								  'demo','',0,NULL,1,--4
								  'demo','',0,4,@code,--5
								  1,'','',1,NULL,--6
								  NULL,'','',457,'',--7
								  0,'')--8



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
								   discountAmount,taxAmount,idsubunit,updated)--11
	values                         (NULL,'',1,NULL,1,--1
									1,49,1,0,NULL,--2
									NULL,NULL,1,7,4,--3
									0,1,'7方数',NULL,0,--4
									'',7,7,1,NULL,--5
									NULL,7,NULL,'demo',0,--6
									0,1,0,7,1,--7
									'0000',NULL,7,49,NULL,--8
									7,2,0,7,'',--9
									NULL,NULL,'',7,NULL,--10
									49,49,2,NULL
									)

end
                               
                               
                               
                               