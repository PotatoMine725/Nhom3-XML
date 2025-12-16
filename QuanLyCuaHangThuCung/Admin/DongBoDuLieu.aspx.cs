using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using QuanLyCuaHangThuCung.App_Code;

namespace QuanLyCuaHangThuCung.Admin
{
    public partial class DongBoDuLieu : BasePage
    {
        HeThong heThong = new HeThong();

        protected void Page_Load(object sender, EventArgs e)
        {
            // Kiểm tra quyền Admin
            if (!IsAdmin())
            {
                Response.Redirect("~/Login.aspx");
            }
        }

        protected void btnXuatSQLToXML_Click(object sender, EventArgs e)
        {
            try
            {
                heThong.TaoXML();
                
                divXuatResult.Visible = true;
                lblXuatResult.Text = "<div class='success-box'><strong>✓ Thành công!</strong> Đã xuất tất cả dữ liệu từ SQL Server sang XML.<br/>" +
                    "Các file XML đã được lưu tại: App_Data/XML/</div>";
            }
            catch (Exception ex)
            {
                divXuatResult.Visible = true;
                lblXuatResult.Text = "<div class='error-box'><strong>✗ Lỗi:</strong> " + ex.Message + "</div>";
            }
        }

        protected void btnImportXMLToSQL_Click(object sender, EventArgs e)
        {
            try
            {
                heThong.CapNhapSQL();
                
                divImportResult.Visible = true;
                lblImportResult.Text = "<div class='success-box'><strong>✓ Thành công!</strong> Đã import tất cả dữ liệu từ XML vào SQL Server.</div>";
            }
            catch (Exception ex)
            {
                divImportResult.Visible = true;
                lblImportResult.Text = "<div class='error-box'><strong>✗ Lỗi:</strong> " + ex.Message + "</div>";
            }
        }

        protected void btnDongBo_Click(object sender, EventArgs e)
        {
            try
            {
                heThong.DongBoHoa();
                
                divDongBoResult.Visible = true;
                lblDongBoResult.Text = "<div class='success-box'><strong>✓ Thành công!</strong> Đã đồng bộ hóa dữ liệu 2 chiều.<br/>" +
                    "Dữ liệu từ SQL đã được xuất sang XML.</div>";
            }
            catch (Exception ex)
            {
                divDongBoResult.Visible = true;
                lblDongBoResult.Text = "<div class='error-box'><strong>✗ Lỗi:</strong> " + ex.Message + "</div>";
            }
        }
    }
}

