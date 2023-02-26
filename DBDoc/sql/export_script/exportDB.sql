USE [master]
GO
/****** Object:  Database [Airbnb]    Script Date: 03.02.2023 19:49:14 ******/
CREATE DATABASE [Airbnb]
 CONTAINMENT = NONE
 ON  PRIMARY 
( NAME = N'Airbnb', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL15.SQLEXPRESS\MSSQL\DATA\Airbnb.mdf' , SIZE = 8192KB , MAXSIZE = UNLIMITED, FILEGROWTH = 65536KB )
 LOG ON 
( NAME = N'Airbnb_log', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL15.SQLEXPRESS\MSSQL\DATA\Airbnb_log.ldf' , SIZE = 8192KB , MAXSIZE = 2048GB , FILEGROWTH = 65536KB )
 WITH CATALOG_COLLATION = DATABASE_DEFAULT
GO
ALTER DATABASE [Airbnb] SET COMPATIBILITY_LEVEL = 150
GO
IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [Airbnb].[dbo].[sp_fulltext_database] @action = 'enable'
end
GO
ALTER DATABASE [Airbnb] SET ANSI_NULL_DEFAULT OFF 
GO
ALTER DATABASE [Airbnb] SET ANSI_NULLS OFF 
GO
ALTER DATABASE [Airbnb] SET ANSI_PADDING OFF 
GO
ALTER DATABASE [Airbnb] SET ANSI_WARNINGS OFF 
GO
ALTER DATABASE [Airbnb] SET ARITHABORT OFF 
GO
ALTER DATABASE [Airbnb] SET AUTO_CLOSE ON 
GO
ALTER DATABASE [Airbnb] SET AUTO_SHRINK OFF 
GO
ALTER DATABASE [Airbnb] SET AUTO_UPDATE_STATISTICS ON 
GO
ALTER DATABASE [Airbnb] SET CURSOR_CLOSE_ON_COMMIT OFF 
GO
ALTER DATABASE [Airbnb] SET CURSOR_DEFAULT  GLOBAL 
GO
ALTER DATABASE [Airbnb] SET CONCAT_NULL_YIELDS_NULL OFF 
GO
ALTER DATABASE [Airbnb] SET NUMERIC_ROUNDABORT OFF 
GO
ALTER DATABASE [Airbnb] SET QUOTED_IDENTIFIER OFF 
GO
ALTER DATABASE [Airbnb] SET RECURSIVE_TRIGGERS OFF 
GO
ALTER DATABASE [Airbnb] SET  ENABLE_BROKER 
GO
ALTER DATABASE [Airbnb] SET AUTO_UPDATE_STATISTICS_ASYNC OFF 
GO
ALTER DATABASE [Airbnb] SET DATE_CORRELATION_OPTIMIZATION OFF 
GO
ALTER DATABASE [Airbnb] SET TRUSTWORTHY OFF 
GO
ALTER DATABASE [Airbnb] SET ALLOW_SNAPSHOT_ISOLATION OFF 
GO
ALTER DATABASE [Airbnb] SET PARAMETERIZATION SIMPLE 
GO
ALTER DATABASE [Airbnb] SET READ_COMMITTED_SNAPSHOT OFF 
GO
ALTER DATABASE [Airbnb] SET HONOR_BROKER_PRIORITY OFF 
GO
ALTER DATABASE [Airbnb] SET RECOVERY SIMPLE 
GO
ALTER DATABASE [Airbnb] SET  MULTI_USER 
GO
ALTER DATABASE [Airbnb] SET PAGE_VERIFY CHECKSUM  
GO
ALTER DATABASE [Airbnb] SET DB_CHAINING OFF 
GO
ALTER DATABASE [Airbnb] SET FILESTREAM( NON_TRANSACTED_ACCESS = OFF ) 
GO
ALTER DATABASE [Airbnb] SET TARGET_RECOVERY_TIME = 60 SECONDS 
GO
ALTER DATABASE [Airbnb] SET DELAYED_DURABILITY = DISABLED 
GO
ALTER DATABASE [Airbnb] SET ACCELERATED_DATABASE_RECOVERY = OFF  
GO
ALTER DATABASE [Airbnb] SET QUERY_STORE = OFF
GO
USE [Airbnb]
GO
/****** Object:  User [zamestnanec]    Script Date: 03.02.2023 19:49:15 ******/
CREATE USER [zamestnanec] WITHOUT LOGIN WITH DEFAULT_SCHEMA=[dbo]
GO
/****** Object:  User [zam]    Script Date: 03.02.2023 19:49:15 ******/
CREATE USER [zam] FOR LOGIN [zam] WITH DEFAULT_SCHEMA=[dbo]
GO
/****** Object:  User [spravce]    Script Date: 03.02.2023 19:49:15 ******/
CREATE USER [spravce] FOR LOGIN [spravce] WITH DEFAULT_SCHEMA=[dbo]
GO
/****** Object:  User [marketer]    Script Date: 03.02.2023 19:49:15 ******/
CREATE USER [marketer] FOR LOGIN [marketer] WITH DEFAULT_SCHEMA=[dbo]
GO
/****** Object:  User [analytik]    Script Date: 03.02.2023 19:49:15 ******/
CREATE USER [analytik] WITHOUT LOGIN WITH DEFAULT_SCHEMA=[dbo]
GO
/****** Object:  User [anal]    Script Date: 03.02.2023 19:49:15 ******/
CREATE USER [anal] FOR LOGIN [anal] WITH DEFAULT_SCHEMA=[dbo]
GO
ALTER ROLE [db_owner] ADD MEMBER [spravce]
GO
/****** Object:  Table [dbo].[Apartment]    Script Date: 03.02.2023 19:49:15 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Apartment](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[name_apartment] [varchar](50) NOT NULL,
	[fk_apartment_photo_id] [int] NOT NULL,
	[fk_apartment_location_id] [int] NOT NULL,
	[size] [decimal](18, 0) NOT NULL,
	[apartment_price] [decimal](18, 0) NOT NULL,
	[fk_apartment_review_id] [int] NOT NULL,
	[apartment_info] [varchar](200) NOT NULL,
	[number_of_guests] [int] NOT NULL,
	[number_of_bedroom] [int] NOT NULL,
	[number_of_beds] [int] NOT NULL,
	[number_of_bathrooms] [int] NOT NULL,
	[fk_apartment_user_owner_id] [int] NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Reservation]    Script Date: 03.02.2023 19:49:15 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Reservation](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[fk_apartment_id] [int] NOT NULL,
	[fk_apartment_costumer_id] [int] NOT NULL,
	[reservation_since] [date] NULL,
	[reservation_to] [date] NULL,
PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  View [dbo].[reservation_count]    Script Date: 03.02.2023 19:49:15 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE VIEW [dbo].[reservation_count] AS
SELECT a.id AS apartment_id, COUNT(r.id) AS 'počet rezervací'
FROM Apartment a
LEFT JOIN Reservation r ON r.id = a.id
GROUP BY a.id;
GO
/****** Object:  Table [dbo].[User]    Script Date: 03.02.2023 19:49:15 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[User](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[name] [varchar](50) NOT NULL,
	[second_name] [varchar](50) NOT NULL,
	[email] [varchar](50) NOT NULL,
	[password] [varchar](50) NOT NULL,
	[fk_type_of_user_id] [int] NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Photo]    Script Date: 03.02.2023 19:49:15 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Photo](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[name_of_photo] [varchar](50) NOT NULL,
	[fk_author_user_id] [int] NOT NULL,
	[day_of_acqusittion] [date] NOT NULL,
	[photo_path] [nchar](100) NULL,
PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  View [dbo].[user_photo_count]    Script Date: 03.02.2023 19:49:15 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE VIEW [dbo].[user_photo_count] AS
SELECT  u.id AS 'id',u.[name] AS 'uživatel',  COUNT(p.id) AS photo_count
FROM [User] u
LEFT JOIN Apartment a ON a.id = u.id
LEFT JOIN Photo p ON p.id = a.id
GROUP BY u.id,u.[name]
ORDER BY 'uživatel' ASC
OFFSET 0 ROWS;
GO
/****** Object:  View [dbo].[recent_reservations]    Script Date: 03.02.2023 19:49:15 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE VIEW [dbo].[recent_reservations] AS
SELECT u.name AS user_name, a.name_apartment AS apartment_number, re.reservation_since, re.reservation_to
FROM Reservation re
JOIN Apartment a ON a.id = re.id
JOIN [User] u ON u.id = a.id
WHERE re.reservation_since >= DATEADD(month, -1, CURRENT_TIMESTAMP);
GO
/****** Object:  Table [dbo].[Review]    Script Date: 03.02.2023 19:49:15 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Review](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[rating] [decimal](18, 0) NOT NULL,
	[comment] [varchar](250) NOT NULL,
	[fk_user_id] [int] NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  View [dbo].[apartment_review_scores]    Script Date: 03.02.2023 19:49:15 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE VIEW [dbo].[apartment_review_scores] AS
SELECT TOP (100) PERCENT a.id AS apartment_id, a.name_apartment AS 'název rezortu', AVG(r.rating) AS 'hodnocení'
FROM Apartment a
LEFT JOIN [User] u ON u.id = a.id
LEFT JOIN Review r ON r.id= u.id
GROUP BY a.id, a.name_apartment;
GO
/****** Object:  View [dbo].[reservation_countles]    Script Date: 03.02.2023 19:49:15 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE VIEW [dbo].[reservation_countles] AS
SELECT a.id AS apartment_id, COUNT(r.id) AS 'počet rezervací'
FROM Apartment a
LEFT JOIN Reservation r ON r.id = a.id
WHERE a.id = 1
GROUP BY a.id;
GO
/****** Object:  Table [dbo].[Location]    Script Date: 03.02.2023 19:49:15 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Location](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[name_of_location] [varchar](50) NOT NULL,
	[city] [varchar](50) NOT NULL,
	[state] [varchar](50) NOT NULL,
	[info] [varchar](250) NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Review_Backup]    Script Date: 03.02.2023 19:49:15 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Review_Backup](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[rating] [decimal](18, 0) NOT NULL,
	[comment] [varchar](250) NOT NULL,
	[fk_user_id] [int] NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Type_of_user]    Script Date: 03.02.2023 19:49:15 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Type_of_user](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[user_type] [varchar](20) NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING ON
GO
/****** Object:  Index [int_name_apartment]    Script Date: 03.02.2023 19:49:15 ******/
CREATE NONCLUSTERED INDEX [int_name_apartment] ON [dbo].[Apartment]
(
	[name_apartment] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
SET ANSI_PADDING ON
GO
/****** Object:  Index [int_location_city_name]    Script Date: 03.02.2023 19:49:15 ******/
CREATE NONCLUSTERED INDEX [int_location_city_name] ON [dbo].[Location]
(
	[name_of_location] ASC,
	[city] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
SET ANSI_PADDING ON
GO
/****** Object:  Index [int_location_name]    Script Date: 03.02.2023 19:49:15 ******/
CREATE NONCLUSTERED INDEX [int_location_name] ON [dbo].[Location]
(
	[name_of_location] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
SET ANSI_PADDING ON
GO
/****** Object:  Index [int_user_name]    Script Date: 03.02.2023 19:49:15 ******/
CREATE NONCLUSTERED INDEX [int_user_name] ON [dbo].[User]
(
	[name] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
ALTER TABLE [dbo].[Apartment]  WITH CHECK ADD FOREIGN KEY([fk_apartment_photo_id])
REFERENCES [dbo].[Photo] ([id])
GO
ALTER TABLE [dbo].[Apartment]  WITH CHECK ADD FOREIGN KEY([fk_apartment_location_id])
REFERENCES [dbo].[Location] ([id])
GO
ALTER TABLE [dbo].[Apartment]  WITH CHECK ADD FOREIGN KEY([fk_apartment_review_id])
REFERENCES [dbo].[Review] ([id])
GO
ALTER TABLE [dbo].[Apartment]  WITH CHECK ADD FOREIGN KEY([fk_apartment_user_owner_id])
REFERENCES [dbo].[User] ([id])
GO
ALTER TABLE [dbo].[Photo]  WITH CHECK ADD FOREIGN KEY([fk_author_user_id])
REFERENCES [dbo].[User] ([id])
GO
ALTER TABLE [dbo].[Reservation]  WITH CHECK ADD FOREIGN KEY([fk_apartment_id])
REFERENCES [dbo].[Apartment] ([id])
GO
ALTER TABLE [dbo].[Reservation]  WITH CHECK ADD FOREIGN KEY([fk_apartment_costumer_id])
REFERENCES [dbo].[User] ([id])
GO
ALTER TABLE [dbo].[Review]  WITH CHECK ADD FOREIGN KEY([fk_user_id])
REFERENCES [dbo].[User] ([id])
GO
ALTER TABLE [dbo].[Review_Backup]  WITH CHECK ADD FOREIGN KEY([fk_user_id])
REFERENCES [dbo].[User] ([id])
GO
ALTER TABLE [dbo].[User]  WITH CHECK ADD FOREIGN KEY([fk_type_of_user_id])
REFERENCES [dbo].[Type_of_user] ([id])
GO
ALTER TABLE [dbo].[Apartment]  WITH CHECK ADD CHECK  (([number_of_guests]>(0)))
GO
ALTER TABLE [dbo].[Apartment]  WITH CHECK ADD CHECK  (([number_of_bedroom]>(0)))
GO
ALTER TABLE [dbo].[Apartment]  WITH CHECK ADD CHECK  (([number_of_beds]>(0)))
GO
ALTER TABLE [dbo].[Apartment]  WITH CHECK ADD CHECK  (([number_of_bathrooms]>(0)))
GO
ALTER TABLE [dbo].[Apartment]  WITH CHECK ADD CHECK  (([size]>(0)))
GO
ALTER TABLE [dbo].[Location]  WITH CHECK ADD CHECK  (([city] like '[A-Za-z]%'))
GO
ALTER TABLE [dbo].[Location]  WITH CHECK ADD CHECK  (([state] like '[A-Za-z]%'))
GO
ALTER TABLE [dbo].[Review]  WITH CHECK ADD CHECK  (([rating]>=(1) AND [rating]<=(5)))
GO
ALTER TABLE [dbo].[Review_Backup]  WITH CHECK ADD CHECK  (([rating]>=(1) AND [rating]<=(5)))
GO
ALTER TABLE [dbo].[User]  WITH CHECK ADD CHECK  (([email] like '%@%'))
GO
ALTER TABLE [dbo].[User]  WITH CHECK ADD CHECK  (([name] like '[A-Za-z]%'))
GO
/****** Object:  StoredProcedure [dbo].[get_apartment_reservation_count]    Script Date: 03.02.2023 19:49:15 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[get_apartment_reservation_count]
(
   @apartment_id INT
)
AS
BEGIN
   SELECT COUNT(r.id) AS reservation_count
   FROM Apartment a
   INNER JOIN Reservation r ON r.id = a.id
   WHERE a.id = @apartment_id;
END
GO
/****** Object:  StoredProcedure [dbo].[get_location_review_average]    Script Date: 03.02.2023 19:49:15 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[get_location_review_average]
(
   @location_id INT
)
AS
BEGIN
   SELECT AVG(r.rating) AS average_score
   FROM [Location] l
   INNER JOIN Apartment a ON a.id = l.id
   INNER JOIN [User] u ON u.id = a.id
   INNER JOIN Review r ON r.id = u.id
   WHERE l.id = @location_id;
END
GO
/****** Object:  StoredProcedure [dbo].[get_user_review_count]    Script Date: 03.02.2023 19:49:15 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[get_user_review_count]
(
   @user_id INT
)
AS
BEGIN
   SELECT COUNT(r.id) AS review_count
   FROM [User] u
   INNER JOIN Review r ON r.id = u.id
   WHERE u.id = @user_id;
END
GO
USE [master]
GO
ALTER DATABASE [Airbnb] SET  READ_WRITE 
GO
