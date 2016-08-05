USE [University]
GO
/****** Object:  Table [dbo].[courses]    Script Date: 8/4/2016 6:03:52 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[courses](
	[name] [varchar](255) NULL,
	[course_number] [varchar](7) NULL,
	[id] [int] IDENTITY(1,1) NOT NULL
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[courses_students]    Script Date: 8/4/2016 6:03:52 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[courses_students](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[course_id] [int] NULL,
	[student_id] [int] NULL
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[students]    Script Date: 8/4/2016 6:03:52 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[students](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[name] [varchar](255) NULL,
	[doe] [varchar](255) NULL
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
SET IDENTITY_INSERT [dbo].[courses] ON 

INSERT [dbo].[courses] ([name], [course_number], [id]) VALUES (N'english', N'101', 5)
SET IDENTITY_INSERT [dbo].[courses] OFF
SET IDENTITY_INSERT [dbo].[students] ON 

INSERT [dbo].[students] ([id], [name], [doe]) VALUES (1, N'bob', N'1')
INSERT [dbo].[students] ([id], [name], [doe]) VALUES (3, N'3', N'3')
INSERT [dbo].[students] ([id], [name], [doe]) VALUES (4, N'3', N'3')
SET IDENTITY_INSERT [dbo].[students] OFF
