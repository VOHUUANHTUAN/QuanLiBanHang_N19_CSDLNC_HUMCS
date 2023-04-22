create database DoAnCSDLNC
go
use DoAnCSDLNC
go

select COUNT(*) from TaiKhoan
select COUNT(*) from QuanTri
select COUNT(*) from NhanVien
select COUNT(*) from DiaChi
select COUNT(*) from TaiKhoanNH 
select COUNT(*) from Doitac
select COUNT(*) from HopDong
select COUNT(*) from ChiNhanh
select COUNT(*) from ThucDon
select COUNT(*) from KhachHang
select COUNT(*) from GioHang
select COUNT(*) from Taixe
select COUNT(*) from DonDatHang
select COUNT(*) from ChiTietDonDatHang


select * from TaiKhoan 
select * from QuanTri
select * from NhanVien
select * from DiaChi
select * from TaiKhoanNH 
select * from Doitac where MaDoiTac = 5002
select * from HopDong
select * from ChiNhanh
select * from ThucDon
select * from KhachHang
select * from GioHang
select * from Taixe
select * from DonDatHang
select * from ChiTietDonDatHang


select (select COUNT(*) from TaiKhoan) TaiKhoan, (select COUNT(*) from QuanTri) QuanTri, (select COUNT(*) from NhanVien) NhanVien, (select COUNT(*) from DiaChi) DiaChi, (select COUNT(*) from TaiKhoanNH) TaiKhoanNH
from DoiTac where MaDoiTac = 1
select (select COUNT(*) from Doitac) Doitac, (select COUNT(*) from HopDong) HopDong, (select COUNT(*) from ChiNhanh) ChiNhanh, (select COUNT(*) from ThucDon) ThucDon
from DoiTac where MaDoiTac = 1
select (select COUNT(*) from Taixe) Taixe, (select COUNT(*) from KhachHang) KhachHang, (select COUNT(*) from GioHang) GioHang, (select COUNT(*) from DonDatHang) DonDatHang, (select COUNT(*) from ChiTietDonDatHang) ChiTietDonDatHang
from DoiTac where MaDoiTac = 1





create table TaiKhoan
(
	username varchar(20), 
	pass varchar(50) not null,
	Loai tinyint not null,
	TinhTrang nvarchar(20)
	-- 1.doiTac - 2.TaiXe - 3.khach - 10.admin - 11.nhanvien
	Constraint PK_TaiKhoan Primary key(username)
)
create table QuanTri
(
	MaQT int, 
	HoTen nvarchar(50), 
	username varchar(20)
	Constraint PK_QuanTri Primary key(MaQT),
	Foreign key(username) references TaiKhoan(username)
)

create table NhanVien
(
	MaNhanVien int, 
	HoTen nvarchar(50), 
	username varchar(20)
	Constraint PK_NhanVien Primary key(MaNhanVien),
	Foreign key(username) references TaiKhoan(username)
)	
create table DiaChi
(
	MaDiaChi int, 
	Tinh nvarchar(20), 
	Quan nvarchar(20), 
	DCCuThe	nvarchar(50)
	Constraint PK_DiaChi Primary key(MaDiaChi)
)	
--create table NganHang
--(
--	MaNganHang int, 
--	MaChiNhanh int, 
--	TenNH nvarchar(50), 
--	TenCN nvarchar(50)
--	Constraint PK_NganHang Primary key(MaNganHang, MaChiNhanh)
--)	
create table TaiKhoanNH
(
	MaNguoiDung int, 
	STK bigint, 
	SoDu int default 0,
	TenNH nvarchar(50)
	Constraint PK_TaiKhoanNH Primary key(MaNguoiDung)
)	
create table Doitac
(
	MaDoiTac int, 
	TenQuan nvarchar(50), 
	SDT bigint, 
	Email varchar(50), 
	MaSTK int, 
	MaSoThue bigint, 
	Tennguoidaidien nvarchar(50), 
	LoaiAmThuc nvarchar(15),
	username varchar(20)
	Constraint PK_Doitac Primary key(MaDoiTac),
	Foreign key(username) references TaiKhoan(username),
	Foreign key(MaSTK) references TaiKhoanNH(MaNguoiDung)
)	
create table HopDong
(
	MaHopDong int, 
	SoChiNhanh tinyint, 
	NgayBatDau date, 
	ThoiHan int, 
	MaDoiTac int, 
	XetDuyet nvarchar(10), 
	MaNhanVien int
	Constraint PK_HopDong Primary key(MaHopDong),
	Foreign key(MaNhanVien) references NhanVien(MaNhanVien),
	Foreign key(MaDoiTac) references DoiTac(MaDoiTac),
)	
create table ChiNhanh
(
	MaDoiTac int, 
	MaChiNhanh int, 
	SDT bigint, 
	MaDiaChi int, 
	GioMoCua time, 
	GioDongCua time, 
	TinhTrang nvarchar(15),
	MaHopDong int
	Constraint PK_ChiNhanh Primary key(MaChiNhanh,MaDoiTac),
	Foreign key(MaDoiTac) references DoiTac(MaDoiTac),
	Foreign key(MaDiaChi) references DiaChi(MaDiaChi),
	Foreign key(MaHopDong) references HopDong(MaHopDong)
)	
create table ThucDon
(	
	MaDoiTac int, 
	MaMonAn int, 
	TenMon nvarchar(20), 
	Gia int, 
	MieuTa nvarchar(50), 
	TinhTrang nvarchar(20),
	TuyChon nvarchar(20)
	Constraint PK_ThucDon Primary key(MaMonAn, MaDoiTac),
	Foreign key(MaDoiTac) references DoiTac(MaDoiTac)
)	
--create table TuyChonMonAn
--(
--	MaMonAn int, 
--	MaDoiTac int, 
--	MaTuyChon int, 
--	TenTC nvarchar(50), 
--	Mota nvarchar(50)
--	Constraint PK_TuyChonMonAn Primary key(MaMonAn, MaDoiTac, MaTuyChon),
--	Foreign key(MaMonAn,MaDoiTac) references ThucDon(MaMonAn,MaDoiTac)
--)	
create table KhachHang
(
	MaKhachHang int, 
	CMND int, 
	HoTen nvarchar(50), 
	SDT bigint, 
	MaDiaChi int, 
	MaSTK int, 
	Email nvarchar(50),
	username varchar(20)
	Constraint PK_KhachHang Primary key(MaKhachHang),
	Foreign key(username) references TaiKhoan(username),
	Foreign key (MaDiaChi) references DiaChi(MaDiaChi),
	Foreign key (MaSTK) references TaiKhoanNH(MaNguoiDung)
)	
create table GioHang
(
--MaGioHang int,
	MaKhachHang int, 
	MaDoiTac int,  
	MaMonAn int, 
	SoLuong	int
	--Constraint PK_GioHang Primary key(MaGioHang),
	Constraint PK_GioHang Primary key(MaKhachHang, MaMonAn, MaDoiTac),
	Foreign key(MaKhachHang) references KhachHang(MaKhachHang),
	Foreign key(MaMonAn,MaDoiTac) references ThucDon(MaMonAn,MaDoiTac)
)	
create table TaiXe
(
	MaTaiXe int , 
	CMND int, 
	HoTen nvarchar(50), 
	SDT bigint, 
	MaDiaChi int, 
	MaSTK int , 
	BienSoXe varchar(12), 
	Email nvarchar(50),
	username varchar(20)
	Constraint PK_Taixe Primary key(MaTaiXe),
	Foreign key(username) references TaiKhoan(username),
	Foreign key (MaDiaChi) references DiaChi(MaDiaChi),
	Foreign key (MaSTK) references TaiKhoanNH(MaNguoiDung)
)	
create table DonDatHang
(
	MaDonHang int, 
	GioDat time, 
	NgayDat date, 
	PhiShip int, 
	TongGia	int, 
	TinhTranggiao nvarchar(50),
	MaKhachHang int, 
	MaDoiTac int, 
	MaTaiXe int, 
	Constraint PK_DonDatHang Primary key(MaDonHang),
	Foreign key(MaKhachHang) references KhachHang(MaKhachHang),
	Foreign key(MaDoiTac) references DoiTac(MaDoiTac),
	Foreign key(MaTaiXe) references TaiXe(MaTaiXe)
)
create table ChiTietDonDatHang
(
	MaDonHang int, 
	MaDoiTac int, 
	MaMonAn int, 
	SoLuong int, 
	DanhGia	int -- số sao: 1 - 5
	Constraint PK_ChiTietDonDatHang Primary key(MaDonHang,MaDoiTac,MaMonAn),
	Foreign key(MaDonHang) references DonDatHang(MaDonHang),
	Foreign key(MaMonAn,MaDoiTac) references ThucDon(MaMonAn,MaDoiTac)
)
CREATE NONCLUSTERED INDEX ct_maDT_dg
ON [dbo].[ChiTietDonDatHang] ([MaDoiTac])
INCLUDE ([DanhGia])
GO
SET STATISTICS IO ON
go
SET STATISTICS TIME ON
go
select * from ChiTietDonDatHang where MaDoiTac = 2323
SET STATISTICS TIME Off
go
SET STATISTICS IO off
go
select* from Doitac
--==================================
ALTER DATABASE DoAnCSDLNC ADD FILEGROUP FG1
ALTER DATABASE DoAnCSDLNC ADD FILEGROUP FG2
ALTER DATABASE DoAnCSDLNC ADD FILEGROUP FG3

ALTER DATABASE DoAnCSDLNC ADD FILE (NAME = N'FG1', FILENAME = N'D:\DATA\FG1.ndf') TO FILEGROUP FG1

ALTER DATABASE DoAnCSDLNC ADD FILE (NAME = N'FG2', FILENAME = N'D:\DATA\FG2.ndf') TO FILEGROUP FG2

ALTER DATABASE DoAnCSDLNC ADD FILE (NAME = N'FG3', FILENAME = N'D:\DATA\FG3.ndf') TO FILEGROUP FG3


CREATE PARTITION FUNCTION PFunc_CTDH_MDT(int) AS RANGE Left FOR VALUES (2000, 4000)
GO
CREATE PARTITION SCHEME PScheme_CTDH_MDT AS PARTITION PFunc_CTDH_MDT TO (FG1, FG2, FG3)
GO

ALTER TABLE ChiTietDonDatHang
DROP CONSTRAINT PK_ChiTietDonDatHang

--
ALTER TABLE ChiTietDonDatHang
ADD PRIMARY KEY NONCLUSTERED(MaDonHang, MaDoiTac, MaMonAn ASC)
ON [PRIMARY]
--
CREATE CLUSTERED INDEX IX_MaDT_INT
ON ChiTietDonDatHang
(
	MaDoiTac
) ON PScheme_CTDH_MDT(MaDoiTac)

