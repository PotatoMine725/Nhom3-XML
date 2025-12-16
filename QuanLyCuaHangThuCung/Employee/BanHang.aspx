<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="BanHang.aspx.cs" Inherits="QuanLyCuaHangThuCung.Employee.BanHang" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <meta charset="utf-8" />
    <title>Bán hàng - Nhân viên</title>
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
            max-width: 1400px;
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
        .form-row {
            display: flex;
            gap: 15px;
            margin-bottom: 15px;
        }
        .form-group {
            flex: 1;
        }
        label {
            display: block;
            margin-bottom: 5px;
            font-weight: bold;
        }
        input[type="text"], input[type="number"], select {
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
        .total-section {
            background: #e8f5e9;
            padding: 15px;
            border-radius: 5px;
            margin-top: 20px;
            text-align: right;
        }
        .total-section h3 {
            margin: 0;
            font-size: 24px;
            color: #2e7d32;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
        <div class="header">
            <a href="EmployeeDashboard.aspx">← Về Dashboard</a>
            <h2 style="display: inline-block; margin: 0;">Bán hàng</h2>
        </div>

        <div class="container">
            <div class="form-section">
                <h3>Thông tin khách hàng</h3>
                <div class="form-row">
                    <div class="form-group">
                        <label>Mã khách hàng:</label>
                        <asp:TextBox ID="txtMaKhachHang" runat="server"></asp:TextBox>
                    </div>
                    <div class="form-group">
                        <label>Tên khách hàng:</label>
                        <asp:TextBox ID="txtTenKhachHang" runat="server"></asp:TextBox>
                    </div>
                    <div class="form-group">
                        <label>Số điện thoại:</label>
                        <asp:TextBox ID="txtSDT" runat="server"></asp:TextBox>
                    </div>
                </div>
                <div>
                    <asp:Button ID="btnTimKhachHang" runat="server" Text="Tìm khách hàng" CssClass="btn btn-primary" OnClick="btnTimKhachHang_Click" />
                    <asp:Button ID="btnThemKhachHang" runat="server" Text="Thêm khách hàng mới" CssClass="btn btn-success" OnClick="btnThemKhachHang_Click" />
                </div>
            </div>

            <div class="form-section">
                <h3>Chọn thú cưng</h3>
                <div class="form-row">
                    <div class="form-group">
                        <label>Mã thú cưng:</label>
                        <asp:DropDownList ID="ddlMaThuCung" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddlMaThuCung_SelectedIndexChanged"></asp:DropDownList>
                    </div>
                    <div class="form-group">
                        <label>Tên thú cưng:</label>
                        <asp:Label ID="lblTenThuCung" runat="server" Text=""></asp:Label>
                    </div>
                    <div class="form-group">
                        <label>Giá:</label>
                        <asp:Label ID="lblGia" runat="server" Text=""></asp:Label>
                    </div>
                    <div class="form-group">
                        <label>Số lượng còn:</label>
                        <asp:Label ID="lblSoLuongCon" runat="server" Text=""></asp:Label>
                    </div>
                    <div class="form-group">
                        <label>Số lượng mua:</label>
                        <asp:TextBox ID="txtSoLuongMua" runat="server" TextMode="Number" Text="1"></asp:TextBox>
                    </div>
                </div>
                <div>
                    <asp:Button ID="btnThemVaoGio" runat="server" Text="Thêm vào giỏ" CssClass="btn btn-primary" OnClick="btnThemVaoGio_Click" />
                </div>
            </div>

            <div class="form-section">
                <h3>Giỏ hàng</h3>
                <asp:GridView ID="gvGioHang" runat="server" CssClass="gridview" AutoGenerateColumns="False" 
                    OnRowDeleting="gvGioHang_RowDeleting" DataKeyNames="MaThuCung">
                    <Columns>
                        <asp:CommandField ShowDeleteButton="True" ButtonType="Button" DeleteText="Xóa" />
                        <asp:BoundField DataField="MaThuCung" HeaderText="Mã TC" />
                        <asp:BoundField DataField="TenThuCung" HeaderText="Tên thú cưng" />
                        <asp:BoundField DataField="DonGia" HeaderText="Đơn giá" DataFormatString="{0:N0} đ" />
                        <asp:BoundField DataField="SoLuong" HeaderText="Số lượng" />
                        <asp:BoundField DataField="ThanhTien" HeaderText="Thành tiền" DataFormatString="{0:N0} đ" />
                    </Columns>
                </asp:GridView>
                
                <div class="total-section">
                    <h3>Tổng tiền: <asp:Label ID="lblTongTien" runat="server" Text="0"></asp:Label> đ</h3>
                </div>
                
                <div style="margin-top: 20px;">
                    <asp:Button ID="btnThanhToan" runat="server" Text="Thanh toán" CssClass="btn btn-success" OnClick="btnThanhToan_Click" />
                    <asp:Button ID="btnXoaGioHang" runat="server" Text="Xóa giỏ hàng" CssClass="btn btn-danger" OnClick="btnXoaGioHang_Click" />
                </div>
                <asp:Label ID="lblThongBao" runat="server" ForeColor="Red"></asp:Label>
            </div>
        </div>
    </form>
</body>
</html>

