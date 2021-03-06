USE [UFTData895739_000005]
GO
/****** Object:  UserDefinedFunction [dbo].[Fun_PU_JieKuanAmount]    Script Date: 11/21/2017 17:33:23 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<黄臣晓>
-- Create date: <2017-11-18 09:48:34>
-- Description:	<采购-累计结款金额>
-- =============================================
ALTER FUNCTION [dbo].[Fun_PU_JieKuanAmount]
(
	@projectname nvarchar(20)
)
RETURNS decimal(18,2)
AS
BEGIN
	declare @Acount decimal(18,2)
    
    set @Acount=(select sum(origAmount) from ARAP_Detail a
				inner join aa_project b on b.id=a.idproject
				where a.idbusitype=1
				and b.name=@projectname
	group by b.name)
    
	RETURN @Acount

END


