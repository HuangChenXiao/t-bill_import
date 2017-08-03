
/****** Object:  Table [dbo].[HM_DataImportInfo]    Script Date: 08/03/2017 09:39:24 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[HM_DataImportInfo](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[code] [nvarchar](500) NULL,
	[warehouse] [nvarchar](500) NULL,
	[ccusname] [nvarchar](500) NULL,
	[linkman ] [nvarchar](500) NULL,
	[address] [nvarchar](500) NULL,
	[contactphone ] [nvarchar](500) NULL,
	[priuserdefnvc1] [nvarchar](500) NULL,
	[priuserdefnvc2] [nvarchar](500) NULL,
	[priuserdefnvc3] [nvarchar](500) NULL,
	[saleInvoiceNo] [nvarchar](500) NULL,
	[memo] [nvarchar](200) NULL,
	[idinventory ] [nvarchar](500) NULL,
	[inventoryname ] [nvarchar](500) NULL,
	[retailprice] [decimal](28, 14) NULL,
	[quantity] [decimal](28, 14) NULL,
	[priuserdefdecm1] [decimal](28, 14) NULL,
	[taxamount] [decimal](28, 14) NULL,
	[maker] [nvarchar](50) NULL
) ON [PRIMARY]

GO


