use DoAnCSDLNC
go

-----------------------------------------------------------------------------------
------------------------------------- ĐỐI TÁC -------------------------------------
-----------------------------------------------------------------------------------
--Dt_SoLuongDonTheoNam
create --alter
proc Dt_SoLuongDonTheoNam
	@MaDoiTac varchar(10)
as
	begin tran
		begin try
			if not exists(select* from DoiTac where @MaDoiTac=MaDoiTac)
			begin
				print N'Mã đối tác không tồn tại'
				rollback tran
				select -1
				return
			end
			if not exists(select* from CHITIETDONDATHANG where MaDoiTac=@MaDoiTac)
			begin
				print N'Đối tác không có đơn hàng nào'
				rollback tran
				select -2
				return
			end
			select year(dh.NgayDat) as 'Nam',count(*) as SLDon
			from (select ddh.NgayDat,ddh.MaDonHang from DONDATHANG ddh, CHITIETDONDATHANG ct where ddh.MaDonHang=ct.MaDonHang and ct.MaDoiTac=@MaDoiTac) dh
			group by year(dh.NgayDat) 
			order by year(dh.NgayDat) desc

		end try
		begin catch
			print N'Lỗi hệ thống!'
			ROLLBACK TRAN
			select -10
			return
		END CATCH
COMMIT TRAN
return
GO
--Dt_SoLuongDonTheoNgay
create --alter
proc Dt_SoLuongDonTheoNgay
	@MaDoiTac varchar(10)
as
	begin tran
		begin try
			if not exists(select* from DoiTac where @MaDoiTac=MaDoiTac)
			begin
				print N'Mã đối tác không tồn tại'
				rollback tran
				select -1
				return
			end
			if not exists(select* from CHITIETDONDATHANG where MaDoiTac=@MaDoiTac)
			begin
				print N'Đối tác không có đơn hàng nào'
				rollback tran
				select -2
				return
			end
			select year(dh.NgayDat) Nam,month(dh.NgayDat) Thang,day(dh.NgayDat) Ngay,count(*) as SLDon
			from (select ddh.NgayDat,ddh.MaDonHang from DONDATHANG ddh, CHITIETDONDATHANG ct where ddh.MaDonHang=ct.MaDonHang and ct.MaDoiTac=@MaDoiTac) dh
			group by dh.NgayDat
			order by dh.NgayDat desc
		end try
		begin catch
			print N'Lỗi hệ thống!'
			ROLLBACK TRAN
			select -10
			return
		END CATCH
COMMIT TRAN
return
GO
--Dt_SoLuongDonTheoThang
create --alter
proc Dt_SoLuongDonTheoThang
	@MaDoiTac varchar(10)
as
	begin tran
		begin try
			if not exists(select* from DoiTac where @MaDoiTac=MaDoiTac)
			begin
				print N'Mã đối tác không tồn tại'
				rollback tran
				select -1
				return
			end
			if not exists(select* from CHITIETDONDATHANG where MaDoiTac=@MaDoiTac)
			begin
				print N'Đối tác không có đơn hàng nào'
				rollback tran
				select -2
				return
			end
			select year(dh.NgayDat) as Nam, month(dh.NgayDat) as Thang,count(*) as SLDon
			from (select ddh.NgayDat,ddh.MaDonHang from DONDATHANG ddh, CHITIETDONDATHANG ct where ddh.MaDonHang=ct.MaDonHang and ct.MaDoiTac=@MaDoiTac) dh
			group by year(dh.NgayDat),month(dh.NgayDat)
			order by year(dh.NgayDat) desc, month(dh.NgayDat) desc

		end try
		begin catch
			print N'Lỗi hệ thống!'
			ROLLBACK TRAN
			select -10
			return
		END CATCH
COMMIT TRAN
return
GO
--Dt_DemSoLuongBan
create --alter
function Dt_DemSoLuongBan(@MaMonAn varchar(10), @MaDoiTac varchar(10))
returns int
as
begin
	declare @out int
	set @out = (select (SUM(SoLuong+0)) from CHITIETDONDATHANG ct where ct.MaMonAn = @MaMonAn and ct.MaDoiTac = @MaDoiTac)
	if @out is null
		set @out = 0
	return @out
end	
GO
--Dt_XuHuongBan
create -- alter 
proc Dt_XuHuongBan
	@MaDoiTac varchar(10)
as
	begin try		
		select tp.MaDoiTac, tp.MaMonAn, tp.TenMon,
		replace(cast(round((select AVG(Danhgia*1.0) from CHITIETDONDATHANG ct where ct.MaMonAn = tp.MaMonAn and ct.MaDoiTac = tp.MaDoiTac),1) as varchar),'00000','')
			Like_,
			(dbo.Dt_DemSoLuongBan(tp.MaMonAn,tp.MaDoiTac)) Ban
		from ThucDon tp
		where MaDoiTac =@MaDoiTac

	end try
		
	begin catch
	end catch
GO
--Dt_DsChiNhanh
create --alter
proc Dt_DsChiNhanh 
	@mahd varchar(10)
as
	begin try
		select * from CHINHANH where MaHopDong = @mahd
	end try
	begin catch
	end catch
GO
--Dt_DsChiNhanhNull
create --alter
proc Dt_DsChiNhanhNull
	@MaDoiTac varchar(10)
as
	begin try
		select * from CHINHANH where MaDoiTac = @MaDoiTac and MaHopDong Is null
	end try
	begin catch
	end catch
GO
--dt_ThemChiNhanh
create -- alter
proc dt_ThemChiNhanh
	@MaDoiTac varchar(10),
	@TP nvarchar(30),
	@Quan nvarchar(30),
	@DiaChiCuThe nvarchar(50),
	@SDT bigint,
	@GioMoCua time, 
	@GioDongCua time,
	@TinhTrang nvarchar(30)
as 
	begin tran
		begin try
			if @MaDoiTac='' or @TP='' or @Quan='' or @DiaChiCuThe='' 
			or @SDT='' or @TinhTrang='' or @GioMoCua=''or @GioDongCua=''
			begin
				print N'Thông tin trống'
				rollback tran
				select 1
				return
			end
			if not exists(select* from DOITAC where MaDoiTac=@MaDoiTac)
			begin
				print N'Mã đối tác không tồn tại'
				rollback tran
				select 2 
				return
			end
			if exists(select* from CHINHANH where SDT=@SDT)
			begin
				print N'Số điện thoại đã tồn tại'
				rollback tran 
				select 3
				return
			end
			if @TinhTrang<>N'Hoạt động' and @TinhTrang<>N'Tạm nghỉ'
			begin
				print N'Tình trạng không hợp lệ'
				rollback tran
				select 4
				return
			end
			declare @stt int
			set @stt=0
			if exists (select * from ChiNhanh where MaDoiTac=@MaDoiTac)
			begin 
				set @stt=(select max(MaChiNhanh) from ChiNhanh where MaDoiTac=@MaDoiTac) 
			end
			set @stt=@stt+1
			declare @diachi int
			set @diachi=(select max(MaDiaChi) from DiaChi)
			set @diachi=@diachi+1
			insert into DiaChi values (@diachi,@TP,@Quan,@DiaChiCuThe)
			insert into ChiNhanh values
				(@MaDoiTac,@stt,@SDT,@diachi,@GioMoCua,@GioDongCua,@TinhTrang,NULL)
			select 0 
		end try
		begin catch
			print N'Lỗi hệ thống!'
			ROLLBACK TRAN
			select 10
			return 
		END CATCH
COMMIT TRAN
GO
--dt_xoaChiNhanh
create --alter
proc dt_xoaChiNhanh
		@MaDoiTac varchar(10),
		@STT int
as
begin transaction
	begin try
		-- Kiểm tra thông tin nhập không đượcc rỗng.
		if (@MaDoiTac = '' or
			@STT = '')
			begin
				print N'Thông tin nhập không được rỗng'
				rollback tran
				select 1
				return 
			end
		-- Kiểm tra tồn tại
		if not exists(select * from CHINHANH where MaChiNhanh = @STT and MaDoiTac = @MaDoiTac)
			begin
				print N'Không thể xoá, chi nhánh không tồn tại'
				rollback tran
				select 5
				return
			end
		declare @hopdong int
		set @hopdong = (select MaHopDong from ChiNhanh where MaDoiTac = @MaDoiTac and MaChiNhanh = @STT)
		delete from CHINHANH where MaDoiTac = @MaDoiTac and MaChiNhanh = @STT
		if @hopdong is not null
			update HopDong set SoChiNhanh = SoChiNhanh -1 where MaHopDong = @hopdong
		select 0
	end try

	begin catch
		print N'Lỗi hệ thống!'
		rollback tran
		return 10
	end catch
commit tran
GO
--dt_SuaChiNhanh
create --  alter
proc dt_SuaChiNhanh
	@MaDoiTac varchar(10),
	@TP nvarchar(30),
	@Quan nvarchar(30),
	@DiaChiCuThe nvarchar(50),
	@SDT bigint,
	@GioMoCua time, 
	@GioDongCua time,
	@TinhTrang nvarchar(30),
	@stt varchar(10)
as 
	begin tran
		begin try
			if @MaDoiTac='' or @TP='' or @Quan='' or @DiaChiCuThe='' 
			or @SDT=''or @GioMoCua=''or @GioDongCua='' or @stt =''
			begin
				print N'Thông tin trống'
				rollback tran
				select 1
				return
			end
			if not exists(select* from DOITAC where MaDoiTac=@MaDoiTac)
			begin
				print N'Mã đối tác không tồn tại'
				rollback tran
				select 2 
				return
			end
			if exists(select *from CHINHANH where SDT=@SDT and (MaDoiTac!= @MaDoiTac or MaChiNhanh != @stt)
			)
			begin
				print N'Số điện thoại đã tồn tại'
				rollback tran 
				select 3
				return
			end
			if @TinhTrang<>N'Hoạt động' and @TinhTrang<>N'Tạm nghỉ'
			begin
				print N'Tình trạng không hợp lệ'
				rollback tran
				select 4
				return
			end
			declare @diachi int
			set @diachi = (select MaDiaChi from ChiNhanh where MaChiNhanh=@stt and MaDoiTac =@MaDoiTac)
			update DiaChi set 
				Tinh=@TP,
				Quan=@Quan,
				DCCuThe=@DiaChiCuThe
				where MaDiaChi = @diachi
			update ChiNhanh set 
				MaDoiTac=@MaDoiTac,
				SDT=@SDT,
				TinhTrang=@TinhTrang,
				GioDongCua=@GioDongCua,
				GioMoCua=@GioMoCua
				where
				MaChiNhanh=@stt and MaDoiTac =@MaDoiTac
			select 0 
		end try
		begin catch
			print N'Lỗi hệ thống!'
			ROLLBACK TRAN
			select 10
			return 
		END CATCH
COMMIT TRAN
return
GO
--dt_ThemThucPham
create -- alter
proc dt_ThemThucPham
	@MaDoiTac varchar(10),
	@TenMon nvarchar(30),
	@MieuTa nvarchar(50),
	@Gia decimal(10,1),
	@TinhTrang nvarchar(30),
	@TuyChon nvarchar(50)
as
	begin tran
		begin try
			if @MaDoiTac='' or @TenMon=''
			or @TinhTrang=''
			begin
				print N'Thông tin trống'
				rollback tran
				select 1
				return
			end
			if not exists(select* from DoiTac where MaDoiTac=@MaDoiTac)
			begin
				print N'Mã đối tác không tồn tại'
				rollback tran
				select 2
				return
			end
			if exists(select* from ThucDon where @TenMon=TenMon and MaDoiTac=@MaDoiTac)
			begin
				print N'Tên thực phẩm này đã tồn tại'
				rollback tran
				select 3
				return
			end
			if @TinhTrang<>N'Sẵn có' and @TinhTrang<>N'Ngưng bán' and @TinhTrang<>N'Hết hàng'
			begin
				print N'Tình trạng không hợp lệ'
				rollback tran
				select 4
				return
			end
			declare @MaTP int
			set @MaTP=0
			if exists (select * from ThucDon where MaDoiTac=@MaDoiTac)
			begin 
				set @MaTP=(select max(MaMonAn) from ThucDon where MaDoiTac=@MaDoiTac) 
			end
			if @MaTP is null
				set @MaTP=1
			set @MaTP=@MaTP+1
			insert into ThucDon values 
			(@MaDoiTac,@MaTP,@TenMon,@Gia,@MieuTa,@TinhTrang,@TuyChon)
			select 0 
		end try
		begin catch
			print N'Lỗi hệ thống!'
			rollback tran
			select 10
			return
		END CATCH
COMMIT TRAN
return
GO
--dt_XoaThucPham
create -- alter
proc dt_XoaThucPham
		@MaTP varchar(10),
		@MaDoiTac varchar(10)
as
begin transaction
	begin try
		-- Kiểm tra thông tin nhập không đượcc rỗng.
		if (@MaTP = '' or
			@MaDoiTac = '')
			begin
				print N'Thông tin nhập không được rỗng'
				rollback tran
				select 1
				return
			end
		if exists(select MaMonAn from CHITIETDONDATHANG where MaMonAn = @MaTP and
			MaDoiTac = @MaDoiTac)
			begin
				print N'Không thể xoá, món ăn đã từng được lên đơn'
				rollback tran
				select 5
				return 
			end
		-- Kiểm tra tồn tại
		if not exists(select MaMonAn from ThucDon where MaMonAn = @MaTP and
			MaDoiTac = @MaDoiTac)
			begin
				print N'Không thể xoá, món ăn không tồn tại'
				rollback tran
				select 6
				return
			end
		delete from ThucDOn where MaMonAn = @MaTP and
			MaDoiTac = @MaDoiTac
		select 0
	end try

	begin catch
		print N'Lỗi hệ thống!'
		rollback tran
		select 10
		return 
	end catch
commit tran
return 
GO
--dt_SuaThucPham
create -- alter
proc dt_SuaThucPham
	@MaDoiTac varchar(10),
	@TenMon nvarchar(30),
	@MieuTa nvarchar(50),
	@Gia decimal(10,1),
	@TinhTrang nvarchar(30),
	@TuyChon nvarchar(50),
	@MaTP varchar(10)
as
	begin tran
		begin try
			if @MaDoiTac='' or @TenMon='' 
			or @TinhTrang=''   or @MaTP=''
			begin
				print N'Thông tin trống'
				rollback tran
				select 1
				return
			end
			if not exists(select* from DoiTac where MaDoiTac=@MaDoiTac)
			begin
				print N'Mã đối tác không tồn tại'
				rollback tran
				select 2
				return
			end
			--if exists(select* from ThucDon where @TenMon=TenMon and (MaDoiTac!=@MaDoiTac or MaMonAn!=@MaTP)
			if exists(select TenMon
				from (select * from ThucDon where MaMonAn != @MaTP and @MaDoiTac = MaDoiTac) t
				where t.TenMon = @TenMon )
			begin
				print N'Tên thực phẩm này đã tồn tại'
				rollback tran
				select 3
				return
			end
			if @TinhTrang<>N'Sẵn có' and @TinhTrang<>N'Ngưng bán' and @TinhTrang<>N'Hết hàng'
			begin
				print N'Tình trạng không hợp lệ'
				rollback tran
				select 4
				return
			end
			update ThucDon
			set TenMon=@TenMon,MieuTa=@MieuTa,Gia=@Gia,TinhTrang=@TinhTrang,TuyChon=@TuyChon
			where MaMonAn=@MaTp and MaDoiTac=@MaDoiTac
			if @Gia<=0
			begin 
				print N'Giá phải lớn hơn 0'
				rollback tran
				select 7
				return
			end
		end try
		begin catch
			print N'Lỗi hệ thống!'
			rollback tran
			select 10
			return
		END CATCH
COMMIT TRAN
select 0 
return
GO

--dt_ThemHopDong
create -- alter
proc dt_ThemHopDong
	@SLChiNhanh smallint,
	@NgayBatDau date,
	@ThoiHan int,
	@MaDoiTac varchar(10)
as
	begin tran
		begin try
			if @SLChiNhanh<=0  or  @NgayBatDau='' or @ThoiHan='' or @MaDoiTac=''
			begin
				print N'Thông tin trống'
				rollback tran
				select 1
				return
			end
			if not exists(select* from DoiTac where @MaDoiTac=MaDoiTac)
			begin
				print N'Mã đối tác không tồn tại'
				select 2
				return
			end
			declare @ngDaiDien nvarchar(30)
			select @ngDaiDien=Tennguoidaidien 
			from DoiTac
			where MaDoiTac=@MaDoiTac


			declare @MaHD int
			set @MaHD=0
			if (select count(*) from HopDong)>0
			begin
				set @MaHD=(select max(MaHopDong) from HopDong)
			end
			set @MaHD=@MaHD+1

			select (@MaHD*-1)
			insert into HopDong values (@MaHD,@SLChiNhanh,@NgayBatDau,@ThoiHan,@MaDoiTac,N'Chờ duyệt',NULL)
			
		end try
		begin catch
			print N'Lỗi hệ thống!'
			ROLLBACK TRAN
			select 10
			return
		END CATCH
COMMIT TRAN
select 0
GO
 --dt_danhsachdonhang
create -- alter
proc dt_danhsachdonhang 
		@keysearch nvarchar(50), 
		@madoitac int,
		@from int,
		@to int
as
begin try
	if @from = ''
		set @from =0
	if @to = ''
		set @to = (select count(MaDonHang) from DonDatHang)
	declare @madon int
	begin try
		set @madon = cast(@keysearch as int)
	end try
	begin catch
		set @madon = 0
	end catch
	
	SELECT *
	FROM	(SELECT *,
					(select k.HoTen from KHACHHANG k where k.MaKhachHang = dh_cho.MaKhachHang) TenKH,
                    (select t.HoTen from TAIXE t where t.MaTaiXe = dh_cho.MaTaiXe) TenTX, 
					ROW_NUMBER() OVER (ORDER BY MaDonHang) as row
			FROM DonDatHang dh_cho
			Where	(MaTaiXe in (select distinct MaTaiXe from TaiXe where HoTen like '%'+@keysearch+'%') or
					MaKhachHang in (select distinct MaKhachHang from KhachHang where HoTen like '%'+@keysearch+'%') or 
					MaDonHang = @madon) and --in (select distinct MaDonHang from DonDatHang where MaDonHang like '%'+@madon+'%')
					MaDoiTac = @madoitac
			) a 
	WHERE @From < row and row <= @to
end try
begin catch
end catch
go
select * from DonDatHang where MaDoiTac = 4384
--dt_danhsachdonhang_dem
create -- alter
proc dt_danhsachdonhang_dem 
		@keysearch nvarchar(50), 
		@madoitac int,
		@from int,
		@to int
as
begin try
	declare @madon int
	begin try
		set @madon = cast(@keysearch as int)
	end try
	begin catch
		set @madon = 0
	end catch
	SELECT distinct count(MaDonHang)
	FROM DonDatHang dh_cho
	Where	(MaTaiXe in (select distinct MaTaiXe from TaiXe where HoTen like '%'+@keysearch+'%') or
			MaKhachHang in (select distinct MaKhachHang from KhachHang where HoTen like '%'+@keysearch+'%') or 
			MaDonHang = @madon) and --in (select distinct MaDonHang from DonDatHang where MaDonHang like '%'+@madon+'%')
			MaDoiTac = @madoitac			
end try
begin catch
end catch
go

-----------------------------------------------------------------------------------
------------------------------- Đăng Nhập - Đăng Ký -------------------------------
-----------------------------------------------------------------------------------
--DangNhap
create -- alter 
proc DangNhap @username varchar(20),@password varchar(50)
as
begin try
	if @username = '' or @password = ''
	begin
		select -1
		return
	end
	if CHARINDEX(' ',trim(' ' from @username)) != 0 
	begin
		select -2
		return
	end
	if CHARINDEX(' ',trim(' ' from @password)) != 0 
	begin
		select -3
		return
	end
	declare @loai int
	set @loai = (select Loai from TaiKhoan where @username = username and @password = pass)
	if @loai is null
	begin
		select -4
		return
	end
	if exists(select * from TaiKhoan where @username = username and (TinhTrang = N'Khóa' or TinhTrang = N'Khoá'))
	begin
		select -5
		return
	end
	select @loai
	return
end try
begin catch
	--Lỗi hệ thông
	select 0
	return
end catch
go
--dangky_DoiTac
create -- alter 
proc dangky_DoiTac
		@username varchar(20),
		@pass varchar(50),
		@Ten nvarchar(50),
		@SDT bigint, 
		@Email varchar(50), 
		@TenNH nvarchar(50),
		@STK bigint, 
		@SoDu int, 

		@TenQuan nvarchar(50), 
		@MST bigint,
		@loaiamthuc nvarchar(15)
as
begin tran dangky
	begin try
		-- 1. Trường trống
		if	@username = '' or
			@pass = '' or
			@Ten = '' or
			@SDT = '' or 
			@Email = '' or 
			@TenNH = '' or
			@STK = '' or 
			@TenQuan = '' or 
			@MST = ''
		begin
			rollback tran 
			select 1
			return
		end
		
		if exists(select username from TaiKhoan where username=@username)
		begin
			rollback tran 
			select 2
			return
		end
		
		if CHARINDEX(' ',trim(' ' from @username)) != 0 or CHARINDEX(' ',trim(' ' from @pass)) != 0 
		begin
			rollback tran 
			select 3
			return
		end
		
		if exists(select MaNguoiDung from TaiKhoanNH where STK = @STK)
		begin
			rollback tran 
			select 4
			return
		end
		/*
		0. Thành công
		10.Dữ liệu lỗi
		1. Trường trống
		2. username đã tồn tại
		3. username và pass không được chứa khoảng trắng
		4. STK đã tồn tại
		*/
		insert into TaiKhoan values (@username,@pass,1,N'Hoạt động')
		
		declare @mastk int
		set @mastk = (select MAX(MaNguoiDung) from TaiKhoanNH)
		if @mastk is null
			set @mastk = 0
		set @mastk = @mastk + 1
		insert into TaiKhoanNH values (@mastk,@STK,@SoDu,@TenNH)
		
		declare @ma int
		set @ma = (select MAX(MaDoiTac) from Doitac)
		if @ma is null
			set @ma = 0
		set @ma = @ma + 1
		insert into Doitac values (@ma,@TenQuan,@SDT,@Email,@mastk,@MST,@Ten,@loaiamthuc,@username) 
		select 0

	end try

	begin catch
		print N'Lỗi hệ thống'
		rollback tran 
		select 10
		return
	end catch
commit tran dangky
go
--dangky_KhachHang
create--alter
proc dangky_KhachHang
		@username varchar(20),
		@pass varchar(50),
		@Ten nvarchar(50),
		@SDT bigint, 
		@Email varchar(50), 
		@TenNH nvarchar(50),
		@STK bigint, 
		@SoDu int, 

		@Tinh nvarchar(20), 
		@Quan nvarchar(20), 
		@DCCuThe nvarchar(50),
		@CMND int
as
begin tran 
	begin try
		-- 1. Trường trống
		if	@username = '' or
			@pass = '' or
			@Ten = '' or
			@SDT = '' or 
			@Email = '' or 
			@TenNH = '' or
			@STK = '' or 
			@CMND = '' or 
			@Tinh = '' or 
			@Quan = '' or 
			@DCCuThe = ''
		begin
			rollback tran
			select 1
			return
		end
		if exists(select username from TaiKhoan where username=trim(' ' from @username))
		begin
			rollback tran
			select 2
			return
		end
		if CHARINDEX(' ',trim(' ' from @username)) != 0 or CHARINDEX(' ',trim(' ' from @pass)) != 0 
		begin
			rollback tran
			select 3
			return
		end
		if exists(select MaNguoiDung from TaiKhoanNH where STK = @STK)
		begin
			rollback tran
			select 4
			return
		end
		/*
		0. Thành công
		10.Dữ liệu lỗi
		1. Trường trống
		2. username đã tồn tại
		3. username và pass không được chứa khoảng trắng
		4. STK đã tồn tại
		*/
		insert into TaiKhoan values (@username,@pass,3,N'Hoạt động')
		
		declare @mastk int
		set @mastk = (select MAX(MaNguoiDung) from TaiKhoanNH)
		if @mastk is null
			set @mastk = 0
		set @mastk = @mastk + 1
		insert into TaiKhoanNH values (@mastk,@STK,@SoDu,@TenNH)
		
		declare @madiachi int
		set @madiachi = (select MAX(MaDiaChi) from DiaChi)
		if @madiachi is null
			set @madiachi = 0
		set @madiachi = @madiachi + 1
		insert into DiaChi values (@madiachi,@Tinh,@Quan,@DCCuThe)
		
		declare @ma int
		set @ma = (select MAX(MaKhachHang) from KhachHang)
		if @ma is null
			set @ma = 0
		set @ma = @ma + 1
		insert into KhachHang values (@ma,@CMND,@Ten,@SDT,@madiachi,@mastk,@Email,@username)
	end try

	begin catch
		print N'Lỗi hệ thống'
		rollback tran
		select 10
		return
	end catch
commit tran
select 0
go
--dangky_TaiXe
create --alter
proc dangky_TaiXe
		@username varchar(20),
		@pass varchar(50),
		@Ten nvarchar(50),
		@SDT bigint, 
		@Email varchar(50), 
		@TenNH nvarchar(50),
		@STK bigint, 
		@SoDu int, 
		

		@Tinh nvarchar(20), 
		@Quan nvarchar(20), 
		@DCCuThe nvarchar(50),
		@CMND int,
		@BienSoXe varchar(12)
as
begin tran
	begin try
		-- 1. Trường trống
		if	@username = '' or
			@pass = '' or
			@Ten = '' or
			@SDT = '' or 
			@Email = '' or 
			@TenNH = '' or
			@STK = '' or 
			@CMND = '' or 
			@Tinh = '' or 
			@Quan = '' or 
			@DCCuThe = ''or 
			@BienSoXe = ''
		begin
			rollback tran
			select 1
			return
		end
		if exists(select username from TaiKhoan where username=trim(' ' from @username))
		begin
			rollback tran
			select 2
			return
		end
		if CHARINDEX(' ',trim(' ' from @username)) != 0 or CHARINDEX(' ',trim(' ' from @pass)) != 0 
		begin
			rollback tran
			select 3
			return
		end
		if exists(select MaNguoiDung from TaiKhoanNH where STK = @STK)
		begin
			rollback tran
			select 4
			return
		end
		/*
		0. Thành công
		10.Dữ liệu lỗi
		1. Trường trống
		2. username đã tồn tại
		3. username và pass không được chứa khoảng trắng
		4. STK đã tồn tại
		*/
		declare @ma int
		insert into TaiKhoan values (@username,@pass,2,N'Hoạt động')
		
		declare @mastk int
		set @mastk = (select MAX(MaNguoiDung) from TaiKhoanNH)
		if @mastk is null
			set @mastk = 0
		set @mastk = @mastk + 1
		insert into TaiKhoanNH values (@mastk,@STK,@SoDu,@TenNH)

		
		declare @madiachi int
		set @madiachi = (select MAX(MaDiaChi) from DiaChi)
		if @madiachi is null
			set @madiachi = 0
		set @madiachi = @madiachi + 1
		insert into DiaChi values (@madiachi,@Tinh,@Quan,@DCCuThe)
		

		set @ma = (select MAX(MaTaiXe) from TaiXe)
		if @ma is null
			set @ma = 0
		set @ma = @ma + 1
		insert into Taixe values (@ma,@CMND,@Ten,@SDT,@madiachi,@mastk,@BienSoXe,@Email,@username)
	end try

	begin catch
		print N'Lỗi hệ thống'
		rollback tran
		select 10
		return
	end catch
commit tran
select 0
go

-----------------------------------------------------------------------------------
----------------------------------- Khách Hàng ------------------------------------
-----------------------------------------------------------------------------------
--kh_capnhatthongtin
create -- alter
proc kh_capnhatthongtin
		@MaKH varchar(20),
		@Ten nvarchar(50),
		@SDT bigint, 
		@Email varchar(50), 
		@TenNH nvarchar(50),
		@STK bigint, 

		@Tinh nvarchar(20), 
		@Quan nvarchar(20), 
		@DCCuThe nvarchar(50),
		@CMND int
as
begin tran 
	begin try
		-- 1. Trường trống
		if	@Ten = '' or
			@SDT = '' or 
			@Email = '' or 
			@TenNH = '' or
			@STK = '' or 
			@CMND = '' or 
			@Tinh = '' or 
			@Quan = '' or 
			@DCCuThe = ''
		begin
			rollback tran
			select 1
			return
		end
		if exists(	select MaNguoiDung 
					from TaiKhoanNH 
					where	STK = @STK and 
							MaNguoiDung != (select MaSTK from KhachHang where MaKhachHang = @MaKH))
		begin
			rollback tran
			select 2
			return
		end
		update DiaChi set Tinh= @Tinh, Quan =@Quan, DCCuThe =@DCCuThe 
			where MaDiaChi = (select MaDiaChi from KhachHang where MaKhachHang = @MaKH)
		update TaiKhoanNH set TenNH= @TenNH, STK = @STK
			where MaNguoiDung = (select MaSTK from KhachHang where MaKhachHang = @MaKH)
		update KhachHang set HoTen=@Ten,SDT = @SDT,Email =@Email,CMND = @CMND
			where MaKhachHang = @MaKH
		select 0
	end try
	begin catch
		rollback tran
		select 10
		return
	end catch
commit tran
go
--kh_danhsachdoitac
create -- alter
proc kh_danhsachdoitac 
		@tenmon nvarchar(20),
		@tendoitac nvarchar(50), 
		@from int,
		@to int
as
begin try
	if @from = ''
		set @from = 0
	if @to = ''
		set @to = (select count(MaDoiTac) from Doitac)
	
	SELECT *
	FROM	(SELECT distinct  MaDoiTac, TenQuan , ROW_NUMBER() OVER (ORDER BY MaDoiTac) as row 
			FROM Doitac
			Where MaDoiTac in (select distinct MaDoiTac from ThucDon where TenMon like '%'+@tenmon+'%') or
					MaDoiTac in (select distinct MaDoiTac from Doitac where TenQuan like '%'+@tendoitac+'%')
			) a 
	WHERE @From < row and row <= @to
end try
begin catch
end catch
go

--kh_danhsachdoitac_dem
create -- alter
proc kh_danhsachdoitac_dem 
		@tenmon nvarchar(20),
		@tendoitac nvarchar(20), 
		@from int,
		@to int
as
begin try
	SELECT distinct  count(MaDoiTac)
	FROM Doitac
	Where MaDoiTac in (select distinct MaDoiTac from ThucDon where TenMon like '%'+@tenmon+'%') or
					MaDoiTac in (select distinct MaDoiTac from Doitac where TenQuan like '%'+@tendoitac+'%')
			
end try
begin catch
end catch
go
--kh_danhsachthucdon
create -- alter
proc kh_danhsachthucdon @MaDoiTac int
as
begin try
	SELECT *,( 
		select replace(cast(round((select AVG(Danhgia*1.0) from CHITIETDONDATHANG ct where ct.MaMonAn = tp.MaMonAn and ct.MaDoiTac = tp.MaDoiTac),1) as varchar),'00000','')    Like_) Rate,Gia*0 SoLuong
	FROM ThucDon tp
	Where TinhTrang = N'Sẵn có' and MaDoiTac =@MaDoiTac 
end try
begin catch
end catch
go
--kh_giohang
create -- alter
proc kh_giohang @MaKhachHang int
as
begin try
	SELECT*, (select TenMon from ThucDon where MaDoiTac = gh.MaDoiTac and MaMonAn= gh.MaMonAn)TenMon,(select Gia from ThucDon where MaDoiTac = gh.MaDoiTac and MaMonAn= gh.MaMonAn)Gia
	FROM GioHang gh
	Where MaKhachHang = @MaKhachHang
end try
begin catch
end catch
go
--kh_tonggiohang
create -- alter
proc kh_tonggiohang @MaKhachHang int
as
begin try
	select sum(Gia*SoLuong)
	from (	SELECT (select Gia from ThucDon where MaDoiTac = gh.MaDoiTac and MaMonAn= gh.MaMonAn)Gia,Soluong
			FROM GioHang gh
			Where MaKhachHang = @MaKhachHang) a
end try
begin catch
end catch
go
go
--kh_ThemDonHang
create -- alter
proc kh_ThemDonHang
		@giodat time,
		@ngaydat date,
		@phiship int,
		@MaKhachHang int
as
begin tran
	begin try
		-- Nếu giỏ rỗng thì ngưng
		if not exists (select MaKhachHang from GioHang where MaKhachHang = @MaKhachHang)
		begin
			rollback tran
			select 1
			return
		end
		-- Lấy mã đối tác ứng với GIOHANG hiện tại của KHACHHANG
		declare @MaDoiTac int
		set @MaDoiTac = (select distinct Madoitac from GioHang where MaKhachHang = @MaKhachHang)

		-- Tạo mã đơn hàng mới 
		declare @maDH int
		set @maDH = (select Max(madonhang) from DonDatHang)
		if @maDH is null
			set @maDH = 1
		set @maDH = @maDH +1

		-- Tính tổng tiền của đơn hàng sắp tạo
		declare @tonggia int
		set @tonggia = 
				(select sum(Gia*SoLuong)
				from (	SELECT (select Gia from ThucDon where MaDoiTac = gh.MaDoiTac and MaMonAn= gh.MaMonAn)Gia,Soluong
						FROM GioHang gh
						Where MaKhachHang = @MaKhachHang) a)
		
		-- Thêm đơn hàng mới
		insert into DonDatHang values (@maDH,@giodat,@ngaydat,@phiship,@tonggia,N'Chờ xử lí',@MaKhachHang,@MaDoiTac,null)

		-- Chuẩn bị để chi tiết đơn hàng
		declare @count int

		declare @mamonan int
		declare @SL int
		set @count = (select count(*) from GioHang where MaKhachHang = @MaKhachHang)
		while @count > 0
		begin
			set @mamonan = (select TOP 1 MaMonAn from GioHang where MaKhachHang = @MaKhachHang)
			set @SL = (select Soluong from GioHang where MaKhachHang = @MaKhachHang and MaMonAn = @mamonan)
			insert into ChiTietDonDatHang values (@maDH,@MaDoiTac,@mamonan,@SL,null)
			delete GioHang where MaKhachHang = @MaKhachHang and @mamonan = MaMonAn
			set @count = @count - 1 
		end

		-- Trừ tiền
		update TaiKhoanNH set SoDu = SoDu - @tonggia - @phiship where MaNguoiDung = (select MaSTK from KhachHang where MaKhachHang = @MaKhachHang)
		
	end try
	begin catch
		rollback tran
		select 10
		return
	end catch
commit tran
select 0
go
--kh_themSPvaogio
create -- alter 
proc kh_themSPvaogio 
		@khachhang int,
		@doitac int,
		@monan int,
		@sl int
as
begin tran
	begin try
		if exists (select Makhachhang from GioHang where MaMonAn = @monan and MaDoiTac = @doitac and MaKhachHang = @khachhang)
		begin
			update GioHang set Soluong = Soluong + @sl where MaMonAn = @monan and MaDoiTac = @doitac and MaKhachHang = @khachhang
		end
		else
		begin
			insert into GioHang values (@khachhang,@doitac,@monan,@sl)
		end
	end try
	begin catch
		
	end catch
commit tran
go
--kh_HuyDonHang
create -- alter 
proc kh_HuyDonHang @MaKhachHang int, @DonHang int
as
begin tran
	begin try
		if not exists (select MaDonHang from DonDatHang where MaDonHang = @DonHang and MaKhachHang = @MaKhachHang and TinhTranggiao = N'Chờ xử lí')
		begin
			rollback tran
			select 2
			return
		end
		-- Hoàn tiền
		update TaiKhoanNH set SoDu = SoDu + dbo.kh_tinhgiadon(@DonHang) + (select TongGia from DonDatHang where MaDonHang =  @DonHang) where MaNguoiDung = (select MaSTK from KhachHang where MaKhachHang = @MaKhachHang)
		update DonDatHang set TinhTranggiao = N'Đã huỷ' where MaDonHang = @DonHang and MaKhachHang = @MaKhachHang
		select 0
	end try
	begin catch
		rollback tran
		select 10
		return
	end catch
commit tran
go
--kh_tinhgiadon
create --alter
function kh_tinhgiadon (@madonhang int)
returns int
as
begin 
	return (select sum(Gia*SoLuong)
			from (	SELECT (select Gia from ThucDon where MaDoiTac = gh.MaDoiTac and MaMonAn= gh.MaMonAn)Gia,Soluong
					FROM ChiTietDonDatHang gh
					Where MaDonHang = @madonhang) a)
end
go




-----------------------------------------------------------------------------------
------------------------------------ QUẢN TRỊ -------------------------------------
-----------------------------------------------------------------------------------
--QTV_danhsachtaikhoan
create -- alter
proc QTV_danhsachtaikhoan 
		@keysearch nvarchar(50),
		@from int,
		@to int
as
begin try
	if @from = ''
		set @from = 0
	if @to = ''
		set @to = (select count(username) from TaiKhoan)
	
	SELECT *
	FROM	(SELECT * , ROW_NUMBER() OVER (ORDER BY username) as row 
			FROM TaiKhoan
			Where	username in (select distinct username from TaiKhoan where username like '%'+@keysearch+'%') or
					username in (select distinct username from Doitac where MaDoiTac like '%'+@keysearch+'%')or
					username in (select distinct username from KhachHang where MaKhachHang like '%'+@keysearch+'%')or
					username in (select distinct username from TaiXe where MaTaiXe like '%'+@keysearch+'%')
			) a 
	WHERE @From < row and row <= @to
end try
begin catch
end catch
go
QTV_danhsachtaikhoan 'Joni','1','20'

--kh_danhsachdoitac_dem
create -- alter
proc QTV_danhsachtaikhoan_dem 
		@keysearch nvarchar(50),
		@from int,
		@to int
as
begin try
	SELECT count(username) 
			FROM TaiKhoan
			Where	username in (select distinct username from TaiKhoan where username like '%'+@keysearch+'%') or
					username in (select distinct username from Doitac where MaDoiTac like '%'+@keysearch+'%')or
					username in (select distinct username from KhachHang where MaKhachHang like '%'+@keysearch+'%')or
					username in (select distinct username from TaiXe where MaTaiXe like '%'+@keysearch+'%')
end try
begin catch
end catch
go
--Lấy thông tin quản trị viên
create proc QTV_LayThongTin
	@username varchar(20)
as
begin
	select MaQT, HoTen
	from QuanTri
	where @username=username
end
go

--Lấy thông tin 1 user
create --alter
proc LayThongTinTaiKhoan
	@username varchar(20)
as
begin tran
	begin try
		if @username=''
		begin 
			print N'Thông tin trống'
			rollback tran
			select -1
			return
		end
		if not exists(select* from TaiKhoan where @username=username)
		begin
			print N'Username không tồn tại'
			rollback tran
			select -2
			return
		end
		select *
		from TaiKhoan
		where @username=username
	end try
	begin catch
		print N'Lỗi hệ thống!'
		ROLLBACK TRAN
		select -10
		return
	END CATCH
COMMIT TRAN
GO


	
--DoiMatKhau
create --alter
proc DoiMatKhau
	@username varchar(20),
	@matkhaucu varchar(50),
	@matkhaumoi varchar(50),
	@matkhaumoi2 varchar(50)
as
begin tran
	begin try
		if @matkhaucu!=(select pass from TaiKhoan where @username=username)
		begin
			print N'Mật khẩu cũ không chính xác'
			rollback tran
			select -1
			return
		end
		if @matkhaumoi=''
		begin
			print N'Mật khẩu mới rỗng'
			rollback tran
			select -2
			return
		end
		if @matkhaumoi!=@matkhaumoi2
		begin
			print N'Mật khẩu nhập lại không giống'
			rollback tran
			select -3
			return
		end
		update TaiKhoan
		set pass=@matkhaumoi
		where username=@username
	end try
	begin catch
		print N'Lỗi hệ thống!'
		ROLLBACK TRAN
		select -10
		return
	END CATCH
COMMIT TRAN
select 0
GO
	

--Exec LayThongTinTaiKhoan 'admin'

--QTV_SuaThongTin
create --alter
proc QTV_SuaThongTin
	@username varchar(20),
	@HoTenMoi nvarchar(50)
as
begin tran
	begin try
		if @HoTenMoi=''
		begin
			print N'Tên rỗng'
			rollback tran
			select -1
			return
		end
		update QuanTri
		set HoTen=@HoTenMoi
		where username=@username
	end try
	begin catch
		print N'Lỗi hệ thống!'
		ROLLBACK TRAN
		select -10
		return
	END CATCH
COMMIT TRAN
select 0
GO
--Exec QTV_SuaThongTin 'admin',N'Tiến Minh Lee'
--select* from TaiKhoan

--CapNhatTinhTrangTaiKhoan
create --alter
proc CapNhatTinhTrangTaiKhoan
	@username varchar(20),
	@TinhTrangMoi nvarchar(20)
as
begin tran
	begin try
		if @username=''
		begin
			print N'Bạn chưa chọn '
			rollback tran
			select -1
			return
		end
		update TaiKhoan
		set TinhTrang=@TinhTrangMoi
		where @username=username
	end try
	begin catch
		print N'Lỗi hệ thống!'
		ROLLBACK TRAN
		select -10
		return
	END CATCH
COMMIT TRAN
select 0
GO


create proc QTV_LayThongTinNhanVien
as
begin
	select	t.username,n.MaNhanVien as 'Ma', n.HoTen, t.Loai
	from TaiKhoan t, NhanVien n
	where t.username=n.username
end
go


create proc QTV_LayThongTinQuanTri
as
begin
	select	t.username,q.MaQT as 'Ma', q.HoTen, t.Loai
	from TaiKhoan t, QuanTri q
	where t.username=q.username
end
go


--Thêm quản trị viên/Nhân viên
create --alter
proc QTV_ThemQuanTriVien_NhanVien
	@username varchar(20),
	@HoTen nvarchar(50),
	@pass varchar(20),
	@pass2 varchar(20),
	@role nvarchar(20)
as
begin tran
	begin try
		if @username='' or @HoTen='' or @pass='' or @pass2 ='' or @role=''
		begin 
			print N'Có trường thông tin bị trống'
			rollback tran
			select -1
			return
		end
		if exists(select* from TaiKhoan where username=@username)
		begin
			print N'Tên đăng nhập đã tồn tại'
			rollback tran
			select -2
			return
		end
		if @pass!=@pass2
		begin
			print N'Pass nhập loại chưa đúng'
			rollback tran
			select -3
			return
		end
		if @role=N'Nhân viên'
		begin
			declare @MaNhanVien int
			set @MaNhanVien=0
			if (select count(*) from NhanVien)>0
			begin
				set @MaNhanVien=(select max(MaNhanVien) from NhanVien)
			end
			set @MaNhanVien+=1
			insert into TaiKhoan values(@username,@pass,11,N'Hoạt động')
			insert into NhanVien values(@MaNhanVien,@HoTen,@username)
		end
		if @role=N'Quản trị'
		begin
			declare @MaQT int
			set @MaQT=0
			if (select count(*) from QuanTri)>0
			begin
				set @MaQT=(select max(MaQT) from QuanTri)
			end
			set @MaQT+=1
			insert into TaiKhoan values(@username,@pass,10,N'Hoạt động')
			insert into QuanTri values(@MaQT,@HoTen,@username)
		end
	end try
	begin catch
		print N'Lỗi hệ thống!'
		ROLLBACK TRAN
		select -10
		return
	END CATCH
COMMIT TRAN
select 0
GO

--Xóa quản trị viên/Nhân viên
create --alter
proc QTV_XoaQuanTriVien_NhanVien
	@username varchar(20),
	@role nvarchar(20),
	@Ma int
as
begin tran
	begin try
		if not exists(select* from TaiKhoan where username=@username)
		begin
			print N'Tên đăng nhập không tồn tại'
			rollback tran
			select -1
			return
		end
		if @role=N'Nhân viên'
		begin
			if not exists(select MaNhanVien from NhanVien where MaNhanVien=@Ma)
			begin
				print N'Nhân viên không tồn tại'
				rollback tran
				select -2
				return
			end
			if exists(select MaNhanVien from HopDong where MaNhanVien=@Ma)
			begin
				print N'Khong the xoa, do nhan vien nay phu trach hop dong'
				rollback tran
				select -3
				return
			end
			delete NhanVien where MaNhanVien=@Ma
		end
		if @role=N'Quản trị'
		begin
			if not exists(select MaQT from QuanTri where MaQT=@Ma)
			begin
				print N'Quản trị không tồn tại'
				rollback tran
				select -4
				return
			end
			delete QuanTri where MaQT=@Ma
		end
		delete TaiKhoan where username=@username
	end try
	begin catch
		print N'Lỗi hệ thống!'
		ROLLBACK TRAN
		select -10
		return
	END CATCH
COMMIT TRAN
select 0
GO

--Sửa Quản trị/Nhân viên
create --alter
proc QTV_SuaQuanTriVien_NhanVien
	@username varchar(20),
	@HoTen nvarchar(50),
	@Ma int,
	@pass varchar(20),
	@pass2 varchar(20),
	@role nvarchar(20)
as
begin tran
	begin try
		if @role=''
		begin 
			print N'Role cần sửa bị trống'
			rollback tran
			select -1
			return
		end
		if @username=''
		begin 
			print N'User cần sửa bị trống'
			rollback tran
			select -2
			return
		end
		if not exists(select username from TaiKhoan where username=@username)
		begin
			print N'Tên đăng nhập không tồn tại'
			rollback tran
			select -3
			return
		end
		if @pass='' or @HoTen=''
		begin
			print N'Có trường thông tin bị trống'
			rollback tran
			select -7
			return
		end
		if @pass!=@pass2
		begin
			print N'Pass nhập loại chưa đúng'
			rollback tran
			select -6
			return
		end
		if @role=N'Nhân viên'
		begin
			if not exists(select MaNhanVien from NhanVien where MaNhanVien=@Ma)
			begin
				print N'Nhân viên không tồn tại'
				rollback tran
				select -4
				return
			end
			update NhanVien set HoTen=@HoTen where MaNhanVien=@Ma
		end
		if @role=N'Quản trị'
		begin
			if not exists(select MaQT from QuanTri where MaQT=@Ma)
			begin
				print N'Quản trị không tồn tại'
				rollback tran
				select -5
				return
			end
			update QuanTri set HoTen=@HoTen where MaQT=@Ma
		end
		update TaiKhoan set pass=@pass where username=@username
	end try
	begin catch
		print N'Lỗi hệ thống!'
		ROLLBACK TRAN
		select -10
		return
	END CATCH
COMMIT TRAN
select 0
GO

-----------------------------------------------------------------------------------
------------------------------------ Nhân Viên ------------------------------------
-----------------------------------------------------------------------------------
--NV_LayThongTin
create --alter
proc NV_LayThongTin
	@username varchar(20)
as
begin
	select MaNhanVien,HoTen
	from NhanVien
	where username=@username
end
go

create --alter
proc NV_SuaThongTin
	@username varchar(20),
	@HoTenMoi nvarchar(50)
as
begin tran
	begin try
		if @HoTenMoi=''
		begin
			print N'Tên rỗng'
			rollback tran
			select -1
			return
		end
		update NhanVien
		set HoTen=@HoTenMoi
		where username=@username
	end try
	begin catch
		print N'Lỗi hệ thống!'
		ROLLBACK TRAN
		select -10
		return
	END CATCH
COMMIT TRAN
select 0
GO


--Lấy các hợp đồng đã duyệt
create --alter
proc NV_LayHopDongDaDuyet
as
begin
	select* from HopDong where XetDuyet=N'Đã duyệt'
end
go

--Lấy các hợp đồng chờ duyệt
create --alter
proc NV_LayHopDongChoDuyet
as
begin
	select* from HopDong where XetDuyet=N'Chờ duyệt'
end
go

--Xét duyệt một hợp đồng
create --alter
proc NV_DuyetHopDong
@MaHD int,
@XetDuyet nvarchar(10),
@MaNV int
as
begin tran
	begin try
	if @XetDuyet=N'Đã duyệt'
	begin 
		print N'Hợp đồng đã được duyệt trước'
		rollback tran
		select -1
		return
	end
	update HopDong
	set XetDuyet=N'Đã duyệt',MaNhanVien=@MaNV
	where MaHopDong=@MaHD
	end try
	begin catch
		print N'Lỗi hệ thống!'
		ROLLBACK TRAN
		select -10
		return
	END CATCH
COMMIT TRAN
select 0
GO

--Tìm kiếm hợp đồng theo mã hợp đồng
create --alter
proc NV_TimKiem
	@LoaiMa varchar(10),
	@Ma int
as 
begin tran
	begin try
		if @LoaiMa=''
		begin 
			print N'Chưa chọn loại'
			rollback tran
			select -1
			return
		end
		if @LoaiMa='MaHD'
		begin
			if not exists(select MaHopDong from HopDong where MaHopDong=@Ma)
				begin
				print N'Mã hợp đồng không tồn tại'
				rollback tran
				select -2
				return
			end
			select* from HopDong where MaHopDong=@Ma
		end
		if @LoaiMa='MaDT'
		begin
			if not exists(select MaDoiTac from HopDong where MaDoiTac=@Ma)
			begin
				print N'Mã đối tác không tồn tại'
				rollback tran
				select -3
				return
			end
			select * from HopDong where MaDoiTac=@Ma
		end
	end try
	begin catch
		print N'Lỗi hệ thống!'
		ROLLBACK TRAN
		select -10
		return
	END CATCH
COMMIT TRAN
select 0
GO

create --alter
proc NV_GiaHanHopDong
@MaHD int,
@XetDuyet nvarchar(10),
@ThoiHan int,
@MaNV int
as
begin tran
	begin try
		if @XetDuyet=N'Chờ duyệt'
		begin
			print N'Hợp đồng chưa được duyệt'
			rollback tran
			select -1
			return
		end
		if @ThoiHan<1
		begin
			print N'Thời hạn không hợp lệ'
			rollback tran
			select -2
			return
		end
		update HopDong
		set ThoiHan=@ThoiHan,NgayBatDau=getdate(),MaNhanVien=@MaNV
		where MaHopDong=@MaHD
	end try
	begin catch
		print N'Lỗi hệ thống!'
		ROLLBACK TRAN
		select -10
		return
	END CATCH
COMMIT TRAN
select 0
GO



-----------------------------------------------------------------------------------
------------------------------------- Tài Xế --------------------------------------
-----------------------------------------------------------------------------------
create --alter
proc TX_ThayDoiThongTin
	@MaTX int,
	@HoTen nvarchar(50), 
	@SDT bigint, 
	@MaDiaChi int, 
	@Tinh nvarchar(20), 
	@Quan nvarchar(20), 
	@DCCuThe nvarchar(50),
	@MaNguoiDung  int ,
	@STK_moi bigint,
	@TenNH nvarchar(50),
	@BienSoXe varchar(12), 
	@Email nvarchar(50)
as
begin tran
	begin try
		if @HoTen='' or @SDT='' or @MaDiaChi='' or @Tinh='' or @Quan='' or @DCCuThe=''
		or @MaNguoiDung='' or @STK_moi=''  or @TenNH='' or @BienSoXe='' or @Email=''
		begin 
			print N'Thong tin trong'
			rollback tran
			select -1
			return
		end
		if @STK_moi!=(select STK from TaiKhoanNH where MaNguoiDung=@MaNguoiDung)
		begin 
			if exists(select STK from TaiKhoanNH where MaNguoiDung!=@MaNguoiDung and STK=@STK_moi)
			begin
				print N'STK bi trung voi stk nguoi khac'
				rollback tran
				select -2
				return
			end
			update TaiKhoanNH set STK=@STK_moi,@TenNH=TenNH where MaNguoiDung=@MaNguoiDung
		end
		update DiaChi set Tinh=@Tinh,Quan =@Quan, DCCuThe=@DCCuThe where @MaDiaChi=MaDiaChi
		update TaiXe set HoTen=@HoTen,@SDT=SDT,Email=@Email, BienSoXe=@BienSoXe where MaTaiXe=@MaTX
	end try
	begin catch
		print N'Lỗi hệ thống!'
		ROLLBACK TRAN
		select -10
		return
	END CATCH
COMMIT TRAN
select 0
GO


create --alter
proc TX_LayThongTin
@username varchar(20)
as
begin 
	select *from Taixe where username=@username
end
go


create 
proc TX_LayThongTinDiaChi
@MaDiaChi int
as
begin
	select* from DiaChi where MaDiaChi=@MaDiaChi
end
go

create 
proc TX_LayThongTinTaiKhoan
@MaNguoDung int
as
begin 
	select * from TaiKhoanNH where MaNguoiDung=@MaNguoDung
end
go


/*================Đơn đặt hàng================*/
--
create --alter
proc TX_LayDonChuaGiao
@Tinh nvarchar(20)
as
begin 
	select dh.MaDonHang,dh.GioDat,dh.NgayDat,dh.TongGia,dh.PhiShip,dh.TinhTranggiao,dh.MaDoiTac,dh.MaKhachHang
	from (select * from DonDatHang where TinhTrangGiao=N'Chờ giao') dh, KhachHang kh,
	(select MaDiaChi from DiaChi where Tinh=@Tinh) dc 
	where dh.MaKhachHang=kh.MaKhachHang and kh.MaDiaChi=dc.MaDiaChi
end
go

create --alter
proc TX_LayDonDangGiao
@MaTaiXe int
as
begin 
	select * from
	DonDatHang
	where @MaTaiXe=MaTaiXe and TinhTranggiao=N'Đang giao'
end
go

create --alter
proc TX_LayDonDaGiao
@MaTaiXe int
as
begin 
	select * from
	DonDatHang
	where @MaTaiXe=MaTaiXe and TinhTrangGiao=N'Đã giao'
end
go



create --alter
proc TX_LayChiTietDonHang
@MaDDh int
as
begin
	select t.TenMon,c.Soluong,t.Gia,c.Danhgia
	from ChiTietDonDatHang c, ThucDon t
	where c.MaDonHang=@MaDDh
	and t.MaDoiTac=c.MaDoiTac and t.MaMonAn=c.MaMonAn
end
go

--Tài xế nhânj đơn hàng
create --alter
proc TX_NhanDonHang
@MaDonHang int,
@MaTaiXe int
as
begin
	update DonDatHang
	set TinhTranggiao=N'Đang giao',MaTaiXe=@MaTaiXe
	where MaDonHang=@MaDonHang
end
go


--Tài xế hủy đơn hàng
create --alter
proc TX_HuyDonHang
@MaDonHang int
as
begin
	update DonDatHang
	set TinhTranggiao=N'Chờ giao',MaTaiXe=NULL
	where MaDonHang=@MaDonHang
end
go


--Tài xế hoàn tất đơn hàng
create --alter
proc TX_HoanTatDonHang
@MaDonHang int,
@MaTaiXe int,
@Phiship int,
@MaDoiTac int,
@TongGia int
as
begin tran
	begin try
		if (not exists(select MaDoiTac from DoiTac where MaDoiTac=@MaDoiTac))
		begin
			print N'Đối tác không tônf tại'
			rollback tran
			select -1
			return
		end
		update DonDatHang
		set TinhTranggiao=N'Đã giao'
		where MaDonHang=@MaDonHang
		update TaiKhoanNH
		set SoDu=SoDu+@TongGia
		where MaNguoiDung=(select MaSTK from Doitac where MaDoiTac=@MaDoiTac)
		update TaiKhoanNH
		set SoDu=SoDu+@Phiship
		where MaNguoiDung=(select MaSTK from Taixe where MaTaiXe=@MaTaiXe)
	end try
	begin catch
		print N'Lỗi hệ thống!'
		ROLLBACK TRAN
		select -10
		return
	END CATCH
COMMIT TRAN
select 0
GO

create --alter
proc TX_LayThongTinKhachHang
@MaKhachHang int
as
begin 
	select m.HoTen, dc.DCCuThe,dc.Quan
	from (select HoTen,MaDiaChi from KhachHang where MaKhachHang=@MaKhachHang) m , DiaChi dc
	where m.MaDiaChi=dc.MaDiaChi
end
go




	


