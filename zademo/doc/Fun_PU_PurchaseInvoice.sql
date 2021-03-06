USE [UFTData895739_000005]
GO
/****** Object:  UserDefinedFunction [dbo].[Fun_PU_PurchaseInvoice]    Script Date: 11/21/2017 17:33:30 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<黄臣晓>
-- Create date: <2017-11-18 09:48:34>
-- Description:	<采购发票>
-- =============================================
ALTER FUNCTION [dbo].[Fun_PU_PurchaseInvoice]
(
	@projectname nvarchar(20)
)
RETURNS decimal(18,2)
AS
BEGIN
	declare @Acount decimal(18,2)
    
    set @Acount=(select sum(a.taxAmount) from PU_PurchaseInvoice_b a 
	inner join PU_PurchaseInvoice b on a.idPurchaseInvoiceDTO =b.id
	inner join aa_project c on a.idproject=c.id
	where c.name=@projectname
	and b.voucherState=189
	group by c.name)
    
	RETURN @Acount

END
