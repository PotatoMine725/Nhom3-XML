<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="QuanLyThuCung.aspx.cs" Inherits="QuanLyCuaHangThuCung.Admin.QuanLyThuCung" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <meta charset="utf-8" />
    <title>Quản lý thú cưng - Admin</title>
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
        input[type="text"], input[type="number"], select, textarea {
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
            <h2 style="display: inline-block; margin: 0;">Quản lý thú cưng</h2>
        </div>

        <div class="container">
            <div class="form-section">
                <h3>Thêm/Sửa thú cưng</h3>
                <div class="form-row">
                    <div class="form-group">
                        <label>Mã thú cưng:</label>
                        <asp:TextBox ID="txtMaThuCung" runat="server"></asp:TextBox>
                    </div>
                    <div class="form-group">
                        <label>Tên thú cưng:</label>
                        <asp:TextBox ID="txtTenThuCung" runat="server"></asp:TextBox>
                    </div>
                </div>
                <div class="form-row">
                    <div class="form-group">
                        <label>Loài:</label>
                        <asp:DropDownList ID="ddlLoai" runat="server">
                            <asp:ListItem Value="Chó" Text="Chó"></asp:ListItem>
                            <asp:ListItem Value="Mèo" Text="Mèo"></asp:ListItem>
                            <asp:ListItem Value="Chim" Text="Chim"></asp:ListItem>
                            <asp:ListItem Value="Cá" Text="Cá"></asp:ListItem>
                            <asp:ListItem Value="Khác" Text="Khác"></asp:ListItem>
                        </asp:DropDownList>
                    </div>
                    <div class="form-group">
                        <label>Giống:</label>
                        <asp:TextBox ID="txtGiong" runat="server"></asp:TextBox>
                    </div>
                </div>
                <div class="form-row">
                    <div class="form-group">
                        <label>Tuổi (tháng):</label>
                        <asp:TextBox ID="txtTuoi" runat="server" TextMode="Number"></asp:TextBox>
                    </div>
                    <div class="form-group">
                        <label>Giới tính:</label>
                        <asp:DropDownList ID="ddlGioiTinh" runat="server">
                            <asp:ListItem Value="Đực" Text="Đực"></asp:ListItem>
                            <asp:ListItem Value="Cái" Text="Cái"></asp:ListItem>
                        </asp:DropDownList>
                    </div>
                </div>
                <div class="form-row">
                    <div class="form-group">
                        <label>Giá (VNĐ):</label>
                        <asp:TextBox ID="txtGia" runat="server" TextMode="Number"></asp:TextBox>
                    </div>
                    <div class="form-group">
                        <label>Số lượng:</label>
                        <asp:TextBox ID="txtSoLuong" runat="server" TextMode="Number"></asp:TextBox>
                    </div>
                </div>
                <div class="form-row">
                    <div class="form-group">
                        <label>Tình trạng sức khỏe:</label>
                        <asp:DropDownList ID="ddlTinhTrangSucKhoe" runat="server">
                            <asp:ListItem Value="Khỏe mạnh" Text="Khỏe mạnh"></asp:ListItem>
                            <asp:ListItem Value="Bình thường" Text="Bình thường"></asp:ListItem>
                            <asp:ListItem Value="Cần chăm sóc" Text="Cần chăm sóc"></asp:ListItem>
                        </asp:DropDownList>
                    </div>
                    <div class="form-group">
                        <label>Mã nhà cung cấp:</label>
                        <asp:DropDownList ID="ddlMaNCC" runat="server"></asp:DropDownList>
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
                <h3>Danh sách thú cưng</h3>
                <div class="form-group">
                    <label>Tìm kiếm:</label>
                    <asp:TextBox ID="txtTimKiem" runat="server" placeholder="Tìm theo tên, loài, giống..."></asp:TextBox>
                    <asp:Button ID="btnTimKiem" runat="server" Text="Tìm kiếm" CssClass="btn btn-primary" OnClick="btnTimKiem_Click" />
                </div>
                <asp:GridView ID="gvThuCung" runat="server" CssClass="gridview" AutoGenerateColumns="False" OnSelectedIndexChanged="gvThuCung_SelectedIndexChanged" DataKeyNames="MaThuCung">
                    <Columns>
                        <asp:CommandField ShowSelectButton="True" ButtonType="Button" SelectText="Chọn" />
                        <asp:BoundField DataField="MaThuCung" HeaderText="Mã TC" />
                        <asp:BoundField DataField="TenThuCung" HeaderText="Tên thú cưng" />
                        <asp:BoundField DataField="Loai" HeaderText="Loài" />
                        <asp:BoundField DataField="Giong" HeaderText="Giống" />
                        <asp:BoundField DataField="Tuoi" HeaderText="Tuổi" />
                        <asp:BoundField DataField="GioiTinh" HeaderText="Giới tính" />
                        <asp:BoundField DataField="Gia" HeaderText="Giá" DataFormatString="{0:N0} đ" />
                        <asp:BoundField DataField="SoLuong" HeaderText="SL" />
                        <asp:BoundField DataField="TinhTrangSucKhoe" HeaderText="Sức khỏe" />
                    </Columns>
                </asp:GridView>
            </div>
        </div>
    </form>
</body>
</html>

