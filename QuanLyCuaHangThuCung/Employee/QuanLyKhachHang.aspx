<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="QuanLyKhachHang.aspx.cs" Inherits="QuanLyCuaHangThuCung.Employee.QuanLyKhachHang" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <meta charset="utf-8" />
    <title>Quản lý khách hàng - Nhân viên</title>
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
        input[type="text"], input[type="email"] {
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
        .btn-success {
            background: #28a745;
            color: white;
        }
        .btn-danger {
            background: #dc3545;
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
        .form-row {
            display: flex;
            gap: 15px;
        }
        .form-row .form-group {
            flex: 1;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
        <div class="header">
            <a href="EmployeeDashboard.aspx">← Về Dashboard</a>
            <h2 style="display: inline-block; margin: 0;">Quản lý khách hàng</h2>
        </div>

        <div class="container">
            <div class="form-section">
                <h3>Thêm/Sửa khách hàng</h3>
                <div class="form-row">
                    <div class="form-group">
                        <label>Mã khách hàng:</label>
                        <asp:TextBox ID="txtMaKhachHang" runat="server"></asp:TextBox>
                    </div>
                    <div class="form-group">
                        <label>Tên khách hàng:</label>
                        <asp:TextBox ID="txtTenKhachHang" runat="server"></asp:TextBox>
                    </div>
                </div>
                <div class="form-row">
                    <div class="form-group">
                        <label>Số điện thoại:</label>
                        <asp:TextBox ID="txtSDT" runat="server"></asp:TextBox>
                    </div>
                    <div class="form-group">
                        <label>Email:</label>
                        <asp:TextBox ID="txtEmail" runat="server" TextMode="Email"></asp:TextBox>
                    </div>
                </div>
                <div class="form-group">
                    <label>Địa chỉ:</label>
                    <asp:TextBox ID="txtDiaChi" runat="server"></asp:TextBox>
                </div>
                <div>
                    <asp:Button ID="btnThem" runat="server" Text="Thêm mới" CssClass="btn btn-primary" OnClick="btnThem_Click" />
                    <asp:Button ID="btnSua" runat="server" Text="Cập nhật" CssClass="btn btn-success" OnClick="btnSua_Click" />
                    <asp:Button ID="btnXoa" runat="server" Text="Xóa" CssClass="btn btn-danger" OnClick="btnXoa_Click" />
                    <asp:Button ID="btnReset" runat="server" Text="Làm mới" CssClass="btn" OnClick="btnReset_Click" />
                </div>
                <asp:Label ID="lblThongBao" runat="server" ForeColor="Red"></asp:Label>
            </div>

            <div class="form-section">
                <h3>Danh sách khách hàng</h3>
                <div class="form-group">
                    <label>Tìm kiếm:</label>
                    <asp:TextBox ID="txtTimKiem" runat="server" placeholder="Tìm theo tên, SĐT, email..."></asp:TextBox>
                    <asp:Button ID="btnTimKiem" runat="server" Text="Tìm kiếm" CssClass="btn btn-primary" OnClick="btnTimKiem_Click" />
                </div>
                <asp:GridView ID="gvKhachHang" runat="server" CssClass="gridview" AutoGenerateColumns="False" 
                    OnSelectedIndexChanged="gvKhachHang_SelectedIndexChanged" DataKeyNames="MaKhachHang">
                    <Columns>
                        <asp:CommandField ShowSelectButton="True" ButtonType="Button" SelectText="Chọn" />
                        <asp:BoundField DataField="MaKhachHang" HeaderText="Mã KH" />
                        <asp:BoundField DataField="TenKhachHang" HeaderText="Tên khách hàng" />
                        <asp:BoundField DataField="SDT" HeaderText="SĐT" />
                        <asp:BoundField DataField="Email" HeaderText="Email" />
                        <asp:BoundField DataField="DiaChi" HeaderText="Địa chỉ" />
                        <asp:BoundField DataField="NgayDangKy" HeaderText="Ngày đăng ký" />
                    </Columns>
                </asp:GridView>
            </div>

            <div class="form-section">
                <h3>Lịch sử mua hàng</h3>
                <asp:GridView ID="gvLichSuMuaHang" runat="server" CssClass="gridview" AutoGenerateColumns="False">
                    <Columns>
                        <asp:BoundField DataField="SoHoaDon" HeaderText="Số HĐ" />
                        <asp:BoundField DataField="NgayLap" HeaderText="Ngày" />
                        <asp:BoundField DataField="TongTien" HeaderText="Tổng tiền" DataFormatString="{0:N0} đ" />
                    </Columns>
                </asp:GridView>
            </div>
        </div>
    </form>
</body>
</html>

