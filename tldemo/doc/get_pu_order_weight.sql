SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<�Ƴ���>
-- Create date: <2017-8-18 13:53:52>
-- Description:	<������������ȡʵ�ط���>
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
		set @str_weight_name = substring(@quantity,0,charindex('.',@quantity))+'����'+'0.'+substring(@quantity,charindex('.',@quantity)+1,len(@quantity))+'ʵ��'
    end
	else
	begin
		set @str_weight_name = @quantity +'����'
	end
    
	RETURN @str_weight_name

END
GO
