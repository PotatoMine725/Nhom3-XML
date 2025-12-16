<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="XemThuCung.aspx.cs" Inherits="QuanLyCuaHangThuCung.Employee.XemThuCung" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <meta charset="utf-8" />
    <title>Xem thú cưng - Nhân viên</title>
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
        .form-group {
            margin-bottom: 15px;
        }
        label {
            display: block;
            margin-bottom: 5px;
            font-weight: bold;
        }
        input[type="text"], select {
            width: 100%;
            padding: 8px;
            border: 1px solid #ddd;
            border-radius: 4px;
            box-sizing: border-box;
        }
        .btn {
            padding: 10px 20px;
            border: none;
            border-radius: 4px;
            cursor: pointer;
            margin-right: 10px;
        }
        .btn-primary {
            background: #48c6ef;
            color: white;
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
            <h2 style="display: inline-block; margin: 0;">Xem thú cưng</h2>
        </div>

        <div class="container">
            <div class="form-section">
                <h3>Tìm kiếm thú cưng</h3>
                <div class="form-group">
                    <label>Tìm kiếm:</label>
                    <asp:TextBox ID="txtTimKiem" runat="server" placeholder="Tìm theo tên, loài, giống..."></asp:TextBox>
                </div>
                <div class="form-group">
                    <label>Lọc theo loài:</label>
                    <asp:DropDownList ID="ddlLoai" runat="server">
                        <asp:ListItem Value="" Text="Tất cả"></asp:ListItem>
                        <asp:ListItem Value="Chó" Text="Chó"></asp:ListItem>
                        <asp:ListItem Value="Mèo" Text="Mèo"></asp:ListItem>
                        <asp:ListItem Value="Chim" Text="Chim"></asp:ListItem>
                        <asp:ListItem Value="Cá" Text="Cá"></asp:ListItem>
                    </asp:DropDownList>
                </div>
                <div>
                    <asp:Button ID="btnTimKiem" runat="server" Text="Tìm kiếm" CssClass="btn btn-primary" OnClick="btnTimKiem_Click" />
                </div>
            </div>

            <div class="form-section">
                <h3>Danh sách thú cưng</h3>
                <asp:GridView ID="gvThuCung" runat="server" CssClass="gridview" AutoGenerateColumns="False">
                    <Columns>
                        <asp:BoundField DataField="MaThuCung" HeaderText="Mã TC" />
                        <asp:BoundField DataField="TenThuCung" HeaderText="Tên thú cưng" />
                        <asp:BoundField DataField="Loai" HeaderText="Loài" />
                        <asp:BoundField DataField="Giong" HeaderText="Giống" />
                        <asp:BoundField DataField="Tuoi" HeaderText="Tuổi" />
                        <asp:BoundField DataField="GioiTinh" HeaderText="Giới tính" />
                        <asp:BoundField DataField="Gia" HeaderText="Giá" DataFormatString="{0:N0} đ" />
                        <asp:BoundField DataField="SoLuong" HeaderText="SL còn" />
                        <asp:BoundField DataField="TinhTrangSucKhoe" HeaderText="Sức khỏe" />
                    </Columns>
                </asp:GridView>
            </div>
        </div>
    </form>
</body>
</html>

