using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using QuanLyCuaHangThuCung.App_Code;

namespace QuanLyCuaHangThuCung.Employee
{
    public partial class EmployeeDashboard : BasePage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            // Kiểm tra đăng nhập
            if (Session["MaNhanVien"] == null)
            {
                Response.Redirect("~/Login.aspx");
            }

            if (!IsPostBack)
            {
                lblTenNhanVien.Text = "Xin chào, " + GetTenNhanVien();
                LoadThongKe();
            }
        }

        private void LoadThongKe()
        {
            FileXml fxml = new FileXml();
            string maNV = GetMaNhanVien();
            string ngayHômNay = DateTime.Now.ToString("dd/MM/yyyy");

            // Doanh thu hôm nay của nhân viên
            var dtDT = fxml.GetDataFromSQL("SELECT ISNULL(SUM(TongTien), 0) as TongTien FROM HoaDon WHERE MaNhanVien = N'" + maNV + "' AND NgayLap = N'" + ngayHômNay + "'");
            if (dtDT.Rows.Count > 0)
            {
                lblDoanhThu.Text = string.Format("{0:N0}", dtDT.Rows[0]["TongTien"]) + " đ";
            }

            // Đơn hàng hôm nay của nhân viên
            var dtDH = fxml.GetDataFromSQL("SELECT COUNT(*) as SoLuong FROM HoaDon WHERE MaNhanVien = N'" + maNV + "' AND NgayLap = N'" + ngayHômNay + "'");
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

