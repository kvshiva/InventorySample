USE [master]
GO
/****** Object:  Database [InventorySample]    Script Date: 8/17/2024 3:28:58 PM ******/
CREATE DATABASE [InventorySample]
 CONTAINMENT = NONE
 ON  PRIMARY 
( NAME = N'InventorySample', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL16.MSSQLSERVER\MSSQL\DATA\InventorySample.mdf' , SIZE = 8192KB , MAXSIZE = UNLIMITED, FILEGROWTH = 65536KB )
 LOG ON 
( NAME = N'InventorySample_log', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL16.MSSQLSERVER\MSSQL\DATA\InventorySample_log.ldf' , SIZE = 8192KB , MAXSIZE = 2048GB , FILEGROWTH = 65536KB )
 WITH CATALOG_COLLATION = DATABASE_DEFAULT, LEDGER = OFF
GO
ALTER DATABASE [InventorySample] SET COMPATIBILITY_LEVEL = 160
GO
IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [InventorySample].[dbo].[sp_fulltext_database] @action = 'enable'
end
GO
ALTER DATABASE [InventorySample] SET ANSI_NULL_DEFAULT OFF 
GO
ALTER DATABASE [InventorySample] SET ANSI_NULLS OFF 
GO
ALTER DATABASE [InventorySample] SET ANSI_PADDING OFF 
GO
ALTER DATABASE [InventorySample] SET ANSI_WARNINGS OFF 
GO
ALTER DATABASE [InventorySample] SET ARITHABORT OFF 
GO
ALTER DATABASE [InventorySample] SET AUTO_CLOSE OFF 
GO
ALTER DATABASE [InventorySample] SET AUTO_SHRINK OFF 
GO
ALTER DATABASE [InventorySample] SET AUTO_UPDATE_STATISTICS ON 
GO
ALTER DATABASE [InventorySample] SET CURSOR_CLOSE_ON_COMMIT OFF 
GO
ALTER DATABASE [InventorySample] SET CURSOR_DEFAULT  GLOBAL 
GO
ALTER DATABASE [InventorySample] SET CONCAT_NULL_YIELDS_NULL OFF 
GO
ALTER DATABASE [InventorySample] SET NUMERIC_ROUNDABORT OFF 
GO
ALTER DATABASE [InventorySample] SET QUOTED_IDENTIFIER OFF 
GO
ALTER DATABASE [InventorySample] SET RECURSIVE_TRIGGERS OFF 
GO
ALTER DATABASE [InventorySample] SET  DISABLE_BROKER 
GO
ALTER DATABASE [InventorySample] SET AUTO_UPDATE_STATISTICS_ASYNC OFF 
GO
ALTER DATABASE [InventorySample] SET DATE_CORRELATION_OPTIMIZATION OFF 
GO
ALTER DATABASE [InventorySample] SET TRUSTWORTHY OFF 
GO
ALTER DATABASE [InventorySample] SET ALLOW_SNAPSHOT_ISOLATION OFF 
GO
ALTER DATABASE [InventorySample] SET PARAMETERIZATION SIMPLE 
GO
ALTER DATABASE [InventorySample] SET READ_COMMITTED_SNAPSHOT OFF 
GO
ALTER DATABASE [InventorySample] SET HONOR_BROKER_PRIORITY OFF 
GO
ALTER DATABASE [InventorySample] SET RECOVERY FULL 
GO
ALTER DATABASE [InventorySample] SET  MULTI_USER 
GO
ALTER DATABASE [InventorySample] SET PAGE_VERIFY CHECKSUM  
GO
ALTER DATABASE [InventorySample] SET DB_CHAINING OFF 
GO
ALTER DATABASE [InventorySample] SET FILESTREAM( NON_TRANSACTED_ACCESS = OFF ) 
GO
ALTER DATABASE [InventorySample] SET TARGET_RECOVERY_TIME = 60 SECONDS 
GO
ALTER DATABASE [InventorySample] SET DELAYED_DURABILITY = DISABLED 
GO
ALTER DATABASE [InventorySample] SET ACCELERATED_DATABASE_RECOVERY = OFF  
GO
EXEC sys.sp_db_vardecimal_storage_format N'InventorySample', N'ON'
GO
ALTER DATABASE [InventorySample] SET QUERY_STORE = ON
GO
ALTER DATABASE [InventorySample] SET QUERY_STORE (OPERATION_MODE = READ_WRITE, CLEANUP_POLICY = (STALE_QUERY_THRESHOLD_DAYS = 30), DATA_FLUSH_INTERVAL_SECONDS = 900, INTERVAL_LENGTH_MINUTES = 60, MAX_STORAGE_SIZE_MB = 1000, QUERY_CAPTURE_MODE = AUTO, SIZE_BASED_CLEANUP_MODE = AUTO, MAX_PLANS_PER_QUERY = 200, WAIT_STATS_CAPTURE_MODE = ON)
GO
USE [InventorySample]
GO
/****** Object:  Schema [General]    Script Date: 8/17/2024 3:28:58 PM ******/
CREATE SCHEMA [General]
GO
/****** Object:  Schema [Part]    Script Date: 8/17/2024 3:28:58 PM ******/
CREATE SCHEMA [Part]
GO
/****** Object:  Schema [State]    Script Date: 8/17/2024 3:28:58 PM ******/
CREATE SCHEMA [State]
GO
/****** Object:  Schema [Store]    Script Date: 8/17/2024 3:28:58 PM ******/
CREATE SCHEMA [Store]
GO
/****** Object:  Table [General].[User]    Script Date: 8/17/2024 3:28:58 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [General].[User](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Version] [timestamp] NOT NULL,
	[CreatedBy] [nvarchar](150) NOT NULL,
	[CreatedDateTime] [nvarchar](20) NOT NULL,
	[UpdatedBy] [nvarchar](150) NULL,
	[UpdatedDateTime] [nvarchar](20) NULL,
	[FullName] [nvarchar](400) NOT NULL,
	[Picture] [nvarchar](max) NULL,
	[Code] [int] NOT NULL,
	[Mobile] [nvarchar](16) NOT NULL,
 CONSTRAINT [PK_User] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [Part].[Category]    Script Date: 8/17/2024 3:28:58 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [Part].[Category](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Title] [nvarchar](100) NOT NULL,
	[Version] [timestamp] NOT NULL,
	[CreatedBy] [nvarchar](150) NOT NULL,
	[CreatedDateTime] [nvarchar](20) NOT NULL,
	[UpdatedBy] [nvarchar](150) NULL,
	[UpdatedDateTime] [nvarchar](20) NULL,
 CONSTRAINT [PK_Category] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [Part].[CountUnit]    Script Date: 8/17/2024 3:28:58 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [Part].[CountUnit](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Title] [nvarchar](50) NOT NULL,
	[Version] [timestamp] NOT NULL,
	[CreatedBy] [nvarchar](150) NOT NULL,
	[CreatedDateTime] [nvarchar](20) NOT NULL,
	[UpdatedBy] [nvarchar](150) NULL,
	[UpdatedDateTime] [nvarchar](20) NULL,
 CONSTRAINT [PK_CountUnit] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [Part].[Part]    Script Date: 8/17/2024 3:28:58 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [Part].[Part](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Title] [nvarchar](100) NOT NULL,
	[Code] [nvarchar](200) NOT NULL,
	[MainCountUnitId] [int] NOT NULL,
	[SecondaryCountUnitId] [int] NULL,
	[CategoryId] [int] NOT NULL,
	[HasSerial] [bit] NOT NULL,
	[Version] [timestamp] NOT NULL,
	[CreatedBy] [nvarchar](150) NOT NULL,
	[CreatedDateTime] [nvarchar](20) NOT NULL,
	[UpdatedBy] [nvarchar](150) NULL,
	[UpdatedDateTime] [nvarchar](20) NULL,
 CONSTRAINT [PK_Part] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [Part].[PartStore]    Script Date: 8/17/2024 3:28:58 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [Part].[PartStore](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[PartId] [int] NOT NULL,
	[StoreId] [int] NULL,
	[IsActive] [bit] NOT NULL,
	[IsDefault] [bit] NOT NULL,
	[Comment] [nvarchar](1000) NULL,
	[Version] [timestamp] NOT NULL,
	[CreatedBy] [nvarchar](150) NOT NULL,
	[CreatedDateTime] [nvarchar](20) NOT NULL,
	[UpdatedBy] [nvarchar](150) NULL,
	[UpdatedDateTime] [nvarchar](20) NULL,
 CONSTRAINT [PK_PartStore] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [State].[EntityEnum]    Script Date: 8/17/2024 3:28:58 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [State].[EntityEnum](
	[Id] [int] NOT NULL,
	[Version] [timestamp] NOT NULL,
	[CreatedBy] [nvarchar](150) NOT NULL,
	[CreatedDateTime] [nvarchar](20) NOT NULL,
	[UpdatedBy] [nvarchar](150) NULL,
	[UpdatedDateTime] [nvarchar](20) NULL,
	[Title] [nvarchar](50) NOT NULL,
	[EntitySchema] [nvarchar](20) NULL,
	[Prefix] [nvarchar](10) NULL,
	[CounterLength] [int] NULL,
 CONSTRAINT [PK_EntityEnum] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [State].[StateEnum]    Script Date: 8/17/2024 3:28:58 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [State].[StateEnum](
	[Id] [int] NOT NULL,
	[Version] [timestamp] NOT NULL,
	[CreatedBy] [nvarchar](150) NOT NULL,
	[CreatedDateTime] [nvarchar](20) NOT NULL,
	[UpdatedBy] [nvarchar](150) NULL,
	[UpdatedDateTime] [nvarchar](20) NULL,
	[Title] [nvarchar](50) NOT NULL,
 CONSTRAINT [PK_StateEnum] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [Store].[InventoryVoucher]    Script Date: 8/17/2024 3:28:58 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [Store].[InventoryVoucher](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[InventoryVoucherNo] [nvarchar](20) NOT NULL,
	[DateTime] [datetime] NULL,
	[PersianDate] [nvarchar](10) NOT NULL,
	[Time] [nvarchar](5) NOT NULL,
	[Comment] [nvarchar](1000) NULL,
	[SystemComment] [nvarchar](2000) NULL,
	[StoreId] [int] NOT NULL,
	[InventoryVoucherSpecificationId] [int] NOT NULL,
	[UserId] [int] NOT NULL,
	[JsonField] [nvarchar](4000) NULL,
	[BaseEntity] [int] NULL,
	[BaseEntityRef] [int] NULL,
	[StateEnumId] [int] NULL,
	[Version] [timestamp] NOT NULL,
	[CreatedBy] [nvarchar](150) NOT NULL,
	[CreatedDateTime] [nvarchar](20) NOT NULL,
	[UpdatedBy] [nvarchar](150) NULL,
	[UpdatedDateTime] [nvarchar](20) NULL,
 CONSTRAINT [PK_InventoryVoucher] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [Store].[InventoryVoucherItem]    Script Date: 8/17/2024 3:28:58 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [Store].[InventoryVoucherItem](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[InventoryVoucherId] [int] NOT NULL,
	[PartId] [int] NOT NULL,
	[Value1] [decimal](18, 0) NOT NULL,
	[Value2] [decimal](18, 0) NULL,
	[Comment] [nvarchar](1000) NULL,
	[SystemComment] [nvarchar](4000) NULL,
	[JsonField] [nvarchar](4000) NULL,
	[Version] [timestamp] NOT NULL,
	[CreatedBy] [nvarchar](150) NOT NULL,
	[CreatedDateTime] [nvarchar](20) NOT NULL,
	[UpdatedBy] [nvarchar](150) NULL,
	[UpdatedDateTime] [nvarchar](20) NULL,
 CONSTRAINT [PK_InventoryVoucherItem] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [Store].[InventoryVoucherItemSerial]    Script Date: 8/17/2024 3:28:58 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [Store].[InventoryVoucherItemSerial](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[InventoryVoucherItemId] [int] NOT NULL,
	[SerialNo] [nvarchar](1000) NOT NULL,
	[Value1] [decimal](18, 0) NOT NULL,
	[Value2] [decimal](18, 0) NULL,
	[Comment] [nvarchar](1000) NULL,
	[SystemComment] [nvarchar](4000) NULL,
	[JsonField] [nvarchar](4000) NULL,
	[Version] [timestamp] NOT NULL,
	[CreatedBy] [nvarchar](150) NOT NULL,
	[CreatedDateTime] [nvarchar](20) NOT NULL,
	[UpdatedBy] [nvarchar](150) NULL,
	[UpdatedDateTime] [nvarchar](20) NULL,
 CONSTRAINT [PK_InventoryVoucherItemSerial] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [Store].[InventoryVoucherSpecification]    Script Date: 8/17/2024 3:28:58 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [Store].[InventoryVoucherSpecification](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Title] [nvarchar](200) NOT NULL,
	[Comment] [nvarchar](1000) NULL,
	[InventoryVoucherSpecificationTypeEnumId] [int] NOT NULL,
	[ReceiptInventoryVoucherSpecificationId] [int] NULL,
	[RemittanceInventoryVoucherSpecificationId] [int] NULL,
	[IsSystemic] [bit] NOT NULL,
	[Jsonfield] [nvarchar](4000) NULL,
	[Version] [timestamp] NOT NULL,
	[CreatedBy] [nvarchar](150) NOT NULL,
	[CreatedDateTime] [nvarchar](20) NOT NULL,
	[UpdatedBy] [nvarchar](150) NULL,
	[UpdatedDateTime] [nvarchar](20) NULL,
 CONSTRAINT [PK_InventoryVoucherSpecification] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [Store].[InventoryVoucherSpecificationTypeEnum]    Script Date: 8/17/2024 3:28:58 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [Store].[InventoryVoucherSpecificationTypeEnum](
	[Id] [int] NOT NULL,
	[Version] [timestamp] NOT NULL,
	[CreatedBy] [nvarchar](150) NOT NULL,
	[CreatedDateTime] [nvarchar](20) NOT NULL,
	[UpdatedBy] [nvarchar](150) NULL,
	[UpdatedDateTime] [nvarchar](20) NULL,
	[Title] [nvarchar](50) NOT NULL,
	[Ratio] [int] NULL,
 CONSTRAINT [PK_InventoryVoucherSpecificationTypeEnum] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [Store].[Store]    Script Date: 8/17/2024 3:28:58 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [Store].[Store](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Title] [nvarchar](200) NOT NULL,
	[Code] [nvarchar](200) NOT NULL,
	[Comment] [nvarchar](1000) NULL,
	[Disabled] [bit] NOT NULL,
	[Jsonfield] [nvarchar](4000) NULL,
	[StoreTypeEnumId] [int] NOT NULL,
	[Version] [timestamp] NOT NULL,
	[CreatedBy] [nvarchar](150) NOT NULL,
	[CreatedDateTime] [nvarchar](20) NOT NULL,
	[UpdatedBy] [nvarchar](150) NULL,
	[UpdatedDateTime] [nvarchar](20) NULL,
 CONSTRAINT [PK_Store] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [Store].[StoreTypeEnum]    Script Date: 8/17/2024 3:28:58 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [Store].[StoreTypeEnum](
	[Id] [int] NOT NULL,
	[Version] [timestamp] NOT NULL,
	[CreatedBy] [nvarchar](150) NOT NULL,
	[CreatedDateTime] [nvarchar](20) NOT NULL,
	[UpdatedBy] [nvarchar](150) NULL,
	[UpdatedDateTime] [nvarchar](20) NULL,
	[Title] [nvarchar](100) NOT NULL,
 CONSTRAINT [PK_StoreTypeEnum] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
SET IDENTITY_INSERT [General].[User] ON 

INSERT [General].[User] ([Id], [CreatedBy], [CreatedDateTime], [UpdatedBy], [UpdatedDateTime], [FullName], [Picture], [Code], [Mobile]) VALUES (1, N'شیوا کاظمی وند', N'1403/05/03', NULL, NULL, N'شیوا کاظمی وند', NULL, 10110, N'093750456335')
SET IDENTITY_INSERT [General].[User] OFF
GO
SET IDENTITY_INSERT [Part].[Category] ON 

INSERT [Part].[Category] ([Id], [Title], [CreatedBy], [CreatedDateTime], [UpdatedBy], [UpdatedDateTime]) VALUES (2, N'مواد اولیه', N'شیوا کاظمی وند', N'1403/05/03 11:58', NULL, NULL)
SET IDENTITY_INSERT [Part].[Category] OFF
GO
SET IDENTITY_INSERT [Part].[CountUnit] ON 

INSERT [Part].[CountUnit] ([Id], [Title], [CreatedBy], [CreatedDateTime], [UpdatedBy], [UpdatedDateTime]) VALUES (1, N'تن', N'شیوا کاظمی وند', N'1403/05/03 11:58', NULL, NULL)
INSERT [Part].[CountUnit] ([Id], [Title], [CreatedBy], [CreatedDateTime], [UpdatedBy], [UpdatedDateTime]) VALUES (2, N'کیلوگرم', N'شیوا کاظمی وند', N'1403/05/04 11:58', NULL, NULL)
SET IDENTITY_INSERT [Part].[CountUnit] OFF
GO
SET IDENTITY_INSERT [Part].[Part] ON 

INSERT [Part].[Part] ([Id], [Title], [Code], [MainCountUnitId], [SecondaryCountUnitId], [CategoryId], [HasSerial], [CreatedBy], [CreatedDateTime], [UpdatedBy], [UpdatedDateTime]) VALUES (2, N'غلتک', N'ABCD', 1, NULL, 2, 0, N'شیوا کاظمی وند', N'1403/05/03 11:58', NULL, NULL)
INSERT [Part].[Part] ([Id], [Title], [Code], [MainCountUnitId], [SecondaryCountUnitId], [CategoryId], [HasSerial], [CreatedBy], [CreatedDateTime], [UpdatedBy], [UpdatedDateTime]) VALUES (4, N'میلگرد', N'EFGH', 1, 2, 2, 1, N'شیوا کاظمی وند', N'1403/05/03 11:58', NULL, NULL)
INSERT [Part].[Part] ([Id], [Title], [Code], [MainCountUnitId], [SecondaryCountUnitId], [CategoryId], [HasSerial], [CreatedBy], [CreatedDateTime], [UpdatedBy], [UpdatedDateTime]) VALUES (6, N'قطعه', N'IJKL', 2, NULL, 2, 0, N'n', N'n', NULL, NULL)
SET IDENTITY_INSERT [Part].[Part] OFF
GO
SET IDENTITY_INSERT [Part].[PartStore] ON 

INSERT [Part].[PartStore] ([Id], [PartId], [StoreId], [IsActive], [IsDefault], [Comment], [CreatedBy], [CreatedDateTime], [UpdatedBy], [UpdatedDateTime]) VALUES (1, 2, 1, 1, 1, NULL, N'n', N'n', NULL, NULL)
INSERT [Part].[PartStore] ([Id], [PartId], [StoreId], [IsActive], [IsDefault], [Comment], [CreatedBy], [CreatedDateTime], [UpdatedBy], [UpdatedDateTime]) VALUES (2, 4, 2, 1, 1, NULL, N'n', N'n', NULL, NULL)
INSERT [Part].[PartStore] ([Id], [PartId], [StoreId], [IsActive], [IsDefault], [Comment], [CreatedBy], [CreatedDateTime], [UpdatedBy], [UpdatedDateTime]) VALUES (3, 2, 2, 1, 0, NULL, N'n', N'n', NULL, NULL)
INSERT [Part].[PartStore] ([Id], [PartId], [StoreId], [IsActive], [IsDefault], [Comment], [CreatedBy], [CreatedDateTime], [UpdatedBy], [UpdatedDateTime]) VALUES (5, 4, 1, 1, 0, NULL, N'n', N'n', NULL, NULL)
INSERT [Part].[PartStore] ([Id], [PartId], [StoreId], [IsActive], [IsDefault], [Comment], [CreatedBy], [CreatedDateTime], [UpdatedBy], [UpdatedDateTime]) VALUES (6, 6, 1, 1, 1, NULL, N'n', N'n', NULL, NULL)
SET IDENTITY_INSERT [Part].[PartStore] OFF
GO
INSERT [State].[EntityEnum] ([Id], [CreatedBy], [CreatedDateTime], [UpdatedBy], [UpdatedDateTime], [Title], [EntitySchema], [Prefix], [CounterLength]) VALUES (1, N'توسط سیستم', N'1403/04/31', NULL, NULL, N'InventoryVoucher', NULL, NULL, NULL)
INSERT [State].[EntityEnum] ([Id], [CreatedBy], [CreatedDateTime], [UpdatedBy], [UpdatedDateTime], [Title], [EntitySchema], [Prefix], [CounterLength]) VALUES (2, N'توسط سیستم', N'1403/04/31', NULL, NULL, N'TransferVoucher', NULL, NULL, NULL)
INSERT [State].[EntityEnum] ([Id], [CreatedBy], [CreatedDateTime], [UpdatedBy], [UpdatedDateTime], [Title], [EntitySchema], [Prefix], [CounterLength]) VALUES (3, N'توسط سیستم', N'1403/04/31', NULL, NULL, N'PartRequest', NULL, NULL, NULL)
GO
INSERT [State].[StateEnum] ([Id], [CreatedBy], [CreatedDateTime], [UpdatedBy], [UpdatedDateTime], [Title]) VALUES (1, N'توسط سیستم', N'1403/04/31', NULL, NULL, N'بدون وضعیت')
GO
SET IDENTITY_INSERT [Store].[InventoryVoucher] ON 

INSERT [Store].[InventoryVoucher] ([Id], [InventoryVoucherNo], [DateTime], [PersianDate], [Time], [Comment], [SystemComment], [StoreId], [InventoryVoucherSpecificationId], [UserId], [JsonField], [BaseEntity], [BaseEntityRef], [StateEnumId], [CreatedBy], [CreatedDateTime], [UpdatedBy], [UpdatedDateTime]) VALUES (1, N'1', NULL, N'1403/05/03', N'12:24', N'تست جدید  سوم', NULL, 1, 1, 1, NULL, NULL, NULL, NULL, N'شیوا کاظمی وند', N'1403/05/03 12:24', N'', N'1403/05/27 14:59:33')
INSERT [Store].[InventoryVoucher] ([Id], [InventoryVoucherNo], [DateTime], [PersianDate], [Time], [Comment], [SystemComment], [StoreId], [InventoryVoucherSpecificationId], [UserId], [JsonField], [BaseEntity], [BaseEntityRef], [StateEnumId], [CreatedBy], [CreatedDateTime], [UpdatedBy], [UpdatedDateTime]) VALUES (2, N'1', NULL, N'1403/05/03', N'12:24', NULL, NULL, 1, 2, 1, NULL, NULL, NULL, 1, N'شیوا کاظمی وند', N'1403/05/03 12:24', NULL, NULL)
INSERT [Store].[InventoryVoucher] ([Id], [InventoryVoucherNo], [DateTime], [PersianDate], [Time], [Comment], [SystemComment], [StoreId], [InventoryVoucherSpecificationId], [UserId], [JsonField], [BaseEntity], [BaseEntityRef], [StateEnumId], [CreatedBy], [CreatedDateTime], [UpdatedBy], [UpdatedDateTime]) VALUES (3, N'2', NULL, N'۱۴۰۳/۰۵/۲۷', N'n', NULL, NULL, 2, 1, 1, NULL, NULL, NULL, NULL, N'n', N'n', N'', N'1403/05/27 15:06:39')
SET IDENTITY_INSERT [Store].[InventoryVoucher] OFF
GO
SET IDENTITY_INSERT [Store].[InventoryVoucherItem] ON 

INSERT [Store].[InventoryVoucherItem] ([Id], [InventoryVoucherId], [PartId], [Value1], [Value2], [Comment], [SystemComment], [JsonField], [CreatedBy], [CreatedDateTime], [UpdatedBy], [UpdatedDateTime]) VALUES (1, 1, 2, CAST(9 AS Decimal(18, 0)), NULL, NULL, NULL, NULL, N'شیوا کاظمی وند', N'1403/05/03 12:24', N'', N'1403/05/21 18:51:17')
INSERT [Store].[InventoryVoucherItem] ([Id], [InventoryVoucherId], [PartId], [Value1], [Value2], [Comment], [SystemComment], [JsonField], [CreatedBy], [CreatedDateTime], [UpdatedBy], [UpdatedDateTime]) VALUES (3, 1, 4, CAST(3 AS Decimal(18, 0)), CAST(3000 AS Decimal(18, 0)), NULL, NULL, NULL, N'شیوا کاظمی وند', N'1403/05/03 12:24', NULL, NULL)
INSERT [Store].[InventoryVoucherItem] ([Id], [InventoryVoucherId], [PartId], [Value1], [Value2], [Comment], [SystemComment], [JsonField], [CreatedBy], [CreatedDateTime], [UpdatedBy], [UpdatedDateTime]) VALUES (4, 2, 2, CAST(1 AS Decimal(18, 0)), NULL, N'k', N'k', NULL, N'k', N'k', NULL, NULL)
INSERT [Store].[InventoryVoucherItem] ([Id], [InventoryVoucherId], [PartId], [Value1], [Value2], [Comment], [SystemComment], [JsonField], [CreatedBy], [CreatedDateTime], [UpdatedBy], [UpdatedDateTime]) VALUES (5, 1, 6, CAST(5 AS Decimal(18, 0)), CAST(0 AS Decimal(18, 0)), NULL, NULL, NULL, N'', N'1403/05/21 17:22:53', NULL, NULL)
SET IDENTITY_INSERT [Store].[InventoryVoucherItem] OFF
GO
SET IDENTITY_INSERT [Store].[InventoryVoucherItemSerial] ON 

INSERT [Store].[InventoryVoucherItemSerial] ([Id], [InventoryVoucherItemId], [SerialNo], [Value1], [Value2], [Comment], [SystemComment], [JsonField], [CreatedBy], [CreatedDateTime], [UpdatedBy], [UpdatedDateTime]) VALUES (1, 3, N'1234', CAST(1 AS Decimal(18, 0)), CAST(1000 AS Decimal(18, 0)), NULL, NULL, NULL, N'شیوا', N'1403/05/03 12:24', NULL, NULL)
INSERT [Store].[InventoryVoucherItemSerial] ([Id], [InventoryVoucherItemId], [SerialNo], [Value1], [Value2], [Comment], [SystemComment], [JsonField], [CreatedBy], [CreatedDateTime], [UpdatedBy], [UpdatedDateTime]) VALUES (2, 3, N'5678', CAST(2 AS Decimal(18, 0)), CAST(2000 AS Decimal(18, 0)), NULL, NULL, NULL, N'شیوا', N'1403/05/03 12:24', NULL, NULL)
SET IDENTITY_INSERT [Store].[InventoryVoucherItemSerial] OFF
GO
SET IDENTITY_INSERT [Store].[InventoryVoucherSpecification] ON 

INSERT [Store].[InventoryVoucherSpecification] ([Id], [Title], [Comment], [InventoryVoucherSpecificationTypeEnumId], [ReceiptInventoryVoucherSpecificationId], [RemittanceInventoryVoucherSpecificationId], [IsSystemic], [Jsonfield], [CreatedBy], [CreatedDateTime], [UpdatedBy], [UpdatedDateTime]) VALUES (1, N'رسید کالای امانی', NULL, 1, NULL, NULL, 0, NULL, N'ش', N'1403/05/03 12:18', NULL, NULL)
INSERT [Store].[InventoryVoucherSpecification] ([Id], [Title], [Comment], [InventoryVoucherSpecificationTypeEnumId], [ReceiptInventoryVoucherSpecificationId], [RemittanceInventoryVoucherSpecificationId], [IsSystemic], [Jsonfield], [CreatedBy], [CreatedDateTime], [UpdatedBy], [UpdatedDateTime]) VALUES (2, N'حواله کالای امانی', NULL, 2, NULL, NULL, 0, NULL, N'k', N'k', NULL, NULL)
SET IDENTITY_INSERT [Store].[InventoryVoucherSpecification] OFF
GO
INSERT [Store].[InventoryVoucherSpecificationTypeEnum] ([Id], [CreatedBy], [CreatedDateTime], [UpdatedBy], [UpdatedDateTime], [Title], [Ratio]) VALUES (1, N'توسط سیستم', N'1403/04/31', NULL, NULL, N'رسید', 1)
INSERT [Store].[InventoryVoucherSpecificationTypeEnum] ([Id], [CreatedBy], [CreatedDateTime], [UpdatedBy], [UpdatedDateTime], [Title], [Ratio]) VALUES (2, N'توسط سیستم', N'1403/04/31', NULL, NULL, N'حواله', -1)
INSERT [Store].[InventoryVoucherSpecificationTypeEnum] ([Id], [CreatedBy], [CreatedDateTime], [UpdatedBy], [UpdatedDateTime], [Title], [Ratio]) VALUES (3, N'توسط سیستم', N'1403/04/31', NULL, NULL, N'انتقال', 0)
INSERT [Store].[InventoryVoucherSpecificationTypeEnum] ([Id], [CreatedBy], [CreatedDateTime], [UpdatedBy], [UpdatedDateTime], [Title], [Ratio]) VALUES (4, N'توسط سیستم', N'1403/04/31', NULL, NULL, N'اول دوره', NULL)
GO
SET IDENTITY_INSERT [Store].[Store] ON 

INSERT [Store].[Store] ([Id], [Title], [Code], [Comment], [Disabled], [Jsonfield], [StoreTypeEnumId], [CreatedBy], [CreatedDateTime], [UpdatedBy], [UpdatedDateTime]) VALUES (1, N'انبار مواد اولیه اول', N'IJK', N'', 0, N'', 1, N'شیوا کاظمی وند', N'1403/05/03 11:58', NULL, NULL)
INSERT [Store].[Store] ([Id], [Title], [Code], [Comment], [Disabled], [Jsonfield], [StoreTypeEnumId], [CreatedBy], [CreatedDateTime], [UpdatedBy], [UpdatedDateTime]) VALUES (2, N'انبار محصول تست', N'ABC', N'', 0, NULL, 2, N'شیوا کاظمی وند', N'1403/05/15 16:44', NULL, NULL)
SET IDENTITY_INSERT [Store].[Store] OFF
GO
INSERT [Store].[StoreTypeEnum] ([Id], [CreatedBy], [CreatedDateTime], [UpdatedBy], [UpdatedDateTime], [Title]) VALUES (1, N'توسط سیستم', N'1403/04/31', NULL, NULL, N'انبار مواد اولیه')
INSERT [Store].[StoreTypeEnum] ([Id], [CreatedBy], [CreatedDateTime], [UpdatedBy], [UpdatedDateTime], [Title]) VALUES (2, N'توسط سیستم', N'1403/04/31', NULL, NULL, N'انبار محصول')
INSERT [Store].[StoreTypeEnum] ([Id], [CreatedBy], [CreatedDateTime], [UpdatedBy], [UpdatedDateTime], [Title]) VALUES (3, N'توسط سیستم', N'1403/04/31', NULL, NULL, N'انبار پای کار محصول')
INSERT [Store].[StoreTypeEnum] ([Id], [CreatedBy], [CreatedDateTime], [UpdatedBy], [UpdatedDateTime], [Title]) VALUES (4, N'توسط سیستم', N'1403/04/31', NULL, NULL, N'انبار مستقل')
INSERT [Store].[StoreTypeEnum] ([Id], [CreatedBy], [CreatedDateTime], [UpdatedBy], [UpdatedDateTime], [Title]) VALUES (5, N'توسط سیستم', N'1403/04/31', NULL, NULL, N'انبار ضایعات')
INSERT [Store].[StoreTypeEnum] ([Id], [CreatedBy], [CreatedDateTime], [UpdatedBy], [UpdatedDateTime], [Title]) VALUES (6, N'توسط سیستم', N'1403/04/31', NULL, NULL, N'انبار پای کار ضایعات')
INSERT [Store].[StoreTypeEnum] ([Id], [CreatedBy], [CreatedDateTime], [UpdatedBy], [UpdatedDateTime], [Title]) VALUES (7, N'توسط سیستم', N'1403/04/31', NULL, NULL, N'انبار پای کار تولید')
GO
ALTER TABLE [Part].[Part]  WITH CHECK ADD  CONSTRAINT [FK_Part_Category] FOREIGN KEY([CategoryId])
REFERENCES [Part].[Category] ([Id])
GO
ALTER TABLE [Part].[Part] CHECK CONSTRAINT [FK_Part_Category]
GO
ALTER TABLE [Part].[Part]  WITH CHECK ADD  CONSTRAINT [FK_Part_MainCountUnit] FOREIGN KEY([MainCountUnitId])
REFERENCES [Part].[CountUnit] ([Id])
GO
ALTER TABLE [Part].[Part] CHECK CONSTRAINT [FK_Part_MainCountUnit]
GO
ALTER TABLE [Part].[Part]  WITH CHECK ADD  CONSTRAINT [FK_Part_SecondaryCountUnit] FOREIGN KEY([SecondaryCountUnitId])
REFERENCES [Part].[CountUnit] ([Id])
GO
ALTER TABLE [Part].[Part] CHECK CONSTRAINT [FK_Part_SecondaryCountUnit]
GO
ALTER TABLE [Part].[PartStore]  WITH CHECK ADD  CONSTRAINT [FK_PartStore_Part] FOREIGN KEY([PartId])
REFERENCES [Part].[Part] ([Id])
GO
ALTER TABLE [Part].[PartStore] CHECK CONSTRAINT [FK_PartStore_Part]
GO
ALTER TABLE [Part].[PartStore]  WITH CHECK ADD  CONSTRAINT [FK_PartStore_Store] FOREIGN KEY([StoreId])
REFERENCES [Store].[Store] ([Id])
GO
ALTER TABLE [Part].[PartStore] CHECK CONSTRAINT [FK_PartStore_Store]
GO
ALTER TABLE [Store].[InventoryVoucher]  WITH CHECK ADD  CONSTRAINT [FK_InventoryVoucher_BaseEntity] FOREIGN KEY([BaseEntity])
REFERENCES [State].[EntityEnum] ([Id])
GO
ALTER TABLE [Store].[InventoryVoucher] CHECK CONSTRAINT [FK_InventoryVoucher_BaseEntity]
GO
ALTER TABLE [Store].[InventoryVoucher]  WITH CHECK ADD  CONSTRAINT [FK_InventoryVoucher_InventoryVoucherSpecification] FOREIGN KEY([InventoryVoucherSpecificationId])
REFERENCES [Store].[InventoryVoucherSpecification] ([Id])
GO
ALTER TABLE [Store].[InventoryVoucher] CHECK CONSTRAINT [FK_InventoryVoucher_InventoryVoucherSpecification]
GO
ALTER TABLE [Store].[InventoryVoucher]  WITH CHECK ADD  CONSTRAINT [FK_InventoryVoucher_StateEnum] FOREIGN KEY([StateEnumId])
REFERENCES [State].[StateEnum] ([Id])
GO
ALTER TABLE [Store].[InventoryVoucher] CHECK CONSTRAINT [FK_InventoryVoucher_StateEnum]
GO
ALTER TABLE [Store].[InventoryVoucher]  WITH CHECK ADD  CONSTRAINT [FK_InventoryVoucher_Store] FOREIGN KEY([StoreId])
REFERENCES [Store].[Store] ([Id])
GO
ALTER TABLE [Store].[InventoryVoucher] CHECK CONSTRAINT [FK_InventoryVoucher_Store]
GO
ALTER TABLE [Store].[InventoryVoucher]  WITH CHECK ADD  CONSTRAINT [FK_InventoryVoucher_User] FOREIGN KEY([UserId])
REFERENCES [General].[User] ([Id])
GO
ALTER TABLE [Store].[InventoryVoucher] CHECK CONSTRAINT [FK_InventoryVoucher_User]
GO
ALTER TABLE [Store].[InventoryVoucherItem]  WITH CHECK ADD  CONSTRAINT [FK_InventoryVoucherItem_InventoryVoucher] FOREIGN KEY([InventoryVoucherId])
REFERENCES [Store].[InventoryVoucher] ([Id])
GO
ALTER TABLE [Store].[InventoryVoucherItem] CHECK CONSTRAINT [FK_InventoryVoucherItem_InventoryVoucher]
GO
ALTER TABLE [Store].[InventoryVoucherItem]  WITH CHECK ADD  CONSTRAINT [FK_InventoryVoucherItem_Part] FOREIGN KEY([PartId])
REFERENCES [Part].[Part] ([Id])
GO
ALTER TABLE [Store].[InventoryVoucherItem] CHECK CONSTRAINT [FK_InventoryVoucherItem_Part]
GO
ALTER TABLE [Store].[InventoryVoucherItemSerial]  WITH CHECK ADD  CONSTRAINT [FK_InventoryVoucherItemSerial_InventoryVoucherItem] FOREIGN KEY([InventoryVoucherItemId])
REFERENCES [Store].[InventoryVoucherItem] ([Id])
GO
ALTER TABLE [Store].[InventoryVoucherItemSerial] CHECK CONSTRAINT [FK_InventoryVoucherItemSerial_InventoryVoucherItem]
GO
ALTER TABLE [Store].[InventoryVoucherSpecification]  WITH CHECK ADD  CONSTRAINT [FK_InventoryVoucherSpecification_InventoryVoucherSpecificationTypeEnum] FOREIGN KEY([InventoryVoucherSpecificationTypeEnumId])
REFERENCES [Store].[InventoryVoucherSpecificationTypeEnum] ([Id])
GO
ALTER TABLE [Store].[InventoryVoucherSpecification] CHECK CONSTRAINT [FK_InventoryVoucherSpecification_InventoryVoucherSpecificationTypeEnum]
GO
ALTER TABLE [Store].[InventoryVoucherSpecification]  WITH CHECK ADD  CONSTRAINT [FK_InventoryVoucherSpecification_ReceiptInventoryVoucherSpecification] FOREIGN KEY([ReceiptInventoryVoucherSpecificationId])
REFERENCES [Store].[InventoryVoucherSpecification] ([Id])
GO
ALTER TABLE [Store].[InventoryVoucherSpecification] CHECK CONSTRAINT [FK_InventoryVoucherSpecification_ReceiptInventoryVoucherSpecification]
GO
ALTER TABLE [Store].[InventoryVoucherSpecification]  WITH CHECK ADD  CONSTRAINT [FK_InventoryVoucherSpecification_RemittanceInventoryVoucherSpecification] FOREIGN KEY([RemittanceInventoryVoucherSpecificationId])
REFERENCES [Store].[InventoryVoucherSpecification] ([Id])
GO
ALTER TABLE [Store].[InventoryVoucherSpecification] CHECK CONSTRAINT [FK_InventoryVoucherSpecification_RemittanceInventoryVoucherSpecification]
GO
ALTER TABLE [Store].[Store]  WITH CHECK ADD  CONSTRAINT [FK_Store_StoreTypeEnum] FOREIGN KEY([StoreTypeEnumId])
REFERENCES [Store].[StoreTypeEnum] ([Id])
GO
ALTER TABLE [Store].[Store] CHECK CONSTRAINT [FK_Store_StoreTypeEnum]
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'شناسه' , @level0type=N'SCHEMA',@level0name=N'General', @level1type=N'TABLE',@level1name=N'User', @level2type=N'COLUMN',@level2name=N'Id'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'نسخه' , @level0type=N'SCHEMA',@level0name=N'General', @level1type=N'TABLE',@level1name=N'User', @level2type=N'COLUMN',@level2name=N'Version'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'ایجاد کننده' , @level0type=N'SCHEMA',@level0name=N'General', @level1type=N'TABLE',@level1name=N'User', @level2type=N'COLUMN',@level2name=N'CreatedBy'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'زمان ایجاد' , @level0type=N'SCHEMA',@level0name=N'General', @level1type=N'TABLE',@level1name=N'User', @level2type=N'COLUMN',@level2name=N'CreatedDateTime'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'تغییر دهنده' , @level0type=N'SCHEMA',@level0name=N'General', @level1type=N'TABLE',@level1name=N'User', @level2type=N'COLUMN',@level2name=N'UpdatedBy'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'زمان تغییر' , @level0type=N'SCHEMA',@level0name=N'General', @level1type=N'TABLE',@level1name=N'User', @level2type=N'COLUMN',@level2name=N'UpdatedDateTime'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'نام کامل' , @level0type=N'SCHEMA',@level0name=N'General', @level1type=N'TABLE',@level1name=N'User', @level2type=N'COLUMN',@level2name=N'FullName'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'تصویر' , @level0type=N'SCHEMA',@level0name=N'General', @level1type=N'TABLE',@level1name=N'User', @level2type=N'COLUMN',@level2name=N'Picture'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'کد' , @level0type=N'SCHEMA',@level0name=N'General', @level1type=N'TABLE',@level1name=N'User', @level2type=N'COLUMN',@level2name=N'Code'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'موبایل' , @level0type=N'SCHEMA',@level0name=N'General', @level1type=N'TABLE',@level1name=N'User', @level2type=N'COLUMN',@level2name=N'Mobile'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'شناسه' , @level0type=N'SCHEMA',@level0name=N'Part', @level1type=N'TABLE',@level1name=N'Category', @level2type=N'COLUMN',@level2name=N'Id'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'عنوان' , @level0type=N'SCHEMA',@level0name=N'Part', @level1type=N'TABLE',@level1name=N'Category', @level2type=N'COLUMN',@level2name=N'Title'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'نسخه' , @level0type=N'SCHEMA',@level0name=N'Part', @level1type=N'TABLE',@level1name=N'Category', @level2type=N'COLUMN',@level2name=N'Version'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'ایجاد کننده' , @level0type=N'SCHEMA',@level0name=N'Part', @level1type=N'TABLE',@level1name=N'Category', @level2type=N'COLUMN',@level2name=N'CreatedBy'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'زمان ایجاد' , @level0type=N'SCHEMA',@level0name=N'Part', @level1type=N'TABLE',@level1name=N'Category', @level2type=N'COLUMN',@level2name=N'CreatedDateTime'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'تغییر دهنده' , @level0type=N'SCHEMA',@level0name=N'Part', @level1type=N'TABLE',@level1name=N'Category', @level2type=N'COLUMN',@level2name=N'UpdatedBy'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'زمان تغییر' , @level0type=N'SCHEMA',@level0name=N'Part', @level1type=N'TABLE',@level1name=N'Category', @level2type=N'COLUMN',@level2name=N'UpdatedDateTime'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'شناسه' , @level0type=N'SCHEMA',@level0name=N'Part', @level1type=N'TABLE',@level1name=N'CountUnit', @level2type=N'COLUMN',@level2name=N'Id'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'عنوان' , @level0type=N'SCHEMA',@level0name=N'Part', @level1type=N'TABLE',@level1name=N'CountUnit', @level2type=N'COLUMN',@level2name=N'Title'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'نسخه' , @level0type=N'SCHEMA',@level0name=N'Part', @level1type=N'TABLE',@level1name=N'CountUnit', @level2type=N'COLUMN',@level2name=N'Version'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'ایجاد کننده' , @level0type=N'SCHEMA',@level0name=N'Part', @level1type=N'TABLE',@level1name=N'CountUnit', @level2type=N'COLUMN',@level2name=N'CreatedBy'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'زمان ایجاد' , @level0type=N'SCHEMA',@level0name=N'Part', @level1type=N'TABLE',@level1name=N'CountUnit', @level2type=N'COLUMN',@level2name=N'CreatedDateTime'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'تغییر دهنده' , @level0type=N'SCHEMA',@level0name=N'Part', @level1type=N'TABLE',@level1name=N'CountUnit', @level2type=N'COLUMN',@level2name=N'UpdatedBy'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'زمان تغییر' , @level0type=N'SCHEMA',@level0name=N'Part', @level1type=N'TABLE',@level1name=N'CountUnit', @level2type=N'COLUMN',@level2name=N'UpdatedDateTime'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'شناسه' , @level0type=N'SCHEMA',@level0name=N'Part', @level1type=N'TABLE',@level1name=N'Part', @level2type=N'COLUMN',@level2name=N'Id'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'عنوان' , @level0type=N'SCHEMA',@level0name=N'Part', @level1type=N'TABLE',@level1name=N'Part', @level2type=N'COLUMN',@level2name=N'Title'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'کد همکاران سیستم' , @level0type=N'SCHEMA',@level0name=N'Part', @level1type=N'TABLE',@level1name=N'Part', @level2type=N'COLUMN',@level2name=N'Code'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'شناسه واحد شمارش اصلی' , @level0type=N'SCHEMA',@level0name=N'Part', @level1type=N'TABLE',@level1name=N'Part', @level2type=N'COLUMN',@level2name=N'MainCountUnitId'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'شناسه واحد شمارش دوم' , @level0type=N'SCHEMA',@level0name=N'Part', @level1type=N'TABLE',@level1name=N'Part', @level2type=N'COLUMN',@level2name=N'SecondaryCountUnitId'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'شناسه دسته بندی' , @level0type=N'SCHEMA',@level0name=N'Part', @level1type=N'TABLE',@level1name=N'Part', @level2type=N'COLUMN',@level2name=N'CategoryId'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'دارای سریال' , @level0type=N'SCHEMA',@level0name=N'Part', @level1type=N'TABLE',@level1name=N'Part', @level2type=N'COLUMN',@level2name=N'HasSerial'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'نسخه' , @level0type=N'SCHEMA',@level0name=N'Part', @level1type=N'TABLE',@level1name=N'Part', @level2type=N'COLUMN',@level2name=N'Version'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'ایجاد کننده' , @level0type=N'SCHEMA',@level0name=N'Part', @level1type=N'TABLE',@level1name=N'Part', @level2type=N'COLUMN',@level2name=N'CreatedBy'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'زمان ایجاد' , @level0type=N'SCHEMA',@level0name=N'Part', @level1type=N'TABLE',@level1name=N'Part', @level2type=N'COLUMN',@level2name=N'CreatedDateTime'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'تغییر دهنده' , @level0type=N'SCHEMA',@level0name=N'Part', @level1type=N'TABLE',@level1name=N'Part', @level2type=N'COLUMN',@level2name=N'UpdatedBy'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'زمان تغییر' , @level0type=N'SCHEMA',@level0name=N'Part', @level1type=N'TABLE',@level1name=N'Part', @level2type=N'COLUMN',@level2name=N'UpdatedDateTime'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'شناسه' , @level0type=N'SCHEMA',@level0name=N'Part', @level1type=N'TABLE',@level1name=N'PartStore', @level2type=N'COLUMN',@level2name=N'Id'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'شناسه کالا' , @level0type=N'SCHEMA',@level0name=N'Part', @level1type=N'TABLE',@level1name=N'PartStore', @level2type=N'COLUMN',@level2name=N'PartId'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'شناسه انبار' , @level0type=N'SCHEMA',@level0name=N'Part', @level1type=N'TABLE',@level1name=N'PartStore', @level2type=N'COLUMN',@level2name=N'StoreId'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'فعال' , @level0type=N'SCHEMA',@level0name=N'Part', @level1type=N'TABLE',@level1name=N'PartStore', @level2type=N'COLUMN',@level2name=N'IsActive'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'انبار پیشفرض' , @level0type=N'SCHEMA',@level0name=N'Part', @level1type=N'TABLE',@level1name=N'PartStore', @level2type=N'COLUMN',@level2name=N'IsDefault'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'توضیح' , @level0type=N'SCHEMA',@level0name=N'Part', @level1type=N'TABLE',@level1name=N'PartStore', @level2type=N'COLUMN',@level2name=N'Comment'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'نسخه' , @level0type=N'SCHEMA',@level0name=N'Part', @level1type=N'TABLE',@level1name=N'PartStore', @level2type=N'COLUMN',@level2name=N'Version'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'ایجاد کننده' , @level0type=N'SCHEMA',@level0name=N'Part', @level1type=N'TABLE',@level1name=N'PartStore', @level2type=N'COLUMN',@level2name=N'CreatedBy'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'زمان ایجاد' , @level0type=N'SCHEMA',@level0name=N'Part', @level1type=N'TABLE',@level1name=N'PartStore', @level2type=N'COLUMN',@level2name=N'CreatedDateTime'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'تغییر دهنده' , @level0type=N'SCHEMA',@level0name=N'Part', @level1type=N'TABLE',@level1name=N'PartStore', @level2type=N'COLUMN',@level2name=N'UpdatedBy'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'زمان تغییر' , @level0type=N'SCHEMA',@level0name=N'Part', @level1type=N'TABLE',@level1name=N'PartStore', @level2type=N'COLUMN',@level2name=N'UpdatedDateTime'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'شناسه' , @level0type=N'SCHEMA',@level0name=N'State', @level1type=N'TABLE',@level1name=N'EntityEnum', @level2type=N'COLUMN',@level2name=N'Id'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'نسخه' , @level0type=N'SCHEMA',@level0name=N'State', @level1type=N'TABLE',@level1name=N'EntityEnum', @level2type=N'COLUMN',@level2name=N'Version'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'ایجاد کننده' , @level0type=N'SCHEMA',@level0name=N'State', @level1type=N'TABLE',@level1name=N'EntityEnum', @level2type=N'COLUMN',@level2name=N'CreatedBy'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'زمان ایجاد' , @level0type=N'SCHEMA',@level0name=N'State', @level1type=N'TABLE',@level1name=N'EntityEnum', @level2type=N'COLUMN',@level2name=N'CreatedDateTime'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'تغییر دهنده' , @level0type=N'SCHEMA',@level0name=N'State', @level1type=N'TABLE',@level1name=N'EntityEnum', @level2type=N'COLUMN',@level2name=N'UpdatedBy'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'زمان تغییر' , @level0type=N'SCHEMA',@level0name=N'State', @level1type=N'TABLE',@level1name=N'EntityEnum', @level2type=N'COLUMN',@level2name=N'UpdatedDateTime'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'عنوان فرم' , @level0type=N'SCHEMA',@level0name=N'State', @level1type=N'TABLE',@level1name=N'EntityEnum', @level2type=N'COLUMN',@level2name=N'Title'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'نام شمای فرم' , @level0type=N'SCHEMA',@level0name=N'State', @level1type=N'TABLE',@level1name=N'EntityEnum', @level2type=N'COLUMN',@level2name=N'EntitySchema'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'پیشوند' , @level0type=N'SCHEMA',@level0name=N'State', @level1type=N'TABLE',@level1name=N'EntityEnum', @level2type=N'COLUMN',@level2name=N'Prefix'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'طول شمارنده' , @level0type=N'SCHEMA',@level0name=N'State', @level1type=N'TABLE',@level1name=N'EntityEnum', @level2type=N'COLUMN',@level2name=N'CounterLength'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'شناسه' , @level0type=N'SCHEMA',@level0name=N'State', @level1type=N'TABLE',@level1name=N'StateEnum', @level2type=N'COLUMN',@level2name=N'Id'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'نسخه' , @level0type=N'SCHEMA',@level0name=N'State', @level1type=N'TABLE',@level1name=N'StateEnum', @level2type=N'COLUMN',@level2name=N'Version'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'ایجاد کننده' , @level0type=N'SCHEMA',@level0name=N'State', @level1type=N'TABLE',@level1name=N'StateEnum', @level2type=N'COLUMN',@level2name=N'CreatedBy'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'زمان ایجاد' , @level0type=N'SCHEMA',@level0name=N'State', @level1type=N'TABLE',@level1name=N'StateEnum', @level2type=N'COLUMN',@level2name=N'CreatedDateTime'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'تغییر دهنده' , @level0type=N'SCHEMA',@level0name=N'State', @level1type=N'TABLE',@level1name=N'StateEnum', @level2type=N'COLUMN',@level2name=N'UpdatedBy'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'زمان تغییر' , @level0type=N'SCHEMA',@level0name=N'State', @level1type=N'TABLE',@level1name=N'StateEnum', @level2type=N'COLUMN',@level2name=N'UpdatedDateTime'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'عنوان' , @level0type=N'SCHEMA',@level0name=N'State', @level1type=N'TABLE',@level1name=N'StateEnum', @level2type=N'COLUMN',@level2name=N'Title'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'شناسه' , @level0type=N'SCHEMA',@level0name=N'Store', @level1type=N'TABLE',@level1name=N'InventoryVoucher', @level2type=N'COLUMN',@level2name=N'Id'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'شماره سند' , @level0type=N'SCHEMA',@level0name=N'Store', @level1type=N'TABLE',@level1name=N'InventoryVoucher', @level2type=N'COLUMN',@level2name=N'InventoryVoucherNo'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'زمان و تاریخ میلادی' , @level0type=N'SCHEMA',@level0name=N'Store', @level1type=N'TABLE',@level1name=N'InventoryVoucher', @level2type=N'COLUMN',@level2name=N'DateTime'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'تاریخ شمسی' , @level0type=N'SCHEMA',@level0name=N'Store', @level1type=N'TABLE',@level1name=N'InventoryVoucher', @level2type=N'COLUMN',@level2name=N'PersianDate'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'زمان' , @level0type=N'SCHEMA',@level0name=N'Store', @level1type=N'TABLE',@level1name=N'InventoryVoucher', @level2type=N'COLUMN',@level2name=N'Time'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'توضیح' , @level0type=N'SCHEMA',@level0name=N'Store', @level1type=N'TABLE',@level1name=N'InventoryVoucher', @level2type=N'COLUMN',@level2name=N'Comment'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'توضیح سیستم (مبنای انتقال:براساس انتقال شماره xx در تاریخ xxx)' , @level0type=N'SCHEMA',@level0name=N'Store', @level1type=N'TABLE',@level1name=N'InventoryVoucher', @level2type=N'COLUMN',@level2name=N'SystemComment'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'شناسه انبار' , @level0type=N'SCHEMA',@level0name=N'Store', @level1type=N'TABLE',@level1name=N'InventoryVoucher', @level2type=N'COLUMN',@level2name=N'StoreId'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'شناسه الگوی سند' , @level0type=N'SCHEMA',@level0name=N'Store', @level1type=N'TABLE',@level1name=N'InventoryVoucher', @level2type=N'COLUMN',@level2name=N'InventoryVoucherSpecificationId'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'شناسه کاربر' , @level0type=N'SCHEMA',@level0name=N'Store', @level1type=N'TABLE',@level1name=N'InventoryVoucher', @level2type=N'COLUMN',@level2name=N'UserId'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'دیتای جیسون' , @level0type=N'SCHEMA',@level0name=N'Store', @level1type=N'TABLE',@level1name=N'InventoryVoucher', @level2type=N'COLUMN',@level2name=N'JsonField'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'شناسه جدول مبنا' , @level0type=N'SCHEMA',@level0name=N'Store', @level1type=N'TABLE',@level1name=N'InventoryVoucher', @level2type=N'COLUMN',@level2name=N'BaseEntity'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'شناسه رکورد در جدول مبنا' , @level0type=N'SCHEMA',@level0name=N'Store', @level1type=N'TABLE',@level1name=N'InventoryVoucher', @level2type=N'COLUMN',@level2name=N'BaseEntityRef'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'شناسه نام وضعیت' , @level0type=N'SCHEMA',@level0name=N'Store', @level1type=N'TABLE',@level1name=N'InventoryVoucher', @level2type=N'COLUMN',@level2name=N'StateEnumId'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'نسخه' , @level0type=N'SCHEMA',@level0name=N'Store', @level1type=N'TABLE',@level1name=N'InventoryVoucher', @level2type=N'COLUMN',@level2name=N'Version'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'ایجاد کننده' , @level0type=N'SCHEMA',@level0name=N'Store', @level1type=N'TABLE',@level1name=N'InventoryVoucher', @level2type=N'COLUMN',@level2name=N'CreatedBy'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'زمان ایجاد' , @level0type=N'SCHEMA',@level0name=N'Store', @level1type=N'TABLE',@level1name=N'InventoryVoucher', @level2type=N'COLUMN',@level2name=N'CreatedDateTime'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'تغییر دهنده' , @level0type=N'SCHEMA',@level0name=N'Store', @level1type=N'TABLE',@level1name=N'InventoryVoucher', @level2type=N'COLUMN',@level2name=N'UpdatedBy'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'زمان تغییر' , @level0type=N'SCHEMA',@level0name=N'Store', @level1type=N'TABLE',@level1name=N'InventoryVoucher', @level2type=N'COLUMN',@level2name=N'UpdatedDateTime'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'شناسه' , @level0type=N'SCHEMA',@level0name=N'Store', @level1type=N'TABLE',@level1name=N'InventoryVoucherItem', @level2type=N'COLUMN',@level2name=N'Id'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'شناسه سند' , @level0type=N'SCHEMA',@level0name=N'Store', @level1type=N'TABLE',@level1name=N'InventoryVoucherItem', @level2type=N'COLUMN',@level2name=N'InventoryVoucherId'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'شناسه کالا' , @level0type=N'SCHEMA',@level0name=N'Store', @level1type=N'TABLE',@level1name=N'InventoryVoucherItem', @level2type=N'COLUMN',@level2name=N'PartId'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'مقدار اول' , @level0type=N'SCHEMA',@level0name=N'Store', @level1type=N'TABLE',@level1name=N'InventoryVoucherItem', @level2type=N'COLUMN',@level2name=N'Value1'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'مقدار دوم' , @level0type=N'SCHEMA',@level0name=N'Store', @level1type=N'TABLE',@level1name=N'InventoryVoucherItem', @level2type=N'COLUMN',@level2name=N'Value2'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'توضیح' , @level0type=N'SCHEMA',@level0name=N'Store', @level1type=N'TABLE',@level1name=N'InventoryVoucherItem', @level2type=N'COLUMN',@level2name=N'Comment'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'توضیح سیستمی' , @level0type=N'SCHEMA',@level0name=N'Store', @level1type=N'TABLE',@level1name=N'InventoryVoucherItem', @level2type=N'COLUMN',@level2name=N'SystemComment'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'دیتای جیسون' , @level0type=N'SCHEMA',@level0name=N'Store', @level1type=N'TABLE',@level1name=N'InventoryVoucherItem', @level2type=N'COLUMN',@level2name=N'JsonField'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'نسخه' , @level0type=N'SCHEMA',@level0name=N'Store', @level1type=N'TABLE',@level1name=N'InventoryVoucherItem', @level2type=N'COLUMN',@level2name=N'Version'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'ایجاد کننده' , @level0type=N'SCHEMA',@level0name=N'Store', @level1type=N'TABLE',@level1name=N'InventoryVoucherItem', @level2type=N'COLUMN',@level2name=N'CreatedBy'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'زمان ایجاد' , @level0type=N'SCHEMA',@level0name=N'Store', @level1type=N'TABLE',@level1name=N'InventoryVoucherItem', @level2type=N'COLUMN',@level2name=N'CreatedDateTime'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'تغییر دهنده' , @level0type=N'SCHEMA',@level0name=N'Store', @level1type=N'TABLE',@level1name=N'InventoryVoucherItem', @level2type=N'COLUMN',@level2name=N'UpdatedBy'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'زمان تغییر' , @level0type=N'SCHEMA',@level0name=N'Store', @level1type=N'TABLE',@level1name=N'InventoryVoucherItem', @level2type=N'COLUMN',@level2name=N'UpdatedDateTime'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'شناسه' , @level0type=N'SCHEMA',@level0name=N'Store', @level1type=N'TABLE',@level1name=N'InventoryVoucherItemSerial', @level2type=N'COLUMN',@level2name=N'Id'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'شناسه کالای سند انبار' , @level0type=N'SCHEMA',@level0name=N'Store', @level1type=N'TABLE',@level1name=N'InventoryVoucherItemSerial', @level2type=N'COLUMN',@level2name=N'InventoryVoucherItemId'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'سریال' , @level0type=N'SCHEMA',@level0name=N'Store', @level1type=N'TABLE',@level1name=N'InventoryVoucherItemSerial', @level2type=N'COLUMN',@level2name=N'SerialNo'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'مقدار اول' , @level0type=N'SCHEMA',@level0name=N'Store', @level1type=N'TABLE',@level1name=N'InventoryVoucherItemSerial', @level2type=N'COLUMN',@level2name=N'Value1'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'مقدار دوم' , @level0type=N'SCHEMA',@level0name=N'Store', @level1type=N'TABLE',@level1name=N'InventoryVoucherItemSerial', @level2type=N'COLUMN',@level2name=N'Value2'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'توضیح' , @level0type=N'SCHEMA',@level0name=N'Store', @level1type=N'TABLE',@level1name=N'InventoryVoucherItemSerial', @level2type=N'COLUMN',@level2name=N'Comment'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'توضیح سیستمی' , @level0type=N'SCHEMA',@level0name=N'Store', @level1type=N'TABLE',@level1name=N'InventoryVoucherItemSerial', @level2type=N'COLUMN',@level2name=N'SystemComment'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'داده های جیسون' , @level0type=N'SCHEMA',@level0name=N'Store', @level1type=N'TABLE',@level1name=N'InventoryVoucherItemSerial', @level2type=N'COLUMN',@level2name=N'JsonField'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'نسخه' , @level0type=N'SCHEMA',@level0name=N'Store', @level1type=N'TABLE',@level1name=N'InventoryVoucherItemSerial', @level2type=N'COLUMN',@level2name=N'Version'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'ایجاد کننده' , @level0type=N'SCHEMA',@level0name=N'Store', @level1type=N'TABLE',@level1name=N'InventoryVoucherItemSerial', @level2type=N'COLUMN',@level2name=N'CreatedBy'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'زمان ایجاد' , @level0type=N'SCHEMA',@level0name=N'Store', @level1type=N'TABLE',@level1name=N'InventoryVoucherItemSerial', @level2type=N'COLUMN',@level2name=N'CreatedDateTime'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'تغییر دهنده' , @level0type=N'SCHEMA',@level0name=N'Store', @level1type=N'TABLE',@level1name=N'InventoryVoucherItemSerial', @level2type=N'COLUMN',@level2name=N'UpdatedBy'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'زمان تغییر' , @level0type=N'SCHEMA',@level0name=N'Store', @level1type=N'TABLE',@level1name=N'InventoryVoucherItemSerial', @level2type=N'COLUMN',@level2name=N'UpdatedDateTime'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'شناسه' , @level0type=N'SCHEMA',@level0name=N'Store', @level1type=N'TABLE',@level1name=N'InventoryVoucherSpecification', @level2type=N'COLUMN',@level2name=N'Id'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'عنوان' , @level0type=N'SCHEMA',@level0name=N'Store', @level1type=N'TABLE',@level1name=N'InventoryVoucherSpecification', @level2type=N'COLUMN',@level2name=N'Title'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'توضیح' , @level0type=N'SCHEMA',@level0name=N'Store', @level1type=N'TABLE',@level1name=N'InventoryVoucherSpecification', @level2type=N'COLUMN',@level2name=N'Comment'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'شناسه نوع الگوی سند' , @level0type=N'SCHEMA',@level0name=N'Store', @level1type=N'TABLE',@level1name=N'InventoryVoucherSpecification', @level2type=N'COLUMN',@level2name=N'InventoryVoucherSpecificationTypeEnumId'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'شناسه الگوی سند رسید در سند انتقال' , @level0type=N'SCHEMA',@level0name=N'Store', @level1type=N'TABLE',@level1name=N'InventoryVoucherSpecification', @level2type=N'COLUMN',@level2name=N'ReceiptInventoryVoucherSpecificationId'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'شناسه الگوی سند حواله در سند انتقال' , @level0type=N'SCHEMA',@level0name=N'Store', @level1type=N'TABLE',@level1name=N'InventoryVoucherSpecification', @level2type=N'COLUMN',@level2name=N'RemittanceInventoryVoucherSpecificationId'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'سیستمی است' , @level0type=N'SCHEMA',@level0name=N'Store', @level1type=N'TABLE',@level1name=N'InventoryVoucherSpecification', @level2type=N'COLUMN',@level2name=N'IsSystemic'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'دیتای جیسون' , @level0type=N'SCHEMA',@level0name=N'Store', @level1type=N'TABLE',@level1name=N'InventoryVoucherSpecification', @level2type=N'COLUMN',@level2name=N'Jsonfield'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'نسخه' , @level0type=N'SCHEMA',@level0name=N'Store', @level1type=N'TABLE',@level1name=N'InventoryVoucherSpecification', @level2type=N'COLUMN',@level2name=N'Version'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'ایجاد کننده' , @level0type=N'SCHEMA',@level0name=N'Store', @level1type=N'TABLE',@level1name=N'InventoryVoucherSpecification', @level2type=N'COLUMN',@level2name=N'CreatedBy'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'زمان ایجاد' , @level0type=N'SCHEMA',@level0name=N'Store', @level1type=N'TABLE',@level1name=N'InventoryVoucherSpecification', @level2type=N'COLUMN',@level2name=N'CreatedDateTime'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'تغییر دهنده' , @level0type=N'SCHEMA',@level0name=N'Store', @level1type=N'TABLE',@level1name=N'InventoryVoucherSpecification', @level2type=N'COLUMN',@level2name=N'UpdatedBy'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'زمان تغییر' , @level0type=N'SCHEMA',@level0name=N'Store', @level1type=N'TABLE',@level1name=N'InventoryVoucherSpecification', @level2type=N'COLUMN',@level2name=N'UpdatedDateTime'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'شناسه' , @level0type=N'SCHEMA',@level0name=N'Store', @level1type=N'TABLE',@level1name=N'InventoryVoucherSpecificationTypeEnum', @level2type=N'COLUMN',@level2name=N'Id'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'نسخه' , @level0type=N'SCHEMA',@level0name=N'Store', @level1type=N'TABLE',@level1name=N'InventoryVoucherSpecificationTypeEnum', @level2type=N'COLUMN',@level2name=N'Version'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'ایجاد کننده' , @level0type=N'SCHEMA',@level0name=N'Store', @level1type=N'TABLE',@level1name=N'InventoryVoucherSpecificationTypeEnum', @level2type=N'COLUMN',@level2name=N'CreatedBy'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'زمان ایجاد' , @level0type=N'SCHEMA',@level0name=N'Store', @level1type=N'TABLE',@level1name=N'InventoryVoucherSpecificationTypeEnum', @level2type=N'COLUMN',@level2name=N'CreatedDateTime'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'تغییر دهنده' , @level0type=N'SCHEMA',@level0name=N'Store', @level1type=N'TABLE',@level1name=N'InventoryVoucherSpecificationTypeEnum', @level2type=N'COLUMN',@level2name=N'UpdatedBy'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'زمان تغییر' , @level0type=N'SCHEMA',@level0name=N'Store', @level1type=N'TABLE',@level1name=N'InventoryVoucherSpecificationTypeEnum', @level2type=N'COLUMN',@level2name=N'UpdatedDateTime'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'عنوان' , @level0type=N'SCHEMA',@level0name=N'Store', @level1type=N'TABLE',@level1name=N'InventoryVoucherSpecificationTypeEnum', @level2type=N'COLUMN',@level2name=N'Title'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'ضریب' , @level0type=N'SCHEMA',@level0name=N'Store', @level1type=N'TABLE',@level1name=N'InventoryVoucherSpecificationTypeEnum', @level2type=N'COLUMN',@level2name=N'Ratio'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'شناسه' , @level0type=N'SCHEMA',@level0name=N'Store', @level1type=N'TABLE',@level1name=N'Store', @level2type=N'COLUMN',@level2name=N'Id'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'عنوان' , @level0type=N'SCHEMA',@level0name=N'Store', @level1type=N'TABLE',@level1name=N'Store', @level2type=N'COLUMN',@level2name=N'Title'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'کد' , @level0type=N'SCHEMA',@level0name=N'Store', @level1type=N'TABLE',@level1name=N'Store', @level2type=N'COLUMN',@level2name=N'Code'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'توضیحات' , @level0type=N'SCHEMA',@level0name=N'Store', @level1type=N'TABLE',@level1name=N'Store', @level2type=N'COLUMN',@level2name=N'Comment'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'غیرفعال' , @level0type=N'SCHEMA',@level0name=N'Store', @level1type=N'TABLE',@level1name=N'Store', @level2type=N'COLUMN',@level2name=N'Disabled'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'داده های جیسون' , @level0type=N'SCHEMA',@level0name=N'Store', @level1type=N'TABLE',@level1name=N'Store', @level2type=N'COLUMN',@level2name=N'Jsonfield'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'شناسه گروه انبار' , @level0type=N'SCHEMA',@level0name=N'Store', @level1type=N'TABLE',@level1name=N'Store', @level2type=N'COLUMN',@level2name=N'StoreTypeEnumId'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'نسخه' , @level0type=N'SCHEMA',@level0name=N'Store', @level1type=N'TABLE',@level1name=N'Store', @level2type=N'COLUMN',@level2name=N'Version'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'ایجاد کننده' , @level0type=N'SCHEMA',@level0name=N'Store', @level1type=N'TABLE',@level1name=N'Store', @level2type=N'COLUMN',@level2name=N'CreatedBy'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'زمان ایجاد' , @level0type=N'SCHEMA',@level0name=N'Store', @level1type=N'TABLE',@level1name=N'Store', @level2type=N'COLUMN',@level2name=N'CreatedDateTime'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'تغییر دهنده' , @level0type=N'SCHEMA',@level0name=N'Store', @level1type=N'TABLE',@level1name=N'Store', @level2type=N'COLUMN',@level2name=N'UpdatedBy'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'زمان تغییر' , @level0type=N'SCHEMA',@level0name=N'Store', @level1type=N'TABLE',@level1name=N'Store', @level2type=N'COLUMN',@level2name=N'UpdatedDateTime'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'شناسه' , @level0type=N'SCHEMA',@level0name=N'Store', @level1type=N'TABLE',@level1name=N'StoreTypeEnum', @level2type=N'COLUMN',@level2name=N'Id'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'نسخه' , @level0type=N'SCHEMA',@level0name=N'Store', @level1type=N'TABLE',@level1name=N'StoreTypeEnum', @level2type=N'COLUMN',@level2name=N'Version'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'ایجاد کننده' , @level0type=N'SCHEMA',@level0name=N'Store', @level1type=N'TABLE',@level1name=N'StoreTypeEnum', @level2type=N'COLUMN',@level2name=N'CreatedBy'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'زمان ایجاد' , @level0type=N'SCHEMA',@level0name=N'Store', @level1type=N'TABLE',@level1name=N'StoreTypeEnum', @level2type=N'COLUMN',@level2name=N'CreatedDateTime'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'تغییر دهنده' , @level0type=N'SCHEMA',@level0name=N'Store', @level1type=N'TABLE',@level1name=N'StoreTypeEnum', @level2type=N'COLUMN',@level2name=N'UpdatedBy'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'زمان تغییر' , @level0type=N'SCHEMA',@level0name=N'Store', @level1type=N'TABLE',@level1name=N'StoreTypeEnum', @level2type=N'COLUMN',@level2name=N'UpdatedDateTime'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'عنوان' , @level0type=N'SCHEMA',@level0name=N'Store', @level1type=N'TABLE',@level1name=N'StoreTypeEnum', @level2type=N'COLUMN',@level2name=N'Title'
GO
USE [master]
GO
ALTER DATABASE [InventorySample] SET  READ_WRITE 
GO
