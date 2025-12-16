<?xml version="1.0" encoding="utf-8"?>
<xsl:stylesheet version="1.0" xmlns:xsl="http://www.w3.org/1999/XSL/Transform"
    xmlns:msxsl="urn:schemas-microsoft-com:xslt" exclude-result-prefixes="msxsl">
    <xsl:output method="html" indent="yes"/>
    <xsl:param name="Data"></xsl:param>
    <xsl:template match="/NewDataSet">
        <html>
            <head>
                <title>Danh sách nhân viên</title>
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
                <h1>👥 DANH SÁCH NHÂN VIÊN</h1>
                <table border="1">
                    <tr>
                        <th>STT</th>
                        <th>Mã nhân viên</th>
                        <th>Tên nhân viên</th>
                        <th>Ngày sinh</th>
                        <th>Địa chỉ</th>
                        <th>SĐT</th>
                        <th>Email</th>
                        <th>Chức vụ</th>
                        <th>Trạng thái</th>
                    </tr>
                    <xsl:for-each select="NhanVien">
                        <xsl:if test="not($Data) or MaNhanVien[.=$Data] or TenNhanVien[contains(., $Data)]">
                            <tr>
                                <td>
                                    <xsl:value-of select="position()"/>
                                </td>
                                <td>
                                    <xsl:value-of select="MaNhanVien"/>
                                </td>
                                <td>
                                    <xsl:value-of select="TenNhanVien"/>
                                </td>
                                <td>
                                    <xsl:value-of select="NgaySinh"/>
                                </td>
                                <td>
                                    <xsl:value-of select="DiaChi"/>
                                </td>
                                <td>
                                    <xsl:value-of select="SDT"/>
                                </td>
                                <td>
                                    <xsl:value-of select="Email"/>
                                </td>
                                <td>
                                    <xsl:value-of select="ChucVu"/>
                                </td>
                                <td>
                                    <xsl:value-of select="TrangThai"/>
                                </td>
                            </tr>
                        </xsl:if>
                    </xsl:for-each>
                </table>
            </body>
        </html>
    </xsl:template>
</xsl:stylesheet>
