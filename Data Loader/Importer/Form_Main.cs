using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

// VistA Data Loader 2.0
// Copyright (C) 2012 Johns Hopkins University
// 
// VistA Data Loader is provided by the Johns Hopkins University School of
// Nursing, and funded by the Department of Health and Human Services, Office
// of the National Coordinator for Health Information Technology under Award
// Number #1U24OC000013-01.
//
// Licensed under the Apache License, Version 2.0 (the "License");
// you may not use this file except in compliance with the License.
// You may obtain a copy of the License at
//
//    http://www.apache.org/licenses/LICENSE-2.0
//
// Unless required by applicable law or agreed to in writing, software
// distributed under the License is distributed on an "AS IS" BASIS,
// WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
// See the License for the specific language governing permissions and
// limitations under the License.
// 
//
// REVISION HISTORY
// ----------------
// V.1.0 JUNE 2012 made possible by JHU, School of Nursing (see above)
// V.2.0 UPDATE JUNE 2014 made possible by University of Michigan
// V.2.1 UPDATE NOV 2014 made possible by Oroville Hospital, to support QRDA.
// V.2.2 Incrimental update: bug fixes, etc.
// V.2.5 Continued incrimental updates, bug fixes (2015)
//
// DECLARATIONS
// -------------------------------
// This software package is NOT for use in any production or clinical setting.
// The software has not been designed, coded, or tested for use in any clinical
// or production setting.
// 
// This should be considered a work in progress.  If folks are interested in 
// collaborating on future versions of the utility set should please contact 
// Mike Stark (starklogic@gmail.com) or ISI GROUP, LLC, Bethesda, MD.
//

namespace DataLoader
{
    using DataLoader.Broker;
    using DataLoader.Common.Model;
    using System.Text.RegularExpressions;
    using System.IO;
    
    public partial class Form_Main : Form
    {
        public string port;
        public string server;
        public string filepath;
        public string logpath;
        StreamWriter log;
        
        DataSet data = new DataSet();
        Client client = new Client();

        public Form_Main()
        {
            InitializeComponent();

            logpath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), "DataLoader\\DataLoader.log");
            string inifile = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), "DataLoader\\DataLoader.ini");

            if (File.Exists(inifile) == false)
            {
                string folder = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), "DataLoader");
                if (!Directory.Exists(folder))
                {
                    Directory.CreateDirectory(folder);
                }
                IniFile ini = new IniFile();
                ini.IniWriteValue("port", "value", "9297");
                ini.IniWriteValue("server", "value", "localhost");
                port = ini.IniReadValue("port", "value");
                server = ini.IniReadValue("server", "value");
            }
            else
            {
                IniFile ini = new IniFile();
                port = ini.IniReadValue("port", "value");
                server = ini.IniReadValue("server", "value");
            }

        }

        public void logfile(string operation)
        {
            if (operation == "open")
            {
                if (!File.Exists(logpath))
                {
                    log = new StreamWriter(logpath);
                }
                else
                {
                    log = File.AppendText(logpath);
                }
            }
            if (operation == "close")
            {
                if (null != log)
                {
                    log.Close();
                    log.Dispose();
                    log = null;
                }
            }
            if (operation == "clear")
            {
                if (null != log)
                {
                    log.Dispose();
                    log = null;
                }
                if (File.Exists(logpath))
                {
                    File.Delete(logpath);
                }
                logfile("open");
                logfile("close");
            }
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void port_TextChanged(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
        }

        public Connection conn { get; set; }

        private void server_TextChanged(object sender, EventArgs e)
        {

        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void loginToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void disconnectToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            int intPort = Convert.ToInt32(port);
            client.Disconnect(server, intPort);
            setdisonnectdisplay();
        }

        private void setconnectdisplay()
        {
            disconnectToolStripMenuItem1.Enabled = true;
            connectToolStripMenuItem.Enabled = false;
            exportTablesToolStripMenuItem.Enabled = true;
            iCD9SearchToolStripMenuItem.Enabled = true;
            batchImportUtilityToolStripMenuItem.Enabled = true;
            conn_status.Text = "connected";
            conn_status.ForeColor = System.Drawing.Color.Green;
            conn_info.Text = server + ":" + port;
            if (data.Tables != null && data.Tables.Count > 0)
            {
                import.Enabled = true;
            }
        }

        private void setdisonnectdisplay()
        {
            connectToolStripMenuItem.Enabled = true;
            disconnectToolStripMenuItem1.Enabled = false;
            exportTablesToolStripMenuItem.Enabled = false;
            iCD9SearchToolStripMenuItem.Enabled = false;
            batchImportUtilityToolStripMenuItem.Enabled = false;
            conn_status.Text = "disconnected";
            conn_status.ForeColor = System.Drawing.Color.Red;
            conn_info.Text = null;
            import.Enabled = false;
        }


        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void connectToolStripMenuItem_Click(object sender, EventArgs e)
        {
            int intPort = -1;
            //
            IniFile ini = new IniFile();
            port = ini.IniReadValue("port", "value");
            server = ini.IniReadValue("server", "value");
            //
            try
            {
                intPort = Convert.ToInt32(port);
            }
            catch
            {
                //Console.WriteLine("Input string is not a sequence of digits.");
            }

            if (client.Connect(server, intPort) != null)
            {
                setconnectdisplay();
            }
        }

        private void excellToolStripMenuItem_Click(object sender, EventArgs e)
        {
            OpenFileDialog OFD = new OpenFileDialog();
            OFD.ShowDialog();
            filepath = OFD.FileName;
            sheetlistBox1.Items.Clear();
            if (filepath.EndsWith("xml"))
            {
                data = LoadQrdaXml.ImportQRDAXML(filepath);
                if (data.Tables != null && data.Tables.Count > 0)
                {
                    dataGridView1.DataSource = data.Tables[0];
                    sheetlistBox1.Enabled = true;
                    if (conn_status.Text == "connected")
                    {
                        import.Enabled = true;
                    }
                }
            }
            if (filepath.EndsWith("xls") || filepath.EndsWith("xlsx"))
            {
                if (filepath != null && (File.Exists(filepath)))
                {
                    data = ExcellImport.ImportExcelXLS(filepath, true);
                    if (data.Tables != null && data.Tables.Count > 0)
                    {
                        dataGridView1.DataSource = data.Tables[0];
                        sheetlistBox1.Enabled = true;
                        if (conn_status.Text == "connected")
                        {
                            import.Enabled = true;
                        }
                    }
                }
            }
            // JSON file support not implimented
            //if (filepath.EndsWith("json"))
            //{
            //    if (filepath != null && (File.Exists(filepath)))
            //    {
            //        data = LoadJSON.ImportJSON(filepath);
            //        if (data.Tables != null && data.Tables.Count > 0)
            //        {
            //            dataGridView1.DataSource = data.Tables[0];
            //            sheetlistBox1.Enabled = true;
            //            if (conn_status.Text == "connected")
            //            {
            //                import.Enabled = true;
            //            }
            //        }
            //    }
            //}
            //
            if (data.Tables != null && data.Tables.Count > 0)
            {
                foreach (DataTable table in data.Tables)
                {
                    if (table.ToString().Contains("$"))
                    {
                        if (table.ToString().Contains("Table"))
                        {
                            //skip
                        }
                        else
                        {
                            sheetlistBox1.Items.Add(table.ToString());
                        }
                    }

                }
                Worksheet worksheet = new Worksheet();
                int nIndex = data.Tables.IndexOf(worksheet.TableName("Patient"));
                if (nIndex > -1)
                {
                    DataTable t = data.Tables[nIndex];
                    dataGridView1.DataSource = t;
                    nIndex = sheetlistBox1.FindString("Patients$");
                    if (nIndex != -1)
                        sheetlistBox1.SetSelected(nIndex, true);
                }
            }
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void label1_Click_1(object sender, EventArgs e)
        {

        }

        private void label2_Click_1(object sender, EventArgs e)
        {

        }

        private void sheetlistBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            foreach (DataTable table in data.Tables)
            {
                if (table.ToString() == (string)sheetlistBox1.SelectedItem)
                    dataGridView1.DataSource = table;
            }
        }

        private void settingsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Form_Connect form2 = new Form_Connect();
            form2.Show();
        }

        private void import_Click(object sender, EventArgs e)
        {
            PatientList pntlist = new PatientList();
            Worksheet worksheet = new Worksheet();
            List<string> results = new List<string>();
            //bool check;
            int success=0,errors=0,confirm=0;
            string errtxt;
            logfile("open");

            //+++ Process patients +++
            int nIndex = data.Tables.IndexOf(worksheet.TableName("Patient"));
            DataTable table = data.Tables[nIndex];
            table.AcceptChanges(); //added
            TableParse tableparse = new TableParse();
            foreach (DataRow row in table.Rows)
            {
                Patient pnt = new Patient();
                List<string> patient = tableparse.ParsePatientRPCData(table, row);
                if (patient[0] == "-1") continue;
                dataGridView1.Columns[0].DisplayIndex = 0;
                Response response = client.CurrentConnection.Execute(new Request { MethodName = "ISI IMPORT PAT" }.AddParameter(patient));
                results = ResponseParser.ParseCreatePatientResponse(response.RawData, pnt);
                if (results[0] == "success")
                {
                    success++;
                    string logtxt = DateTime.Now.ToString() + "| Successfully created VistA PATIENT record. |Name:" + pnt.PatientName + " DFN:" + pnt.PatientDFN + " SSN:" + pnt.SSN + "\r\n";
                    log.Write(logtxt);
                    pntlist.Patients.Add(pnt);
                    row.Delete();
                }
                if (results[0] == "error")
                {
                    errors++;
                    errtxt = DateTime.Now.ToString() + "| Error when attempting to create VistA PATIENT record.  |" + results[1];
                    log.WriteLine(errtxt);
                }
            }
            table.AcceptChanges();

            //+++ Process Appointments +++
            nIndex = data.Tables.IndexOf(worksheet.TableName("Appointment"));
            if (nIndex > -1)
            {
                table = data.Tables[nIndex];
                table.AcceptChanges();
                foreach (DataRow row in table.Rows)
                {
                    List<string> lst = tableparse.ParseApptRPCData(table, row, pntlist);
                    if (lst[0] == "-1") continue;
                    Response response = client.CurrentConnection.Execute(new Request { MethodName = "ISI IMPORT APPT" }.AddParameter(lst));
                    results.Clear();
                    results = ResponseParser.ParseCreateApptResponse(response.RawData);
                    if (results != null)
                    {
                        if (results[0] == "success")
                        {
                            success++;
                            string logtxt = DateTime.Now.ToString() + "| Successfully created appointment. |";
                            foreach (string rec in lst)
                            {
                                logtxt = logtxt + " : " + rec;
                            }
                            logtxt = logtxt + "\r\n";
                            log.Write(logtxt);
                            row.Delete();
                        }
                        if (results[0] == "error")
                        {
                            errors++;
                            errtxt = DateTime.Now.ToString() + "| Error creating appointment. |" + results[1] + "|";
                            foreach (string rec in lst)
                            {
                                errtxt = errtxt + " : " + rec;
                            }
                            errtxt = errtxt + "\r\n";
                            log.Write(errtxt);
                        }
                        if (results[0] == "duplicate")
                        {
                            errors++;
                            errtxt = DateTime.Now.ToString() + "| Duplicate appointment.  Entry not created. |" + results[1] + "|";
                            foreach (string rec in lst)
                            {
                                errtxt = errtxt + " : " + rec;
                            }
                            errtxt = errtxt + "\r\n";
                            log.Write(errtxt);
                            row.Delete();
                        }
                    }
                }
                table.AcceptChanges();
            }
            //
            //+++ Insert Encounters +++
            //
            //+++ Process OP Encounters as Appointments & VISIT events +++
            nIndex = data.Tables.IndexOf(worksheet.TableName("Encounters"));
            if (nIndex > -1)
            {
                table = data.Tables[nIndex];
                table.AcceptChanges();
                foreach (DataRow row in table.Rows)
                {
                    List<string> lst = tableparse.ParseOPEncounterRPCData(table, row, pntlist);
                    if (lst[0] == "-1") continue;
                    Response response = client.CurrentConnection.Execute(new Request { MethodName = "ISI IMPORT APPT" }.AddParameter(lst));
                    results.Clear();
                    results = ResponseParser.ParseCreateApptResponse(response.RawData);
                    if (results != null)
                    {
                        if (results[0] == "success")
                        {
                            success++;
                            string logtxt = DateTime.Now.ToString() + "| Successfully created appointment. |";
                            foreach (string rec in lst)
                            {
                                logtxt = logtxt + " : " + rec;
                            }
                            logtxt = logtxt + "\r\n";
                            log.Write(logtxt);
                            row.Delete();
                        }
                        if (results[0] == "error")
                        {
                            errors++;
                            errtxt = DateTime.Now.ToString() + "| Error creating appointment. |" + results[1] + "|";
                            foreach (string rec in lst)
                            {
                                errtxt = errtxt + " : " + rec;
                            }
                            errtxt = errtxt + "\r\n";
                            log.Write(errtxt);
                        }
                        if (results[0] == "duplicate")
                        {
                            errors++;
                            errtxt = DateTime.Now.ToString() + "| Duplicate appointment.  No appointment created. |" + results[1] + "|";
                            foreach (string rec in lst)
                            {
                                errtxt = errtxt + " : " + rec;
                            }
                            errtxt = errtxt + "\r\n";
                            log.Write(errtxt);
                            row.Delete();
                        }
                    }
                }
                table.AcceptChanges();
            }

            //
            //+++ Process IP Encounters as Admissions +++
            nIndex = data.Tables.IndexOf(worksheet.TableName("Encounters"));
            if (nIndex > -1)
            {
                table = data.Tables[nIndex];
                table.AcceptChanges();
                foreach (DataRow row in table.Rows)
                {
                    List<string> lst = tableparse.ParseIPEncounterRPCData(table, row, pntlist);
                    if (lst[0] == "-1") continue;
                    Response response = client.CurrentConnection.Execute(new Request { MethodName = "ISI IMPORT ADMIT" }.AddParameter(lst));
                    results.Clear();
                    results = ResponseParser.ParseCreateApptResponse(response.RawData);
                    if (results != null)
                    {
                        if (results[0] == "success")
                        {
                            success++;
                            string logtxt = DateTime.Now.ToString() + "| Successfully created admission. |";
                            foreach (string rec in lst)
                            {
                                logtxt = logtxt + " : " + rec;
                            }
                            logtxt = logtxt + "\r\n";
                            log.Write(logtxt);
                            row.Delete();
                        }
                        if (results[0] == "duplicate")
                        {
                            errors++;
                            errtxt = DateTime.Now.ToString() + "| Duplicated admit.  No admission created. |" + results[1] + "|";
                            foreach (string rec in lst)
                            {
                                errtxt = errtxt + " : " + rec;
                            }
                            errtxt = errtxt + "\r\n";
                            log.Write(errtxt);
                            row.Delete();
                        }
                        if (results[0] == "error")
                        {
                            errors++;
                            errtxt = DateTime.Now.ToString() + "| Error creating admission. |" + results[1] + "|";
                            foreach (string rec in lst)
                            {
                                errtxt = errtxt + " : " + rec;
                            }
                            errtxt = errtxt + "\r\n";
                            log.Write(errtxt);
                        }
                    }
                }
                table.AcceptChanges();
            }
            
            //+++ Process Problems +++
            nIndex = data.Tables.IndexOf(worksheet.TableName("Problem"));
            if (nIndex > -1)
            {
                table = data.Tables[nIndex];
                table.AcceptChanges();
                foreach (DataRow row in table.Rows)
                {
                    List<string> lst = tableparse.ParseProbRPCData(table, row, pntlist);
                    if (lst[0] == "-1") continue;
                    Response response = client.CurrentConnection.Execute(new Request { MethodName = "ISI IMPORT PROB" }.AddParameter(lst));
                    results.Clear();
                    results = ResponseParser.ParseCreateProbResponse(response.RawData);
                    if (results != null)
                    {
                        if (results[0] == "success")
                        {
                            success++;
                            string logtxt = DateTime.Now.ToString() + "| Successfully created Problem entry |";
                            foreach (string rec in lst)
                            {
                                logtxt = logtxt + " : " + rec;
                            }
                            logtxt = logtxt + "\r\n";
                            log.Write(logtxt);
                            row.Delete();
                        }
                        if (results[0] == "error")
                        {
                            errors++;
                            errtxt = DateTime.Now.ToString() + "| Error creating Problem entry. |" + results[1] + "|";
                            foreach (string rec in lst)
                            {
                                errtxt = errtxt + " : " + rec;
                            }
                            errtxt = errtxt + "\r\n";
                            log.Write(errtxt);
                        }
                        if (results[0] == "duplicate")
                        {
                            errors++;
                            errtxt = DateTime.Now.ToString() + "| Duplicated found.  No Problem entry created. |" + results[1] + "|";
                            foreach (string rec in lst)
                            {
                                errtxt = errtxt + " : " + rec;
                            }
                            errtxt = errtxt + "\r\n";
                            log.Write(errtxt);
                            row.Delete();
                        }
                    }
                }
                table.AcceptChanges();
            }
            
            //+++ Process Vitals +++
            nIndex = data.Tables.IndexOf(worksheet.TableName("Vital"));
            if (nIndex > -1)
            {
                table = data.Tables[nIndex];
                table.AcceptChanges();
                foreach (DataRow row in table.Rows)
                {
                    List<string> lst = tableparse.ParseVitalRPCData(table, row, pntlist);
                    if (lst[0] == "-1") continue;
                    Response response = client.CurrentConnection.Execute(new Request { MethodName = "ISI IMPORT VITALS" }.AddParameter(lst));
                    results.Clear();
                    results = ResponseParser.ParseCreateVitalResponse(response.RawData);
                    if (results != null)
                    {
                        if (results[0] == "success")
                        {
                            success++;
                            string logtxt = DateTime.Now.ToString() + "| Successfully created vital entry. |";
                            foreach (string rec in lst)
                            {
                                logtxt = logtxt + " : " + rec;
                            }
                            logtxt = logtxt + "\r\n";
                            log.Write(logtxt);
                            row.Delete();
                        }
                        if (results[0] == "error")
                        {
                            errors++;
                            errtxt = DateTime.Now.ToString() + "| Error creating vital entry. |" + results[1] + "|";
                            foreach (string rec in lst)
                            {
                                errtxt = errtxt + " : " + rec;
                            }
                            errtxt = errtxt + "\r\n";
                            log.Write(errtxt);
                        }
                    }
                }
                table.AcceptChanges();
            }
            
            //+++ Process Allergies +++
            nIndex = data.Tables.IndexOf(worksheet.TableName("Allergy"));
            if (nIndex > -1)
            {
                table = data.Tables[nIndex];
                table.AcceptChanges();
                foreach (DataRow row in table.Rows)
                {
                    List<string> lst = tableparse.ParseAllergyRPCData(table, row, pntlist);
                    if (lst[0] == "-1") continue;
                    Response response = client.CurrentConnection.Execute(new Request { MethodName = "ISI IMPORT ALLERGY" }.AddParameter(lst));
                    results.Clear();
                    results = ResponseParser.ParseCreateAllergyResponse(response.RawData);
                    if (results != null)
                    {
                        if (results[0] == "success")
                        {
                            success++;
                            string logtxt = DateTime.Now.ToString() + "| Successfully created allergy entry. |";
                            foreach (string rec in lst)
                            {
                                logtxt = logtxt + " : " + rec;
                            }
                            logtxt = logtxt + "\r\n";
                            log.Write(logtxt);
                            row.Delete();
                        }
                        if (results[0] == "error")
                        {
                            errors++;
                            errtxt = DateTime.Now.ToString() + "| Error creating allergy entry. |" + results[1] + "|";
                            foreach (string rec in lst)
                            {
                                errtxt = errtxt + " : " + rec;
                            }
                            errtxt = errtxt + "\r\n";
                            log.Write(errtxt);
                        }
                    }
                }
                table.AcceptChanges();
            }
            
            //+++ Process Labs +++
            nIndex = data.Tables.IndexOf(worksheet.TableName("Lab"));
            if (nIndex > -1)
            {
                table = data.Tables[nIndex];
                table.AcceptChanges();
                foreach (DataRow row in table.Rows)
                {
                    List<string> lst = tableparse.ParseLabRPCData(table, row, pntlist);
                    if (lst[0] == "-1") continue;
                    Response response = new Response();
                    try
                    {
                        response = client.CurrentConnection.Execute(new Request { MethodName = "ISI IMPORT LAB" }.AddParameter(lst));
                    }
                    catch
                    {
                        response = null;
                    }
                    results.Clear();
                    results = ResponseParser.ParseCreateLabResponse(response.RawData);
                    if (results != null)
                    {
                        if (results[0] == "success")
                        {
                            success++;
                            string logtxt = DateTime.Now.ToString() + "| Successfully created lab entry. |";
                            foreach (string rec in lst)
                            {
                                logtxt = logtxt + " : " + rec;
                            }
                            logtxt = logtxt + "\r\n";
                            log.Write(logtxt);
                            row.Delete();
                        }
                        if (results[0] == "error")
                        {
                            errors++;
                            errtxt = DateTime.Now.ToString() + "| Error creating lab entry. |" + results[1] + "|";
                            foreach (string rec in lst)
                            {
                                errtxt = errtxt + " : " + rec;
                            }
                            errtxt = errtxt + "\r\n";
                            log.Write(errtxt);
                        }
                        if (results[0] == "confirm")
                        {
                            confirm++;
                            string logtxt = DateTime.Now.ToString() + "| Please confirm lab entry. |";
                            foreach (string rec in lst)
                            {
                                logtxt = logtxt + " : " + rec;
                            }
                            logtxt = logtxt + "\r\n";
                            log.Write(logtxt);
                            row.Delete();
                        }
                    }
                }
                table.AcceptChanges();
            }
            
            //+++ Process Progress Notes+++
            nIndex = data.Tables.IndexOf(worksheet.TableName("Note"));
            if (nIndex > -1)
            {
                table = data.Tables[nIndex];
                table.AcceptChanges();
                foreach (DataRow row in table.Rows)
                {
                    List<string> lst = tableparse.ParseNoteRPCData(table, row, pntlist);
                    if (lst[0] == "-1") continue;
                    Response response = client.CurrentConnection.Execute(new Request { MethodName = "ISI IMPORT NOTE" }.AddParameter(lst));
                    results.Clear();
                    results = ResponseParser.ParseCreateNoteResponse(response.RawData);
                    if (results != null)
                    {
                        if (results[0] == "success")
                        {
                            success++;
                            string logtxt = DateTime.Now.ToString() + "| Successfully created progress note. |";
                            foreach (string rec in lst)
                            {
                                logtxt = logtxt + " : " + rec;
                            }
                            logtxt = logtxt + "\r\n";
                            log.Write(logtxt);
                            row.Delete();
                        }
                        if (results[0] == "error")
                        {
                            errors++;
                            errtxt = DateTime.Now.ToString() + "| Error creating progress note. |" + results[1] + "|";
                            foreach (string rec in lst)
                            {
                                errtxt = errtxt + " : " + rec;
                            }
                            errtxt = errtxt + "\r\n";
                            log.Write(errtxt);
                        }
                    }
                }
                table.AcceptChanges();
            }
            
            //+++ Process Meds+++
            nIndex = data.Tables.IndexOf(worksheet.TableName("Med"));
            if (nIndex > -1)
            {
                table = data.Tables[nIndex];
                table.AcceptChanges();
                foreach (DataRow row in table.Rows)
                {
                    List<string> lst = tableparse.ParseMedRPCData(table, row, pntlist);
                    if (lst[0] == "-1") continue;
                    Response response = client.CurrentConnection.Execute(new Request { MethodName = "ISI IMPORT MED" }.AddParameter(lst));
                    results.Clear();
                    results = ResponseParser.ParseCreateMedResponse(response.RawData);
                    if (results != null)
                    {
                        if (results[0] == "success")
                        {
                            success++;
                            string logtxt = DateTime.Now.ToString() + "| Successfully created medication entry. |";
                            foreach (string rec in lst)
                            {
                                logtxt = logtxt + " : " + rec;
                            }
                            logtxt = logtxt + "\r\n";
                            log.Write(logtxt);

                        }
                        if (results[0] == "error")
                        {
                            errors++;
                            errtxt = DateTime.Now.ToString() + "| Error creating medication entry. |" + results[1] + "|";
                            foreach (string rec in lst)
                            {
                                errtxt = errtxt + " : " + rec;
                            }
                            errtxt = errtxt + "\r\n";
                            log.Write(errtxt);
                        }
                        if (results[0] == "duplicate")
                        {
                            errors++;
                            errtxt = DateTime.Now.ToString() + "| Duplicate medication entry.  Nothing created. |" + results[1] + "|";
                            foreach (string rec in lst)
                            {
                                errtxt = errtxt + " : " + rec;
                            }
                            errtxt = errtxt + "\r\n";
                            log.Write(errtxt);
                            row.Delete();
                        }
                    }
                }
                table.AcceptChanges();
            }

            //+++ Process NonVAMeds+++
            nIndex = data.Tables.IndexOf(worksheet.TableName("NonVAMed"));
            if (nIndex > -1)
            {
                table.AcceptChanges();
                table = data.Tables[nIndex];
                foreach (DataRow row in table.Rows)
                {
                    List<string> lst = tableparse.ParseNonVAMedsRPCData(table, row, pntlist);
                    if (lst[0] == "-1") continue;
                    Response response = client.CurrentConnection.Execute(new Request { MethodName = "ISI IMPORT NONVA MED" }.AddParameter(lst));
                    results.Clear();
                    results = ResponseParser.ParseCreateNonVAMedResponse(response.RawData);
                    if (results != null)
                    {
                        if (results[0] == "success")
                        {
                            success++;
                            string logtxt = DateTime.Now.ToString() + "| Successfully created non-va medication order entry. |";
                            foreach (string rec in lst)
                            {
                                logtxt = logtxt + " : " + rec;
                            }
                            logtxt = logtxt + "\r\n";
                            log.Write(logtxt);
                            row.Delete();
                        }
                        if (results[0] == "error")
                        {
                            errors++;
                            errtxt = DateTime.Now.ToString() + "| Error creating non-va medication order entry. |" + results[1] + "|";
                            foreach (string rec in lst)
                            {
                                errtxt = errtxt + " : " + rec;
                            }
                            errtxt = errtxt + "\r\n";
                            log.Write(errtxt);
                        }
                    }
                }
                table.AcceptChanges();
            }

            //+++ Process Consults+++
            nIndex = data.Tables.IndexOf(worksheet.TableName("Consult"));
            if (nIndex > -1)
            {
                table = data.Tables[nIndex];
                table.AcceptChanges();
                foreach (DataRow row in table.Rows)
                {
                    List<string> lst = tableparse.ParseConsultRPCData(table, row, pntlist);
                    if (lst[0] == "-1") continue;
                    Response response = client.CurrentConnection.Execute(new Request { MethodName = "ISI IMPORT CONSULT" }.AddParameter(lst));
                    results.Clear();
                    results = ResponseParser.ParseCreateConsultResponse(response.RawData);
                    if (results != null)
                    {
                        if (results[0] == "success")
                        {
                            success++;
                            string logtxt = DateTime.Now.ToString() + "| Successfully created consult entry. |";
                            foreach (string rec in lst)
                            {
                                logtxt = logtxt + " : " + rec;
                            }
                            logtxt = logtxt + "\r\n";
                            log.Write(logtxt);
                            row.Delete();
                        }
                        if (results[0] == "error")
                        {
                            errors++;
                            errtxt = DateTime.Now.ToString() + "| Error creating consult entry. |" + results[1] + "|";
                            foreach (string rec in lst)
                            {
                                errtxt = errtxt + " : " + rec;
                            }
                            errtxt = errtxt + "\r\n";
                            log.Write(errtxt);
                        }
                    }
                }
                table.AcceptChanges();
            }

            //+++ Process Rad Orders+++
            nIndex = data.Tables.IndexOf(worksheet.TableName("Rad"));
            if (nIndex > -1)
            {
                table = data.Tables[nIndex];
                table.AcceptChanges();
                foreach (DataRow row in table.Rows)
                {
                    List<string> lst = tableparse.ParseRadRPCData(table, row, pntlist);
                    if (lst[0] == "-1") continue;
                    Response response = client.CurrentConnection.Execute(new Request { MethodName = "ISI IMPORT RAD ORDER" }.AddParameter(lst));
                    results.Clear();
                    results = ResponseParser.ParseCreateRadResponse(response.RawData);
                    if (results != null)
                    {
                        if (results[0] == "success")
                        {
                            success++;
                            string logtxt = DateTime.Now.ToString() + "| Successfully created Rad Order entry. |";
                            foreach (string rec in lst)
                            {
                                logtxt = logtxt + " : " + rec;
                            }
                            logtxt = logtxt + "\r\n";
                            log.Write(logtxt);
                            row.Delete();
                        }
                        if (results[0] == "error")
                        {
                            errors++;
                            errtxt = DateTime.Now.ToString() + "| Error creating Rad Order entry. |" + results[1] + "|";
                            foreach (string rec in lst)
                            {
                                errtxt = errtxt + " : " + rec;
                            }
                            errtxt = errtxt + "\r\n";
                            log.Write(errtxt);
                        }
                    }
                }
                table.AcceptChanges();
            }
            //
            //+++ Process V Health Factors +++
            nIndex = data.Tables.IndexOf(worksheet.TableName("HFactors"));
            if (nIndex > -1)
            {
                table = data.Tables[nIndex];
                table.AcceptChanges();
                foreach (DataRow row in table.Rows)
                {
                    List<string> lst = tableparse.ParseGenericRPCData(table, row, pntlist,"HFactors");
                    if (lst[0] == "-1") continue;
                    Response response = client.CurrentConnection.Execute(new Request { MethodName = "ISI IMPORT HFACTOR" }.AddParameter(lst));
                    results.Clear();
                    results = ResponseParser.ParseGenericResponse(response.RawData);
                    ResultLog("H Factor", results, ref success, ref errors, row, lst);
                 }
                table.AcceptChanges();
            }
            //
            //+++ Process V IMMUNIZATIONS +++
            nIndex = data.Tables.IndexOf(worksheet.TableName("VImmunization"));
            if (nIndex > -1)
            {
                table = data.Tables[nIndex];
                table.AcceptChanges();
                foreach (DataRow row in table.Rows)
                {
                    List<string> lst = tableparse.ParseGenericRPCData(table, row, pntlist,"VImmunization");
                    if (lst[0] == "-1") continue;
                    Response response = client.CurrentConnection.Execute(new Request { MethodName = "ISI IMPORT IMMUNIZATIONS" }.AddParameter(lst));
                    results.Clear();
                    results = ResponseParser.ParseGenericResponse(response.RawData);
                    ResultLog("V IMMUNIZATION", results, ref success, ref errors, row, lst);
                }
                table.AcceptChanges();
            }

            //+++ Process V CPT +++
            nIndex = data.Tables.IndexOf(worksheet.TableName("VCpt"));
            if (nIndex > -1)
            {
                table = data.Tables[nIndex];
                table.AcceptChanges();
                foreach (DataRow row in table.Rows)
                {
                    List<string> lst = tableparse.ParseGenericRPCData(table, row, pntlist,"VCpt");
                    if (lst[0] == "-1") continue;
                    Response response = client.CurrentConnection.Execute(new Request { MethodName = "ISI IMPORT V CPT" }.AddParameter(lst));
                    results.Clear();
                    results = ResponseParser.ParseGenericResponse(response.RawData);
                    ResultLog("V CPT", results, ref success, ref errors, row, lst);
                }
                table.AcceptChanges();
            }
            //
            //+++ Process V EXAM entries +++
            nIndex = data.Tables.IndexOf(worksheet.TableName("VExam"));
            if (nIndex > -1)
            {
                table = data.Tables[nIndex];
                table.AcceptChanges();
                foreach (DataRow row in table.Rows)
                {
                    List<string> lst = tableparse.ParseGenericRPCData(table, row, pntlist, "VExam");
                    if (lst[0] == "-1") continue;
                    Response response = client.CurrentConnection.Execute(new Request { MethodName = "ISI IMPORT V EXAM" }.AddParameter(lst));
                    results.Clear();
                    results = ResponseParser.ParseGenericResponse(response.RawData);
                    ResultLog("V EXAM", results, ref success, ref errors, row, lst);
                }
                table.AcceptChanges();
            }
            //
            //+++ Process V POV entries +++
            nIndex = data.Tables.IndexOf(worksheet.TableName("VPov"));
            if (nIndex > -1)
            {
                table = data.Tables[nIndex];
                table.AcceptChanges();
                foreach (DataRow row in table.Rows)
                {
                    List<string> lst = tableparse.ParseGenericRPCData(table, row, pntlist, "VPov");
                    if (lst[0] == "-1") continue;
                    Response response = client.CurrentConnection.Execute(new Request { MethodName = "ISI IMPORT V POV" }.AddParameter(lst));
                    results.Clear();
                    results = ResponseParser.ParseGenericResponse(response.RawData);
                    ResultLog("V POV", results, ref success, ref errors, row, lst);
                }
                table.AcceptChanges();
            }
            //
            //VPatientEd
            //+++ Process VPatientEd entries +++
            nIndex = data.Tables.IndexOf(worksheet.TableName("VPatientEd"));
            if (nIndex > -1)
            {
                table = data.Tables[nIndex];
                table.AcceptChanges();
                foreach (DataRow row in table.Rows)
                {
                    List<string> lst = tableparse.ParseGenericRPCData(table, row, pntlist, "VPatientEd");
                    if (lst[0] == "-1") continue;
                    Response response = client.CurrentConnection.Execute(new Request { MethodName = "ISI IMPORT V PATIENT ED" }.AddParameter(lst));
                    results.Clear();
                    results = ResponseParser.ParseGenericResponse(response.RawData);
                    ResultLog("V PNT ED", results, ref success, ref errors, row, lst);
                }
                table.AcceptChanges();
            }
            //
            // +++Process Treating Facility List++ +
            nIndex = data.Tables.IndexOf(worksheet.TableName("TreatingFacility"));
            if (nIndex > -1)
            {
                table = data.Tables[nIndex];
                table.AcceptChanges();
                foreach (DataRow row in table.Rows)
                {
                    List<string> lst = tableparse.ParseTreatingFacilityRPCData(table, row, pntlist);
                    if (lst[0] == "-1") continue;
                    Response response = client.CurrentConnection.Execute(new Request { MethodName = "ISI IMPORT TFL" }.AddParameter(lst));
                    results.Clear();
                    results = ResponseParser.ParseCreateTreatingFacility(response.RawData);
                    if (results != null)
                    {
                        if (results[0] == "success")
                        {
                            success++;
                            string logtxt = DateTime.Now.ToString() + "| Successfully created Treating Facility List entry. |";
                            foreach (string rec in lst)
                            {
                                logtxt = logtxt + " : " + rec;
                            }
                            logtxt = logtxt + "\r\n";
                            log.Write(logtxt);
                            row.Delete();
                        }
                        if (results[0] == "error")
                        {
                            errors++;
                            errtxt = DateTime.Now.ToString() + "| Error creating Treating Facility List entry. |" + results[1] + "|";
                            foreach (string rec in lst)
                            {
                                errtxt = errtxt + " : " + rec;
                            }
                            errtxt = errtxt + "\r\n";
                            log.Write(errtxt);
                        }
                    }
                }
                table.AcceptChanges();
            }
            //
            if (errors == 0)
            {
                dataGridView1.DataSource = null;
                dataGridView1.Refresh();
                sheetlistBox1.Items.Clear();
                import.Enabled = false;
            }
            else
            {
                dataGridView1.Refresh();
            }
            //
            errtxt = "Import results:  " + success.ToString() + " successful operations, with " + errors.ToString() + " failures. ";
            if (confirm > 0)
                errtxt = errtxt + confirm.ToString() + " lab entries need to be confirmed.\n";
            MessageBox.Show(errtxt,"Import Results");
            logfile("close");
        }

        private void ResultLog(string package, List<string> results, ref int success, ref int errors, DataRow row, List<string> lst)
        {
            if (results != null)
            {
                if (results[0] == "success")
                {
                    success++;
                    string logtxt = DateTime.Now.ToString() + "| Successfully created " + package +" entry |";
                    foreach (string rec in lst)
                    {
                        logtxt = logtxt + " : " + rec;
                    }
                    logtxt = logtxt + "\r\n";
                    log.Write(logtxt);
                    row.Delete();
                }
                if (results[0] == "error")
                {
                    errors++;
                    string errtxt = DateTime.Now.ToString() + "| Error creating " + package + " entry. |" + results[1] + "|";
                    foreach (string rec in lst)
                    {
                        errtxt = errtxt + " : " + rec;
                    }
                    errtxt = errtxt + "\r\n";
                    log.Write(errtxt);
                }
                if (results[0] == "duplicate")
                {
                    errors++;
                    string logtxt = DateTime.Now.ToString() + "| Duplicate entry - nothing created for " + package + " entry |";
                    foreach (string rec in lst)
                    {
                        logtxt = logtxt + " : " + rec;
                    }
                    logtxt = logtxt + "\r\n";
                    log.Write(logtxt);
                    row.Delete();
                }
            }
        }

        private void viewToolStripMenuItem_Click(object sender, EventArgs e)
        {
            System.Diagnostics.Process.Start("notepad.exe", logpath);
        }

        private void clearToolStripMenuItem_Click(object sender, EventArgs e)
        {  
            logfile("clear");
        }

        private void SearchICD(string search)
        {
            string errtxt;
            string filepath;
            List<string> results = new List<string>();
            filepath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), "DataLoader\\ICDFind.txt");
            Response response = client.CurrentConnection.Execute(new Request { MethodName = "ISI IMPORT ICDFIND" }.AddParameter(search));
            results = ResponseParser.ParseFetchTableResponse(response.RawData);

            if (results[0] == "error")
            {
                errtxt = "Error looking for " + search + " in VistA. (" + results[1] + ")";
                MessageBox.Show("ICD9 Search: " + errtxt + "\n", "ICD9 Search Error");
            }
            else
            {
                StreamWriter filewrite;
                if (File.Exists(filepath))
                {
                    File.Delete(filepath);
                }
                filewrite = new StreamWriter(filepath);
                foreach (string line in results)
                {
                    filewrite.Write(line + "\r\n");
                }
                filewrite.Close();
                filewrite.Dispose();
                filewrite = null;
                System.Diagnostics.Process.Start("notepad.exe", filepath);
            }
        }

        private void CreateColumnList(string column)
        {
            string errtxt;
            string filepath;
            List<string> results = new List<string>();
            filepath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), "DataLoader\\" + column + ".txt");
            Response response = client.CurrentConnection.Execute(new Request { MethodName = "ISI IMPORT TABLEFETCH" }.AddParameter(column));
            results = ResponseParser.ParseFetchTableResponse(response.RawData);

            if (results[0] == "error")
            {
                errtxt = "Error fetching " + column + " from VistA. (" + results[1] + ")";
                MessageBox.Show("Export Table " + errtxt + "\n", "Export Table Error");
            }
            else
            {
                StreamWriter filewrite;
                if (File.Exists(filepath))
                {
                    File.Delete(filepath);
                }
                filewrite = new StreamWriter(filepath);
                foreach (string line in results)
                {
                    filewrite.Write(line + "\r\n");
                }
                filewrite.Close();
                filewrite.Dispose();
                filewrite = null;
                System.Diagnostics.Process.Start("notepad.exe", filepath);
            }
        }

        private void noteToolStripMenuItem_Click(object sender, EventArgs e)
        {
            CreateColumnList("NOTE");
        }

        private void genderToolStripMenuItem_Click(object sender, EventArgs e)
        {
            CreateColumnList("GENDER");
        }
        private void booleenToolStripMenuItem_Click(object sender, EventArgs e)
        {
            CreateColumnList("BOOL");
        }

        private void raceToolStripMenuItem_Click(object sender, EventArgs e)
        {
            CreateColumnList("RACE");
        }

        private void ethnicityToolStripMenuItem_Click(object sender, EventArgs e)
        {
            CreateColumnList("ETHN");
        }

        private void employStatusToolStripMenuItem_Click(object sender, EventArgs e)
        {
            CreateColumnList("EMPLOY");
        }

        private void insuranceToolStripMenuItem_Click(object sender, EventArgs e)
        {
            CreateColumnList("INSUR");
        }

        private void locationToolStripMenuItem_Click(object sender, EventArgs e)
        {
            CreateColumnList("LOC");
        }

        private void personToolStripMenuItem_Click(object sender, EventArgs e)
        {
            CreateColumnList("USER");
        }

        private void problemstatusToolStripMenuItem_Click(object sender, EventArgs e)
        {
            CreateColumnList("PROBSTAT");
        }

        private void problemTypeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            CreateColumnList("PROBTYPE");
        }

        private void toolStripMenuItem2_Click(object sender, EventArgs e)
        {
            CreateColumnList("PROV");
        }
        
        private void vitaltypeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            CreateColumnList("VITAL");
        }

        private void allergenToolStripMenuItem_Click(object sender, EventArgs e)
        {
            CreateColumnList("ALLER");
        }

        private void symptomToolStripMenuItem_Click(object sender, EventArgs e)
        {
            CreateColumnList("SYMP");
        }

        private void labtestToolStripMenuItem_Click(object sender, EventArgs e)
        {
            CreateColumnList("LAB");
        }

        private void druglistToolStripMenuItem_Click(object sender, EventArgs e)
        {
            CreateColumnList("DRUG");
        }

        private void siglistToolStripMenuItem_Click(object sender, EventArgs e)
        {
            CreateColumnList("SIG");
        }

        private void consultToolStripMenuItem_Click(object sender, EventArgs e)
        {
            CreateColumnList("CONSULT");
        }

        private void imagLoc791ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            CreateColumnList("MAGLOC");
        }

        private void radProc71ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            CreateColumnList("RADPROC");
        }

        private void toolStripMenuItemHFactor_Click(object sender, EventArgs e)
        {
            CreateColumnList("HFACTOR");
        }

        private void toolStripMenuItemCpt_Click(object sender, EventArgs e)
        {
            CreateColumnList("CPT");
        }

        private void toolStripMenuItemProvNarr_Click(object sender, EventArgs e)
        {
            CreateColumnList("PRVNAR");
        }

        private void toolStripMenuItemImmz_Click(object sender, EventArgs e)
        {
            CreateColumnList("IMZ");
        }

        private void toolStripMenuItemExam_Click(object sender, EventArgs e)
        {
            CreateColumnList("EXAM");
        }

        private void toolStripMenuItemEdTopic_Click(object sender, EventArgs e)
        {
            CreateColumnList("EDTOPIC");
        }

        private void institution4ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            CreateColumnList("INST");
        }

        private void nonvamedsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            CreateColumnList("NVAMEDS");
        }
        private void exportTablesToolStripMenuItem_Click(object sender, EventArgs e)
        {
        
        }

        private void aboutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            string display="VistA Data Loader Tool 2.0.05\r\n\r\n";
            display = display + "VistA Data Loader is provided by the Johns Hopkins University School of Nursing, and funded by the Department of Health and Human Services, Office of the National Coordinator for Health Information Technology under Award Number #1U24OC000013-01.\r\n\r\n";
            display = display + "Copyright (C) 2012 Johns Hopkins University\r\n\r\n";
            display = display + "All portions of this release that are modified from the original Freedom of Information Act release provided by the Department of Veterans Affairs is subject to the terms of the GNU Affero General Public License as published by the Free Software Foundation, either version 3 of the License, or any later version.\r\n\r\n";
            display = display + "This program is distributed in the hope that it will be useful, but WITHOUT ANY WARRANTY; without even the implied warranty of MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the GNU Affero General Public License for more details.\r\n\r\n";
            display = display + "You should have received a copy of the GNU Affero General Public License along with this program.  If not, see http://www.gnu.org/licenses/.";
            MessageBox.Show(display, "About VistA Data Loader");
        }

        private void iCD9SearchToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Form_ICDSearch frm = new Form_ICDSearch();
            frm.Show();
            frm.VisibleChanged += formVisableChanged;
        }

        private void formVisableChanged(object sender, EventArgs e)
        {
            Form_ICDSearch frm = (Form_ICDSearch)sender;
            if (!frm.Visible)
            {
                SearchICD(frm.SearchText);
                frm.Dispose();
            }
        }

        private void launchbatchform()
        {
            if (conn_status.Text == "disconnected" || conn_status.Text == "") return;
            Form_BatchUtil batchform = Form_BatchUtil.GetInstance(client, this);
            batchform.Show();
            batchform.Focus();
        }

        private void batchImportUtilityToolStripMenuItem_Click(object sender, EventArgs e)
        {
            launchbatchform();
        }

    }
}
