USE [master]
GO

/****** Object:  Database [agenda]    Script Date: 06/03/2024 14:23:45 ******/
CREATE DATABASE [agenda]
 CONTAINMENT = NONE
 ON  PRIMARY 
( NAME = N'agenda', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL16.SQLEXPRESS\MSSQL\DATA\agenda.mdf' , SIZE = 8192KB , MAXSIZE = UNLIMITED, FILEGROWTH = 65536KB )
 LOG ON 
( NAME = N'agenda_log', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL16.SQLEXPRESS\MSSQL\DATA\agenda_log.ldf' , SIZE = 8192KB , MAXSIZE = 2048GB , FILEGROWTH = 65536KB )
 WITH CATALOG_COLLATION = DATABASE_DEFAULT, LEDGER = OFF
GO

IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [agenda].[dbo].[sp_fulltext_database] @action = 'enable'
end
GO

ALTER DATABASE [agenda] SET ANSI_NULL_DEFAULT OFF 
GO

ALTER DATABASE [agenda] SET ANSI_NULLS OFF 
GO

ALTER DATABASE [agenda] SET ANSI_PADDING OFF 
GO

ALTER DATABASE [agenda] SET ANSI_WARNINGS OFF 
GO

ALTER DATABASE [agenda] SET ARITHABORT OFF 
GO

ALTER DATABASE [agenda] SET AUTO_CLOSE OFF 
GO

ALTER DATABASE [agenda] SET AUTO_SHRINK OFF 
GO

ALTER DATABASE [agenda] SET AUTO_UPDATE_STATISTICS ON 
GO

ALTER DATABASE [agenda] SET CURSOR_CLOSE_ON_COMMIT OFF 
GO

ALTER DATABASE [agenda] SET CURSOR_DEFAULT  GLOBAL 
GO

ALTER DATABASE [agenda] SET CONCAT_NULL_YIELDS_NULL OFF 
GO

ALTER DATABASE [agenda] SET NUMERIC_ROUNDABORT OFF 
GO

ALTER DATABASE [agenda] SET QUOTED_IDENTIFIER OFF 
GO

ALTER DATABASE [agenda] SET RECURSIVE_TRIGGERS OFF 
GO

ALTER DATABASE [agenda] SET  DISABLE_BROKER 
GO

ALTER DATABASE [agenda] SET AUTO_UPDATE_STATISTICS_ASYNC OFF 
GO

ALTER DATABASE [agenda] SET DATE_CORRELATION_OPTIMIZATION OFF 
GO

ALTER DATABASE [agenda] SET TRUSTWORTHY OFF 
GO

ALTER DATABASE [agenda] SET ALLOW_SNAPSHOT_ISOLATION OFF 
GO

ALTER DATABASE [agenda] SET PARAMETERIZATION SIMPLE 
GO

ALTER DATABASE [agenda] SET READ_COMMITTED_SNAPSHOT OFF 
GO

ALTER DATABASE [agenda] SET HONOR_BROKER_PRIORITY OFF 
GO

ALTER DATABASE [agenda] SET RECOVERY SIMPLE 
GO

ALTER DATABASE [agenda] SET  MULTI_USER 
GO

ALTER DATABASE [agenda] SET PAGE_VERIFY CHECKSUM  
GO

ALTER DATABASE [agenda] SET DB_CHAINING OFF 
GO

ALTER DATABASE [agenda] SET FILESTREAM( NON_TRANSACTED_ACCESS = OFF ) 
GO

ALTER DATABASE [agenda] SET TARGET_RECOVERY_TIME = 60 SECONDS 
GO

ALTER DATABASE [agenda] SET DELAYED_DURABILITY = DISABLED 
GO

ALTER DATABASE [agenda] SET ACCELERATED_DATABASE_RECOVERY = OFF  
GO

ALTER DATABASE [agenda] SET QUERY_STORE = ON
GO

ALTER DATABASE [agenda] SET QUERY_STORE (OPERATION_MODE = READ_WRITE, CLEANUP_POLICY = (STALE_QUERY_THRESHOLD_DAYS = 30), DATA_FLUSH_INTERVAL_SECONDS = 900, INTERVAL_LENGTH_MINUTES = 60, MAX_STORAGE_SIZE_MB = 1000, QUERY_CAPTURE_MODE = AUTO, SIZE_BASED_CLEANUP_MODE = AUTO, MAX_PLANS_PER_QUERY = 200, WAIT_STATS_CAPTURE_MODE = ON)
GO

ALTER DATABASE [agenda] SET  READ_WRITE 
GO

