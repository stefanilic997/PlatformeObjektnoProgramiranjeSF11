USE [master]
GO
CREATE DATABASE [POP]
 CONTAINMENT = NONE
 ON  PRIMARY 
( NAME = N'POPSalon', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL12.SQLEXPRESS\MSSQL\DATA\POPSalon.mdf' , SIZE = 5120KB , MAXSIZE = UNLIMITED, FILEGROWTH = 1024KB )
 LOG ON 
( NAME = N'POPSalon_log', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL12.SQLEXPRESS\MSSQL\DATA\POPSalon_log.ldf' , SIZE = 2048KB , MAXSIZE = 2048GB , FILEGROWTH = 10%)
GO
ALTER DATABASE [POP] SET COMPATIBILITY_LEVEL = 120
GO
IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [POP].[dbo].[sp_fulltext_database] @action = 'enable'
end
GO
ALTER DATABASE [POP] SET ANSI_NULL_DEFAULT OFF 
GO
ALTER DATABASE [POP] SET ANSI_NULLS OFF 
GO
ALTER DATABASE [POP] SET ANSI_PADDING OFF 
GO
ALTER DATABASE [POP] SET ANSI_WARNINGS OFF 
GO
ALTER DATABASE [POP] SET ARITHABORT OFF 
GO
ALTER DATABASE [POP] SET AUTO_CLOSE OFF 
GO
ALTER DATABASE [POP] SET AUTO_SHRINK OFF 
GO
ALTER DATABASE [POP] SET AUTO_UPDATE_STATISTICS ON 
GO
ALTER DATABASE [POP] SET CURSOR_CLOSE_ON_COMMIT OFF 
GO
ALTER DATABASE [POP] SET CURSOR_DEFAULT  GLOBAL 
GO
ALTER DATABASE [POP] SET CONCAT_NULL_YIELDS_NULL OFF 
GO
ALTER DATABASE [POP] SET NUMERIC_ROUNDABORT OFF 
GO
ALTER DATABASE [POP] SET QUOTED_IDENTIFIER OFF 
GO
ALTER DATABASE [POP] SET RECURSIVE_TRIGGERS OFF 
GO
ALTER DATABASE [POP] SET  DISABLE_BROKER 
GO
ALTER DATABASE [POP] SET AUTO_UPDATE_STATISTICS_ASYNC OFF 
GO
ALTER DATABASE [POP] SET DATE_CORRELATION_OPTIMIZATION OFF 
GO
ALTER DATABASE [POP] SET TRUSTWORTHY OFF 
GO
ALTER DATABASE [POP] SET ALLOW_SNAPSHOT_ISOLATION OFF 
GO
ALTER DATABASE [POP] SET PARAMETERIZATION SIMPLE 
GO
ALTER DATABASE [POP] SET READ_COMMITTED_SNAPSHOT OFF 
GO
ALTER DATABASE [POP] SET HONOR_BROKER_PRIORITY OFF 
GO
ALTER DATABASE [POP] SET RECOVERY SIMPLE 
GO
ALTER DATABASE [POP] SET  MULTI_USER 
GO
ALTER DATABASE [POP] SET PAGE_VERIFY CHECKSUM  
GO
ALTER DATABASE [POP] SET DB_CHAINING OFF 
GO
ALTER DATABASE [POP] SET FILESTREAM( NON_TRANSACTED_ACCESS = OFF ) 
GO
ALTER DATABASE [POP] SET TARGET_RECOVERY_TIME = 0 SECONDS 
GO
ALTER DATABASE [POP] SET DELAYED_DURABILITY = DISABLED 
GO
USE [POP]
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [Akcije](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[DatumPocetka] [date] NULL,
	[DatumZavresetka] [date] NULL,
	[Popust] [int] NULL,
	[Obrisan] [bit] NULL,
PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [DodatneUsluge](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Naziv] [varchar](50) NULL,
	[Cena] [float] NULL,
	[Obrisan] [bit] NULL,
PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [Korisnici](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Ime] [varchar](100) NULL,
	[Prezime] [varchar](100) NULL,
	[KorisnickoIme] [varchar](50) NULL,
	[Lozinka] [varchar](50) NULL,
	[TipKorisnika] [varchar](30) NOT NULL,
	[Obrisan] [bit] NULL,
PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [Namestaj](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Naziv] [varchar](80) NULL,
	[Cena] [numeric](9, 2) NULL,
	[Kolicina] [int] NULL,
	[TipNamestajaId] [int] NULL,
	[AkcijaId] [int] NULL,
	[Obrisan] [bit] NULL,
 CONSTRAINT [PK__Namestaj__3214EC07D087654F] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [Racuni](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[DatumProdaje] [datetime] NULL,
	[Kupac] [varchar](100) NULL,
	[BrojRacuna] [varchar](100) NULL,
	[UkupnaCena] [numeric](9, 2) NULL,
	[Obrisan] [bit] NOT NULL,
 CONSTRAINT [PK__Racuni__3214EC07579641A4] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [Saloni](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Naziv] [varchar](50) NULL,
	[Adresa] [varchar](100) NULL,
	[Telefon] [varchar](30) NULL,
	[Email] [varchar](50) NULL,
	[AdresaSajta] [varchar](40) NULL,
	[PIB] [int] NULL,
	[MaticniBroj] [int] NULL,
	[BrojZiroRacuna] [varchar](50) NULL,
	[Obrisan] [bit] NULL,
PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [StavkeProdajeDodatneUsluge](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Naziv] [varchar](50) NULL,
	[RacunId] [int] NULL,
	[DodatnaUslugaId] [int] NULL,
 CONSTRAINT [PK__StavkePr__3214EC0771A0650C] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [StavkeProdajeNamestaja](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Naziv] [varchar](50) NULL,
	[RacunId] [int] NULL,
	[NamestajId] [int] NULL,
	[Kolicina] [int] NULL,
 CONSTRAINT [PK__StavkePr__3214EC0717989BCB] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [TipNamestaja](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Naziv] [varchar](80) NULL,
	[Obrisan] [bit] NULL,
PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET IDENTITY_INSERT [Akcije] ON 

INSERT [Akcije] ([Id], [DatumPocetka], [DatumZavresetka], [Popust], [Obrisan]) VALUES (1, CAST(N'2017-12-21' AS Date), CAST(N'2017-12-29' AS Date), 5, 0)
INSERT [Akcije] ([Id], [DatumPocetka], [DatumZavresetka], [Popust], [Obrisan]) VALUES (2, CAST(N'2018-01-01' AS Date), CAST(N'2018-01-15' AS Date), 5, 0)
INSERT [Akcije] ([Id], [DatumPocetka], [DatumZavresetka], [Popust], [Obrisan]) VALUES (3, CAST(N'2018-01-07' AS Date), CAST(N'2018-01-09' AS Date), 3, 0)
SET IDENTITY_INSERT [Akcije] OFF
SET IDENTITY_INSERT [DodatneUsluge] ON 

INSERT [DodatneUsluge] ([Id], [Naziv], [Cena], [Obrisan]) VALUES (1, N'Montaza', 300, 0)
INSERT [DodatneUsluge] ([Id], [Naziv], [Cena], [Obrisan]) VALUES (2, N'Ugradnja', 450, 0)
INSERT [DodatneUsluge] ([Id], [Naziv], [Cena], [Obrisan]) VALUES (3, N'Prenos', 250, 0)
INSERT [DodatneUsluge] ([Id], [Naziv], [Cena], [Obrisan]) VALUES (4, N'Iznos', 100, 0)
INSERT [DodatneUsluge] ([Id], [Naziv], [Cena], [Obrisan]) VALUES (5, N'Probba', 312, 1)
SET IDENTITY_INSERT [DodatneUsluge] OFF
SET IDENTITY_INSERT [Korisnici] ON 

INSERT [Korisnici] ([Id], [Ime], [Prezime], [KorisnickoIme], [Lozinka], [TipKorisnika], [Obrisan]) VALUES (1, N'proba', N'probic', N'a', N'a', N'Administrator', 0)
INSERT [Korisnici] ([Id], [Ime], [Prezime], [KorisnickoIme], [Lozinka], [TipKorisnika], [Obrisan]) VALUES (1003, N'b', N'b', N'b', N'b', N'Prodavac', 0)
SET IDENTITY_INSERT [Korisnici] OFF
SET IDENTITY_INSERT [Namestaj] ON 

INSERT [Namestaj] ([Id], [Naziv], [Cena], [Kolicina], [TipNamestajaId], [AkcijaId], [Obrisan]) VALUES (1, N'Sofa', CAST(1253.00 AS Numeric(9, 2)), 4, 1010, 1, 0)
INSERT [Namestaj] ([Id], [Naziv], [Cena], [Kolicina], [TipNamestajaId], [AkcijaId], [Obrisan]) VALUES (2, N'Kauc', CAST(2212.00 AS Numeric(9, 2)), 2, 1009, 2, 1)
INSERT [Namestaj] ([Id], [Naziv], [Cena], [Kolicina], [TipNamestajaId], [AkcijaId], [Obrisan]) VALUES (3, N'Stolica', CAST(355.00 AS Numeric(9, 2)), 2, 1009, 3, 0)
INSERT [Namestaj] ([Id], [Naziv], [Cena], [Kolicina], [TipNamestajaId], [AkcijaId], [Obrisan]) VALUES (5, N'Nocni sto', CAST(350.00 AS Numeric(9, 2)), 7, 1013, 3, 0)
INSERT [Namestaj] ([Id], [Naziv], [Cena], [Kolicina], [TipNamestajaId], [AkcijaId], [Obrisan]) VALUES (6, N'Dvosed', CAST(5500.00 AS Numeric(9, 2)), 5, 1015, 2, 0)
INSERT [Namestaj] ([Id], [Naziv], [Cena], [Kolicina], [TipNamestajaId], [AkcijaId], [Obrisan]) VALUES (7, N'Viseci(Kuhinja)', CAST(778.00 AS Numeric(9, 2)), 9, 1016, 2, 0)
SET IDENTITY_INSERT [Namestaj] OFF
SET IDENTITY_INSERT [Racuni] ON 

INSERT [Racuni] ([Id], [DatumProdaje], [Kupac], [BrojRacuna], [UkupnaCena], [Obrisan]) VALUES (1, CAST(N'2018-01-01T00:00:00.000' AS DateTime), N'Nikogovic', N'2234', CAST(750231.00 AS Numeric(9, 2)), 1)
INSERT [Racuni] ([Id], [DatumProdaje], [Kupac], [BrojRacuna], [UkupnaCena], [Obrisan]) VALUES (2, CAST(N'2018-01-10T21:00:23.563' AS DateTime), N'stefan', N'331', CAST(-6390.30 AS Numeric(9, 2)), 1)
INSERT [Racuni] ([Id], [DatumProdaje], [Kupac], [BrojRacuna], [UkupnaCena], [Obrisan]) VALUES (3, CAST(N'2018-01-10T21:39:33.013' AS DateTime), N'312', N'3123', CAST(23923.08 AS Numeric(9, 2)), 1)
INSERT [Racuni] ([Id], [DatumProdaje], [Kupac], [BrojRacuna], [UkupnaCena], [Obrisan]) VALUES (4, CAST(N'2018-01-10T22:22:14.970' AS DateTime), N'mile', N'da32', CAST(7500.00 AS Numeric(9, 2)), 0)
INSERT [Racuni] ([Id], [DatumProdaje], [Kupac], [BrojRacuna], [UkupnaCena], [Obrisan]) VALUES (5, CAST(N'2018-01-10T22:34:25.023' AS DateTime), N'Djape', N'sfd2', CAST(1377.00 AS Numeric(9, 2)), 0)
INSERT [Racuni] ([Id], [DatumProdaje], [Kupac], [BrojRacuna], [UkupnaCena], [Obrisan]) VALUES (6, CAST(N'2018-01-10T22:40:14.753' AS DateTime), N'fsaw', N'31as', CAST(15963.00 AS Numeric(9, 2)), 0)
INSERT [Racuni] ([Id], [DatumProdaje], [Kupac], [BrojRacuna], [UkupnaCena], [Obrisan]) VALUES (7, CAST(N'2018-01-10T22:45:26.623' AS DateTime), N'jgfb', N'bsd3', CAST(11322.00 AS Numeric(9, 2)), 0)
SET IDENTITY_INSERT [Racuni] OFF
SET IDENTITY_INSERT [Saloni] ON 

INSERT [Saloni] ([Id], [Naziv], [Adresa], [Telefon], [Email], [AdresaSajta], [PIB], [MaticniBroj], [BrojZiroRacuna], [Obrisan]) VALUES (1, N'esh', N'ispod mosta bb', N'021/332/31', N'esh@gmail.com', N'www.esh.com', 31650, 673290, N'341-2135-123', 0)
SET IDENTITY_INSERT [Saloni] OFF
SET IDENTITY_INSERT [StavkeProdajeDodatneUsluge] ON 

INSERT [StavkeProdajeDodatneUsluge] ([Id], [Naziv], [RacunId], [DodatnaUslugaId]) VALUES (4, N'Ugradnja', 4, 2)
INSERT [StavkeProdajeDodatneUsluge] ([Id], [Naziv], [RacunId], [DodatnaUslugaId]) VALUES (6, N'Montaza', 5, 1)
INSERT [StavkeProdajeDodatneUsluge] ([Id], [Naziv], [RacunId], [DodatnaUslugaId]) VALUES (7, N'Prenos', 6, 3)
INSERT [StavkeProdajeDodatneUsluge] ([Id], [Naziv], [RacunId], [DodatnaUslugaId]) VALUES (8, N'Prenos', 6, 3)
INSERT [StavkeProdajeDodatneUsluge] ([Id], [Naziv], [RacunId], [DodatnaUslugaId]) VALUES (9, N'Iznos', 7, 4)
SET IDENTITY_INSERT [StavkeProdajeDodatneUsluge] OFF
SET IDENTITY_INSERT [StavkeProdajeNamestaja] ON 

INSERT [StavkeProdajeNamestaja] ([Id], [Naziv], [RacunId], [NamestajId], [Kolicina]) VALUES (5, N'Nocni sto', 0, 5, 1)
INSERT [StavkeProdajeNamestaja] ([Id], [Naziv], [RacunId], [NamestajId], [Kolicina]) VALUES (10, N'Nocni sto', 4, 5, 2)
INSERT [StavkeProdajeNamestaja] ([Id], [Naziv], [RacunId], [NamestajId], [Kolicina]) VALUES (13, N'Nocni sto', 5, 5, 2)
INSERT [StavkeProdajeNamestaja] ([Id], [Naziv], [RacunId], [NamestajId], [Kolicina]) VALUES (14, N'Stolica', 6, 3, 2)
INSERT [StavkeProdajeNamestaja] ([Id], [Naziv], [RacunId], [NamestajId], [Kolicina]) VALUES (15, N'Dvosed', 7, 6, 1)
SET IDENTITY_INSERT [StavkeProdajeNamestaja] OFF
SET IDENTITY_INSERT [TipNamestaja] ON 

INSERT [TipNamestaja] ([Id], [Naziv], [Obrisan]) VALUES (1009, N'Stolica', 0)
INSERT [TipNamestaja] ([Id], [Naziv], [Obrisan]) VALUES (1010, N'Krevet', 0)
INSERT [TipNamestaja] ([Id], [Naziv], [Obrisan]) VALUES (1011, N'Lampa', 0)
INSERT [TipNamestaja] ([Id], [Naziv], [Obrisan]) VALUES (1012, N'Fotelja', 1)
INSERT [TipNamestaja] ([Id], [Naziv], [Obrisan]) VALUES (1013, N'Sto', 0)
INSERT [TipNamestaja] ([Id], [Naziv], [Obrisan]) VALUES (1014, N'Polica', 1)
INSERT [TipNamestaja] ([Id], [Naziv], [Obrisan]) VALUES (1015, N'Kauc', 0)
INSERT [TipNamestaja] ([Id], [Naziv], [Obrisan]) VALUES (1016, N'Fioka', 0)
INSERT [TipNamestaja] ([Id], [Naziv], [Obrisan]) VALUES (1017, N'Radni sto', 0)
SET IDENTITY_INSERT [TipNamestaja] OFF
ALTER TABLE [Namestaj]  WITH CHECK ADD  CONSTRAINT [FK__Namestaj__Akcija__38996AB5] FOREIGN KEY([AkcijaId])
REFERENCES [Akcije] ([Id])
GO
ALTER TABLE [Namestaj] CHECK CONSTRAINT [FK__Namestaj__Akcija__38996AB5]
GO
ALTER TABLE [Namestaj]  WITH CHECK ADD  CONSTRAINT [FK__Namestaj__TipNam__145C0A3F] FOREIGN KEY([TipNamestajaId])
REFERENCES [TipNamestaja] ([Id])
GO
ALTER TABLE [Namestaj] CHECK CONSTRAINT [FK__Namestaj__TipNam__145C0A3F]
GO
ALTER TABLE [Korisnici]  WITH CHECK ADD CHECK  (([TipKorisnika]='Prodavac' OR [TipKorisnika]='Administrator'))
GO
USE [master]
GO
ALTER DATABASE [POP] SET  READ_WRITE 
GO
