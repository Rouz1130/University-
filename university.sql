USE [University]
GO
/****** Object:  Table [dbo].[students]    Script Date: 8/2/2016 5:37:12 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[students](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[name] [varchar](255) NULL,
	[doe] [varchar](255) NULL
) ON [PRIMARY]

GO
