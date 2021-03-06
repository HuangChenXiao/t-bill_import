USE [UFTData134085_000002]
GO
/****** Object:  StoredProcedure [dbo].[Sync_Proc_ExpenseVoucher]    Script Date: 09/04/2017 16:23:20 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

ALTER PROC [dbo].[Sync_Proc_ExpenseVoucher]
    @ResultStr nvarchar(300)='' output, --返回值
	@voucherdate nvarchar(50),--单据日期
	@parent_name nvarchar(50)--供应商
as
begin
    --生成订单号
	declare @code nvarchar(50)
	exec Sync_GetCode 112,@code output
	select @code
	--查询表头内容
	declare @maker nvarchar(50),@idpartner int,@pubuserdefnvc4 nvarchar(50)
	select distinct @maker=a.maker,@idpartner=b.id,@pubuserdefnvc4=a.pubuserdefnvc4
	from Sync_Table_ExpenseVoucher a
	left join AA_Partner b on a.parent_name=b.name
    where a.voucherdate=@voucherdate and a.parent_name=@parent_name
	--判断基础档案是否存在
	if(isnull(@idpartner,0)=0)
	begin
		set @ResultStr='导入失败，往来单位基础信息不完善！'
		return 0
	end
	
	insert into CS_ExpenseVoucher(origpaymentamount,iscarriedforwardin,amount,createdtime,madedate,--1
							  auditor,idpartner,idbusinesstype,origTaxAmountSum,maker,--2
							  iscarriedforwardout,origAmount,sourceVoucherCode,PrintCount,reviser,--3
							  IdMarketingOrgan,saleordercode,invoiceCode,voucherstate,paymentamount,--4
							  voucherdate,ismodifiedcode,voucherSettleState,memo,idcurrency,--5
							  code,arapAccountDirection,backgroundCreateVoucher,taxAmountSum,billType,--6
							  exchangeRate,pubuserdefnvc4)
	values                        (0,0,NULL,@voucherdate,@voucherdate,--1
                              '',@idpartner,19,0,@maker,--2
                              0,NULL,'',0,'',--3
                              1,'','',181,0,--4
                              @voucherdate,0,428,'',4,--5
                              @code,2094,0,0,455,--6
                              1,@pubuserdefnvc4)

    declare @MasterID int
    set @MasterID=@@Identity
    
    declare @idexpenseitem int--费用id
    set @idexpenseitem = (select id from AA_ExpenseItem where name='加工费')
    
	insert into CS_ExpenseVoucher_b(origTax,isTax,taxamount,tax,origamount,--1
									origSettleAmount,idexpenseitem,settleAmount,amount,taxRate,--2
									code,origTaxAmount,ismodifyorigtax,isApportion,idExpenseVoucherDTO,--3
									priuserdefnvc1,priuserdefnvc2,priuserdefdecm1,priuserdefdecm2,priuserdefdecm3,--4
									pubuserdefdecm3,pubuserdefdecm4,priuserdefnvc3,priuserdefnvc4,--5
									pubuserdefnvc1,pubuserdefdecm1,pubuserdefnvc3,pubuserdefnvc2,priuserdefdecm4)--6
	select							0,1,money,0,money,--1
								    NULL,@idexpenseitem,NULL,money,0,--2
								    '0000',money,0,0,@MasterID,--3
								    priuserdefnvc1,priuserdefnvc2,priuserdefdecm1,priuserdefdecm2,priuserdefdecm3,--4
								    pubuserdefdecm3,pubuserdefdecm4,priuserdefnvc3,priuserdefnvc4,--5
								    pubuserdefnvc1,pubuserdefdecm1,pubuserdefnvc3,pubuserdefnvc2,price--6
	from                            Sync_Table_ExpenseVoucher a
	where                           voucherdate=@voucherdate and parent_name=@parent_name
	
	--修改表头金额
	update CS_ExpenseVoucher set origTaxAmountSum=a.taxAmount,taxAmountSum=a.taxAmount
	from  (select sum(taxAmount) as taxAmount from CS_ExpenseVoucher_b where idExpenseVoucherDTO=@MasterID) a
	where id=@MasterID
	
	--插入应收应付表
	INSERT INTO  ARAP_Detail(AuditBussinessFlag,AuditFlag,PrepayFlag,BussinessFlag,Flag,--1
                            CreatedTime,Idpartner,IdnoSettlePartner,Iddepartment,Idperson,--2
                            Idcurrency,ExchangeRate,IsArFlag,BusinessID,BusinessCode,--3
                            DocId,IdbusiType,IdvoucherType,IdarapVoucherType,VoucherID,--4
                            VoucherCode,VoucherDetailID,DetailID,DetailCode,DetailName,--5
                            Memo,sourcevouchermemo,VoucherDate,RegisterDate,ArrivalDate,--6
                            Year,Period,OrigAmount,Amount,BookFlag,--7
                            OrigSettleAmount,SettleAmount,origCashAmount,cashAmount,origInAllowances,--8
                            inAllowances,SourceOrderCode,SourceOrderID,SaleOrderCode,SaleOrderID,--9
                            DetailType,Idproject,IdDetailProject,isCarriedForwardOut,isCarriedForwardIn,--10
                            IdMarketingOrgan)--11
     select                 0,0,0,0,-1,--1
							GETDATE(),@idpartner,@idpartner,null,null,--2
							4,1,0,@MasterID,@code,--3
							null,19,112,112,@MasterID,--4
							@code,id,@idexpenseitem,'01001','加工费',--5
							'','',GETDATE(),GETDATE(),null,--6
							YEAR(GETDATE()),month(GETDATE()),taxamount,taxamount,2217,--7
							0,0,0,0,0,--8
							0,'',null,'',null,--9
							'inventory',null,null,0,0,--10
							1--11
    from CS_ExpenseVoucher_b where idExpenseVoucherDTO=@MasterID	
	
	
	exec Sync_SetCode 112 --费用单
	
	set @ResultStr='同步成功'
    return 1						

end
      