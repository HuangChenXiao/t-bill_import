SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<黄臣晓>
-- Create date: <2017-8-18 13:53:52>
-- Description:	<根据数量来获取实重方数>
-- =============================================
ALTER FUNCTION get_pu_order_weight
(
	@quantity nvarchar(50)
)
RETURNS nvarchar(50)
AS
BEGIN
	DECLARE @str_weight_name nvarchar(50),@char_length int
	set @char_length=charindex('.',@quantity)
    if(@char_length>0)
    begin
		set @str_weight_name = substring(@quantity,0,charindex('.',@quantity))+'方数'+'0.'+substring(@quantity,charindex('.',@quantity)+1,len(@quantity))+'实重'
    end
	else
	begin
		set @str_weight_name = @quantity +'方数'
	end
    
	RETURN @str_weight_name

END
GO
