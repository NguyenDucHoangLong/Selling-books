USE [master]
GO
/****** Object:  Database [QLSach]    Script Date: 12/19/2015 10:41:26 AM ******/
CREATE DATABASE [QLSach]
 CONTAINMENT = NONE
 ON  PRIMARY 
( NAME = N'QLSach', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL11.SQLEXPRESS\MSSQL\DATA\QLSach.mdf' , SIZE = 4096KB , MAXSIZE = UNLIMITED, FILEGROWTH = 1024KB )
 LOG ON 
( NAME = N'QLSach_log', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL11.SQLEXPRESS\MSSQL\DATA\QLSach_log.ldf' , SIZE = 1024KB , MAXSIZE = 2048GB , FILEGROWTH = 10%)
GO
ALTER DATABASE [QLSach] SET COMPATIBILITY_LEVEL = 110
GO
IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [QLSach].[dbo].[sp_fulltext_database] @action = 'enable'
end
GO
ALTER DATABASE [QLSach] SET ANSI_NULL_DEFAULT OFF 
GO
ALTER DATABASE [QLSach] SET ANSI_NULLS OFF 
GO
ALTER DATABASE [QLSach] SET ANSI_PADDING OFF 
GO
ALTER DATABASE [QLSach] SET ANSI_WARNINGS OFF 
GO
ALTER DATABASE [QLSach] SET ARITHABORT OFF 
GO
ALTER DATABASE [QLSach] SET AUTO_CLOSE OFF 
GO
ALTER DATABASE [QLSach] SET AUTO_CREATE_STATISTICS ON 
GO
ALTER DATABASE [QLSach] SET AUTO_SHRINK OFF 
GO
ALTER DATABASE [QLSach] SET AUTO_UPDATE_STATISTICS ON 
GO
ALTER DATABASE [QLSach] SET CURSOR_CLOSE_ON_COMMIT OFF 
GO
ALTER DATABASE [QLSach] SET CURSOR_DEFAULT  GLOBAL 
GO
ALTER DATABASE [QLSach] SET CONCAT_NULL_YIELDS_NULL OFF 
GO
ALTER DATABASE [QLSach] SET NUMERIC_ROUNDABORT OFF 
GO
ALTER DATABASE [QLSach] SET QUOTED_IDENTIFIER OFF 
GO
ALTER DATABASE [QLSach] SET RECURSIVE_TRIGGERS OFF 
GO
ALTER DATABASE [QLSach] SET  DISABLE_BROKER 
GO
ALTER DATABASE [QLSach] SET AUTO_UPDATE_STATISTICS_ASYNC OFF 
GO
ALTER DATABASE [QLSach] SET DATE_CORRELATION_OPTIMIZATION OFF 
GO
ALTER DATABASE [QLSach] SET TRUSTWORTHY OFF 
GO
ALTER DATABASE [QLSach] SET ALLOW_SNAPSHOT_ISOLATION OFF 
GO
ALTER DATABASE [QLSach] SET PARAMETERIZATION SIMPLE 
GO
ALTER DATABASE [QLSach] SET READ_COMMITTED_SNAPSHOT OFF 
GO
ALTER DATABASE [QLSach] SET HONOR_BROKER_PRIORITY OFF 
GO
ALTER DATABASE [QLSach] SET RECOVERY SIMPLE 
GO
ALTER DATABASE [QLSach] SET  MULTI_USER 
GO
ALTER DATABASE [QLSach] SET PAGE_VERIFY CHECKSUM  
GO
ALTER DATABASE [QLSach] SET DB_CHAINING OFF 
GO
ALTER DATABASE [QLSach] SET FILESTREAM( NON_TRANSACTED_ACCESS = OFF ) 
GO
ALTER DATABASE [QLSach] SET TARGET_RECOVERY_TIME = 0 SECONDS 
GO
USE [QLSach]
GO
/****** Object:  StoredProcedure [dbo].[KiemTraTaiKhoan]    Script Date: 12/19/2015 10:41:26 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
create procedure [dbo].[KiemTraTaiKhoan] @username nvarchar(30), @pass nvarchar(20)
as
begin
	select *
	from TaiKhoan
	where MatKhau=@pass and TenDangNhap=@username
end







GO
/****** Object:  StoredProcedure [dbo].[ThemDonHang]    Script Date: 12/19/2015 10:41:26 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
create procedure [dbo].[ThemDonHang] @masach int,@ngaygiao date, @soluong int,@tinhtrang nvarchar(20),@mahopdong int
as
begin
	declare @dongia decimal;
	set @dongia=(select DonGia from ChiTietHopDong where MaSach=@masach and MaHopDong=@mahopdong);
	declare @tong decimal;
	set @tong=@soluong * @dongia;
	insert into DonHang_NCC values(@masach,@ngaygiao,@soluong,@tinhtrang,@tong,@mahopdong)
end







GO
/****** Object:  StoredProcedure [dbo].[ThongTinSanPham]    Script Date: 12/19/2015 10:41:26 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
create procedure [dbo].[ThongTinSanPham] @mancc int
as
begin
	select hd.MaHopDong, s.MaSach, s.TenSach, s.SoLuongTon
	from Sach s, HopDong hd, ChiTietHopDong ct
	where s.MaSach=ct.MaSach and hd.MaHopDong=ct.MaHopDong and hd.TinhTrang=1 and hd.MaNCC=@mancc
end







GO
/****** Object:  Table [dbo].[ChiTietHopDong]    Script Date: 12/19/2015 10:41:26 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[ChiTietHopDong](
	[MaHopDong] [int] NOT NULL,
	[MaSach] [int] NOT NULL,
	[DonGia] [decimal](18, 0) NULL,
 CONSTRAINT [PK_ChiTietHopDong] PRIMARY KEY CLUSTERED 
(
	[MaHopDong] ASC,
	[MaSach] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[CTDH_KH]    Script Date: 12/19/2015 10:41:26 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[CTDH_KH](
	[MaDonHang] [int] NOT NULL,
	[MaSach] [int] NOT NULL,
	[SoLuong] [int] NULL,
	[DonGia] [decimal](18, 0) NULL,
	[ThanhTien] [decimal](18, 0) NULL,
 CONSTRAINT [PK_CTDH_KH] PRIMARY KEY CLUSTERED 
(
	[MaDonHang] ASC,
	[MaSach] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[DanhMuc]    Script Date: 12/19/2015 10:41:26 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[DanhMuc](
	[MaDanhMuc] [int] IDENTITY(1,1) NOT NULL,
	[TenDanhMuc] [nvarchar](50) NULL,
 CONSTRAINT [PK_DanhMuc] PRIMARY KEY CLUSTERED 
(
	[MaDanhMuc] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[DonHang_KH]    Script Date: 12/19/2015 10:41:26 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[DonHang_KH](
	[MaDonHang] [int] IDENTITY(1,1) NOT NULL,
	[NgayGiao] [date] NULL,
	[DiaChiGiao] [nvarchar](50) NULL,
	[TinhTrang] [nvarchar](50) NULL,
	[DaThanhToan] [bit] NULL,
	[NgayLap] [date] NULL,
	[TongTien] [decimal](18, 0) NULL,
	[MaKH] [int] NULL,
 CONSTRAINT [PK_DonHang_KH] PRIMARY KEY CLUSTERED 
(
	[MaDonHang] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[DonHang_NCC]    Script Date: 12/19/2015 10:41:26 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[DonHang_NCC](
	[MaDonHang] [int] IDENTITY(1,1) NOT NULL,
	[MaSach] [int] NOT NULL,
	[NgayGiao] [date] NOT NULL,
	[SoLuong] [int] NOT NULL,
	[TinhTrang] [nvarchar](20) NULL,
	[TongTien] [decimal](18, 0) NULL,
	[MaHopDong] [int] NULL,
 CONSTRAINT [PK_DonHang_NCC] PRIMARY KEY CLUSTERED 
(
	[MaDonHang] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[HopDong]    Script Date: 12/19/2015 10:41:26 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[HopDong](
	[MaHopDong] [int] IDENTITY(1,1) NOT NULL,
	[NgayBatDau] [date] NULL,
	[NgayKetThuc] [date] NULL,
	[TinhTrang] [nchar](10) NULL,
	[MaNCC] [int] NULL,
 CONSTRAINT [PK_HopDong] PRIMARY KEY CLUSTERED 
(
	[MaHopDong] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[KhachHang]    Script Date: 12/19/2015 10:41:26 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[KhachHang](
	[MaKH] [int] IDENTITY(1,1) NOT NULL,
	[HoTen] [nvarchar](30) NULL,
	[NgSinh] [date] NULL,
	[GioiTinh] [nvarchar](5) NULL,
	[DiaChi] [nvarchar](50) NULL,
	[SDT] [nvarchar](15) NULL,
	[Email] [nvarchar](30) NULL,
 CONSTRAINT [PK_KhachHang] PRIMARY KEY CLUSTERED 
(
	[MaKH] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[KhaNangCungCap]    Script Date: 12/19/2015 10:41:26 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[KhaNangCungCap](
	[MaNCC] [int] NOT NULL,
	[MaSach] [int] NOT NULL,
	[DonGia] [decimal](18, 0) NULL,
	[Soluong] [int] NULL,
 CONSTRAINT [PK_KhaNangCungCap] PRIMARY KEY CLUSTERED 
(
	[MaNCC] ASC,
	[MaSach] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[NhaCungCap]    Script Date: 12/19/2015 10:41:26 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[NhaCungCap](
	[MaNCC] [int] IDENTITY(1,1) NOT NULL,
	[TenNCC] [nvarchar](30) NULL,
	[DiaChi] [nvarchar](50) NULL,
	[SDT] [nvarchar](15) NULL,
	[TaiKhoan] [nvarchar](40) NULL,
	[Email] [nvarchar](30) NULL,
 CONSTRAINT [PK_NhaCungCap] PRIMARY KEY CLUSTERED 
(
	[MaNCC] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[Sach]    Script Date: 12/19/2015 10:41:26 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Sach](
	[MaSach] [int] IDENTITY(1,1) NOT NULL,
	[TenSach] [nvarchar](100) NULL,
	[MoTa] [nvarchar](max) NULL,
	[DanhMuc] [int] NULL,
	[Gia] [decimal](18, 0) NULL,
	[SoLuongTon] [int] NULL,
	[NamSanXuat] [int] NULL,
	[NhaXuatBan] [nvarchar](30) NULL,
	[TacGia] [nvarchar](100) NULL,
	[AnhBia] [nvarchar](50) NULL,
	[SoLuongCan] [int] NULL,
	[TinhTrang] [bit] NULL,
	[SoLuongTonToiThieu] [int] NULL,
	[ThoiHan] [int] NULL,
 CONSTRAINT [PK_Sach] PRIMARY KEY CLUSTERED 
(
	[MaSach] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
/****** Object:  Table [dbo].[TaiKhoan]    Script Date: 12/19/2015 10:41:26 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[TaiKhoan](
	[MaTK] [int] IDENTITY(1,1) NOT NULL,
	[TenDangNhap] [nvarchar](40) NULL,
	[MatKhau] [nvarchar](40) NULL,
	[TrangThai] [bit] NULL,
	[Quyen] [nvarchar](30) NULL,
	[MaNguoiDung] [int] NULL,
 CONSTRAINT [PK_TaiKhoan] PRIMARY KEY CLUSTERED 
(
	[MaTK] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[Token]    Script Date: 12/19/2015 10:41:26 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Token](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[Consumer_key] [nvarchar](20) NULL,
	[Request_token] [nvarchar](50) NULL,
	[Veritify_token] [nvarchar](50) NULL,
	[Access_token] [nvarchar](50) NULL,
	[MaNCC] [int] NULL,
 CONSTRAINT [PK_Token] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
INSERT [dbo].[ChiTietHopDong] ([MaHopDong], [MaSach], [DonGia]) VALUES (1, 1, CAST(50000 AS Decimal(18, 0)))
INSERT [dbo].[ChiTietHopDong] ([MaHopDong], [MaSach], [DonGia]) VALUES (1, 9, CAST(180000 AS Decimal(18, 0)))
INSERT [dbo].[ChiTietHopDong] ([MaHopDong], [MaSach], [DonGia]) VALUES (2, 1, CAST(50000 AS Decimal(18, 0)))
INSERT [dbo].[ChiTietHopDong] ([MaHopDong], [MaSach], [DonGia]) VALUES (2, 9, CAST(180000 AS Decimal(18, 0)))
INSERT [dbo].[ChiTietHopDong] ([MaHopDong], [MaSach], [DonGia]) VALUES (2, 14, CAST(40000 AS Decimal(18, 0)))
INSERT [dbo].[CTDH_KH] ([MaDonHang], [MaSach], [SoLuong], [DonGia], [ThanhTien]) VALUES (1, 1, 1, CAST(65000 AS Decimal(18, 0)), CAST(65000 AS Decimal(18, 0)))
INSERT [dbo].[CTDH_KH] ([MaDonHang], [MaSach], [SoLuong], [DonGia], [ThanhTien]) VALUES (1, 2, 1, CAST(48000 AS Decimal(18, 0)), CAST(48000 AS Decimal(18, 0)))
INSERT [dbo].[CTDH_KH] ([MaDonHang], [MaSach], [SoLuong], [DonGia], [ThanhTien]) VALUES (2, 15, 4, CAST(24000 AS Decimal(18, 0)), CAST(96000 AS Decimal(18, 0)))
INSERT [dbo].[CTDH_KH] ([MaDonHang], [MaSach], [SoLuong], [DonGia], [ThanhTien]) VALUES (3, 11, 2, CAST(245000 AS Decimal(18, 0)), CAST(490000 AS Decimal(18, 0)))
SET IDENTITY_INSERT [dbo].[DanhMuc] ON 

INSERT [dbo].[DanhMuc] ([MaDanhMuc], [TenDanhMuc]) VALUES (1, N'Kỹ thuật')
INSERT [dbo].[DanhMuc] ([MaDanhMuc], [TenDanhMuc]) VALUES (2, N'Giáo dục')
INSERT [dbo].[DanhMuc] ([MaDanhMuc], [TenDanhMuc]) VALUES (3, N'Y dược')
INSERT [dbo].[DanhMuc] ([MaDanhMuc], [TenDanhMuc]) VALUES (4, N'Văn học - Xã hội')
INSERT [dbo].[DanhMuc] ([MaDanhMuc], [TenDanhMuc]) VALUES (5, N'Khoa học - Tự nhiên')
INSERT [dbo].[DanhMuc] ([MaDanhMuc], [TenDanhMuc]) VALUES (6, N'Pháp luật')
INSERT [dbo].[DanhMuc] ([MaDanhMuc], [TenDanhMuc]) VALUES (7, N'Báo chí')
INSERT [dbo].[DanhMuc] ([MaDanhMuc], [TenDanhMuc]) VALUES (8, N'Chính trị - Triết học')
INSERT [dbo].[DanhMuc] ([MaDanhMuc], [TenDanhMuc]) VALUES (9, N'Công nghệ thông tin')
INSERT [dbo].[DanhMuc] ([MaDanhMuc], [TenDanhMuc]) VALUES (10, N'Lịch sử - Địa lý')
INSERT [dbo].[DanhMuc] ([MaDanhMuc], [TenDanhMuc]) VALUES (11, N'Mỹ thuật - Kiến trúc - Nhiếp ảnh')
INSERT [dbo].[DanhMuc] ([MaDanhMuc], [TenDanhMuc]) VALUES (12, N'Tâm lý - Thần học')
INSERT [dbo].[DanhMuc] ([MaDanhMuc], [TenDanhMuc]) VALUES (13, N'Tạp chí')
INSERT [dbo].[DanhMuc] ([MaDanhMuc], [TenDanhMuc]) VALUES (14, N'Tiếng anh')
INSERT [dbo].[DanhMuc] ([MaDanhMuc], [TenDanhMuc]) VALUES (15, N'Truyện thiếu nhi')
INSERT [dbo].[DanhMuc] ([MaDanhMuc], [TenDanhMuc]) VALUES (16, N'Truyện tranh, Manga, Comic')
INSERT [dbo].[DanhMuc] ([MaDanhMuc], [TenDanhMuc]) VALUES (17, N'Sách giáo khoa')
INSERT [dbo].[DanhMuc] ([MaDanhMuc], [TenDanhMuc]) VALUES (18, N'Sách tham khảo')
INSERT [dbo].[DanhMuc] ([MaDanhMuc], [TenDanhMuc]) VALUES (19, N'Truyện cho tuổi mới lớn')
INSERT [dbo].[DanhMuc] ([MaDanhMuc], [TenDanhMuc]) VALUES (20, N'Kỹ năng sống')
SET IDENTITY_INSERT [dbo].[DanhMuc] OFF
SET IDENTITY_INSERT [dbo].[DonHang_KH] ON 

INSERT [dbo].[DonHang_KH] ([MaDonHang], [NgayGiao], [DiaChiGiao], [TinhTrang], [DaThanhToan], [NgayLap], [TongTien], [MaKH]) VALUES (1, CAST(N'2015-09-12' AS Date), N'10 Tran Hung Dao, Q1, HCM', N'Delete', 1, CAST(N'2015-09-08' AS Date), CAST(113000 AS Decimal(18, 0)), 1)
INSERT [dbo].[DonHang_KH] ([MaDonHang], [NgayGiao], [DiaChiGiao], [TinhTrang], [DaThanhToan], [NgayLap], [TongTien], [MaKH]) VALUES (2, CAST(N'2015-09-13' AS Date), N'100 Hung Vuong, Q5, HCM', NULL, 0, CAST(N'2015-09-10' AS Date), CAST(96000 AS Decimal(18, 0)), 2)
INSERT [dbo].[DonHang_KH] ([MaDonHang], [NgayGiao], [DiaChiGiao], [TinhTrang], [DaThanhToan], [NgayLap], [TongTien], [MaKH]) VALUES (3, CAST(N'2015-09-14' AS Date), N'10 Tran Hung Dao, Q1, HCM', N'Start', 0, CAST(N'2015-09-08' AS Date), CAST(490000 AS Decimal(18, 0)), 1)
SET IDENTITY_INSERT [dbo].[DonHang_KH] OFF
SET IDENTITY_INSERT [dbo].[DonHang_NCC] ON 

INSERT [dbo].[DonHang_NCC] ([MaDonHang], [MaSach], [NgayGiao], [SoLuong], [TinhTrang], [TongTien], [MaHopDong]) VALUES (1, 1, CAST(N'2015-04-20' AS Date), 100, N'Start', CAST(5000000 AS Decimal(18, 0)), 2)
INSERT [dbo].[DonHang_NCC] ([MaDonHang], [MaSach], [NgayGiao], [SoLuong], [TinhTrang], [TongTien], [MaHopDong]) VALUES (2, 14, CAST(N'2015-04-20' AS Date), 100, N'Start', CAST(4000000 AS Decimal(18, 0)), 2)
INSERT [dbo].[DonHang_NCC] ([MaDonHang], [MaSach], [NgayGiao], [SoLuong], [TinhTrang], [TongTien], [MaHopDong]) VALUES (3, 9, CAST(N'2015-06-12' AS Date), 50, N'Start', CAST(9000000 AS Decimal(18, 0)), 2)
INSERT [dbo].[DonHang_NCC] ([MaDonHang], [MaSach], [NgayGiao], [SoLuong], [TinhTrang], [TongTien], [MaHopDong]) VALUES (4, 1, CAST(N'2015-07-10' AS Date), 50, N'Start', CAST(2500000 AS Decimal(18, 0)), 2)
SET IDENTITY_INSERT [dbo].[DonHang_NCC] OFF
SET IDENTITY_INSERT [dbo].[HopDong] ON 

INSERT [dbo].[HopDong] ([MaHopDong], [NgayBatDau], [NgayKetThuc], [TinhTrang], [MaNCC]) VALUES (1, CAST(N'2015-01-01' AS Date), CAST(N'2015-06-01' AS Date), N'0         ', 1)
INSERT [dbo].[HopDong] ([MaHopDong], [NgayBatDau], [NgayKetThuc], [TinhTrang], [MaNCC]) VALUES (2, CAST(N'2015-06-02' AS Date), CAST(N'2015-12-02' AS Date), N'1         ', 1)
SET IDENTITY_INSERT [dbo].[HopDong] OFF
SET IDENTITY_INSERT [dbo].[KhachHang] ON 

INSERT [dbo].[KhachHang] ([MaKH], [HoTen], [NgSinh], [GioiTinh], [DiaChi], [SDT], [Email]) VALUES (1, N'Nguyễn Văn An', CAST(N'1985-03-15' AS Date), N'Nam', N'An Duong Vuong, Q5, HCM', N'01688567123', N'vanan@gmail.com')
INSERT [dbo].[KhachHang] ([MaKH], [HoTen], [NgSinh], [GioiTinh], [DiaChi], [SDT], [Email]) VALUES (2, N'Nguyễn Thu Hương', CAST(N'1989-04-12' AS Date), N'Nữ', N'Tran Hung Dao, Q1, HCM', N'0986345123', N'thuhuong@gmail.com')
SET IDENTITY_INSERT [dbo].[KhachHang] OFF
INSERT [dbo].[KhaNangCungCap] ([MaNCC], [MaSach], [DonGia], [Soluong]) VALUES (1, 1, CAST(50000 AS Decimal(18, 0)), 500)
INSERT [dbo].[KhaNangCungCap] ([MaNCC], [MaSach], [DonGia], [Soluong]) VALUES (1, 2, CAST(35000 AS Decimal(18, 0)), 500)
INSERT [dbo].[KhaNangCungCap] ([MaNCC], [MaSach], [DonGia], [Soluong]) VALUES (1, 9, CAST(180000 AS Decimal(18, 0)), 400)
INSERT [dbo].[KhaNangCungCap] ([MaNCC], [MaSach], [DonGia], [Soluong]) VALUES (1, 14, CAST(40000 AS Decimal(18, 0)), 400)
INSERT [dbo].[KhaNangCungCap] ([MaNCC], [MaSach], [DonGia], [Soluong]) VALUES (2, 2, CAST(34000 AS Decimal(18, 0)), 550)
INSERT [dbo].[KhaNangCungCap] ([MaNCC], [MaSach], [DonGia], [Soluong]) VALUES (2, 3, CAST(50000 AS Decimal(18, 0)), 300)
INSERT [dbo].[KhaNangCungCap] ([MaNCC], [MaSach], [DonGia], [Soluong]) VALUES (2, 4, CAST(65000 AS Decimal(18, 0)), 450)
SET IDENTITY_INSERT [dbo].[NhaCungCap] ON 

INSERT [dbo].[NhaCungCap] ([MaNCC], [TenNCC], [DiaChi], [SDT], [TaiKhoan], [Email]) VALUES (1, N'Phát Đạt', N'Ly Thuong Kiet, Q10, HCM', N'01668889998', NULL, N'phatdatbook@gmail.com')
INSERT [dbo].[NhaCungCap] ([MaNCC], [TenNCC], [DiaChi], [SDT], [TaiKhoan], [Email]) VALUES (2, N'Hoàng Long', N'Nguyễn Trãi, Quận 1, HCM', N'01688111222', NULL, N'hoanglong@gmail.com')
SET IDENTITY_INSERT [dbo].[NhaCungCap] OFF
SET IDENTITY_INSERT [dbo].[Sach] ON 

INSERT [dbo].[Sach] ([MaSach], [TenSach], [MoTa], [DanhMuc], [Gia], [SoLuongTon], [NamSanXuat], [NhaXuatBan], [TacGia], [AnhBia], [SoLuongCan], [TinhTrang], [SoLuongTonToiThieu], [ThoiHan]) VALUES (1, N'Tôi Thấy Hoa Vàng Trên Cỏ Xanh', N'“Tôi thấy hoa vàng trên cỏ xanh” truyện dài mới nhất của nhà văn vừa nhận giải văn chương ASEAN Nguyễn Nhật Ánh - đã được Nhà xuất bản Trẻ mua tác quyền và giới thiệu đến độc giả cả nước.', 4, CAST(65000 AS Decimal(18, 0)), 100, 2015, N'Kim Đồng', N'Nguyễn Nhật ÁNh', N'1.jpg', 100, 0, 5, 1)
INSERT [dbo].[Sach] ([MaSach], [TenSach], [MoTa], [DanhMuc], [Gia], [SoLuongTon], [NamSanXuat], [NhaXuatBan], [TacGia], [AnhBia], [SoLuongCan], [TinhTrang], [SoLuongTonToiThieu], [ThoiHan]) VALUES (2, N'Hiệu Ứng Cánh Bướm', N'Ai mới chính xác là người đã cứu hai tỷ người? Có phải là một người cụ thể nào đó mà chúng ta có thể chỉ đích danh? Chúng ta có thể trở lại quá khứ xa đến mức nào? Chúng ta cần kiểm chứng bao nhiêu người để xác định được người thực sự đã cứu được hai tỷ người.... một con số vẫn tiếp tục tăng lên mỗi phút ?', 5, CAST(48000 AS Decimal(18, 0)), 50, 2011, N'Tuổi Trẻ', N'Andy Andrew', N'2.jpg', 100, 1, 5, 1)
INSERT [dbo].[Sach] ([MaSach], [TenSach], [MoTa], [DanhMuc], [Gia], [SoLuongTon], [NamSanXuat], [NhaXuatBan], [TacGia], [AnhBia], [SoLuongCan], [TinhTrang], [SoLuongTonToiThieu], [ThoiHan]) VALUES (3, N'Năng Lượng Kim Tự Tháp', N'Những Kim Tự Tháp Ai Cập vẫn luôn là một nghi vấn lớn đối với nhân loại. Rất rất nhiều những câu hỏi đã được đặt ra, chẳng hạn: Ai đã xây dựng các Kim Tự Tháp? Họ xây dựng Kim Tự Tháp bằng cách nào? Và để làm gì? Nhưng không ai có thể trả lời chúng. Nói đúng hơn, không nhà khoa học nào có thể trả lời.', 5, CAST(56000 AS Decimal(18, 0)), 150, 2010, N'Tuổi Trẻ', N'Nhiều tác giả', N'3.jpg', 100, 1, 5, 1)
INSERT [dbo].[Sach] ([MaSach], [TenSach], [MoTa], [DanhMuc], [Gia], [SoLuongTon], [NamSanXuat], [NhaXuatBan], [TacGia], [AnhBia], [SoLuongCan], [TinhTrang], [SoLuongTonToiThieu], [ThoiHan]) VALUES (4, N'Kỹ Thuật Cắt May Thiết Kế', N'Nhiều người thường quan niệm, trang phục nam có thiết kế đơn giản và kĩ thuật may ráp dễ dàng hơn so với trang phục nữ. Nhưng sự thực lại khác hẳn. Để thiết kế, cắt may được một bộ trang phục nam đẹp, chuẩn, vừa vặn, phù hợp với cơ thể, hoàn toàn không phải là việc dễ dàng. Đặc biệt là với những trang phục mang tính chính thức, trang trọng như quần Âu, áo sơ mi, comple... các yêu cầu về thiết kế, cắt may thường rất cao, độ tỉ mỉ không hề kém cạnh so với trang phục nữ.', 1, CAST(71000 AS Decimal(18, 0)), 35, 2008, N'Kim Đồng', N'Ngọc Hà', N'4.jpg', 100, 0, 5, 1)
INSERT [dbo].[Sach] ([MaSach], [TenSach], [MoTa], [DanhMuc], [Gia], [SoLuongTon], [NamSanXuat], [NhaXuatBan], [TacGia], [AnhBia], [SoLuongCan], [TinhTrang], [SoLuongTonToiThieu], [ThoiHan]) VALUES (5, N'Thao Tác Nhanh Trên Tổ Hợp Phím Tắt', N'Quyển sách giới thiệu vô số cách dùng phím tắt trên Windows, Microsoft Office, và cả khi dùng các chương trình Media.', 1, CAST(26000 AS Decimal(18, 0)), 12, 2007, N'Tuổi Trẻ', N'IT Club', N'5.jpg', 100, 1, 5, 1)
INSERT [dbo].[Sach] ([MaSach], [TenSach], [MoTa], [DanhMuc], [Gia], [SoLuongTon], [NamSanXuat], [NhaXuatBan], [TacGia], [AnhBia], [SoLuongCan], [TinhTrang], [SoLuongTonToiThieu], [ThoiHan]) VALUES (6, N'Hướng Dẫn Thiết Kế Website', N'Thiết kế web là một trong những lĩnh vực dễ dàng có sự chia sẻ, tìm kiếm thông tin. Bạn có thể dễ dàng thiết kế được web nhờ vào việc tìm hiểu các thông tin trên Internet hoặc đọc sách hướng dẫn. Cuốn sách Hướng Dẫn Thiết Kế Website do NXB Văn Hóa Thông Tin ấn hành là một cuốn sách có thể giúp bạn làm được điều đó.', 1, CAST(29000 AS Decimal(18, 0)), 56, 2005, N'Tuổi Trẻ', N'Water PC', N'6.jpg', 100, 1, 5, 1)
INSERT [dbo].[Sach] ([MaSach], [TenSach], [MoTa], [DanhMuc], [Gia], [SoLuongTon], [NamSanXuat], [NhaXuatBan], [TacGia], [AnhBia], [SoLuongCan], [TinhTrang], [SoLuongTonToiThieu], [ThoiHan]) VALUES (9, N'Xây Dựng Đôi Ngũ Nhà Giáo', N'Dạy học là một trong những nghề khó khăn nhất nhưng cũng tốt đẹp nhất mà bạn có thể chọn, nhưng chỉ khi bạn thực sự dành trái tim mình cho nó. Bằng tất cả tâm huyết của mình, hai giáo sư Trường Đại học Bang Kennesaw - David Jerner Martin và Kimberly S. Loomis - đã cho ra đời cuốn sách Xây dựng đội ngũ nhà giáo với mục đích khích lệ những ai có mong muốn trở thành giáo viên tự xây dựng những hiểu biết căn bản nhất về công việc của một người thầy, đồng thời cung cấp kiến thức toàn diện nhất về Giáo dục học.', 2, CAST(196000 AS Decimal(18, 0)), 101, 2006, N'Tuổi Trẻ', N'David Jerner Martin', N'9.png', 100, 0, 5, 1)
INSERT [dbo].[Sach] ([MaSach], [TenSach], [MoTa], [DanhMuc], [Gia], [SoLuongTon], [NamSanXuat], [NhaXuatBan], [TacGia], [AnhBia], [SoLuongCan], [TinhTrang], [SoLuongTonToiThieu], [ThoiHan]) VALUES (10, N'Kỹ Nắng Làm Bài Thi Đại Học', N'Quyển sách này sẽ xác định và đề cập đến các lĩnh vực mà hầu hết sinh viên đều cần hỗ trợ khi chuẩn bị cho việc thi cử và bước vào kỳ thi, đồng thời cung cấp các giải pháp cũng như lời khuyên thiết thực và dễ hiểu mà nhờ đó, bạn có thể đánh giá, cải thiện thành tích của mình, đạt kết quả tốt hơn - và đạt điểm cao hơn.', 2, CAST(110000 AS Decimal(18, 0)), 25, 2003, N'Nhi Đồng', N'Jonathan Weyers', N'10.jpg', 100, 1, 5, 1)
INSERT [dbo].[Sach] ([MaSach], [TenSach], [MoTa], [DanhMuc], [Gia], [SoLuongTon], [NamSanXuat], [NhaXuatBan], [TacGia], [AnhBia], [SoLuongCan], [TinhTrang], [SoLuongTonToiThieu], [ThoiHan]) VALUES (11, N'Nghiệp Vụ Kế Toán Trường Học', NULL, 6, CAST(245000 AS Decimal(18, 0)), 15, 2008, N'Nhi Đồng', N'Quang Minh', N'11.jpg', 100, 1, 5, 1)
INSERT [dbo].[Sach] ([MaSach], [TenSach], [MoTa], [DanhMuc], [Gia], [SoLuongTon], [NamSanXuat], [NhaXuatBan], [TacGia], [AnhBia], [SoLuongCan], [TinhTrang], [SoLuongTonToiThieu], [ThoiHan]) VALUES (12, N'Dược Học Tham Luận', N'Con người sinh ra gốc ở trời mà gần với đất, tức là bẫm năm vận sáu khí của trời đất để sinh năm tạng sáu phủ. Vật tuy cùng người khác loại, nhưng đều gốc từ một khí của trời đất mà sinh. Mỗi vật riêng được một khí thiên lệch, còn người thì được trọn vẹn khí của trời đất vậy.Một khi khí trong thân người chênh lệch thì sinh ra bệnh tật, thì lại mượn món thuốc thiên lệch một khí để điều chỉnh sự nghiêng lệch trong thân ta cho trở về với quân bình thì hết bệnh vậy. Vả chăng mượn âm dương của vật để biến hóa âm dương trong thân người, nên căn cứ vào đó Thần Nông dùng thuốc để chữa bệnh', 3, CAST(49000 AS Decimal(18, 0)), 25, 2010, N'Nhi Đồng', N'Chơn Nguyên', N'12.jpg', 100, 1, 5, 1)
INSERT [dbo].[Sach] ([MaSach], [TenSach], [MoTa], [DanhMuc], [Gia], [SoLuongTon], [NamSanXuat], [NhaXuatBan], [TacGia], [AnhBia], [SoLuongCan], [TinhTrang], [SoLuongTonToiThieu], [ThoiHan]) VALUES (13, N'Giải Pháp Xua Tan Mệt Mỏi', N'Nhiều phụ nữ cảm thấy mình ngày càng già đi và trở nên ít năng lượng hơn. Họ không ngừng tăng cân, rụng tóc, khô da và luôn trong tình trạng mệt mỏi không thể giải thích được. Ngay cả những chuyên gia về sức khỏe cũng không giải quyết hết các vấn đề. Rõ ràng, đây là các dấu hiệu liên quan đến quá trình lão hóa mà không ai có thể tránh.', 3, CAST(48000 AS Decimal(18, 0)), 44, 2008, N'Nhi Đồng', N'Evawyner.M.D', N'13.jpg', 100, 1, 5, 1)
INSERT [dbo].[Sach] ([MaSach], [TenSach], [MoTa], [DanhMuc], [Gia], [SoLuongTon], [NamSanXuat], [NhaXuatBan], [TacGia], [AnhBia], [SoLuongCan], [TinhTrang], [SoLuongTonToiThieu], [ThoiHan]) VALUES (14, N'Tin Học Cho Người Tự Học', N'Cuốn sách đáp ứng nhu cầu của người sử dụng máy tính, với mục đích giúp người học có thể sử dụng thành thạo nhiều phần mềm ứng dụng khác nhau.', 9, CAST(50000 AS Decimal(18, 0)), 30, 2005, N'Tuổi Trẻ', N'Hà Thanh-Việt Trì', N'14.jpg', 100, 0, 5, 1)
INSERT [dbo].[Sach] ([MaSach], [TenSach], [MoTa], [DanhMuc], [Gia], [SoLuongTon], [NamSanXuat], [NhaXuatBan], [TacGia], [AnhBia], [SoLuongCan], [TinhTrang], [SoLuongTonToiThieu], [ThoiHan]) VALUES (15, N'Văn Học Teen', N'Văn Học Teen - Quý Cô Horoscope là tuyển tập truyện ngắn với những cảm xúc tinh tế, nhẹ nhàng, rất hợp với tuổi teen.', 4, CAST(24000 AS Decimal(18, 0)), 45, 2005, N'Nhi Đồng', N'Nhiều tác giả', N'15.jpg', 100, 1, 5, 1)
INSERT [dbo].[Sach] ([MaSach], [TenSach], [MoTa], [DanhMuc], [Gia], [SoLuongTon], [NamSanXuat], [NhaXuatBan], [TacGia], [AnhBia], [SoLuongCan], [TinhTrang], [SoLuongTonToiThieu], [ThoiHan]) VALUES (16, N'Kẻ Trăn Trở', N'Kẻ Trăn Trở là cuốn sách được tập hợp từ rất nhiều bài viết đã được đăng trên các trang báo điện tử: VNExpress, Thanh niên, Tuổi trẻ, Giáo dục, báo Điện tử Chính phủ, VTC News…', 7, CAST(74000 AS Decimal(18, 0)), 30, 2006, N'Tuổi Trẻ', N'Lương Hoài Nam', N'16.jpg', 100, 1, 5, 1)
INSERT [dbo].[Sach] ([MaSach], [TenSach], [MoTa], [DanhMuc], [Gia], [SoLuongTon], [NamSanXuat], [NhaXuatBan], [TacGia], [AnhBia], [SoLuongCan], [TinhTrang], [SoLuongTonToiThieu], [ThoiHan]) VALUES (17, N'Hơn Cả Tin Tức', N'Cuốn sách là một nghiên cứu độc đáo, đôi khi có tính phê phán về báo chí đương đại, cả trên mạng lẫn ngoài mạng internet, và tìm thấy niềm hứng khởi cho một sự thấu hiểu hiệu quả và đầy khát vọng về nghề báo trong những ví dụ từ các bài báo và blog thế kỷ 21, đồng thời trong những hiểu biết chọn lọc về nghề báo thế kỷ 20 và các tác phẩm viết lách của Benjamin Franklin thế kỷ 18. Hầu hết nỗ lực xử lý cuộc khủng hoảng báo chí hiện nay nhấn mạnh vào công nghệ. Stephens nhấn mạnh vào tư duy và nhu cầu tư duy lại về báo chí đã từng là và có thể trở thành công việc gì.', 7, CAST(86000 AS Decimal(18, 0)), 15, 2008, N'Tuổi Trẻ', N'MitChell Stephens', N'17.jpg', 50, 1, 5, 1)
INSERT [dbo].[Sach] ([MaSach], [TenSach], [MoTa], [DanhMuc], [Gia], [SoLuongTon], [NamSanXuat], [NhaXuatBan], [TacGia], [AnhBia], [SoLuongCan], [TinhTrang], [SoLuongTonToiThieu], [ThoiHan]) VALUES (18, N'Ba Nhà Báo Sài Gòn', N'BA NHÀ BÁO SÀI GÒN là cuốn sách kể sơ lược về cuộc đời và hoạt động báo chí của các nhà báo Dương Tử Giang (Nguyễn Tấn Sĩ), Trần Tấn Quốc (Trần Chí Thanh) và bà Bút Trà Nguyên Đức Nhuận (Tô Thị Thân).', 7, CAST(62000 AS Decimal(18, 0)), 30, 2009, N'Kim Đồng', N'Trần Nhật Vy', N'18.jpg', 100, 1, 5, 1)
INSERT [dbo].[Sach] ([MaSach], [TenSach], [MoTa], [DanhMuc], [Gia], [SoLuongTon], [NamSanXuat], [NhaXuatBan], [TacGia], [AnhBia], [SoLuongCan], [TinhTrang], [SoLuongTonToiThieu], [ThoiHan]) VALUES (19, N'Việt Nam Sử Lược', N'Sử là sách không những chỉ để ghi chép những công việc đã qua mà thôi, nhưng lại phải suy xét việc gốc ngọn, tìm tòi cái căn nguyên những công việc của người ta đã làm để hiểu cho rõ những vận hội trị loạn của một nước, những trình độ tiến hóa của một dân tộc.', 10, CAST(74000 AS Decimal(18, 0)), 20, 2008, N'Tuổi Trẻ', N'Trần Trọng Kim', N'19.jpg', 80, 1, 5, 1)
INSERT [dbo].[Sach] ([MaSach], [TenSach], [MoTa], [DanhMuc], [Gia], [SoLuongTon], [NamSanXuat], [NhaXuatBan], [TacGia], [AnhBia], [SoLuongCan], [TinhTrang], [SoLuongTonToiThieu], [ThoiHan]) VALUES (20, N'Giản Yếu Sử Việt Nam', N'Ngày nay ở nước ta có một thực tế đáng buồn là nhiều người biết rất ít về lịch sử đất nước và không yêu sử. Họ không chỉ là những anh chị em phải ngày đêm lao động vật vả kiếm sống, ít có điều kiện để đọc, tìm hiểu và nghiên cứu, mà ngày cả nhiều thanh niên trí thức, học sinh cũng hiểu rất lơ mơ về lịch sử dân tộc. Kết quả qua những bài thi môn lịch sử trong các kì tuyển sinh Đại học mấy năm gần đây là những minh chứng đầy đủ về sự thực đau lòng đó. Tập sách Giản Yếu Sử Việt Nam được biên soạn để giới thiệu, cung cấp thêm tư liệu, giúp hiểu sâu hơn về lịch sử nước nhà.', 10, CAST(126000 AS Decimal(18, 0)), 50, 2005, N'Kim Đồng', N'Đặng Duy Phúc', N'20.jpg', 70, 1, 5, 1)
INSERT [dbo].[Sach] ([MaSach], [TenSach], [MoTa], [DanhMuc], [Gia], [SoLuongTon], [NamSanXuat], [NhaXuatBan], [TacGia], [AnhBia], [SoLuongCan], [TinhTrang], [SoLuongTonToiThieu], [ThoiHan]) VALUES (21, N'TÌm Hiểu Về Trái Đất', N'Địa lý học là một bộ môn khoa học nghiên cứu về hệ thống từng vùng khí hậu, môi trường nhân loại ở bề mặt vỏ trái đất. Địa lý học vừa là bộ môn khoa học tự nhiên vừa là một bộ môn xã hội. Cho tới nay, Địa lý học vẫn chưa có một hệ thống phân loại được công nhận. Cuốn sách giới thiệu về những nội dung như trái đất với nhiều mê hoặc, nước là nguồn gốc của sinh mạng, không khí là yếu tộ của sự sống, bí mật trong vương quốc sinh vật, trái đất là ngôi nhà chung của nhân loại... với mong muốn phổ cập kiến thức địa lý cho mọi người, nâng cao sự hiểu biết về trái đất.', 10, CAST(46000 AS Decimal(18, 0)), 60, 2009, N'Tuổi Trẻ', N'Nguyễn Hùng', N'21.jpg', NULL, 1, 5, 1)
INSERT [dbo].[Sach] ([MaSach], [TenSach], [MoTa], [DanhMuc], [Gia], [SoLuongTon], [NamSanXuat], [NhaXuatBan], [TacGia], [AnhBia], [SoLuongCan], [TinhTrang], [SoLuongTonToiThieu], [ThoiHan]) VALUES (22, N'Biển Việt - Đảo Việt', N'Trong sự hiểu biết của mình, tác giả có mong muốn được giới thiệu một số giá trị lịch sử, văn hóa của đất nước quê hương chia sẻ cùng bè bạn.Cuốn sách đầu tiên theo mảng đề tài này giới thiệu về thiên nhiên, lịch sử, văn hóa chiến công của những người trên dải Trường Sơn có tên Ngàn dặm Trường Sơn ra đời năm 1982 được tái bản nhiều lần và được dịch ra tiếng Anh với tựa đề “The Hồ Chí Minh trail” (Đường mòn Hồ Chí Minh) đã tạo thêm cảm hứng để tác giả viết tiếp những tác phẩm sau.', 10, CAST(37000 AS Decimal(18, 0)), 70, 2010, N'Nhi Đồng', N'Vũ Ngọc Khôi', N'22.jpg', NULL, 1, 5, 1)
INSERT [dbo].[Sach] ([MaSach], [TenSach], [MoTa], [DanhMuc], [Gia], [SoLuongTon], [NamSanXuat], [NhaXuatBan], [TacGia], [AnhBia], [SoLuongCan], [TinhTrang], [SoLuongTonToiThieu], [ThoiHan]) VALUES (23, N'Điêu Khắc Trung Quốc', N'Một quốc gia cổ đại sẻ có nền nghệ thuật cổ đại, trong hàng ngàn  năm lịch sử Trung Quốc, nghệ thuật điêu khắc đã thể hiện diện mạo vô cùng phong phú. Do có lịch  sử lâu đời nên các loại hình văn hóa có nhiều thay đổi, sự khác biệt địa lý khu vực rõ ràng, nghệ thuật điêu khắc cổ đại của Trung Quốc không thể tóm lượt khái quát bởi một vài loại hình một cách đơn giản. Trong nghệ thuật điêu khắc, khái niệm xác lập phong cách Trung Quốc hoàn toàn không phải đơn thuần là "xây móng thì xong nhà" hay là "đã hoàn thiện thì không thay đổi", mà đó là sự phát triển và biến hóa liên tục không ngừng cùng với dòng chảy của thời đại và quá trình vận hành phát triển của nền văn hóa. Nghệ thuật điêu khắc Trung Quốc đã trải qua hàng ngàn năm lịch sử phát triển, trong quá trình thay vua đổi chúa giữa các triều đại và thay đổi của nền văn hóa, đã từng bước hình thành phong cách độc đáo và thủ pháp tạo hình điêu khắc của riêng mình.', 11, CAST(63000 AS Decimal(18, 0)), 40, 2005, N'Thời Đại', N'Triệu Văn Binh', N'23.jpg', NULL, 1, 5, 1)
INSERT [dbo].[Sach] ([MaSach], [TenSach], [MoTa], [DanhMuc], [Gia], [SoLuongTon], [NamSanXuat], [NhaXuatBan], [TacGia], [AnhBia], [SoLuongCan], [TinhTrang], [SoLuongTonToiThieu], [ThoiHan]) VALUES (24, N'Tháp Cổ Việt Nam', N'Từ “tháp” là dạng Hán Việt của từ Ấn Độ cổ stupa trong nhiều ngôn ngữ Ấn Độ và khi nhập vào nhiều tiếng nói khác, trước sau chỉ một loại mộ chôn người cao quý hay chôn xá lị Phật, táng trên đồi gò, hoặc xây đắp thành đồi gò, thành công trình kiến trúc cao. Từ tháp, vào tiếng ta, trước cũng chỉ loại mộ có kiến trúc cao như vậy, sau được dùng rộng ra, không chỉ riêng kiến trúc mộ cao nữa, mà còn chỉ kiến trúc cao khác mang tính linh thiêng như mộ: kiến trúc cao nơi ở của thần, kiến trúc cao nơi thể hiện Phật và thờ Phật. Lại cũng được gọi là tháp, kiếm trúc cao khác không được gọi là mộ mà cũng không mang tính tín ngưỡng, tôn giáo như mộ: tháp canh, kiến trúc cao quân sự; tháp nước, kiến trúc cao dân dụng… Với nội dung ngữ nghĩa mà nó bao hàm như thế, tháp, trước hết và xem xét chung nhất là một dạng kiến trúc tôn giáo.', 11, CAST(50000 AS Decimal(18, 0)), 35, 2004, N'Dân Trí', N'Nguyễn Duy Hinh', N'24.jpg', NULL, 1, 5, 1)
INSERT [dbo].[Sach] ([MaSach], [TenSach], [MoTa], [DanhMuc], [Gia], [SoLuongTon], [NamSanXuat], [NhaXuatBan], [TacGia], [AnhBia], [SoLuongCan], [TinhTrang], [SoLuongTonToiThieu], [ThoiHan]) VALUES (25, N'Nghệ Thuật Trang Điểm', N'Ngày nay, việc làm đẹp trở thành một nhu cầu không thể thiếu không thể thiếu đối với chị em phụ nữ. Cuộc sống càng hiện đại, nhịp sống càng hối hả, bạn càng muốn mình đẹp hơn bằng cách tự mình trang điểm mà không mất nhiều thời gian để đến các trung tâm làm đẹp. Quyển sách này sẽ hướng dẫn cho bạn nghệ thuật trang điểm mới nhất và các kỷ xảo để che lấp khuyết điểm và tôn vinh những ưu điểm trên khuôn mặt của bạn.', 11, CAST(48000 AS Decimal(18, 0)), 20, 2010, N'Phụ Nữ', N'Tuyết Minh', N'25.jpg', NULL, 1, 5, 1)
INSERT [dbo].[Sach] ([MaSach], [TenSach], [MoTa], [DanhMuc], [Gia], [SoLuongTon], [NamSanXuat], [NhaXuatBan], [TacGia], [AnhBia], [SoLuongCan], [TinhTrang], [SoLuongTonToiThieu], [ThoiHan]) VALUES (26, N'Họa Sĩ Và Thiếu Nữ', N'Nhiều bức tranh của hoạ sĩ Nguyễn Phan Chánh gắn liền với những câu chuyện tình yêu. Chính vì vậy, con gái ông - Nguyệt Tú đã viết lại cuốn sách này nhằm giúp người đọc hiểu thêm về nguồn cảm hứng của hoạ sỹ Nguyễn Phan Chánh khi vẽ những bức tranh lụa nổi tiếng còn mãi với thời gian. Nguyễn Phan Chánh đã dùng cả cuộc đời để vẽ người phụ nữ ở lứa tuổi đẹp nhất - lứa tuổi thanh xuân. Những câu chuyện tình trong cuốn sách này  là mảnh đời riêng của hoạ sỹ Nguyễn Phan Chánh mà con gái ông biết được qua những trang nhật ký mà ông để lại, và qua những kỉ niệm không thể nào quên trong quãng đời con gái ông sống với ông.', 11, CAST(18000 AS Decimal(18, 0)), 15, 2011, N'Thanh Niên', N'Nguyệt Tú', N'26.jpg', NULL, 1, 5, 1)
INSERT [dbo].[Sach] ([MaSach], [TenSach], [MoTa], [DanhMuc], [Gia], [SoLuongTon], [NamSanXuat], [NhaXuatBan], [TacGia], [AnhBia], [SoLuongCan], [TinhTrang], [SoLuongTonToiThieu], [ThoiHan]) VALUES (27, N'Về Bản Tính Người', N'Không một người nào thực sự quan tâm đến tương lai nhân lọai mà có thể bỏ qua cuốn sách Về bản tính người của   E. O. Wilson. Hành vi của con người có bị kiểm soát bởi di sản sinh học của giống loài không? Di sản này có khiến cho số phận loài người bị giới hạn không? Bằng trí tuệ sắc sảo và văn phong giản dị, nhà bác học nổi tiếng thế giới, hai lần đoạt giải Pulitzer đặt nghi vấn về nhiều ngộ nhận phổ biến hiện nay về hành vi của loài người, và tìm cách lý giải hành vi của loài người ừ góc độ sinh học, qua đó ông đề nghị chúng ta không xem loài người như một loài hoàn toàn ngoại lệ, cho dẫu con người đã đạt được những thành tựu lớn lao đến đâu và mặc dù có sự thiên vị tự nhiên do bởi bản thân chúng ta thuộc loài người.', 12, CAST(74000 AS Decimal(18, 0)), 20, 2005, N'Lao Động', N'Edward O.Wilson', N'27.jpg', NULL, 1, 5, 1)
INSERT [dbo].[Sach] ([MaSach], [TenSach], [MoTa], [DanhMuc], [Gia], [SoLuongTon], [NamSanXuat], [NhaXuatBan], [TacGia], [AnhBia], [SoLuongCan], [TinhTrang], [SoLuongTonToiThieu], [ThoiHan]) VALUES (28, N'Ngộ Về Chứ Tu', N'"Tu" nguyên nghĩa Hán Việt là "sửa", thí dụ như tu sửa, tu bổ, tu tập... Nếu theo Tự điển Tiếng Việt của Trung Tâm Khoa Học Xã Hội và Nhân Văn Quốc Gia thì định nghĩa của "Tu" là "sống theo những quy định nghiêm ngặt nhằm mục đích sửa mình theo đúng giáo lý của môt tông giáo nào đó". Như vậy hiện thời người ta thường hiểu nghĩa chữ "tu" theo đường hướng tông giáo. Thật sự "tu" mang ý nghĩa khá rộng rãi, một hành vi nhận biết đúng sai để tự sửa mình cho phù hợp với đạo đức của xã hội. Không theo một tông giáo nào, người ta vẫn có thể tu tập để trở thành người hữu ích hơn hoặc tìm cầu một cuộc sống hài hoà thanh thản.', 12, CAST(36000 AS Decimal(18, 0)), 25, 2007, N'Dân Trí', N'Huyền Cơ', N'28.jpg', NULL, 1, 5, 1)
INSERT [dbo].[Sach] ([MaSach], [TenSach], [MoTa], [DanhMuc], [Gia], [SoLuongTon], [NamSanXuat], [NhaXuatBan], [TacGia], [AnhBia], [SoLuongCan], [TinhTrang], [SoLuongTonToiThieu], [ThoiHan]) VALUES (29, N'Đập Vỡ Vỏ Hồ Đào', N'Hạt hồ đào (walnut) ăn rất ngon nhưng vỏ của nó rất cứng. Ở Tây phương người ta chế ra một cái kẹp sắt, chỉ cần bóp mạnh cái kẹp thì vỏ hồ đào vỡ và ta có thể thưởng thức ngay hương vị thơm ngọt và bùi của hồ đào. Có những người trong chúng ta từng bị lúng túng khi đọc những bài kệ Trung Quán Luận. Ở đây, với một phong cách giảng giải bình dị nhưng thâm sâu, thiền sư Nhất Hạnh đã cho chúng ta một cái nhìn rất mới và rất dễ hiểu.', 12, CAST(63000 AS Decimal(18, 0)), 45, 2006, N'Thanh Niên', N'Thích Nhất Hạnh', N'29.jpg', NULL, 1, 5, 1)
INSERT [dbo].[Sach] ([MaSach], [TenSach], [MoTa], [DanhMuc], [Gia], [SoLuongTon], [NamSanXuat], [NhaXuatBan], [TacGia], [AnhBia], [SoLuongCan], [TinhTrang], [SoLuongTonToiThieu], [ThoiHan]) VALUES (30, N'Tỉnh Thức', N'"Con người còn luôn mãi khổ đau một khi chưa tỉnh thức."Không gian nội tâm mênh mông sâu thẳm của con người luôn chứa đựng những điều diệu kỳ chưa được khám phá. Qua nghiên cứu và trải nghiệm của một nhà khoa học, Bác sĩ Prashant Kakode sẽ giúp chúng ta bước vào một cuộc hành trình nội tâm thật thú vị.', 12, CAST(29000 AS Decimal(18, 0)), 20, 2006, N'Lao Động', N'Dr. Prashant Kakode', N'30.jpg', NULL, 1, 5, 1)
INSERT [dbo].[Sach] ([MaSach], [TenSach], [MoTa], [DanhMuc], [Gia], [SoLuongTon], [NamSanXuat], [NhaXuatBan], [TacGia], [AnhBia], [SoLuongCan], [TinhTrang], [SoLuongTonToiThieu], [ThoiHan]) VALUES (31, N'Forbes Việt Nam - Số 30', N'Một trong những tín hiệu quan trọng đánh dấu sự phục hồi của thị trường bất động sản trong bốn quý trở lại đây là dòng tiền quay lại thị trường thông qua việc ghi nhận lượng giao dịch thành công hay tín dụng cho bất động sản tăng. Vấn đề đặt ra là làm sao cho thị trường phát triển ổn định và bền vững. Hiệp định đối tác xuyên Thái Bình Dương mang lại vận hội mới cho các doanh nghiệp có bản lĩnh, có khả năng cạnh tranh và thích ứng nhanh với môi trường kinh doanh đang thay đổi rất nhanh. Và đó cũng là cơ hội cho Forbes Việt Nam cung cấp các câu chuyện kinh doanh đầy hứng khởi trong số báo này.', 13, CAST(45000 AS Decimal(18, 0)), 35, 2008, N'Dân Trí', N'Nhiều Tác Giả', N'31.jpg', NULL, 1, 5, 1)
INSERT [dbo].[Sach] ([MaSach], [TenSach], [MoTa], [DanhMuc], [Gia], [SoLuongTon], [NamSanXuat], [NhaXuatBan], [TacGia], [AnhBia], [SoLuongCan], [TinhTrang], [SoLuongTonToiThieu], [ThoiHan]) VALUES (32, N'Forbes Việt Nam - Số 26', N'Trong số này, Forbes Việt Nam điểm lại quá trình tái cơ cấu của hệ thống ngân hàng Việt Nam, trong đó có danh sách các cuộc "hôn nhân ngân hàng" trong thời gian từ 2011 tới nay. Để nỗ lực tái cơ cấu có kết quả, các ngân hàng còn đứng trước thách thức tạo sự biến đổi về chất: trong đó có việc củng cố việc thực thi các quy định pháp luật và quản lý nhà nước, áp dụng những quy trình quản lý rủi ro, và việc tăng cường sự phong phú của dịch vụ tài chính..', 13, CAST(45000 AS Decimal(18, 0)), 30, 2008, N'Dân Trí', N'Nhiều Tác Giả', N'32.jpg', NULL, 1, 5, 1)
INSERT [dbo].[Sach] ([MaSach], [TenSach], [MoTa], [DanhMuc], [Gia], [SoLuongTon], [NamSanXuat], [NhaXuatBan], [TacGia], [AnhBia], [SoLuongCan], [TinhTrang], [SoLuongTonToiThieu], [ThoiHan]) VALUES (33, N'Áo Trắng - Tập 6.2015', N'Tập san giới thiệu truyện ngắn của các tác giả: Hà Huỳnh Thảo Nhi, Hoàng Thu Thủy, Long Vũ, Trương Thị Thu Phương, Tuyết Tạ,...Những sáng tác xoay quanh chủ đề tình cảm gia đình, tình yêu, kỷ niệm học trò... Đó là mùa hè của cô gái trẻ xách ba lô đi khám phá cuộc đời, là Thương Hoài trong sáng, hồn nhiên và mùa vải trên đồi... Mỗi câu chuyện là một góc nhìn về cuộc sống, là nhớ thương hoài niệm, là những suy ngẫm sâu lắng về con người và cuộc đời.', 13, CAST(26000 AS Decimal(18, 0)), 20, 2015, N'Thanh Niên', N'Nhiều Tác Giả', N'33.jpg', NULL, 1, 5, 1)
INSERT [dbo].[Sach] ([MaSach], [TenSach], [MoTa], [DanhMuc], [Gia], [SoLuongTon], [NamSanXuat], [NhaXuatBan], [TacGia], [AnhBia], [SoLuongCan], [TinhTrang], [SoLuongTonToiThieu], [ThoiHan]) VALUES (34, N'Áo Trắng - Tập 7.2015', N'Tập san giới thiệu truyện ngắn của các tác giả: Chân Thuyên, Nam Giao, Phúc Thịnh, Lê Hoàng Hựu, Nhiên Phượng... Những cơn mưa nơi xứ người. Buồn và da diết cô đơn. Những màn mưa ký ức về một thời đã qua, về tình yêu đã trắng xóa màu nhung nhớ. Và trong vắt đâu đó, cơn mưa tuổi học trò lấp lánh trên nhành phượng vĩ cuối sân trường. Mỗi câu chuyện là một sắc thái cung bậc cảm xúc khác nhau, mưa Huế "ri rỉ, thì thầm như người yêu nói chuyện với nhau", đêm mưa Pleiku buồn rười rượi, cơn mưa rào bất chợt của Sài thành hay cơn mưa nặng hạt trên mảnh đất xứ sở kim chi... Tất cả gọi tên thành nỗi nhớ, niềm yêu và thương mến đến nao lòng!', 13, CAST(26000 AS Decimal(18, 0)), 25, 2015, N'Thanh Niên', N'Nhiều Tác Giả', N'34.jpg', NULL, 1, 5, 1)
INSERT [dbo].[Sach] ([MaSach], [TenSach], [MoTa], [DanhMuc], [Gia], [SoLuongTon], [NamSanXuat], [NhaXuatBan], [TacGia], [AnhBia], [SoLuongCan], [TinhTrang], [SoLuongTonToiThieu], [ThoiHan]) VALUES (35, N'At First Sight', N'There are a few things Jeremy Marsh was sure he''d never do: he''d never leave New York City; never give his heart away again after barely surviving one failed marriage; and, most of all, never become a parent. Now, Jeremy is living in the tiny town of Boone Creek, North Carolina, married to Lexie Darnell, the love of his life, and anticipating the birth of their daughter. But just as his life seems to be settling into a blissful pattern, an unsettling and mysterious message re-opens old wounds and sets off a chain of events that will forever change the course of this young couple''s marriage.', 14, CAST(155000 AS Decimal(18, 0)), 40, 2012, N'Lao Động', N'Nicholas Sparks', N'35.jpg', NULL, 1, 5, 1)
INSERT [dbo].[Sach] ([MaSach], [TenSach], [MoTa], [DanhMuc], [Gia], [SoLuongTon], [NamSanXuat], [NhaXuatBan], [TacGia], [AnhBia], [SoLuongCan], [TinhTrang], [SoLuongTonToiThieu], [ThoiHan]) VALUES (36, N'The Lucky One', N'In his 14th book, bestselling author Nicholas Sparks tells the unforgettable story of a man whose brushes with death lead him to the love of his life. After U.S. Marine Logan Thibault finds a photograph of a smiling young woman buried in the dirt during his tour of duty in Iraq, he experiences a sudden streak of luck - winning poker games and even surviving deadly combat. Only his best friend, Victor, seems to have an explanation for his good fortune: the photograph - his lucky charm. Back home in Colorado, Thibault can''t seem to get the woman in the photograph out of his mind and he sets out on a journey across the country to find her. But Thibault is caught off guard by the strong attraction he feels for the woman he encounters in North Carolina - Elizabeth, a divorced mother - and he keeps the story of the photo, and his luck, a secret. As he and Elizabeth embark upon a passionate love affair, his secret soon threatens to tear them apart - destroying not only their love, but also their lives.', 14, CAST(155000 AS Decimal(18, 0)), 35, 2010, N'Phụ Nữ', N'Nicholas Sparks', N'36.jpg', NULL, 1, 5, 1)
INSERT [dbo].[Sach] ([MaSach], [TenSach], [MoTa], [DanhMuc], [Gia], [SoLuongTon], [NamSanXuat], [NhaXuatBan], [TacGia], [AnhBia], [SoLuongCan], [TinhTrang], [SoLuongTonToiThieu], [ThoiHan]) VALUES (37, N'I Am Pilgrim', N'Can you commit the perfect crime? Pilgrim is the codename for a man who doesn''t exist. The adopted son of a wealthy American family, he once headed up a secret espionage unit for US intelligence. Before he disappeared into anonymous retirement, he wrote the definitive book on forensic criminal investigation. But that book will come back to haunt him. It will help NYPD detective Ben Bradley track him down. And it will take him to a rundown New York hotel room where the body of a woman is found facedown in a bath of acid, her features erased, her teeth missing, her fingerprints gone. It is a textbook murder - and Pilgrim wrote the book. What begins as an unusual and challenging investigation will become a terrifying race-against-time to save America from oblivion. Pilgrim will have to make a journey from a public beheading in Mecca to a deserted ruins on the Turkish coast via a Nazi death camp in Alsace and the barren wilderness of the Hindu Kush in search of the faceless man who would commit an appalling act of mass murder in the name of his God.', 14, CAST(155000 AS Decimal(18, 0)), 20, 2011, N'Lao Động', N'Tea Obreht', N'37.jpg', NULL, 1, 5, 1)
INSERT [dbo].[Sach] ([MaSach], [TenSach], [MoTa], [DanhMuc], [Gia], [SoLuongTon], [NamSanXuat], [NhaXuatBan], [TacGia], [AnhBia], [SoLuongCan], [TinhTrang], [SoLuongTonToiThieu], [ThoiHan]) VALUES (38, N'Message In A Bottle', N'Divorced and disillusioned about relationships, Theresa Osborne is jogging when she finds a bottle on the beach. Inside is a letter of love and longing to "Catherine," signed simply "Garrett." Challenged by the mystery and pulled by emotions she doesn''t fully understand, Theresa begins a search for this man that will change her life. What happens to her is unexpected, perhaps miraculous-an encounter that embraces all our hopes for finding someone special, for having a love that is timeless and everlasting.... Nicholas Sparks exquisitely chronicles the human heart. In his first bestselling novel, The Notebook, he created a testament to romantic love that touched readers around the world. Now in this New York Times bestseller, he renews our faith in destiny, in the ability of lovers to find each other no matter where, no matter when...', 14, CAST(199000 AS Decimal(18, 0)), 10, 2013, N'Dân Trí', N'Nicholas Sparks', N'38.jpg', NULL, 1, 5, 1)
INSERT [dbo].[Sach] ([MaSach], [TenSach], [MoTa], [DanhMuc], [Gia], [SoLuongTon], [NamSanXuat], [NhaXuatBan], [TacGia], [AnhBia], [SoLuongCan], [TinhTrang], [SoLuongTonToiThieu], [ThoiHan]) VALUES (39, N'Cuộc Phưu Lưu Của Cún Polo', N'Đây là một cuốn sách rất đặc biệt - chỉ có tranh và… tranh! Sẽ có rất nhiều hướng tưởng tượng khác nhau qua những khung tranh trong Cuộc phiêu lưu của Cún Polo. Cuốn sách đã được nhiều độc giả nhỏ tuổi trên thế giới cất làm sách gối đầu giường. Mỗi ngày các em tự “đọc” đi “đọc” lại không biết chán. Không thụ động và không bó hẹp, mỗi em nhỏ có một cách riêng để cảm nhận tranh và tình tiết của câu chuyện. Phần đầu sách, tác giả cung cấp một vài gợi ý về cách đọc sách cùng con để cha mẹ tham khảo và tìm ra cách “đọc” hiệu quả giúp việc đọc sách trở nên thú vị và ý nghĩa. Nhưng tuyệt hơn cả là hãy để bé tự khám phá các bức tranh và kể lại câu chuyện theo cách của riêng mình!', 15, CAST(69000 AS Decimal(18, 0)), 50, 2010, N'Nhi Đồng', N'Régis Faller', N'39.jpg', NULL, 1, 5, 1)
INSERT [dbo].[Sach] ([MaSach], [TenSach], [MoTa], [DanhMuc], [Gia], [SoLuongTon], [NamSanXuat], [NhaXuatBan], [TacGia], [AnhBia], [SoLuongCan], [TinhTrang], [SoLuongTonToiThieu], [ThoiHan]) VALUES (40, N'Siêu Thỏ', N'Thỏ con Simon luôn tự nhận mình là Siêu Thỏ: một Siêu Thỏ can đảm, không sợ gì, dù trời có tối đến đâu, đêm có lạnh đến mấy, dằm có đâm vào tay đau điếng thế nào... Nhưng mà này, Siêu Thỏ thì có tè dầm không nhỉ? Siêu Thỏ có nằng nặc không chịu đi học, không thích ngồi bô, không chịu đi ngủ? Siêu Thỏ thì có, suỵt!...thỉnh thoảng lại nói bậy nữa không? Hãy hỏi Simon xem điều kiện để được gia nhập đội quân Siêu Thỏ là gì nhé!', 15, CAST(27000 AS Decimal(18, 0)), 40, 2009, N'Kim Đồng', N'Stephanie Blake', N'40.jpg', NULL, 1, 5, 1)
INSERT [dbo].[Sach] ([MaSach], [TenSach], [MoTa], [DanhMuc], [Gia], [SoLuongTon], [NamSanXuat], [NhaXuatBan], [TacGia], [AnhBia], [SoLuongCan], [TinhTrang], [SoLuongTonToiThieu], [ThoiHan]) VALUES (41, N'Công Chúa Ngọc Trai', N'Câu chuyện kể về nàng công chúa có khả năng điều khiển những viên ngọc trai kì diệu. Khi vừa chào đời, nàng đã bị người bác xấu xa âm mưu ám hại. May mà bà tiên cá tốt bụng Scylla đã mang nàng đến một nơi xa xôi và nuôi nàng khôn lớn, bà đặt tên nàng là Lumina. Rồi trong một lần mang thư mời cho cô Scylla, Lumia vô tình đến Seagundia mà không hề biết rằng nàng đang trở về quê hương. Liệu nàng Lumina xinh đẹp, tốt bụng có tìm được gia đình đích thực của mình? Âm mưu của người bác xấu xa có bị bại lộ? Chúng mình cùng đến vương quốc Seagundia để biết diễn biến câu chuyện thế nào nhé!', 15, CAST(18000 AS Decimal(18, 0)), 60, 2010, N'Nhi Đồng', N'Mary Tillworth', N'41.jpg', NULL, 1, 5, 1)
INSERT [dbo].[Sach] ([MaSach], [TenSach], [MoTa], [DanhMuc], [Gia], [SoLuongTon], [NamSanXuat], [NhaXuatBan], [TacGia], [AnhBia], [SoLuongCan], [TinhTrang], [SoLuongTonToiThieu], [ThoiHan]) VALUES (42, N'Gia Tộc Dối Trá', N'Một gia tộc giàu có và đẹp đẽ: chẳng ai phạm tội, chẳng ai nghiện ngập, chẳng ai thất bại; một hòn đảo riêng ngoài khơi Massachussetts; những kỳ nghỉ hè thơ mộng trong những dinh thự xa hoa, là bức màn huyền thoại, bao phủ lên nhà Sinclair trước mắt người đời. Không ai hay sự rạn nứt gây ra bởi thói gia trưởng và tự đại của người cha, sự tham lam của những người con vô dụng, mối tình đầu bị cấm đoán giữa người thừa kế gia tộc Sinclair "thuần Mỹ" Cadence và cậu bé gốc Ấn Gat... đã dẫn tới nỗi bất mãn và sự nổi loạn của thế hệ thứ ba, vụ tai nạn kinh hoàng trên hòn đảo mộng mơ, chứng quên không thể lý giải của Cadence. Để nhớ lại sự kiện bí ẩn đã làm đảo lộn toàn bộ cuộc sống và các mối quan hệ trong gia đình Sinclair, Cadence phải gạn lọc từng mẩu sự thật từ tầng tầng lớp lớp những lời dối trá. Nhưng sự thật lại là điều cô bé không thể nào ngờ.', 15, CAST(53000 AS Decimal(18, 0)), 55, 2009, N'Kim Đồng', N'Emily Lockhart', N'42.jpg', NULL, 1, 5, 1)
INSERT [dbo].[Sach] ([MaSach], [TenSach], [MoTa], [DanhMuc], [Gia], [SoLuongTon], [NamSanXuat], [NhaXuatBan], [TacGia], [AnhBia], [SoLuongCan], [TinhTrang], [SoLuongTonToiThieu], [ThoiHan]) VALUES (43, N'Ma Vương - Tập 1', N'Ando và Inukai là hai nhân vật đứng ở hai góc nhìn về “mặt trăng” mang tên “Dự án thành phố mới”. Đối với Inukai, dự án này là ngọn nguồn đầu độc sự mục ruỗng của thành phố Nekota, và anh cố làm mọi cách để ngăn cản nó. Nhưng trong quá trình đem cái tốt đẩy lùi cái xấu, Inukai đã không còn nhận ra đâu là đúng, đâu là sai. Anh dần lún sâu vào niềm tin mình có thể thay đổi thế giới, và khi đó, Ando xuất hiện với hai vai trò vô cùng quan trọng: vừa là thuốc thử để Inukai định hướng đúng - sai cho hành động của mình, vừa là phanh hãm cho niềm tin vượt tầm kiểm soát của Inukai. Cuộc chiến ban đầu được xây dựng trên sự đối đầu giữa Ando và Inukai, nhưng thực chất là cuộc chiến trong nội tại của chính Inukai để tìm ra chân lý, khi mà quyền lực và tài năng cùng song hành mà không có chốt hãm. Liệu Inukai có tìm ra lời giải đáp cho tất cả? Hay chính anh sẽ khởi đầu cho một loạt bão lũ kéo về tràn ngập thành phố Nekota?', 16, CAST(16000 AS Decimal(18, 0)), 30, 2009, N'Nhi Đồng', N'Yoko Kamio', N'43.jpg', NULL, 1, 5, 1)
INSERT [dbo].[Sach] ([MaSach], [TenSach], [MoTa], [DanhMuc], [Gia], [SoLuongTon], [NamSanXuat], [NhaXuatBan], [TacGia], [AnhBia], [SoLuongCan], [TinhTrang], [SoLuongTonToiThieu], [ThoiHan]) VALUES (44, N'Ma Vương - Tập 2', N'Ando nhận ra bầu không khí u tối đang bao trùm cuộc sống thường nhật. Hai chữ "cảnh cáo" lóe lên trong đầu. Một tai nạn khó hiểu bất ngờ xảy ra... Và ve một sát thủ xuất hiện. Khắp nơi đều rõ dấu ấn của Inukai. Câu chuyện bây giờ mới bắt đầu.', 16, CAST(16000 AS Decimal(18, 0)), 25, 2009, N'Nhi Đồng', N'Yoko Kamio', N'44.jpg', NULL, 1, 5, 1)
INSERT [dbo].[Sach] ([MaSach], [TenSach], [MoTa], [DanhMuc], [Gia], [SoLuongTon], [NamSanXuat], [NhaXuatBan], [TacGia], [AnhBia], [SoLuongCan], [TinhTrang], [SoLuongTonToiThieu], [ThoiHan]) VALUES (45, N'Ma Vương - Tập 3', N'Viên đạn đã được bắn ra nhắm vào Inukai. Tuy nhiên, nó lại không đến được Inukai. Chiều ngược lại, hắn cũng bắt đầu. Trong dòng người tháo chạy, Ando đứng đó một mình đơn độc. Cái gì đang xảy ra? Điều có thể làm lúc này là gì? Và thời khắc đối đầu giữa hai người đã đến.', 16, CAST(16000 AS Decimal(18, 0)), 30, 2009, N'Nhi Đồng', N'Yoko Kamio', N'45.jpg', NULL, 1, 5, 1)
INSERT [dbo].[Sach] ([MaSach], [TenSach], [MoTa], [DanhMuc], [Gia], [SoLuongTon], [NamSanXuat], [NhaXuatBan], [TacGia], [AnhBia], [SoLuongCan], [TinhTrang], [SoLuongTonToiThieu], [ThoiHan]) VALUES (46, N'Ma Vương - Tập 4', N'Màu đen. Phân hóa thành châu chấu đen. Thời điểm để chiến đấu, là ngay bây giờ. Dưới ánh trăng đêm, Inukai đã nói: "Phải đánh đổ Anderson". Trong khi ánh đèn ủng hộ Inukai đang lan khắp trong thành phố, thì Ando cũng đang đối đầu với một nhân vật vô cùng nguy hiểm. Ve tái xuất giang hồ. Lần này có một kẻ khác truy sát ve. Chính là Kujira. Trong lúc nguy cấp, Ando đã đối diện với thế giới của chính mình.', 16, CAST(16000 AS Decimal(18, 0)), 20, 2009, N'Nhi Đồng', N'Yoko Kamio', N'46.jpg', NULL, 1, 5, 1)
INSERT [dbo].[Sach] ([MaSach], [TenSach], [MoTa], [DanhMuc], [Gia], [SoLuongTon], [NamSanXuat], [NhaXuatBan], [TacGia], [AnhBia], [SoLuongCan], [TinhTrang], [SoLuongTonToiThieu], [ThoiHan]) VALUES (47, N'Bộ Sách Giáo Khoa Lớp 12', N'1. Giải tích2. Hình học3. Vật lý4. Hoá học5. Sinh học6. Công nghệ7. Ngữ văn - tập 18. Ngữ văn - tập 29. GD công dân10. Tin học11. Tiếng anh12. Lịch sử13. Địa lý14. Giáo dục Quốc phòng', 17, CAST(122000 AS Decimal(18, 0)), 50, 2012, N'Dân Trí', N'Nhiều Tác Giả ', N'47.jpg', NULL, 1, 5, 1)
INSERT [dbo].[Sach] ([MaSach], [TenSach], [MoTa], [DanhMuc], [Gia], [SoLuongTon], [NamSanXuat], [NhaXuatBan], [TacGia], [AnhBia], [SoLuongCan], [TinhTrang], [SoLuongTonToiThieu], [ThoiHan]) VALUES (48, N'Bộ Sách Giáo Khoa Lớp 4', N'1. Tiếng Việt - tập 1 2. Tiếng Việt - tập 2 3. Toán 4. Khoa học 5. Lịch sử và Địa lý 6. Nhạc 7. Đạo đức 8. Kĩ thuật 9. Mỹ thuật10. Vở BT đạo đức11. Vở BT tiếng việt (Tập 1)12. Vở BT tiếng việt (Tập 2)13. Vở BT toán (Tập 1)14. Vở BT toán ...', 17, CAST(66000 AS Decimal(18, 0)), 40, 2011, N'Dân Trí', N'Nhiều Tác Giả ', N'48.jpg', NULL, 1, 5, 1)
INSERT [dbo].[Sach] ([MaSach], [TenSach], [MoTa], [DanhMuc], [Gia], [SoLuongTon], [NamSanXuat], [NhaXuatBan], [TacGia], [AnhBia], [SoLuongCan], [TinhTrang], [SoLuongTonToiThieu], [ThoiHan]) VALUES (49, N'Những Bài Văn Kể Chuyện Lớp 3', NULL, 17, CAST(18000 AS Decimal(18, 0)), 35, 2010, N'Nhi Đồng', N'Nguyễn Thị Kim Dung', N'49.jpg', NULL, 1, 5, 1)
INSERT [dbo].[Sach] ([MaSach], [TenSach], [MoTa], [DanhMuc], [Gia], [SoLuongTon], [NamSanXuat], [NhaXuatBan], [TacGia], [AnhBia], [SoLuongCan], [TinhTrang], [SoLuongTonToiThieu], [ThoiHan]) VALUES (50, N'Bộ Vở Bài Tập Lớp 8', NULL, 17, CAST(69000 AS Decimal(18, 0)), 20, 2010, N'Lao Động', N'Nhiều Tác Giả', N'50.jpg', NULL, 1, 5, 1)
SET IDENTITY_INSERT [dbo].[Sach] OFF
SET IDENTITY_INSERT [dbo].[TaiKhoan] ON 

INSERT [dbo].[TaiKhoan] ([MaTK], [TenDangNhap], [MatKhau], [TrangThai], [Quyen], [MaNguoiDung]) VALUES (1, N'VanAn', N'25F9E794323B453885F5181F1B624D0B', 1, N'Customer', 1)
INSERT [dbo].[TaiKhoan] ([MaTK], [TenDangNhap], [MatKhau], [TrangThai], [Quyen], [MaNguoiDung]) VALUES (2, N'ThuHuong', N'25F9E794323B453885F5181F1B624D0B', 1, N'Customer', 2)
INSERT [dbo].[TaiKhoan] ([MaTK], [TenDangNhap], [MatKhau], [TrangThai], [Quyen], [MaNguoiDung]) VALUES (3, N'1212205', N'26DAE66D062F121D4403D9B270DDC2FB', 1, N'Admin', NULL)
INSERT [dbo].[TaiKhoan] ([MaTK], [TenDangNhap], [MatKhau], [TrangThai], [Quyen], [MaNguoiDung]) VALUES (4, N'PhatDat', N'25D55AD283AA400AF464C76D713C07AD', 1, N'Business', 1)
INSERT [dbo].[TaiKhoan] ([MaTK], [TenDangNhap], [MatKhau], [TrangThai], [Quyen], [MaNguoiDung]) VALUES (5, N'HoangLong', N'25F9E794323B453885F5181F1B624D0B', 1, N'Business', 2)
SET IDENTITY_INSERT [dbo].[TaiKhoan] OFF
SET IDENTITY_INSERT [dbo].[Token] ON 

INSERT [dbo].[Token] ([ID], [Consumer_key], [Request_token], [Veritify_token], [Access_token], [MaNCC]) VALUES (1, N'123', N'xZIRwEmRg0yXmYESww0h7Q', N'FCmpmg2nukdxgr0G3IC4w', N'78dmNxDi0khwmed0vDjPQbws3laB6ncy', 1)
INSERT [dbo].[Token] ([ID], [Consumer_key], [Request_token], [Veritify_token], [Access_token], [MaNCC]) VALUES (2, N'55', NULL, NULL, NULL, 2)
SET IDENTITY_INSERT [dbo].[Token] OFF
ALTER TABLE [dbo].[ChiTietHopDong]  WITH CHECK ADD  CONSTRAINT [FK_ChiTietHopDong_HopDong] FOREIGN KEY([MaHopDong])
REFERENCES [dbo].[HopDong] ([MaHopDong])
GO
ALTER TABLE [dbo].[ChiTietHopDong] CHECK CONSTRAINT [FK_ChiTietHopDong_HopDong]
GO
ALTER TABLE [dbo].[ChiTietHopDong]  WITH CHECK ADD  CONSTRAINT [FK_ChiTietHopDong_Sach] FOREIGN KEY([MaSach])
REFERENCES [dbo].[Sach] ([MaSach])
GO
ALTER TABLE [dbo].[ChiTietHopDong] CHECK CONSTRAINT [FK_ChiTietHopDong_Sach]
GO
ALTER TABLE [dbo].[CTDH_KH]  WITH CHECK ADD  CONSTRAINT [FK_CTDH_KH_DonHang_KH] FOREIGN KEY([MaDonHang])
REFERENCES [dbo].[DonHang_KH] ([MaDonHang])
GO
ALTER TABLE [dbo].[CTDH_KH] CHECK CONSTRAINT [FK_CTDH_KH_DonHang_KH]
GO
ALTER TABLE [dbo].[CTDH_KH]  WITH CHECK ADD  CONSTRAINT [FK_CTDH_KH_Sach] FOREIGN KEY([MaSach])
REFERENCES [dbo].[Sach] ([MaSach])
GO
ALTER TABLE [dbo].[CTDH_KH] CHECK CONSTRAINT [FK_CTDH_KH_Sach]
GO
ALTER TABLE [dbo].[DonHang_KH]  WITH CHECK ADD  CONSTRAINT [FK_DonHang_KH_KhachHang] FOREIGN KEY([MaKH])
REFERENCES [dbo].[KhachHang] ([MaKH])
GO
ALTER TABLE [dbo].[DonHang_KH] CHECK CONSTRAINT [FK_DonHang_KH_KhachHang]
GO
ALTER TABLE [dbo].[DonHang_NCC]  WITH CHECK ADD  CONSTRAINT [FK_DonHang_NCC_HopDong] FOREIGN KEY([MaHopDong])
REFERENCES [dbo].[HopDong] ([MaHopDong])
GO
ALTER TABLE [dbo].[DonHang_NCC] CHECK CONSTRAINT [FK_DonHang_NCC_HopDong]
GO
ALTER TABLE [dbo].[DonHang_NCC]  WITH CHECK ADD  CONSTRAINT [FK_DonHang_NCC_Sach] FOREIGN KEY([MaSach])
REFERENCES [dbo].[Sach] ([MaSach])
GO
ALTER TABLE [dbo].[DonHang_NCC] CHECK CONSTRAINT [FK_DonHang_NCC_Sach]
GO
ALTER TABLE [dbo].[HopDong]  WITH CHECK ADD  CONSTRAINT [FK_HopDong_NhaCungCap] FOREIGN KEY([MaNCC])
REFERENCES [dbo].[NhaCungCap] ([MaNCC])
GO
ALTER TABLE [dbo].[HopDong] CHECK CONSTRAINT [FK_HopDong_NhaCungCap]
GO
ALTER TABLE [dbo].[KhaNangCungCap]  WITH CHECK ADD  CONSTRAINT [FK_KhaNangCungCap_NhaCungCap] FOREIGN KEY([MaNCC])
REFERENCES [dbo].[NhaCungCap] ([MaNCC])
GO
ALTER TABLE [dbo].[KhaNangCungCap] CHECK CONSTRAINT [FK_KhaNangCungCap_NhaCungCap]
GO
ALTER TABLE [dbo].[KhaNangCungCap]  WITH CHECK ADD  CONSTRAINT [FK_KhaNangCungCap_Sach] FOREIGN KEY([MaSach])
REFERENCES [dbo].[Sach] ([MaSach])
GO
ALTER TABLE [dbo].[KhaNangCungCap] CHECK CONSTRAINT [FK_KhaNangCungCap_Sach]
GO
ALTER TABLE [dbo].[Sach]  WITH CHECK ADD  CONSTRAINT [FK_Sach_DanhMuc] FOREIGN KEY([DanhMuc])
REFERENCES [dbo].[DanhMuc] ([MaDanhMuc])
GO
ALTER TABLE [dbo].[Sach] CHECK CONSTRAINT [FK_Sach_DanhMuc]
GO
ALTER TABLE [dbo].[Token]  WITH CHECK ADD  CONSTRAINT [FK_Token_NhaCungCap] FOREIGN KEY([MaNCC])
REFERENCES [dbo].[NhaCungCap] ([MaNCC])
GO
ALTER TABLE [dbo].[Token] CHECK CONSTRAINT [FK_Token_NhaCungCap]
GO
USE [master]
GO
ALTER DATABASE [QLSach] SET  READ_WRITE 
GO
