USE [UFTData895739_000005]
GO
/****** Object:  UserDefinedFunction [dbo].[Fun_ST_RDRecordCaiLiao]    Script Date: 11/21/2017 17:33:49 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<黄臣晓>
-- Create date: <2017-11-18 09:48:34>
-- Description:	<材料成本>
-- =============================================
ALTER FUNCTION [dbo].[Fun_ST_RDRecordCaiLiao]
(
	@projectname nvarchar(20)
)
RETURNS decimal(18,2)
AS
BEGIN
	declare @Acount decimal(18,2)
    
    set @Acount=(select sum(a.amount) from ST_RDRecord_b a
			     inner join ST_RDRecord b on a.idRDRecordDTO=b.id
				 inner join aa_project c on a.idproject=c.id
				 where c.name=@projectname
				 and b.idvouchertype =21
				 and b.voucherState=189)
    
	RETURN @Acount

END

