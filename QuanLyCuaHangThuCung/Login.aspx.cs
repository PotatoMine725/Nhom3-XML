using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using QuanLyCuaHangThuCung.App_Code;

namespace QuanLyCuaHangThuCung
{
    public partial class Login : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            // Kiểm tra nếu đã đăng nhập thì chuyển hướng
            if (Session["MaNhanVien"] != null)
            {
                int quyen = Convert.ToInt32(Session["Quyen"]);
                if (quyen == 1) // Admin
                {
                    Response.Redirect("~/Admin/AdminDashboard.aspx");
                }
                else // Nhân viên
                {
                    Response.Redirect("~/Employee/EmployeeDashboard.aspx");
                }
            }
        }

        protected void btnDangNhap_Click(object sender, EventArgs e)
        {
            string maNV = txtMaNhanVien.Text.Trim();
            string matKhau = txtMatKhau.Text.Trim();

            if (string.IsNullOrEmpty(maNV) || string.IsNullOrEmpty(matKhau))
            {
                lblThongBao.Text = "Vui lòng nhập đầy đủ thông tin!";
                lblThongBao.Visible = true;
                return;
            }

            DangNhap dangNhap = new DangNhap();
            int quyen = dangNhap.KiemTraDangNhap(maNV, matKhau);

            if (quyen >= 0) // Đăng nhập thành công
            {
                // Lưu thông tin vào Session
                Session["MaNhanVien"] = maNV;
                Session["Quyen"] = quyen;
                Session["TenNhanVien"] = dangNhap.LayTenNhanVien(maNV);

                // Chuyển hướng theo quyền
                if (quyen == 1) // Admin
                {
                    Response.Redirect("~/Admin/AdminDashboard.aspx");
                }
                else // Nhân viên (quyen = 0)
                {
                    Response.Redirect("~/Employee/EmployeeDashboard.aspx");
                }
            }
            else // Đăng nhập thất bại
            {
                lblThongBao.Text = "Mã nhân viên hoặc mật khẩu không đúng!";
                lblThongBao.Visible = true;
            }
        }
    }
}

