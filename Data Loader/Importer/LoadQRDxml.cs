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
using System.Collections.Generic;
using System.Data;
using System.Data.OleDb;
using System.IO;
using System.Linq;
using System.Text;
using System.Xml;
using DataLoader.Common.Model;
 

namespace DataLoader
{
    class LoadQrdaXml
    {
        public static DataSet ImportQRDAXML(string filepath)
        {
            QrdaDataLocationList qrdaLocationList = new QrdaDataLocationList();
            qrdaLocationList = LoadQrdaLocations(); //load custom data locations from QRDA_Config_File.xml
            XmlDocument xmldoc = new XmlDocument();
            DataSet ds = new DataSet();
            //
            //**************************************************
            // Patient load...
            //**************************************************
            PntMapping pntmap = new PntMapping();
            string worksheet = "Patients$";
            DataTable dt = new DataTable(worksheet);
            ds.Tables.Add(dt);
            xmldoc.Load(filepath);
            XmlElement root = xmldoc.DocumentElement;
            XmlNamespaceManager nsmgr = new XmlNamespaceManager(xmldoc.NameTable);
            nsmgr.AddNamespace("demo", "urn:hl7-org:v3");
            string[,] QRDmap = pntmap.QrdaMapping();
            string xpath = "";
            string column = "";
            DataRow row = dt.NewRow();
            for (int i = 0; i <= QRDmap.GetUpperBound(0); i++)
            {
                column = QRDmap[i, 1];
                if (column.Contains(":"))
                    column = column.Substring(0,column.IndexOf(":"));
                dt.Columns.Add(column, typeof(string));
            }
            string parentnode = pntmap.QRDParentNode;
            for (int i = 0; i <= QRDmap.GetUpperBound(0); i++)
            {
                xpath = QRDmap[i, 0];
                column = QRDmap[i, 1];
                if (xpath == "")
                {
                    if (column == "Case name") row[i] = "CASE1";
                    if (column == "Insurance") row[i] = "Other";
                    continue;
                }
                
                int last = xpath.LastIndexOf("@");
                string attrib = "";
                if (last > 0)
                {
                    attrib = xpath.Substring((last+1), (xpath.Length-(last+1)));
                    xpath = xpath.Substring(0, (last-1));
                }
                string fullXpath = parentnode + xpath;
                fullXpath = fullXpath.Replace("/", "/demo:");
                XmlNodeList nodes = root.SelectNodes("//demo:" + fullXpath, nsmgr);
                for (int ii = 0; ii <nodes.Count; ii++)
                {
                    XmlNode content = nodes.Item(ii);
                    string value = content.InnerText;
                    if (attrib != "")
                    {
                        try
                        {
                            value = content.Attributes[attrib].Value;
                        }
                        catch
                        {
                            break;
                        }
                    }
                    if (xpath.Contains("birthTime"))
                    {
                        value = datetimeconvert(value); //convert DOB
                        last = value.LastIndexOf("@");
                        if (last > 0) value = value.Substring(0, last);
                    }
                    //
                    if (column == "Ethnicity" || column == "Race") value = pntmap.DemographMapping(column, value);
                    //
                    row[i] = value;
                }
            }
            dt.Rows.Add(row);
            //
            //***************************************************
            //Problems load...
            //***************************************************
            //
            StringItemXmlList xmlitemlist = new StringItemXmlList();
            worksheet = "Problems$";
            List<string> qrdmaplist = new List<string>();
            int filtercount = 0;
            bool headeron = true;
            NewWorkWorksheet(ds, worksheet, ref dt, ref row, ref xmlitemlist, ref qrdmaplist, ref filtercount, ref headeron);
            MapandFetch(qrdaLocationList, dt, root, nsmgr, ref row, ref parentnode, ref xmlitemlist, qrdmaplist, ref filtercount, "Problems", ref headeron);
            if (qrdmaplist.Any())
            {
                xmlitemlist.Clear();
                BuildHeaders(dt, qrdmaplist, ref xmlitemlist, ref filtercount, headeron);
                row = FetchDataRows(dt, root, nsmgr, row, parentnode, xmlitemlist, filtercount);
                qrdmaplist.Clear();
            }
            //
            //***************************************************
            // Encounter Load
            //***************************************************
            worksheet = "Encounters$";
            NewWorkWorksheet(ds, worksheet, ref dt, ref row, ref xmlitemlist, ref qrdmaplist, ref filtercount, ref headeron);
            MapandFetch(qrdaLocationList, dt, root, nsmgr, ref row, ref parentnode, ref xmlitemlist, qrdmaplist, ref filtercount, "Encounters", ref headeron);
            if (qrdmaplist.Any())
            {
                xmlitemlist.Clear();
                BuildHeaders(dt, qrdmaplist, ref xmlitemlist, ref filtercount, headeron);
                row = FetchDataRows(dt, root, nsmgr, row, parentnode, xmlitemlist, filtercount);
                qrdmaplist.Clear();
            }
            //
            //***************************************************
            // Visit HealthFactor Load
            //***************************************************
            worksheet = "HFactors$";
            NewWorkWorksheet(ds, worksheet, ref dt, ref row, ref xmlitemlist, ref qrdmaplist, ref filtercount, ref headeron);
            MapandFetch(qrdaLocationList, dt, root, nsmgr, ref row, ref parentnode, ref xmlitemlist, qrdmaplist, ref filtercount, "HealthFactor", ref headeron);
            if (qrdmaplist.Any())
            {
                xmlitemlist.Clear();
                BuildHeaders(dt, qrdmaplist, ref xmlitemlist, ref filtercount, headeron);
                row = FetchDataRows(dt, root, nsmgr, row, parentnode, xmlitemlist, filtercount);
                qrdmaplist.Clear();
            }
            //***************************************************
            // Outpatient Meds Load
            //***************************************************
            worksheet = "Meds$";
            NewWorkWorksheet(ds, worksheet, ref dt, ref row, ref xmlitemlist, ref qrdmaplist, ref filtercount, ref headeron);
            MapandFetch(qrdaLocationList, dt, root, nsmgr, ref row, ref parentnode, ref xmlitemlist, qrdmaplist, ref filtercount, "Meds", ref headeron);
            if (qrdmaplist.Any())
            {
                xmlitemlist.Clear();
                BuildHeaders(dt, qrdmaplist, ref xmlitemlist, ref filtercount, headeron);
                row = FetchDataRows(dt, root, nsmgr, row, parentnode, xmlitemlist, filtercount);
                qrdmaplist.Clear();
            }
            //***************************************************
            // Vitals Load
            //***************************************************
            worksheet = "Vitals$";
            NewWorkWorksheet(ds, worksheet, ref dt, ref row, ref xmlitemlist, ref qrdmaplist, ref filtercount, ref headeron);
            MapandFetch(qrdaLocationList, dt, root, nsmgr, ref row, ref parentnode, ref xmlitemlist, qrdmaplist, ref filtercount, "Vitals", ref headeron);
            if (qrdmaplist.Any())
            {
                xmlitemlist.Clear();
                BuildHeaders(dt, qrdmaplist, ref xmlitemlist, ref filtercount, headeron);
                row = FetchDataRows(dt, root, nsmgr, row, parentnode, xmlitemlist, filtercount);
                qrdmaplist.Clear();
            }
            //***************************************************
            // Labs Load
            //***************************************************
            worksheet = "Labs$";
            NewWorkWorksheet(ds, worksheet, ref dt, ref row, ref xmlitemlist, ref qrdmaplist, ref filtercount, ref headeron);
            MapandFetch(qrdaLocationList, dt, root, nsmgr, ref row, ref parentnode, ref xmlitemlist, qrdmaplist, ref filtercount, "Labs", ref headeron);
            if (qrdmaplist.Any())
            {
                xmlitemlist.Clear();
                BuildHeaders(dt, qrdmaplist, ref xmlitemlist, ref filtercount, headeron);
                row = FetchDataRows(dt, root, nsmgr, row, parentnode, xmlitemlist, filtercount);
                qrdmaplist.Clear();
            }
            //***************************************************
            // V CPT
            //***************************************************
            worksheet = "VCpt$";
            NewWorkWorksheet(ds, worksheet, ref dt, ref row, ref xmlitemlist, ref qrdmaplist, ref filtercount, ref headeron);
            MapandFetch(qrdaLocationList, dt, root, nsmgr, ref row, ref parentnode, ref xmlitemlist, qrdmaplist, ref filtercount, "VCpt", ref headeron);
            if (qrdmaplist.Any())
            {
                xmlitemlist.Clear();
                BuildHeaders(dt, qrdmaplist, ref xmlitemlist, ref filtercount, headeron);
                row = FetchDataRows(dt, root, nsmgr, row, parentnode, xmlitemlist, filtercount);
                qrdmaplist.Clear();
            }
            //
            //***************************************************
            // V EXAM
            //***************************************************
            worksheet = "VExam$";
            NewWorkWorksheet(ds, worksheet, ref dt, ref row, ref xmlitemlist, ref qrdmaplist, ref filtercount, ref headeron);
            MapandFetch(qrdaLocationList, dt, root, nsmgr, ref row, ref parentnode, ref xmlitemlist, qrdmaplist, ref filtercount, "VExam", ref headeron);
            if (qrdmaplist.Any())
            {
                xmlitemlist.Clear();
                BuildHeaders(dt, qrdmaplist, ref xmlitemlist, ref filtercount, headeron);
                row = FetchDataRows(dt, root, nsmgr, row, parentnode, xmlitemlist, filtercount);
                qrdmaplist.Clear();
            }
            //
            //***************************************************
            // V IMMUNIZATION
            //***************************************************
            worksheet = "VImmunization$";
            NewWorkWorksheet(ds, worksheet, ref dt, ref row, ref xmlitemlist, ref qrdmaplist, ref filtercount, ref headeron);
            MapandFetch(qrdaLocationList, dt, root, nsmgr, ref row, ref parentnode, ref xmlitemlist, qrdmaplist, ref filtercount, "VImmunization", ref headeron);
            if (qrdmaplist.Any())
            {
                xmlitemlist.Clear();
                BuildHeaders(dt, qrdmaplist, ref xmlitemlist, ref filtercount, headeron);
                row = FetchDataRows(dt, root, nsmgr, row, parentnode, xmlitemlist, filtercount);
                qrdmaplist.Clear();
            }
            //***************************************************
            // Radiology Orders
            //***************************************************
            worksheet = "RadOrders$";
            NewWorkWorksheet(ds, worksheet, ref dt, ref row, ref xmlitemlist, ref qrdmaplist, ref filtercount, ref headeron);
            MapandFetch(qrdaLocationList, dt, root, nsmgr, ref row, ref parentnode, ref xmlitemlist, qrdmaplist, ref filtercount, "RadOrders", ref headeron);
            if (qrdmaplist.Any())
            {
                xmlitemlist.Clear();
                BuildHeaders(dt, qrdmaplist, ref xmlitemlist, ref filtercount, headeron);
                row = FetchDataRows(dt, root, nsmgr, row, parentnode, xmlitemlist, filtercount);
                qrdmaplist.Clear();
            }
            //
            //***************************************************
            // V PATIENT ED
            //***************************************************
            worksheet = "VPatientEd$";
            NewWorkWorksheet(ds, worksheet, ref dt, ref row, ref xmlitemlist, ref qrdmaplist, ref filtercount, ref headeron);
            MapandFetch(qrdaLocationList, dt, root, nsmgr, ref row, ref parentnode, ref xmlitemlist, qrdmaplist, ref filtercount, "VPatientEd", ref headeron);
            if (qrdmaplist.Any())
            {
                xmlitemlist.Clear();
                BuildHeaders(dt, qrdmaplist, ref xmlitemlist, ref filtercount, headeron);
                row = FetchDataRows(dt, root, nsmgr, row, parentnode, xmlitemlist, filtercount);
                qrdmaplist.Clear();
            }
            //
            //***************************************************
            // Consults
            //***************************************************
            worksheet = "Consults$";
            NewWorkWorksheet(ds, worksheet, ref dt, ref row, ref xmlitemlist, ref qrdmaplist, ref filtercount, ref headeron);
            MapandFetch(qrdaLocationList, dt, root, nsmgr, ref row, ref parentnode, ref xmlitemlist, qrdmaplist, ref filtercount, "Consults", ref headeron);
            if (qrdmaplist.Any())
            {
                xmlitemlist.Clear();
                BuildHeaders(dt, qrdmaplist, ref xmlitemlist, ref filtercount, headeron);
                row = FetchDataRows(dt, root, nsmgr, row, parentnode, xmlitemlist, filtercount);
                qrdmaplist.Clear();
            }
            //
            qrdmaplist.Clear();
            xmlitemlist.Clear();
            //
            return ds;
        }

        private static void MapandFetch(QrdaDataLocationList qrdaLocationList, DataTable dt, XmlElement root, XmlNamespaceManager nsmgr, ref DataRow row, ref string parentnode, ref StringItemXmlList xmlitemlist, List<string> qrdmaplist, ref int filtercount, string worksheet, ref bool headeron)
        {
            parentnode = "";
            string maprecord = "";
            string mapstring = "";
            foreach (QrdaDataLocation dataLocation in qrdaLocationList)
            {
                if (dataLocation.worksheet != worksheet) continue;
                if ((maprecord != "" && dataLocation.record != maprecord) || (parentnode != "" && dataLocation.parentnode != parentnode))
                {
                    xmlitemlist.Clear();
                    BuildHeaders(dt, qrdmaplist, ref xmlitemlist, ref filtercount, headeron);
                    row = FetchDataRows(dt, root, nsmgr, row, parentnode, xmlitemlist, filtercount);
                    qrdmaplist.Clear();
                    filtercount = 0;
                    headeron = false;
                }
                parentnode = dataLocation.parentnode;
                maprecord = dataLocation.record;
                mapstring = dataLocation.xpath + "^" + dataLocation.type.Substring(0, 1) + "^" + dataLocation.column + "^" + dataLocation.match + "^";
                mapstring = mapstring + dataLocation.matchAttrib + "^" + dataLocation.matchVal + "^" + dataLocation.matchFunction;
                qrdmaplist.Add(mapstring);
            }
        }

        private static void NewWorkWorksheet(DataSet ds, string worksheet, ref DataTable dt, ref DataRow row, ref StringItemXmlList xmlitemlist, ref List<string> qrdmaplist, ref int filtercount, ref bool headeron)
        {
            dt = new DataTable(worksheet);
            ds.Tables.Add(dt);
            qrdmaplist.Clear();
            row = dt.NewRow();
            xmlitemlist.Clear();
            filtercount = 0;
            headeron = true;
        }

        private static void BuildHeaders(DataTable dt, List<string> QRDmap, ref StringItemXmlList xmlitemlist, ref int filtercount, bool headeron)
        {
            int columnCount = 1;
            // insert Case Name
            if (headeron == true) dt.Columns.Add("Case name", typeof(string));
            StringItemXml xmlitem = new StringItemXml();
            xmlitem.column = "Case name";
            xmlitem.id = "0";
            xmlitemlist.xmlitems.Add(xmlitem);
            ;
            // build mapped set
            foreach (String s in QRDmap)
            {
                string[] map = s.Split('^');
                xmlitem = new StringItemXml();
                string xpath = map[0]; //QRDmap[i, 0];
                string type = map[1];
                string column = map[2]; //QRDmap[i, 1];
                string match = map[3];
                string attrib = "";
                string matchattrib = map[4];
                string matchval = map[5];
                string matchfunction = map[6];
                if (type == "D" || type == "S")
                {
                    if (headeron == true)
                    {
                        dt.Columns.Add(column, typeof(string));
                    }
                }
                else
                {
                    filtercount++; //check number of filterchecks per parent node
                }
                if (xpath.Contains("@"))
                {
                    int last = xpath.LastIndexOf("@");
                    attrib = xpath.Substring((last + 1), (xpath.Length - (last + 1)));
                    xpath = xpath.Substring(0, (last - 1));
                }
                if (type == "D" || type == "S")
                {
                    xmlitem.id = columnCount.ToString();
                    columnCount++;
                }
                xmlitem.column = column; // data grid column to map to
                xmlitem.xpath = xpath; // xpath where to find data
                xmlitem.match = match; // match value for evaluations
                xmlitem.attrib = attrib; // attribute where data is (sometimes) stored
                xmlitem.matchval = matchval; // value (sometimes) stuffed in column if match is made
                xmlitem.matchattrib = matchattrib; // look for match in seperate attrib
                xmlitem.matchfunction = matchfunction; //designates location of match function
                xmlitem.type = type;
                xmlitemlist.xmlitems.Add(xmlitem);
            }

        }

        private static DataRow FetchDataRows(DataTable dt, XmlElement root, XmlNamespaceManager nsmgr, DataRow row, string parentnode, StringItemXmlList xmlitemlist, int filtercount)
        {
            string fullxpath = "//demo:" + parentnode;
            XmlNodeList nodelist = root.SelectNodes(fullxpath, nsmgr);
            string holdValue = "";
            foreach (XmlNode node in nodelist)
            {
                if (node.HasChildNodes)
                {
                    row = dt.NewRow();
                    row[0] = "CASE1";
                    int quit = 0;
                    for (int i = 0; i < node.ChildNodes.Count; i++)
                    {
                        int irow;
                        XmlNode childnode = node.ChildNodes[i];
                        string element = childnode.Name;
                        // for every childnode, check for all node matches
                        foreach (StringItemXml sItem in xmlitemlist)
                        {
                            irow = Convert.ToInt32(sItem.id);
                            if (sItem.type == "S")
                            {
                                row[irow] = sItem.matchval;
                                continue;
                            }
                            if (sItem.xpath == null) continue;
                            if (sItem.xpath.Contains(element))
                            {
                                XmlNode chcknode = CheckNodes(childnode, sItem);
                                if (chcknode == null) continue;
                                string value = chcknode.InnerText;
                                if (sItem.match != "")
                                {
                                    // look for match value
                                    if (sItem.attrib != "")
                                    {
                                        // check against attrib value
                                        try
                                        {
                                            value = chcknode.Attributes[sItem.attrib].Value;
                                        }
                                        catch
                                        {
                                            value = null;
                                        }
                                    }
                                    if (value == sItem.match)
                                    {
                                        if (sItem.matchval != "")
                                        {
                                            value = sItem.matchval;
                                            value = ReplaceAmp(value);
                                        }
                                        if (sItem.id == null)
                                        {
                                            quit++;
                                            sItem.filter = true;
                                        }
                                    }
                                }
                                else
                                {
                                    if (sItem.attrib != "")
                                    {
                                        // check against attrib value
                                        try
                                        {
                                            value = chcknode.Attributes[sItem.attrib].Value;
                                        }
                                        catch
                                        {
                                            value = null;
                                        }
                                    }
                                }
                                if (sItem.matchfunction != "")
                                {
                                value = matchFunction(sItem, chcknode, ref holdValue, ref dt, row);
                                    if (value == "") value = null;
                                    if (value == "[QUIT]")
                                    {
                                        value = null;
                                        quit--;
                                    }
                                    if (value != null) value = ReplaceAmp(value);
                                }
                                if (sItem.id != null && value != null)
                                {
                                    if (sItem.xpath.Contains("effectiveTime")) value = datetimeconvert(value);
                                    if (sItem.xpath.Contains("author/time")) value = datetimeconvert(value);
                                    if (sItem.column == "Problem onset date" || sItem.column == "Entry date" || sItem.column == "Resolved date") value = value.Substring(0, 10);
                                    row[irow] = value;
                                }
                            }
                        }
                        //
                    }
                    if (quit < filtercount) continue;
                    holdValue = ""; //clear out hold values
                    dt.Rows.Add(row);
                }
            }
            return row;
        }

        private static string ReplaceAmp(string value)
        {
            int linepos;
            linepos = value.IndexOf("(AND)");
            if (linepos > 0)
            {
                value = value.Substring(0, linepos) + "&" + value.Substring(linepos + 5);
            }
            return value;
        }

        private static string datetimeconvert(string datetime)
        {
            string convertedvalue = "";
            string year = datetime.Substring(0, 4);
            string month = datetime.Substring(4, 2);
            string day = datetime.Substring(6, 2);
            string time = "";
            if (datetime.Length > 8) time = datetime.Substring(8, 4);
            convertedvalue = month + "/" + day + "/" + year;
            if (time.Length > 0) convertedvalue = convertedvalue + "@" + time;
            return convertedvalue;
        }

        private static string matchFunction(StringItemXml sItem, XmlNode node, ref string holdValue, ref DataTable dt, DataRow row)
        {
            string returnval = "";
            string function = sItem.matchfunction;
            string codesystem = "";
            string code = "";
            //
            if (function == "EncounterType" || function == "Location" || function == "Room-Bed" || function == "Provider" || function == "AdmittingRegulation" || function == "FacilityTreatingSpecialty")
            {
                try
                {
                    codesystem = node.Attributes["codeSystem"].Value; //codeSystem
                    code = node.Attributes["code"].Value;
                }
                catch { return returnval; }
                returnval = CodeTranslateI("Encounters", "codesystem", codesystem, code);
                if (function != "EncounterType" && returnval !="")
                {
                    if (function == "Location")
                    {
                        returnval = CodeTranslateI("Encounters", "location", "location", returnval);
                    }
                    if (function == "Room-Bed")
                    {
                        returnval = CodeTranslateI("Encounters", "roombed", "roombed", returnval);
                    }
                    if (function == "Provider")
                    {
                        returnval = CodeTranslateI("Encounters", "provider", "provider", returnval);
                    }
                    if (function == "AdmittingRegulation")
                    {
                        returnval = CodeTranslateI("Encounters", "admittingregulation", "admittingregulation", returnval);
                    }
                    if (function == "FacilityTreatingSpecialty")
                    {
                        returnval = CodeTranslateI("Encounters", "facilitytreatingspecialty", "facilitytreatingspecialty", returnval);
                    }
                    //
                }
                return returnval;
            }
            if (function == "DischargeType")
            {
                try
                {
                    codesystem = node.Attributes["codeSystem"].Value; //codeSystem
                    code = node.Attributes["code"].Value;
                }
                catch { return returnval; }
                returnval = CodeTranslateI("Encounters", "dischargetype", codesystem, code);
                if (returnval == "") returnval = "REGULAR";
                return returnval;
            }
            if (function == "HealthFactor")
            {
                try
                {
                    codesystem = node.Attributes["codeSystem"].Value; //codeSystem
                    code = node.Attributes["code"].Value;
                }
                catch { return returnval; }
                returnval = CodeTranslateI("HealthFactor", "codesystem", codesystem, code);
                if (returnval == "") returnval = "[QUIT]";
            }
            if (function == "Vitals")
            {
                string column = sItem.column;
                if (column == "Vital type")
                {
                    try
                    {
                        codesystem = node.Attributes["codeSystem"].Value; //codeSystem
                        code = node.Attributes["code"].Value;
                    }
                    catch { return returnval; }
                    
                    if (codesystem == "2.16.840.1.113883.6.1")
                    {
                        switch (code)
                        {
                            case "8462-4": // Diastolic
                                if (holdValue.Contains("SY"))
                                {
                                    returnval = "BLOOD PRESSURE";
                                    holdValue = holdValue + "^DI";
                                }
                                else
                                {
                                    holdValue = "^DI";
                                    returnval = "[QUIT]";
                                }
                                break;
                            case "8480-6": // Systolic
                                if (holdValue.Contains("DI"))
                                {
                                    returnval = "BLOOD PRESSURE";
                                    holdValue = holdValue + "^SY";
                                }
                                else
                                {
                                    returnval = "[QUIT]";
                                    holdValue = "^SY";
                                }
                                break;
                            case "39156-5": //BMI
                                returnval = "BMI";
                                holdValue = "BMI";
                                break;
                            default:
                                returnval = "[QUIT]";
                                break;
                        }
                    }
                }
               if (column == "Rate 2") // diastolic
               {
                   if (holdValue.Length > 0)
                   {
                     int len = holdValue.Length;
                     string stringCheck = holdValue.Substring((len-2),2);
                     if (stringCheck != "DI")
                     {
                         if (holdValue.Contains("DI^"))
                         {
                             try
                             {
                                 string chk = node.Attributes["value"].Value; //make sure context is correct
                                 int dipos = holdValue.LastIndexOf("DI^") + 3;
                                 int sypos = holdValue.LastIndexOf("^SY");
                                 stringCheck = holdValue.Substring(dipos, (sypos-dipos));
                                 returnval = stringCheck;
                                 return returnval;
                             }
                             catch
                             {
                                 return returnval;
                             }
                         }
                     }
                     if (stringCheck == "DI")
                     {
   
                             try
                             {
                                 returnval = node.Attributes["value"].Value;
                                 holdValue = holdValue + "^" + returnval;
                                 return returnval;
                             }
                            catch
                            {
                                return returnval;
                            }
                     }
                   }
               }

               if (column == "Rate 1") //systolic
               {
                   if (holdValue.Length > 0)
                   {
                       if (holdValue == "BMI")
                       {
                           Random Rand = new Random();
                           double Weight = Math.Round(99.2 + Rand.NextDouble() * (286.6 - 99.2),1);
                           double BMI = Convert.ToDouble(node.Attributes["value"].Value);
                           double Height = Math.Round(Math.Sqrt((Weight * 703) / BMI),1);
                           DataRow WeightRow = dt.NewRow();
                           DataRow HeightRow = dt.NewRow();
                           WeightRow[0] = HeightRow[0] = row[0];
                           WeightRow[1] = "WEIGHT";
                           HeightRow[1] = "HEIGHT";
                           WeightRow[2] = Weight;
                           HeightRow[2] = Height;
                           WeightRow[4] = HeightRow[4] = row[4];
                           WeightRow[5] = HeightRow[5] = row[5];
                           WeightRow[6] = HeightRow[6] = row[6];
                           WeightRow[7] = HeightRow[7] = row[7];
                           dt.Rows.Add(WeightRow);
                           dt.Rows.Add(HeightRow);
                           dt.AcceptChanges();
                           holdValue = ""; 
                           returnval = "[QUIT]";
                           return returnval;
                       }
                       int len = holdValue.Length;
                       string stringCheck = holdValue.Substring((len - 2), 2);
                       if (stringCheck != "SY")
                       {
                           if (holdValue.Contains("SY^"))
                           {
                               try
                               {
                                   string chk = node.Attributes["value"].Value; //make sure context is correct
                                   int sypos = holdValue.LastIndexOf("SY^")+3;
                                   int dipos = holdValue.LastIndexOf("^DI");
                                   stringCheck = holdValue.Substring(sypos, (dipos-sypos));
                                   returnval = stringCheck;
                                   return returnval;
                               }
                               catch
                               {
                                   return returnval;
                               }
                           }
                           return returnval;
                       }
                       if (stringCheck == "SY")
                       {
                             try
                               {
                                   returnval = node.Attributes["value"].Value;
                                   holdValue = holdValue + "^" + returnval;
                                   return returnval;
                               }
                               catch
                               {
                                   return returnval;
                               }
                       }
                       }
                      
                   }
            }
            //
            if (function == "Problem" || function == "Lab Test" || function == "Medication" || function == "Procedure" || function == "Exam" || function == "Immunization" || function == "EdTopic" || function == "Consult")
            {
                try
                {
                    codesystem = node.Attributes["codeSystem"].Value; //codeSystem
                    code = node.Attributes["code"].Value;
                }
                catch { return returnval; }
                string strWorksheet = "";
                switch (function)
                {
                    case "Problem":
                        strWorksheet = "Problems";
                        break;
                    case "Lab Test":
                        strWorksheet = "Labs";
                        break;
                    case "Medication":
                        strWorksheet = "Meds";
                        break;
                    case "Procedure":
                        strWorksheet =  "VCpt";
                        break;
                    case "Immunization":
                        strWorksheet = "VImmunization";
                        break;
                    case "EdTopic":
                        strWorksheet = "VPatientEd";
                        break;
                    case "Consult":
                        strWorksheet = "Consults";
                        break;
                    case "Exam":
                        strWorksheet = "VExam";
                        break;
                    default:
                        strWorksheet = "";
                        break;
                }

                if (strWorksheet != "") returnval = CodeTranslateI(strWorksheet, "codesystem", codesystem, code);

                if ((function == "Problem" || function == "Medication") && returnval != "") returnval = code;
                if ((function == "Immunization" || function == "Medication" || function == "Procedure" || function == "Problem" || function == "Exam" || function == "EdTopic" || function == "Consult") && returnval == "") returnval = "[QUIT]";
                return returnval;
            }
            //
            if (function == "RadProc")
            {
                try
                {
                    codesystem = node.Attributes["codeSystem"].Value; //codeSystem
                    code = node.Attributes["code"].Value;
                }
                catch { return returnval; }
                returnval = CodeTranslateI("RadOrders", "codesystem", codesystem, code);
                if (sItem.column == "Rad Procedure")
                {
                    if (sItem.column == "Rad Procedure" && returnval == "") returnval = "[QUIT]";
                    return returnval;
                }
                if (sItem.column == "Imaging Location")
                {
                    if (returnval == "Bone Scan")
                    {
                        returnval = "TENZING PET"; //nuc med location
                    }
                    else
                    {
                        returnval = "TENZING MAIN XRAY"; //general rad location
                    }
                    return returnval;
                }


            }
            return returnval;
        }

        private static String CodeTranslateI(string strWorksheet, string strType, string strID, string strInputValue)
        {
            string result = "";
            QrdaMapList qrdamaplist = new QrdaMapList();
            qrdamaplist = LoadQrdaMapping(strWorksheet, strType, strID); //load custom mapping translations from QRDA_Config_File.xml
            foreach (QrdaMap qrdmap in qrdamaplist)
            {
                if (qrdmap.inputvalue == strInputValue)
                {
                    result = qrdmap.vistavalue;
                }
            }
            return result;
        }

        private static String CodeTranslate(string package, string codesystem, string code)
        {
            string result = "";
            if (package == "Problem")
            {
                ProblemMapping probmap = new ProblemMapping();
                result = probmap.GetProblemType(codesystem, code);
                //
                if (codesystem == "2.16.840.1.113883.6.1")
                {
                    result = "LOINC:" + code;
                }
                if (codesystem == "2.16.840.1.113883.6.90")
                {
                    result = "ICD10:" + code;
                }
            }
            if (package == "HealthFactor")
            {
                HealthFactorMapping hfactormap = new HealthFactorMapping();
                result = hfactormap.GetHealthFactor(codesystem, code);
            }
            if (package == "EncounterType")
            {
                EncounterMapping encountermap = new EncounterMapping();
                result = encountermap.GetEncounterType(codesystem, code);
            }
            if (package == "Location" || package == "Room-Bed" || package == "Provider" || package == "AdmittingRegulation")
            {
                EncounterMapping encountermap = new EncounterMapping();
                result = encountermap.GetEncounterOther(codesystem, code);
            }
            if (package == "Lab Test")
            {
                LabMapping labmap = new LabMapping();
                result = labmap.GetLabTest(codesystem, code);
            }
            if (package == "Medication")
            {
                MedMapping medmap = new MedMapping();
                result = medmap.GetRXName(codesystem, code);
            }
            if (package == "Procedure")
            {
                VCptMapping VCptmap = new VCptMapping();
                result = VCptmap.GetVCptType(codesystem, code);
            }
            if (package == "Exam")
            {
                VExamMapping VExammap = new VExamMapping();
                result = VExammap.GetVExamType(codesystem, code);
            }
            if (package == "Immunization")
            {
                VImmunizationMapping VImmunizationmap = new VImmunizationMapping();
                result = VImmunizationmap.GetVImmunizationType(codesystem, code);
            }
            if (package == "RadProc")
            {
                RadMapping radmap = new RadMapping();
                result = radmap.GetRadType(codesystem, code);
            }
            if (package == "EdTopic")
            {
                VPntEducationMapping edmap = new VPntEducationMapping();
                result = edmap.GetEdTopicType(codesystem, code);
            }
            if (package == "Consult")
            {
                ConsultMapping conmap = new ConsultMapping();
                result = conmap.GetConsultType(codesystem, code);
            }
            //
            return result;
        }

        private static XmlNode CheckNodes(XmlNode node, StringItemXml sItem)
        {
            string chkElement = sItem.xpath;
            int loc = chkElement.LastIndexOf("/");
            if (loc > 0) chkElement = chkElement.Substring((loc + 1), (chkElement.Length - (loc + 1)));
            if (node.Name == chkElement)
            {
                if (sItem.match != "")
                {
                    bool check = EvalNode(node, sItem);
                    if (check == false) node = null;
                }
                return node;
            }
            else
            {
                if (node.HasChildNodes == true)
                {
                    XmlNode chcknode = EvalNodeList(node.ChildNodes, sItem, chkElement);
                    if (chcknode == null)
                    {
                        foreach (XmlNode child in node.ChildNodes)
                        {
                            XmlNode xnode = CheckNodes(child, sItem);
                            if (xnode != null) return xnode;
                        }
                    }
                    else
                    {
                        node = chcknode;
                    }
                }
                else
                {
                    node = null;
                }
            }
            return node;
        }

        private static XmlNode EvalNodeList(XmlNodeList nodelist, StringItemXml sItem, string chkElement)
        {
            XmlNode node = null;
            foreach (XmlNode chknode in nodelist)
            {
                if (chknode.Name != chkElement) continue;
                if (sItem.match != "")
                {
                    bool check = EvalNode(chknode, sItem);
                    if (check == false) continue;
                }
                node = chknode;
                break;
            }
            return node;
        }

        private static bool EvalNode(XmlNode node, StringItemXml sItem)
        {
            bool result = false;
            string value = node.OuterXml;
            if (sItem.match != "")
            {
                // look for match value
                if (sItem.attrib != "")
                {
                    // validate against 'main' attrib value
                    try
                    {
                        value = node.Attributes[sItem.attrib].Value;
                    }
                    catch
                    {
                        value = "";
                    }
                }
                if (sItem.matchattrib != "")
                {
                    // validate against 'match' attrib value
                    try
                    {
                        value = node.Attributes[sItem.matchattrib].Value;
                    }
                    catch
                    {
                        value = "";
                    }
                }
                if (value == sItem.match)
                {
                    result = true;
                }
            }
            return result;
        }

        

        struct ColumnType
        {
            public Type type;
            private string name;
            public ColumnType(Type type) { this.type = type; this.name = type.ToString().ToLower(); }
            public object ParseString(string input)
            {
                if (String.IsNullOrEmpty(input))
                    return DBNull.Value;
                switch (type.ToString())
                {
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

        private static ColumnType getDefaultType()
        {
            return new ColumnType(typeof(String));
        }


        private static List<string> worksheetArray = new List<string>
        {
            "Patients$",
            "Appts$",
            "Problems$",
            "Vitals$",
            "Allergies$",
            "Notes$",
            "Meds$",
            "Consults$",
            "RadOrders$"
        };

        private static QrdaMapList LoadQrdaMapping(string strWorksheet, string strType, string strID)
        {
            // top level entry point to populate qrdamaplist
            QrdaMapList maplist = new QrdaMapList();
            string filepath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ProgramFilesX86), "Vista\\Tools\\Data Loader\\QRDA_Config_File.xml");
            XmlDocument xmldoc = new XmlDocument();
            xmldoc.Load(filepath);
            XmlElement root = xmldoc.DocumentElement;
            XmlNamespaceManager nsmgr = new XmlNamespaceManager(xmldoc.NameTable);
            nsmgr.AddNamespace("qrdamap", "DataLoader-XMLMapping");
            XmlNodeList nodes = root.SelectNodes("//qrdamap:worksheet", nsmgr);
            //
            foreach (XmlNode node in nodes)
            {
                string checkworksheet = node.Attributes["value"].Value;
                if (checkworksheet != strWorksheet) continue;
                if (node.HasChildNodes)
                {
                    for (int i = 0; i < node.ChildNodes.Count; i++)
                    {
                        XmlNode childnode = node.ChildNodes[i];
                        string name = childnode.Name;
                        if (name != "mapping") continue;
                        if (childnode.HasChildNodes)
                        {
                            mapping(ref maplist, childnode, strWorksheet, strType, strID);
                        };

                    }
                }
            }
            //
            return maplist;
        }

        private static void mapping(ref QrdaMapList maplist, XmlNode node, string worksheet, string strType, string strID)
        {
            // called from LoadQrdaMapping
            if (node.HasChildNodes)
            {
                for (int i = 0; i < node.ChildNodes.Count; i++)
                {
                    XmlNode childnode = node.ChildNodes[i];
                    string strNodename = childnode.Name;
                    if (strNodename != "type") continue;
                    string strTypeActive = childnode.Attributes["active"].Value;
                    if (strTypeActive != "YES" && strTypeActive != "yes" && strTypeActive !="Yes") continue;
                    //
                    string strCheckType = childnode.Attributes["name"].Value;
                    if (strCheckType != strType) continue;
                    string strCheckId = childnode.Attributes["id"].Value;
                    if (strCheckId != strID) continue;
                    if (childnode.HasChildNodes)
                    {
                        Type(ref maplist, childnode, worksheet, strType, strID);
                    };
                }
            }
        }

        private static void Type(ref QrdaMapList maplist, XmlNode node, string worksheet, string strTypeName, string strTypeId)
        {
            // called from mapping
            if (node.HasChildNodes)
            {
                for (int i = 0; i < node.ChildNodes.Count; i++)
                {
                    XmlNode childnode = node.ChildNodes[i];
                    string name = childnode.Name;
                    if (name != "item") continue;
                    QrdaMap mapitem = new QrdaMap();
                    try
                    {
                        string strItemInputvalue = childnode.Attributes["inputvalue"].Value;
                        string strVistavalue = childnode.Attributes["vistavalue"].Value;
                        mapitem.inputvalue = strItemInputvalue;
                        mapitem.vistavalue = strVistavalue;
                        mapitem.worksheet = worksheet;
                        mapitem.type = strTypeName;
                        mapitem.id = strTypeId;
                        maplist.QrdaMaps.Add(mapitem);
                    }
                    catch { }
                }
            }
        }
        //
        private static QrdaDataLocationList LoadQrdaLocations()
        {
            string filepath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ProgramFilesX86), "Vista\\Tools\\Data Loader\\QRDA_Config_File.xml");
            XmlDocument xmldoc = new XmlDocument();
            xmldoc.Load(filepath);
            XmlElement root = xmldoc.DocumentElement;
            XmlNamespaceManager nsmgr = new XmlNamespaceManager(xmldoc.NameTable);
            QrdaDataLocationList datalocations= new QrdaDataLocationList();
            
            nsmgr.AddNamespace("qrdamap", "DataLoader-XMLMapping");
            //
            string fullXpath = "worksheet";
            XmlNodeList nodes = root.SelectNodes("//qrdamap:" + fullXpath, nsmgr);

            foreach (XmlNode node in nodes)
            {
                string worksheet = node.Attributes["value"].Value;
                if (node.HasChildNodes)
                {
                    for (int i = 0; i < node.ChildNodes.Count; i++)
                    {
                        XmlNode childnode = node.ChildNodes[i];
                        string name = childnode.Name;
                        if (name != "datalocation") continue;
                        if (childnode.HasChildNodes)
                        {
                            Datalocation(ref datalocations, childnode, worksheet);
                        };

                    }
                }
            }
            //
            return datalocations;
        }

        private static void Datalocation(ref QrdaDataLocationList datalocations, XmlNode node, string worksheet)
        {
            if (node.HasChildNodes)
            {
                for (int i = 0; i < node.ChildNodes.Count; i++)
                {
                    XmlNode childnode = node.ChildNodes[i];
                    string name = childnode.Name;
                    if (name != "parentnode") continue;
                    string parentnode = childnode.Attributes["name"].Value;
                    if (childnode.HasChildNodes)
                    {
                        ParentNode(ref datalocations, childnode, worksheet, parentnode);
                    };
                }
            }
        }

        private static void ParentNode(ref QrdaDataLocationList datalocations, XmlNode node, string worksheet, string parentnode)
        {
            if (node.HasChildNodes)
            {
                for (int i = 0; i < node.ChildNodes.Count; i++)
                {
                    XmlNode childnode = node.ChildNodes[i];
                    string name = childnode.Name;
                    if (name != "record") continue;
                    string record = i.ToString();
                    if (childnode.HasChildNodes)
                    {
                        RecordSet(ref datalocations, childnode, worksheet, parentnode, record);
                    };
                }
            }
        }

        private static void RecordSet(ref QrdaDataLocationList datalocations, XmlNode node, string worksheet, string parentnode, string record)
        {
            if (node.HasChildNodes)
            {
                    for (int i = 0; i < node.ChildNodes.Count; i++)
                    {
                        XmlNode childnode = node.ChildNodes[i];
                        string name = childnode.Name;
                        if (name != "item") continue;
                        if (childnode.HasChildNodes)
                        {
                            QrdaDataLocation qrdalocation = new QrdaDataLocation();
                            qrdalocation.worksheet = worksheet;
                            qrdalocation.parentnode = parentnode;
                            qrdalocation.record = record;
                            Items(ref qrdalocation, childnode);
                            datalocations.QrdaDataLocations.Add(qrdalocation);
                        };
                    }
            }
        }

        private static void Items(ref QrdaDataLocation qrdalocation, XmlNode node)
        {
            if (node.HasChildNodes)
            {
                for (int i = 0; i < node.ChildNodes.Count; i++)
                {
                    XmlNode childnode = node.ChildNodes[i];
                    string value = childnode.InnerText;
                    string name = childnode.Name;
                    if (name == "xpath") qrdalocation.xpath = value;
                    if (name == "type") qrdalocation.type = value;
                    if (name == "column") qrdalocation.column = value;
                    if (name == "match") qrdalocation.match = value;
                    if (name == "matchAttrib") qrdalocation.matchAttrib = value;
                    if (name == "matchVal") qrdalocation.matchVal = value;
                    if (name == "matchFunction") qrdalocation.matchFunction = value;
                }
            }
        }

    }
}
