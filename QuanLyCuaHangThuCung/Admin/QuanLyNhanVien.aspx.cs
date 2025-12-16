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
    public partial class QuanLyNhanVien : BasePage
    {
        NhanVien nv = new NhanVien();
        DangNhap dangNhap = new DangNhap();
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
                LoadDanhSachNhanVien();
            }
        }

        private void LoadDanhSachNhanVien()
        {
            DataTable dt = fxml.GetDataFromSQL("SELECT * FROM NhanVien ORDER BY MaNhanVien");
            gvNhanVien.DataSource = dt;
            gvNhanVien.DataBind();
        }

        protected void btnThem_Click(object sender, EventArgs e)
        {
            try
            {
                string maNV = txtMaNhanVien.Text.Trim();
                
                if (string.IsNullOrEmpty(maNV))
                {
                    lblThongBao.Text = "Vui lòng nhập mã nhân viên!";
                    return;
                }

                // Kiểm tra mã nhân viên đã tồn tại chưa
                if (nv.KiemTraMaNhanVien(maNV))
                {
                    lblThongBao.Text = "Mã nhân viên đã tồn tại!";
                    return;
                }

                // Thêm nhân viên
                nv.ThemNhanVien(
                    maNV,
                    txtTenNhanVien.Text.Trim(),
                    txtNgaySinh.Text.Trim(),
                    txtDiaChi.Text.Trim(),
                    txtSDT.Text.Trim(),
                    txtEmail.Text.Trim(),
                    txtChucVu.Text.Trim(),
                    ddlTrangThai.SelectedValue
                );

                // Tạo tài khoản nếu có mật khẩu
                if (!string.IsNullOrEmpty(txtMatKhau.Text.Trim()))
                {
                    int quyen = Convert.ToInt32(ddlQuyen.SelectedValue);
                    dangNhap.DangKiTaiKhoan(maNV, txtMatKhau.Text.Trim(), quyen);
                }

                lblThongBao.Text = "Thêm nhân viên thành công!";
                lblThongBao.ForeColor = System.Drawing.Color.Green;
                
                // Đồng bộ XML
                HeThong heThong = new HeThong();
                heThong.TaoXML();
                
                LoadDanhSachNhanVien();
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
                string maNV = txtMaNhanVien.Text.Trim();
                
                if (string.IsNullOrEmpty(maNV))
                {
                    lblThongBao.Text = "Vui lòng chọn nhân viên cần sửa!";
                    return;
                }

                // Sửa thông tin nhân viên
                nv.SuaNhanVien(
                    maNV,
                    txtTenNhanVien.Text.Trim(),
                    txtNgaySinh.Text.Trim(),
                    txtDiaChi.Text.Trim(),
                    txtSDT.Text.Trim(),
                    txtEmail.Text.Trim(),
                    txtChucVu.Text.Trim(),
                    ddlTrangThai.SelectedValue
                );

                // Đổi mật khẩu nếu có
                if (!string.IsNullOrEmpty(txtMatKhau.Text.Trim()))
                {
                    dangNhap.DoiMatKhau(maNV, txtMatKhau.Text.Trim());
                }

                // Cập nhật quyền
                string sql = "UPDATE TaiKhoan SET Quyen = " + ddlQuyen.SelectedValue + " WHERE MaNhanVien = N'" + maNV + "'";
                fxml.InsertOrUpDateSQL(sql);

                lblThongBao.Text = "Cập nhật nhân viên thành công!";
                lblThongBao.ForeColor = System.Drawing.Color.Green;
                
                // Đồng bộ XML
                HeThong heThong = new HeThong();
                heThong.TaoXML();
                
                LoadDanhSachNhanVien();
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
                string maNV = txtMaNhanVien.Text.Trim();
                
                if (string.IsNullOrEmpty(maNV))
                {
                    lblThongBao.Text = "Vui lòng chọn nhân viên cần xóa!";
                    return;
                }

                // Xóa tài khoản trước
                dangNhap.XoaTaiKhoan(maNV);
                
                // Xóa nhân viên
                nv.XoaNhanVien(maNV);

                lblThongBao.Text = "Xóa nhân viên thành công!";
                lblThongBao.ForeColor = System.Drawing.Color.Green;
                
                // Đồng bộ XML
                HeThong heThong = new HeThong();
                heThong.TaoXML();
                
                LoadDanhSachNhanVien();
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
            txtMaNhanVien.Text = "";
            txtTenNhanVien.Text = "";
            txtNgaySinh.Text = "";
            txtDiaChi.Text = "";
            txtSDT.Text = "";
            txtEmail.Text = "";
            txtChucVu.Text = "";
            ddlTrangThai.SelectedIndex = 0;
            ddlQuyen.SelectedIndex = 0;
            txtMatKhau.Text = "";
            lblThongBao.Text = "";
        }

        protected void gvNhanVien_SelectedIndexChanged(object sender, EventArgs e)
        {
            string maNV = gvNhanVien.SelectedDataKey.Value.ToString();
            
            // Lấy thông tin nhân viên từ SQL
            DataTable dt = fxml.GetDataFromSQL("SELECT * FROM NhanVien WHERE MaNhanVien = N'" + maNV + "'");
            
            if (dt.Rows.Count > 0)
            {
                DataRow row = dt.Rows[0];
                txtMaNhanVien.Text = row["MaNhanVien"].ToString();
                txtTenNhanVien.Text = row["TenNhanVien"].ToString();
                txtNgaySinh.Text = row["NgaySinh"].ToString();
                txtDiaChi.Text = row["DiaChi"].ToString();
                txtSDT.Text = row["SDT"].ToString();
                txtEmail.Text = row["Email"].ToString();
                txtChucVu.Text = row["ChucVu"].ToString();
                ddlTrangThai.SelectedValue = row["TrangThai"].ToString();
            }

            // Lấy quyền từ tài khoản
            DataTable dtTK = fxml.GetDataFromSQL("SELECT Quyen FROM TaiKhoan WHERE MaNhanVien = N'" + maNV + "'");
            if (dtTK.Rows.Count > 0)
            {
                ddlQuyen.SelectedValue = dtTK.Rows[0]["Quyen"].ToString();
            }
        }
    }
}

