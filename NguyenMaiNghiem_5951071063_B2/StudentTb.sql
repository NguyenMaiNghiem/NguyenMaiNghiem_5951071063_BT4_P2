USE [DemoCRUD]
GO

/****** Object:  Table [dbo].[StudentsTb]    Script Date: 22/03/2021 1:28:15 CH ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[StudentsTb](
	[StudentID] [int] IDENTITY(1,1) NOT NULL,
	[Name] [varchar](50) NULL,
	[FatherName] [varchar](50) NULL,
	[RollNumber] [varchar](50) NULL,
	[Address] [varchar](200) NULL,
	[Mobile] [varchar](15) NULL
) ON [PRIMARY]
GO

