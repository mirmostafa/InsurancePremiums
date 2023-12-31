USE [master]
GO
/****** Object:  Database [InsurancePremiums]    Script Date: 11/14/2023 11:35:25 AM ******/
CREATE DATABASE [InsurancePremiums]
 CONTAINMENT = NONE
 ON  PRIMARY 
( NAME = N'InsurancePremiums', FILENAME = N'D:\Dev\Sql\Data\InsurancePremiums.mdf' , SIZE = 8192KB , MAXSIZE = UNLIMITED, FILEGROWTH = 65536KB )
 LOG ON 
( NAME = N'InsurancePremiums_log', FILENAME = N'D:\Dev\Sql\Data\InsurancePremiums_log.ldf' , SIZE = 8192KB , MAXSIZE = 2048GB , FILEGROWTH = 65536KB )
 WITH CATALOG_COLLATION = DATABASE_DEFAULT, LEDGER = OFF
GO
ALTER DATABASE [InsurancePremiums] SET COMPATIBILITY_LEVEL = 160
GO
IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [InsurancePremiums].[dbo].[sp_fulltext_database] @action = 'enable'
end
GO
ALTER DATABASE [InsurancePremiums] SET ANSI_NULL_DEFAULT OFF 
GO
ALTER DATABASE [InsurancePremiums] SET ANSI_NULLS OFF 
GO
ALTER DATABASE [InsurancePremiums] SET ANSI_PADDING OFF 
GO
ALTER DATABASE [InsurancePremiums] SET ANSI_WARNINGS OFF 
GO
ALTER DATABASE [InsurancePremiums] SET ARITHABORT OFF 
GO
ALTER DATABASE [InsurancePremiums] SET AUTO_CLOSE OFF 
GO
ALTER DATABASE [InsurancePremiums] SET AUTO_SHRINK OFF 
GO
ALTER DATABASE [InsurancePremiums] SET AUTO_UPDATE_STATISTICS ON 
GO
ALTER DATABASE [InsurancePremiums] SET CURSOR_CLOSE_ON_COMMIT OFF 
GO
ALTER DATABASE [InsurancePremiums] SET CURSOR_DEFAULT  GLOBAL 
GO
ALTER DATABASE [InsurancePremiums] SET CONCAT_NULL_YIELDS_NULL OFF 
GO
ALTER DATABASE [InsurancePremiums] SET NUMERIC_ROUNDABORT OFF 
GO
ALTER DATABASE [InsurancePremiums] SET QUOTED_IDENTIFIER OFF 
GO
ALTER DATABASE [InsurancePremiums] SET RECURSIVE_TRIGGERS OFF 
GO
ALTER DATABASE [InsurancePremiums] SET  DISABLE_BROKER 
GO
ALTER DATABASE [InsurancePremiums] SET AUTO_UPDATE_STATISTICS_ASYNC OFF 
GO
ALTER DATABASE [InsurancePremiums] SET DATE_CORRELATION_OPTIMIZATION OFF 
GO
ALTER DATABASE [InsurancePremiums] SET TRUSTWORTHY OFF 
GO
ALTER DATABASE [InsurancePremiums] SET ALLOW_SNAPSHOT_ISOLATION OFF 
GO
ALTER DATABASE [InsurancePremiums] SET PARAMETERIZATION SIMPLE 
GO
ALTER DATABASE [InsurancePremiums] SET READ_COMMITTED_SNAPSHOT OFF 
GO
ALTER DATABASE [InsurancePremiums] SET HONOR_BROKER_PRIORITY OFF 
GO
ALTER DATABASE [InsurancePremiums] SET RECOVERY SIMPLE 
GO
ALTER DATABASE [InsurancePremiums] SET  MULTI_USER 
GO
ALTER DATABASE [InsurancePremiums] SET PAGE_VERIFY CHECKSUM  
GO
ALTER DATABASE [InsurancePremiums] SET DB_CHAINING OFF 
GO
ALTER DATABASE [InsurancePremiums] SET FILESTREAM( NON_TRANSACTED_ACCESS = OFF ) 
GO
ALTER DATABASE [InsurancePremiums] SET TARGET_RECOVERY_TIME = 60 SECONDS 
GO
ALTER DATABASE [InsurancePremiums] SET DELAYED_DURABILITY = DISABLED 
GO
ALTER DATABASE [InsurancePremiums] SET ACCELERATED_DATABASE_RECOVERY = OFF  
GO
ALTER DATABASE [InsurancePremiums] SET QUERY_STORE = ON
GO
ALTER DATABASE [InsurancePremiums] SET QUERY_STORE (OPERATION_MODE = READ_WRITE, CLEANUP_POLICY = (STALE_QUERY_THRESHOLD_DAYS = 30), DATA_FLUSH_INTERVAL_SECONDS = 900, INTERVAL_LENGTH_MINUTES = 60, MAX_STORAGE_SIZE_MB = 1000, QUERY_CAPTURE_MODE = AUTO, SIZE_BASED_CLEANUP_MODE = AUTO, MAX_PLANS_PER_QUERY = 200, WAIT_STATS_CAPTURE_MODE = ON)
GO
USE [InsurancePremiums]
GO
/****** Object:  Table [dbo].[Coverage]    Script Date: 11/14/2023 11:35:26 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Coverage](
	[Id] [uniqueidentifier] ROWGUIDCOL  NOT NULL,
	[Name] [nvarchar](50) NOT NULL,
	[Rate] [decimal](10, 4) NOT NULL,
	[InvestmentMin] [bigint] NULL,
	[InvestmentMax] [bigint] NULL,
 CONSTRAINT [PK_Coverage] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[InvestmentRequest]    Script Date: 11/14/2023 11:35:26 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[InvestmentRequest](
	[Id] [uniqueidentifier] ROWGUIDCOL  NOT NULL,
	[UserId] [uniqueidentifier] NOT NULL,
	[Title] [nvarchar](1024) NOT NULL,
	[CreateDate] [datetime2](7) NOT NULL,
 CONSTRAINT [PK_InvestmentRequest] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[InvestmentValue]    Script Date: 11/14/2023 11:35:26 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[InvestmentValue](
	[Id] [uniqueidentifier] ROWGUIDCOL  NOT NULL,
	[InvestmentRequestId] [uniqueidentifier] NOT NULL,
	[CoverageId] [uniqueidentifier] NOT NULL,
	[Value] [decimal](18, 0) NOT NULL,
 CONSTRAINT [PK_InvestmentValue] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
INSERT [dbo].[Coverage] ([Id], [Name], [Rate], [InvestmentMin], [InvestmentMax]) VALUES (N'1453cb7f-3c2f-4762-bd2f-4a372d3ecaf4', N'Surgery', CAST(0.0052 AS Decimal(10, 4)), 5000, 500000000)
INSERT [dbo].[Coverage] ([Id], [Name], [Rate], [InvestmentMin], [InvestmentMax]) VALUES (N'02d24862-b84e-4a01-9cdd-94249bb4b50b', N'Hospitalization', CAST(0.0050 AS Decimal(10, 4)), 4000, 400000000)
INSERT [dbo].[Coverage] ([Id], [Name], [Rate], [InvestmentMin], [InvestmentMax]) VALUES (N'34c41c99-b4b8-4de0-89ba-c0b5b0b071b2', N'Dental', CAST(0.0042 AS Decimal(10, 4)), 2000, 200000000)
GO
ALTER TABLE [dbo].[Coverage] ADD  CONSTRAINT [DF_Coverage_Id]  DEFAULT (newid()) FOR [Id]
GO
ALTER TABLE [dbo].[InvestmentRequest] ADD  CONSTRAINT [DF_InvestmentRequest_Id]  DEFAULT (newid()) FOR [Id]
GO
ALTER TABLE [dbo].[InvestmentValue] ADD  CONSTRAINT [DF_InvestmentValue_Id]  DEFAULT (newid()) FOR [Id]
GO
ALTER TABLE [dbo].[InvestmentValue]  WITH CHECK ADD  CONSTRAINT [FK_InvestmentValue_Coverage] FOREIGN KEY([CoverageId])
REFERENCES [dbo].[Coverage] ([Id])
GO
ALTER TABLE [dbo].[InvestmentValue] CHECK CONSTRAINT [FK_InvestmentValue_Coverage]
GO
ALTER TABLE [dbo].[InvestmentValue]  WITH CHECK ADD  CONSTRAINT [FK_InvestmentValue_InvestmentRequest] FOREIGN KEY([InvestmentRequestId])
REFERENCES [dbo].[InvestmentRequest] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[InvestmentValue] CHECK CONSTRAINT [FK_InvestmentValue_InvestmentRequest]
GO
USE [master]
GO
ALTER DATABASE [InsurancePremiums] SET  READ_WRITE 
GO
