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
    /// Interaction logic for NhanVien_Window.xaml
    /// </summary>
    public partial class NhanVien_Window : Window
    {
        private string username;
        private string manhanvien;
        DBConnect db = new DBConnect();
        public NhanVien_Window(string user)
        {
            InitializeComponent();
            username = user;
        }
        public bool IsNumber(string pValue)
        {
            foreach (Char c in pValue)
            {
                if (!Char.IsDigit(c))
                    return false;
            }
            return true;
        }
        /*============================================THÔNG TIN===================================
         * =======================================================================================
         * =======================================================================================*/
        private void Tt_getThongTin()
        {
            try
            {
                string query = "Exec NV_LayThongTin '" + username + "'";
                DataTable dt = db.sql_select(query);
                Tt_tb_manhanvien.Text = dt.Rows[0]["MaNhanVien"].ToString();
                manhanvien= dt.Rows[0]["MaNhanVien"].ToString();
                Tt_tb_hoten.Text = dt.Rows[0]["HoTen"].ToString();
            }
            catch
            {
                Tt_lb_thongtincanhan_errorout.Content = "Không tìm được thông tin!!!";
                Tt_lb_thongtincanhan_errorout.Background = Brushes.IndianRed;
            }



        }

        private void Tt_loaded(object sender, RoutedEventArgs e)
        {
            Tt_getThongTin();
        }

        private void Tt_capnhatthongtincanhan_click(object sender, RoutedEventArgs e)
        {
            try
            {
                string query = "Exec NV_SuaThongTin '" + username + "','" + Tt_tb_hoten.Text + "' ";
                DataTable dt = db.sql_select(query);
                string loi = dt.Rows[0][0].ToString();
                if (loi == "-1")
                {
                    Tt_lb_thongtincanhan_errorout.Content = "Thông tin bị trống!!!";
                    Tt_lb_thongtincanhan_errorout.Background = Brushes.IndianRed;
                }
                else if (loi == "-10")
                {
                    Tt_lb_thongtincanhan_errorout.Content = "Lỗi giao tác!!!";
                    Tt_lb_thongtincanhan_errorout.Background = Brushes.IndianRed;
                }
                else
                {
                    Tt_lb_thongtincanhan_errorout.Content = "Cập nhật thành công.";
                    Tt_lb_thongtincanhan_errorout.Background = Brushes.LightGreen;
                }

            }
            catch
            { }
        }

        private void Tt_doimatkhau_loaded(object sender, RoutedEventArgs e)
        {
            TT_tb_taikhoan.Text = username;
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
        /*============================================QUẢN LÍ HỢP ĐỒNG===================================
        * =======================================================================================
        * =======================================================================================*/
        private void Qlhd_btn_timkiem_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (IsNumber(Qlhd_tb_timkiem.Text))
                {
                    string query = "Exec NV_TimKiem '" + Qlhd_cb_timkiem.Text + "','" + Qlhd_tb_timkiem.Text + "'";
                    DataTable dt = db.sql_select(query);

                    string loi = dt.Rows[0][0].ToString();
                    if (loi == "-1")
                    {
                        Qlhd_lb_errorout.Content = "Vui lòng chọn loại mã muốn tìm kiếm";
                        Qlhd_lb_errorout.Background = Brushes.IndianRed;
                    }
                    else if (loi == "-2")
                    {
                        Qlhd_lb_errorout.Content = "Mã hợp đồng không tồn tại";
                        Qlhd_lb_errorout.Background = Brushes.IndianRed;
                    }
                    else if (loi == "-3")
                    {
                        Qlhd_lb_errorout.Content = "Mã đối tác không tồn tại";
                        Qlhd_lb_errorout.Background = Brushes.IndianRed;
                    }
                    else if (loi == "-10")
                    {
                        Qlhd_lb_errorout.Content = "Lỗi hệ thống";
                        Qlhd_lb_errorout.Background = Brushes.IndianRed;
                    }
                    else
                    {
                        Qlhd_datagrid.ItemsSource = dt.DefaultView;
                        Qlhd_lb_errorout.Content = "Tìm kiếm thành công";
                        Qlhd_lb_errorout.Background = Brushes.LightGreen;
                        Qlhd_btn_duyet.Visibility = Visibility.Visible;
                        Qlhd_btn_giahan.Visibility = Visibility.Visible;
                        Cq_cb_loai.Text = "";
                    }
                }
                else
                {
                    Qlhd_lb_errorout.Content = "Vui lòng nhập 1 số !!!";
                    Qlhd_lb_errorout.Background = Brushes.IndianRed;
                }
               
            }
            catch
            {

            }
        }
        private void Qlhd_btn_duyet_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (Qlhd_datagrid.SelectedIndex.ToString() != null)
                {
                    DataRowView rowview = (DataRowView)Qlhd_datagrid.SelectedItem;
                    if (rowview != null)
                    {
                        string query = "Exec NV_DuyetHopDong '" + Qlhd_tb_mahd.Text + "',N'" + Qlhd_tb_tinhtrang.Text + "','" + manhanvien + "'";
                        DataTable dt = db.sql_select(query);

                        string loi = dt.Rows[0][0].ToString();
                        if (loi == "-1")
                        {
                            Qlhd_lb_errorout.Content = "Hợp đồng đã được duyệt trước!!!";
                            Qlhd_lb_errorout.Background = Brushes.IndianRed;
                        }
                        else if (loi == "-2")
                        {
                            Qlhd_lb_errorout.Content = "Mã hợp đồng không tồn tại";
                            Qlhd_lb_errorout.Background = Brushes.IndianRed;
                        }
                        else if (loi == "-10")
                        {
                            Qlhd_lb_errorout.Content = "Lỗi hệ thống";
                            Qlhd_lb_errorout.Background = Brushes.IndianRed;
                        }
                        else
                        {
                            Qlhd_lb_errorout.Content = "Duyệt thành công";
                            Qlhd_lb_errorout.Background = Brushes.LightGreen;
                            Cq_cb_loai.Text = "Hợp đồng chờ duyệt";
                            set_Qlhd_datagrid("Hợp đồng chờ duyệt");

                        }
                    }
                    else
                    {
                        Qlhd_lb_errorout.Content = "Vui lòng chọn một hợp đồng!!!";
                        Qlhd_lb_errorout.Background = Brushes.IndianRed;
                    }
                }
            }
            catch
            {

            }
        }

        private void Qlhd_btn_giahan_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (Qlhd_datagrid.SelectedIndex.ToString() != null)
                {
                    DataRowView rowview = (DataRowView)Qlhd_datagrid.SelectedItem;
                    if (rowview != null)
                    {
                        if (IsNumber(Qlhd_tb_thoihan.Text))
                        {
                            string query = "Exec NV_GiaHanHopDong '" + Qlhd_tb_mahd.Text + "',N'" + Qlhd_tb_tinhtrang.Text + "','" + Qlhd_tb_thoihan.Text + "','" + manhanvien + "'";
                            DataTable dt = db.sql_select(query);

                            string loi = dt.Rows[0][0].ToString();
                            if (loi == "-1")
                            {
                                Qlhd_lb_errorout.Content = "Hợp đồng đang chờ duyệt!!!";
                                Qlhd_lb_errorout.Background = Brushes.IndianRed;
                            }
                            else if (loi == "-2")
                            {
                                Qlhd_lb_errorout.Content = "Thời hạn không hợp lệ";
                                Qlhd_lb_errorout.Background = Brushes.IndianRed;
                            }
                            else if (loi == "-10")
                            {
                                Qlhd_lb_errorout.Content = "Lỗi hệ thống";
                                Qlhd_lb_errorout.Background = Brushes.IndianRed;
                            }
                            else
                            {
                                Qlhd_lb_errorout.Content = "Gia hạn thành công";
                                Qlhd_lb_errorout.Background = Brushes.LightGreen;
                                Cq_cb_loai.Text = "Hợp đồng đã duyệt";
                                set_Qlhd_datagrid("Hợp đồng đã duyệt");
                            }
                        }
                        else
                        {
                            Qlhd_lb_errorout.Content = "Thời hạn bạn nhập không hợp lệ!!!";
                            Qlhd_lb_errorout.Background = Brushes.IndianRed;
                        }
                        
                    }
                    else
                    {
                        Qlhd_lb_errorout.Content = "Vui lòng chọn một hợp đồng!!!";
                        Qlhd_lb_errorout.Background = Brushes.IndianRed;
                    }
                }
            }
            catch
            {

            }
        }
        private void Qlhd_datagrid_Loaded(object sender, RoutedEventArgs e)
        {

        }

        private void Qlhd_datagrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            try
            {
                if (Qlhd_datagrid.SelectedIndex.ToString() != null)
                {
                    DataRowView rowview = (DataRowView)Qlhd_datagrid.SelectedItem;
                    if (rowview != null)
                    {
                        
                        Qlhd_tb_mahd.Text = rowview["MaHopDong"].ToString();
                        Qlhd_tb_sochinhanh.Text= rowview["SoChiNhanh"].ToString();
                        Qlhd_tb_madoitac.Text= rowview["MaDoiTac"].ToString();
                        Qlhd_tb_thoihan.Text= rowview["ThoiHan"].ToString();
                        Qlhd_dp_ngaybatdau.Text = rowview["NgayBatDau"].ToString();
                        Qlhd_tb_tinhtrang.Text= rowview["XetDuyet"].ToString();
                        
                    }
                }
            }
            catch
            {

            }
        }

        private void set_Qlhd_datagrid(string loai)
        {
            if (loai == "Hợp đồng đã duyệt")
            {
                string query = "Exec NV_LayHopDongDaDuyet";
                DataTable dt = db.sql_select(query);
                Qlhd_datagrid.ItemsSource = dt.DefaultView;
                Qlhd_btn_duyet.Visibility = Visibility.Hidden;
                Qlhd_btn_giahan.Visibility = Visibility.Visible;

            }
            else if (loai == "Hợp đồng chờ duyệt")
            {
                string query = "Exec NV_LayHopDongChoDuyet";
                DataTable dt = db.sql_select(query);
                Qlhd_datagrid.ItemsSource = dt.DefaultView;

                Qlhd_btn_duyet.Visibility = Visibility.Visible;
                Qlhd_btn_giahan.Visibility = Visibility.Hidden;
            }
        }
        private void Qlhd_cb_tinhtrang_changed(object sender, SelectionChangedEventArgs e)
        {
            try
            {
                ComboBoxItem comboBoxItem = (ComboBoxItem)e.AddedItems[0];
                string loai = comboBoxItem.Content.ToString();
                set_Qlhd_datagrid(loai);



            }
            catch
            {

            }
        }

        private void Qlhd_cb_timkiem_changed(object sender, SelectionChangedEventArgs e)
        {
            
        }

       

      
        private void Btn_dangxuat_Click_1(object sender, RoutedEventArgs e)
        {

            LoginWindows lg = new LoginWindows(username);
            this.Close();
            lg.Show();
        }

        private void Qlhd_loaded(object sender, RoutedEventArgs e)
        {
            //Cq_cb_loai.Text = "Hợp đồng chờ duyệt";
        }
    }
}
