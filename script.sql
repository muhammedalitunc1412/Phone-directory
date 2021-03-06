USE [TelefonRehberi]
GO
/****** Object:  Table [dbo].[Admin]    Script Date: 14.01.2020 22:29:48 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Admin](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[username] [nvarchar](max) NOT NULL,
	[password] [nvarchar](max) NOT NULL,
 CONSTRAINT [PK_Admin] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Calisanlar]    Script Date: 14.01.2020 22:29:48 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Calisanlar](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[ad] [nvarchar](max) NOT NULL,
	[soyad] [nvarchar](max) NOT NULL,
	[telefon] [nvarchar](max) NOT NULL,
	[departmanId] [int] NOT NULL,
	[yoneticiId] [int] NULL,
 CONSTRAINT [PK_Calisanlar] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Departmanlar]    Script Date: 14.01.2020 22:29:48 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Departmanlar](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[adi] [nvarchar](max) NOT NULL,
 CONSTRAINT [PK_Departmanlar] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
SET IDENTITY_INSERT [dbo].[Admin] ON 

INSERT [dbo].[Admin] ([id], [username], [password]) VALUES (1, N'Muhammed', N'123456')
SET IDENTITY_INSERT [dbo].[Admin] OFF
SET IDENTITY_INSERT [dbo].[Calisanlar] ON 

INSERT [dbo].[Calisanlar] ([id], [ad], [soyad], [telefon], [departmanId], [yoneticiId]) VALUES (1, N'Muhammed Ali', N'Tunç', N'(423) 342-3234', 2, NULL)
INSERT [dbo].[Calisanlar] ([id], [ad], [soyad], [telefon], [departmanId], [yoneticiId]) VALUES (2, N'Sefa', N'Samed', N'(545) 635-5465', 1, 1)
INSERT [dbo].[Calisanlar] ([id], [ad], [soyad], [telefon], [departmanId], [yoneticiId]) VALUES (3, N'Sefer', N'Porsuk', N'(564) 676-5756', 2, 2)
SET IDENTITY_INSERT [dbo].[Calisanlar] OFF
SET IDENTITY_INSERT [dbo].[Departmanlar] ON 

INSERT [dbo].[Departmanlar] ([id], [adi]) VALUES (1, N'Finans')
INSERT [dbo].[Departmanlar] ([id], [adi]) VALUES (2, N'Yazılım')
SET IDENTITY_INSERT [dbo].[Departmanlar] OFF
ALTER TABLE [dbo].[Calisanlar]  WITH CHECK ADD  CONSTRAINT [FK_Calisanlar_Departmanlar] FOREIGN KEY([departmanId])
REFERENCES [dbo].[Departmanlar] ([id])
GO
ALTER TABLE [dbo].[Calisanlar] CHECK CONSTRAINT [FK_Calisanlar_Departmanlar]
GO
