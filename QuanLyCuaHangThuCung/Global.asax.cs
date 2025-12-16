using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.SessionState;

namespace QuanLyCuaHangThuCung
{
    public class Global : System.Web.HttpApplication
    {
        protected void Application_Start(object sender, EventArgs e)
        {
            // Khởi tạo ứng dụng
        }

        protected void Session_Start(object sender, EventArgs e)
        {
            // Khởi tạo session
        }

        protected void Application_BeginRequest(object sender, EventArgs e)
        {
            // Xử lý request
        }

        protected void Application_AuthenticateRequest(object sender, EventArgs e)
        {
            // Xác thực request
        }

        protected void Application_Error(object sender, EventArgs e)
        {
            // Xử lý lỗi
        }

        protected void Session_End(object sender, EventArgs e)
        {
            // Kết thúc session
        }

        protected void Application_End(object sender, EventArgs e)
        {
            // Kết thúc ứng dụng
        }
    }
}

