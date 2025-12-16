<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="QuanLyNhaCungCap.aspx.cs" Inherits="QuanLyCuaHangThuCung.Admin.QuanLyNhaCungCap" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <meta charset="utf-8" />
    <title>Quản lý nhà cung cấp - Admin</title>
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
        .form-group {
            margin-bottom: 15px;
        }
        label {
            display: block;
            margin-bottom: 5px;
            font-weight: bold;
        }
        input[type="text"], input[type="email"], textarea {
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
            background: #667eea;
            color: white;
        }
        .btn-danger {
            background: #dc3545;
            color: white;
        }
        .btn-success {
            background: #28a745;
            color: white;
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
            <a href="AdminDashboard.aspx">← Về Dashboard</a>
            <h2 style="display: inline-block; margin: 0;">Quản lý nhà cung cấp</h2>
        </div>

        <div class="container">
            <div class="form-section">
                <h3>Thêm/Sửa nhà cung cấp</h3>
                <div class="form-row">
                    <div class="form-group">
                        <label>Mã nhà cung cấp:</label>
                        <asp:TextBox ID="txtMaNCC" runat="server"></asp:TextBox>
                    </div>
                    <div class="form-group">
                        <label>Tên nhà cung cấp:</label>
                        <asp:TextBox ID="txtTenNCC" runat="server"></asp:TextBox>
                    </div>
                </div>
                <div class="form-row">
                    <div class="form-group">
                        <label>Địa chỉ:</label>
                        <asp:TextBox ID="txtDiaChi" runat="server"></asp:TextBox>
                    </div>
                    <div class="form-group">
                        <label>Số điện thoại:</label>
                        <asp:TextBox ID="txtSDT" runat="server"></asp:TextBox>
                    </div>
                </div>
                <div class="form-row">
                    <div class="form-group">
                        <label>Email:</label>
                        <asp:TextBox ID="txtEmail" runat="server" TextMode="Email"></asp:TextBox>
                    </div>
                </div>
                <div class="form-group">
                    <label>Mô tả:</label>
                    <asp:TextBox ID="txtMoTa" runat="server" TextMode="MultiLine" Rows="3"></asp:TextBox>
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
                <h3>Danh sách nhà cung cấp</h3>
                <asp:GridView ID="gvNhaCungCap" runat="server" CssClass="gridview" AutoGenerateColumns="False" OnSelectedIndexChanged="gvNhaCungCap_SelectedIndexChanged" DataKeyNames="MaNCC">
                    <Columns>
                        <asp:CommandField ShowSelectButton="True" ButtonType="Button" SelectText="Chọn" />
                        <asp:BoundField DataField="MaNCC" HeaderText="Mã NCC" />
                        <asp:BoundField DataField="TenNCC" HeaderText="Tên nhà cung cấp" />
                        <asp:BoundField DataField="DiaChi" HeaderText="Địa chỉ" />
                        <asp:BoundField DataField="SDT" HeaderText="SĐT" />
                        <asp:BoundField DataField="Email" HeaderText="Email" />
                        <asp:BoundField DataField="MoTa" HeaderText="Mô tả" />
                    </Columns>
                </asp:GridView>
            </div>
        </div>
    </form>
</body>
</html>

