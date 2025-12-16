using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Xml;
using System.Data.SqlClient;
using System.Data;

namespace QuanLyCuaHangThuCung.App_Code
{
    public class DangNhap
    {
        FileXml Fxml = new FileXml();
        string Conn = @"Data Source=Potato;Initial Catalog=QuanLyCuaHangThuCung;Integrated Security=True";

        // Kiểm tra đăng nhập và trả về quyền
        public int KiemTraDangNhap(string MaNhanVien, string MatKhau)
        {
            // Kiểm tra từ SQL Server
            SqlConnection con = new SqlConnection(Conn);
            try
            {
                con.Open();
                string sql = "SELECT Quyen FROM TaiKhoan WHERE MaNhanVien = @MaNV AND MatKhau = @MatKhau";
                SqlCommand cmd = new SqlCommand(sql, con);
                cmd.Parameters.AddWithValue("@MaNV", MaNhanVien);
                cmd.Parameters.AddWithValue("@MatKhau", MatKhau);
                
                object result = cmd.ExecuteScalar();
                if (result != null)
                {
                    return Convert.ToInt32(result); // 0 = Nhân viên, 1 = Admin
                }
                return -1; // Không tìm thấy tài khoản
            }
            catch
            {
                return -1;
            }
            finally
            {
                if (con.State == System.Data.ConnectionState.Open)
                    con.Close();
            }
        }

        // Kiểm tra đăng nhập từ XML
        public bool KiemTraDangNhapXML(string MaNhanVien, string MatKhau)
        {
            DataTable dt = Fxml.HienThi("TaiKhoan.xml");
            dt.DefaultView.RowFilter = "MaNhanVien = '" + MaNhanVien + "' AND MatKhau = '" + MatKhau + "'";
            
            if (dt.DefaultView.Count > 0)
                return true;
            return false;
        }

        // Lấy quyền từ XML
        public int LayQuyen(string MaNhanVien)
        {
            string quyenStr = Fxml.LayGiaTri("TaiKhoan.xml", "MaNhanVien", MaNhanVien, "Quyen");
            if (!string.IsNullOrEmpty(quyenStr))
            {
                return Convert.ToInt32(quyenStr);
            }
            return -1;
        }

        // Lấy quyền từ SQL
        public int LayQuyenSQL(string MaNhanVien)
        {
            SqlConnection con = new SqlConnection(Conn);
            try
            {
                con.Open();
                string sql = "SELECT Quyen FROM TaiKhoan WHERE MaNhanVien = @MaNV";
                SqlCommand cmd = new SqlCommand(sql, con);
                cmd.Parameters.AddWithValue("@MaNV", MaNhanVien);
                
                object result = cmd.ExecuteScalar();
                if (result != null)
                {
                    return Convert.ToInt32(result);
                }
                return -1;
            }
            catch
            {
                return -1;
            }
            finally
            {
                if (con.State == System.Data.ConnectionState.Open)
                    con.Close();
            }
        }

        // Đăng ký tài khoản mới
        public void DangKiTaiKhoan(string MaNhanVien, string MatKhau, int Quyen)
        {
            string noiDung = "<TaiKhoan>" +
                    "<MaNhanVien>" + MaNhanVien + "</MaNhanVien>" +
                    "<MatKhau>" + MatKhau + "</MatKhau>" +
                    "<Quyen>" + Quyen + "</Quyen>" +
                    "</TaiKhoan>";
            
            Fxml.Them("TaiKhoan.xml", noiDung);
            
            // Đồng bộ vào SQL
            string sql = "INSERT INTO TaiKhoan (MaNhanVien, MatKhau, Quyen) VALUES (N'" + MaNhanVien + "', N'" + MatKhau + "', " + Quyen + ")";
            Fxml.InsertOrUpDateSQL(sql);
        }

        // Xóa tài khoản
        public void XoaTaiKhoan(string MaNhanVien)
        {
            Fxml.Xoa("TaiKhoan.xml", "TaiKhoan", "MaNhanVien", MaNhanVien);
            
            // Xóa trong SQL
            string sql = "DELETE FROM TaiKhoan WHERE MaNhanVien = N'" + MaNhanVien + "'";
            Fxml.InsertOrUpDateSQL(sql);
        }

        // Kiểm tra tài khoản đã tồn tại chưa
        public bool KiemTraTaiKhoanTonTai(string MaNhanVien)
        {
            DataTable dt = Fxml.HienThi("TaiKhoan.xml");
            dt.DefaultView.RowFilter = "MaNhanVien = '" + MaNhanVien + "'";
            
            if (dt.DefaultView.Count > 0)
                return true;
            return false;
        }

        // Đổi mật khẩu
        public void DoiMatKhau(string nguoiDung, string matKhauMoi)
        {
            Fxml.DoiMatKhau(nguoiDung, matKhauMoi);
            
            // Cập nhật trong SQL
            string sql = "UPDATE TaiKhoan SET MatKhau = N'" + matKhauMoi + "' WHERE MaNhanVien = N'" + nguoiDung + "'";
            Fxml.InsertOrUpDateSQL(sql);
        }

        // Lấy tên nhân viên từ mã nhân viên
        public string LayTenNhanVien(string MaNhanVien)
        {
            return Fxml.LayGiaTri("NhanVien.xml", "MaNhanVien", MaNhanVien, "TenNhanVien");
        }
    }
}

