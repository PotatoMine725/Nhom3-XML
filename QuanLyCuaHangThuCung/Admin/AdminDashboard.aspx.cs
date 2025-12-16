using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using QuanLyCuaHangThuCung.App_Code;

namespace QuanLyCuaHangThuCung.Admin
{
    public partial class AdminDashboard : BasePage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            // Kiểm tra quyền Admin
            if (!IsAdmin())
            {
                Response.Redirect("~/Login.aspx");
            }

            if (!IsPostBack)
            {
                lblTenNhanVien.Text = "Xin chào, " + GetTenNhanVien() + " (Admin)";
                LoadThongKe();
            }
        }

        private void LoadThongKe()
        {
            FileXml fxml = new FileXml();
            
            // Đếm tổng nhân viên
            var dtNV = fxml.GetDataFromSQL("SELECT COUNT(*) as SoLuong FROM NhanVien");
            if (dtNV.Rows.Count > 0)
            {
                lblTongNhanVien.Text = dtNV.Rows[0]["SoLuong"].ToString();
            }

            // Đếm tổng thú cưng
            var dtTC = fxml.GetDataFromSQL("SELECT COUNT(*) as SoLuong FROM ThuCung");
            if (dtTC.Rows.Count > 0)
            {
                lblTongThuCung.Text = dtTC.Rows[0]["SoLuong"].ToString();
            }

            // Doanh thu hôm nay
            string ngayHômNay = DateTime.Now.ToString("dd/MM/yyyy");
            var dtDT = fxml.GetDataFromSQL("SELECT ISNULL(SUM(TongTien), 0) as TongTien FROM HoaDon WHERE NgayLap = N'" + ngayHômNay + "'");
            if (dtDT.Rows.Count > 0)
            {
                lblDoanhThu.Text = string.Format("{0:N0}", dtDT.Rows[0]["TongTien"]) + " đ";
            }

            // Đơn hàng hôm nay
            var dtDH = fxml.GetDataFromSQL("SELECT COUNT(*) as SoLuong FROM HoaDon WHERE NgayLap = N'" + ngayHômNay + "'");
            if (dtDH.Rows.Count > 0)
            {
                lblDonHang.Text = dtDH.Rows[0]["SoLuong"].ToString();
            }
        }

        protected void lnkDangXuat_Click(object sender, EventArgs e)
        {
            Session.Clear();
            Response.Redirect("~/Login.aspx");
        }
    }
}

