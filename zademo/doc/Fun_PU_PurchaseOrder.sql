USE [UFTData895739_000005]
GO
/****** Object:  UserDefinedFunction [dbo].[Fun_PU_PurchaseOrder]    Script Date: 11/21/2017 17:33:36 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<黄臣晓>
-- Create date: <2017-11-18 09:48:34>
-- Description:	<采购订单-应付金额>
-- =============================================
ALTER FUNCTION [dbo].[Fun_PU_PurchaseOrder]
(
	@projectname nvarchar(20)
)
RETURNS decimal(18,2)
AS
BEGIN
	declare @Acount decimal(18,2)
    
    set @Acount=(select sum(a.taxAmount) from PU_PurchaseOrder_b a 
	inner join PU_PurchaseOrder b on a.idPurchaseOrderDTO =b.id
	inner join aa_project c on a.idproject=c.id
	where c.name=@projectname
	and b.voucherState=189
	group by c.name)
    
	RETURN @Acount

END

