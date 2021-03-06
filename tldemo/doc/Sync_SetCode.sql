
/****** Object:  StoredProcedure [dbo].[Sync_SetCode]    Script Date: 08/22/2017 09:42:07 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<黄陈晓>
-- Create date: <2017-8-20 14:51:07>
-- Description:	<修改T+表自增长编码>
-- =============================================
ALTER PROCEDURE [dbo].[Sync_SetCode]
	@idvouchertype int--单据id
AS
BEGIN
	SET NOCOUNT ON;
	declare @SerialNumberPrefixion nvarchar(50),@date varchar(10),@count int,@idusedrule int,@voucher_code nvarchar(50),@MaxSerialNumber int
	if(@idvouchertype=27)
		begin
		set @date=(select CONVERT(varchar(7),getdate(),20))
		set @voucher_code='PO-'+@date+'-'
		set @count=(select count(1) from SM_DocSerialNumber where SerialNumberPrefixion=@voucher_code and idvouchertype=@idvouchertype)
		set @idusedrule=(select max(idusedrule) from SM_DocSerialNumber)+1
		set @MaxSerialNumber=(select isnull(MaxSerialNumber,1) from SM_DocSerialNumber where SerialNumberPrefixion=@voucher_code)+1

		select @MaxSerialNumber
		if(@count<=0)
		begin 
		  insert SM_DocSerialNumber (idvouchertype, idusedrule, SerialNumberPrefixion, MaxSerialNumber)
		  values                    (@idvouchertype,@idusedrule,@voucher_code,isnull(@MaxSerialNumber,1))
		end
		else
		begin
		  update SM_DocSerialNumber set MaxSerialNumber=@MaxSerialNumber where SerialNumberPrefixion=@voucher_code
		end
	end
	if(@idvouchertype=112)
		begin
		set @date=(select CONVERT(varchar(7),getdate(),20))
		set @voucher_code='BE-'+@date+'-'
		set @count=(select count(1) from SM_DocSerialNumber where SerialNumberPrefixion=@voucher_code and idvouchertype=@idvouchertype)
		set @idusedrule=(select max(idusedrule) from SM_DocSerialNumber)+1
		set @MaxSerialNumber=(select isnull(MaxSerialNumber,1) from SM_DocSerialNumber where SerialNumberPrefixion=@voucher_code)+1

		select @MaxSerialNumber
		if(@count<=0)
		begin 
		  insert SM_DocSerialNumber (idvouchertype, idusedrule, SerialNumberPrefixion, MaxSerialNumber)
		  values                    (@idvouchertype,@idusedrule,@voucher_code,isnull(@MaxSerialNumber,1))
		end
		else
		begin
		  update SM_DocSerialNumber set MaxSerialNumber=@MaxSerialNumber where SerialNumberPrefixion=@voucher_code
		end
	end
END
