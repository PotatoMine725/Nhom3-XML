<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Login.aspx.cs" Inherits="QuanLyCuaHangThuCung.Login" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <meta charset="utf-8" />
    <title>Đăng nhập - Quản lý cửa hàng thú cưng</title>
    <style>
        body {
            font-family: Arial, sans-serif;
            background: linear-gradient(135deg, #667eea 0%, #764ba2 100%);
            display: flex;
            justify-content: center;
            align-items: center;
            height: 100vh;
            margin: 0;
        }
        .login-container {
            background: white;
            padding: 40px;
            border-radius: 10px;
            box-shadow: 0 10px 30px rgba(0,0,0,0.3);
            width: 400px;
        }
        h1 {
            text-align: center;
            color: #333;
            margin-bottom: 30px;
        }
        .form-group {
            margin-bottom: 20px;
        }
        label {
            display: block;
            margin-bottom: 5px;
            color: #555;
            font-weight: bold;
        }
        input[type="text"], input[type="password"] {
            width: 100%;
            padding: 12px;
            border: 1px solid #ddd;
            border-radius: 5px;
            font-size: 14px;
            box-sizing: border-box;
        }
        .btn-login {
            width: 100%;
            padding: 12px;
            background: #667eea;
            color: white;
            border: none;
            border-radius: 5px;
            font-size: 16px;
            cursor: pointer;
            font-weight: bold;
        }
        .btn-login:hover {
            background: #5568d3;
        }
        .error-message {
            color: red;
            text-align: center;
            margin-top: 10px;
            padding: 10px;
            background: #ffe6e6;
            border-radius: 5px;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
        <div class="login-container">
            <h1>Đăng nhập hệ thống</h1>
            
            <div class="form-group">
                <label for="txtMaNhanVien">Mã nhân viên:</label>
                <asp:TextBox ID="txtMaNhanVien" runat="server" CssClass="form-control" placeholder="Nhập mã nhân viên"></asp:TextBox>
            </div>
            
            <div class="form-group">
                <label for="txtMatKhau">Mật khẩu:</label>
                <asp:TextBox ID="txtMatKhau" runat="server" TextMode="Password" CssClass="form-control" placeholder="Nhập mật khẩu"></asp:TextBox>
            </div>
            
            <asp:Button ID="btnDangNhap" runat="server" Text="Đăng nhập" CssClass="btn-login" OnClick="btnDangNhap_Click" />
            
            <asp:Label ID="lblThongBao" runat="server" CssClass="error-message" Visible="false"></asp:Label>
        </div>
    </form>
</body>
</html>

