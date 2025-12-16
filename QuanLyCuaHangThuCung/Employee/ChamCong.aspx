<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ChamCong.aspx.cs" Inherits="QuanLyCuaHangThuCung.Employee.ChamCong" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <meta charset="utf-8" />
    <title>Chấm công - Nhân viên</title>
    <style>
        body {
            font-family: Arial, sans-serif;
            margin: 0;
            padding: 0;
            background: #f5f5f5;
        }
        .header {
            background: linear-gradient(135deg, #48c6ef 0%, #6f86d6 100%);
            color: white;
            padding: 15px;
        }
        .header a {
            color: white;
            text-decoration: none;
            margin-right: 20px;
        }
        .container {
            max-width: 1000px;
            margin: 20px auto;
            padding: 20px;
        }
        .form-section {
            background: white;
            padding: 20px;
            margin-bottom: 20px;
            border-radius: 5px;
            box-shadow: 0 2px 5px rgba(0,0,0,0.1);
        }
        .btn {
            padding: 15px 30px;
            border: none;
            border-radius: 5px;
            cursor: pointer;
            margin: 10px;
            font-size: 16px;
            font-weight: bold;
        }
        .btn-success {
            background: #28a745;
            color: white;
        }
        .btn-info {
            background: #17a2b8;
            color: white;
        }
        .info-box {
            background: #e3f2fd;
            padding: 15px;
            border-radius: 5px;
            margin: 10px 0;
            border-left: 4px solid #2196f3;
        }
        .gridview {
            width: 100%;
            margin-top: 20px;
        }
        .gridview th {
            background: #48c6ef;
            color: white;
            padding: 10px;
        }
        .gridview td {
            padding: 8px;
            border-bottom: 1px solid #ddd;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
        <div class="header">
            <a href="EmployeeDashboard.aspx">← Về Dashboard</a>
            <h2 style="display: inline-block; margin: 0;">Chấm công</h2>
        </div>

        <div class="container">
            <div class="form-section">
                <h3>Xác nhận đi làm hôm nay</h3>
                <div class="info-box">
                    <p><strong>Mã nhân viên:</strong> <asp:Label ID="lblMaNhanVien" runat="server"></asp:Label></p>
                    <p><strong>Ngày hôm nay:</strong> <asp:Label ID="lblNgayHomNay" runat="server"></asp:Label></p>
                </div>
                <asp:Button ID="btnChamCong" runat="server" Text="Xác nhận đi làm" CssClass="btn btn-success" OnClick="btnChamCong_Click" />
                <asp:Label ID="lblThongBao" runat="server" ForeColor="Red"></asp:Label>
            </div>

            <div class="form-section">
                <h3>Lịch sử chấm công</h3>
                <div class="form-group">
                    <label>Tháng/Năm:</label>
                    <asp:DropDownList ID="ddlThang" runat="server"></asp:DropDownList>
                    <asp:DropDownList ID="ddlNam" runat="server"></asp:DropDownList>
                    <asp:Button ID="btnXem" runat="server" Text="Xem" CssClass="btn btn-info" OnClick="btnXem_Click" />
                </div>
                <asp:GridView ID="gvChamCong" runat="server" CssClass="gridview" AutoGenerateColumns="False">
                    <Columns>
                        <asp:BoundField DataField="Ngay" HeaderText="Ngày" />
                        <asp:BoundField DataField="Thang" HeaderText="Tháng" />
                        <asp:BoundField DataField="Nam" HeaderText="Năm" />
                        <asp:BoundField DataField="GioVao" HeaderText="Giờ vào" />
                        <asp:BoundField DataField="GioRa" HeaderText="Giờ ra" />
                    </Columns>
                </asp:GridView>
            </div>
        </div>
    </form>
</body>
</html>

