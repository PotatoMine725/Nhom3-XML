<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="QuanLyNhanVien.aspx.cs" Inherits="QuanLyCuaHangThuCung.Admin.QuanLyNhanVien" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <meta charset="utf-8" />
    <title>Quản lý nhân viên - Admin</title>
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
        input[type="text"], input[type="password"], select {
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
    </style>
</head>
<body>
    <form id="form1" runat="server">
        <div class="header">
            <a href="AdminDashboard.aspx">← Về Dashboard</a>
            <h2 style="display: inline-block; margin: 0;">Quản lý nhân viên</h2>
        </div>

        <div class="container">
            <div class="form-section">
                <h3>Thêm/Sửa nhân viên</h3>
                <div class="form-group">
                    <label>Mã nhân viên:</label>
                    <asp:TextBox ID="txtMaNhanVien" runat="server"></asp:TextBox>
                </div>
                <div class="form-group">
                    <label>Tên nhân viên:</label>
                    <asp:TextBox ID="txtTenNhanVien" runat="server"></asp:TextBox>
                </div>
                <div class="form-group">
                    <label>Ngày sinh:</label>
                    <asp:TextBox ID="txtNgaySinh" runat="server" placeholder="dd/MM/yyyy"></asp:TextBox>
                </div>
                <div class="form-group">
                    <label>Địa chỉ:</label>
                    <asp:TextBox ID="txtDiaChi" runat="server"></asp:TextBox>
                </div>
                <div class="form-group">
                    <label>Số điện thoại:</label>
                    <asp:TextBox ID="txtSDT" runat="server"></asp:TextBox>
                </div>
                <div class="form-group">
                    <label>Email:</label>
                    <asp:TextBox ID="txtEmail" runat="server"></asp:TextBox>
                </div>
                <div class="form-group">
                    <label>Chức vụ:</label>
                    <asp:TextBox ID="txtChucVu" runat="server"></asp:TextBox>
                </div>
                <div class="form-group">
                    <label>Trạng thái:</label>
                    <asp:DropDownList ID="ddlTrangThai" runat="server">
                        <asp:ListItem Value="Đang làm việc" Text="Đang làm việc"></asp:ListItem>
                        <asp:ListItem Value="Nghỉ việc" Text="Nghỉ việc"></asp:ListItem>
                    </asp:DropDownList>
                </div>
                <div class="form-group">
                    <label>Quyền truy cập:</label>
                    <asp:DropDownList ID="ddlQuyen" runat="server">
                        <asp:ListItem Value="0" Text="Nhân viên"></asp:ListItem>
                        <asp:ListItem Value="1" Text="Admin"></asp:ListItem>
                    </asp:DropDownList>
                </div>
                <div class="form-group">
                    <label>Mật khẩu (để trống nếu không đổi):</label>
                    <asp:TextBox ID="txtMatKhau" runat="server" TextMode="Password"></asp:TextBox>
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
                <h3>Danh sách nhân viên</h3>
                <asp:GridView ID="gvNhanVien" runat="server" CssClass="gridview" AutoGenerateColumns="False" OnSelectedIndexChanged="gvNhanVien_SelectedIndexChanged" DataKeyNames="MaNhanVien">
                    <Columns>
                        <asp:CommandField ShowSelectButton="True" ButtonType="Button" SelectText="Chọn" />
                        <asp:BoundField DataField="MaNhanVien" HeaderText="Mã NV" />
                        <asp:BoundField DataField="TenNhanVien" HeaderText="Tên nhân viên" />
                        <asp:BoundField DataField="NgaySinh" HeaderText="Ngày sinh" />
                        <asp:BoundField DataField="SDT" HeaderText="SĐT" />
                        <asp:BoundField DataField="Email" HeaderText="Email" />
                        <asp:BoundField DataField="ChucVu" HeaderText="Chức vụ" />
                        <asp:BoundField DataField="TrangThai" HeaderText="Trạng thái" />
                    </Columns>
                </asp:GridView>
                <asp:Label ID="lblThongBaoGrid" runat="server"></asp:Label>
            </div>
        </div>
    </form>
</body>
</html>

