using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;

namespace QuanLyCuaHangThuCung.App_Code
{
    public class HoaDon
    {
        FileXml Fxml = new FileXml();

        // Tạo hóa đơn mới
        public int TaoHoaDon(string MaNhanVien, string MaKhachHang, string NgayLap, int TongTien)
        {
            // Lấy số hóa đơn tiếp theo
            DataTable dt = Fxml.GetDataFromSQL("SELECT ISNULL(MAX(SoHoaDon), 0) + 1 as SoHoaDonMoi FROM HoaDon");
            int soHoaDon = 1;
            if (dt.Rows.Count > 0)
            {
                soHoaDon = Convert.ToInt32(dt.Rows[0]["SoHoaDonMoi"]);
            }

            // Thêm hóa đơn vào XML
            string noiDung = "<HoaDon>" +
                    "<SoHoaDon>" + soHoaDon + "</SoHoaDon>" +
                    "<MaNhanVien>" + MaNhanVien + "</MaNhanVien>" +
                    "<MaKhachHang>" + MaKhachHang + "</MaKhachHang>" +
                    "<NgayLap>" + NgayLap + "</NgayLap>" +
                    "<TongTien>" + TongTien + "</TongTien>" +
                    "</HoaDon>";
            
            Fxml.Them("HoaDon.xml", noiDung);
            
            // Thêm vào SQL
            string sql = "INSERT INTO HoaDon (SoHoaDon, MaNhanVien, MaKhachHang, NgayLap, TongTien) " +
                "VALUES (" + soHoaDon + ", N'" + MaNhanVien + "', N'" + MaKhachHang + "', N'" + NgayLap + "', " + TongTien + ")";
            Fxml.InsertOrUpDateSQL(sql);

            return soHoaDon;
        }

        // Thêm chi tiết hóa đơn
        public void ThemChiTietHoaDon(int SoHoaDon, string MaThuCung, int DonGia, int SoLuong)
        {
            // Lấy ID tiếp theo
            DataTable dt = Fxml.GetDataFromSQL("SELECT ISNULL(MAX(Id), 0) + 1 as IdMoi FROM ChiTietHoaDon");
            int id = 1;
            if (dt.Rows.Count > 0)
            {
                id = Convert.ToInt32(dt.Rows[0]["IdMoi"]);
            }

            string noiDung = "<ChiTietHoaDon>" +
                    "<Id>" + id + "</Id>" +
                    "<MaThuCung>" + MaThuCung + "</MaThuCung>" +
                    "<SoHoaDon>" + SoHoaDon + "</SoHoaDon>" +
                    "<DonGia>" + DonGia + "</DonGia>" +
                    "<SoLuong>" + SoLuong + "</SoLuong>" +
                    "</ChiTietHoaDon>";
            
            Fxml.Them("ChiTietHoaDon.xml", noiDung);
            
            // Thêm vào SQL
            string sql = "INSERT INTO ChiTietHoaDon (Id, MaThuCung, SoHoaDon, DonGia, SoLuong) " +
                "VALUES (" + id + ", N'" + MaThuCung + "', " + SoHoaDon + ", " + DonGia + ", " + SoLuong + ")";
            Fxml.InsertOrUpDateSQL(sql);

            // Cập nhật số lượng thú cưng
            string sqlUpdate = "UPDATE ThuCung SET SoLuong = SoLuong - " + SoLuong + " WHERE MaThuCung = N'" + MaThuCung + "'";
            Fxml.InsertOrUpDateSQL(sqlUpdate);
        }

        // Lấy thông tin hóa đơn
        public DataTable LayHoaDon(int SoHoaDon)
        {
            return Fxml.GetDataFromSQL("SELECT * FROM HoaDon WHERE SoHoaDon = " + SoHoaDon);
        }

        // Lấy chi tiết hóa đơn
        public DataTable LayChiTietHoaDon(int SoHoaDon)
        {
            return Fxml.GetDataFromSQL("SELECT c.*, t.TenThuCung, t.Loai, t.Giong FROM ChiTietHoaDon c " +
                "INNER JOIN ThuCung t ON c.MaThuCung = t.MaThuCung WHERE c.SoHoaDon = " + SoHoaDon);
        }
    }
}

