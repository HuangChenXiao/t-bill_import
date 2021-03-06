USE [UFTData242065_000001]
GO
/****** Object:  UserDefinedFunction [dbo].[Sync_amount]    Script Date: 08/01/2017 09:07:03 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date, ,>
-- Description:	<判断金额>
-- =============================================
ALTER FUNCTION [dbo].[Sync_amount]
(
	@id int
)
RETURNS decimal(18,2)
AS
BEGIN
    declare @amount decimal(18,2),@quantity decimal(18,2),@idinventory int
	select @amount=taxamount,@quantity=quantity,@idinventory=b.id from HM_DataImportInfo a left join AA_Inventory b on a.idinventory=b.code
	where a.id=@id
	if(isnull(@amount,0)<=0)
	begin
		select @amount=retailPriceNew*@quantity from AA_InventoryPrice where idinventory=@idinventory
	end
	RETURN @amount
END
