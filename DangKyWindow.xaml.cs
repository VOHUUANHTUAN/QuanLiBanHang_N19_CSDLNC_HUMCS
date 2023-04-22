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
    /// Interaction logic for DangKyWindow.xaml
    /// </summary>
    public partial class DangKyWindow : Window
    {
        int LuaChon = 1;
        DBConnect db = new DBConnect();
        // 1. Đối tác
        // 2. Khách hàng
        // 3. Tài xế
        Brush lightCyan = new BrushConverter().ConvertFromString("#95DBD9") as SolidColorBrush;
        Brush darkCyan = new BrushConverter().ConvertFromString("#79B1BB") as SolidColorBrush;
        public DangKyWindow()
        {
            InitializeComponent();
            tb_sodu.Text = "400000000";
            lb_custom_4.Visibility = Visibility.Hidden;
            tb_custom_4.Visibility = Visibility.Hidden;
        }

        public void HienMaLoi(int code)
        {
            if (code == 0)
            {
                lb_errorout.Content = "Đăng ký thành công";
                lb_errorout.Background = Brushes.LightGreen;
                return;
            }    
            switch (code)
            {
                case 1:
                    lb_errorout.Content = "Có trường trống";
                    break;
                case 2:
                    lb_errorout.Content = "username đã tồn tại";
                    break;
                case 3:
                    lb_errorout.Content = "username và pass không được chứa khoảng trắng";
                    break;
                case 4:
                    lb_errorout.Content = "Số tài khoản đã tồn tại";
                    break;
                case 10:
                    lb_errorout.Content = "Dữ liệu nhập bị lỗi";
                    break;
            }
            lb_errorout.Background = Brushes.IndianRed;
        }
        private int QuerryRegister(string loai)
        {
            try
            {
                string query = "Exec dangky_" + loai + " '"
                    + tb_username.Text + "','" + tb_password.Text + "',N'" + tb_ten.Text + "','" +
                    tb_sdt.Text + "','" + tb_email.Text + "',N'" + tb_nganhang.Text + "','" +
                    tb_sotaikhoan.Text + "','" + tb_sodu.Text + "',N'" + tb_custom_1.Text + "',N'" +
                    tb_custom_2.Text + "',N'" + tb_custom_3.Text + "'";
                switch (loai.ToLower())
                {
                    case "khachhang":
                        query = query + " ,'" + tb_cmnd.Text + "'";
                        break;
                    case "taixe":
                        query = query + " ,'" + tb_cmnd.Text + "', '" + tb_custom_4.Text + "'";
                        break;
                }

                return (int)db.sql_select(query).Rows[0][0];
            }
            catch
            {
                return 10;
            }
        }
        private void DangKy_Click(object sender, RoutedEventArgs e)
        {
            string role = "doitac";
            switch (LuaChon)
            {
                case 1:
                    role = "doitac";
                    break;
                case 2:
                    role = "taixe";
                    break;
                case 3:
                    role = "khachhang";
                    break;

            }
            int ErrorCode = QuerryRegister(role);
            HienMaLoi(ErrorCode);

        }

        private void choose_doitac_Click(object sender, RoutedEventArgs e)
        {
            LuaChon = 1;
            tb_sodu.Text = "40000000";
            choose_doitac_BackGround.Background = lightCyan;
            choose_taixe_BackGround.Background = darkCyan;
            choose_khachhang_BackGround.Background = darkCyan;
            lb_custom_1.Content = "Tên quán";
            lb_custom_2.Content = "Mã số thuế";
            lb_custom_3.Content = "Loại ẩm thực";
            lb_custom_4.Visibility = Visibility.Hidden;
            tb_custom_4.Visibility = Visibility.Hidden;
        }

        private object BrushConverter()
        {
            throw new NotImplementedException();
        }

        private void choose_khachhang_Click(object sender, RoutedEventArgs e)
        {
            LuaChon = 3;
            tb_sodu.Text = "5000000";
            choose_doitac_BackGround.Background = darkCyan;
            choose_taixe_BackGround.Background = darkCyan;
            choose_khachhang_BackGround.Background = lightCyan;
            lb_custom_1.Content = "Tỉnh";
            lb_custom_2.Content = "Quận";
            lb_custom_3.Content = "Địa chỉ";
            lb_custom_4.Visibility = Visibility.Hidden;
            tb_custom_4.Visibility = Visibility.Hidden;
        }

        private void choose_taixe_Click(object sender, RoutedEventArgs e)
        {
            LuaChon = 2;
            tb_sodu.Text = "3000000";
            choose_doitac_BackGround.Background = darkCyan;
            choose_taixe_BackGround.Background = lightCyan;
            choose_khachhang_BackGround.Background = darkCyan;
            lb_custom_1.Content = "Tỉnh";
            lb_custom_2.Content = "Quận";
            lb_custom_3.Content = "Địa chỉ";
            lb_custom_4.Visibility = Visibility.Visible;
            tb_custom_4.Visibility = Visibility.Visible;
        }

        private void Back_Click(object sender, RoutedEventArgs e)
        {

            LoginWindows lg = new LoginWindows(tb_username.Text);
            this.Close();
            lg.Show();
        }
    }
}
