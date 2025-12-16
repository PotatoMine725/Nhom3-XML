using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.IO;
using System.Data.SqlClient;
using System.Xml;
using System.Xml.Xsl;
using System.Xml.XPath;

namespace QuanLyCuaHangThuCung.App_Code
{
    public class FileXml
    {
        // Connection string với SQL Server DESKTOP-4488UKD
        string Conn = @"Data Source=Potato;Initial Catalog=QuanLyCuaHangThuCung;Integrated Security=True";
        
        // Lấy đường dẫn thư mục App_Data/XML
        private string GetXmlPath(string fileName)
        {
            return HttpContext.Current.Server.MapPath("~/App_Data/XML/" + fileName);
        }

        // Lấy đường dẫn thư mục App_Data/XSLT
        private string GetXsltPath(string fileName)
        {
            return HttpContext.Current.Server.MapPath("~/App_Data/XSLT/" + fileName);
        }

        // Hiển thị dữ liệu từ file XML
        public DataTable HienThi(string file)
        {
            DataTable dt = new DataTable();
            string filePath = GetXmlPath(file);
            
            if (File.Exists(filePath))
            {
                FileStream fsReadXML = new FileStream(filePath, FileMode.Open);
                dt.ReadXml(fsReadXML);
                fsReadXML.Close();
            }
            else
            {
                // Tạo file XML rỗng nếu chưa tồn tại
                dt.TableName = file.Replace(".xml", "");
                dt.WriteXml(filePath, XmlWriteMode.WriteSchema);
            }

            return dt;
        }

        // Tạo file XML từ bảng SQL
        public void TaoXML(string bang)
        {
            SqlConnection con = new SqlConnection(Conn);
            try
            {
                con.Open();
                string sql = "SELECT * FROM " + bang;
                SqlDataAdapter ad = new SqlDataAdapter(sql, con);
                DataTable dt = new DataTable(bang);
                ad.Fill(dt);
                
                string filePath = GetXmlPath(bang + ".xml");
                dt.WriteXml(filePath, XmlWriteMode.WriteSchema);
            }
            catch (Exception ex)
            {
                throw new Exception("Lỗi khi tạo XML từ bảng " + bang + ": " + ex.Message);
            }
            finally
            {
                if (con.State == System.Data.ConnectionState.Open)
                    con.Close();
            }
        }

        // Thêm dữ liệu vào file XML
        public void Them(string duongDan, string noiDung)
        {
            string filePath = GetXmlPath(duongDan);
            
            if (!File.Exists(filePath))
            {
                // Tạo file XML mới nếu chưa tồn tại
                XmlDocument doc = new XmlDocument();
                doc.LoadXml("<?xml version=\"1.0\" standalone=\"yes\"?><NewDataSet></NewDataSet>");
                doc.Save(filePath);
            }

            XmlTextReader reader = new XmlTextReader(filePath);
            XmlDocument doc2 = new XmlDocument();
            doc2.Load(reader);
            reader.Close();
            
            XmlNode currNode;
            XmlDocumentFragment docFrag = doc2.CreateDocumentFragment();
            docFrag.InnerXml = noiDung;
            currNode = doc2.DocumentElement;
            currNode.InsertAfter(docFrag, currNode.LastChild);
            doc2.Save(filePath);
        }

        // Xóa dữ liệu từ file XML
        public void Xoa(string duongDan, string tenFileXML, string xoaTheoTruong, string giaTriTruong)
        {
            string filePath = GetXmlPath(duongDan);
            XmlDocument doc = new XmlDocument();
            doc.Load(filePath);
            
            // Tìm node cần xóa
            XmlNode nodeCu = doc.SelectSingleNode("NewDataSet/" + "_x0027_" + tenFileXML + "_x0027_" + "[./" + xoaTheoTruong + "/text()='" + giaTriTruong + "']");
            
            if (nodeCu != null)
            {
                doc.DocumentElement.RemoveChild(nodeCu);
                doc.Save(filePath);
            }
        }

        // Sửa dữ liệu trong file XML
        public void Sua(string duongDan, string tenFile, string suaTheoTruong, string giaTriTruong, string noiDung)
        {
            string filePath = GetXmlPath(duongDan);
            XmlTextReader reader = new XmlTextReader(filePath);
            XmlDocument doc = new XmlDocument();
            doc.Load(reader);
            reader.Close();
            
            XmlNode oldNode = doc.SelectSingleNode("NewDataSet/" + "_x0027_" + tenFile + "_x0027_" + "[./" + suaTheoTruong + "/text()='" + giaTriTruong + "']");
            
            if (oldNode != null)
            {
                XmlElement newNode = doc.CreateElement("_x0027_" + tenFile + "_x0027_");
                newNode.InnerXml = noiDung;
                doc.DocumentElement.ReplaceChild(newNode, oldNode);
                doc.Save(filePath);
            }
        }

        // Lấy giá trị từ file XML
        public string LayGiaTri(string duongDan, string truongA, string giaTriA, string truongB)
        {
            string giatriB = "";
            DataTable dt = HienThi(duongDan);
            int soDong = dt.Rows.Count;
            
            for (int i = 0; i < soDong; i++)
            {
                if (dt.Rows[i][truongA].ToString().Trim().Equals(giaTriA))
                {
                    giatriB = dt.Rows[i][truongB].ToString();
                    return giatriB;
                }
            }
            return giatriB;
        }

        // Đổi mật khẩu trong XML
        public void DoiMatKhau(string nguoiDung, string matKhau)
        {
            string filePath = GetXmlPath("TaiKhoan.xml");
            XmlDocument doc = new XmlDocument();
            doc.Load(filePath);
            XmlNode node = doc.SelectSingleNode("NewDataSet/TaiKhoan[MaNhanVien = '" + nguoiDung + "']");
            
            if (node != null)
            {
                node.ChildNodes[1].InnerText = matKhau;
                doc.Save(filePath);
            }
        }

        // Tìm kiếm và transform XML bằng XSLT
        public string TimKiemXSLT(string data, string tenFileXML, string tenfileXSLT)
        {
            try
            {
                XslCompiledTransform xslt = new XslCompiledTransform();
                string xsltPath = GetXsltPath(tenfileXSLT + ".xslt");
                xslt.Load(xsltPath);
                
                XsltArgumentList argList = new XsltArgumentList();
                argList.AddParam("Data", "", data);
                
                string xmlPath = GetXmlPath(tenFileXML + ".xml");
                string outputPath = HttpContext.Current.Server.MapPath("~/App_Data/Output/" + tenFileXML + ".html");
                
                // Đảm bảo thư mục Output tồn tại
                Directory.CreateDirectory(Path.GetDirectoryName(outputPath));
                
                XmlWriter writer = XmlWriter.Create(outputPath);
                xslt.Transform(new XPathDocument(xmlPath), argList, writer);
                writer.Close();
                
                return "~/App_Data/Output/" + tenFileXML + ".html";
            }
            catch (Exception ex)
            {
                throw new Exception("Lỗi khi transform XSLT: " + ex.Message);
            }
        }

        // Thực thi SQL Insert/Update/Delete
        public void InsertOrUpDateSQL(string sql)
        {
            SqlConnection con = new SqlConnection(Conn);
            try
            {
                con.Open();
                SqlCommand cmd = new SqlCommand(sql, con);
                cmd.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                throw new Exception("Lỗi khi thực thi SQL: " + ex.Message);
            }
            finally
            {
                if (con.State == System.Data.ConnectionState.Open)
                    con.Close();
            }
        }

        // Lấy dữ liệu từ SQL trả về DataTable
        public DataTable GetDataFromSQL(string sql)
        {
            SqlConnection con = new SqlConnection(Conn);
            DataTable dt = new DataTable();
            try
            {
                con.Open();
                SqlDataAdapter ad = new SqlDataAdapter(sql, con);
                ad.Fill(dt);
            }
            catch (Exception ex)
            {
                throw new Exception("Lỗi khi lấy dữ liệu từ SQL: " + ex.Message);
            }
            finally
            {
                if (con.State == System.Data.ConnectionState.Open)
                    con.Close();
            }
            return dt;
        }
    }
}

