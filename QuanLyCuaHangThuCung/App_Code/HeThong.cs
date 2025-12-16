using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;

namespace QuanLyCuaHangThuCung.App_Code
{
    public class HeThong
    {
        FileXml Fxml = new FileXml();

        // Tạo XML từ tất cả các bảng
        public void TaoXML()
        {
            Fxml.TaoXML("ChamCong");
            Fxml.TaoXML("ChiTietHoaDon");
            Fxml.TaoXML("ThuCung");
            Fxml.TaoXML("HoaDon");
            Fxml.TaoXML("NhanVien");
            Fxml.TaoXML("NhaCungCap");
            Fxml.TaoXML("PhieuNhap");
            Fxml.TaoXML("TaiKhoan");
            Fxml.TaoXML("KhachHang");
            Fxml.TaoXML("DichVu");
            Fxml.TaoXML("DatLichDichVu");
        }

        // Cập nhật từng bảng từ XML vào SQL
        void CapNhapTungBang(string tenBang)
        {
            string duongDan = tenBang + ".xml";
            DataTable table = Fxml.HienThi(duongDan);
            
            for (int i = 0; i < table.Rows.Count; i++)
            {
                string sql = "INSERT INTO " + tenBang + " VALUES(";
                for (int j = 0; j < table.Columns.Count - 1; j++)
                {
                    sql += "N'" + table.Rows[i][j].ToString().Trim().Replace("'", "''") + "',";
                }
                sql += "N'" + table.Rows[i][table.Columns.Count - 1].ToString().Trim().Replace("'", "''") + "'";
                sql += ")";
                
                try
                {
                    Fxml.InsertOrUpDateSQL(sql);
                }
                catch
                {
                    // Bỏ qua nếu đã tồn tại
                }
            }
        }

        // Cập nhật toàn bộ dữ liệu từ XML vào SQL
        public void CapNhapSQL()
        {
            // Xóa toàn bộ dữ liệu các bảng (cẩn thận khi sử dụng)
            Fxml.InsertOrUpDateSQL("DELETE FROM DatLichDichVu");
            Fxml.InsertOrUpDateSQL("DELETE FROM ChamCong");
            Fxml.InsertOrUpDateSQL("DELETE FROM ChiTietHoaDon");
            Fxml.InsertOrUpDateSQL("DELETE FROM HoaDon");
            Fxml.InsertOrUpDateSQL("DELETE FROM PhieuNhap");
            Fxml.InsertOrUpDateSQL("DELETE FROM ThuCung");
            Fxml.InsertOrUpDateSQL("DELETE FROM KhachHang");
            Fxml.InsertOrUpDateSQL("DELETE FROM NhaCungCap");
            Fxml.InsertOrUpDateSQL("DELETE FROM DichVu");
            Fxml.InsertOrUpDateSQL("DELETE FROM NhanVien");
            Fxml.InsertOrUpDateSQL("DELETE FROM TaiKhoan");

            // Cập nhật toàn bộ dữ liệu các bảng
            CapNhapTungBang("TaiKhoan");
            CapNhapTungBang("NhanVien");
            CapNhapTungBang("NhaCungCap");
            CapNhapTungBang("ThuCung");
            CapNhapTungBang("KhachHang");
            CapNhapTungBang("DichVu");
            CapNhapTungBang("HoaDon");
            CapNhapTungBang("ChiTietHoaDon");
            CapNhapTungBang("PhieuNhap");
            CapNhapTungBang("ChamCong");
            CapNhapTungBang("DatLichDichVu");
        }

        // Đồng bộ hóa 2 chiều (XML ↔ SQL)
        public void DongBoHoa()
        {
            // Bước 1: Xuất SQL → XML
            TaoXML();
            
            // Bước 2: Import XML → SQL (nếu cần)
            // CapNhapSQL(); // Chỉ dùng khi cần thiết
        }
    }
}

