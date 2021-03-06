USE [UFTData242065_000001]
GO
/****** Object:  StoredProcedure [dbo].[Sync_GetCode]    Script Date: 07/29/2017 16:16:20 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
ALTER PROC [dbo].[Sync_GetCode]
	@idvouchertype int,
	@code nvarchar(50) output
as
begin
	declare @date varchar(10),@count int,@maxcode varchar(6)
	set @date=(select CONVERT(varchar(7),getdate(),20))
 --销售订单
 if @idvouchertype=43 
	begin
	 select @count=COUNT(1) from SM_UsedRule where idvouchertype =  N'43' and prefixion1='SO' and prefixion2=@date and prefixion3 =  N'' and separator =  N'-' and codelen = 15 and serialnolength = 4
	 if @count=0
		begin
			insert into SM_UsedRule (prefixion1,prefixion2,prefixion3,separator,codelen,serialnolength,allprefixion,startcode,endcode,idvouchertype)
			values ('SO',@date,'','-',15,4,'SO' + '-' + @date + '-','SO' + '-' + @date + '-0001','SO' + '-' + @date + '-9999',43)
		end
	 set @code='SO' + '-' + @date + '-'

	 select @maxcode=CONVERT(varchar(6),max(convert(int,substring(code,LEN(@code)+1,5)))+10001) from SA_SaleOrder where code like @code + '%'

	 set @code=@code+SUBSTRING(isnull(@maxcode,'10001'),2,5)
     return 0
	end
 --销货单	
 if @idvouchertype=104 
	begin
	 select @count=COUNT(1) from SM_UsedRule where idvouchertype =  N'104' and prefixion1='SA' and prefixion2=@date and prefixion3 =  N'' and separator =  N'-' and codelen = 15 and serialnolength = 4
	 if @count=0
		begin
			insert into SM_UsedRule (prefixion1,prefixion2,prefixion3,separator,codelen,serialnolength,allprefixion,startcode,endcode,idvouchertype)
			values ('SA',@date,'','-',15,4,'SA' + '-' + @date + '-','SA' + '-' + @date + '-00001','SA' + '-' + @date + '-9999',104)
		end
	 set @code='SA' + '-' + @date + '-'
	 
	 select @maxcode=CONVERT(varchar(5),max(convert(int,substring(code,LEN(@code)+1,5)))+10001) from SA_SaleDelivery where code like @code + '%'
	 
	 set @code=@code+SUBSTRING(isnull(@maxcode,'10001'),2,5)
     return 0
	end
 --收款单
 if @idvouchertype=92 
   begin
	 select @count=COUNT(1) from SM_UsedRule where idvouchertype =  N'92' and prefixion1='SK' and prefixion2=@date and prefixion3 =  N'' and separator =  N'-' and codelen = 15 and serialnolength = 4
	 if @count=0
		begin
			insert into SM_UsedRule (prefixion1,prefixion2,prefixion3,separator,codelen,serialnolength,allprefixion,startcode,endcode,idvouchertype)
			values ('SK',@date,'','-',15,4,'SK' + '-' + @date + '-','SK' + '-' + @date + '-0001','SK' + '-' + @date + '-9999',92)
		end
	 set @code='SK' + '-' + @date + '-'
	 
	 select @maxcode=CONVERT(varchar(6),max(convert(int,substring(code,LEN(@code)+1,5)))+10001) from ARAP_ReceivePayment where code like @code + '%'
	 
	 set @code=@code+SUBSTRING(isnull(@maxcode,'10001'),2,5)
     return 0
	end
 --出库单
 if @idvouchertype=19 
   begin
	 select @count=COUNT(1) from SM_UsedRule where idvouchertype =  N'19' and prefixion1='IO' and prefixion2=@date and prefixion3 =  N'' and separator =  N'-' and codelen = 15 and serialnolength = 4
	 if @count=0
		begin
			insert into SM_UsedRule (prefixion1,prefixion2,prefixion3,separator,codelen,serialnolength,allprefixion,startcode,endcode,idvouchertype)
			values ('IO',@date,'','-',15,4,'IO' + '-' + @date + '-','IO' + '-' + @date + '-0001','IO' + '-' + @date + '-9999',19)
		end
	 set @code='IO' + '-' + @date + '-'
	 
	 select @maxcode=CONVERT(varchar(6),max(convert(int,substring(code,LEN(@code)+1,5)))+10001) from ST_RDRecord where code like @code + '%'
	 
	 set @code=@code+SUBSTRING(isnull(@maxcode,'10001'),2,5)
     return 0
	end

end




/****** Object:  StoredProcedure [dbo].[Sync_SA_SaleDelivery]    Script Date: 11/14/2016 14:05:59 ******/
SET ANSI_NULLS ON
