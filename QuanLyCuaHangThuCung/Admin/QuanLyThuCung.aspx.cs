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
    public partial class QuanLyThuCung : BasePage
    {
        ThuCung tc = new ThuCung();
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
                LoadDanhSachThuCung();
            }
        }

        private void LoadDanhSachNhaCungCap()
        {
            DataTable dt = fxml.GetDataFromSQL("SELECT MaNCC, TenNCC FROM NhaCungCap");
            ddlMaNCC.DataSource = dt;
            ddlMaNCC.DataTextField = "TenNCC";
            ddlMaNCC.DataValueField = "MaNCC";
            ddlMaNCC.DataBind();
        }

        private void LoadDanhSachThuCung()
        {
            DataTable dt = fxml.GetDataFromSQL("SELECT * FROM ThuCung ORDER BY MaThuCung");
            gvThuCung.DataSource = dt;
            gvThuCung.DataBind();
        }

        protected void btnThem_Click(object sender, EventArgs e)
        {
            try
            {
                string maTC = txtMaThuCung.Text.Trim();
                
                if (string.IsNullOrEmpty(maTC))
                {
                    lblThongBao.Text = "Vui lòng nhập mã thú cưng!";
                    return;
                }

                if (tc.KiemTraMaThuCung(maTC))
                {
                    lblThongBao.Text = "Mã thú cưng đã tồn tại!";
                    return;
                }

                int tuoi = string.IsNullOrEmpty(txtTuoi.Text) ? 0 : Convert.ToInt32(txtTuoi.Text);
                int gia = string.IsNullOrEmpty(txtGia.Text) ? 0 : Convert.ToInt32(txtGia.Text);
                int soLuong = string.IsNullOrEmpty(txtSoLuong.Text) ? 0 : Convert.ToInt32(txtSoLuong.Text);

                tc.ThemThuCung(
                    maTC,
                    txtTenThuCung.Text.Trim(),
                    ddlLoai.SelectedValue,
                    txtGiong.Text.Trim(),
                    tuoi,
                    ddlGioiTinh.SelectedValue,
                    gia,
                    ddlTinhTrangSucKhoe.SelectedValue,
                    ddlMaNCC.SelectedValue,
                    soLuong,
                    txtMoTa.Text.Trim()
                );

                lblThongBao.Text = "Thêm thú cưng thành công!";
                lblThongBao.ForeColor = System.Drawing.Color.Green;
                
                HeThong heThong = new HeThong();
                heThong.TaoXML();
                
                LoadDanhSachThuCung();
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
                string maTC = txtMaThuCung.Text.Trim();
                
                if (string.IsNullOrEmpty(maTC))
                {
                    lblThongBao.Text = "Vui lòng chọn thú cưng cần sửa!";
                    return;
                }

                int tuoi = string.IsNullOrEmpty(txtTuoi.Text) ? 0 : Convert.ToInt32(txtTuoi.Text);
                int gia = string.IsNullOrEmpty(txtGia.Text) ? 0 : Convert.ToInt32(txtGia.Text);
                int soLuong = string.IsNullOrEmpty(txtSoLuong.Text) ? 0 : Convert.ToInt32(txtSoLuong.Text);

                tc.SuaThuCung(
                    maTC,
                    txtTenThuCung.Text.Trim(),
                    ddlLoai.SelectedValue,
                    txtGiong.Text.Trim(),
                    tuoi,
                    ddlGioiTinh.SelectedValue,
                    gia,
                    ddlTinhTrangSucKhoe.SelectedValue,
                    ddlMaNCC.SelectedValue,
                    soLuong,
                    txtMoTa.Text.Trim()
                );

                lblThongBao.Text = "Cập nhật thú cưng thành công!";
                lblThongBao.ForeColor = System.Drawing.Color.Green;
                
                HeThong heThong = new HeThong();
                heThong.TaoXML();
                
                LoadDanhSachThuCung();
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
                string maTC = txtMaThuCung.Text.Trim();
                
                if (string.IsNullOrEmpty(maTC))
                {
                    lblThongBao.Text = "Vui lòng chọn thú cưng cần xóa!";
                    return;
                }

                tc.XoaThuCung(maTC);

                lblThongBao.Text = "Xóa thú cưng thành công!";
                lblThongBao.ForeColor = System.Drawing.Color.Green;
                
                HeThong heThong = new HeThong();
                heThong.TaoXML();
                
                LoadDanhSachThuCung();
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

        protected void btnTimKiem_Click(object sender, EventArgs e)
        {
            string timKiem = txtTimKiem.Text.Trim();
            string sql = "SELECT * FROM ThuCung WHERE 1=1";
            
            if (!string.IsNullOrEmpty(timKiem))
            {
                sql += " AND (TenThuCung LIKE N'%" + timKiem + "%' OR Loai LIKE N'%" + timKiem + "%' OR Giong LIKE N'%" + timKiem + "%')";
            }
            
            sql += " ORDER BY MaThuCung";
            
            DataTable dt = fxml.GetDataFromSQL(sql);
            gvThuCung.DataSource = dt;
            gvThuCung.DataBind();
        }

        private void ResetForm()
        {
            txtMaThuCung.Text = "";
            txtTenThuCung.Text = "";
            ddlLoai.SelectedIndex = 0;
            txtGiong.Text = "";
            txtTuoi.Text = "";
            ddlGioiTinh.SelectedIndex = 0;
            txtGia.Text = "";
            txtSoLuong.Text = "";
            ddlTinhTrangSucKhoe.SelectedIndex = 0;
            ddlMaNCC.SelectedIndex = 0;
            txtMoTa.Text = "";
            lblThongBao.Text = "";
        }

        protected void gvThuCung_SelectedIndexChanged(object sender, EventArgs e)
        {
            string maTC = gvThuCung.SelectedDataKey.Value.ToString();
            DataTable dt = fxml.GetDataFromSQL("SELECT * FROM ThuCung WHERE MaThuCung = N'" + maTC + "'");
            
            if (dt.Rows.Count > 0)
            {
                DataRow row = dt.Rows[0];
                txtMaThuCung.Text = row["MaThuCung"].ToString();
                txtTenThuCung.Text = row["TenThuCung"].ToString();
                ddlLoai.SelectedValue = row["Loai"].ToString();
                txtGiong.Text = row["Giong"].ToString();
                txtTuoi.Text = row["Tuoi"].ToString();
                ddlGioiTinh.SelectedValue = row["GioiTinh"].ToString();
                txtGia.Text = row["Gia"].ToString();
                txtSoLuong.Text = row["SoLuong"].ToString();
                ddlTinhTrangSucKhoe.SelectedValue = row["TinhTrangSucKhoe"].ToString();
                ddlMaNCC.SelectedValue = row["MaNCC"].ToString();
                txtMoTa.Text = row["MoTa"].ToString();
            }
        }
    }
}

