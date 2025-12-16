<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="EmployeeDashboard.aspx.cs" Inherits="QuanLyCuaHangThuCung.Employee.EmployeeDashboard" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <meta charset="utf-8" />
    <title>Dashboard Nhân viên - Quản lý cửa hàng thú cưng</title>
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
            padding: 20px;
            box-shadow: 0 2px 5px rgba(0,0,0,0.1);
        }
        .header h1 {
            margin: 0;
            display: inline-block;
        }
        .user-info {
            float: right;
            margin-top: 10px;
        }
        .menu {
            background: white;
            padding: 15px;
            box-shadow: 0 2px 5px rgba(0,0,0,0.1);
        }
        .menu a {
            display: inline-block;
            padding: 10px 20px;
            margin-right: 10px;
            background: #48c6ef;
            color: white;
            text-decoration: none;
            border-radius: 5px;
        }
        .menu a:hover {
            background: #3ab5d8;
        }
        .content {
            padding: 20px;
        }
        .widget {
            background: white;
            padding: 20px;
            margin: 10px;
            border-radius: 5px;
            box-shadow: 0 2px 5px rgba(0,0,0,0.1);
            display: inline-block;
            width: 200px;
            text-align: center;
        }
        .widget h3 {
            margin: 0 0 10px 0;
            color: #48c6ef;
        }
        .widget .number {
            font-size: 36px;
            font-weight: bold;
            color: #333;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
        <div class="header">
            <h1>🐾 Quản lý cửa hàng thú cưng - Nhân viên</h1>
            <div class="user-info">
                <asp:Label ID="lblTenNhanVien" runat="server"></asp:Label>
                | <asp:LinkButton ID="lnkDangXuat" runat="server" OnClick="lnkDangXuat_Click" ForeColor="White">Đăng xuất</asp:LinkButton>
            </div>
            <div style="clear: both;"></div>
        </div>

        <div class="menu">
            <a href="BanHang.aspx">💰 Bán hàng</a>
            <a href="QuanLyKhachHang.aspx">👤 Quản lý khách hàng</a>
            <a href="XemThuCung.aspx">🐕 Xem thú cưng</a>
            <a href="ChamCong.aspx">⏰ Chấm công</a>
        </div>

        <div class="content">
            <h2>Dashboard</h2>
            
            <div class="widget">
                <h3>Doanh thu hôm nay</h3>
                <div class="number">
                    <asp:Label ID="lblDoanhThu" runat="server" Text="0"></asp:Label>
                </div>
            </div>

            <div class="widget">
                <h3>Đơn hàng hôm nay</h3>
                <div class="number">
                    <asp:Label ID="lblDonHang" runat="server" Text="0"></asp:Label>
                </div>
            </div>
        </div>
    </form>
</body>
</html>

