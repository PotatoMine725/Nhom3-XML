<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="DongBoDuLieu.aspx.cs" Inherits="QuanLyCuaHangThuCung.Admin.DongBoDuLieu" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <meta charset="utf-8" />
    <title>Đồng bộ dữ liệu - Admin</title>
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
        .btn {
            padding: 15px 30px;
            border: none;
            border-radius: 5px;
            cursor: pointer;
            margin: 10px;
            font-size: 16px;
            font-weight: bold;
        }
        .btn-primary {
            background: #667eea;
            color: white;
        }
        .btn-success {
            background: #28a745;
            color: white;
        }
        .btn-warning {
            background: #ffc107;
            color: #333;
        }
        .btn:hover {
            opacity: 0.9;
        }
        .info-box {
            background: #e3f2fd;
            padding: 15px;
            border-radius: 5px;
            margin: 10px 0;
            border-left: 4px solid #2196f3;
        }
        .success-box {
            background: #e8f5e9;
            padding: 15px;
            border-radius: 5px;
            margin: 10px 0;
            border-left: 4px solid #4caf50;
        }
        .error-box {
            background: #ffebee;
            padding: 15px;
            border-radius: 5px;
            margin: 10px 0;
            border-left: 4px solid #f44336;
        }
        .table-list {
            width: 100%;
            border-collapse: collapse;
            margin-top: 20px;
        }
        .table-list th, .table-list td {
            padding: 10px;
            text-align: left;
            border-bottom: 1px solid #ddd;
        }
        .table-list th {
            background: #667eea;
            color: white;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
        <div class="header">
            <a href="AdminDashboard.aspx">← Về Dashboard</a>
            <h2 style="display: inline-block; margin: 0;">Đồng bộ dữ liệu XML ↔ SQL</h2>
        </div>

        <div class="container">
            <div class="form-section">
                <h3>Xuất dữ liệu SQL → XML</h3>
                <p>Xuất tất cả dữ liệu từ SQL Server sang các file XML trong thư mục App_Data/XML/</p>
                <asp:Button ID="btnXuatSQLToXML" runat="server" Text="Xuất SQL → XML" CssClass="btn btn-primary" OnClick="btnXuatSQLToXML_Click" />
                
                <asp:Panel ID="divXuatResult" runat="server" Visible="false">
                    <asp:Label ID="lblXuatResult" runat="server"></asp:Label>
                </asp:Panel>
            </div>

            <div class="form-section">
                <h3>Import dữ liệu XML → SQL</h3>
                <p class="error-box"><strong>⚠️ Cảnh báo:</strong> Thao tác này sẽ xóa toàn bộ dữ liệu hiện tại trong SQL và thay thế bằng dữ liệu từ XML!</p>
                <asp:Button ID="btnImportXMLToSQL" runat="server" Text="Import XML → SQL" CssClass="btn btn-warning" OnClick="btnImportXMLToSQL_Click" />
                
                <asp:Panel ID="divImportResult" runat="server" Visible="false">
                    <asp:Label ID="lblImportResult" runat="server"></asp:Label>
                </asp:Panel>
            </div>

            <div class="form-section">
                <h3>Đồng bộ hóa 2 chiều</h3>
                <p>Xuất dữ liệu từ SQL sang XML (không xóa dữ liệu SQL)</p>
                <asp:Button ID="btnDongBo" runat="server" Text="Đồng bộ hóa" CssClass="btn btn-success" OnClick="btnDongBo_Click" />
                
                <asp:Panel ID="divDongBoResult" runat="server" Visible="false">
                    <asp:Label ID="lblDongBoResult" runat="server"></asp:Label>
                </asp:Panel>
            </div>

            <div class="form-section">
                <h3>Danh sách các bảng</h3>
                <table class="table-list">
                    <tr>
                        <th>Tên bảng</th>
                        <th>Mô tả</th>
                        <th>File XML</th>
                    </tr>
                    <tr>
                        <td>NhanVien</td>
                        <td>Thông tin nhân viên</td>
                        <td>NhanVien.xml</td>
                    </tr>
                    <tr>
                        <td>TaiKhoan</td>
                        <td>Tài khoản đăng nhập</td>
                        <td>TaiKhoan.xml</td>
                    </tr>
                    <tr>
                        <td>ThuCung</td>
                        <td>Danh sách thú cưng</td>
                        <td>ThuCung.xml</td>
                    </tr>
                    <tr>
                        <td>KhachHang</td>
                        <td>Danh sách khách hàng</td>
                        <td>KhachHang.xml</td>
                    </tr>
                    <tr>
                        <td>HoaDon</td>
                        <td>Hóa đơn bán hàng</td>
                        <td>HoaDon.xml</td>
                    </tr>
                    <tr>
                        <td>ChiTietHoaDon</td>
                        <td>Chi tiết hóa đơn</td>
                        <td>ChiTietHoaDon.xml</td>
                    </tr>
                    <tr>
                        <td>NhaCungCap</td>
                        <td>Nhà cung cấp</td>
                        <td>NhaCungCap.xml</td>
                    </tr>
                    <tr>
                        <td>PhieuNhap</td>
                        <td>Phiếu nhập hàng</td>
                        <td>PhieuNhap.xml</td>
                    </tr>
                    <tr>
                        <td>DichVu</td>
                        <td>Dịch vụ</td>
                        <td>DichVu.xml</td>
                    </tr>
                    <tr>
                        <td>ChamCong</td>
                        <td>Chấm công</td>
                        <td>ChamCong.xml</td>
                    </tr>
                </table>
            </div>
        </div>
    </form>
</body>
</html>

