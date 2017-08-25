

/****** Object:  Table [dbo].[Sync_Table_PurchaseOrder]    Script Date: 08/25/2017 15:43:54 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[Sync_Table_PurchaseOrder](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[voucherdate] [datetime] NULL,
	[clerk_name] [nvarchar](500) NULL,
	[partner_name] [nvarchar](500) NULL,
	[pubuserdefdecm2] [decimal](18, 4) NULL,
	[cinvcode] [nvarchar](500) NULL,
	[cinvname] [nvarchar](500) NULL,
	[priuserdefnvc1] [nvarchar](500) NULL,
	[freeItem1] [nvarchar](300) NULL,
	[freeItem2] [nvarchar](300) NULL,
	[freeItem3] [nvarchar](300) NULL,
	[quantity] [decimal](18, 4) NULL,
	[pubuserdefdecm1] [decimal](18, 4) NULL,
	[quantity2] [decimal](18, 4) NULL,
	[price] [decimal](18, 4) NULL,
	[amount] [decimal](18, 4) NULL,
	[pubuserdefdecm3] [decimal](18, 4) NULL,
	[pubuserdefdecm4] [decimal](18, 4) NULL,
	[priuserdefdecm1] [decimal](18, 4) NULL,
	[priuserdefdecm2] [decimal](18, 4) NULL,
	[maker] [nvarchar](500) NULL,
 CONSTRAINT [PK_Sync_Table_PurchaseOrder] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]

GO


