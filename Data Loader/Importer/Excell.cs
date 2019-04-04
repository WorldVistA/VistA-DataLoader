/**
 * 
 * VistA Data Loader 2.0
 *
 * Copyright (C) 2012 Johns Hopkins University
 *
 * VistA Data Loader is provided by the Johns Hopkins University School of
 * Nursing, and funded by the Department of Health and Human Services, Office
 * of the National Coordinator for Health Information Technology under Award
 * Number #1U24OC000013-01.
 *
 *Licensed under the Apache License, Version 2.0 (the "License");
 *you may not use this file except in compliance with the License.
 *You may obtain a copy of the License at
 *
 *    http://www.apache.org/licenses/LICENSE-2.0
 *
 *Unless required by applicable law or agreed to in writing, software
 *distributed under the License is distributed on an "AS IS" BASIS,
 *WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
 *See the License for the specific language governing permissions and
 *limitations under the License.
 * 
 * Date Created: 5/30/2012
 * Developer:  Mike Stark
 * Description: 
 * 
 */

 using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Xml;
using System.IO;
using System.Web;
using System.Data.OleDb;
using System.Windows.Forms;

namespace DataLoader
{
    class ExcellImport
    {
        public static DataSet ImportExcelXLS(string FileName, bool hasHeaders)
        {
            string HDR = hasHeaders ? "Yes" : "No";
            string strConn;
            if (FileName.Substring(FileName.LastIndexOf('.')).ToLower() == ".xlsx")
                strConn = "Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" + FileName + ";Extended Properties=\"Excel 12.0;HDR=" + HDR + ";IMEX=0\"";
            else
                strConn = "Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" + FileName + ";Extended Properties=\"Excel 8.0;HDR=" + HDR + ";IMEX=0\"";

            DataSet output = new DataSet();

            using (OleDbConnection conn = new OleDbConnection(strConn))
            {
                output.Clear();
                try
                {
                    conn.Open();
                }
                catch
                {
                    MessageBox.Show("Unable to open excell file.", "Error!");
                    return output;
                }
                DataTable schemaTable = new DataTable();
                try
                {
                    schemaTable = conn.GetOleDbSchemaTable(
                        OleDbSchemaGuid.Tables, new object[] { null, null, null, "TABLE" });
                }
                catch
                {
                    MessageBox.Show("Unable to open excell file.","Error!");
                    return output;
                }
                foreach (DataRow schemaRow in schemaTable.Rows)
                {
                    string sheet = schemaRow["TABLE_NAME"].ToString();

                    if (!sheet.EndsWith("_"))
                    {
                        try
                        {
                            OleDbCommand cmd = new OleDbCommand("SELECT * FROM [" + sheet + "]", conn);
                            cmd.CommandType = CommandType.Text;

                            DataTable outputTable = new DataTable(sheet);
                            output.Tables.Add(outputTable);
                            new OleDbDataAdapter(cmd).Fill(outputTable);
                        }
                        catch (Exception ex)
                        {
                            throw new Exception(ex.Message + string.Format("Sheet:{0}.File:F{1}", sheet, FileName), ex);
                        }
                    }
                }
            }
            return output;
        }

        public static DataSet ImportExcelXML(Stream inputFileStream,
                          bool hasHeaders, bool autoDetectColumnType)
        {
            XmlDocument doc = new XmlDocument();
            doc.Load(new XmlTextReader(inputFileStream));
            XmlNamespaceManager nsmgr = new XmlNamespaceManager(doc.NameTable);

            nsmgr.AddNamespace("o", "urn:schemas-microsoft-com:office:office");
            nsmgr.AddNamespace("x", "urn:schemas-microsoft-com:office:excel");
            nsmgr.AddNamespace("ss", "urn:schemas-microsoft-com:office:spreadsheet");

            DataSet ds = new DataSet();

            foreach (XmlNode node in
              doc.DocumentElement.SelectNodes("//ss:Worksheet", nsmgr))
            {
                DataTable dt = new DataTable(node.Attributes["ss:Name"].Value);
                ds.Tables.Add(dt);
                XmlNodeList rows = node.SelectNodes("ss:Table/ss:Row", nsmgr);
                if (rows.Count > 0)
                {

                    //*************************
                    //Add Columns To Table from header row
                    //*************************
                    List<ColumnType> columns = new List<ColumnType>();
                    int startIndex = 0;
                    if (hasHeaders)
                    {
                        foreach (XmlNode data in rows[0].SelectNodes("ss:Cell/ss:Data", nsmgr))
                        {
                            columns.Add(new ColumnType(typeof(string)));//default to text
                            dt.Columns.Add(data.InnerText, typeof(string));
                        }
                        startIndex++;
                    }
                    //*************************
                    //Update Data-Types of columns if Auto-Detecting
                    //*************************
                    if (autoDetectColumnType && rows.Count > 0)
                    {
                        XmlNodeList cells = rows[startIndex].SelectNodes("ss:Cell", nsmgr);
                        int actualCellIndex = 0;
                        for (int cellIndex = 0; cellIndex < cells.Count; cellIndex++)
                        {
                            XmlNode cell = cells[cellIndex];
                            if (cell.Attributes["ss:Index"] != null)
                                actualCellIndex =
                                  int.Parse(cell.Attributes["ss:Index"].Value) - 1;

                            ColumnType autoDetectType =
                              getType(cell.SelectSingleNode("ss:Data", nsmgr));

                            if (actualCellIndex >= dt.Columns.Count)
                            {
                                dt.Columns.Add("Column" +
                                  actualCellIndex.ToString(), autoDetectType.type);
                                columns.Add(autoDetectType);
                            }
                            else
                            {
                                dt.Columns[actualCellIndex].DataType = autoDetectType.type;
                                columns[actualCellIndex] = autoDetectType;
                            }

                            actualCellIndex++;
                        }
                    }
                    //*************************
                    //Load Data
                    //*************************
                    for (int i = startIndex; i < rows.Count; i++)
                    {
                        DataRow row = dt.NewRow();
                        XmlNodeList cells = rows[i].SelectNodes("ss:Cell", nsmgr);
                        int actualCellIndex = 0;
                        for (int cellIndex = 0; cellIndex < cells.Count; cellIndex++)
                        {
                            XmlNode cell = cells[cellIndex];
                            if (cell.Attributes["ss:Index"] != null)
                                actualCellIndex = int.Parse(cell.Attributes["ss:Index"].Value) - 1;

                            XmlNode data = cell.SelectSingleNode("ss:Data", nsmgr);

                            if (actualCellIndex >= dt.Columns.Count)
                            {
                                for (int ii = dt.Columns.Count; ii < actualCellIndex; ii++)
                                {
                                    dt.Columns.Add("Column" +
                                               actualCellIndex.ToString(), typeof(string));
                                    columns.Add(getDefaultType());
                                }
                                ColumnType autoDetectType =
                                   getType(cell.SelectSingleNode("ss:Data", nsmgr));
                                dt.Columns.Add("Column" + actualCellIndex.ToString(),
                                               typeof(string));
                                columns.Add(autoDetectType);
                            }
                            if (data != null)
                                row[actualCellIndex] = data.InnerText;

                            actualCellIndex++;
                        }

                        dt.Rows.Add(row);
                    }
                }
            }
            return ds;
        }
        
        struct ColumnType {
               public Type type;
               private string name;
               public ColumnType(Type type) { this.type = type; this.name = type.ToString().ToLower(); }
               public object ParseString(string input) {
                   if (String.IsNullOrEmpty(input))
                       return DBNull.Value;
                   switch (type.ToString()) {
                       case "system.datetime":
                           return DateTime.Parse(input);
                       case "system.decimal":
                           return decimal.Parse(input);
                       case "system.boolean":
                           return bool.Parse(input);
                       default:
                           return input;
                   }
               }
           }

        private static ColumnType getDefaultType() {
              return new ColumnType(typeof(String));
          }
  
        private static ColumnType getType(XmlNode data) {
              string type = null;
              if (data.Attributes["ss:Type"] == null || data.Attributes["ss:Type"].Value == null)
                  type = "";
              else
                  type = data.Attributes["ss:Type"].Value;
  
              switch (type) {
                  case "DateTime":
                      return new ColumnType(typeof(DateTime));
                  case "Boolean":
                      return new ColumnType(typeof(Boolean));
                  case "Number":
                      return new ColumnType(typeof(Decimal));
                  case "":
                      decimal test2;
                      if (data == null || String.IsNullOrEmpty(data.InnerText) || decimal.TryParse(data.InnerText, out test2)) {
                          return new ColumnType(typeof(Decimal));
                      } else {
                          return new ColumnType(typeof(String));
                      }
                  default://"String"
                      return new ColumnType(typeof(String));
              }
        }
    }

}
