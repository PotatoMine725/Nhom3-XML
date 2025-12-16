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
    public partial class QuanLyNhaCungCap : BasePage
    {
        NhaCungCap ncc = new NhaCungCap();
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
                LoadDanhSachNhaCungCap();
            }
        }

        private void LoadDanhSachNhaCungCap()
        {
            DataTable dt = fxml.GetDataFromSQL("SELECT * FROM NhaCungCap ORDER BY MaNCC");
            gvNhaCungCap.DataSource = dt;
            gvNhaCungCap.DataBind();
        }

        protected void btnThem_Click(object sender, EventArgs e)
        {
            try
            {
                string maNCC = txtMaNCC.Text.Trim();
                
                if (string.IsNullOrEmpty(maNCC))
                {
                    lblThongBao.Text = "Vui lòng nhập mã nhà cung cấp!";
                    return;
                }

                if (ncc.KiemTraMaNCC(maNCC))
                {
                    lblThongBao.Text = "Mã nhà cung cấp đã tồn tại!";
                    return;
                }

                ncc.ThemNhaCungCap(
                    maNCC,
                    txtTenNCC.Text.Trim(),
                    txtDiaChi.Text.Trim(),
                    txtSDT.Text.Trim(),
                    txtEmail.Text.Trim(),
                    txtMoTa.Text.Trim()
                );

                lblThongBao.Text = "Thêm nhà cung cấp thành công!";
                lblThongBao.ForeColor = System.Drawing.Color.Green;
                
                HeThong heThong = new HeThong();
                heThong.TaoXML();
                
                LoadDanhSachNhaCungCap();
                ResetForm();
            }
            catch (Exception ex)
            {
                lblThongBao.Text = "Lỗi: " + ex.Message;
                lblThongBao.ForeColor = System.Drawing.Color.Red;
            }
        }

        protected void btnSua_Click(object sender, EventArgs e)
        {
            try
            {
                string maNCC = txtMaNCC.Text.Trim();
                
                if (string.IsNullOrEmpty(maNCC))
                {
                    lblThongBao.Text = "Vui lòng chọn nhà cung cấp cần sửa!";
                    return;
                }

                ncc.SuaNhaCungCap(
                    maNCC,
                    txtTenNCC.Text.Trim(),
                    txtDiaChi.Text.Trim(),
                    txtSDT.Text.Trim(),
                    txtEmail.Text.Trim(),
                    txtMoTa.Text.Trim()
                );

                lblThongBao.Text = "Cập nhật nhà cung cấp thành công!";
                lblThongBao.ForeColor = System.Drawing.Color.Green;
                
                HeThong heThong = new HeThong();
                heThong.TaoXML();
                
                LoadDanhSachNhaCungCap();
                ResetForm();
            }
            catch (Exception ex)
            {
                lblThongBao.Text = "Lỗi: " + ex.Message;
                lblThongBao.ForeColor = System.Drawing.Color.Red;
            }
        }

        protected void btnXoa_Click(object sender, EventArgs e)
        {
            try
            {
                string maNCC = txtMaNCC.Text.Trim();
                
                if (string.IsNullOrEmpty(maNCC))
                {
                    lblThongBao.Text = "Vui lòng chọn nhà cung cấp cần xóa!";
                    return;
                }

                ncc.XoaNhaCungCap(maNCC);

                lblThongBao.Text = "Xóa nhà cung cấp thành công!";
                lblThongBao.ForeColor = System.Drawing.Color.Green;
                
                HeThong heThong = new HeThong();
                heThong.TaoXML();
                
                LoadDanhSachNhaCungCap();
                ResetForm();
            }
            catch (Exception ex)
            {
                lblThongBao.Text = "Lỗi: " + ex.Message;
                lblThongBao.ForeColor = System.Drawing.Color.Red;
            }
        }

        protected void btnReset_Click(object sender, EventArgs e)
        {
            ResetForm();
        }

        private void ResetForm()
        {
            txtMaNCC.Text = "";
            txtTenNCC.Text = "";
            txtDiaChi.Text = "";
            txtSDT.Text = "";
            txtEmail.Text = "";
            txtMoTa.Text = "";
            lblThongBao.Text = "";
        }

        protected void gvNhaCungCap_SelectedIndexChanged(object sender, EventArgs e)
        {
            string maNCC = gvNhaCungCap.SelectedDataKey.Value.ToString();
            DataTable dt = fxml.GetDataFromSQL("SELECT * FROM NhaCungCap WHERE MaNCC = N'" + maNCC + "'");
            
            if (dt.Rows.Count > 0)
            {
                DataRow row = dt.Rows[0];
                txtMaNCC.Text = row["MaNCC"].ToString();
                txtTenNCC.Text = row["TenNCC"].ToString();
                txtDiaChi.Text = row["DiaChi"].ToString();
                txtSDT.Text = row["SDT"].ToString();
                txtEmail.Text = row["Email"].ToString();
                txtMoTa.Text = row["MoTa"].ToString();
            }
        }
    }
}

