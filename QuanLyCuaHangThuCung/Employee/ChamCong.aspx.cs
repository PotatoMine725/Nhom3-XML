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
    public partial class ChamCong : BasePage
    {
        FileXml fxml = new FileXml();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                lblMaNhanVien.Text = GetMaNhanVien();
                lblNgayHomNay.Text = DateTime.Now.ToString("dd/MM/yyyy");
                
                // Load dropdown tháng
                for (int i = 1; i <= 12; i++)
                {
                    ddlThang.Items.Add(new ListItem(i.ToString(), i.ToString()));
                }
                ddlThang.SelectedValue = DateTime.Now.Month.ToString();
                
                // Load dropdown năm
                for (int i = DateTime.Now.Year - 2; i <= DateTime.Now.Year; i++)
                {
                    ddlNam.Items.Add(new ListItem(i.ToString(), i.ToString()));
                }
                ddlNam.SelectedValue = DateTime.Now.Year.ToString();
                
                LoadLichSuChamCong();
            }
        }

        protected void btnChamCong_Click(object sender, EventArgs e)
        {
            try
            {
                string maNV = GetMaNhanVien();
                int ngay = DateTime.Now.Day;
                int thang = DateTime.Now.Month;
                int nam = DateTime.Now.Year;
                string gioVao = DateTime.Now.ToString("HH:mm");

                // Kiểm tra đã chấm công chưa
                DataTable dt = fxml.GetDataFromSQL("SELECT * FROM ChamCong WHERE MaNhanVien = N'" + maNV + 
                    "' AND Ngay = " + ngay + " AND Thang = " + thang + " AND Nam = " + nam);
                
                if (dt.Rows.Count > 0)
                {
                    // Cập nhật giờ ra
                    string sql = "UPDATE ChamCong SET GioRa = N'" + DateTime.Now.ToString("HH:mm") + 
                        "' WHERE MaNhanVien = N'" + maNV + "' AND Ngay = " + ngay + " AND Thang = " + thang + " AND Nam = " + nam;
                    fxml.InsertOrUpDateSQL(sql);
                    lblThongBao.Text = "Đã ghi nhận giờ ra: " + DateTime.Now.ToString("HH:mm");
                    lblThongBao.ForeColor = System.Drawing.Color.Green;
                }
                else
                {
                    // Thêm mới chấm công
                    DataTable dtMax = fxml.GetDataFromSQL("SELECT ISNULL(MAX(Id), 0) + 1 as IdMoi FROM ChamCong");
                    int id = 1;
                    if (dtMax.Rows.Count > 0)
                    {
                        id = Convert.ToInt32(dtMax.Rows[0]["IdMoi"]);
                    }

                    string sql = "INSERT INTO ChamCong (Id, MaNhanVien, Ngay, Thang, Nam, GioVao, GioRa) " +
                        "VALUES (" + id + ", N'" + maNV + "', " + ngay + ", " + thang + ", " + nam + 
                        ", N'" + gioVao + "', NULL)";
                    fxml.InsertOrUpDateSQL(sql);
                    
                    lblThongBao.Text = "Đã ghi nhận giờ vào: " + gioVao;
                    lblThongBao.ForeColor = System.Drawing.Color.Green;
                }

                // Đồng bộ XML
                HeThong heThong = new HeThong();
                heThong.TaoXML();
                
                LoadLichSuChamCong();
            }
            catch (Exception ex)
            {
                lblThongBao.Text = "Lỗi: " + ex.Message;
                lblThongBao.ForeColor = System.Drawing.Color.Red;
            }
        }

        protected void btnXem_Click(object sender, EventArgs e)
        {
            LoadLichSuChamCong();
        }

        private void LoadLichSuChamCong()
        {
            string maNV = GetMaNhanVien();
            int thang = Convert.ToInt32(ddlThang.SelectedValue);
            int nam = Convert.ToInt32(ddlNam.SelectedValue);
            
            string sql = "SELECT * FROM ChamCong WHERE MaNhanVien = N'" + maNV + 
                "' AND Thang = " + thang + " AND Nam = " + nam + " ORDER BY Ngay DESC";
            
            DataTable dt = fxml.GetDataFromSQL(sql);
            gvChamCong.DataSource = dt;
            gvChamCong.DataBind();
        }
    }
}

