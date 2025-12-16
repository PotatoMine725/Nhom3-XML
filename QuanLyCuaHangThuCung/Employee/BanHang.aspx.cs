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
    public partial class BanHang : BasePage
    {
        HoaDon hoaDon = new HoaDon();
        KhachHang khachHang = new KhachHang();
        FileXml fxml = new FileXml();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                LoadDanhSachThuCung();
                Session["GioHang"] = new DataTable();
                CreateGioHangTable();
            }
            else
            {
                if (Session["GioHang"] == null)
                {
                    CreateGioHangTable();
                }
            }
        }

        private void CreateGioHangTable()
        {
            DataTable dt = new DataTable();
            dt.Columns.Add("MaThuCung");
            dt.Columns.Add("TenThuCung");
            dt.Columns.Add("DonGia", typeof(int));
            dt.Columns.Add("SoLuong", typeof(int));
            dt.Columns.Add("ThanhTien", typeof(int));
            Session["GioHang"] = dt;
        }

        private void LoadDanhSachThuCung()
        {
            DataTable dt = fxml.GetDataFromSQL("SELECT MaThuCung, TenThuCung + ' - ' + Loai + ' (' + Giong + ')' as Ten FROM ThuCung WHERE SoLuong > 0 ORDER BY MaThuCung");
            ddlMaThuCung.DataSource = dt;
            ddlMaThuCung.DataTextField = "Ten";
            ddlMaThuCung.DataValueField = "MaThuCung";
            ddlMaThuCung.DataBind();
            ddlMaThuCung.Items.Insert(0, new ListItem("-- Chọn thú cưng --", ""));
        }

        protected void ddlMaThuCung_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (ddlMaThuCung.SelectedValue != "")
            {
                string maTC = ddlMaThuCung.SelectedValue;
                DataTable dt = fxml.GetDataFromSQL("SELECT * FROM ThuCung WHERE MaThuCung = N'" + maTC + "'");
                
                if (dt.Rows.Count > 0)
                {
                    lblTenThuCung.Text = dt.Rows[0]["TenThuCung"].ToString();
                    lblGia.Text = string.Format("{0:N0}", dt.Rows[0]["Gia"]) + " đ";
                    lblSoLuongCon.Text = dt.Rows[0]["SoLuong"].ToString();
                }
            }
        }

        protected void btnThemVaoGio_Click(object sender, EventArgs e)
        {
            if (ddlMaThuCung.SelectedValue == "")
            {
                lblThongBao.Text = "Vui lòng chọn thú cưng!";
                return;
            }

            int soLuong = string.IsNullOrEmpty(txtSoLuongMua.Text) ? 1 : Convert.ToInt32(txtSoLuongMua.Text);
            string maTC = ddlMaThuCung.SelectedValue;
            
            DataTable dt = fxml.GetDataFromSQL("SELECT * FROM ThuCung WHERE MaThuCung = N'" + maTC + "'");
            if (dt.Rows.Count > 0)
            {
                int soLuongCon = Convert.ToInt32(dt.Rows[0]["SoLuong"]);
                if (soLuong > soLuongCon)
                {
                    lblThongBao.Text = "Số lượng không đủ! Chỉ còn " + soLuongCon + " con.";
                    return;
                }

                int donGia = Convert.ToInt32(dt.Rows[0]["Gia"]);
                string tenTC = dt.Rows[0]["TenThuCung"].ToString();

                DataTable gioHang = (DataTable)Session["GioHang"];
                
                // Kiểm tra đã có trong giỏ chưa
                DataRow[] existingRows = gioHang.Select("MaThuCung = '" + maTC + "'");
                if (existingRows.Length > 0)
                {
                    int soLuongCu = Convert.ToInt32(existingRows[0]["SoLuong"]);
                    int soLuongMoi = soLuongCu + soLuong;
                    if (soLuongMoi > soLuongCon)
                    {
                        lblThongBao.Text = "Số lượng không đủ!";
                        return;
                    }
                    existingRows[0]["SoLuong"] = soLuongMoi;
                    existingRows[0]["ThanhTien"] = soLuongMoi * donGia;
                }
                else
                {
                    DataRow row = gioHang.NewRow();
                    row["MaThuCung"] = maTC;
                    row["TenThuCung"] = tenTC;
                    row["DonGia"] = donGia;
                    row["SoLuong"] = soLuong;
                    row["ThanhTien"] = soLuong * donGia;
                    gioHang.Rows.Add(row);
                }

                Session["GioHang"] = gioHang;
                LoadGioHang();
                lblThongBao.Text = "Đã thêm vào giỏ hàng!";
                lblThongBao.ForeColor = System.Drawing.Color.Green;
            }
        }

        private void LoadGioHang()
        {
            DataTable gioHang = (DataTable)Session["GioHang"];
            gvGioHang.DataSource = gioHang;
            gvGioHang.DataBind();

            int tongTien = 0;
            foreach (DataRow row in gioHang.Rows)
            {
                tongTien += Convert.ToInt32(row["ThanhTien"]);
            }
            lblTongTien.Text = string.Format("{0:N0}", tongTien);
        }

        protected void gvGioHang_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            DataTable gioHang = (DataTable)Session["GioHang"];
            gioHang.Rows.RemoveAt(e.RowIndex);
            Session["GioHang"] = gioHang;
            LoadGioHang();
        }

        protected void btnThanhToan_Click(object sender, EventArgs e)
        {
            DataTable gioHang = (DataTable)Session["GioHang"];
            if (gioHang.Rows.Count == 0)
            {
                lblThongBao.Text = "Giỏ hàng trống!";
                return;
            }

            string maKH = txtMaKhachHang.Text.Trim();
            if (string.IsNullOrEmpty(maKH))
            {
                lblThongBao.Text = "Vui lòng nhập mã khách hàng!";
                return;
            }

            // Kiểm tra khách hàng có tồn tại không
            DataTable dtKH = fxml.GetDataFromSQL("SELECT * FROM KhachHang WHERE MaKhachHang = N'" + maKH + "'");
            if (dtKH.Rows.Count == 0)
            {
                // Tạo khách hàng mới
                if (string.IsNullOrEmpty(txtTenKhachHang.Text))
                {
                    lblThongBao.Text = "Vui lòng nhập tên khách hàng!";
                    return;
                }
                khachHang.ThemKhachHang(maKH, txtTenKhachHang.Text, txtSDT.Text, "", "", DateTime.Now.ToString("dd/MM/yyyy"));
            }

            // Tính tổng tiền
            int tongTien = 0;
            foreach (DataRow row in gioHang.Rows)
            {
                tongTien += Convert.ToInt32(row["ThanhTien"]);
            }

            // Tạo hóa đơn
            string maNV = GetMaNhanVien();
            string ngayLap = DateTime.Now.ToString("dd/MM/yyyy");
            int soHoaDon = hoaDon.TaoHoaDon(maNV, maKH, ngayLap, tongTien);

            // Thêm chi tiết hóa đơn
            foreach (DataRow row in gioHang.Rows)
            {
                hoaDon.ThemChiTietHoaDon(
                    soHoaDon,
                    row["MaThuCung"].ToString(),
                    Convert.ToInt32(row["DonGia"]),
                    Convert.ToInt32(row["SoLuong"])
                );
            }

            // Đồng bộ XML
            HeThong heThong = new HeThong();
            heThong.TaoXML();

            lblThongBao.Text = "Thanh toán thành công! Số hóa đơn: " + soHoaDon;
            lblThongBao.ForeColor = System.Drawing.Color.Green;

            // Xóa giỏ hàng
            CreateGioHangTable();
            LoadGioHang();
        }

        protected void btnXoaGioHang_Click(object sender, EventArgs e)
        {
            CreateGioHangTable();
            LoadGioHang();
            lblThongBao.Text = "Đã xóa giỏ hàng!";
        }

        protected void btnTimKhachHang_Click(object sender, EventArgs e)
        {
            string maKH = txtMaKhachHang.Text.Trim();
            if (string.IsNullOrEmpty(maKH))
            {
                lblThongBao.Text = "Vui lòng nhập mã khách hàng!";
                return;
            }

            DataTable dt = fxml.GetDataFromSQL("SELECT * FROM KhachHang WHERE MaKhachHang = N'" + maKH + "'");
            if (dt.Rows.Count > 0)
            {
                txtTenKhachHang.Text = dt.Rows[0]["TenKhachHang"].ToString();
                txtSDT.Text = dt.Rows[0]["SDT"].ToString();
                lblThongBao.Text = "Tìm thấy khách hàng!";
                lblThongBao.ForeColor = System.Drawing.Color.Green;
            }
            else
            {
                lblThongBao.Text = "Không tìm thấy khách hàng!";
                txtTenKhachHang.Text = "";
                txtSDT.Text = "";
            }
        }

        protected void btnThemKhachHang_Click(object sender, EventArgs e)
        {
            string maKH = txtMaKhachHang.Text.Trim();
            string tenKH = txtTenKhachHang.Text.Trim();
            string sdt = txtSDT.Text.Trim();

            if (string.IsNullOrEmpty(maKH) || string.IsNullOrEmpty(tenKH))
            {
                lblThongBao.Text = "Vui lòng nhập đầy đủ thông tin!";
                return;
            }

            if (khachHang.KiemTraMaKhachHang(maKH))
            {
                lblThongBao.Text = "Mã khách hàng đã tồn tại!";
                return;
            }

            khachHang.ThemKhachHang(maKH, tenKH, sdt, "", "", DateTime.Now.ToString("dd/MM/yyyy"));
            lblThongBao.Text = "Thêm khách hàng thành công!";
            lblThongBao.ForeColor = System.Drawing.Color.Green;

            HeThong heThong = new HeThong();
            heThong.TaoXML();
        }
    }
}

