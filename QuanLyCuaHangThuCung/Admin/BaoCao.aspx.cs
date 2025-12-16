using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using QuanLyCuaHangThuCung.App_Code;

namespace QuanLyCuaHangThuCung.Admin
{
    public partial class BaoCao : BasePage
    {
        FileXml fxml = new FileXml();

        protected void Page_Load(object sender, EventArgs e)
        {
            // Kiểm tra quyền Admin
            if (!IsAdmin())
            {
                Response.Redirect("~/Login.aspx");
            }

            if (!IsPostBack)
            {
                LoadThongKe();
            }
        }

        private void LoadThongKe()
        {
            // Tổng doanh thu
            DataTable dtDT = fxml.GetDataFromSQL("SELECT ISNULL(SUM(TongTien), 0) as TongTien FROM HoaDon");
            if (dtDT.Rows.Count > 0)
            {
                lblTongDoanhThu.Text = string.Format("{0:N0}", dtDT.Rows[0]["TongTien"]) + " đ";
            }

            // Tổng đơn hàng
            DataTable dtDH = fxml.GetDataFromSQL("SELECT COUNT(*) as SoLuong FROM HoaDon");
            if (dtDH.Rows.Count > 0)
            {
                lblTongDonHang.Text = dtDH.Rows[0]["SoLuong"].ToString();
            }

            // Tổng khách hàng
            DataTable dtKH = fxml.GetDataFromSQL("SELECT COUNT(*) as SoLuong FROM KhachHang");
            if (dtKH.Rows.Count > 0)
            {
                lblTongKhachHang.Text = dtKH.Rows[0]["SoLuong"].ToString();
            }

            // Tổng thú cưng
            DataTable dtTC = fxml.GetDataFromSQL("SELECT COUNT(*) as SoLuong FROM ThuCung");
            if (dtTC.Rows.Count > 0)
            {
                lblTongThuCung.Text = dtTC.Rows[0]["SoLuong"].ToString();
            }

            // Top 5 sản phẩm bán chạy
            string sqlTopSP = @"SELECT TOP 5 t.TenThuCung, 
                SUM(c.SoLuong) as TongSoLuong,
                SUM(c.DonGia * c.SoLuong) as TongTien
                FROM ChiTietHoaDon c
                INNER JOIN ThuCung t ON c.MaThuCung = t.MaThuCung
                GROUP BY t.TenThuCung
                ORDER BY SUM(c.SoLuong) DESC";
            DataTable dtTopSP = fxml.GetDataFromSQL(sqlTopSP);
            gvTopSanPham.DataSource = dtTopSP;
            gvTopSanPham.DataBind();

            // Top 5 khách hàng
            string sqlTopKH = @"SELECT TOP 5 k.TenKhachHang,
                COUNT(h.SoHoaDon) as SoDonHang,
                SUM(h.TongTien) as TongTien
                FROM HoaDon h
                INNER JOIN KhachHang k ON h.MaKhachHang = k.MaKhachHang
                GROUP BY k.TenKhachHang
                ORDER BY SUM(h.TongTien) DESC";
            DataTable dtTopKH = fxml.GetDataFromSQL(sqlTopKH);
            gvTopKhachHang.DataSource = dtTopKH;
            gvTopKhachHang.DataBind();
        }
    }
}

