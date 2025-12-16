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
    public partial class QuanLyKhachHang : BasePage
    {
        KhachHang kh = new KhachHang();
        FileXml fxml = new FileXml();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                LoadDanhSachKhachHang();
            }
        }

        private void LoadDanhSachKhachHang()
        {
            DataTable dt = fxml.GetDataFromSQL("SELECT * FROM KhachHang ORDER BY MaKhachHang");
            gvKhachHang.DataSource = dt;
            gvKhachHang.DataBind();
        }

        protected void btnThem_Click(object sender, EventArgs e)
        {
            try
            {
                string maKH = txtMaKhachHang.Text.Trim();
                
                if (string.IsNullOrEmpty(maKH))
                {
                    lblThongBao.Text = "Vui lòng nhập mã khách hàng!";
                    return;
                }

                if (kh.KiemTraMaKhachHang(maKH))
                {
                    lblThongBao.Text = "Mã khách hàng đã tồn tại!";
                    return;
                }

                kh.ThemKhachHang(
                    maKH,
                    txtTenKhachHang.Text.Trim(),
                    txtSDT.Text.Trim(),
                    txtEmail.Text.Trim(),
                    txtDiaChi.Text.Trim(),
                    DateTime.Now.ToString("dd/MM/yyyy")
                );

                lblThongBao.Text = "Thêm khách hàng thành công!";
                lblThongBao.ForeColor = System.Drawing.Color.Green;
                
                HeThong heThong = new HeThong();
                heThong.TaoXML();
                
                LoadDanhSachKhachHang();
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
                string maKH = txtMaKhachHang.Text.Trim();
                
                if (string.IsNullOrEmpty(maKH))
                {
                    lblThongBao.Text = "Vui lòng chọn khách hàng cần sửa!";
                    return;
                }

                DataTable dt = fxml.GetDataFromSQL("SELECT NgayDangKy FROM KhachHang WHERE MaKhachHang = N'" + maKH + "'");
                string ngayDangKy = dt.Rows.Count > 0 ? dt.Rows[0]["NgayDangKy"].ToString() : DateTime.Now.ToString("dd/MM/yyyy");

                kh.SuaKhachHang(
                    maKH,
                    txtTenKhachHang.Text.Trim(),
                    txtSDT.Text.Trim(),
                    txtEmail.Text.Trim(),
                    txtDiaChi.Text.Trim(),
                    ngayDangKy
                );

                lblThongBao.Text = "Cập nhật khách hàng thành công!";
                lblThongBao.ForeColor = System.Drawing.Color.Green;
                
                HeThong heThong = new HeThong();
                heThong.TaoXML();
                
                LoadDanhSachKhachHang();
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
                string maKH = txtMaKhachHang.Text.Trim();
                
                if (string.IsNullOrEmpty(maKH))
                {
                    lblThongBao.Text = "Vui lòng chọn khách hàng cần xóa!";
                    return;
                }

                kh.XoaKhachHang(maKH);

                lblThongBao.Text = "Xóa khách hàng thành công!";
                lblThongBao.ForeColor = System.Drawing.Color.Green;
                
                HeThong heThong = new HeThong();
                heThong.TaoXML();
                
                LoadDanhSachKhachHang();
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
            string sql = "SELECT * FROM KhachHang WHERE 1=1";
            
            if (!string.IsNullOrEmpty(timKiem))
            {
                sql += " AND (TenKhachHang LIKE N'%" + timKiem + "%' OR SDT LIKE N'%" + timKiem + "%' OR Email LIKE N'%" + timKiem + "%')";
            }
            
            sql += " ORDER BY MaKhachHang";
            
            DataTable dt = fxml.GetDataFromSQL(sql);
            gvKhachHang.DataSource = dt;
            gvKhachHang.DataBind();
        }

        private void ResetForm()
        {
            txtMaKhachHang.Text = "";
            txtTenKhachHang.Text = "";
            txtSDT.Text = "";
            txtEmail.Text = "";
            txtDiaChi.Text = "";
            lblThongBao.Text = "";
            gvLichSuMuaHang.DataSource = null;
            gvLichSuMuaHang.DataBind();
        }

        protected void gvKhachHang_SelectedIndexChanged(object sender, EventArgs e)
        {
            string maKH = gvKhachHang.SelectedDataKey.Value.ToString();
            DataTable dt = fxml.GetDataFromSQL("SELECT * FROM KhachHang WHERE MaKhachHang = N'" + maKH + "'");
            
            if (dt.Rows.Count > 0)
            {
                DataRow row = dt.Rows[0];
                txtMaKhachHang.Text = row["MaKhachHang"].ToString();
                txtTenKhachHang.Text = row["TenKhachHang"].ToString();
                txtSDT.Text = row["SDT"].ToString();
                txtEmail.Text = row["Email"].ToString();
                txtDiaChi.Text = row["DiaChi"].ToString();
            }

            // Load lịch sử mua hàng
            DataTable dtLS = fxml.GetDataFromSQL("SELECT SoHoaDon, NgayLap, TongTien FROM HoaDon WHERE MaKhachHang = N'" + maKH + "' ORDER BY NgayLap DESC");
            gvLichSuMuaHang.DataSource = dtLS;
            gvLichSuMuaHang.DataBind();
        }
    }
}

