USE [UFTData895739_000005]
GO
/****** Object:  UserDefinedFunction [dbo].[Fun_getHetongAmount]    Script Date: 11/21/2017 17:33:15 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<黄臣晓>
-- Create date: <2017-11-16 15:31:37>
-- Description:	<查询 新项目合同 - 增补合同>
-- =============================================
ALTER FUNCTION [dbo].[Fun_getHetongAmount]
(
	@projectname nvarchar(50),
	@projecttype nvarchar(50)
)
RETURNS decimal(18,2)
AS
BEGIN
	declare @Acount decimal(18,2)
    
    set @Acount=(select sum(a.taxAmount) from sa_saleorder_b a 
	inner join sa_saleorder b on a.idSaleOrderDTO =b.id
	inner join aa_project c on a.idproject=c.id
	where b.pubuserdefnvc1=@projecttype and c.name=@projectname
	and b.voucherState=189
	group by c.name)
    
	RETURN @Acount

END
