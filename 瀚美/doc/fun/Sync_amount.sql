USE [UFTData242065_000001]
GO
/****** Object:  UserDefinedFunction [dbo].[Sync_amount]    Script Date: 07/31/2017 16:44:51 ******/
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
	-- Add the parameters for the function here
	@idinventory int,
	@id int
)
RETURNS decimal(18,2)
AS
BEGIN
    declare @amount decimal(18,2),@quantity decimal(18,2)
	select @amount=taxamount,@quantity=quantity from HM_DataImportInfo a left join AA_Inventory b on a.idinventory=b.code
	where b.id=@idinventory 
	and a.id=@id
	if(isnull(@amount,0)<=0)
	begin
		select @amount=retailPriceNew*@quantity from AA_InventoryPrice where idinventory=@idinventory
	end
	RETURN @amount
END
