using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using Csdhnc_N19_BanHang.DBClass;

namespace Csdhnc_N19_BanHang
{
    /// <summary>
    /// Interaction logic for TaiXe_Window.xaml
    /// </summary>
    /// 
    
    public partial class TaiXe_Window : Window
    {
        public string username;
        DBConnect db = new DBConnect();
        public string maTaiXe;
        public string maNguoiDung;
        public string maDiaChi;
        public string tinh;

        public TaiXe_Window(string user)
        {
            InitializeComponent();
            username = user;
        }
        private void Tt_getThongTin()
        {
            try
            {
                string query = "Exec TX_LayThongTin '" + username + "'";
                DataTable dt = db.sql_select(query);
                Tt_tb_mataixe.Text = dt.Rows[0]["MaTaiXe"].ToString();
                Tt_tb_hoten.Text = dt.Rows[0]["HoTen"].ToString();
                Tt_tb_email.Text = dt.Rows[0]["Email"].ToString();
                Tt_tb_cmnd.Text= dt.Rows[0]["CMND"].ToString();
                Tt_tb_sdt.Text = dt.Rows[0]["SDT"].ToString();
                Tt_tb_biensoxe.Text = dt.Rows[0]["BienSoXe"].ToString();
                string query1= "Exec TX_LayThongTinDiaChi '" + dt.Rows[0]["MaDiaChi"].ToString() + "'";
                DataTable dt1 = db.sql_select(query1);
                Tt_tb_tinh.Text = dt1.Rows[0]["Tinh"].ToString();
                Tt_tb_quan.Text = dt1.Rows[0]["Quan"].ToString();
                Tt_tb_diachi.Text = dt1.Rows[0]["DCCuThe"].ToString();
                string query2 = "Exec TX_LayThongTinTaiKhoan '" + dt.Rows[0]["MaSTK"].ToString() + "'";
                DataTable dt2 = db.sql_select(query2);
                Tt_tb_sotaikhoan.Text = dt2.Rows[0]["STK"].ToString();
                Tt_tb_nganhang.Text = dt2.Rows[0]["TenNH"].ToString();
                Tt_tb_sodu.Text = dt2.Rows[0]["SoDu"].ToString();

                maTaiXe = dt.Rows[0]["MaTaiXe"].ToString();
                maNguoiDung= dt.Rows[0]["MaSTK"].ToString();
                maDiaChi = dt.Rows[0]["MaDiaChi"].ToString();


            }
            catch
            {
                Tt_lb_thongtincanhan_errorout.Content = "Không tìm được thông tin!!!";
                Tt_lb_thongtincanhan_errorout.Background = Brushes.IndianRed;
            }


    
        }
        private void Tt_capnhatmatkhau_click(object sender, RoutedEventArgs e)
        {
            try
            {
                string query = "Exec DoiMatKhau '" + username + "','" + TT_tb_matkhaucu.Password + "','" + TT_tb_matkhaumoi.Password + "' ,'" + TT_tb_matkhaumoi_2.Password + "'";
                DataTable dt = db.sql_select(query);
                string loi = dt.Rows[0][0].ToString();
                if (loi == "-1")
                {

                    Tt_lb_doimatkhau_errorout.Content = "Sai mật khẩu cũ!!!";
                    Tt_lb_doimatkhau_errorout.Background = Brushes.IndianRed;
                }
                else if (loi == "-2")
                {
                    Tt_lb_doimatkhau_errorout.Content = "Mật khẩu mới bị trống!!!";
                    Tt_lb_doimatkhau_errorout.Background = Brushes.IndianRed;
                }
                else if (loi == "-3")
                {
                    Tt_lb_doimatkhau_errorout.Content = "Mật khẩu nhập lại không đúng!!!";
                    Tt_lb_doimatkhau_errorout.Background = Brushes.IndianRed;
                }
                else if (loi == "-10")
                {
                    Tt_lb_doimatkhau_errorout.Content = "Lỗi giao tác!!!";
                    Tt_lb_doimatkhau_errorout.Background = Brushes.IndianRed;
                }
                else
                {
                    Tt_lb_doimatkhau_errorout.Content = "Cập nhật thành công.";
                    Tt_lb_doimatkhau_errorout.Background = Brushes.LightGreen;
                }
            }
            catch
            {

            }

        }

        private void Btn_dangxuat_Click_1(object sender, RoutedEventArgs e)
        {
            LoginWindows lg = new LoginWindows(username);
            this.Close();
            lg.Show();
        }

        private void Tt_loaded(object sender, RoutedEventArgs e)
        {
        }

        private void Tt_doimatkhau_loaded(object sender, RoutedEventArgs e)
        {
            TT_tb_taikhoan.Text = username;
        }

        private void Tt_capnhatthongtin_click(object sender, RoutedEventArgs e)
        {
            try
            {
                string query = "Exec TX_ThayDoiThongTin '" + maTaiXe + "',N'" + Tt_tb_hoten.Text + "','" + Tt_tb_sdt.Text + "','" + maDiaChi + "',N'" + Tt_tb_tinh.Text + "',N'" + Tt_tb_quan.Text + "'," +
                "N'" + Tt_tb_diachi.Text + "','" + maNguoiDung + "','" + Tt_tb_sotaikhoan.Text + "',N'" + Tt_tb_nganhang.Text + "','" + Tt_tb_biensoxe.Text + "',N'" + Tt_tb_email.Text + "'";
                DataTable dt = db.sql_select(query);

                string loi = dt.Rows[0][0].ToString();
                if (loi == "-1")
                {
                    Tt_lb_thongtincanhan_errorout.Content = "Có thông tin bị trống!!!";
                    Tt_lb_thongtincanhan_errorout.Background = Brushes.IndianRed;
                }
                else if (loi == "-2")
                {
                    Tt_lb_thongtincanhan_errorout.Content = "Số tài khoản bị trùng với người khác!!!";
                    Tt_lb_thongtincanhan_errorout.Background = Brushes.IndianRed;
                }
                else if (loi == "-10")
                {
                    Tt_lb_thongtincanhan_errorout.Content = "Lỗi hệ thống";
                    Tt_lb_thongtincanhan_errorout.Background = Brushes.IndianRed;
                }
                else
                {
                    Tt_lb_thongtincanhan_errorout.Content = "Sửa thành công";
                    Tt_lb_thongtincanhan_errorout.Background = Brushes.LightGreen;
                }

            }
            catch { }
        }

        private void Dh_cb_loaidon_changed(object sender, SelectionChangedEventArgs e)
        {
            try
            {
                ComboBoxItem comboBoxItem = (ComboBoxItem)e.AddedItems[0];
                string loai = comboBoxItem.Content.ToString();
                if (loai == "Đơn chờ giao")
                {
                    string q = "select Tinh from DiaChi where MaDiaChi='" + maDiaChi + "'";
             
                    DataTable d = db.sql_select(q);
                    tinh = d.Rows[0][0].ToString();
                    string query = "Exec TX_LayDonChuaGiao N'"+tinh+"'";
                    DataTable dt = db.sql_select(query);
                    Dh_datagrid.ItemsSource = dt.DefaultView;
                    Dh_bt_nhandon.Visibility = Visibility.Visible;
                    Dh_bt_huydon.Visibility = Visibility.Hidden;
                    Dh_bt_hoantat.Visibility = Visibility.Hidden;

                }
                else if (loai == "Đơn đang giao")
                {
                    string query = "Exec TX_LayDonDangGiao '" + maTaiXe + "'";
                    DataTable dt = db.sql_select(query);
                    Dh_datagrid.ItemsSource = dt.DefaultView;
                    Dh_bt_nhandon.Visibility = Visibility.Hidden;
                    Dh_bt_huydon.Visibility = Visibility.Visible;
                    Dh_bt_hoantat.Visibility = Visibility.Visible;
                }
                else if (loai == "Đơn đã giao")
                {
                    string query = "Exec TX_LayDonDaGiao '" + maTaiXe + "'";
                    DataTable dt = db.sql_select(query);
                    Dh_datagrid.ItemsSource = dt.DefaultView;
                    Dh_bt_nhandon.Visibility = Visibility.Hidden;
                    Dh_bt_huydon.Visibility = Visibility.Hidden;
                    Dh_bt_hoantat.Visibility = Visibility.Hidden;
                }

            }
            catch
            {

            }
        }

        private void Dh_bt_nhandon_click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (Dh_datagrid.SelectedIndex.ToString() != null)
                {
                    DataRowView rowview = (DataRowView)Dh_datagrid.SelectedItem;
                    if (rowview != null)
                    {
                        string query = "Exec TX_NhanDonHang '" + rowview["MaDonHang"].ToString() + "','"+maTaiXe+"'";
                        DataTable dt = db.sql_select(query);
                        string query1 = "Exec TX_LayDonChuaGiao N'" + tinh + "'";
                        DataTable dt1= db.sql_select(query1);
                        Dh_datagrid.ItemsSource = dt1.DefaultView;
                        Dh_lb_errorout.Content = "Nhân đơn thành công.";
                        Dh_lb_errorout.Background = Brushes.LightGreen;
                    }
                    else
                    {
                        Dh_lb_errorout.Content = "Vui lòng chọn một đơn đặt hàng!!!";
                        Dh_lb_errorout.Background = Brushes.IndianRed;
                    }
                }
              
            }
            catch
            {

            }
        }

        private void Dh_bt_huydon_click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (Dh_datagrid.SelectedIndex.ToString() != null)
                {
                    DataRowView rowview = (DataRowView)Dh_datagrid.SelectedItem;
                    if (rowview != null)
                    {
                        string query = "Exec TX_HuyDonHang '" + rowview["MaDonHang"].ToString() + "'";
                        DataTable dt = db.sql_select(query);
                        string query1 = "Exec TX_LayDonDangGiao '" + maTaiXe + "'";
                        DataTable dt1 = db.sql_select(query1);
                        Dh_datagrid.ItemsSource = dt1.DefaultView;
                        Dh_lb_errorout.Content = "Đã hủy giao đơn hàng thành công!!!";
                        Dh_lb_errorout.Background = Brushes.LightGreen;
                    }
                    else
                    {
                        Dh_lb_errorout.Content = "Vui lòng chọn một đơn đặt hàng!!!";
                        Dh_lb_errorout.Background = Brushes.IndianRed;
                    }
                }

            }
            catch
            {

            }
        }
     


        private void Dh_bt_dagiao_click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (Dh_datagrid.SelectedIndex.ToString() != null)
                {
                    DataRowView rowview = (DataRowView)Dh_datagrid.SelectedItem;
                    if (rowview != null)
                    {
                        string query = "Exec TX_HoanTatDonHang '" + rowview["MaDonHang"].ToString() + "','" + rowview["MaTaiXe"].ToString() + "'" +
                            ",'" + rowview["Phiship"].ToString() + "','" + rowview["MaDoiTac"].ToString() + "','" + rowview["TongGia"].ToString() + "'";
                        DataTable dt = db.sql_select(query);

                        string query1 = "Exec TX_LayDonDangGiao '" + maTaiXe + "'";
                        DataTable dt1 = db.sql_select(query1);
                        Dh_datagrid.ItemsSource = dt1.DefaultView;
                        Dh_lb_errorout.Content = "Đã giao đơn hàng thành công!!!";
                        Dh_lb_errorout.Background = Brushes.LightGreen;
                    }
                    else
                    {
                        Dh_lb_errorout.Content = "Vui lòng chọn một đơn đặt hàng!!!";
                        Dh_lb_errorout.Background = Brushes.IndianRed;
                    }
                }

            }
            catch
            {

            }
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
                        string query = "Exec TX_LayChiTietDonHang '" + rowview["MaDonHang"].ToString() + "'";
                        DataTable dt = db.sql_select(query);
                        Ctdh_datagrid.ItemsSource = dt.DefaultView;

                        Dh_tb_madonhang.Text = rowview["MaDonHang"].ToString();
                        int tongTien = Int32.Parse(rowview["TongGia"].ToString()) + Int32.Parse(rowview["Phiship"].ToString());
                        Dh_tb_tongtien.Text = tongTien.ToString();
                        string q="Exec TX_LayThongTinKhachHang '" + rowview["MaKhachHang"].ToString() + "'";
                        DataTable d = db.sql_select(q);
                        Dh_tb_tenkhach.Text = d.Rows[0]["HoTen"].ToString();
                        Dh_tb_diachicuthe.Text = d.Rows[0]["DCCuThe"].ToString();
                        Dh_tb_quan.Text= d.Rows[0]["Quan"].ToString();
                        Dh_tb_phiship.Text = rowview["Phiship"].ToString();
                        
                    }
                }
            }
            catch
            {

            }
        }

        private void Dh_datagrid_loaded(object sender, RoutedEventArgs e)
        {
            
        }

        private void TabItem_Loaded(object sender, RoutedEventArgs e)
        {

        }

        private void TabItem_DH_Loaded(object sender, RoutedEventArgs e)
        {
            Dh_cb_loaidon.Text = "Đơn chờ giao";
        }

        private void TabItem_Loaded_1(object sender, RoutedEventArgs e)
        {
            Tt_getThongTin();
        }
    }
}
