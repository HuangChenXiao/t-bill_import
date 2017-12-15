SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<黄臣晓>
-- Create date: <2017-11-20 17:55:59>
-- Description:	<项目资金汇总>
-- =============================================
-- exec PROC_getProjectAmount '','2017-11-14','2017-11-15'
ALTER PROCEDURE PROC_getProjectAmount
	@projectname nvarchar(50),
	@startdate nvarchar(50),
	@enddate nvarchar(50)
AS
BEGIN
	SET NOCOUNT ON;
	select name,isnull(xinhetong,0) as xinhetong,isnull(buhetong,0) as buhetong,sum(SAInvoicetaxAmount)+isnull(buhetong,0) as SAInvoicetaxAmount,
		   SA_origSettleAmount+isnull(buhetong,0) as SA_origSettleAmount,isnull(xinhetong,0)+ isnull(buhetong,0) as hetongAmount,CaiLiaoAmount,
		   JieKuanAmount,PUInvoicetaxAmount,PU_Amount
	from (

	select c.name,dbo.Fun_getHetongAmount(c.name,'新项目合同') as xinhetong,dbo.Fun_getHetongAmount(c.name,'增补合同') as buhetong,
		   d.taxAmount as SAInvoicetaxAmount,dbo.Fun_SA_origSettleAmount(c.name) as SA_origSettleAmount,dbo.Fun_ST_RDRecordCaiLiao(c.name) as CaiLiaoAmount,
		   dbo.Fun_PU_JieKuanAmount(c.name) as JieKuanAmount,dbo.Fun_PU_PurchaseInvoice(c.name) as PUInvoicetaxAmount,dbo.Fun_PU_PurchaseOrder(c.name) as PU_Amount
	from sa_saleorder_b a 
	inner join sa_saleorder b on a.idSaleOrderDTO =b.id
	inner join aa_project c on a.idproject=c.id
	inner join SA_SaleInvoice_b d on d.idproject=c.id
	where c.name like '%'+isnull(@projectname,'')+'%'
	and (b.voucherdate>=convert(datetime,@startdate,120) and b.voucherdate<=convert(datetime,@enddate,120))
	group by c.name,d.taxAmount
	)t1
	group by name,xinhetong,buhetong,PUInvoicetaxAmount,PU_Amount,SA_origSettleAmount,CaiLiaoAmount,JieKuanAmount
END
GO
