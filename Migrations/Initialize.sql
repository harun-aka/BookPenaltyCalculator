CREATE TABLE [dbo].[Countries](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Name] [varchar](50) NOT NULL,
	[CurrencyId] [int] NOT NULL,
	[FirstWeekendDayOfWeek] [int] NOT NULL,
	[SecondWeekendDayOfWeek] [int] NOT NULL,
 CONSTRAINT [PK_Countries] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON  ) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Currencies]    Script Date: 22.03.2022 00:05:58 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Currencies](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Name] [varchar](50) NOT NULL,
	[Code] [nvarchar](1) NOT NULL,
	[Value] [money] NOT NULL,
 CONSTRAINT [PK_Currency] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON  ) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Holidays]    Script Date: 22.03.2022 00:05:58 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Holidays](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Name] [varchar](50) NOT NULL,
	[StartDate] [datetime] NOT NULL,
	[EndDate] [datetime] NOT NULL,
	[CountryId] [int] NOT NULL,
 CONSTRAINT [PK_Holidays] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON  ) ON [PRIMARY]
) ON [PRIMARY]
GO
SET IDENTITY_INSERT [dbo].[Countries] ON 

INSERT [dbo].[Countries] ([Id], [Name], [CurrencyId], [FirstWeekendDayOfWeek], [SecondWeekendDayOfWeek]) VALUES (1, N'Turkey', 2, 6, 0)
INSERT [dbo].[Countries] ([Id], [Name], [CurrencyId], [FirstWeekendDayOfWeek], [SecondWeekendDayOfWeek]) VALUES (2, N'USA', 1, 6, 0)
INSERT [dbo].[Countries] ([Id], [Name], [CurrencyId], [FirstWeekendDayOfWeek], [SecondWeekendDayOfWeek]) VALUES (3, N'UAE', 3, 5, 6)
SET IDENTITY_INSERT [dbo].[Countries] OFF
GO
SET IDENTITY_INSERT [dbo].[Currencies] ON 

INSERT [dbo].[Currencies] ([Id], [Name], [Code], [Value]) VALUES (1, N'Dollar', N'$', 1.0000)
INSERT [dbo].[Currencies] ([Id], [Name], [Code], [Value]) VALUES (2, N'Turkish Lira', N'₺', 15.2000)
INSERT [dbo].[Currencies] ([Id], [Name], [Code], [Value]) VALUES (3, N'Uae Dirham', N'd', 3.7400)
SET IDENTITY_INSERT [dbo].[Currencies] OFF
GO
SET IDENTITY_INSERT [dbo].[Holidays] ON 

INSERT [dbo].[Holidays] ([Id], [Name], [StartDate], [EndDate], [CountryId]) VALUES (1, N'New Year', CAST(N'2022-01-01T00:00:00.000' AS DateTime), CAST(N'2022-01-02T00:00:00.000' AS DateTime), 1)
INSERT [dbo].[Holidays] ([Id], [Name], [StartDate], [EndDate], [CountryId]) VALUES (2, N'New Year', CAST(N'2022-01-01T00:00:00.000' AS DateTime), CAST(N'2022-01-01T00:00:00.000' AS DateTime), 2)
INSERT [dbo].[Holidays] ([Id], [Name], [StartDate], [EndDate], [CountryId]) VALUES (3, N'Holiday 1', CAST(N'2022-03-15T00:00:00.000' AS DateTime), CAST(N'2022-03-20T00:00:00.000' AS DateTime), 1)
INSERT [dbo].[Holidays] ([Id], [Name], [StartDate], [EndDate], [CountryId]) VALUES (4, N'Holiday 2', CAST(N'2022-05-12T00:00:00.000' AS DateTime), CAST(N'2022-05-13T00:00:00.000' AS DateTime), 1)
INSERT [dbo].[Holidays] ([Id], [Name], [StartDate], [EndDate], [CountryId]) VALUES (5, N'Holiday 3', CAST(N'2022-09-11T00:00:00.000' AS DateTime), CAST(N'2022-09-22T00:00:00.000' AS DateTime), 1)
SET IDENTITY_INSERT [dbo].[Holidays] OFF
GO
ALTER TABLE [dbo].[Countries]  WITH CHECK ADD  CONSTRAINT [FK_Countries_Countries] FOREIGN KEY([CurrencyId])
REFERENCES [dbo].[Currencies] ([Id])
GO
ALTER TABLE [dbo].[Countries] CHECK CONSTRAINT [FK_Countries_Countries]
GO
ALTER TABLE [dbo].[Holidays]  WITH CHECK ADD  CONSTRAINT [FK_Holidays_Countries] FOREIGN KEY([CountryId])
REFERENCES [dbo].[Countries] ([Id])
GO
ALTER TABLE [dbo].[Holidays] CHECK CONSTRAINT [FK_Holidays_Countries]
GO
USE [master]
GO
ALTER DATABASE [Library] SET  READ_WRITE 
GO
