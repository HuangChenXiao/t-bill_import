USE [UFTData895739_000005]
GO
/****** Object:  UserDefinedFunction [dbo].[Fun_SA_origSettleAmount]    Script Date: 11/21/2017 17:33:42 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<黄臣晓>
-- Create date: <2017-11-18 09:48:34>
-- Description:	<累计结款金额>
-- =============================================
ALTER FUNCTION [dbo].[Fun_SA_origSettleAmount]
(
	@projectname nvarchar(20)
)
RETURNS decimal(18,2)
AS
BEGIN
	declare @Acount decimal(18,2)
    
    set @Acount=(select sum(a.origSettleAmount) from ARAP_Detail a
				 inner join aa_project b on a.detailName=b.name
				 where b.name=@projectname)
    
	RETURN @Acount

END
