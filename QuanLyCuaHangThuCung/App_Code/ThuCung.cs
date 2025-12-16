using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Xml;

namespace QuanLyCuaHangThuCung.App_Code
{
    public class ThuCung
    {
        FileXml Fxml = new FileXml();

        // Kiểm tra mã thú cưng đã tồn tại chưa
        public bool KiemTraMaThuCung(string MaThuCung)
        {
            System.Data.DataTable dt = Fxml.HienThi("ThuCung.xml");
            dt.DefaultView.RowFilter = "MaThuCung = '" + MaThuCung + "'";
            
            if (dt.DefaultView.Count > 0)
                return true;
            return false;
        }

        // Thêm thú cưng mới
        public void ThemThuCung(string MaThuCung, string TenThuCung, string Loai, string Giong, 
            int Tuoi, string GioiTinh, int Gia, string TinhTrangSucKhoe, string MaNCC, int SoLuong, string MoTa)
        {
            string noiDung = "<ThuCung>" +
                    "<MaThuCung>" + MaThuCung + "</MaThuCung>" +
                    "<TenThuCung>" + TenThuCung + "</TenThuCung>" +
                    "<Loai>" + Loai + "</Loai>" +
                    "<Giong>" + Giong + "</Giong>" +
                    "<Tuoi>" + Tuoi + "</Tuoi>" +
                    "<GioiTinh>" + GioiTinh + "</GioiTinh>" +
                    "<Gia>" + Gia + "</Gia>" +
                    "<TinhTrangSucKhoe>" + TinhTrangSucKhoe + "</TinhTrangSucKhoe>" +
                    "<MaNCC>" + MaNCC + "</MaNCC>" +
                    "<SoLuong>" + SoLuong + "</SoLuong>" +
                    "<MoTa>" + MoTa + "</MoTa>" +
                    "</ThuCung>";
            
            Fxml.Them("ThuCung.xml", noiDung);
            
            // Đồng bộ vào SQL
            string sql = "INSERT INTO ThuCung (MaThuCung, TenThuCung, Loai, Giong, Tuoi, GioiTinh, Gia, TinhTrangSucKhoe, MaNCC, SoLuong, MoTa) " +
                "VALUES (N'" + MaThuCung + "', N'" + TenThuCung + "', N'" + Loai + "', N'" + Giong + "', " + Tuoi + 
                ", N'" + GioiTinh + "', " + Gia + ", N'" + TinhTrangSucKhoe + "', N'" + MaNCC + "', " + SoLuong + ", N'" + MoTa + "')";
            Fxml.InsertOrUpDateSQL(sql);
        }

        // Sửa thông tin thú cưng
        public void SuaThuCung(string MaThuCung, string TenThuCung, string Loai, string Giong, 
            int Tuoi, string GioiTinh, int Gia, string TinhTrangSucKhoe, string MaNCC, int SoLuong, string MoTa)
        {
            string noiDung = "<MaThuCung>" + MaThuCung + "</MaThuCung>" +
                    "<TenThuCung>" + TenThuCung + "</TenThuCung>" +
                    "<Loai>" + Loai + "</Loai>" +
                    "<Giong>" + Giong + "</Giong>" +
                    "<Tuoi>" + Tuoi + "</Tuoi>" +
                    "<GioiTinh>" + GioiTinh + "</GioiTinh>" +
                    "<Gia>" + Gia + "</Gia>" +
                    "<TinhTrangSucKhoe>" + TinhTrangSucKhoe + "</TinhTrangSucKhoe>" +
                    "<MaNCC>" + MaNCC + "</MaNCC>" +
                    "<SoLuong>" + SoLuong + "</SoLuong>" +
                    "<MoTa>" + MoTa + "</MoTa>";

            Fxml.Sua("ThuCung.xml", "ThuCung", "MaThuCung", MaThuCung, noiDung);
            
            // Cập nhật trong SQL
            string sql = "UPDATE ThuCung SET TenThuCung = N'" + TenThuCung + "', Loai = N'" + Loai + 
                "', Giong = N'" + Giong + "', Tuoi = " + Tuoi + ", GioiTinh = N'" + GioiTinh + 
                "', Gia = " + Gia + ", TinhTrangSucKhoe = N'" + TinhTrangSucKhoe + 
                "', MaNCC = N'" + MaNCC + "', SoLuong = " + SoLuong + ", MoTa = N'" + MoTa + 
                "' WHERE MaThuCung = N'" + MaThuCung + "'";
            Fxml.InsertOrUpDateSQL(sql);
        }

        // Xóa thú cưng
        public void XoaThuCung(string MaThuCung)
        {
            Fxml.Xoa("ThuCung.xml", "ThuCung", "MaThuCung", MaThuCung);
            
            // Xóa trong SQL
            string sql = "DELETE FROM ThuCung WHERE MaThuCung = N'" + MaThuCung + "'";
            Fxml.InsertOrUpDateSQL(sql);
        }
    }
}

