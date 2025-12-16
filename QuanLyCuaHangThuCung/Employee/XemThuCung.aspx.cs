using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using QuanLyCuaHangThuCung.App_Code;

namespace QuanLyCuaHangThuCung.Employee
{
    public partial class XemThuCung : BasePage
    {
        FileXml fxml = new FileXml();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                LoadDanhSachThuCung();
            }
        }

        private void LoadDanhSachThuCung()
        {
            DataTable dt = fxml.GetDataFromSQL("SELECT * FROM ThuCung ORDER BY MaThuCung");
            gvThuCung.DataSource = dt;
            gvThuCung.DataBind();
        }

        protected void btnTimKiem_Click(object sender, EventArgs e)
        {
            string timKiem = txtTimKiem.Text.Trim();
            string loai = ddlLoai.SelectedValue;
            string sql = "SELECT * FROM ThuCung WHERE 1=1";
            
            if (!string.IsNullOrEmpty(timKiem))
            {
                sql += " AND (TenThuCung LIKE N'%" + timKiem + "%' OR Loai LIKE N'%" + timKiem + "%' OR Giong LIKE N'%" + timKiem + "%')";
            }
            
            if (!string.IsNullOrEmpty(loai))
            {
                sql += " AND Loai = N'" + loai + "'";
            }
            
            sql += " ORDER BY MaThuCung";
            
            DataTable dt = fxml.GetDataFromSQL(sql);
            gvThuCung.DataSource = dt;
            gvThuCung.DataBind();
        }
    }
}

