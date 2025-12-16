<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="BaoCao.aspx.cs" Inherits="QuanLyCuaHangThuCung.Admin.BaoCao" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <meta charset="utf-8" />
    <title>Báo cáo - Admin</title>
    <style>
        body {
            font-family: Arial, sans-serif;
            margin: 0;
            padding: 0;
            background: #f5f5f5;
        }
        .header {
            background: linear-gradient(135deg, #667eea 0%, #764ba2 100%);
            color: white;
            padding: 15px;
        }
        .header a {
            color: white;
            text-decoration: none;
            margin-right: 20px;
        }
        .container {
            max-width: 1200px;
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
        .stat-box {
            background: linear-gradient(135deg, #667eea 0%, #764ba2 100%);
            color: white;
            padding: 20px;
            border-radius: 5px;
            margin: 10px;
            display: inline-block;
            min-width: 200px;
            text-align: center;
        }
        .stat-box h3 {
            margin: 0 0 10px 0;
            font-size: 14px;
        }
        .stat-box .number {
            font-size: 36px;
            font-weight: bold;
        }
        .gridview {
            width: 100%;
            margin-top: 20px;
        }
        .gridview th {
            background: #667eea;
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
            <a href="AdminDashboard.aspx">← Về Dashboard</a>
            <h2 style="display: inline-block; margin: 0;">Báo cáo và thống kê</h2>
        </div>

        <div class="container">
            <div class="form-section">
                <h3>Thống kê tổng quan</h3>
                <div class="stat-box">
                    <h3>Tổng doanh thu</h3>
                    <div class="number">
                        <asp:Label ID="lblTongDoanhThu" runat="server" Text="0"></asp:Label>
                    </div>
                </div>
                <div class="stat-box">
                    <h3>Tổng đơn hàng</h3>
                    <div class="number">
                        <asp:Label ID="lblTongDonHang" runat="server" Text="0"></asp:Label>
                    </div>
                </div>
                <div class="stat-box">
                    <h3>Tổng khách hàng</h3>
                    <div class="number">
                        <asp:Label ID="lblTongKhachHang" runat="server" Text="0"></asp:Label>
                    </div>
                </div>
                <div class="stat-box">
                    <h3>Tổng thú cưng</h3>
                    <div class="number">
                        <asp:Label ID="lblTongThuCung" runat="server" Text="0"></asp:Label>
                    </div>
                </div>
            </div>

            <div class="form-section">
                <h3>Top 5 sản phẩm bán chạy</h3>
                <asp:GridView ID="gvTopSanPham" runat="server" CssClass="gridview" AutoGenerateColumns="False">
                    <Columns>
                        <asp:BoundField DataField="TenThuCung" HeaderText="Tên thú cưng" />
                        <asp:BoundField DataField="TongSoLuong" HeaderText="Tổng số lượng bán" />
                        <asp:BoundField DataField="TongTien" HeaderText="Tổng tiền" DataFormatString="{0:N0} đ" />
                    </Columns>
                </asp:GridView>
            </div>

            <div class="form-section">
                <h3>Top 5 khách hàng mua nhiều nhất</h3>
                <asp:GridView ID="gvTopKhachHang" runat="server" CssClass="gridview" AutoGenerateColumns="False">
                    <Columns>
                        <asp:BoundField DataField="TenKhachHang" HeaderText="Tên khách hàng" />
                        <asp:BoundField DataField="SoDonHang" HeaderText="Số đơn hàng" />
                        <asp:BoundField DataField="TongTien" HeaderText="Tổng tiền" DataFormatString="{0:N0} đ" />
                    </Columns>
                </asp:GridView>
            </div>
        </div>
    </form>
</body>
</html>

