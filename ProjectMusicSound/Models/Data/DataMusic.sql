USE [master]
GO
/****** Object:  Database [MusicData]    Script Date: 01/13/2021 10:29:42 AM ******/
CREATE DATABASE [MusicData]
 CONTAINMENT = NONE
 ON  PRIMARY 
( NAME = N'MusicData', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL11.SQLEXPRESS\MSSQL\DATA\MusicData.mdf' , SIZE = 4096KB , MAXSIZE = UNLIMITED, FILEGROWTH = 1024KB )
 LOG ON 
( NAME = N'MusicData_log', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL11.SQLEXPRESS\MSSQL\DATA\MusicData_log.ldf' , SIZE = 1024KB , MAXSIZE = 2048GB , FILEGROWTH = 10%)
GO
ALTER DATABASE [MusicData] SET COMPATIBILITY_LEVEL = 110
GO
IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [MusicData].[dbo].[sp_fulltext_database] @action = 'enable'
end
GO
ALTER DATABASE [MusicData] SET ANSI_NULL_DEFAULT OFF 
GO
ALTER DATABASE [MusicData] SET ANSI_NULLS OFF 
GO
ALTER DATABASE [MusicData] SET ANSI_PADDING OFF 
GO
ALTER DATABASE [MusicData] SET ANSI_WARNINGS OFF 
GO
ALTER DATABASE [MusicData] SET ARITHABORT OFF 
GO
ALTER DATABASE [MusicData] SET AUTO_CLOSE OFF 
GO
ALTER DATABASE [MusicData] SET AUTO_CREATE_STATISTICS ON 
GO
ALTER DATABASE [MusicData] SET AUTO_SHRINK OFF 
GO
ALTER DATABASE [MusicData] SET AUTO_UPDATE_STATISTICS ON 
GO
ALTER DATABASE [MusicData] SET CURSOR_CLOSE_ON_COMMIT OFF 
GO
ALTER DATABASE [MusicData] SET CURSOR_DEFAULT  GLOBAL 
GO
ALTER DATABASE [MusicData] SET CONCAT_NULL_YIELDS_NULL OFF 
GO
ALTER DATABASE [MusicData] SET NUMERIC_ROUNDABORT OFF 
GO
ALTER DATABASE [MusicData] SET QUOTED_IDENTIFIER OFF 
GO
ALTER DATABASE [MusicData] SET RECURSIVE_TRIGGERS OFF 
GO
ALTER DATABASE [MusicData] SET  DISABLE_BROKER 
GO
ALTER DATABASE [MusicData] SET AUTO_UPDATE_STATISTICS_ASYNC OFF 
GO
ALTER DATABASE [MusicData] SET DATE_CORRELATION_OPTIMIZATION OFF 
GO
ALTER DATABASE [MusicData] SET TRUSTWORTHY OFF 
GO
ALTER DATABASE [MusicData] SET ALLOW_SNAPSHOT_ISOLATION OFF 
GO
ALTER DATABASE [MusicData] SET PARAMETERIZATION SIMPLE 
GO
ALTER DATABASE [MusicData] SET READ_COMMITTED_SNAPSHOT OFF 
GO
ALTER DATABASE [MusicData] SET HONOR_BROKER_PRIORITY OFF 
GO
ALTER DATABASE [MusicData] SET RECOVERY SIMPLE 
GO
ALTER DATABASE [MusicData] SET  MULTI_USER 
GO
ALTER DATABASE [MusicData] SET PAGE_VERIFY CHECKSUM  
GO
ALTER DATABASE [MusicData] SET DB_CHAINING OFF 
GO
ALTER DATABASE [MusicData] SET FILESTREAM( NON_TRANSACTED_ACCESS = OFF ) 
GO
ALTER DATABASE [MusicData] SET TARGET_RECOVERY_TIME = 0 SECONDS 
GO
USE [MusicData]
GO
/****** Object:  Table [dbo].[Author]    Script Date: 01/13/2021 10:29:42 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Author](
	[author_id] [int] IDENTITY(1,1) NOT NULL,
	[author_name] [nvarchar](200) NULL,
	[author_active] [bit] NULL,
	[author_bin] [bit] NULL,
	[author_note] [nvarchar](max) NULL,
 CONSTRAINT [PK_Author] PRIMARY KEY CLUSTERED 
(
	[author_id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
/****** Object:  Table [dbo].[Category]    Script Date: 01/13/2021 10:29:42 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Category](
	[category_id] [int] IDENTITY(1,1) NOT NULL,
	[category_name] [nvarchar](200) NULL,
	[category_active] [bit] NULL,
	[category_bin] [bit] NULL,
	[category_note] [nvarchar](max) NULL,
	[category_view] [int] NULL,
 CONSTRAINT [PK_Category] PRIMARY KEY CLUSTERED 
(
	[category_id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
/****** Object:  Table [dbo].[Music]    Script Date: 01/13/2021 10:29:42 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Music](
	[music_id] [int] IDENTITY(1,1) NOT NULL,
	[music_name] [nvarchar](300) NULL,
	[music_img] [nvarchar](max) NULL,
	[music_lyric] [nvarchar](max) NULL,
	[music_time] [nvarchar](30) NULL,
	[music_view] [int] NULL,
	[music_dowload] [int] NULL,
	[music_love] [int] NULL,
	[user_id] [int] NULL,
	[music_linkdow] [nvarchar](max) NULL,
	[music_datecreate] [date] NULL,
	[music_dateedit] [date] NULL,
	[music_active] [bit] NULL,
	[music_bin] [bit] NULL,
	[music_option] [bit] NULL,
 CONSTRAINT [PK_Music] PRIMARY KEY CLUSTERED 
(
	[music_id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
/****** Object:  Table [dbo].[Music_Author]    Script Date: 01/13/2021 10:29:42 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Music_Author](
	[ma_id] [int] IDENTITY(1,1) NOT NULL,
	[music_id] [int] NULL,
	[author_id] [int] NULL,
 CONSTRAINT [PK_Music_Author] PRIMARY KEY CLUSTERED 
(
	[ma_id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[Music_Category]    Script Date: 01/13/2021 10:29:42 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Music_Category](
	[mc_id] [int] IDENTITY(1,1) NOT NULL,
	[music_id] [int] NULL,
	[category_id] [int] NULL,
 CONSTRAINT [PK_Music_Category] PRIMARY KEY CLUSTERED 
(
	[mc_id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[Music_Singer]    Script Date: 01/13/2021 10:29:42 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Music_Singer](
	[ms_id] [int] IDENTITY(1,1) NOT NULL,
	[music_id] [int] NULL,
	[singer_id] [int] NULL,
 CONSTRAINT [PK_Music_Singer] PRIMARY KEY CLUSTERED 
(
	[ms_id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[Profile]    Script Date: 01/13/2021 10:29:42 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[Profile](
	[profile_id] [int] IDENTITY(1,1) NOT NULL,
	[profile_birth] [date] NULL,
	[profile_phone] [varchar](12) NULL,
	[profile_name] [nvarchar](200) NULL,
	[profile_lastname] [nvarchar](200) NULL,
	[profile_note] [nvarchar](max) NULL,
	[profile_favorite] [nvarchar](max) NULL,
	[sex_id] [int] NULL,
	[user_id] [int] NULL,
	[profile_address] [nvarchar](max) NULL,
	[profile_point] [int] NULL,
 CONSTRAINT [PK_Profile] PRIMARY KEY CLUSTERED 
(
	[profile_id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[Role]    Script Date: 01/13/2021 10:29:42 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Role](
	[role_id] [int] IDENTITY(1,1) NOT NULL,
	[role_name] [nvarchar](200) NULL,
	[role_option] [bit] NULL,
	[role_bin] [bit] NULL,
	[role_active] [bit] NULL,
 CONSTRAINT [PK_Role] PRIMARY KEY CLUSTERED 
(
	[role_id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[Sex]    Script Date: 01/13/2021 10:29:42 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Sex](
	[sex_id] [int] IDENTITY(1,1) NOT NULL,
	[sex_name] [nvarchar](10) NULL,
 CONSTRAINT [PK_Sex] PRIMARY KEY CLUSTERED 
(
	[sex_id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[Singer]    Script Date: 01/13/2021 10:29:42 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Singer](
	[singer_id] [int] IDENTITY(1,1) NOT NULL,
	[singer_name] [nvarchar](200) NULL,
	[singer_active] [bit] NULL,
	[singer_bin] [bit] NULL,
	[singer_note] [nvarchar](max) NULL,
 CONSTRAINT [PK_Singer] PRIMARY KEY CLUSTERED 
(
	[singer_id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
/****** Object:  Table [dbo].[Users]    Script Date: 01/13/2021 10:29:42 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[Users](
	[user_id] [int] IDENTITY(1,1) NOT NULL,
	[user_name] [nvarchar](200) NULL,
	[user_img] [nvarchar](max) NULL,
	[user_email] [varchar](200) NULL,
	[user_pass] [nvarchar](max) NULL,
	[user_token] [nvarchar](max) NULL,
	[user_datecreate] [datetime] NULL,
	[user_datelogin] [datetime] NULL,
	[user_active] [bit] NULL,
	[user_option] [bit] NULL,
	[user_bin] [bit] NULL,
	[role_id] [int] NULL,
	[user_code] [nvarchar](200) NULL,
 CONSTRAINT [PK_Users] PRIMARY KEY CLUSTERED 
(
	[user_id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
SET IDENTITY_INSERT [dbo].[Category] ON 

INSERT [dbo].[Category] ([category_id], [category_name], [category_active], [category_bin], [category_note], [category_view]) VALUES (1, N'Âu Mỹ', 1, 0, N'Kiem tra', 0)
INSERT [dbo].[Category] ([category_id], [category_name], [category_active], [category_bin], [category_note], [category_view]) VALUES (2, N'Việt Nam', 1, 0, NULL, 0)
INSERT [dbo].[Category] ([category_id], [category_name], [category_active], [category_bin], [category_note], [category_view]) VALUES (3, N'Nhạc Trẻ', 1, 0, NULL, 0)
INSERT [dbo].[Category] ([category_id], [category_name], [category_active], [category_bin], [category_note], [category_view]) VALUES (4, N'Hàn', 1, 0, NULL, 0)
SET IDENTITY_INSERT [dbo].[Category] OFF
SET IDENTITY_INSERT [dbo].[Music] ON 

INSERT [dbo].[Music] ([music_id], [music_name], [music_img], [music_lyric], [music_time], [music_view], [music_dowload], [music_love], [user_id], [music_linkdow], [music_datecreate], [music_dateedit], [music_active], [music_bin], [music_option]) VALUES (1, N'Attendtion', N'Charlie_Puth_-_Attention_(Official_Single_Cover).png', N'adadadada', N'4:30', 0, NULL, 0, 34, NULL, CAST(N'2021-01-07' AS Date), CAST(N'2021-01-07' AS Date), 1, 0, 1)
INSERT [dbo].[Music] ([music_id], [music_name], [music_img], [music_lyric], [music_time], [music_view], [music_dowload], [music_love], [user_id], [music_linkdow], [music_datecreate], [music_dateedit], [music_active], [music_bin], [music_option]) VALUES (2, N'Attendtion', N'Charlie_Puth_-_Attention_(Official_Single_Cover).png', N'aasasasasas', N'4:30', 0, NULL, 0, 34, NULL, CAST(N'2021-01-07' AS Date), CAST(N'2021-01-07' AS Date), 1, 0, 1)
INSERT [dbo].[Music] ([music_id], [music_name], [music_img], [music_lyric], [music_time], [music_view], [music_dowload], [music_love], [user_id], [music_linkdow], [music_datecreate], [music_dateedit], [music_active], [music_bin], [music_option]) VALUES (3, N'Attendtion', N'Charlie_Puth_-_Attention_(Official_Single_Cover).png', N'adadadadadada', N'4:30', 0, NULL, 0, 34, NULL, CAST(N'2021-01-07' AS Date), CAST(N'2021-01-07' AS Date), 1, 0, 1)
INSERT [dbo].[Music] ([music_id], [music_name], [music_img], [music_lyric], [music_time], [music_view], [music_dowload], [music_love], [user_id], [music_linkdow], [music_datecreate], [music_dateedit], [music_active], [music_bin], [music_option]) VALUES (4, N'Attendtioncs', N'Charlie_Puth_-_Attention_(Official_Single_Cover).png', N'sffsfsfsfssfsffsfssfsf', N'4:30', 0, NULL, 0, 34, N'Attention-CharliePuth-6429177.mp3', CAST(N'2021-01-08' AS Date), CAST(N'2021-01-08' AS Date), 1, 0, 1)
INSERT [dbo].[Music] ([music_id], [music_name], [music_img], [music_lyric], [music_time], [music_view], [music_dowload], [music_love], [user_id], [music_linkdow], [music_datecreate], [music_dateedit], [music_active], [music_bin], [music_option]) VALUES (5, N'Attendtions', N'Charlie_Puth_-_Attention_(Official_Single_Cover).png', N'aSSAASDASDASDADASD', N'4:30', 0, NULL, 0, 34, N'Attention-CharliePuth-6429177.mp3', CAST(N'2021-01-08' AS Date), CAST(N'2021-01-08' AS Date), 1, 0, 1)
INSERT [dbo].[Music] ([music_id], [music_name], [music_img], [music_lyric], [music_time], [music_view], [music_dowload], [music_love], [user_id], [music_linkdow], [music_datecreate], [music_dateedit], [music_active], [music_bin], [music_option]) VALUES (6, N'Test', NULL, N'fwfwfwfwfwfwfwf', N'4:30', 0, NULL, 0, 36, N'Attention-CharliePuth-6429177.mp3', CAST(N'2021-01-11' AS Date), CAST(N'2021-01-11' AS Date), 1, 0, 0)
SET IDENTITY_INSERT [dbo].[Music] OFF
SET IDENTITY_INSERT [dbo].[Music_Category] ON 

INSERT [dbo].[Music_Category] ([mc_id], [music_id], [category_id]) VALUES (2, 4, 1)
INSERT [dbo].[Music_Category] ([mc_id], [music_id], [category_id]) VALUES (3, 4, 3)
INSERT [dbo].[Music_Category] ([mc_id], [music_id], [category_id]) VALUES (4, 6, 2)
INSERT [dbo].[Music_Category] ([mc_id], [music_id], [category_id]) VALUES (5, 6, 3)
INSERT [dbo].[Music_Category] ([mc_id], [music_id], [category_id]) VALUES (6, 6, 4)
SET IDENTITY_INSERT [dbo].[Music_Category] OFF
SET IDENTITY_INSERT [dbo].[Music_Singer] ON 

INSERT [dbo].[Music_Singer] ([ms_id], [music_id], [singer_id]) VALUES (1, 1, 1)
SET IDENTITY_INSERT [dbo].[Music_Singer] OFF
SET IDENTITY_INSERT [dbo].[Profile] ON 

INSERT [dbo].[Profile] ([profile_id], [profile_birth], [profile_phone], [profile_name], [profile_lastname], [profile_note], [profile_favorite], [sex_id], [user_id], [profile_address], [profile_point]) VALUES (1, CAST(N'1999-06-21' AS Date), N'0364338950', N'Phương Vy', N'Nguyễn Trần', N'"Xin Chào"', N'Nhạc trẻ, Kpop', 3, 34, N'Số 14, Đường số 9, Phường Hiệp Thành Bình Dương', 150)
INSERT [dbo].[Profile] ([profile_id], [profile_birth], [profile_phone], [profile_name], [profile_lastname], [profile_note], [profile_favorite], [sex_id], [user_id], [profile_address], [profile_point]) VALUES (2, NULL, NULL, NULL, NULL, NULL, NULL, NULL, 36, NULL, 0)
SET IDENTITY_INSERT [dbo].[Profile] OFF
SET IDENTITY_INSERT [dbo].[Role] ON 

INSERT [dbo].[Role] ([role_id], [role_name], [role_option], [role_bin], [role_active]) VALUES (1, N'User', 1, 0, 1)
INSERT [dbo].[Role] ([role_id], [role_name], [role_option], [role_bin], [role_active]) VALUES (2, N'Admin', 1, 0, 1)
SET IDENTITY_INSERT [dbo].[Role] OFF
SET IDENTITY_INSERT [dbo].[Sex] ON 

INSERT [dbo].[Sex] ([sex_id], [sex_name]) VALUES (1, N'Nam')
INSERT [dbo].[Sex] ([sex_id], [sex_name]) VALUES (2, N'Nữ')
INSERT [dbo].[Sex] ([sex_id], [sex_name]) VALUES (3, N'Không Rõ')
SET IDENTITY_INSERT [dbo].[Sex] OFF
SET IDENTITY_INSERT [dbo].[Singer] ON 

INSERT [dbo].[Singer] ([singer_id], [singer_name], [singer_active], [singer_bin], [singer_note]) VALUES (1, N'Test', 1, 0, NULL)
SET IDENTITY_INSERT [dbo].[Singer] OFF
SET IDENTITY_INSERT [dbo].[Users] ON 

INSERT [dbo].[Users] ([user_id], [user_name], [user_img], [user_email], [user_pass], [user_token], [user_datecreate], [user_datelogin], [user_active], [user_option], [user_bin], [role_id], [user_code]) VALUES (34, N'Phương Vy', N'userImg.png', N'a@gmail.com', N'Phuongvy99**', N'3672129e-f557-42c3-b653-047d9f8dd183', CAST(N'2021-01-04 14:13:46.283' AS DateTime), CAST(N'2021-01-08 13:23:58.980' AS DateTime), 1, 1, 0, 1, N'MS-34')
INSERT [dbo].[Users] ([user_id], [user_name], [user_img], [user_email], [user_pass], [user_token], [user_datecreate], [user_datelogin], [user_active], [user_option], [user_bin], [role_id], [user_code]) VALUES (35, N'TestTK', N'449anh-nen-dep-20201006125142.jpeg', N'Phuongvy99@gmail.com', N'Phuongvy99^^', N'5779ce72-b24a-45f8-adcf-fe6e5e5a4e17', CAST(N'2021-01-10 17:58:38.780' AS DateTime), CAST(N'2021-01-10 17:58:38.780' AS DateTime), 1, 1, 0, 1, N'MS-35')
INSERT [dbo].[Users] ([user_id], [user_name], [user_img], [user_email], [user_pass], [user_token], [user_datecreate], [user_datelogin], [user_active], [user_option], [user_bin], [role_id], [user_code]) VALUES (36, N'Test_2', N'userImg.png', N'test2@gmail.com', N'Test12345678**', N'7f7a4d24-d73e-466a-a787-3a043beafba5', CAST(N'2021-01-11 15:07:30.813' AS DateTime), CAST(N'2021-01-11 15:07:30.813' AS DateTime), 1, 1, 0, 1, N'MS-36')
SET IDENTITY_INSERT [dbo].[Users] OFF
ALTER TABLE [dbo].[Music]  WITH CHECK ADD  CONSTRAINT [FK_Music_Users] FOREIGN KEY([user_id])
REFERENCES [dbo].[Users] ([user_id])
GO
ALTER TABLE [dbo].[Music] CHECK CONSTRAINT [FK_Music_Users]
GO
ALTER TABLE [dbo].[Music_Author]  WITH CHECK ADD  CONSTRAINT [FK_Music_Author_Author] FOREIGN KEY([author_id])
REFERENCES [dbo].[Author] ([author_id])
GO
ALTER TABLE [dbo].[Music_Author] CHECK CONSTRAINT [FK_Music_Author_Author]
GO
ALTER TABLE [dbo].[Music_Author]  WITH CHECK ADD  CONSTRAINT [FK_Music_Author_Music] FOREIGN KEY([music_id])
REFERENCES [dbo].[Music] ([music_id])
GO
ALTER TABLE [dbo].[Music_Author] CHECK CONSTRAINT [FK_Music_Author_Music]
GO
ALTER TABLE [dbo].[Music_Category]  WITH CHECK ADD  CONSTRAINT [FK_Music_Category_Category] FOREIGN KEY([category_id])
REFERENCES [dbo].[Category] ([category_id])
GO
ALTER TABLE [dbo].[Music_Category] CHECK CONSTRAINT [FK_Music_Category_Category]
GO
ALTER TABLE [dbo].[Music_Category]  WITH CHECK ADD  CONSTRAINT [FK_Music_Category_Music] FOREIGN KEY([music_id])
REFERENCES [dbo].[Music] ([music_id])
GO
ALTER TABLE [dbo].[Music_Category] CHECK CONSTRAINT [FK_Music_Category_Music]
GO
ALTER TABLE [dbo].[Music_Singer]  WITH CHECK ADD  CONSTRAINT [FK_Music_Singer_Music] FOREIGN KEY([music_id])
REFERENCES [dbo].[Music] ([music_id])
GO
ALTER TABLE [dbo].[Music_Singer] CHECK CONSTRAINT [FK_Music_Singer_Music]
GO
ALTER TABLE [dbo].[Music_Singer]  WITH CHECK ADD  CONSTRAINT [FK_Music_Singer_Singer] FOREIGN KEY([singer_id])
REFERENCES [dbo].[Singer] ([singer_id])
GO
ALTER TABLE [dbo].[Music_Singer] CHECK CONSTRAINT [FK_Music_Singer_Singer]
GO
ALTER TABLE [dbo].[Profile]  WITH CHECK ADD  CONSTRAINT [FK_Profile_Sex] FOREIGN KEY([sex_id])
REFERENCES [dbo].[Sex] ([sex_id])
GO
ALTER TABLE [dbo].[Profile] CHECK CONSTRAINT [FK_Profile_Sex]
GO
ALTER TABLE [dbo].[Profile]  WITH CHECK ADD  CONSTRAINT [FK_Profile_Users] FOREIGN KEY([user_id])
REFERENCES [dbo].[Users] ([user_id])
GO
ALTER TABLE [dbo].[Profile] CHECK CONSTRAINT [FK_Profile_Users]
GO
ALTER TABLE [dbo].[Users]  WITH CHECK ADD  CONSTRAINT [FK_Users_Role] FOREIGN KEY([role_id])
REFERENCES [dbo].[Role] ([role_id])
GO
ALTER TABLE [dbo].[Users] CHECK CONSTRAINT [FK_Users_Role]
GO
USE [master]
GO
ALTER DATABASE [MusicData] SET  READ_WRITE 
GO
