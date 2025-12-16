using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;

namespace QuanLyCuaHangThuCung.App_Code
{
    // Base class cho các trang cần kiểm tra đăng nhập
    public class BasePage : Page
    {
        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);

            // Kiểm tra đăng nhập
            if (Session["MaNhanVien"] == null)
            {
                Response.Redirect("~/Login.aspx");
            }
        }

        // Kiểm tra quyền Admin
        protected bool IsAdmin()
        {
            if (Session["Quyen"] != null)
            {
                return Convert.ToInt32(Session["Quyen"]) == 1;
            }
            return false;
        }

        // Kiểm tra quyền Nhân viên
        protected bool IsEmployee()
        {
            if (Session["Quyen"] != null)
            {
                return Convert.ToInt32(Session["Quyen"]) == 0;
            }
            return false;
        }

        // Lấy mã nhân viên từ Session
        protected string GetMaNhanVien()
        {
            if (Session["MaNhanVien"] != null)
                return Session["MaNhanVien"].ToString();
            return "";
        }

        // Lấy tên nhân viên từ Session
        protected string GetTenNhanVien()
        {
            if (Session["TenNhanVien"] != null)
                return Session["TenNhanVien"].ToString();
            return "";
        }
    }
}

