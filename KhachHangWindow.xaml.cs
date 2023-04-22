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
    /// Interaction logic for KhachHangWindow.xaml
    /// </summary>
    public partial class KhachHangWindow : Window
    {
        public string MaKhachHang;
        DBConnect db = new DBConnect();
        DoiTac dt = new DoiTac();
        public string username = "";
        string MaDiaChi;
        string MaSTK;
        public KhachHangWindow(string username_)
        {
            InitializeComponent();
            username = username_;
            //this.username = username; 
            MaKhachHang = LayMaKhach();
            if (MaKhachHang == null)
            {
                MessageBox.Show("Không tồn tại tài khoản khách hàng với username "+username);
                this.Close();  
            }
        }
        ///================================================================================================================
        ///================================================================================================================
        ///================================================ HÀM DÙNG CHUNG ================================================
        ///================================================================================================================
        ///================================================================================================================

        private void Btn_dangxuat_Click_1(object sender, RoutedEventArgs e)
        {
            LoginWindows lg = new LoginWindows(username);
            this.Close();
            lg.Show();
        }                     

        private string LayMaKhach()
        {
            try
            {
             return db.sql_select("select MaKhachHang from KhachHang where Username = '" + username + "'").Rows[0][0].ToString();
            }
            catch 
            {
                return null;
            }
        }
        private int tinhSoDu()
        {
            try
            {
                return (int)db.sql_select("select SoDu from TaiKhoanNH where MaNguoiDung = (select MaSTK from KhachHang where MaKhachHang = '" + MaKhachHang + "')").Rows[0][0];
            }
            catch
            {
                return 0;
            }
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
                string query = "select * from KhachHang where username = '"+username+"'";
                DataTable dt = db.sql_select(query);
                DataRow r = dt.Rows[0];
                TT_tb_ten.Text = r["HoTen"].ToString();
                TT_tb_cmnd.Text = r["CMND"].ToString();
                TT_tb_sdt.Text = r["SDT"].ToString();
                TT_tb_email.Text = r["Email"].ToString();
                MaDiaChi = r["MaDiaChi"].ToString();
                MaSTK = r["MaSTK"].ToString();

                query = "select * from DiaChi " +
                    "where MaDiaChi = '" +MaDiaChi+ "'";
                dt = db.sql_select(query);
                r = dt.Rows[0];
                TT_tb_tinh.Text = r["Tinh"].ToString();
                TT_tb_quan.Text = r["Quan"].ToString();
                TT_tb_diachi.Text = r["DCCuThe"].ToString();


                query = "select * from TaiKhoanNH " +
                    "where MaNguoiDung = '" + MaSTK+ "'";
                dt = db.sql_select(query);
                r = dt.Rows[0];
                TT_tb_nganhang.Text = r["TenNH"].ToString();
                TT_tb_sotaikhoan.Text = r["STK"].ToString();
                TT_tb_sodu.Text = r["SoDu"].ToString();

                TT_tb_username.Text = username;

                TT_lb_errorout.Content = "";
                TT_lb_errorout.Background = Brushes.Transparent;
            }
            catch
            {
                TT_lb_errorout.Content = "Không tìm được thông tin. Lỗi!!!";
                TT_lb_errorout.Background = Brushes.IndianRed;
            }
        }
        private void Tt_ThongTin_loaded(object sender, RoutedEventArgs e)
        {
            Tt_load_Datagrid();
        }
        private void TT_HienMaLoi(int maloi)
        {
            if (maloi == 0)
            {
                TT_lb_errorout.Content = "Cập nhật thông tin thành công";
                TT_lb_errorout.Background = Brushes.LightGreen;
            }
            else
            {
                switch (maloi)
                {
                    case 1:
                        TT_lb_errorout.Content = "Có trường trống";
                        break;
                    case 2:
                        TT_lb_errorout.Content = "Số tài khoản đã tồn tại";
                        break;
                    case 10:
                        TT_lb_errorout.Content = "Dữ liệu nhập bị sai";
                        break;
                }
                TT_lb_errorout.Background = Brushes.LightGreen;
            }
        }
        private void Tt_capnhatthongtincanhan_click(object sender, RoutedEventArgs e)
        {
            try
            {
                string query = "exec kh_capnhatthongtin '" +
                    MaKhachHang + "',N'" + TT_tb_ten.Text + "','" +
                    TT_tb_sdt.Text + "','" + TT_tb_email.Text + "',N'" +
                    TT_tb_nganhang.Text + "','" + TT_tb_sotaikhoan.Text + "',N'" +
                    TT_tb_tinh.Text + "',N'" + TT_tb_quan.Text + "',N'" +
                    TT_tb_diachi.Text + "','" + TT_tb_cmnd.Text + "'";
                int kq = (int)db.sql_select(query).Rows[0][0];
                TT_HienMaLoi(kq);
            }
            catch { }
        }
        private void Tt_capnhatmatkhau_click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (TT_tb_matkhaucu.Text == "" || TT_tb_matkhaumoi.Password =="" || TT_tb_matkhaumoi_2.Password == "")
                {
                    Tt_lb_doimatkhau_errorout.Content = "Có trường trống";
                    Tt_lb_doimatkhau_errorout.Background = Brushes.LightGreen;
                }
                else if (TT_tb_matkhaucu.Text != (string)db.sql_select("select pass from TaiKhoan where username = '"+username+"'").Rows[0][0])
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

                        dt.doiMatKhau(TT_tb_matkhaumoi.Password, username);
                        Tt_lb_doimatkhau_errorout.Content = "Đổi mật khẩu thành công";
                        Tt_lb_doimatkhau_errorout.Background = Brushes.LightGreen;
                        //MessageBox.Show("Cập nhật thành công!", "Thông báo");
                    }
                }

            }
            catch
            {

            }
            
        }
        private void tt_bt_doimatkhau_loaded(object sender, RoutedEventArgs e)
        {
            TT_tb_taikhoan.Text = username;

        }

        ///================================================================================================================
        ///================================================================================================================
        ///================================================ BẢNG GIỎ HÀNG =================================================
        ///================================================================================================================
        ///================================================================================================================
        private void Gh_datagrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            try
            {
                if (Gh_datagrid.SelectedIndex.ToString() != null)
                {
                    DataRowView rowview = (DataRowView)Gh_datagrid.SelectedItem;
                    if (rowview != null)
                    {
                        Gh_tb_mamonan.Text = rowview["MaMonAn"].ToString();
                        Gh_tb_soluong.Text = rowview["SoLuong"].ToString();
                    }
                }
            }
            catch
            { }
        }
        private void Gh_load_datagrid()
        {
            try
            {
                DataTable dt = db.sql_select("exec kh_giohang '" + MaKhachHang + "'");
                Gh_datagrid.ItemsSource = dt.DefaultView;
                Gh_tb_tendoitac.Text = db.sql_select("select TenQuan from DoiTac where MaDoiTac ='" + dt.Rows[0][2].ToString() + "'").Rows[0][0].ToString();
                Gh_tb_phiship.Text = "15000";
                Gh_tb_tonggio.Text = db.sql_select("exec kh_tonggiohang '" + MaKhachHang + "'").Rows[0][0].ToString();
                Gh_tb_sodu.Text = db.sql_select("select SoDu from TaiKhoanNH where MaNguoiDung = (select MaSTK from KhachHang where MaKhachHang = '" + MaKhachHang + "')").Rows[0][0].ToString();

            }
            catch { }
        }
        private void Gh_datagrid_Loaded(object sender, RoutedEventArgs e)
        {
            Gh_load_datagrid();
        }
        private void Gh_tb_soluong_TextChanged(object sender, TextChangedEventArgs e)
        {
            try
            {
                if (Int32.Parse(Gh_tb_soluong.Text) < 1)
                    Gh_tb_soluong.Text = "1";
            }
            catch
            {
                Gh_tb_soluong.Text = "1";
            }
            db.sql_select("update GioHang set SoLuong = '" + Gh_tb_soluong.Text + "' where MaKhachHang = '" + MaKhachHang + "' and MaMonAn = '" + Gh_tb_mamonan.Text + "'");
            Gh_load_datagrid();
        }
        private void Gh_bt_xoamon_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                db.sql_select("delete GioHang where MaKhachHang = '" + MaKhachHang + "' and MaMonAn = '" + Gh_tb_mamonan.Text + "'");
                Gh_load_datagrid();
            }
            catch { }
        }
        private void Gh_bt_xoagio_Click(object sender, RoutedEventArgs e)
        {

            if (MessageBox.Show("Bạn có muốn xoá giỏ không?", "Giỏ hàng", MessageBoxButton.YesNo, MessageBoxImage.Warning) == MessageBoxResult.No)
            {
                //do no stuff
                //MessageBox.Show("Không xoá");
            }
            else
            {
                //do yes stuff            
                //MessageBox.Show("Đã xoá");
                db.sql_select("delete GioHang where MaKhachHang = '" + MaKhachHang + "'");
                Gh_load_datagrid();
                Dh_lb_errorout.Content = "Đã xoá giỏ";
                Gh_lb_errorout.Content = Gh_tb_phiship.Text;
                Gh_lb_errorout.Background = Brushes.LightGreen;

            }
        }

        ///================================================================================================================
        ///================================================================================================================
        ///================================================= BẢNG ĐẶT HÀNG ================================================
        ///================================================================================================================
        ///================================================================================================================

        private void dh_tb_search_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (!string.IsNullOrEmpty(dh_tb_search.Text) && dh_tb_search.Text.Length > 0)
            {
                dh_lb_search.Visibility = Visibility.Collapsed;
            }
            else
            {
                dh_lb_search.Visibility = Visibility.Visible;
            } 
            dh_update_pageNum();
            Dh_load_doitac();

        }
        private void dh_update_pageNum()
        {
            try
            {
                string TotalRow = db.sql_select("exec kh_danhsachdoitac_dem N'" +
                    dh_tb_search.Text + "',N'" +
                    dh_tb_search.Text + "','',''").Rows[0][0].ToString();
                int a = (int)Math.Ceiling(1.0 * Int32.Parse(TotalRow) / Int32.Parse(dh_tb_doitac_rowperpage.Text));
                dh_doitac_totalpage.Content = a.ToString();
                if (Int32.Parse(dh_tb_doitac_curpage.Text) > a )
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
                Dh_datagrid_doitac.ItemsSource = db.sql_select("exec " +
                    "kh_danhsachdoitac N'" +
                    dh_tb_search.Text + "',N'" +
                    dh_tb_search.Text + "','" +
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
                if (Int32.Parse(dh_tb_doitac_curpage.Text) < 1 && Int32.Parse(dh_doitac_totalpage.Content.ToString())>0)
                    dh_tb_doitac_curpage.Text = "1";
                if (Int32.Parse(dh_doitac_totalpage.Content.ToString()) == 0)
                    dh_tb_doitac_curpage.Text = "0";
                if (Int32.Parse(dh_doitac_totalpage.Content.ToString()) == 1)
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
            if (Int32.Parse(dh_tb_doitac_curpage.Text) > Int32.Parse(dh_doitac_totalpage.Content.ToString()))
                dh_tb_doitac_curpage.Text = dh_doitac_totalpage.Content.ToString();
            Dh_load_doitac();
        }


        private void Dh_datagrid_doitac_Loaded(object sender, RoutedEventArgs e)
        {
            dh_update_pageNum();
            Dh_load_doitac();
        }
        private void Dh_load_monan()
        {
            try
            {
                if (Dh_datagrid_doitac.SelectedIndex.ToString() != null)
                {
                    DataRowView rowview = (DataRowView)Dh_datagrid_doitac.SelectedItem;
                    if (rowview != null)
                    {
                        Dh_datagrid_MonAn.ItemsSource = db.sql_select("exec kh_danhsachthucdon '"+rowview["MaDoiTac"].ToString()+"'").DefaultView;
                    }
                }
            }
            catch
            { }
        }
        private void Dh_datagrid_doitac_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            Dh_load_monan();
        }
        private void Dh_datagrid_MonAn_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            int tongbill = 0;
            DataRowView rowview = (DataRowView)Dh_datagrid_MonAn.SelectedItem;
            if (rowview != null)
            {
                var dsmonan = Dh_LayMonAnDaChon();
                foreach (string stt in dsmonan)
                {
                    tongbill = tongbill + (int)db.sql_select("select Gia from ThucDon where MaDoiTac = '" + rowview["MaDoiTac"].ToString() + "' and MaMonAn = '" + stt + "'").Rows[0][0];
                }
            }
            Dh_tongbill.Text = tongbill.ToString();
        }
        //---------------Mua hàng------------------
        List<string> Dh_LayMonAnDaChon()
        {
            /// Lấy list index dòng đã chọn
            var SelectedList = new List<string>();
            for (int i = 0; i < Dh_datagrid_MonAn.Items.Count; i++)
            {
                var item = Dh_datagrid_MonAn.Items[i];
                var mycheckbox = Dh_datagrid_MonAn.Columns[4].GetCellContent(item) as CheckBox;
                if ((bool)mycheckbox.IsChecked)
                {
                    TextBlock x = Dh_datagrid_MonAn.Columns[0].GetCellContent(Dh_datagrid_MonAn.Items[i]) as TextBlock;
                    SelectedList.Add(x.Text);
                }
            }
            return SelectedList;
        }
        private void Dh_themvaogio()
        {
            try
            {
                if (Dh_LayMonAnDaChon().Count() > 0)
                {
                    DataRowView r = (DataRowView)Dh_datagrid_MonAn.SelectedItem;
                    string MaDoiTac = r["MaDoiTac"].ToString();
                    //Kiểm tra giỏ hàng có sản phầm chưa, nếu có của shop khác thì huỷ
                    if (0 < (int)db.sql_select("select count(MaMonAn) " +
                            "from GioHang " +
                            "where MaKhachHang = '" + MaKhachHang +
                            "' and MaDoiTac != '" + MaDoiTac + "'").Rows[0][0])
                    {
                        {
                            //Nếu trong giỏ hàng có sản phẩm của đôi tác khác
                            if (MessageBox.Show("Trong giỏ đã có sản phẩm của quán khác\nBạn có muốn xoá giỏ và thêm sản phẩm vừa chọn", "Giỏ hàng", MessageBoxButton.YesNo, MessageBoxImage.Warning) == MessageBoxResult.No)
                            {
                                //do no stuff
                                Dh_lb_errorout.Content = "Đã xoá giỏ hàng cũ";
                                MessageBox.Show("Không xoá");
                                return;
                            }
                            else
                            {
                                //do yes stuff            
                                MessageBox.Show("Đã xoá");
                                db.sql_select("delete GioHang where MaKhachHang = '" + MaKhachHang + "'");
                            }
                        }
                    }
                    else
                    {
                        //Nếu trong giỏ hàng chỉ có sản phẩm của đối tác đang lựa
                    }

                    DataRowView rowview = (DataRowView)Dh_datagrid_MonAn.SelectedItem;
                    if (rowview != null)
                    {
                        var dsmonan = Dh_LayMonAnDaChon();
                        foreach (string stt in dsmonan)
                        {
                            db.sql_select("exec kh_themSPvaogio '" + MaKhachHang + "','" + rowview["MaDoiTac"].ToString() + "','" + stt + "',1");
                        }

                    }
                    Dh_lb_errorout.Content = "Thêm vào giỏ thành công";
                    Dh_lb_errorout.Background = Brushes.LightGreen;

                }

            }
            catch
            { }
        }
        private void Dh_btn_them_Click(object sender, RoutedEventArgs e)
        {
            Dh_themvaogio();
        }
        private void Gh_bt_muangay_Click(object sender, RoutedEventArgs e)
        {

            try
            {
                //Thêm đơn hàng
                int maloi = (int)db.sql_select("exec kh_ThemDonHang '" + DateTime.Now.ToString("HH:mm:ss") + "','" + DateTime.Now.ToString("MM/dd/yyyy") + "','" + 15000 + "','" + MaKhachHang + "'").Rows[0][0];
                if (maloi == 0)
                {
                    Gh_lb_errorout.Content = "Mua thành công";
                    Gh_lb_errorout.Background = Brushes.LightGreen;
                }
                Gh_load_datagrid();
                Gh_tb_sodu.Text = tinhSoDu().ToString();
                TT_tb_sodu.Text = tinhSoDu().ToString();
                return;

            }
            catch { }
            Dh_lb_errorout.Content = "Không thể đặt hàng";
            Dh_lb_errorout.Background = Brushes.IndianRed;
        }

        ///================================================================================================================
        ///================================================================================================================
        ///============================================= BẢNG QUẢN LÝ ĐƠN HÀNG ============================================
        ///================================================================================================================
        ///================================================================================================================

        private void QLDH_load_datagrid()
        {
        }
        private void QLDH_datagrid_Loaded(object sender, RoutedEventArgs e)
        {
            try
            {
                QLDH_datagrid.ItemsSource = db.sql_select("select *,(select HoTen from TaiXe where MaTaiXe = dh.MaTaiXe)TenTX from DonDatHang dh where MaKhachHang = '" + MaKhachHang + "'").DefaultView;
            }
            catch { }
        }
        private void QLDH_load_chitietdon(string MaDonHang)
        {
            try
            {
                QLDH_datagrid_Chitietdon.ItemsSource = db.sql_select("select *," +
                    "(select TenMon from ThucDon where MaDoitac = ct.MaDOiTac and MaMonAn = ct.MamonAn)TenMon," +
                    "(select Gia from ThucDon where MaDoitac = ct.MaDOiTac and MaMonAn = ct.MamonAn)Gia " +
                    "from chitietdondathang ct where MaDonHang = '" + MaDonHang + "'").DefaultView;
            }
            catch { }

        }
        private void QLDH_datagrid_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            try
            {
                if (QLDH_datagrid.SelectedIndex.ToString() != null)
                {
                    DataRowView rowview = (DataRowView)QLDH_datagrid.SelectedItem;
                    if (rowview != null)
                    {
                        QLDH_tb_madon.Text = rowview["MaDonHang"].ToString();
                        QLDH_tb_tendoitac.Text = db.sql_select("select TenQuan from DoiTac where MaDoiTac = '"+rowview["MaDoiTac"].ToString() + "'").Rows[0][0].ToString();
                        QLDH_tb_phiship.Text = rowview["Phiship"].ToString();
                        QLDH_tb_giahang.Text = rowview["Tonggia"].ToString();
                        QLDH_tb_tonggiadon.Text = (Int32.Parse(QLDH_tb_phiship.Text) + Int32.Parse(QLDH_tb_giahang.Text)).ToString();
                        QLDH_load_chitietdon(rowview["MaDonHang"].ToString());
                        //QLDH_tb_danhgia.Text = rowview["Danhgia"].ToString();
                    }
                }
            }
            catch { }
        }
        private void QLDH_datagrid_Chitietdon_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            try
            {
                if (QLDH_datagrid_Chitietdon.SelectedIndex.ToString() != null)
                {
                    DataRowView rowview = (DataRowView)QLDH_datagrid_Chitietdon.SelectedItem;
                    if (rowview != null)
                    {
                        QLDH_tb_danhgia.Text = rowview["Danhgia"].ToString();
                    }
                }
            }
            catch { }
        }
        private void QLDH_tb_danhgia_TextChanged(object sender, TextChangedEventArgs e)
        {
            try
            {
                int rate = Int32.Parse(QLDH_tb_danhgia.Text);
                if (rate > 5)
                    QLDH_tb_danhgia.Text = "5";
                else
                if (rate < 1)
                    QLDH_tb_danhgia.Text = "1";
                db.sql_select("update chitietdondathang set DanhGia = " + rate.ToString() + " where MaDonHang = '" + ((DataRowView)QLDH_datagrid_Chitietdon.SelectedItem)["MaDonHang"].ToString() +
                    "' and MaMonAn = '" + ((DataRowView)QLDH_datagrid_Chitietdon.SelectedItem)["MaMonAn"].ToString() + "'");
                QLDH_load_chitietdon(((DataRowView)QLDH_datagrid_Chitietdon.SelectedItem)["MaDonHang"].ToString());
            }
            catch
            {
                QLDH_tb_danhgia.Text = "";
            }
        }
        private void QLDH_bt_huydonhang_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                int maloi = (int)db.sql_select("exec kh_HuyDonHang "+MaKhachHang+", "+ QLDH_tb_madon.Text).Rows[0][0];
                if (maloi == 0)
                {
                    // Thành công
                    GLDH_lb_errorout.Content = "Đã huỷ đơn hàng. Hoàn "+ db.sql_select("select dbo.kh_tinhgiadon('" + QLDH_tb_madon.Text+"')").Rows[0][0].ToString();
                    GLDH_lb_errorout.Background = Brushes.LightGreen;  
                    QLDH_tb_madon.Text = "";
                    QLDH_tb_tendoitac.Text = ""; 
                    QLDH_load_datagrid();
                }
                else
                {
                    //Thất bại
                    GLDH_lb_errorout.Content = "Không thể huỷ đơn hàng";
                    GLDH_lb_errorout.Background = Brushes.IndianRed;
                }    
            }
            catch { }
        }

        private void Dh_btn_mua_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                Dh_themvaogio();
                //Thêm đơn hàng
                int maloi = (int)db.sql_select("exec kh_ThemDonHang '" + DateTime.Now.ToString("HH:mm:ss") + "','" + DateTime.Now.ToString("M/d/yyyy") + "','" + 15000 + "','" + MaKhachHang + "'").Rows[0][0];
                if (maloi == 0)
                {
                    Dh_lb_errorout.Content = "Thêm thành công";
                    Dh_lb_errorout.Background = Brushes.LightGreen;
                }
                return;

            }
            catch { }
            Dh_lb_errorout.Content = "Không thể đặt hàng";
            Dh_lb_errorout.Background = Brushes.IndianRed;
        }                 
    }
}