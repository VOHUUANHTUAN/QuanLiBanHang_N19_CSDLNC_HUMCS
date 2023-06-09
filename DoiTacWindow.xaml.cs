﻿
using System;
using System.Collections.Generic;
using System.Data;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using Csdhnc_N19_BanHang.DBClass;


namespace Csdhnc_N19_BanHang
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class DoiTacWindow : Window
    {

        public string MaDoiTac;
        DBConnect db = new DBConnect();
        public string username = "";

        public DoiTacWindow(string username_)
        {
            InitializeComponent();
            username = username_;
            //this.username = username; 
            MaDoiTac = db.sql_select("select MaDoiTac from DOITAC where Username = '" + username + "'").Rows[0][0].ToString();
            if (MaDoiTac == null)
            {
                MessageBox.Show("Username không tồn tại tài khoản Đối Tác");
                this.Close();
            }
        }
        DoiTac doitac = new DoiTac();

        private void Btn_dangxuat_Click_1(object sender, RoutedEventArgs e)
        {
            LoginWindows lg = new LoginWindows(username);
            this.Close();
            lg.Show();
        }

        ///================================================================================================================
        ///================================================================================================================
        ///================================================ BẢNG THÔNG TIN ================================================
        ///================================================================================================================
        ///================================================================================================================
        private void Tt_load_Datagrid()
        {
            try
            {
                DataTable dt = doitac.LayThongTinDoiTac(MaDoiTac);
                DataRow r = dt.Rows[0];
                TT_tb_nguoidaidien.Text = r["Tennguoidaidien"].ToString();
                TT_tb_tenquan.Text = r["TenQuan"].ToString();
                TT_tb_slchinhanh.Text = r["SLChiNhanh"].ToString();
                TT_tb_loaithucpham.Text = r["LoaiAmThuc"].ToString();
                TT_tb_email.Text = r["Email"].ToString();
                TT_tb_sodu.Text = r["SoDu"].ToString();
                TT_tb_masothue.Text = r["MaSoThue"].ToString();
                TT_tb_sotaikhoan.Text = r["STK"].ToString();
                //Tt_lb_thongtin_errorout.Content = "Thông tin user : " + doitac.username;
                //Tt_lb_thongtin_errorout.Background = Brushes.LightGreen;
                Tt_lb_thongtin_errorout.Content = "";
                Tt_lb_thongtin_errorout.Background = Brushes.Transparent;
            }
            catch
            {
                Tt_lb_thongtin_errorout.Content = "Không tìm được thông tin. Lỗi!!!";
                Tt_lb_thongtin_errorout.Background = Brushes.IndianRed;
            }
        }
        private void Tt_ThongTin_loaded(object sender, RoutedEventArgs e)
        {
            Tt_load_Datagrid();
        }
        private void Tt_capnhatthongtincanhan_click(object sender, RoutedEventArgs e)
        {
            //DialogResult t= MessageBox.Show("Bạn có muốn cập nhật thông tin?","Thong bao", MessageBoxButton.YesNo, );
            try
            {
                doitac.TT_capNhatThongTin(MaDoiTac, TT_tb_nguoidaidien.Text, TT_tb_tenquan.Text, TT_tb_email.Text, TT_tb_loaithucpham.Text,TT_tb_masothue.Text);
                //doitac.capNhatThongTin();
                Tt_lb_thongtin_errorout.Content = "Cập nhật thông tin thành công";
                Tt_lb_thongtin_errorout.Background = Brushes.LightGreen;
            }
            catch
            { }
        }
        private void Tt_capnhatmatkhau_click(object sender, RoutedEventArgs e)
        {
            if (TT_tb_matkhaucu.Password != doitac.layMatKhau(username))
            {
                Tt_lb_doimatkhau_errorout.Content = "Mật khẩu cũ không đúng!";
                Tt_lb_doimatkhau_errorout.Background = Brushes.IndianRed;
                //MessageBox.Show("Mật khẩu không đúng!", "Thông báo");
            }
            else
            {
                if (TT_tb_matkhaumoi.Password != TT_tb_matkhaumoi_2.Password)
                {
                    Tt_lb_doimatkhau_errorout.Content = "Mật khẩu nhập lại không đúng!";
                    Tt_lb_doimatkhau_errorout.Background = Brushes.IndianRed;
                    //MessageBox.Show("Mật khẩu nhập lại không đúng!", "Thông báo");
                }
                else
                {

                    doitac.doiMatKhau(TT_tb_matkhaumoi.Password, username);
                    Tt_lb_doimatkhau_errorout.Content = "Đổi mật khẩu thành công";
                    Tt_lb_doimatkhau_errorout.Background = Brushes.LightGreen;
                    //MessageBox.Show("Cập nhật thành công!", "Thông báo");
                }
            }

        }
        private void tt_bt_doimatkhau_loaded(object sender, RoutedEventArgs e)
        {
            TT_tb_taikhoan.Text = username;

        }

        ///================================================================================================================
        ///================================================================================================================
        ///================================================ BẢNG CHI NHANH ================================================
        ///================================================================================================================
        ///================================================================================================================

        //Hàm load datagrid của tab Chi nhánh
        private void HienMaLoi_Label(int kq, string cauLenh, string tenTab)
        {
            if (kq == 0)
            {
                Cn_lb_errorout.Content = "Đã " + cauLenh + " " + tenTab.ToLower() + " thành công";
                Cn_load_Datagrid();
                Cn_lb_errorout.Background = Brushes.LightGreen;
            }
            else
            {
                switch (kq)
                {
                    case 1:
                        Cn_lb_errorout.Content = "Có trường trống";
                        break;
                    case 2:
                        Cn_lb_errorout.Content = "Mã đối tác không tồn tại";
                        break;
                    case 3:
                        Cn_lb_errorout.Content = "Số điện thoại trùng lắp";
                        break;
                    case 4:
                        Cn_lb_errorout.Content = "Tình trạng không hợp lệ";
                        break;
                    case 5:
                        Cn_lb_errorout.Content = "Mã chi nhánh không tồn tại";
                        break;
                    case 10:
                        Cn_lb_errorout.Content = "Lỗi Server SQL";
                        break;
                    case 11:
                        Cn_lb_errorout.Content = "Chọn dòng cần " + cauLenh;
                        break;

                    default:
                        break;
                }
                Cn_lb_errorout.Background = Brushes.IndianRed;
            }
        }
        private void Cn_load_Datagrid()
        {
            try
            {
                DataTable dt = db.sql_select("select * ,(select Tinh from DiaChi where MaDiaChi = cn.MaDiaChi)Tinh, (select Quan from DiaChi where MaDiaChi = cn.MaDiaChi)Quan,(select DCCuThe from DiaChi where MaDiaChi = cn.MaDiaChi)DCCuThe" +
                    " from CHINHANH cn where MaDoiTac ='" + MaDoiTac + "'");
                cn_datagrid.ItemsSource = dt.DefaultView;
            }
            catch
            { }
        }
        private void Cn_btn_them_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                int maLoi = doitac.QueryChiNhanh("Them", "", MaDoiTac.ToString(),
                    Cn_tb_tinh.Text, Cn_tb_quan.Text, Cn_tb_diachi.Text,
                    Cn_tb_sdt.Text, Cn_tb_tinhtrang.Text,Cn_tb_giomocua.Text,Cn_tb_giodongcua.Text);//, Convert.ToDateTime(Cn_tb_ngaylap.Text).ToString("MM-dd-yyyy"));
                HienMaLoi_Label(maLoi, "thêm", "chi nhánh");
                if (maLoi == 0)
                {
                    Cn_tb_sdt.Text = "";
                    Cn_tb_tinhtrang.Text = "";
                    //Cn_tb_ngaylap.Text = "";
                    Cn_tb_tinh.Text = "";
                    Cn_tb_quan.Text = "";
                    Cn_tb_diachi.Text = "";
                    Tt_load_Datagrid();
                }
            }
            catch
            { }
        }
        private void Cn_btn_xoa_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                DataRowView rowview = (DataRowView)cn_datagrid.SelectedItem;
                if (rowview != null)
                {
                    int maLoi = doitac.QueryChiNhanh("Xoa", rowview["MaChiNhanh"].ToString(), MaDoiTac.ToString(),
                            "", "", "",
                            "", "", "","");
                    HienMaLoi_Label(maLoi, "xoá", "chi nhánh");
                    Tt_load_Datagrid();
                }
                else
                {
                    HienMaLoi_Label(11, "xoá", "chi nhánh");
                }
            }
            catch
            {
            }
        }

        private void SL_tb_search_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (!string.IsNullOrEmpty(SL_tb_search.Text) && SL_tb_search.Text.Length > 0)
            {
                SL_lb_search.Visibility = Visibility.Collapsed;
            }
            else
            {
                SL_lb_search.Visibility = Visibility.Visible;
            }

            SL_update_pageNum();
            Sl_load_datagrid_donhang();

        }
            private void Cn_btn_sua_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                DataRowView rowview = (DataRowView)cn_datagrid.SelectedItem;
                if (rowview != null)
                {
                    int maLoi = doitac.QueryChiNhanh("Sua", rowview["MaChiNhanh"].ToString(), MaDoiTac.ToString(),
                        Cn_tb_tinh.Text.ToString(), Cn_tb_quan.Text.ToString(), Cn_tb_diachi.Text.ToString(),
                        Cn_tb_sdt.Text.ToString(), Cn_tb_tinhtrang.Text.ToString(), Cn_tb_giomocua.Text, Cn_tb_giodongcua.Text);//, Convert.ToDateTime(Cn_tb_ngaylap.Text).ToString("MM-dd-yyyy"));
                    HienMaLoi_Label(maLoi, "sửa", "chi nhánh");
                }
                else
                {
                    HienMaLoi_Label(11, "sửa", "chi nhánh");
                }

            }
            catch
            {
            }
        }


        //Khi datagrid Chi Nhánh load lần đầu
        private void Cn_datagrid_Loaded(object sender, RoutedEventArgs e)
        {
            Cn_load_Datagrid();
        }
        //Khi nhấn vào 1 dòng của bảng ChiNhanh
        private void Cn_datagrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            try
            {
                if (cn_datagrid.SelectedIndex.ToString() != null)
                {
                    DataRowView rowview = (DataRowView)cn_datagrid.SelectedItem;
                    if (rowview != null)
                    {
                        Cn_tb_sdt.Text = rowview["SDT"].ToString();
                        Cn_tb_tinhtrang.Text = rowview["TinhTrang"].ToString();
                        //Cn_tb_ngaylap.Text = rowview["NgayLap"].ToString();
                        Cn_tb_tinh.Text = rowview["Tinh"].ToString();
                        Cn_tb_quan.Text = rowview["Quan"].ToString();
                        Cn_tb_diachi.Text = rowview["DCCuThe"].ToString();
                        Cn_tb_giomocua.Text = Convert.ToDateTime(rowview["GioMoCua"].ToString()).ToShortTimeString();
                        Cn_tb_giodongcua.Text = Convert.ToDateTime(rowview["GioDongCua"].ToString()).ToShortTimeString();
                    }
                }
            }
            catch
            {

            }
        }

        ///================================================================================================================
        ///================================================================================================================
        ///================================================ BẢNG THỰC PHẨM ================================================
        ///================================================================================================================
        ///================================================================================================================
        private void Tp_HienMaLoi_Label(int kq, string cauLenh)
        {
            if (kq == 0)
            {
                Tp_lb_errorout.Content = "Đã " + cauLenh + " thực phẩm thành công";
                Tp_load_Datagrid();
                Tp_lb_errorout.Background = Brushes.LightGreen;
            }
            else
            {
                switch (kq)
                {
                    case 1:
                        Tp_lb_errorout.Content = "Có trường trống";
                        break;
                    case 2:
                        Tp_lb_errorout.Content = "Mã đối tác không tồn tại";
                        break;
                    case 3:
                        Tp_lb_errorout.Content = "Tên thực phẩm này đã tồn tại";
                        break;
                    case 4:
                        Tp_lb_errorout.Content = "Tình trạng không hợp lệ";
                        break;
                    case 5:
                        Tp_lb_errorout.Content = "Không thể xoá, món ăn đã từng được lên đơn";
                        break;
                    case 6:
                        Tp_lb_errorout.Content = "Không thể xoá, món ăn không tồn tại";
                        break;
                    case 7:
                        Tp_lb_errorout.Content = "Giá không hợp lệ";
                        break;
                    case 10:
                        Tp_lb_errorout.Content = "Lỗi Server SQL";
                        break;
                    case 11:
                        Tp_lb_errorout.Content = "Chọn dòng cần " + cauLenh;
                        break;

                    default:
                        break;
                }
                Tp_lb_errorout.Background = Brushes.IndianRed;
            }
        }
        private void Tp_load_Datagrid()
        {
            DataTable dt = db.sql_select("select * from ThucDon where MaDoiTac =" + MaDoiTac);
            Tp_datagrid.ItemsSource = dt.DefaultView;
        }

        private void Tp_btn_them_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                int maLoi = doitac.QueryThucPham("Them", "", MaDoiTac.ToString(),
                    Tp_tb_tenmon.Text.ToString(), Tp_tb_mieuta.Text.ToString(), Tp_tb_gia.Text.ToString(), Tp_tb_tinhtrang.Text.ToString(),
                    Tp_tb_tuychon.Text.ToString());
                Tp_HienMaLoi_Label(maLoi, "thêm");
                if (maLoi == 0)
                {
                    Tp_tb_matp.Text = "";
                    Tp_tb_mieuta.Text = "";
                    Tp_tb_tenmon.Text = "";
                    Tp_tb_tinhtrang.Text = "";
                    Tp_tb_gia.Text = "";
                    Tp_tb_tuychon.Text = "";
                }
            }
            catch
            { }
        }
        private void Tp_btn_sua_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                DataRowView rowview = (DataRowView)Tp_datagrid.SelectedItem;
                if (rowview != null)
                {
                    int maLoi = doitac.QueryThucPham("Sua", Tp_tb_matp.Text, MaDoiTac.ToString(),
                        Tp_tb_tenmon.Text.ToString(), Tp_tb_mieuta.Text.ToString(), Tp_tb_gia.Text.ToString(), 
                        Tp_tb_tinhtrang.Text.ToString(), Tp_tb_tuychon.Text.ToString());
                    Tp_HienMaLoi_Label(maLoi, "sửa");
                }
                else
                {
                    Tp_HienMaLoi_Label(11, "sửa");
                }
            }
            catch
            {
            }
        }
        private void Tp_btn_xoa_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                DataRowView rowview = (DataRowView)Tp_datagrid.SelectedItem;

                if (rowview != null)
                {
                    int maLoi = doitac.QueryThucPham("Xoa", rowview["MaMonAn"].ToString(), MaDoiTac.ToString(),
                            "", "", "",
                            "", "");
                    Tp_HienMaLoi_Label(maLoi, "xoá");
                }
                else
                {
                    Tp_HienMaLoi_Label(11, "xoá");
                }
            }
            catch
            {
            }
        }

        private void Tp_datagrid_Loaded(object sender, RoutedEventArgs e)
        {
            try
            {
                Tp_load_Datagrid();
            }
            catch
            {

            }
        }
        private void TP_datagrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            try
            {
                if (Tp_datagrid.SelectedIndex.ToString() != null)
                {
                    DataRowView rowview = (DataRowView)Tp_datagrid.SelectedItem;
                    if (rowview != null)
                    {
                        Tp_tb_matp.Text = rowview["MaMonAn"].ToString();
                        Tp_tb_tenmon.Text = rowview["TenMon"].ToString();
                        Tp_tb_gia.Text = rowview["Gia"].ToString();
                        Tp_tb_mieuta.Text = rowview["MieuTa"].ToString();
                        Tp_tb_tinhtrang.Text = rowview["TinhTrang"].ToString();
                        Tp_tb_tuychon.Text = rowview["TuyChon"].ToString();
                    }
                }
            }
            catch
            {
            }
        }


        ///================================================================================================================
        ///================================================================================================================
        ///================================================ BẢNG ĐƠN HÀNG =================================================
        ///================================================================================================================
        ///================================================================================================================

        private void Dh_load_Datagrid()
        {
            try
            {
                DataTable dt = db.sql_select("select *, " +
                    "(select k.HoTen from KHACHHANG k where k.MaKhachHang = dh_cho.MaKhachHang) TenKH," +
                    "(select t.HoTen from TAIXE t where t.MaTaiXe = dh_cho.MaTaiXe) TenTX " +
                    "from(select * from DONDATHANG where TinhTrangGiao = N'Chờ xử lí') dh_cho  " +
                    "where MaDonHang in (select distinct MaDonHang from CHITIETDONDATHANG where MaDoiTac = " + MaDoiTac+")");
                Dh_datagrid.ItemsSource = dt.DefaultView;
            }
            catch
            {

            }
        }
        private void Dh_load_datagrid_Chitietdon(string MaDonHang)
        {
            try
            {
                DataTable dt = db.sql_select("select* ," +
                    "(select tp.TenMon from ThucDon tp where tp.MaMonAn = ct.MaMonAn and tp.MaDoiTac= '" + MaDoiTac + "') TenMon," +
                    "(select tp.Gia from ThucDon tp where tp.MaMonAn = ct.MaMonAn and tp.MaDoiTac= '" + MaDoiTac + "') Gia " +
                    "from CHITIETDONDATHANG ct where ct.MaDonHang = '" + MaDonHang + "'");
                Dh_datagrid_Chitietdon.ItemsSource = dt.DefaultView;
            }
            catch
            {

            }
        }

        private void Dh_btn_nhandon_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                DataRowView rowview = (DataRowView)Dh_datagrid.SelectedItem;
                if (rowview != null)
                {
                    string query = " begin try " +
                    "update DONDATHANG set TinhTrangGiao = N'Chờ giao' where MaDonHang='" + Dh_tb_madon.Text + "' " +
                    "select 0 end try " +
                    "begin catch select 1 end catch";
                    int kq = (int)db.sql_select(query).Rows[0][0];
                    if (kq == 0)
                    {
                        Dh_lb_errorout.Content = "Nhận đơn thành công";
                        Dh_lb_errorout.Background = Brushes.LightGreen;
                        ///Refresh bảng
                        Dh_load_Datagrid();
                        ///Refresh textbox
                        Dh_tb_madon.Text = "";
                        Dh_tb_mataixe.Text = "";
                        Dh_tb_khachhang.Text = "";
                        return;
                    }
                    else
                    {
                        Dh_lb_errorout.Content = "Lỗi khi kết nốt SQL";
                        Dh_lb_errorout.Background = Brushes.IndianRed; ;
                    }
                }
                else
                {
                    Dh_lb_errorout.Content = "Chọn đơn hàng để nhận";
                    Dh_lb_errorout.Background = Brushes.IndianRed; ;
                }

            }
            catch
            { }
        }
        private void Dh_btn_huydon_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                DataRowView rowview = (DataRowView)Dh_datagrid.SelectedItem;
                if (rowview != null)
                {
                    string query = " begin try " +
                    "update DONDATHANG set TinhTrangGiao = N'Đã hủy' where MaDonHang='" + Dh_tb_madon.Text + "' " +
                    "select 0 end try " +
                    "begin catch select 1 end catch";
                    int kq = (int)db.sql_select(query).Rows[0][0];
                    if (kq == 0)
                    {
                        Dh_lb_errorout.Content = "Huỷ đơn thành công";
                        Dh_lb_errorout.Background = Brushes.LightGreen;
                        ///Refresh bảng
                        Dh_load_Datagrid();

                        ///Refresh textbox
                        Dh_tb_madon.Text = "";
                        Dh_tb_mataixe.Text = "";
                        Dh_tb_khachhang.Text = "";
                    }
                }
                else
                {
                    Dh_lb_errorout.Content = "Chọn đơn hàng để huỷ";
                    Dh_lb_errorout.Background = Brushes.IndianRed; ;
                }
            }
            catch
            { }
        }
        private void Dh_datagrid_Loaded(object sender, RoutedEventArgs e)
        {
        }

        private void Dh_datagrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            try
            {
                if (Dh_datagrid.SelectedIndex.ToString() != null)
                {
                    DataRowView rowview = (DataRowView)Dh_datagrid.SelectedItem;
                    if (rowview != null)
                    {
                        Dh_tb_madon.Text = rowview["MaDonHang"].ToString();
                        Dh_tb_mataixe.Text = rowview["MaTaiXe"].ToString();
                        Dh_tb_khachhang.Text = rowview["MaKhachHang"].ToString();
                        Dh_load_datagrid_Chitietdon(rowview["MaDonHang"].ToString());
                    }
                }
            }
            catch
            {

            }
        }


        ///================================================================================================================
        ///================================================================================================================
        ///================================================ BẢNG HỢP ĐỒNG =================================================
        ///================================================================================================================
        ///================================================================================================================
        private void Hd_HienMaLoi_Label(int kq, string cauLenh)
        {
            if (kq < 0)
            {
                Hd_lb_errorout.Content = "Đã " + cauLenh + " hợp đồng thành công";
                Hd_load_datagrid();
                Hd_lb_errorout.Background = Brushes.LightGreen;
            }
            else
            {
                switch (kq)
                {
                    case 1:
                        Hd_lb_errorout.Content = "Có trường trống";
                        break;
                    case 2:
                        Hd_lb_errorout.Content = "Mã đối tác không tồn tại";
                        break;
                    case 3:
                        Hd_lb_errorout.Content = "Ngày hết hạn phải sau ngày ký";
                        break;

                    default:
                        break;
                }
                Hd_lb_errorout.Background = Brushes.IndianRed;
            }
        }
        private void Hd_load_datagrid()
        {
            try
            {
                DataTable dt = db.sql_select("select *,(select Tennguoidaidien from DOITAC where MaDoiTac = '" + MaDoiTac + "')NguoiDaiDien  from HopDong where MaDoiTac ='" + MaDoiTac + "'");
                Hd_datagrid.ItemsSource = dt.DefaultView;
            }
            catch
            { }
        }
        //Hàm sẽ load danh sách chi nhánh null vào datagrid_chinhanh
        private void Hd_load_datagrid_chinhanh_null()
        {
            try
            {
                DataTable dt = db.sql_select("exec Dt_DsChiNhanhNull '" + MaDoiTac + "'");
                Hd_datagrid_chinhanh.ItemsSource = dt.DefaultView;
            }
            catch
            { }

        }
        //Hàm sẽ load danh sách chi nhánh của DÒNG ĐANG ĐƯỢC CHỌN vào datagrid_chinhanh
        private void Hd_load_datagrid_chinhanh(string MaHopDong)
        {
            try
            {
                DataTable dt = db.sql_select("exec Dt_DsChiNhanh '" + MaHopDong + "'");
                Hd_datagrid_chinhanh.ItemsSource = dt.DefaultView;
            }
            catch
            { }

        }

        // Khi nhấn nút thêm hợp đồng
        private void Hd_btn_them_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                Convert.ToDateTime(Hd_tb_ngayki.Text).ToString("MM-dd-yyyy");

                int maLoi = doitac.QueryHopDong(
                    Hd_LayChiNhanhDaChon().Count.ToString(),
                    Convert.ToDateTime(Hd_tb_ngayki.Text).ToString("MM-dd-yyyy"),
                    Hd_tb_thoihan.Text, MaDoiTac);
                Hd_HienMaLoi_Label(maLoi, "thêm");

                if (maLoi < 0)
                {
                    var DsChiNhanh = Hd_LayChiNhanhDaChon();
                    foreach (string stt in DsChiNhanh)
                    {
                        Hd_capNhatTinhTrangChiNhanh(stt, (maLoi * -1).ToString());
                    }
                    Hd_load_datagrid();
                    Hd_load_datagrid_chinhanh_null();

                }

            }
            catch
            {
                Hd_lb_errorout.Content = "Có trường trống";
                Hd_lb_errorout.Background = Brushes.IndianRed;
            }

        }
        //Khi nhấn nút reload trong tab Hợp đồng
        private void Hd_btn_reload_Click(object sender, RoutedEventArgs e)
        {
            Hd_datagrid_chinhanh.IsReadOnly = false;
            Hd_load_datagrid_chinhanh_null();
        }

        /// Khi chọn 1 dòng trong bảng Hd_chinhanh thì tự update số lượng chi nhánh đã chọn        
        //private void Hd_datagrid_chinhanh_GotFocus(object sender, RoutedEventArgs e)
        //{
        //    List<string> x = Hd_LayChiNhanhDaChon();
        //    Hd_tb_slchinhanh.Text = x.Count.ToString();
        //}

        private void Hd_datagrid_chinhanh_SelectedCellsChanged(object sender, SelectedCellsChangedEventArgs e)
        {
            List<string> x = Hd_LayChiNhanhDaChon();
            Hd_tb_slchinhanh.Text = x.Count.ToString();
        }
        private void Hd_datagrid_chinhanh_MouseDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            List<string> x = Hd_LayChiNhanhDaChon();
            Hd_tb_slchinhanh.Text = x.Count.ToString();
        }
        //private void Hd_datagrid_chinhanh_MouseDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
        //{
        //    List<string> x = Hd_LayChiNhanhDaChon();
        //    Hd_tb_slchinhanh.Text = x.Count.ToString();
        //}
        private void Hd_datagrid_chinhanh_Loaded(object sender, RoutedEventArgs e)
        {
            Hd_load_datagrid_chinhanh_null();
        }
        // tự load datagrid chính khi mở tab Hợp Đồng
        private void Hd_datagrid_Loaded(object sender, RoutedEventArgs e)
        {
            Hd_load_datagrid();
        }

        private void Hd_datagrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            try
            {
                if (Hd_datagrid.SelectedIndex.ToString() != null)
                {
                    DataRowView rowview = (DataRowView)Hd_datagrid.SelectedItem;
                    if (rowview != null)
                    {
                        Hd_datagrid_chinhanh.IsReadOnly = true;

                        Hd_load_datagrid_chinhanh(rowview["MaHopDong"].ToString());
                        Hd_tb_slchinhanh.Text = rowview["SoChiNhanh"].ToString();
                        //Hd_tb_sotaikhoan.Text = rowview["SoTaiKhoan"].ToString();
                        //Hd_tb_nganhang.Text = rowview["TenNganHang"].ToString();
                        //Hd_tb_chinhanhnganhang.Text = rowview["CNNganHang"].ToString();
                        //Hd_tb_masothue.Text = rowview["MaSoThue"].ToString();
                        Hd_tb_tinhtrang.Text = rowview["XetDuyet"].ToString();
                        Hd_tb_thoihan.Text = rowview["ThoiHan"].ToString();
                        Hd_tb_nvduyet.Text = rowview["MaNhanVien"].ToString();
                        Hd_tb_ngayki.Text = rowview["NgayBatDau"].ToString();
                        //Hd_tb_ngayhethan.Text = rowview["NgayHetHan"].ToString();
                        Hd_tb_nguoidaidien.Text = rowview["NguoiDaiDien"].ToString();
                    }
                }
            }
            catch
            { }
        }

        /// Lấy STT của các chi nhánh đã chọn trong datagrid Tab Hợp Đồng 
        List<string> Hd_LayChiNhanhDaChon()
        {
            /// Lấy list index dòng đã chọn
            var SelectedList = new List<string>();
            for (int i = 0; i < Hd_datagrid_chinhanh.Items.Count; i++)
            {
                var item = Hd_datagrid_chinhanh.Items[i];
                var mycheckbox = Hd_datagrid_chinhanh.Columns[2].GetCellContent(item) as CheckBox;
                if ((bool)mycheckbox.IsChecked)
                {
                    TextBlock x = Hd_datagrid_chinhanh.Columns[0].GetCellContent(Hd_datagrid_chinhanh.Items[i]) as TextBlock;
                    SelectedList.Add(x.Text);
                }
            }
            return SelectedList;
        }
        private void Hd_capNhatTinhTrangChiNhanh(string sttChiNhanh, string MaHopDong)
        {
            try
            {
                DataTable dt = db.sql_select("update CHINHANH set MaHopDong ='" + MaHopDong + "' where MaDoiTac ='" + MaDoiTac + "' and MaChiNhanh = '" + sttChiNhanh + "'");
            }
            catch
            { }
        }


        ///================================================================================================================
        ///================================================================================================================
        ///================================================ BẢNG SỐ LIỆU ==================================================
        ///================================================================================================================
        ///================================================================================================================
        private int SL_HienMaLoi_Label(string kq, string kieu)
        {
            switch (kq)
            {
                //case "-1":
                //    Sl_lb_errorout.Content = "Có trường trống";
                //    Sl_lb_errorout.Background = Brushes.IndianRed;
                //    return 1;
                //    break;
                //case "-2":
                //    Sl_lb_errorout.Content = "Mã đối tác không có thực phẩm ";
                //    Sl_lb_errorout.Background = Brushes.IndianRed;
                //    return 1;
                //    break;
                //case "-10":
                //    Sl_lb_errorout.Content = "Lỗi Server SQL";
                //    Sl_lb_errorout.Background = Brushes.IndianRed;
                //    return 1;
                //    break;

                default:
                    //Sl_lb_errorout.Content = "Danh sách đơn hàng theo " + kieu;
                    //Sl_lb_errorout.Background = Brushes.LightGreen;
                    return 0;
            }
        }
        private void SL_tb_donhang_curpage_TextChanged(object sender, TextChangedEventArgs e)
        {
            //Giữ số trang luôn đúng
            try
            {
                if (Int32.Parse(SL_tb_donhang_curpage.Text) > Int32.Parse(SL_donhang_totalpage.Content.ToString()))
                {
                    SL_tb_donhang_curpage.Text = "1"; //SL_donhang_totalpage.Content.ToString();
                }
                if (SL_tb_donhang_curpage.Text == "0")
                    SL_tb_donhang_curpage.Text = "1";


            }
            catch
            {
                SL_tb_donhang_curpage.Text = "1";
            }

            //Load trang với số trang tương ứng
            try
            {
                Sl_load_datagrid_donhang();
            }
            catch { }
        }
        private void SL_load_rating_Datagrid()
        {
            try
            {
                DataTable dt = db.sql_select("Exec dt_XuHuongBan '" + MaDoiTac + "'");
                Sl_rating_datagrid.ItemsSource = dt.DefaultView;
            }
            catch
            { }
        }

        private void Sl_rating_datagrid_Loaded(object sender, RoutedEventArgs e)
        {
        }
        private void Sl_datagrid_Loaded(object sender, RoutedEventArgs e)
        {


        }
        private void Sl_datagridLoaded(string kieu)
        {
            DataTable dt = doitac.laySoLuongDon(kieu, MaDoiTac);
            string hienthi = dt.Rows[0][0].ToString();
            int kq = SL_HienMaLoi_Label(hienthi, kieu);
            if (kq == 0)
                Sl_datagrid.ItemsSource = dt.DefaultView;
        }

        private void Sl_rb_ngay_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                Sl_datagridLoaded("Ngay");
                SL_SL_datagrid_thang.Width = 80;
                SL_SL_datagrid_ngay.Width = 80;
            }
            catch
            {

            }

        }

        private void Sl_rb_thang_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                Sl_datagridLoaded("Thang");
                SL_SL_datagrid_thang.Width = 80;
                SL_SL_datagrid_ngay.Width = 0;

            }
            catch
            {

            }
        }

        private void Sl_rb_nam_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                Sl_datagridLoaded("Nam");
                SL_SL_datagrid_thang.Width = 0;
                SL_SL_datagrid_ngay.Width = 0;
            }
            catch
            {

            }
        }

        private void TabItem_Loaded(object sender, RoutedEventArgs e)
        {

        }

        private void Hd_TabItem_Loaded(object sender, RoutedEventArgs e)
        {
            try
            {
                DataTable dt = doitac.LayThongTinDoiTac(MaDoiTac);
                DataRow r = dt.Rows[0];
                Hd_tb_nguoidaidien.Text = r["Tennguoidaidien"].ToString();
                Hd_tb_nganhang.Text = r["TenNganHang"].ToString();
                Hd_tb_masothue.Text = r["MaSoThue"].ToString();
                Hd_tb_sotaikhoan.Text = r["STK"].ToString();
                //Tt_lb_thongtin_errorout.Content = "Thông tin user : " + doitac.username;
                //Tt_lb_thongtin_errorout.Background = Brushes.LightGreen;
                Tt_lb_thongtin_errorout.Content = "";
                Tt_lb_thongtin_errorout.Background = Brushes.Transparent;
            }
            catch
            {
                Tt_lb_thongtin_errorout.Content = "Không tìm được thông tin. Lỗi!!!";
                Tt_lb_thongtin_errorout.Background = Brushes.IndianRed;
            }
        }


        private void SL_TabItem_MouseDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
        }


        private void Sl_load_datagrid_donhang()
        {
            try
            {
                int numpage = Int32.Parse(SL_tb_donhang_curpage.Text);
                int perpage = Int32.Parse(SL_tb_donhang_rowperpage.Text);
                Sl_datagrid_donhang.ItemsSource = db.sql_select("exec " +
                    "dt_danhsachdonhang N'" +
                    SL_tb_search.Text + "','" +
                    MaDoiTac + "','" +
                    (numpage - 1) * perpage + "','" + (numpage) * perpage + "'").DefaultView;
            }
            catch { }
        }
        private void Sl_load_datagrid_donhang_Chitietdon(string MaDonHang)
        {
            try
            {
                DataTable dt = db.sql_select("select* ," +
                    "(select tp.TenMon from ThucDon tp where tp.MaMonAn = ct.MaMonAn and tp.MaDoiTac= '" + MaDoiTac + "') TenMon," +
                    "(select tp.Gia from ThucDon tp where tp.MaMonAn = ct.MaMonAn and tp.MaDoiTac= '" + MaDoiTac + "') Gia " +
                    "from CHITIETDONDATHANG ct where ct.MaDonHang = '" + MaDonHang + "'");
                Sl_datagrid_donhang_Chitietdon.ItemsSource = dt.DefaultView;
            }
            catch
            {

            }
        }
        private void Sl_datagrid_donhang_Loaded(object sender, RoutedEventArgs e)
        {
            SL_update_pageNum();
            Sl_load_datagrid_donhang();
        }

        private void Sl_datagrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void Sl_datagrid_donhang_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            try
            {
                if (Sl_datagrid_donhang.SelectedIndex.ToString() != null)
                {
                    DataRowView rowview = (DataRowView)Sl_datagrid_donhang.SelectedItem;
                    if (rowview != null)
                    {
                        Sl_load_datagrid_donhang_Chitietdon(rowview["MaDonHang"].ToString());
                    }
                }
            }
            catch
            {

            }
        }

        private void TabItem_MouseDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {

        }

        private void TabItem_MouseDown_1(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {

        }

        private void SL_tuongtacthucpham_MouseDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            SL_load_rating_Datagrid();
        }

        private void Dh_tabitem_mousedown(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            Dh_load_Datagrid();
        }

        private void dh_bt_doitac_prevpage_Click(object sender, RoutedEventArgs e)
        {

        }


        private void dh_bt_doitac_nextpage_Click(object sender, RoutedEventArgs e)
        {

        }

        private void SL_bt_donhang_prevpage_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (SL_tb_donhang_curpage.Text == "" || SL_tb_donhang_curpage.Text == "1")
                {
                    SL_tb_donhang_curpage.Text = "1";
                }
                else
                {
                    SL_tb_donhang_curpage.Text = (Int32.Parse(SL_tb_donhang_curpage.Text) - 1).ToString();
                }
            }
            catch
            {
                SL_tb_donhang_curpage.Text = "1";
            }
        }

        private void SL_bt_donhang_nextpage_Click(object sender, RoutedEventArgs e)
        {

            try
            {
                if (Int32.Parse(SL_tb_donhang_curpage.Text) == Int32.Parse(SL_donhang_totalpage.Content.ToString()))
                {
                    SL_tb_donhang_curpage.Text = SL_donhang_totalpage.Content.ToString();
                }
                else
                {
                    SL_tb_donhang_curpage.Text = (Int32.Parse(SL_tb_donhang_curpage.Text) + 1).ToString();
                }
            }
            catch
            {
                SL_tb_donhang_curpage.Text = "1";
            }
        }

        private void SL_tb_donhang_rowperpage_TextChanged(object sender, TextChangedEventArgs e)
        {
            //Giữ số trang luôn đúng
            try
            {
                if (Int32.Parse(SL_tb_donhang_rowperpage.Text) < 10)
                    SL_tb_donhang_rowperpage.Text = "10";
            }
            catch
            {
                SL_tb_donhang_rowperpage.Text = "10";
            }
            SL_update_pageNum();
            if (Int32.Parse(SL_tb_donhang_curpage.Text) > Int32.Parse(SL_donhang_totalpage.Content.ToString()))
                SL_tb_donhang_curpage.Text = SL_donhang_totalpage.Content.ToString();
            Sl_load_datagrid_donhang(); 
        }
        private void SL_update_pageNum()
        {
            try
            {
                string TotalRow = db.sql_select("exec dt_danhsachdonhang_dem N'" +
                    SL_tb_search.Text + "','" +
                    MaDoiTac+ "','',''").Rows[0][0].ToString();
                int a = (int)Math.Ceiling(1.0 * Int32.Parse(TotalRow) / Int32.Parse(SL_tb_donhang_rowperpage.Text));
                SL_donhang_totalpage.Content = a.ToString();
            }
            catch { }
        }

    }
}