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
group by c.name,d.taxAmount
)t1
group by name,xinhetong,buhetong,PUInvoicetaxAmount,PU_Amount,SA_origSettleAmount,CaiLiaoAmount,JieKuanAmount






