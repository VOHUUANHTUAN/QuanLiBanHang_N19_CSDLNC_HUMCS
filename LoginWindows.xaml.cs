using System;
using System.Collections.Generic;
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
    /// Interaction logic for LoginWindows.xaml
    /// </summary>       
    public partial class LoginWindows : Window
    {
        DBConnect db = new DBConnect();
        public LoginWindows(string username)
        {
            InitializeComponent();
            tb_username.Text = username;
        }

        //animation cửa số đăng nhập
        private void tb_username_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (!string.IsNullOrEmpty(tb_username.Text) && tb_username.Text.Length > 0)
            {
                lb_username.Visibility = Visibility.Collapsed;
            }
            else
            {
                lb_username.Visibility = Visibility.Visible;
            }
        }
        private void Hd_HienMaLoi_Label(int ketqua)
        {
            if (ketqua > 0)
            {
                // đăng nhập thành công    
                login_lb_error.Content = "Đăng nhập thành công";
                login_lb_error.Foreground = Brushes.DarkGreen;
            }
            else
            {
                // đăng nhập thất bại
                switch (ketqua)
                {
                    case -1:
                        login_lb_error.Content = "Có trường trống";
                        break;
                    case -2:
                        login_lb_error.Content = "username không được chứa khoảng trắng";
                        break;
                    case -3:
                        login_lb_error.Content = "password không được chứa khoảng trắng";
                        break;
                    case -4:
                        login_lb_error.Content = "username hoặc password sai";
                        break;
                    case -5:
                        login_lb_error.Content = "Tài khoản bị khoá";
                        break;
                    case 0:
                        login_lb_error.Content = "Lỗi hệ thống";
                        break;
                    default:
                        break;
                }
                login_lb_error.Foreground = Brushes.IndianRed;
            }
        }
        private void Login()
        {
            try
            {
                string query = "exec DangNhap '" + tb_username.Text + "', '" + tb_password.Password + "'";
                int kq = (int)db.sql_select(query).Rows[0][0];
                Hd_HienMaLoi_Label(kq);
                switch (kq)
                {
                    case 1:
                        //Cửa sổ Đối Tác
                        DoiTacWindow dt= new DoiTacWindow(tb_username.Text);
                        this.Close();    
                        dt.Show();
                        break;
                    case 2:
                        //Cửa sổ Tài Xế      
                        TaiXe_Window tx = new TaiXe_Window(tb_username.Text);
                        this.Close();
                        tx.Show();
                        break;
                    case 3:
                        //Cửa sổ Khách Hàng   
                        KhachHangWindow kh = new KhachHangWindow(tb_username.Text);
                        this.Close();
                        kh.Show();
                        break;
                    case 10:
                        //Cửa sổ Quản trị viên     
                        QuanTri_Window qt = new QuanTri_Window(tb_username.Text);
                        this.Close();
                        qt.Show();
                        break;
                    case 11:
                        //Cửa sổ Nhân Viên    
                        NhanVien_Window nv = new NhanVien_Window(tb_username.Text);
                        this.Close();
                        nv.Show();
                        break;
                    default:
                        break;
                }
            }
            catch { }

        }
        private void Label_MouseDown_dangky(object sender, MouseButtonEventArgs e)
        {
            DangKyWindow signup = new DangKyWindow();
            this.Hide();
            signup.ShowDialog();
            this.Show();
        }                     
        private void Login_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                Login();
            }
        }

        private void DangNhap_Click(object sender, RoutedEventArgs e)
        {
            Login();
        }

        private void tb_password_TextChanged(object sender, RoutedEventArgs e)
        {
            if (!string.IsNullOrEmpty(tb_password.Password) && tb_password.Password.Length > 0)
            {
                lb_password.Visibility = Visibility.Collapsed;
            }
            else
            {
                lb_password.Visibility = Visibility.Visible;
            }
        }
    }
}
