USE [UFTData242065_000001]
GO
/****** Object:  UserDefinedFunction [dbo].[Sync_quantity]    Script Date: 08/01/2017 09:09:49 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date, ,>
-- Description:	<判断金额返回单价>
-- =============================================
CREATE FUNCTION [dbo].[Sync_price]
(
	@id int
)
RETURNS decimal(18,2)
AS
BEGIN
    declare @amount decimal(18,2),@price decimal(18,2),@idinventory int,@quantity decimal(18,2)
	select @amount=taxamount,@idinventory=b.id,@quantity=a.quantity from HM_DataImportInfo a left join AA_Inventory b on a.idinventory=b.code
	where a.id=@id
	if(isnull(@amount,0)<=0)
	begin
		select @price=retailPriceNew from AA_InventoryPrice where idinventory=@idinventory
	end
	else
	begin
		set @price=convert(decimal(18,4),(@amount/@quantity))
	end
	RETURN @price
END

