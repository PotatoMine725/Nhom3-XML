using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;

namespace QuanLyCuaHangThuCung.App_Code
{
    public class NhaCungCap
    {
        FileXml Fxml = new FileXml();

        // Kiểm tra mã nhà cung cấp đã tồn tại chưa
        public bool KiemTraMaNCC(string MaNCC)
        {
            DataTable dt = Fxml.HienThi("NhaCungCap.xml");
            dt.DefaultView.RowFilter = "MaNCC = '" + MaNCC + "'";
            
            if (dt.DefaultView.Count > 0)
                return true;
            return false;
        }

        // Thêm nhà cung cấp mới
        public void ThemNhaCungCap(string MaNCC, string TenNCC, string DiaChi, string SDT, string Email, string MoTa)
        {
            string noiDung = "<NhaCungCap>" +
                    "<MaNCC>" + MaNCC + "</MaNCC>" +
                    "<TenNCC>" + TenNCC + "</TenNCC>" +
                    "<DiaChi>" + DiaChi + "</DiaChi>" +
                    "<SDT>" + SDT + "</SDT>" +
                    "<Email>" + Email + "</Email>" +
                    "<MoTa>" + MoTa + "</MoTa>" +
                    "</NhaCungCap>";
            
            Fxml.Them("NhaCungCap.xml", noiDung);
            
            // Đồng bộ vào SQL
            string sql = "INSERT INTO NhaCungCap (MaNCC, TenNCC, DiaChi, SDT, Email, MoTa) " +
                "VALUES (N'" + MaNCC + "', N'" + TenNCC + "', N'" + DiaChi + "', N'" + SDT + "', N'" + Email + "', N'" + MoTa + "')";
            Fxml.InsertOrUpDateSQL(sql);
        }

        // Sửa thông tin nhà cung cấp
        public void SuaNhaCungCap(string MaNCC, string TenNCC, string DiaChi, string SDT, string Email, string MoTa)
        {
            string noiDung = "<MaNCC>" + MaNCC + "</MaNCC>" +
                    "<TenNCC>" + TenNCC + "</TenNCC>" +
                    "<DiaChi>" + DiaChi + "</DiaChi>" +
                    "<SDT>" + SDT + "</SDT>" +
                    "<Email>" + Email + "</Email>" +
                    "<MoTa>" + MoTa + "</MoTa>";

            Fxml.Sua("NhaCungCap.xml", "NhaCungCap", "MaNCC", MaNCC, noiDung);
            
            // Cập nhật trong SQL
            string sql = "UPDATE NhaCungCap SET TenNCC = N'" + TenNCC + "', DiaChi = N'" + DiaChi + 
                "', SDT = N'" + SDT + "', Email = N'" + Email + "', MoTa = N'" + MoTa + 
                "' WHERE MaNCC = N'" + MaNCC + "'";
            Fxml.InsertOrUpDateSQL(sql);
        }

        // Xóa nhà cung cấp
        public void XoaNhaCungCap(string MaNCC)
        {
            Fxml.Xoa("NhaCungCap.xml", "NhaCungCap", "MaNCC", MaNCC);
            
            // Xóa trong SQL
            string sql = "DELETE FROM NhaCungCap WHERE MaNCC = N'" + MaNCC + "'";
            Fxml.InsertOrUpDateSQL(sql);
        }
    }
}

