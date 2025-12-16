<?xml version="1.0" encoding="utf-8"?>
<xsl:stylesheet version="1.0" xmlns:xsl="http://www.w3.org/1999/XSL/Transform"
    xmlns:msxsl="urn:schemas-microsoft-com:xslt" exclude-result-prefixes="msxsl">
    <xsl:output method="html" indent="yes"/>
    <xsl:param name="Data"></xsl:param>
    <xsl:template match="/NewDataSet">
        <html>
            <head>
                <title>Danh sách thú cưng</title>
                <style>
                    body {
                        font-family: Arial, sans-serif;
                        margin: 20px;
                        background: #f5f5f5;
                    }
                    h1 {
                        text-align: center;
                        color: #667eea;
                    }
                    table {
                        width: 100%;
                        border-collapse: collapse;
                        background: white;
                        box-shadow: 0 2px 5px rgba(0,0,0,0.1);
                    }
                    th {
                        background: #667eea;
                        color: white;
                        padding: 12px;
                        text-align: left;
                    }
                    td {
                        padding: 10px;
                        border-bottom: 1px solid #ddd;
                    }
                    tr:hover {
                        background: #f0f0f0;
                    }
                </style>
            </head>
            <body>
                <h1>🐾 DANH SÁCH THÚ CƯNG</h1>
                <table border="1">
                    <tr>
                        <th>STT</th>
                        <th>Mã thú cưng</th>
                        <th>Tên thú cưng</th>
                        <th>Loài</th>
                        <th>Giống</th>
                        <th>Tuổi</th>
                        <th>Giới tính</th>
                        <th>Giá</th>
                        <th>Số lượng</th>
                        <th>Tình trạng sức khỏe</th>
                    </tr>
                    <xsl:for-each select="ThuCung">
                        <xsl:if test="not($Data) or MaThuCung[.=$Data] or TenThuCung[contains(., $Data)] or Loai[contains(., $Data)]">
                            <tr>
                                <td>
                                    <xsl:value-of select="position()"/>
                                </td>
                                <td>
                                    <xsl:value-of select="MaThuCung"/>
                                </td>
                                <td>
                                    <xsl:value-of select="TenThuCung"/>
                                </td>
                                <td>
                                    <xsl:value-of select="Loai"/>
                                </td>
                                <td>
                                    <xsl:value-of select="Giong"/>
                                </td>
                                <td>
                                    <xsl:value-of select="Tuoi"/> tháng
                                </td>
                                <td>
                                    <xsl:value-of select="GioiTinh"/>
                                </td>
                                <td>
                                    <xsl:value-of select="format-number(Gia, '#,##0')"/> đ
                                </td>
                                <td>
                                    <xsl:value-of select="SoLuong"/>
                                </td>
                                <td>
                                    <xsl:value-of select="TinhTrangSucKhoe"/>
                                </td>
                            </tr>
                        </xsl:if>
                    </xsl:for-each>
                </table>
            </body>
        </html>
    </xsl:template>
</xsl:stylesheet>
