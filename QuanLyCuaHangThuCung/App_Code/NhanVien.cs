using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Xml;

namespace QuanLyCuaHangThuCung.App_Code
{
    public class NhanVien
    {
        FileXml Fxml = new FileXml();

        // Kiểm tra mã nhân viên đã tồn tại chưa
        public bool KiemTraMaNhanVien(string MaNhanVien)
        {
            System.Data.DataTable dt = Fxml.HienThi("NhanVien.xml");
            dt.DefaultView.RowFilter = "MaNhanVien = '" + MaNhanVien + "'";
            
            if (dt.DefaultView.Count > 0)
                return true;
            return false;
        }

        // Thêm nhân viên mới
        public void ThemNhanVien(string MaNhanVien, string TenNhanVien, string NgaySinh, 
            string DiaChi, string SDT, string Email, string ChucVu, string TrangThai)
        {
            string noiDung = "<NhanVien>" +
                    "<MaNhanVien>" + MaNhanVien + "</MaNhanVien>" +
                    "<TenNhanVien>" + TenNhanVien + "</TenNhanVien>" +
                    "<NgaySinh>" + NgaySinh + "</NgaySinh>" +
                    "<DiaChi>" + DiaChi + "</DiaChi>" +
                    "<SDT>" + SDT + "</SDT>" +
                    "<Email>" + Email + "</Email>" +
                    "<ChucVu>" + ChucVu + "</ChucVu>" +
                    "<TrangThai>" + TrangThai + "</TrangThai>" +
                    "</NhanVien>";
            
            Fxml.Them("NhanVien.xml", noiDung);
            
            // Đồng bộ vào SQL
            string sql = "INSERT INTO NhanVien (MaNhanVien, TenNhanVien, NgaySinh, DiaChi, SDT, Email, ChucVu, TrangThai) " +
                "VALUES (N'" + MaNhanVien + "', N'" + TenNhanVien + "', N'" + NgaySinh + "', N'" + DiaChi + "', N'" + SDT + "', N'" + Email + "', N'" + ChucVu + "', N'" + TrangThai + "')";
            Fxml.InsertOrUpDateSQL(sql);
        }

        // Sửa thông tin nhân viên
        public void SuaNhanVien(string MaNhanVien, string TenNhanVien, string NgaySinh, 
            string DiaChi, string SDT, string Email, string ChucVu, string TrangThai)
        {
            string noiDung = "<MaNhanVien>" + MaNhanVien + "</MaNhanVien>" +
                    "<TenNhanVien>" + TenNhanVien + "</TenNhanVien>" +
                    "<NgaySinh>" + NgaySinh + "</NgaySinh>" +
                    "<DiaChi>" + DiaChi + "</DiaChi>" +
                    "<SDT>" + SDT + "</SDT>" +
                    "<Email>" + Email + "</Email>" +
                    "<ChucVu>" + ChucVu + "</ChucVu>" +
                    "<TrangThai>" + TrangThai + "</TrangThai>";

            Fxml.Sua("NhanVien.xml", "NhanVien", "MaNhanVien", MaNhanVien, noiDung);
            
            // Cập nhật trong SQL
            string sql = "UPDATE NhanVien SET TenNhanVien = N'" + TenNhanVien + "', NgaySinh = N'" + NgaySinh + 
                "', DiaChi = N'" + DiaChi + "', SDT = N'" + SDT + "', Email = N'" + Email + 
                "', ChucVu = N'" + ChucVu + "', TrangThai = N'" + TrangThai + 
                "' WHERE MaNhanVien = N'" + MaNhanVien + "'";
            Fxml.InsertOrUpDateSQL(sql);
        }

        // Xóa nhân viên
        public void XoaNhanVien(string MaNhanVien)
        {
            Fxml.Xoa("NhanVien.xml", "NhanVien", "MaNhanVien", MaNhanVien);
            
            // Xóa trong SQL
            string sql = "DELETE FROM NhanVien WHERE MaNhanVien = N'" + MaNhanVien + "'";
            Fxml.InsertOrUpDateSQL(sql);
        }
    }
}

