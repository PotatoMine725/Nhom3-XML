<?xml version="1.0" encoding="utf-8"?>
<xsl:stylesheet version="1.0" xmlns:xsl="http://www.w3.org/1999/XSL/Transform"
    xmlns:msxsl="urn:schemas-microsoft-com:xslt" exclude-result-prefixes="msxsl">
    <xsl:output method="html" indent="yes"/>
    <xsl:param name="Data"></xsl:param>
    <xsl:template match="/NewDataSet">
        <html>
            <head>
                <title>Hóa đơn</title>
                <style>
                    body {
                        font-family: Arial, sans-serif;
                        margin: 20px;
                        background: #f5f5f5;
                    }
                    .invoice {
                        background: white;
                        padding: 30px;
                        max-width: 800px;
                        margin: 0 auto;
                        box-shadow: 0 2px 10px rgba(0,0,0,0.1);
                    }
                    h1 {
                        text-align: center;
                        color: #667eea;
                        border-bottom: 2px solid #667eea;
                        padding-bottom: 10px;
                    }
                    .info {
                        margin: 20px 0;
                    }
                    table {
                        width: 100%;
                        border-collapse: collapse;
                        margin: 20px 0;
                    }
                    th {
                        background: #667eea;
                        color: white;
                        padding: 10px;
                        text-align: left;
                    }
                    td {
                        padding: 8px;
                        border-bottom: 1px solid #ddd;
                    }
                    .total {
                        text-align: right;
                        font-size: 18px;
                        font-weight: bold;
                        margin-top: 20px;
                        color: #667eea;
                    }
                </style>
            </head>
            <body>
                <div class="invoice">
                    <h1>💰 HÓA ĐƠN BÁN HÀNG</h1>
                    <xsl:for-each select="HoaDon">
                        <xsl:if test="not($Data) or SoHoaDon[.=$Data]">
                            <div class="info">
                                <p><strong>Số hóa đơn:</strong> <xsl:value-of select="SoHoaDon"/></p>
                                <p><strong>Ngày lập:</strong> <xsl:value-of select="NgayLap"/></p>
                                <p><strong>Mã nhân viên:</strong> <xsl:value-of select="MaNhanVien"/></p>
                                <p><strong>Mã khách hàng:</strong> <xsl:value-of select="MaKhachHang"/></p>
                            </div>
                            
                            <table border="1">
                                <tr>
                                    <th>STT</th>
                                    <th>Mã thú cưng</th>
                                    <th>Đơn giá</th>
                                    <th>Số lượng</th>
                                    <th>Thành tiền</th>
                                </tr>
                                <xsl:for-each select="//ChiTietHoaDon[SoHoaDon = current()/SoHoaDon]">
                                    <tr>
                                        <td><xsl:value-of select="position()"/></td>
                                        <td><xsl:value-of select="MaThuCung"/></td>
                                        <td><xsl:value-of select="format-number(DonGia, '#,##0')"/> đ</td>
                                        <td><xsl:value-of select="SoLuong"/></td>
                                        <td><xsl:value-of select="format-number(DonGia * SoLuong, '#,##0')"/> đ</td>
                                    </tr>
                                </xsl:for-each>
                            </table>
                            
                            <div class="total">
                                <p>Tổng tiền: <xsl:value-of select="format-number(TongTien, '#,##0')"/> đ</p>
                            </div>
                        </xsl:if>
                    </xsl:for-each>
                </div>
            </body>
        </html>
    </xsl:template>
</xsl:stylesheet>
