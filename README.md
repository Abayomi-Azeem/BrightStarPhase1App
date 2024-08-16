# BrightStarPhase1App

This project is a Web API, focusing on basic API functionalities such as user authentication, subscription management, and status checking. The API uses token-based authentication to ensure secure access to the services.

It's built using Vertical SLice Architecturee and Mediator Pattern.


### Database Creation Script
USE [master]
GO
/****** Object:  Database [BrightStarDb]    Script Date: 8/16/2024 11:19:31 PM ******/
CREATE DATABASE [BrightStarDb]
 CONTAINMENT = NONE
 ON  PRIMARY 
( NAME = N'BrightStarDb', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL15.SQLEXPRESS\MSSQL\DATA\BrightStarDb.mdf' , SIZE = 8192KB , MAXSIZE = UNLIMITED, FILEGROWTH = 65536KB )
 LOG ON 
( NAME = N'BrightStarDb_log', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL15.SQLEXPRESS\MSSQL\DATA\BrightStarDb_log.ldf' , SIZE = 8192KB , MAXSIZE = 2048GB , FILEGROWTH = 65536KB )
 WITH CATALOG_COLLATION = DATABASE_DEFAULT
GO
ALTER DATABASE [BrightStarDb] SET COMPATIBILITY_LEVEL = 150
GO
IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [BrightStarDb].[dbo].[sp_fulltext_database] @action = 'enable'
end
GO
ALTER DATABASE [BrightStarDb] SET ANSI_NULL_DEFAULT OFF 
GO
ALTER DATABASE [BrightStarDb] SET ANSI_NULLS OFF 
GO
ALTER DATABASE [BrightStarDb] SET ANSI_PADDING OFF 
GO
ALTER DATABASE [BrightStarDb] SET ANSI_WARNINGS OFF 
GO
ALTER DATABASE [BrightStarDb] SET ARITHABORT OFF 
GO
ALTER DATABASE [BrightStarDb] SET AUTO_CLOSE ON 
GO
ALTER DATABASE [BrightStarDb] SET AUTO_SHRINK OFF 
GO
ALTER DATABASE [BrightStarDb] SET AUTO_UPDATE_STATISTICS ON 
GO
ALTER DATABASE [BrightStarDb] SET CURSOR_CLOSE_ON_COMMIT OFF 
GO
ALTER DATABASE [BrightStarDb] SET CURSOR_DEFAULT  GLOBAL 
GO
ALTER DATABASE [BrightStarDb] SET CONCAT_NULL_YIELDS_NULL OFF 
GO
ALTER DATABASE [BrightStarDb] SET NUMERIC_ROUNDABORT OFF 
GO
ALTER DATABASE [BrightStarDb] SET QUOTED_IDENTIFIER OFF 
GO
ALTER DATABASE [BrightStarDb] SET RECURSIVE_TRIGGERS OFF 
GO
ALTER DATABASE [BrightStarDb] SET  ENABLE_BROKER 
GO
ALTER DATABASE [BrightStarDb] SET AUTO_UPDATE_STATISTICS_ASYNC OFF 
GO
ALTER DATABASE [BrightStarDb] SET DATE_CORRELATION_OPTIMIZATION OFF 
GO
ALTER DATABASE [BrightStarDb] SET TRUSTWORTHY OFF 
GO
ALTER DATABASE [BrightStarDb] SET ALLOW_SNAPSHOT_ISOLATION OFF 
GO
ALTER DATABASE [BrightStarDb] SET PARAMETERIZATION SIMPLE 
GO
ALTER DATABASE [BrightStarDb] SET READ_COMMITTED_SNAPSHOT ON 
GO
ALTER DATABASE [BrightStarDb] SET HONOR_BROKER_PRIORITY OFF 
GO
ALTER DATABASE [BrightStarDb] SET RECOVERY SIMPLE 
GO
ALTER DATABASE [BrightStarDb] SET  MULTI_USER 
GO
ALTER DATABASE [BrightStarDb] SET PAGE_VERIFY CHECKSUM  
GO
ALTER DATABASE [BrightStarDb] SET DB_CHAINING OFF 
GO
ALTER DATABASE [BrightStarDb] SET FILESTREAM( NON_TRANSACTED_ACCESS = OFF ) 
GO
ALTER DATABASE [BrightStarDb] SET TARGET_RECOVERY_TIME = 60 SECONDS 
GO
ALTER DATABASE [BrightStarDb] SET DELAYED_DURABILITY = DISABLED 
GO
ALTER DATABASE [BrightStarDb] SET ACCELERATED_DATABASE_RECOVERY = OFF  
GO
ALTER DATABASE [BrightStarDb] SET QUERY_STORE = OFF
GO
USE [BrightStarDb]
GO
/****** Object:  Table [dbo].[__EFMigrationsHistory]    Script Date: 8/16/2024 11:19:31 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[__EFMigrationsHistory](
	[MigrationId] [nvarchar](150) NOT NULL,
	[ProductVersion] [nvarchar](32) NOT NULL,
 CONSTRAINT [PK___EFMigrationsHistory] PRIMARY KEY CLUSTERED 
(
	[MigrationId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Services]    Script Date: 8/16/2024 11:19:31 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Services](
	[Id] [uniqueidentifier] NOT NULL,
	[Name] [nvarchar](100) NOT NULL,
	[Description] [nvarchar](500) NOT NULL,
	[Password] [nvarchar](max) NOT NULL,
	[TokenExpiryHours] [int] NOT NULL,
 CONSTRAINT [PK_Services] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[ServiceTokens]    Script Date: 8/16/2024 11:19:31 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[ServiceTokens](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Token] [nvarchar](100) NOT NULL,
	[TokenExpirationDate] [datetime2](7) NULL,
	[TokenDateCreated] [datetime2](7) NOT NULL,
	[ServiceId] [uniqueidentifier] NOT NULL,
 CONSTRAINT [PK_ServiceTokens] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Subscribers]    Script Date: 8/16/2024 11:19:31 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Subscribers](
	[Id] [uniqueidentifier] NOT NULL,
	[PhoneNumber] [nvarchar](20) NOT NULL,
	[DateCreated] [datetime2](7) NOT NULL,
	[DateModified] [datetime2](7) NULL,
	[IsActive] [bit] NOT NULL,
	[ServiceId] [uniqueidentifier] NOT NULL,
 CONSTRAINT [PK_Subscribers] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Index [IX_ServiceTokens_ServiceId]    Script Date: 8/16/2024 11:19:31 PM ******/
CREATE NONCLUSTERED INDEX [IX_ServiceTokens_ServiceId] ON [dbo].[ServiceTokens]
(
	[ServiceId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
/****** Object:  Index [IX_Subscribers_ServiceId]    Script Date: 8/16/2024 11:19:31 PM ******/
CREATE NONCLUSTERED INDEX [IX_Subscribers_ServiceId] ON [dbo].[Subscribers]
(
	[ServiceId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
ALTER TABLE [dbo].[ServiceTokens]  WITH CHECK ADD  CONSTRAINT [FK_ServiceTokens_Services_ServiceId] FOREIGN KEY([ServiceId])
REFERENCES [dbo].[Services] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[ServiceTokens] CHECK CONSTRAINT [FK_ServiceTokens_Services_ServiceId]
GO
ALTER TABLE [dbo].[Subscribers]  WITH CHECK ADD  CONSTRAINT [FK_Subscribers_Services_ServiceId] FOREIGN KEY([ServiceId])
REFERENCES [dbo].[Services] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[Subscribers] CHECK CONSTRAINT [FK_Subscribers_Services_ServiceId]
GO
USE [master]
GO
ALTER DATABASE [BrightStarDb] SET  READ_WRITE 
GO


#####################
## Getting Started

1. **Clone the repository**:
    ```bash
    git clone https://github.com/Abayomi-Azeem/BrightStarPhase1.git
    cd BrightStarPhase1
    ```

2. **Configure the database**:
   - Update the connection string in `appsettings.json`:
     ```json
     "ConnectionStrings": {
         "DefaultConnection": "Server=(localdb)\\mssqllocaldb;Database=BrightStarPhase1tDb;Trusted_Connection=True;"
     }
     ```

3. **Run database migrations**:
    ```bash
    dotnet ef database update
    ```

4. **Run the application**:
    ```bash
    dotnet run
    ```

5. **Access the API**:
   - Use a tool like [Postman](https://www.postman.com/) to interact with the API endpoints.

## API Endpoints

1. **Login**: `POST /api/auth/login`
2. **Subscribe**: `POST /api/services/subscribe`
3. **Unsubscribe**: `POST /api/services/unsubscribe`
4. **Check Status**: `GET /api/services/status`

## Response Codes
OK = 0,
Error = 1,
InvalidPassword=3,
InvalidServiceId=4,
InvalidTokenId =5,
TokenExpired=6,
SubscriptionExists=7,
SubscriptionNotFound=8

