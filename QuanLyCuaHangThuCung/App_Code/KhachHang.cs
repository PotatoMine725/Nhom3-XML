using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;

namespace QuanLyCuaHangThuCung.App_Code
{
    public class KhachHang
    {
        FileXml Fxml = new FileXml();

        // Kiểm tra mã khách hàng đã tồn tại chưa
        public bool KiemTraMaKhachHang(string MaKhachHang)
        {
            DataTable dt = Fxml.HienThi("KhachHang.xml");
            dt.DefaultView.RowFilter = "MaKhachHang = '" + MaKhachHang + "'";
            
            if (dt.DefaultView.Count > 0)
                return true;
            return false;
        }

        // Thêm khách hàng mới
        public void ThemKhachHang(string MaKhachHang, string TenKhachHang, string SDT, string Email, string DiaChi, string NgayDangKy)
        {
            string noiDung = "<KhachHang>" +
                    "<MaKhachHang>" + MaKhachHang + "</MaKhachHang>" +
                    "<TenKhachHang>" + TenKhachHang + "</TenKhachHang>" +
                    "<SDT>" + SDT + "</SDT>" +
                    "<Email>" + Email + "</Email>" +
                    "<DiaChi>" + DiaChi + "</DiaChi>" +
                    "<NgayDangKy>" + NgayDangKy + "</NgayDangKy>" +
                    "</KhachHang>";
            
            Fxml.Them("KhachHang.xml", noiDung);
            
            // Đồng bộ vào SQL
            string sql = "INSERT INTO KhachHang (MaKhachHang, TenKhachHang, SDT, Email, DiaChi, NgayDangKy) " +
                "VALUES (N'" + MaKhachHang + "', N'" + TenKhachHang + "', N'" + SDT + "', N'" + Email + "', N'" + DiaChi + "', N'" + NgayDangKy + "')";
            Fxml.InsertOrUpDateSQL(sql);
        }

        // Sửa thông tin khách hàng
        public void SuaKhachHang(string MaKhachHang, string TenKhachHang, string SDT, string Email, string DiaChi, string NgayDangKy)
        {
            string noiDung = "<MaKhachHang>" + MaKhachHang + "</MaKhachHang>" +
                    "<TenKhachHang>" + TenKhachHang + "</TenKhachHang>" +
                    "<SDT>" + SDT + "</SDT>" +
                    "<Email>" + Email + "</Email>" +
                    "<DiaChi>" + DiaChi + "</DiaChi>" +
                    "<NgayDangKy>" + NgayDangKy + "</NgayDangKy>";

            Fxml.Sua("KhachHang.xml", "KhachHang", "MaKhachHang", MaKhachHang, noiDung);
            
            // Cập nhật trong SQL
            string sql = "UPDATE KhachHang SET TenKhachHang = N'" + TenKhachHang + "', SDT = N'" + SDT + 
                "', Email = N'" + Email + "', DiaChi = N'" + DiaChi + "', NgayDangKy = N'" + NgayDangKy + 
                "' WHERE MaKhachHang = N'" + MaKhachHang + "'";
            Fxml.InsertOrUpDateSQL(sql);
        }

        // Xóa khách hàng
        public void XoaKhachHang(string MaKhachHang)
        {
            Fxml.Xoa("KhachHang.xml", "KhachHang", "MaKhachHang", MaKhachHang);
            
            // Xóa trong SQL
            string sql = "DELETE FROM KhachHang WHERE MaKhachHang = N'" + MaKhachHang + "'";
            Fxml.InsertOrUpDateSQL(sql);
        }
    }
}

