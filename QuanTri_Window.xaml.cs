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
    /// Interaction logic for QuanTri_Window.xaml
    /// </summary>
    public partial class QuanTri_Window : Window
    {
        private string username ;
        DBConnect db = new DBConnect();
        int count = 0;
        public QuanTri_Window(string user)
        {
            InitializeComponent();
            username = user;
        }
        /*============================================THÔNG TIN===================================
         * =======================================================================================
         * =======================================================================================*/
        private void Tt_getThongTin()
        {
            try
            {
                string query = "Exec QTV_LayThongTin '" + username + "'";
                DataTable dt = db.sql_select(query);
                Tt_tb_maquantri.Text = dt.Rows[0]["MaQT"].ToString();
                Tt_tb_hoten.Text = dt.Rows[0]["HoTen"].ToString();
            }
            catch
            {
                Tt_lb_thongtincanhan_errorout.Content = "Không tìm được thông tin!!!";
                Tt_lb_thongtincanhan_errorout.Background= Brushes.IndianRed;
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
                string query = "Exec QTV_SuaThongTin '" + username + "',N'" + Tt_tb_hoten.Text + "' ";
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
        /*============================================QUẢN LÍ TÀI KHOẢN===================================
         * =======================================================================================
         * =======================================================================================*/
        private void Ql_loaded_bangTaiKhoan()
        {
            try
            {
                DataTable dt = db.sql_select("select * from TaiKhoan");
                Ql_datagrid.ItemsSource = dt.DefaultView;
            }
            catch
            { }
        }
        private void Ql_datagrid_Loaded(object sender, RoutedEventArgs e)
        {
            if (count == 0)
            {
                count = 1;
                return;
            }
            //Ql_loaded_bangTaiKhoan(); 
            dh_update_pageNum();
            Dh_load_doitac();
        }
        private void Ql_datagrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            try
            {
                if (Ql_datagrid.SelectedIndex.ToString() != null)
                {
                    DataRowView rowview = (DataRowView)Ql_datagrid.SelectedItem;
                    if (rowview != null)
                    {
                        Ql_tb_username.Text = rowview["username"].ToString();
                        Ql_tb_loai.Text = rowview["Loai"].ToString();
                        Ql_tb_tinhtrang.Text = rowview["TinhTrang"].ToString();
                        if (Ql_tb_tinhtrang.Text == "Khóa")
                        {
                            Ql_tb_tinhtrangmoi.Text = "Hoạt động";
                        }
                        else
                        {
                            Ql_tb_tinhtrangmoi.Text = "Khóa";
                        }
                    }
                }
            }
            catch
            {

            }
        }
        private void Ql_timkiemusername_click(object sender, RoutedEventArgs e)
        {
            dh_update_pageNum();
            Dh_load_doitac();
            //try
            //{
            //    string query = "Exec LayThongTinTaiKhoan '" + Ql_tb_timusername.Text + "'";
            //    DataTable dt = db.sql_select(query);
            //    if (dt.Columns.Count ==1)
            //    {
            //        string loi = dt.Rows[0][0].ToString();
            //        if (loi == "-1")
            //        {

            //            Ql_lb_errorout.Content = "Thông tin rỗng!!!";
            //            Ql_lb_errorout.Background= Brushes.IndianRed;
            //        }
            //        else if (loi == "-2")
            //        {
            //            Ql_lb_errorout.Content = "Username không tồn tại!!!";
            //            Ql_lb_errorout.Background = Brushes.IndianRed;
            //        }
            //        else if (loi == "-10")
            //        {
            //            Ql_lb_errorout.Content = "Lỗi giao tác!!!";
            //            Ql_lb_errorout.Background = Brushes.IndianRed;
            //        }
            //        Ql_loaded_bangTaiKhoan();

            //    }
            //    else
            //    {
            //        Ql_datagrid.ItemsSource = dt.DefaultView;
            //        Ql_lb_errorout.Content = "Tìm thành công.";
            //        Ql_lb_errorout.Background = Brushes.LightGreen;
            //    }


            //}
            //catch{

            //}
        }

        private void Ql_capnhattinhtrang_click(object sender, RoutedEventArgs e)
        {
            try
            {
                string query = "Exec CapNhatTinhTrangTaiKhoan '" + Ql_tb_username.Text + "',N'" + Ql_tb_tinhtrangmoi.Text + "' ";
                DataTable dt = db.sql_select(query);
                string loi = dt.Rows[0][0].ToString();
                if (loi == "-1")
                {
                    Ql_lb_errorout.Content = "Bạn chưa chọn user cần update!!!";
                    Ql_lb_errorout.Background = Brushes.IndianRed;
                }
                else if (loi == "-10")
                {
                    Ql_lb_errorout.Content = "Lỗi giao tác!!!";
                    Ql_lb_errorout.Background = Brushes.IndianRed;
                }
                else
                {
                    Ql_lb_errorout.Content = "Cập nhật thành công.";
                    Ql_lb_errorout.Background = Brushes.LightGreen;
                }
                if (Ql_tb_timusername.Text != Ql_tb_username.Text)
                {
                    Ql_loaded_bangTaiKhoan();
                }
                else
                {
                    Ql_timkiemusername_click(sender, e);
                }
                dh_update_pageNum();
                Dh_load_doitac();
            }
            catch
            {

            }
        }




        private void dh_tb_search_TextChanged(object sender, TextChangedEventArgs e)
        {

        }
        private void dh_update_pageNum()
        {
            try
            {
                string TotalRow = db.sql_select("exec QTV_danhsachtaikhoan_dem N'" +
                    Ql_tb_timusername.Text + "','',''").Rows[0][0].ToString();
                int a = (int)Math.Ceiling(1.0 * Int32.Parse(TotalRow) / Int32.Parse(dh_tb_doitac_rowperpage.Text));
                dh_doitac_totalpage.Content = a.ToString();
                if (Int32.Parse(dh_tb_doitac_curpage.Text) > a)
                {
                    dh_tb_doitac_curpage.Text = a.ToString();
                }
                if (dh_tb_doitac_curpage.Text == "0" && a > 0)
                    dh_tb_doitac_curpage.Text = "1";
                if (a == 0)
                    dh_tb_doitac_curpage.Text = "0";
            }
            catch { }
        }
        private void Dh_load_doitac()
        {
            try
            {
                int numpage = Int32.Parse(dh_tb_doitac_curpage.Text);
                int perpage = Int32.Parse(dh_tb_doitac_rowperpage.Text);
                Ql_datagrid.ItemsSource = db.sql_select("exec " +
                    "QTV_danhsachtaikhoan N'" +
                    Ql_tb_timusername.Text + "','" +
                    (numpage - 1) * perpage + "','" + (numpage) * perpage + "'").DefaultView;
            }
            catch { }
        }
        private void dh_bt_doitac_prevpage_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (dh_tb_doitac_curpage.Text == "" || dh_tb_doitac_curpage.Text == "1")
                {
                    dh_tb_doitac_curpage.Text = "1";
                }
                else
                {
                    dh_tb_doitac_curpage.Text = (Int32.Parse(dh_tb_doitac_curpage.Text) - 1).ToString();
                }
            }
            catch
            {
                dh_tb_doitac_curpage.Text = "1";
            }
        }
        private void dh_bt_doitac_nextpage_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (Int32.Parse(dh_tb_doitac_curpage.Text) == Int32.Parse(dh_doitac_totalpage.Content.ToString()))
                {
                    dh_tb_doitac_curpage.Text = dh_doitac_totalpage.Content.ToString();
                }
                else
                {
                    dh_tb_doitac_curpage.Text = (Int32.Parse(dh_tb_doitac_curpage.Text) + 1).ToString();
                }
            }
            catch
            {
                dh_tb_doitac_curpage.Text = "1";
            }
        }
        private void dh_tb_doitac_curpage_TextChanged(object sender, TextChangedEventArgs e)
        {
            //Giữ số trang luôn đúng
            try
            {
                if (Int32.Parse(dh_tb_doitac_curpage.Text) > Int32.Parse(dh_doitac_totalpage.Content.ToString()))
                {
                    dh_tb_doitac_curpage.Text = dh_doitac_totalpage.Content.ToString();
                }
                else if (Int32.Parse(dh_tb_doitac_curpage.Text) < 1 && Int32.Parse(dh_doitac_totalpage.Content.ToString()) > 0)
                    dh_tb_doitac_curpage.Text = "1";
                else if (Int32.Parse(dh_doitac_totalpage.Content.ToString()) == 0)
                    dh_tb_doitac_curpage.Text = "0";
                else if (Int32.Parse(dh_doitac_totalpage.Content.ToString()) == 1)
                    dh_tb_doitac_curpage.Text = "1";
            }
            catch
            {
                dh_tb_doitac_curpage.Text = "1";
            }
            //Load trang với số trang tương ứng
            try
            {
                Dh_load_doitac();
            }
            catch
            {

            }
        }
        private void dh_tb_doitac_rowperpage_TextChanged(object sender, TextChangedEventArgs e)
        {
            //Giữ số trang luôn đúng
            try
            {
                if (Int32.Parse(dh_tb_doitac_rowperpage.Text) < 10)
                    dh_tb_doitac_rowperpage.Text = "10";
            }
            catch
            {
                dh_tb_doitac_rowperpage.Text = "10";
            }
            dh_update_pageNum();
            try
            {
                int num1 = Int32.Parse(dh_tb_doitac_curpage.Text);
                int num2 = Int32.Parse(dh_doitac_totalpage.Content.ToString());
                if (num1 > num2)
                    dh_tb_doitac_curpage.Text = dh_doitac_totalpage.Content.ToString();
            }
            catch { }
            Dh_load_doitac();
        }

        /*============================================CẤP QUYỀN===================================
         * =======================================================================================
         * =======================================================================================*/
        private void Cq_btn_them_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                string query = "Exec QTV_ThemQuanTriVien_NhanVien '" + Cq_tb_username.Text + "',N'" + Cq_tb_hoten.Text + "'," +
               "'" + Cq_tb_matkhau.Password + "','" + Cq_tb_matkhau2.Password + "',N'" + Cq_cb_loai.Text + "' ";
                DataTable dt = db.sql_select(query);
                string loi = dt.Rows[0][0].ToString();
                if (loi == "-1")
                {
                    Cq_lb_errorout.Content = "Có trường thông tin bị trống!!!";
                    Cq_lb_errorout.Background = Brushes.IndianRed;
                }
                else if (loi == "-10")
                {
                    Cq_lb_errorout.Content = "Lỗi giao tác!!!";
                    Cq_lb_errorout.Background = Brushes.IndianRed;
                }
                else if(loi=="-2")
                {
                    Cq_lb_errorout.Content = "Tài khoản đã tồn tại!!!";
                    Cq_lb_errorout.Background = Brushes.IndianRed;
                }
                else if (loi == "-3")
                {
                    Cq_lb_errorout.Content = "Mật khẩu nhập lại không đúng!!!";
                    Cq_lb_errorout.Background = Brushes.IndianRed;
                }
                else
                {
                    Cq_lb_errorout.Content = "Thêm "+Cq_cb_loai.Text+ " thành công.";
                    Cq_lb_errorout.Background = Brushes.LightGreen;
                    Cq_loaded_bangQuanTriNhanVien();
                }

            }
            catch { }
        }
        private void Cq_btn_sua_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (Cq_datagrid.SelectedIndex.ToString() != null)
                {
                    DataRowView rowview = (DataRowView)Cq_datagrid.SelectedItem;
                    if (rowview != null)
                    {
                        string query = "Exec QTV_SuaQuanTriVien_NhanVien  '" + rowview["username"].ToString() + "',N'" + Cq_tb_hoten.Text + "', '" + rowview["Ma"].ToString() + "'," +
                            "'" + Cq_tb_matkhau.Password + "','" + Cq_tb_matkhau2.Password + "',N'" + Cq_cb_loai.Text + "'";
                        DataTable dt = db.sql_select(query);
                        string loi = dt.Rows[0][0].ToString();
                        if (loi == "-1")
                        {
                            Cq_lb_errorout.Content = "Role cần sửa trống!!!";
                            Cq_lb_errorout.Background = Brushes.IndianRed;
                        }
                        else if (loi == "-10")
                        {
                            Cq_lb_errorout.Content = "Lỗi giao tác!!!";
                            Cq_lb_errorout.Background = Brushes.IndianRed;
                        }
                        else if (loi == "-2")
                        {
                            Cq_lb_errorout.Content = "Chưa chọn user cần sửa !!!";
                            Cq_lb_errorout.Background = Brushes.IndianRed;
                        }
                        else if (loi == "-3")
                        {
                            Cq_lb_errorout.Content = "User không tồn tại!!!";
                            Cq_lb_errorout.Background = Brushes.IndianRed;
                        }
                        else if (loi == "-4")
                        {
                            Cq_lb_errorout.Content = "Không tồn tại nhân viên!!!";
                            Cq_lb_errorout.Background = Brushes.IndianRed;
                        }
                        else if (loi == "-5")
                        {
                            Cq_lb_errorout.Content = "Quản trị viên không tồn tại!!!";
                            Cq_lb_errorout.Background = Brushes.IndianRed;
                        }
                        else if (loi == "-6")
                        {
                            Cq_lb_errorout.Content = "Pass nhập lại không đúng!!!";
                            Cq_lb_errorout.Background = Brushes.IndianRed;
                        }
                        else if (loi == "-7")
                        {
                            Cq_lb_errorout.Content = "Có trường thông tin bị trống";
                            Cq_lb_errorout.Background = Brushes.IndianRed;
                        }
                        else
                        {
                            Cq_lb_errorout.Content = "Sửa " + Cq_cb_loai.Text + " thành công.";
                            Cq_lb_errorout.Background = Brushes.LightGreen;
                            Cq_loaded_bangQuanTriNhanVien();
                        }

                    }
                    else
                    {
                        Cq_lb_errorout.Content = "Vui lòng chọn user cần sửa!!!";
                        Cq_lb_errorout.Background = Brushes.IndianRed;
                    }

                }
            }
            catch
            {

            }
        }

        private void Cq_btn_xoa_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (Cq_datagrid.SelectedIndex.ToString() != null)
                {
                    DataRowView rowview = (DataRowView)Cq_datagrid.SelectedItem;
                    if (rowview != null)
                    {
                        string query = "Exec QTV_XoaQuanTriVien_NhanVien  '" + rowview["username"].ToString() + "',N'" + Cq_cb_loai.Text + "', '" + rowview["Ma"].ToString() + "'";
                        DataTable dt = db.sql_select(query);
                        string loi = dt.Rows[0][0].ToString();
                        if (loi == "-1")
                        {
                            Cq_lb_errorout.Content = "User không tồn tại!!!";
                            Cq_lb_errorout.Background = Brushes.IndianRed;
                        }
                        else if (loi == "-10")
                        {
                            Cq_lb_errorout.Content = "Lỗi giao tác!!!";
                            Cq_lb_errorout.Background = Brushes.IndianRed;
                        }
                        else if (loi == "-2")
                        {
                            Cq_lb_errorout.Content = "Không tồn tại nhân viên!!!";
                            Cq_lb_errorout.Background = Brushes.IndianRed;
                        }
                        else if (loi == "-3")
                        {
                            Cq_lb_errorout.Content = "Không thể xóa, nhân viên đang phụ trách hợp đồng!!!";
                            Cq_lb_errorout.Background = Brushes.IndianRed;
                        }
                        else if (loi == "-4")
                        {
                            Cq_lb_errorout.Content = "Quản trị viên không tồn tại!!!";
                            Cq_lb_errorout.Background = Brushes.IndianRed;
                        }
                        else
                        {
                            Cq_lb_errorout.Content = "Xóa " + Cq_cb_loai.Text + " thành công.";
                            Cq_lb_errorout.Background = Brushes.LightGreen;
                            Cq_loaded_bangQuanTriNhanVien();
                        }

                    }
                    else
                    {
                        Cq_lb_errorout.Content = "Vui lòng chọn user cần xóa!!!";
                        Cq_lb_errorout.Background = Brushes.IndianRed;
                    }

                }
            }
            catch
            {

            }
        }
        private void Cq_loaded_bangQuanTriNhanVien()
        {
            try
            {
               
                if (Cq_cb_loai.Text== "Nhân viên")
                {
                    string query = "Exec QTV_LayThongTinNhanVien";
                    DataTable dt = db.sql_select(query);
                    Cq_datagrid.ItemsSource = dt.DefaultView;
                    Cq_lb_tenbang.Content = "Bảng Nhân viên";
                }
                else if (Cq_cb_loai.Text == "Quản trị")
                {
                    string query = "Exec QTV_LayThongTinQuanTri";
                    DataTable dt = db.sql_select(query);
                    Cq_datagrid.ItemsSource = dt.DefaultView;
                    Cq_lb_tenbang.Content = "Bảng Quản trị";
                }
                
            }
            catch
            {

            }
        }
        private void Cq_datagrid_Loaded(object sender, RoutedEventArgs e)
        {
            
            //Cq_loaded_bangQuanTriNhanVien();
        }

        private void Cq_cb_loai_changed(object sender, SelectionChangedEventArgs e)
        {
            try
            {
                ComboBoxItem comboBoxItem = (ComboBoxItem)e.AddedItems[0];
                string loai = comboBoxItem.Content.ToString();
                if (loai == "Nhân viên")
                {
                    string query = "Exec QTV_LayThongTinNhanVien";
                    DataTable dt = db.sql_select(query);
                    Cq_datagrid.ItemsSource = dt.DefaultView;
                   
                }
                else if (loai == "Quản trị")
                {
                    string query = "Exec QTV_LayThongTinQuanTri";
                    DataTable dt = db.sql_select(query);
                    Cq_datagrid.ItemsSource = dt.DefaultView;
                }
                Cq_lb_tenbang.Content = "Bảng " + loai;
            }
            catch
            {

            }
            
            
        }

        private void Cq_datagrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            try
            {
                if (Cq_datagrid.SelectedIndex.ToString() != null)
                {
                    DataRowView rowview = (DataRowView)Cq_datagrid.SelectedItem;
                    if (rowview != null)
                    {
                        Cq_tb_ma.Text= rowview["Ma"].ToString();
                        Cq_tb_username.Text= rowview["username"].ToString();
                        Cq_tb_hoten.Text= rowview["HoTen"].ToString();
                       
                    }
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

        private void Ql_tb_timusername_TextChanged(object sender, TextChangedEventArgs e)
        {

            //Dh_datagrid_doitac.ItemsSource = db.sql_select("exec kh_danhsachdoitac N'"+
            //    dh_tb_search.Text+ "',N'"+ dh_tb_search.Text + "','"+
            //    +"','"++"'");
        }
    }
}
