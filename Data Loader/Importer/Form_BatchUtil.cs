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
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace DataLoader
{

    using DataLoader.Broker;
    using DataLoader.Common.Model;
    using System.Text.RegularExpressions;
    using System.IO;
    
    public partial class Form_BatchUtil : Form
    {
        public string port;
        public string server;
        public string filepath;
        public string logpath;
        private Form_Main mainwindow = null;

        Client client = new Client();
        List<string> results = new List<string>();
        StringItemList List = new StringItemList();
        Template template = new Template();
        PatientLoad patient = new PatientLoad();
        UserLoad user = new UserLoad();
        Validate validate = new Validate();

        public Form_BatchUtil(Client passclient, Form callingform)
        {
            client = passclient;
            mainwindow = callingform as Form_Main;
            InitializeComponent();
            logpath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), "DataLoader\\DataLoader.log");
            PopulateTemplateList(); // populates combox list
            PopulateComboBoxes();
        }

        private static Form_BatchUtil _instance;

        public static Form_BatchUtil GetInstance(Client client, Form callingform)
        {
            if (_instance == null || _instance.IsDisposed) _instance = new Form_BatchUtil(client, callingform);
            _instance.KeyPreview = true;
            return _instance;
        }

        private void PopulateTemplateList()
        {
            StringItemList list = new StringItemList();
            Response response = client.CurrentConnection.Execute(new Request { MethodName = "ISI IMPORT GET TEMPLATES" });
            list = ResponseParser.ParseFetchTemplateResponse(response.RawData);
            this.comboBoxTemplate1.Items.Clear();
            this.comboBoxTemplate2.Items.Clear();
            this.comboBoxTemplate3.Items.Clear();
            foreach (StringItem item in list)
            {
                Item obj = new Item();
                obj.strText = item.name.ToUpper();
                obj.strValue = item.id;
                this.comboBoxTemplate1.Items.Add(obj);
                this.comboBoxTemplate2.Items.Add(obj);
                this.comboBoxTemplate3.Items.Add(obj);
            }
            //this.comboBoxTemplate3.Items.Add("<NEW>");
            //
            this.comboBoxTemplate1.Text = "Select...";
            this.comboBoxTemplate2.Text = "Select...";
            this.comboBoxTemplate3.Text = "Select...";
        }

        private void PopulateComboBoxes()
        {
            List<string> list = new List<string>();
            list.Add("PNTTYPE");
            list.Add("GENDER");
            list.Add("EMPLOY");
            list.Add("MARSTAT");
            list.Add("STATE");
            list.Add("SERVICE");
            list.Add("USER");
            //
            foreach (string name in list)
                PopulateComboBoxLists(name);
        }
        private void PopulateComboBoxLists(string strTable)
        {
            string errtxt;
            List<string> results = new List<string>();
            Response response = client.CurrentConnection.Execute(new Request { MethodName = "ISI IMPORT TABLEFETCH" }.AddParameter(strTable));
            results = ResponseParser.ParseFetchTableResponse(response.RawData);
            if (results[0] == "error")
            {
                errtxt = "Error fetching " + strTable + " from VistA. (" + results[1] + ")";
                MessageBox.Show("Select Table " + errtxt + "\n", "Error Fetching Table");
            }
            else
            {
                foreach (string line in results)
                {
                    Item obj = new Item();
                    obj.strText = line;
                    obj.strValue = line;
                    switch (strTable)
                    {
                        case "PNTTYPE":
                            {
                                this.comboBoxPntType.Items.Add(obj);
                                this.comboBoxTemplatePntType.Items.Add(obj);
                                break;
                            }
                        case "GENDER":
                            {
                                this.comboBoxSex.Items.Add(obj);
                                this.comboBoxUserSex.Items.Add(obj);
                                this.comboBoxTemplateSex.Items.Add(obj);
                                break;
                            }
                        case "EMPLOY":
                            {
                                this.comboBoxEmploy.Items.Add(obj);
                                this.comboboxTemplateEmploy.Items.Add(obj);
                                break;
                            }
                        case "MARSTAT":
                            {
                                this.comboBoxMarStat.Items.Add(obj);
                                this.comboBoxTemplateMarStat.Items.Add(obj);
                                break;
                            }
                        case "STATE":
                            {
                                this.comboBoxState.Items.Add(obj);
                                this.comboBoxUserState.Items.Add(obj);
                                this.comboBoxTemplateState.Items.Add(obj);
                                break;
                            }
                        case "SERVICE":
                            {
                                this.comboBoxService.Items.Add(obj);
                                this.comboBoxTemplateService.Items.Add(obj);
                                break;
                            }
                        case "USER":
                            {
                                this.comboBoxSourceUser.Items.Add(obj);
                                break;
                            }
                        default:
                            {
                                break;
                            }
                    }
                    // add to patient type combo box
                }
            }
        }

        private class Item
        {
            public string strText;
            public string strValue;
            public override string ToString()
            {
                return this.strText;
            }
        }

        private void comboBoxTemplate1_SelectedIndexChanged(object sender, EventArgs e)
        {
            Item obj = comboBoxTemplate1.SelectedItem as Item;
            if (obj != null)
            {
                FetchTemplateDetails(obj);
                PopulatePatientDetails();
                FillScreen1(); //patient screen
            }
        }

        private void comboBoxTemplate2_SelectedIndexChanged(object sender, EventArgs e)
        {
            Item obj = comboBoxTemplate2.SelectedItem as Item;
            if (obj != null)
            {
                FetchTemplateDetails(obj);
                PopulateUserDetails();
                FillScreen2(); //user screen
            }
        }

        private void comboBoxTemplate3_SelectedIndexChanged(object sender, EventArgs e)
        {
            //if (comboBoxTemplate3.SelectedItem.ToString() == "<NEW>")
            //{
            //    textBoxNewTemplate.Enabled = true;
            //    return;
            //}
            Item obj = comboBoxTemplate3.SelectedItem as Item;
            if (obj != null)
            {
                textBoxNewTemplate.Enabled = false;
                FetchTemplateDetails(obj);
                FillScreen3(); //template screen
                //
            }
        }

        private void FetchTemplateDetails(Item obj)
        {
            template.Clear();
            template.strName = obj.strText;
            template.strNameInt = obj.strValue;
            Response response = client.CurrentConnection.Execute(new Request { MethodName = "ISI IMPORT GET TEMPLATE DETLS" }.AddParameter(obj.strValue));
            StringItemList list = ResponseParser.ParseTemplateDetailResponse(response.RawData);
            PopulateTemplateDetails(list);
        }

        private void PopulateTemplateDetails(StringItemList list)
        {
            foreach (StringItem item in list)
            {
                string name = item.name;
                string id = item.id;
                string[] parts = name.Split('|');
                try
                {
                    string xtern = parts[0];
                    string intern = parts[1];
                    switch (id)
                    {
                        case "1":
                            {
                                //Type
                                template.strType = xtern;
                                template.strTypeInt = intern;
                                break;
                            }
                        case "2":
                            {
                                //Patient Mask
                                template.strNameMask = xtern;
                                break;
                            }
                        case "4":
                            {
                                //SSN Mask
                                template.strSsnMask = xtern;
                                break;
                            }
                        case "5":
                            {
                                //Sex
                                template.strSex = xtern;
                                break;
                            }
                        case "6":
                            {
                                //EDOB (earliest or Low DOB)
                                template.strEDOB = xtern;
                                template.strEDOBInt = intern;
                                break;
                            }
                        case "7":
                            {
                                //LDOB (latest or Upper DOB)
                                template.strLDOB = xtern;
                                template.strLDOBInt = intern;
                                break;
                            }
                        case "8":
                            {
                                //Maritial Status
                                template.strMaritalStat = xtern;
                                template.strMaritalStatInt = intern;
                                break;
                            }

                        case "9":
                            {
                                //ZIP mask
                                template.strZIP4 = xtern;
                                break;
                            }
                        case "10":
                            {
                                //Phone mask
                                template.strPHMask = xtern;
                                break;
                            }
                        case "11":
                            {
                                //City
                                template.strCity = xtern;
                                break;
                            }
                        case "12":
                            {
                                //State
                                template.strState = xtern;
                                template.strStateInt = intern;
                                break;
                            }
                        case "13":
                            {
                                //Veteran
                                template.strVeteran = xtern;
                                break;
                            }
                        case "14":
                            {
                                //DFN Name
                                template.strDfnName = xtern;
                                break;
                            }
                        case "15":
                            {
                                //Employment status
                                template.strEmployStat = xtern;
                                template.strEmployStatInt = intern;
                                break;
                            }
                        case "16":
                            {
                                //Service
                                template.strService = xtern;
                                template.strServiceInt = intern;
                                break;
                            }
                        case "17":
                            {
                                //Email mask
                                template.strEmail = xtern;
                                break;
                            }
                        case "18":
                            {
                                //User mask
                                template.strUserMask = xtern;
                                break;
                            }
                        case "19":
                            {
                                //ESIG APPEND
                                //
                                template.strESigApnd = xtern;
                                break;
                            }
                        case "20":
                            {
                                //ACCESS APPEND
                                template.strAcccessApnd = xtern;
                                break;
                            }
                        case "21":
                            {
                                //VERIFY APPEND 
                                template.strVerifyApnd = xtern;
                                break;
                            }
                         default:
                            {
                                break;
                            }
                    }
                }
                catch
                {
                    continue;
                }                
            }
        }

        private void PopulatePatientDetails()
        {
            //Set Patient Batch Load details to selected template values
            patient.Clear();
            patient.Template = template.strName;
            patient.Type = template.strType;
            patient.NameMask = template.strNameMask;
            patient.SSNMask = template.strSsnMask;
            patient.Sex = template.strSex;
            patient.LowDob = template.strEDOB;
            patient.UpDob = template.strLDOB;
            patient.MaritalStatus = template.strMaritalStat;
            patient.Zip4Mask = template.strZIP4;
            patient.PhNumMask = template.strPHMask;
            patient.City = template.strCity;
            patient.State = template.strState;
            patient.Veteran = template.strVeteran;
            patient.DfnName = template.strDfnName;
            patient.EmployStat = template.strEmployStat;
        }

        private void PopulateUserDetails()
        {
            //Set User batch load details to selected template values
            user.Clear();
            user.Template = template.strName;
            user.ImpType = template.strType;
            user.DfnName = template.strDfnName;
            user.NameMask = template.strUserMask;
            user.Sex = template.strSex;
            user.LowDob = template.strEDOB;
            user.UpDob = template.strLDOB;
            user.SSNMask = template.strSsnMask;
            user.City = template.strCity;
            user.State = template.strState;
            user.Zip4Mask = template.strZIP4;
            user.PhNumMask = template.strPHMask;
            user.EmailMask = template.strEmail;
            user.Service = template.strService;
            user.AccessAppend = template.strAcccessApnd;
            user.VerifyAppend = template.strVerifyApnd;
            user.ESAppend = template.strESigApnd;
        }

        private void FillScreen1()
        {
            //Populate the batch patient loader screen
            comboBoxPntType.Text = patient.Type;
            textBoxNameMask.Text = patient.NameMask;
            textBoxSSNMask.Text = patient.SSNMask;
            comboBoxSex.Text = patient.Sex;
            textBoxLDOB.Text = patient.LowDob;
            textBoxUDOB.Text = patient.UpDob;
            comboBoxMarStat.Text = patient.MaritalStatus;
            textBoxZip.Text = patient.Zip4Mask;
            textBoxPhone.Text = patient.PhNumMask;
            textBoxCity.Text = patient.City;
            comboBoxState.Text = patient.State;
            comboBoxVeteran.Text = patient.Veteran;
            comboBoxPntDFN.Text = patient.DfnName;
            comboBoxEmploy.Text = patient.EmployStat;
        }

        private void FillScreen2()
        {
            //populate the batch user loader screen
            textBoxUserCity.Text = user.City;
            textBoxUserEmail.Text = user.EmailMask;
            textBoxUserLDOB.Text = user.LowDob;
            textBoxUserEDOB.Text = user.UpDob;
            comboBoxUserDFN.Text = user.DfnName;
            comboBoxUserSex.Text = user.Sex;
            comboBoxUserState.Text = user.State;
            comboBoxSourceUser.Text = user.MrgSource;
            textBoxUserNameMask.Text = user.NameMask;
            textBoxUserPhone.Text = user.PhNumMask;
            textBoxUserSSN.Text = user.SSNMask;
            textBoxUserZip.Text = user.Zip4Mask;
            comboBoxService.Text = user.Service;
            textBoxUserAccessAppend.Text = user.AccessAppend;
            textBoxUserVerifyAppend.Text = user.VerifyAppend;
            textBoxUserESAppend.Text = user.ESAppend;
        }

        private void FillScreen3()
        {
            //populate the template edit screen
            textBoxTemplateCity.Text = template.strCity;
            textBoxTemplateEmailMask.Text = template.strEmail;
            textBoxTemplateLDOB.Text = template.strEDOB;  //earliest or latest DOB
            textBoxTemplateNameMask.Text = template.strNameMask;
            textBoxTemplatePHMask.Text = template.strPHMask;
            textBoxTemplateSSNMask.Text = template.strSsnMask;
            textBoxTemplateUDOB.Text = template.strLDOB; // latest or upper DOB
            textBoxTemplateZIPMask.Text = template.strZIP4;
            comboboxTemplateEmploy.Text = template.strEmployStat;
            comboBoxTemplateMarStat.Text = template.strMaritalStat;
            comboBoxTemplatePntType.Text = template.strType;
            comboBoxTemplateService.Text = template.strService;
            comboBoxTemplateSex.Text = template.strSex;
            comboBoxTemplateState.Text = template.strState;
            comboBoxTemplateVeteran.Text = template.strVeteran;
            comboTemplateDFN.Text = template.strDfnName;
            textBoxTemplateUserNameMask.Text = template.strUserMask;
            textBoxTemplateAccessApnd.Text = template.strAcccessApnd;
            textBoxTemplateESAppend.Text = template.strESigApnd;
            textBoxTemplateVerifyAppnd.Text = template.strVerifyApnd;
        }

        private void buttonPntSubmit_Click(object sender, EventArgs e)
        {
            Patient pnt = new Patient();
            
            // validate values
            int valcheck = ValidatePatientValues();
            if (valcheck > 0)
            {
                string errtxt = "";
                if (valcheck == 1) errtxt = "Bad (start) DOB format.";
                if (valcheck == 2) errtxt = "Bad (end) DOB format.";
                if (valcheck == 3) errtxt = "Bad 'numeric' Mask value.";
                if (valcheck == 4) errtxt = "Create # must be greater than zero.";
                MessageBox.Show("Validation Error: " + errtxt + "\n", "Can't create patients.");
                return;
            }

            //update patient array
            UpdatePntValues();

            // Create array to pass to rpc
            List<string> pntrpclist = CreatePntRPCArray();
            
            // call rpc
            Response response = client.CurrentConnection.Execute(new Request { MethodName = "ISI IMPORT PAT" }.AddParameter(pntrpclist));
            results = ResponseParser.ParseCreatePatientResponse(response.RawData, pnt);
            if (results[0] == "success")
            {
                foreach (string result in results)
                {
                    if (result == "success") continue;
                    textBatchLogDisp.Text = textBatchLogDisp.Text + result + "\r\n";
                }
                
            }
            if (results[0] == "error")
            {
                string errtxt = DateTime.Now.ToString() + "| Error when attempting to create VistA PATIENT record.  |" + results[1];
                textBatchLogDisp.Text = textBatchLogDisp.Text + errtxt + "\r\n";
            }
        }

        private int ValidatePatientValues()
        {
            int results = 0;
            string check;
            
            //check dates
            if (textBoxLDOB.Text.Length > 0)
            {
                check = validate.VerifyandSetDateTime(client, textBoxLDOB.Text, false);
                if (check.Length < 1)
                {
                    results = 1; //bad LDOB
                    return results;
                }
            }
            if (textBoxUDOB.Text.Length > 0)
            {
                check = validate.VerifyandSetDateTime(client, textBoxUDOB.Text, false);
                if (check.Length < 1)
                {
                    results = 2; //bad UDOB
                    return results;
                }
            }
            //
            //validate numeric mask values
            try
            {
                int num;
                if (textBoxSSNMask.Text.Length > 0)
                {
                    num = Convert.ToInt32(textBoxSSNMask.Text);
                }
                if (textBoxZip.Text.Length > 0)
                {
                    num = Convert.ToInt32(textBoxZip.Text);
                }
                if (textBoxPhone.Text.Length > 0)
                {
                    num = Convert.ToInt32(textBoxPhone.Text);
                }
            }
            catch
            {
                results = 3; //bad mask value (must be numeric)
                return results;
            }
            if (numericBatchNum.Value < 1)
            {
                results = 4;
            }
            //
            return results;
        }

        private void UpdatePntValues()
        {
            patient.Template = comboBoxTemplate1.Text;
            patient.Type = comboBoxPntType.Text;
            patient.NameMask = textBoxNameMask.Text;
            patient.SSNMask = textBoxSSNMask.Text;
            patient.Sex = comboBoxSex.Text;
            patient.LowDob = textBoxLDOB.Text;
            patient.UpDob = textBoxUDOB.Text;
            patient.MaritalStatus = comboBoxMarStat.Text;
            patient.Zip4Mask = textBoxZip.Text;
            patient.PhNumMask = textBoxPhone.Text;
            patient.City = textBoxCity.Text;
            patient.State = comboBoxState.Text;
            patient.Veteran = comboBoxVeteran.Text;
            patient.DfnName = comboBoxPntDFN.Text;
            patient.EmployStat = comboBoxEmploy.Text;
        }

        private int ValidateTemplateValues()
        {
            int results = 0;
            string check;

            //check dates
            if (textBoxTemplateLDOB.Text.Length > 0)
            {
                check = validate.VerifyandSetDateTime(client, textBoxTemplateLDOB.Text, false);
                if (check.Length < 1)
                {
                    results = 1; //bad LDOB
                    return results;
                }
            }
            if (textBoxTemplateUDOB.Text.Length >0)
            {
                check = validate.VerifyandSetDateTime(client, textBoxTemplateUDOB.Text, false);
                if (check.Length < 1)
                {
                    results = 2; //bad UDOB
                    return results;
                }
            }
            //
            //validate numeric mask values
            try
            {
                int num;
                if (textBoxTemplateSSNMask.Text.Length >0)
                {
                    num = Convert.ToInt32(textBoxTemplateSSNMask.Text);
                }
                if (textBoxTemplatePHMask.Text.Length >0)
                {
                    num = Convert.ToInt32(textBoxTemplatePHMask.Text);
                }
                if (textBoxTemplateZIPMask.Text.Length >0)
                {
                    num = Convert.ToInt32(textBoxTemplateZIPMask.Text);
                }
            }
            catch
            {
                results = 3; //bad mask value (must be numeric)
                return results;
            }
            if (comboBoxTemplate3.Text != "DEFAULT")
            {
                results = 4; //must select Template
            }
            return results;
        }

        private int ValidateUserValues()
        {
            int results = 0;
            string check;

            //check dates
            if (textBoxUserLDOB.Text.Length > 0)
            {
                check = validate.VerifyandSetDateTime(client, textBoxUserLDOB.Text, false);
                if (check.Length < 1)
                {
                    results = 1; //bad LDOB
                    return results;
                }
            }
            if (textBoxUserEDOB.Text.Length > 0)
            {
                check = validate.VerifyandSetDateTime(client, textBoxUserEDOB.Text, false);
                if (check.Length < 1)
                {
                    results = 2; //bad UDOB
                    return results;
                }
            }
            //
            //validate numeric mask values
            try
            {
                int num;
                if (textBoxUserSSN.Text.Length > 0)
                {
                    num = Convert.ToInt32(textBoxUserSSN.Text);
                }
                if (textBoxUserZip.Text.Length > 0)
                {
                    num = Convert.ToInt32(textBoxUserZip.Text);
                }
                if (textBoxUserPhone.Text.Length > 0)
                {
                    num = Convert.ToInt32(textBoxUserPhone.Text);
                }
            }
            catch
            {
                results = 3; //bad mask value (must be numeric)
                return results;
            }
            if (numericUserBatchNum.Value < 1)
            {
                results = 4;
            }
            //
            return results;
        }

        private void UpdateUserValues()
        {
            user.City = textBoxUserCity.Text;
            user.EmailMask = textBoxUserEmail.Text;
            user.LowDob = textBoxUserLDOB.Text;
            user.UpDob = textBoxUserEDOB.Text;
            user.DfnName = comboBoxUserDFN.Text;
            user.Sex = comboBoxUserSex.Text;
            user.State = comboBoxUserState.Text;
            user.MrgSource = comboBoxSourceUser.Text;
            user.NameMask = textBoxUserNameMask.Text;
            user.PhNumMask = textBoxUserPhone.Text;
            user.SSNMask = textBoxUserSSN.Text;
            user.Zip4Mask = textBoxUserZip.Text;
            user.Service = comboBoxService.Text;
            user.AccessAppend = textBoxUserAccessAppend.Text;
            user.VerifyAppend = textBoxUserVerifyAppend.Text;
            user.ESAppend = textBoxUserESAppend.Text;
        }

        private void UpdateTemplateValues()
        {
            template.strCity = textBoxTemplateCity.Text; // = template.strCity;
            template.strEmail = textBoxTemplateEmailMask.Text;
            template.strEDOB = textBoxTemplateLDOB.Text;  //earliest or latest DOB
            template.strNameMask = textBoxTemplateNameMask.Text;
            template.strPHMask = textBoxTemplatePHMask.Text;
            template.strSsnMask = textBoxTemplateSSNMask.Text;
            template.strLDOB = textBoxTemplateUDOB.Text; // latest or upper DOB
            template.strZIP4 = textBoxTemplateZIPMask.Text;
            template.strEmployStat = comboboxTemplateEmploy.Text;
            template.strMaritalStat = comboBoxTemplateMarStat.Text;
            template.strType = comboBoxTemplatePntType.Text;
            template.strService = comboBoxTemplateService.Text;
            template.strSex = comboBoxTemplateSex.Text;
            template.strState = comboBoxTemplateState.Text;
            template.strVeteran = comboBoxTemplateVeteran.Text;
            template.strDfnName = comboTemplateDFN.Text;
            template.strUserMask = textBoxTemplateUserNameMask.Text;
            template.strVerifyApnd = textBoxTemplateVerifyAppnd.Text;
            template.strAcccessApnd = textBoxTemplateAccessApnd.Text;
            template.strESigApnd = textBoxTemplateESAppend.Text;
        }

        private void buttonSaveTemplate_Click(object sender, EventArgs e)
        {
            Template template = new Template();
            //
            // validate values
            int valcheck = ValidateTemplateValues();
            if (valcheck > 0)
            {
                string errtxt = "";
                if (valcheck == 1) errtxt = "Bad (start) DOB format.";
                if (valcheck == 2) errtxt = "Bad (end) DOB format.";
                if (valcheck == 3) errtxt = "Bad 'numeric' Mask value.";
                if (valcheck == 4) errtxt = "Must select Template.";
                MessageBox.Show("Validation Error: " + errtxt + "\n", "Can't save templates.");
                return;
            }
            //
            UpdateTemplateValues();
            //
            List<string> templaterpclist = CreateTemplateArray();
            //
            // call rpc
            Response response = client.CurrentConnection.Execute(new Request { MethodName = "ISI IMPORT SAVE TEMPLATE" }.AddParameter(templaterpclist));
            results = ResponseParser.ParseTemplateSaveResponse(response.RawData);
            if (results[0] == "success")
            {
                foreach (string result in results)
                {
                    if (result == "success")
                    {
                        textBatchLogDisp.Text = textBatchLogDisp.Text + "Saved " + comboBoxTemplate3.Text + " template.\r\n";
                        continue;
                    }
                }
            }
            if (results[0] == "error")
            {
                string errtxt = DateTime.Now.ToString() + "| Error when attempting to update Template record.  |" + results[1];
                textBatchLogDisp.Text = textBatchLogDisp.Text + errtxt + "\r\n";
            }
        }

        private void buttonsubmitUser_Click(object sender, EventArgs e)
        {

            UserLoad user = new UserLoad();

            // validate values
            int valcheck = ValidateUserValues();
            

            if (valcheck > 0)
            {
                string errtxt = "";
                if (valcheck == 1) errtxt = "Bad (start) DOB format.";
                if (valcheck == 2) errtxt = "Bad (end) DOB format.";
                if (valcheck == 3) errtxt = "Bad 'numeric' Mask value.";
                if (valcheck == 4) errtxt = "Create # must be greater than zero.";
                MessageBox.Show("Validation Error: " + errtxt + "\n", "Can't create Users.");
                return;
            }

            //update patient array
            UpdateUserValues();

            // Create array to pass to rpc
            List<string> userrpclist = CreateUserArray();

            // call rpc
            Response response = client.CurrentConnection.Execute(new Request { MethodName = "ISI IMPORT USER" }.AddParameter(userrpclist));
            results = ResponseParser.ParseCreateUserResponse(response.RawData, user);
            if (results[0] == "success")
            {
                foreach (string result in results)
                {
                    if (result == "success") continue;
                    textBatchLogDisp.Text = textBatchLogDisp.Text + result + "\r\n";
                }
            }
            if (results[0] == "error")
            {
                string errtxt = DateTime.Now.ToString() + "| Error when attempting to create VistA User record.  |" + results[1];
                textBatchLogDisp.Text = textBatchLogDisp.Text + errtxt + "\r\n";
            }
        }

        private List<string> CreatePntRPCArray()
        {
            List<string> list = new List<string>();
            list.Add("TEMPLATE^DEFAULT"); // + patient.Template);
            list.Add("IMP_TYPE^B");
            list.Add("IMP_BATCH_NUM^" + numericBatchNum.Value.ToString());
            if (patient.DfnName !="") list.Add("DFN_NAME^" + patient.DfnName);
            if (patient.Type !="") list.Add("TYPE^" + patient.Type);
            if (patient.NameMask !="") list.Add("NAME_MASK^" + patient.NameMask);
            if (patient.Sex !="") list.Add("SEX^" + patient.Sex);
            if (patient.LowDob !="") list.Add("LOW_DOB^" + patient.LowDob);
            if (patient.UpDob !="") list.Add("UP_DOB^" + patient.UpDob);
            if (patient.MaritalStatus !="") list.Add("MARITAL_STATUS^" + patient.MaritalStatus);
            if (patient.SSNMask !="") list.Add("SSN_MASK^" + patient.SSNMask);
            if (patient.City !="") list.Add("CITY^" + patient.City);
            if (patient.State !="") list.Add("STATE^" + patient.State);
            if (patient.Zip4Mask !="") list.Add("ZIP_4_MASK^" + patient.Zip4Mask);
            if (patient.PhNumMask !="") list.Add("PH_NUM_MASK^" + patient.PhNumMask);
            if (patient.EmployStat !="") list.Add("EMPLOY_STAT^" + patient.EmployStat);
            if (patient.Veteran !="") list.Add("VETERAN^" + patient.Veteran);
            if (textBoxSourcePatient.Text != "" && textBoxSourcePatient.Text != null)
            {
                list.Add("MRG_SOURCE^" + textBoxSourcePatient.Text);
            }
            return list;
        }

        private List<string> CreateUserArray()
        {
            List<string> list = new List<string>();
            list.Add("TEMPLATE^DEFAULT"); // + user.Template);
            list.Add("IMP_TYPE^B");
            list.Add("IMP_BATCH_NUM^" + numericUserBatchNum.Value.ToString());
            if (user.DfnName !="") list.Add("DFN_NAME^" + user.DfnName);
            if (user.NameMask !="") list.Add("NAME_MASK^" + user.NameMask);
            if (user.Sex !="") list.Add("SEX^" + user.Sex);
            if (user.LowDob !="") list.Add("LOW_DOB^" + user.LowDob);
            if (user.UpDob !="") list.Add("UP_DOB^" + user.UpDob);
            if (user.SSNMask !="") list.Add("SSN_MASK^" + user.SSNMask);
            if (user.City !="") list.Add("CITY^" + user.City);
            if (user.State !="") list.Add("STATE^" + user.State);
            if (user.Zip4Mask !="") list.Add("ZIP_4_MASK^" + user.Zip4Mask);
            if (user.PhNumMask !="") list.Add("PH_NUM_MASK^" + user.PhNumMask);
            if (user.Service !="") list.Add("SERVICE^" + user.Service);
            if (user.MrgSource !="") list.Add("MRG_SOURCE^" + user.MrgSource);
            if (user.ESAppend !="") list.Add("ELSIG_APND^" + user.ESAppend);
            if (user.AccessAppend !="") list.Add("ACCESS_APND^" + user.AccessAppend);
            if (user.VerifyAppend !="") list.Add("VERIFY_APND^" + user.VerifyAppend);
            if (CreateAccessVerify.Checked == true)
            {
                list.Add("GEN_ACCVER^1");
            }
            //
            return list;
        }

        private List<string> CreateTemplateArray()
        {
            List<string> list = new List<string>();
            list.Add("NAME^DEFAULT"); // + template.strName);
            list.Add("TYPE^" + template.strType);
            list.Add("NAME_MASK^" + template.strNameMask);
            list.Add("SSN_MASK^" + template.strSsnMask);
            list.Add("SEX^" + template.strSex);
            list.Add("EDOB^" + template.strEDOB);
            list.Add("LDOB^" + template.strLDOB);
            list.Add("MARITAL_STATUS^" + template.strMaritalStat);
            list.Add("ZIP_MASK^" + template.strZIP4);
            list.Add("PH_NUM^" + template.strPHMask);
            list.Add("CITY^" + template.strCity);
            list.Add("STATE^" + template.strState);
            list.Add("VETERAN^" + template.strVeteran);
            list.Add("DFN_NAME^" + template.strDfnName);
            list.Add("EMPLOY_STAT^"  + template.strEmployStat);
            list.Add("SERVICE^" + template.strService);
            list.Add("EMAIL_MASK^"  + template.strEmail);
            list.Add("USER_MASK^" + template.strUserMask);
            list.Add("ESIG_APND^" + template.strESigApnd);
            list.Add("ACCESS_APND^" + template.strAcccessApnd);
            list.Add("VERIFY_APND^" + template.strVerifyApnd);
            return list;
        }

        private void textBoxSourcePatient_TextChanged(object sender, EventArgs e)
        {
            if (textBoxSourcePatient.Text.Length > 8)
            {
                string rpcname = "";
                string search = "";
                try
                {
                    int conv = Convert.ToInt32(textBoxSourcePatient.Text.Substring(0,9));
                    rpcname = "ORWPT FULLSSN";
                    search = textBoxSourcePatient.Text;
                }
                catch
                {
                    //
                }
                if (rpcname != "")
                {
                    Response response = new Response();
                    Request req = new Request { MethodName = rpcname };
                    req.AddParameter(search);
                    response = client.CurrentConnection.Execute(req);
                    try
                    {
                        string strpnt = response.RawData;
                        string[] fields = strpnt.Split('^');
                        textBoxSourcePatient.Text = fields[1];
                    }
                    catch
                    {
                            //
                    }
                }
            }
        }

        private void tab2_Users_Click(object sender, EventArgs e)
        {

        }
    }
}
