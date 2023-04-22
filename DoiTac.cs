using System.Data;

namespace Csdhnc_N19_BanHang.DBClass
{
    public class DoiTac
    {
        DBConnect dbconnect = new DBConnect();

        public DataTable LayThongTinDoiTac(string MaDoiTac)
        {

            string sql = "select * ," +
                "(select count(MaChiNhanh) from CHINHANH where MaDoiTac = " + MaDoiTac + ")SLChiNhanh, " +
                "(select STK from TaiKhoanNH where MaNguoiDung = dt.MaSTK)STK, " +
                "(select TenNH from TaiKhoanNH where MaNguoiDung = dt.MaSTK)TenNganHang, " +
                "(select SoDu from TaiKhoanNH where MaNguoiDung = dt.MaSTK)SoDu " +
                "from DOITAC dt where MaDoiTac=" + MaDoiTac;
            DataTable dt = new DataTable();
            dt = dbconnect.sql_select(sql);
            return dt;
        }
        public void TT_capNhatThongTin(string MaDT, string nguoidaidien, string tenquan, string email, string loaithucpham,string masothue)
        {
            string sql = "update DOITAC set Email='" + email + "', Tennguoidaidien= N'" + nguoidaidien + "', TenQuan=N'" + tenquan + "' , LoaiAmThuc=N'" + loaithucpham + "' , MaSoThue=" + masothue + " where MaDoiTac='" + MaDT + "'";
            dbconnect.sql_insert_update_delete(sql);
        }

        public void doiMatKhau(string passmoi, string user)
        {
            string query = "update TAIKHOAN set pass='" + passmoi + "' where username='" + user + "'";
            dbconnect.sql_insert_update_delete(query);
        }
        public string layMatKhau(string user)
        {
            string query = "select pass from TaiKhoan where Username='" + user + "'";
            return dbconnect.layMotGiaTri(query);
        }
        public int QueryChiNhanh(string LoaiQuery, string stt, string Ma, string TP, string Quan, string DiaChi, string SDT, string TT, string giomo, string giodong)
        {
            string query = "Exec dt_" + LoaiQuery + "ChiNhanh '" + Ma + "',N'" + TP +
                "',N'" + Quan + "',N'" + DiaChi
                + "','" + SDT + "','" + giomo + "','" + giodong + "',N'" + TT+"'";
            switch (LoaiQuery)
            {
                case "Xoa":
                    query = "Exec dt_XoaChiNhanh '" + Ma + "','" + stt.ToString() + "'";
                    break;
                case "Sua":
                    query = query + ",'" + stt + "'";
                    break;
                default:
                    break;
            }
            return (int)dbconnect.sql_select(query).Rows[0][0];
        }
        public int QueryThucPham(string LoaiQuery, string MaTP, string MaDT, string TenMon, string MieuTa, string gia,
            string TinhTrang, string TuyChon)
        {
            string query = "Exec dt_" + LoaiQuery + "ThucPham '" + MaDT + "',N'" + TenMon +
                "',N'" + MieuTa + "','" + gia + "',N'" + TinhTrang + "',N'" + TuyChon + "'";
            switch (LoaiQuery)
            {
                case "Xoa":
                    query = "Exec dt_XoaThucPham '" + MaTP  + "','" + MaDT + "'";
                    break;
                case "Sua":
                    query = query + ",'" + MaTP + "'";
                    break;
                default:
                    break;
            }
            return (int)dbconnect.sql_select(query).Rows[0][0];
        }


        public int QueryHopDong(string SLChiNhanh,string NgayBatDau, string ThoiHan, string MaDT)
        {
            string query = "Exec dt_ThemHopDong " + SLChiNhanh + ",'" +
                    NgayBatDau + "','" + ThoiHan + "','" + MaDT + "'";
            return (int)dbconnect.sql_select(query).Rows[0][0];
        }
        public DataTable laySoLuongDon(string kieu, string MaDT)
        {
            string sql = "Exec dt_SoLuongDonTheo" + kieu + " " + "'" + MaDT + "'";
            return dbconnect.sql_select(sql);
        }
    }
}
