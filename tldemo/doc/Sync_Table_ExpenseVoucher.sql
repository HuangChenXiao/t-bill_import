

/****** Object:  Table [dbo].[Sync_Table_ExpenseVoucher]    Script Date: 08/22/2017 09:42:50 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[Sync_Table_ExpenseVoucher](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[parent_name] [nvarchar](500) NULL,
	[pubuserdefnvc4] [nvarchar](500) NULL,
	[voucherdate] [nvarchar](500) NULL,
	[priuserdefnvc1] [nvarchar](500) NULL,
	[priuserdefnvc2] [nvarchar](500) NULL,
	[priuserdefdecm1] [decimal](18, 4) NULL,
	[priuserdefdecm2] [decimal](18, 4) NULL,
	[priuserdefdecm3] [decimal](18, 4) NULL,
	[pubuserdefdecm3] [decimal](18, 4) NULL,
	[pubuserdefdecm4] [decimal](18, 4) NULL,
	[priuserdefnvc3] [nvarchar](500) NULL,
	[priuserdefnvc4] [nvarchar](500) NULL,
	[pubuserdefnvc1] [nvarchar](500) NULL,
	[pubuserdefdecm1] [decimal](18, 4) NULL,
	[pubuserdefnvc3] [nvarchar](500) NULL,
	[pubuserdefnvc2] [nvarchar](500) NULL,
	[price] [decimal](18, 4) NULL,
	[money] [decimal](18, 4) NULL,
	[maker] [nvarchar](500) NULL,
 CONSTRAINT [PK_Sync_Table_ExpenseVoucher] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]

GO


