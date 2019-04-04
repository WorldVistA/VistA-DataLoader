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
    partial class Form_BatchUtil
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form_BatchUtil));
            this.BatchImport = new System.Windows.Forms.TabControl();
            this.tab1_Patients = new System.Windows.Forms.TabPage();
            this.label19 = new System.Windows.Forms.Label();
            this.textBoxSourcePatient = new System.Windows.Forms.TextBox();
            this.comboBoxState = new System.Windows.Forms.ComboBox();
            this.comboBoxMarStat = new System.Windows.Forms.ComboBox();
            this.comboBoxSex = new System.Windows.Forms.ComboBox();
            this.comboBoxEmploy = new System.Windows.Forms.ComboBox();
            this.comboBoxVeteran = new System.Windows.Forms.ComboBox();
            this.comboBoxPntDFN = new System.Windows.Forms.ComboBox();
            this.comboBoxPntType = new System.Windows.Forms.ComboBox();
            this.comboBoxTemplate1 = new System.Windows.Forms.ComboBox();
            this.buttonPntSubmit = new System.Windows.Forms.Button();
            this.labelNumber = new System.Windows.Forms.Label();
            this.numericBatchNum = new System.Windows.Forms.NumericUpDown();
            this.label12 = new System.Windows.Forms.Label();
            this.label11 = new System.Windows.Forms.Label();
            this.label10 = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.textBoxCity = new System.Windows.Forms.TextBox();
            this.label7 = new System.Windows.Forms.Label();
            this.textBoxPhone = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.textBoxZip = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.textBoxUDOB = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.textBoxLDOB = new System.Windows.Forms.TextBox();
            this.labelSex = new System.Windows.Forms.Label();
            this.labelSSNMask = new System.Windows.Forms.Label();
            this.textBoxSSNMask = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.labelTypeofPnt = new System.Windows.Forms.Label();
            this.textBoxNameMask = new System.Windows.Forms.TextBox();
            this.labelTemplate = new System.Windows.Forms.Label();
            this.tab2_Users = new System.Windows.Forms.TabPage();
            this.label47 = new System.Windows.Forms.Label();
            this.textBoxUserESAppend = new System.Windows.Forms.TextBox();
            this.label48 = new System.Windows.Forms.Label();
            this.textBoxUserVerifyAppend = new System.Windows.Forms.TextBox();
            this.label49 = new System.Windows.Forms.Label();
            this.textBoxUserAccessAppend = new System.Windows.Forms.TextBox();
            this.comboBoxUserDFN = new System.Windows.Forms.ComboBox();
            this.label15 = new System.Windows.Forms.Label();
            this.comboBoxUserSex = new System.Windows.Forms.ComboBox();
            this.comboBoxUserState = new System.Windows.Forms.ComboBox();
            this.comboBoxService = new System.Windows.Forms.ComboBox();
            this.comboBoxSourceUser = new System.Windows.Forms.ComboBox();
            this.comboBoxTemplate2 = new System.Windows.Forms.ComboBox();
            this.label21 = new System.Windows.Forms.Label();
            this.label16 = new System.Windows.Forms.Label();
            this.textBoxUserEmail = new System.Windows.Forms.TextBox();
            this.label14 = new System.Windows.Forms.Label();
            this.buttonsubmitUser = new System.Windows.Forms.Button();
            this.label13 = new System.Windows.Forms.Label();
            this.numericUserBatchNum = new System.Windows.Forms.NumericUpDown();
            this.label17 = new System.Windows.Forms.Label();
            this.label18 = new System.Windows.Forms.Label();
            this.textBoxUserCity = new System.Windows.Forms.TextBox();
            this.labelUserPhoneMask = new System.Windows.Forms.Label();
            this.textBoxUserPhone = new System.Windows.Forms.TextBox();
            this.label20 = new System.Windows.Forms.Label();
            this.textBoxUserZip = new System.Windows.Forms.TextBox();
            this.label22 = new System.Windows.Forms.Label();
            this.textBoxUserEDOB = new System.Windows.Forms.TextBox();
            this.label23 = new System.Windows.Forms.Label();
            this.textBoxUserLDOB = new System.Windows.Forms.TextBox();
            this.labelUserSex = new System.Windows.Forms.Label();
            this.label25 = new System.Windows.Forms.Label();
            this.textBoxUserSSN = new System.Windows.Forms.TextBox();
            this.label26 = new System.Windows.Forms.Label();
            this.textBoxUserNameMask = new System.Windows.Forms.TextBox();
            this.label28 = new System.Windows.Forms.Label();
            this.tab3_Template = new System.Windows.Forms.TabPage();
            this.label46 = new System.Windows.Forms.Label();
            this.textBoxTemplateESAppend = new System.Windows.Forms.TextBox();
            this.label45 = new System.Windows.Forms.Label();
            this.textBoxTemplateVerifyAppnd = new System.Windows.Forms.TextBox();
            this.label24 = new System.Windows.Forms.Label();
            this.textBoxTemplateAccessApnd = new System.Windows.Forms.TextBox();
            this.labelTemplateName = new System.Windows.Forms.Label();
            this.textBoxNewTemplate = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.textBoxTemplateUserNameMask = new System.Windows.Forms.TextBox();
            this.comboBoxTemplateService = new System.Windows.Forms.ComboBox();
            this.comboBoxTemplateVeteran = new System.Windows.Forms.ComboBox();
            this.comboBoxTemplateState = new System.Windows.Forms.ComboBox();
            this.comboTemplateDFN = new System.Windows.Forms.ComboBox();
            this.comboboxTemplateEmploy = new System.Windows.Forms.ComboBox();
            this.comboBoxTemplateMarStat = new System.Windows.Forms.ComboBox();
            this.comboBoxTemplateSex = new System.Windows.Forms.ComboBox();
            this.comboBoxTemplatePntType = new System.Windows.Forms.ComboBox();
            this.comboBoxTemplate3 = new System.Windows.Forms.ComboBox();
            this.label44 = new System.Windows.Forms.Label();
            this.label27 = new System.Windows.Forms.Label();
            this.textBoxTemplateEmailMask = new System.Windows.Forms.TextBox();
            this.buttonSaveTemplate = new System.Windows.Forms.Button();
            this.label29 = new System.Windows.Forms.Label();
            this.label30 = new System.Windows.Forms.Label();
            this.label31 = new System.Windows.Forms.Label();
            this.label32 = new System.Windows.Forms.Label();
            this.label33 = new System.Windows.Forms.Label();
            this.textBoxTemplateCity = new System.Windows.Forms.TextBox();
            this.label34 = new System.Windows.Forms.Label();
            this.textBoxTemplatePHMask = new System.Windows.Forms.TextBox();
            this.label35 = new System.Windows.Forms.Label();
            this.textBoxTemplateZIPMask = new System.Windows.Forms.TextBox();
            this.label36 = new System.Windows.Forms.Label();
            this.label37 = new System.Windows.Forms.Label();
            this.textBoxTemplateUDOB = new System.Windows.Forms.TextBox();
            this.label38 = new System.Windows.Forms.Label();
            this.textBoxTemplateLDOB = new System.Windows.Forms.TextBox();
            this.label39 = new System.Windows.Forms.Label();
            this.label40 = new System.Windows.Forms.Label();
            this.textBoxTemplateSSNMask = new System.Windows.Forms.TextBox();
            this.label41 = new System.Windows.Forms.Label();
            this.label42 = new System.Windows.Forms.Label();
            this.textBoxTemplateNameMask = new System.Windows.Forms.TextBox();
            this.label43 = new System.Windows.Forms.Label();
            this.textBatchLogDisp = new System.Windows.Forms.TextBox();
            this.CreateAccessVerify = new System.Windows.Forms.CheckBox();
            this.BatchImport.SuspendLayout();
            this.tab1_Patients.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericBatchNum)).BeginInit();
            this.tab2_Users.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericUserBatchNum)).BeginInit();
            this.tab3_Template.SuspendLayout();
            this.SuspendLayout();
            // 
            // BatchImport
            // 
            this.BatchImport.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.BatchImport.Controls.Add(this.tab1_Patients);
            this.BatchImport.Controls.Add(this.tab2_Users);
            this.BatchImport.Controls.Add(this.tab3_Template);
            this.BatchImport.Location = new System.Drawing.Point(-1, 3);
            this.BatchImport.Name = "BatchImport";
            this.BatchImport.SelectedIndex = 0;
            this.BatchImport.Size = new System.Drawing.Size(797, 298);
            this.BatchImport.TabIndex = 0;
            // 
            // tab1_Patients
            // 
            this.tab1_Patients.Controls.Add(this.label19);
            this.tab1_Patients.Controls.Add(this.textBoxSourcePatient);
            this.tab1_Patients.Controls.Add(this.comboBoxState);
            this.tab1_Patients.Controls.Add(this.comboBoxMarStat);
            this.tab1_Patients.Controls.Add(this.comboBoxSex);
            this.tab1_Patients.Controls.Add(this.comboBoxEmploy);
            this.tab1_Patients.Controls.Add(this.comboBoxVeteran);
            this.tab1_Patients.Controls.Add(this.comboBoxPntDFN);
            this.tab1_Patients.Controls.Add(this.comboBoxPntType);
            this.tab1_Patients.Controls.Add(this.comboBoxTemplate1);
            this.tab1_Patients.Controls.Add(this.buttonPntSubmit);
            this.tab1_Patients.Controls.Add(this.labelNumber);
            this.tab1_Patients.Controls.Add(this.numericBatchNum);
            this.tab1_Patients.Controls.Add(this.label12);
            this.tab1_Patients.Controls.Add(this.label11);
            this.tab1_Patients.Controls.Add(this.label10);
            this.tab1_Patients.Controls.Add(this.label9);
            this.tab1_Patients.Controls.Add(this.label8);
            this.tab1_Patients.Controls.Add(this.textBoxCity);
            this.tab1_Patients.Controls.Add(this.label7);
            this.tab1_Patients.Controls.Add(this.textBoxPhone);
            this.tab1_Patients.Controls.Add(this.label6);
            this.tab1_Patients.Controls.Add(this.textBoxZip);
            this.tab1_Patients.Controls.Add(this.label5);
            this.tab1_Patients.Controls.Add(this.label4);
            this.tab1_Patients.Controls.Add(this.textBoxUDOB);
            this.tab1_Patients.Controls.Add(this.label3);
            this.tab1_Patients.Controls.Add(this.textBoxLDOB);
            this.tab1_Patients.Controls.Add(this.labelSex);
            this.tab1_Patients.Controls.Add(this.labelSSNMask);
            this.tab1_Patients.Controls.Add(this.textBoxSSNMask);
            this.tab1_Patients.Controls.Add(this.label2);
            this.tab1_Patients.Controls.Add(this.labelTypeofPnt);
            this.tab1_Patients.Controls.Add(this.textBoxNameMask);
            this.tab1_Patients.Controls.Add(this.labelTemplate);
            this.tab1_Patients.Location = new System.Drawing.Point(4, 22);
            this.tab1_Patients.Name = "tab1_Patients";
            this.tab1_Patients.Padding = new System.Windows.Forms.Padding(3);
            this.tab1_Patients.Size = new System.Drawing.Size(789, 272);
            this.tab1_Patients.TabIndex = 0;
            this.tab1_Patients.Text = "Patients";
            this.tab1_Patients.UseVisualStyleBackColor = true;
            // 
            // label19
            // 
            this.label19.AutoSize = true;
            this.label19.Enabled = false;
            this.label19.Location = new System.Drawing.Point(505, 29);
            this.label19.Name = "label19";
            this.label19.Size = new System.Drawing.Size(80, 13);
            this.label19.TabIndex = 42;
            this.label19.Text = "Source Patient:";
            this.label19.Visible = false;
            // 
            // textBoxSourcePatient
            // 
            this.textBoxSourcePatient.Enabled = false;
            this.textBoxSourcePatient.Location = new System.Drawing.Point(591, 27);
            this.textBoxSourcePatient.Name = "textBoxSourcePatient";
            this.textBoxSourcePatient.Size = new System.Drawing.Size(188, 20);
            this.textBoxSourcePatient.TabIndex = 41;
            this.textBoxSourcePatient.Visible = false;
            this.textBoxSourcePatient.TextChanged += new System.EventHandler(this.textBoxSourcePatient_TextChanged);
            // 
            // comboBoxState
            // 
            this.comboBoxState.FormattingEnabled = true;
            this.comboBoxState.Location = new System.Drawing.Point(591, 100);
            this.comboBoxState.Name = "comboBoxState";
            this.comboBoxState.Size = new System.Drawing.Size(150, 21);
            this.comboBoxState.TabIndex = 40;
            // 
            // comboBoxMarStat
            // 
            this.comboBoxMarStat.FormattingEnabled = true;
            this.comboBoxMarStat.Location = new System.Drawing.Point(359, 122);
            this.comboBoxMarStat.Name = "comboBoxMarStat";
            this.comboBoxMarStat.Size = new System.Drawing.Size(121, 21);
            this.comboBoxMarStat.TabIndex = 39;
            // 
            // comboBoxSex
            // 
            this.comboBoxSex.FormattingEnabled = true;
            this.comboBoxSex.Location = new System.Drawing.Point(134, 153);
            this.comboBoxSex.Name = "comboBoxSex";
            this.comboBoxSex.Size = new System.Drawing.Size(53, 21);
            this.comboBoxSex.TabIndex = 38;
            // 
            // comboBoxEmploy
            // 
            this.comboBoxEmploy.FormattingEnabled = true;
            this.comboBoxEmploy.Location = new System.Drawing.Point(359, 148);
            this.comboBoxEmploy.Name = "comboBoxEmploy";
            this.comboBoxEmploy.Size = new System.Drawing.Size(121, 21);
            this.comboBoxEmploy.TabIndex = 37;
            // 
            // comboBoxVeteran
            // 
            this.comboBoxVeteran.FormattingEnabled = true;
            this.comboBoxVeteran.Items.AddRange(new object[] {
            "NO",
            "YES"});
            this.comboBoxVeteran.Location = new System.Drawing.Point(591, 149);
            this.comboBoxVeteran.Name = "comboBoxVeteran";
            this.comboBoxVeteran.Size = new System.Drawing.Size(48, 21);
            this.comboBoxVeteran.TabIndex = 36;
            // 
            // comboBoxPntDFN
            // 
            this.comboBoxPntDFN.FormattingEnabled = true;
            this.comboBoxPntDFN.Items.AddRange(new object[] {
            "YES",
            "NO"});
            this.comboBoxPntDFN.Location = new System.Drawing.Point(358, 178);
            this.comboBoxPntDFN.Name = "comboBoxPntDFN";
            this.comboBoxPntDFN.Size = new System.Drawing.Size(54, 21);
            this.comboBoxPntDFN.TabIndex = 35;
            // 
            // comboBoxPntType
            // 
            this.comboBoxPntType.FormattingEnabled = true;
            this.comboBoxPntType.Location = new System.Drawing.Point(134, 73);
            this.comboBoxPntType.Name = "comboBoxPntType";
            this.comboBoxPntType.Size = new System.Drawing.Size(121, 21);
            this.comboBoxPntType.TabIndex = 34;
            // 
            // comboBoxTemplate1
            // 
            this.comboBoxTemplate1.FormattingEnabled = true;
            this.comboBoxTemplate1.Location = new System.Drawing.Point(134, 27);
            this.comboBoxTemplate1.Name = "comboBoxTemplate1";
            this.comboBoxTemplate1.Size = new System.Drawing.Size(121, 21);
            this.comboBoxTemplate1.TabIndex = 33;
            this.comboBoxTemplate1.SelectedIndexChanged += new System.EventHandler(this.comboBoxTemplate1_SelectedIndexChanged);
            // 
            // buttonPntSubmit
            // 
            this.buttonPntSubmit.Location = new System.Drawing.Point(359, 243);
            this.buttonPntSubmit.Name = "buttonPntSubmit";
            this.buttonPntSubmit.Size = new System.Drawing.Size(75, 23);
            this.buttonPntSubmit.TabIndex = 32;
            this.buttonPntSubmit.Text = "submit";
            this.buttonPntSubmit.UseVisualStyleBackColor = true;
            this.buttonPntSubmit.Click += new System.EventHandler(this.buttonPntSubmit_Click);
            // 
            // labelNumber
            // 
            this.labelNumber.AutoSize = true;
            this.labelNumber.Location = new System.Drawing.Point(301, 33);
            this.labelNumber.Name = "labelNumber";
            this.labelNumber.Size = new System.Drawing.Size(51, 13);
            this.labelNumber.TabIndex = 31;
            this.labelNumber.Text = "Create #:";
            // 
            // numericBatchNum
            // 
            this.numericBatchNum.Location = new System.Drawing.Point(358, 29);
            this.numericBatchNum.Name = "numericBatchNum";
            this.numericBatchNum.Size = new System.Drawing.Size(54, 20);
            this.numericBatchNum.TabIndex = 30;
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Location = new System.Drawing.Point(285, 151);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(67, 13);
            this.label12.TabIndex = 29;
            this.label12.Text = "Employment:";
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Location = new System.Drawing.Point(274, 181);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(78, 13);
            this.label11.TabIndex = 27;
            this.label11.Text = "DFN for Name:";
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(509, 152);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(76, 13);
            this.label10.TabIndex = 25;
            this.label10.Text = "Veteran (Y/N):";
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(550, 99);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(35, 13);
            this.label9.TabIndex = 23;
            this.label9.Text = "State:";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(558, 73);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(27, 13);
            this.label8.TabIndex = 21;
            this.label8.Text = "City:";
            // 
            // textBoxCity
            // 
            this.textBoxCity.Location = new System.Drawing.Point(591, 70);
            this.textBoxCity.Name = "textBoxCity";
            this.textBoxCity.Size = new System.Drawing.Size(150, 20);
            this.textBoxCity.TabIndex = 20;
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(58, 182);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(70, 13);
            this.label7.TabIndex = 19;
            this.label7.Text = "Phone Mask:";
            // 
            // textBoxPhone
            // 
            this.textBoxPhone.Location = new System.Drawing.Point(134, 179);
            this.textBoxPhone.MaxLength = 6;
            this.textBoxPhone.Name = "textBoxPhone";
            this.textBoxPhone.Size = new System.Drawing.Size(120, 20);
            this.textBoxPhone.TabIndex = 18;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(529, 126);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(56, 13);
            this.label6.TabIndex = 17;
            this.label6.Text = "ZIP Mask:";
            // 
            // textBoxZip
            // 
            this.textBoxZip.Location = new System.Drawing.Point(591, 123);
            this.textBoxZip.MaxLength = 5;
            this.textBoxZip.Name = "textBoxZip";
            this.textBoxZip.Size = new System.Drawing.Size(72, 20);
            this.textBoxZip.TabIndex = 16;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(278, 126);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(74, 13);
            this.label5.TabIndex = 15;
            this.label5.Text = "Marital Status:";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(291, 99);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(61, 13);
            this.label4.TabIndex = 13;
            this.label4.Text = "DOB (End):";
            // 
            // textBoxUDOB
            // 
            this.textBoxUDOB.Location = new System.Drawing.Point(358, 96);
            this.textBoxUDOB.Name = "textBoxUDOB";
            this.textBoxUDOB.Size = new System.Drawing.Size(120, 20);
            this.textBoxUDOB.TabIndex = 12;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(288, 73);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(64, 13);
            this.label3.TabIndex = 11;
            this.label3.Text = "DOB (Start):";
            // 
            // textBoxLDOB
            // 
            this.textBoxLDOB.Location = new System.Drawing.Point(358, 70);
            this.textBoxLDOB.Name = "textBoxLDOB";
            this.textBoxLDOB.Size = new System.Drawing.Size(120, 20);
            this.textBoxLDOB.TabIndex = 10;
            // 
            // labelSex
            // 
            this.labelSex.AutoSize = true;
            this.labelSex.Location = new System.Drawing.Point(99, 156);
            this.labelSex.Name = "labelSex";
            this.labelSex.Size = new System.Drawing.Size(28, 13);
            this.labelSex.TabIndex = 9;
            this.labelSex.Text = "Sex:";
            // 
            // labelSSNMask
            // 
            this.labelSSNMask.AutoSize = true;
            this.labelSSNMask.Location = new System.Drawing.Point(69, 130);
            this.labelSSNMask.Name = "labelSSNMask";
            this.labelSSNMask.Size = new System.Drawing.Size(61, 13);
            this.labelSSNMask.TabIndex = 7;
            this.labelSSNMask.Text = "SSN Mask:";
            // 
            // textBoxSSNMask
            // 
            this.textBoxSSNMask.Location = new System.Drawing.Point(134, 127);
            this.textBoxSSNMask.MaxLength = 5;
            this.textBoxSSNMask.Name = "textBoxSSNMask";
            this.textBoxSSNMask.Size = new System.Drawing.Size(120, 20);
            this.textBoxSSNMask.TabIndex = 6;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(63, 103);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(67, 13);
            this.label2.TabIndex = 5;
            this.label2.Text = "Name Mask:";
            // 
            // labelTypeofPnt
            // 
            this.labelTypeofPnt.AutoSize = true;
            this.labelTypeofPnt.Location = new System.Drawing.Point(48, 77);
            this.labelTypeofPnt.Name = "labelTypeofPnt";
            this.labelTypeofPnt.Size = new System.Drawing.Size(82, 13);
            this.labelTypeofPnt.TabIndex = 4;
            this.labelTypeofPnt.Text = "Type of Patient:";
            // 
            // textBoxNameMask
            // 
            this.textBoxNameMask.Location = new System.Drawing.Point(134, 100);
            this.textBoxNameMask.Name = "textBoxNameMask";
            this.textBoxNameMask.Size = new System.Drawing.Size(120, 20);
            this.textBoxNameMask.TabIndex = 3;
            // 
            // labelTemplate
            // 
            this.labelTemplate.AutoSize = true;
            this.labelTemplate.Location = new System.Drawing.Point(73, 33);
            this.labelTemplate.Name = "labelTemplate";
            this.labelTemplate.Size = new System.Drawing.Size(54, 13);
            this.labelTemplate.TabIndex = 1;
            this.labelTemplate.Text = "Template:";
            // 
            // tab2_Users
            // 
            this.tab2_Users.Controls.Add(this.CreateAccessVerify);
            this.tab2_Users.Controls.Add(this.label47);
            this.tab2_Users.Controls.Add(this.textBoxUserESAppend);
            this.tab2_Users.Controls.Add(this.label48);
            this.tab2_Users.Controls.Add(this.textBoxUserVerifyAppend);
            this.tab2_Users.Controls.Add(this.label49);
            this.tab2_Users.Controls.Add(this.textBoxUserAccessAppend);
            this.tab2_Users.Controls.Add(this.comboBoxUserDFN);
            this.tab2_Users.Controls.Add(this.label15);
            this.tab2_Users.Controls.Add(this.comboBoxUserSex);
            this.tab2_Users.Controls.Add(this.comboBoxUserState);
            this.tab2_Users.Controls.Add(this.comboBoxService);
            this.tab2_Users.Controls.Add(this.comboBoxSourceUser);
            this.tab2_Users.Controls.Add(this.comboBoxTemplate2);
            this.tab2_Users.Controls.Add(this.label21);
            this.tab2_Users.Controls.Add(this.label16);
            this.tab2_Users.Controls.Add(this.textBoxUserEmail);
            this.tab2_Users.Controls.Add(this.label14);
            this.tab2_Users.Controls.Add(this.buttonsubmitUser);
            this.tab2_Users.Controls.Add(this.label13);
            this.tab2_Users.Controls.Add(this.numericUserBatchNum);
            this.tab2_Users.Controls.Add(this.label17);
            this.tab2_Users.Controls.Add(this.label18);
            this.tab2_Users.Controls.Add(this.textBoxUserCity);
            this.tab2_Users.Controls.Add(this.labelUserPhoneMask);
            this.tab2_Users.Controls.Add(this.textBoxUserPhone);
            this.tab2_Users.Controls.Add(this.label20);
            this.tab2_Users.Controls.Add(this.textBoxUserZip);
            this.tab2_Users.Controls.Add(this.label22);
            this.tab2_Users.Controls.Add(this.textBoxUserEDOB);
            this.tab2_Users.Controls.Add(this.label23);
            this.tab2_Users.Controls.Add(this.textBoxUserLDOB);
            this.tab2_Users.Controls.Add(this.labelUserSex);
            this.tab2_Users.Controls.Add(this.label25);
            this.tab2_Users.Controls.Add(this.textBoxUserSSN);
            this.tab2_Users.Controls.Add(this.label26);
            this.tab2_Users.Controls.Add(this.textBoxUserNameMask);
            this.tab2_Users.Controls.Add(this.label28);
            this.tab2_Users.Location = new System.Drawing.Point(4, 22);
            this.tab2_Users.Name = "tab2_Users";
            this.tab2_Users.Padding = new System.Windows.Forms.Padding(3);
            this.tab2_Users.Size = new System.Drawing.Size(789, 272);
            this.tab2_Users.TabIndex = 1;
            this.tab2_Users.Text = "Users";
            this.tab2_Users.UseVisualStyleBackColor = true;
            this.tab2_Users.Click += new System.EventHandler(this.tab2_Users_Click);
            // 
            // label47
            // 
            this.label47.AutoSize = true;
            this.label47.Location = new System.Drawing.Point(544, 201);
            this.label47.Name = "label47";
            this.label47.Size = new System.Drawing.Size(64, 13);
            this.label47.TabIndex = 104;
            this.label47.Text = "ES Append:";
            // 
            // textBoxUserESAppend
            // 
            this.textBoxUserESAppend.Location = new System.Drawing.Point(614, 198);
            this.textBoxUserESAppend.Name = "textBoxUserESAppend";
            this.textBoxUserESAppend.Size = new System.Drawing.Size(72, 20);
            this.textBoxUserESAppend.TabIndex = 103;
            // 
            // label48
            // 
            this.label48.AutoSize = true;
            this.label48.Location = new System.Drawing.Point(532, 175);
            this.label48.Name = "label48";
            this.label48.Size = new System.Drawing.Size(76, 13);
            this.label48.TabIndex = 102;
            this.label48.Text = "Verify Append:";
            // 
            // textBoxUserVerifyAppend
            // 
            this.textBoxUserVerifyAppend.Location = new System.Drawing.Point(614, 172);
            this.textBoxUserVerifyAppend.Name = "textBoxUserVerifyAppend";
            this.textBoxUserVerifyAppend.Size = new System.Drawing.Size(72, 20);
            this.textBoxUserVerifyAppend.TabIndex = 101;
            // 
            // label49
            // 
            this.label49.AutoSize = true;
            this.label49.Location = new System.Drawing.Point(523, 149);
            this.label49.Name = "label49";
            this.label49.Size = new System.Drawing.Size(85, 13);
            this.label49.TabIndex = 100;
            this.label49.Text = "Access Append:";
            // 
            // textBoxUserAccessAppend
            // 
            this.textBoxUserAccessAppend.Location = new System.Drawing.Point(614, 146);
            this.textBoxUserAccessAppend.Name = "textBoxUserAccessAppend";
            this.textBoxUserAccessAppend.Size = new System.Drawing.Size(72, 20);
            this.textBoxUserAccessAppend.TabIndex = 99;
            // 
            // comboBoxUserDFN
            // 
            this.comboBoxUserDFN.FormattingEnabled = true;
            this.comboBoxUserDFN.Items.AddRange(new object[] {
            "YES",
            "NO"});
            this.comboBoxUserDFN.Location = new System.Drawing.Point(614, 97);
            this.comboBoxUserDFN.Name = "comboBoxUserDFN";
            this.comboBoxUserDFN.Size = new System.Drawing.Size(47, 21);
            this.comboBoxUserDFN.TabIndex = 81;
            // 
            // label15
            // 
            this.label15.AutoSize = true;
            this.label15.Location = new System.Drawing.Point(530, 100);
            this.label15.Name = "label15";
            this.label15.Size = new System.Drawing.Size(78, 13);
            this.label15.TabIndex = 80;
            this.label15.Text = "DFN for Name:";
            // 
            // comboBoxUserSex
            // 
            this.comboBoxUserSex.FormattingEnabled = true;
            this.comboBoxUserSex.Location = new System.Drawing.Point(134, 127);
            this.comboBoxUserSex.Name = "comboBoxUserSex";
            this.comboBoxUserSex.Size = new System.Drawing.Size(54, 21);
            this.comboBoxUserSex.TabIndex = 77;
            // 
            // comboBoxUserState
            // 
            this.comboBoxUserState.FormattingEnabled = true;
            this.comboBoxUserState.Location = new System.Drawing.Point(358, 150);
            this.comboBoxUserState.Name = "comboBoxUserState";
            this.comboBoxUserState.Size = new System.Drawing.Size(150, 21);
            this.comboBoxUserState.TabIndex = 76;
            // 
            // comboBoxService
            // 
            this.comboBoxService.FormattingEnabled = true;
            this.comboBoxService.Location = new System.Drawing.Point(614, 71);
            this.comboBoxService.Name = "comboBoxService";
            this.comboBoxService.Size = new System.Drawing.Size(131, 21);
            this.comboBoxService.TabIndex = 74;
            // 
            // comboBoxSourceUser
            // 
            this.comboBoxSourceUser.FormattingEnabled = true;
            this.comboBoxSourceUser.Location = new System.Drawing.Point(531, 29);
            this.comboBoxSourceUser.Name = "comboBoxSourceUser";
            this.comboBoxSourceUser.Size = new System.Drawing.Size(232, 21);
            this.comboBoxSourceUser.TabIndex = 73;
            // 
            // comboBoxTemplate2
            // 
            this.comboBoxTemplate2.FormattingEnabled = true;
            this.comboBoxTemplate2.Location = new System.Drawing.Point(134, 30);
            this.comboBoxTemplate2.Name = "comboBoxTemplate2";
            this.comboBoxTemplate2.Size = new System.Drawing.Size(121, 21);
            this.comboBoxTemplate2.TabIndex = 72;
            this.comboBoxTemplate2.SelectedIndexChanged += new System.EventHandler(this.comboBoxTemplate2_SelectedIndexChanged);
            // 
            // label21
            // 
            this.label21.AutoSize = true;
            this.label21.Location = new System.Drawing.Point(456, 34);
            this.label21.Name = "label21";
            this.label21.Size = new System.Drawing.Size(69, 13);
            this.label21.TabIndex = 71;
            this.label21.Text = "Source User:";
            // 
            // label16
            // 
            this.label16.AutoSize = true;
            this.label16.Location = new System.Drawing.Point(64, 183);
            this.label16.Name = "label16";
            this.label16.Size = new System.Drawing.Size(64, 13);
            this.label16.TabIndex = 69;
            this.label16.Text = "Email Mask:";
            // 
            // textBoxUserEmail
            // 
            this.textBoxUserEmail.Location = new System.Drawing.Point(134, 180);
            this.textBoxUserEmail.Name = "textBoxUserEmail";
            this.textBoxUserEmail.Size = new System.Drawing.Size(120, 20);
            this.textBoxUserEmail.TabIndex = 68;
            // 
            // label14
            // 
            this.label14.AutoSize = true;
            this.label14.Location = new System.Drawing.Point(562, 77);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(46, 13);
            this.label14.TabIndex = 67;
            this.label14.Text = "Service:";
            // 
            // buttonsubmitUser
            // 
            this.buttonsubmitUser.Location = new System.Drawing.Point(358, 243);
            this.buttonsubmitUser.Name = "buttonsubmitUser";
            this.buttonsubmitUser.Size = new System.Drawing.Size(75, 23);
            this.buttonsubmitUser.TabIndex = 65;
            this.buttonsubmitUser.Text = "submit";
            this.buttonsubmitUser.UseVisualStyleBackColor = true;
            this.buttonsubmitUser.Click += new System.EventHandler(this.buttonsubmitUser_Click);
            // 
            // label13
            // 
            this.label13.AutoSize = true;
            this.label13.Location = new System.Drawing.Point(301, 34);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(51, 13);
            this.label13.TabIndex = 64;
            this.label13.Text = "Create #:";
            // 
            // numericUserBatchNum
            // 
            this.numericUserBatchNum.Location = new System.Drawing.Point(358, 30);
            this.numericUserBatchNum.Name = "numericUserBatchNum";
            this.numericUserBatchNum.Size = new System.Drawing.Size(54, 20);
            this.numericUserBatchNum.TabIndex = 63;
            // 
            // label17
            // 
            this.label17.AutoSize = true;
            this.label17.Location = new System.Drawing.Point(317, 153);
            this.label17.Name = "label17";
            this.label17.Size = new System.Drawing.Size(35, 13);
            this.label17.TabIndex = 56;
            this.label17.Text = "State:";
            // 
            // label18
            // 
            this.label18.AutoSize = true;
            this.label18.Location = new System.Drawing.Point(325, 127);
            this.label18.Name = "label18";
            this.label18.Size = new System.Drawing.Size(27, 13);
            this.label18.TabIndex = 54;
            this.label18.Text = "City:";
            // 
            // textBoxUserCity
            // 
            this.textBoxUserCity.Location = new System.Drawing.Point(358, 124);
            this.textBoxUserCity.Name = "textBoxUserCity";
            this.textBoxUserCity.Size = new System.Drawing.Size(150, 20);
            this.textBoxUserCity.TabIndex = 53;
            // 
            // labelUserPhoneMask
            // 
            this.labelUserPhoneMask.AutoSize = true;
            this.labelUserPhoneMask.Location = new System.Drawing.Point(58, 157);
            this.labelUserPhoneMask.Name = "labelUserPhoneMask";
            this.labelUserPhoneMask.Size = new System.Drawing.Size(70, 13);
            this.labelUserPhoneMask.TabIndex = 52;
            this.labelUserPhoneMask.Text = "Phone Mask:";
            // 
            // textBoxUserPhone
            // 
            this.textBoxUserPhone.Location = new System.Drawing.Point(134, 154);
            this.textBoxUserPhone.Name = "textBoxUserPhone";
            this.textBoxUserPhone.Size = new System.Drawing.Size(120, 20);
            this.textBoxUserPhone.TabIndex = 51;
            // 
            // label20
            // 
            this.label20.AutoSize = true;
            this.label20.Location = new System.Drawing.Point(296, 180);
            this.label20.Name = "label20";
            this.label20.Size = new System.Drawing.Size(56, 13);
            this.label20.TabIndex = 50;
            this.label20.Text = "ZIP Mask:";
            // 
            // textBoxUserZip
            // 
            this.textBoxUserZip.Location = new System.Drawing.Point(358, 177);
            this.textBoxUserZip.Name = "textBoxUserZip";
            this.textBoxUserZip.Size = new System.Drawing.Size(72, 20);
            this.textBoxUserZip.TabIndex = 49;
            // 
            // label22
            // 
            this.label22.AutoSize = true;
            this.label22.Location = new System.Drawing.Point(291, 100);
            this.label22.Name = "label22";
            this.label22.Size = new System.Drawing.Size(61, 13);
            this.label22.TabIndex = 46;
            this.label22.Text = "DOB (End):";
            // 
            // textBoxUserEDOB
            // 
            this.textBoxUserEDOB.Location = new System.Drawing.Point(358, 97);
            this.textBoxUserEDOB.Name = "textBoxUserEDOB";
            this.textBoxUserEDOB.Size = new System.Drawing.Size(120, 20);
            this.textBoxUserEDOB.TabIndex = 45;
            // 
            // label23
            // 
            this.label23.AutoSize = true;
            this.label23.Location = new System.Drawing.Point(288, 74);
            this.label23.Name = "label23";
            this.label23.Size = new System.Drawing.Size(64, 13);
            this.label23.TabIndex = 44;
            this.label23.Text = "DOB (Start):";
            // 
            // textBoxUserLDOB
            // 
            this.textBoxUserLDOB.Location = new System.Drawing.Point(358, 71);
            this.textBoxUserLDOB.Name = "textBoxUserLDOB";
            this.textBoxUserLDOB.Size = new System.Drawing.Size(120, 20);
            this.textBoxUserLDOB.TabIndex = 43;
            // 
            // labelUserSex
            // 
            this.labelUserSex.AutoSize = true;
            this.labelUserSex.Location = new System.Drawing.Point(99, 127);
            this.labelUserSex.Name = "labelUserSex";
            this.labelUserSex.Size = new System.Drawing.Size(28, 13);
            this.labelUserSex.TabIndex = 42;
            this.labelUserSex.Text = "Sex:";
            // 
            // label25
            // 
            this.label25.AutoSize = true;
            this.label25.Location = new System.Drawing.Point(67, 101);
            this.label25.Name = "label25";
            this.label25.Size = new System.Drawing.Size(61, 13);
            this.label25.TabIndex = 40;
            this.label25.Text = "SSN Mask:";
            // 
            // textBoxUserSSN
            // 
            this.textBoxUserSSN.Location = new System.Drawing.Point(134, 98);
            this.textBoxUserSSN.Name = "textBoxUserSSN";
            this.textBoxUserSSN.Size = new System.Drawing.Size(120, 20);
            this.textBoxUserSSN.TabIndex = 39;
            // 
            // label26
            // 
            this.label26.AutoSize = true;
            this.label26.Location = new System.Drawing.Point(61, 74);
            this.label26.Name = "label26";
            this.label26.Size = new System.Drawing.Size(67, 13);
            this.label26.TabIndex = 38;
            this.label26.Text = "Name Mask:";
            // 
            // textBoxUserNameMask
            // 
            this.textBoxUserNameMask.Location = new System.Drawing.Point(134, 71);
            this.textBoxUserNameMask.Name = "textBoxUserNameMask";
            this.textBoxUserNameMask.Size = new System.Drawing.Size(120, 20);
            this.textBoxUserNameMask.TabIndex = 36;
            // 
            // label28
            // 
            this.label28.AutoSize = true;
            this.label28.Location = new System.Drawing.Point(73, 34);
            this.label28.Name = "label28";
            this.label28.Size = new System.Drawing.Size(54, 13);
            this.label28.TabIndex = 34;
            this.label28.Text = "Template:";
            // 
            // tab3_Template
            // 
            this.tab3_Template.Controls.Add(this.label46);
            this.tab3_Template.Controls.Add(this.textBoxTemplateESAppend);
            this.tab3_Template.Controls.Add(this.label45);
            this.tab3_Template.Controls.Add(this.textBoxTemplateVerifyAppnd);
            this.tab3_Template.Controls.Add(this.label24);
            this.tab3_Template.Controls.Add(this.textBoxTemplateAccessApnd);
            this.tab3_Template.Controls.Add(this.labelTemplateName);
            this.tab3_Template.Controls.Add(this.textBoxNewTemplate);
            this.tab3_Template.Controls.Add(this.label1);
            this.tab3_Template.Controls.Add(this.textBoxTemplateUserNameMask);
            this.tab3_Template.Controls.Add(this.comboBoxTemplateService);
            this.tab3_Template.Controls.Add(this.comboBoxTemplateVeteran);
            this.tab3_Template.Controls.Add(this.comboBoxTemplateState);
            this.tab3_Template.Controls.Add(this.comboTemplateDFN);
            this.tab3_Template.Controls.Add(this.comboboxTemplateEmploy);
            this.tab3_Template.Controls.Add(this.comboBoxTemplateMarStat);
            this.tab3_Template.Controls.Add(this.comboBoxTemplateSex);
            this.tab3_Template.Controls.Add(this.comboBoxTemplatePntType);
            this.tab3_Template.Controls.Add(this.comboBoxTemplate3);
            this.tab3_Template.Controls.Add(this.label44);
            this.tab3_Template.Controls.Add(this.label27);
            this.tab3_Template.Controls.Add(this.textBoxTemplateEmailMask);
            this.tab3_Template.Controls.Add(this.buttonSaveTemplate);
            this.tab3_Template.Controls.Add(this.label29);
            this.tab3_Template.Controls.Add(this.label30);
            this.tab3_Template.Controls.Add(this.label31);
            this.tab3_Template.Controls.Add(this.label32);
            this.tab3_Template.Controls.Add(this.label33);
            this.tab3_Template.Controls.Add(this.textBoxTemplateCity);
            this.tab3_Template.Controls.Add(this.label34);
            this.tab3_Template.Controls.Add(this.textBoxTemplatePHMask);
            this.tab3_Template.Controls.Add(this.label35);
            this.tab3_Template.Controls.Add(this.textBoxTemplateZIPMask);
            this.tab3_Template.Controls.Add(this.label36);
            this.tab3_Template.Controls.Add(this.label37);
            this.tab3_Template.Controls.Add(this.textBoxTemplateUDOB);
            this.tab3_Template.Controls.Add(this.label38);
            this.tab3_Template.Controls.Add(this.textBoxTemplateLDOB);
            this.tab3_Template.Controls.Add(this.label39);
            this.tab3_Template.Controls.Add(this.label40);
            this.tab3_Template.Controls.Add(this.textBoxTemplateSSNMask);
            this.tab3_Template.Controls.Add(this.label41);
            this.tab3_Template.Controls.Add(this.label42);
            this.tab3_Template.Controls.Add(this.textBoxTemplateNameMask);
            this.tab3_Template.Controls.Add(this.label43);
            this.tab3_Template.Location = new System.Drawing.Point(4, 22);
            this.tab3_Template.Name = "tab3_Template";
            this.tab3_Template.Padding = new System.Windows.Forms.Padding(3);
            this.tab3_Template.Size = new System.Drawing.Size(789, 272);
            this.tab3_Template.TabIndex = 2;
            this.tab3_Template.Text = "Template";
            this.tab3_Template.UseVisualStyleBackColor = true;
            // 
            // label46
            // 
            this.label46.AutoSize = true;
            this.label46.Location = new System.Drawing.Point(524, 224);
            this.label46.Name = "label46";
            this.label46.Size = new System.Drawing.Size(64, 13);
            this.label46.TabIndex = 92;
            this.label46.Text = "ES Append:";
            // 
            // textBoxTemplateESAppend
            // 
            this.textBoxTemplateESAppend.Location = new System.Drawing.Point(594, 221);
            this.textBoxTemplateESAppend.Name = "textBoxTemplateESAppend";
            this.textBoxTemplateESAppend.Size = new System.Drawing.Size(72, 20);
            this.textBoxTemplateESAppend.TabIndex = 91;
            // 
            // label45
            // 
            this.label45.AutoSize = true;
            this.label45.Location = new System.Drawing.Point(512, 198);
            this.label45.Name = "label45";
            this.label45.Size = new System.Drawing.Size(76, 13);
            this.label45.TabIndex = 90;
            this.label45.Text = "Verify Append:";
            // 
            // textBoxTemplateVerifyAppnd
            // 
            this.textBoxTemplateVerifyAppnd.Location = new System.Drawing.Point(594, 195);
            this.textBoxTemplateVerifyAppnd.Name = "textBoxTemplateVerifyAppnd";
            this.textBoxTemplateVerifyAppnd.Size = new System.Drawing.Size(72, 20);
            this.textBoxTemplateVerifyAppnd.TabIndex = 89;
            // 
            // label24
            // 
            this.label24.AutoSize = true;
            this.label24.Location = new System.Drawing.Point(503, 172);
            this.label24.Name = "label24";
            this.label24.Size = new System.Drawing.Size(85, 13);
            this.label24.TabIndex = 88;
            this.label24.Text = "Access Append:";
            // 
            // textBoxTemplateAccessApnd
            // 
            this.textBoxTemplateAccessApnd.Location = new System.Drawing.Point(594, 169);
            this.textBoxTemplateAccessApnd.Name = "textBoxTemplateAccessApnd";
            this.textBoxTemplateAccessApnd.Size = new System.Drawing.Size(72, 20);
            this.textBoxTemplateAccessApnd.TabIndex = 87;
            // 
            // labelTemplateName
            // 
            this.labelTemplateName.AutoSize = true;
            this.labelTemplateName.Enabled = false;
            this.labelTemplateName.Location = new System.Drawing.Point(265, 24);
            this.labelTemplateName.Name = "labelTemplateName";
            this.labelTemplateName.Size = new System.Drawing.Size(85, 13);
            this.labelTemplateName.TabIndex = 86;
            this.labelTemplateName.Text = "Template Name:";
            this.labelTemplateName.Visible = false;
            // 
            // textBoxNewTemplate
            // 
            this.textBoxNewTemplate.Enabled = false;
            this.textBoxNewTemplate.Location = new System.Drawing.Point(357, 21);
            this.textBoxNewTemplate.Name = "textBoxNewTemplate";
            this.textBoxNewTemplate.Size = new System.Drawing.Size(123, 20);
            this.textBoxNewTemplate.TabIndex = 85;
            this.textBoxNewTemplate.Visible = false;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(265, 58);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(92, 13);
            this.label1.TabIndex = 84;
            this.label1.Text = "User Name Mask:";
            // 
            // textBoxTemplateUserNameMask
            // 
            this.textBoxTemplateUserNameMask.Location = new System.Drawing.Point(357, 55);
            this.textBoxTemplateUserNameMask.Name = "textBoxTemplateUserNameMask";
            this.textBoxTemplateUserNameMask.Size = new System.Drawing.Size(123, 20);
            this.textBoxTemplateUserNameMask.TabIndex = 83;
            // 
            // comboBoxTemplateService
            // 
            this.comboBoxTemplateService.FormattingEnabled = true;
            this.comboBoxTemplateService.Location = new System.Drawing.Point(594, 139);
            this.comboBoxTemplateService.Name = "comboBoxTemplateService";
            this.comboBoxTemplateService.Size = new System.Drawing.Size(150, 21);
            this.comboBoxTemplateService.TabIndex = 82;
            // 
            // comboBoxTemplateVeteran
            // 
            this.comboBoxTemplateVeteran.FormattingEnabled = true;
            this.comboBoxTemplateVeteran.Items.AddRange(new object[] {
            "YES",
            "NO"});
            this.comboBoxTemplateVeteran.Location = new System.Drawing.Point(594, 112);
            this.comboBoxTemplateVeteran.Name = "comboBoxTemplateVeteran";
            this.comboBoxTemplateVeteran.Size = new System.Drawing.Size(42, 21);
            this.comboBoxTemplateVeteran.TabIndex = 81;
            // 
            // comboBoxTemplateState
            // 
            this.comboBoxTemplateState.FormattingEnabled = true;
            this.comboBoxTemplateState.Location = new System.Drawing.Point(594, 52);
            this.comboBoxTemplateState.Name = "comboBoxTemplateState";
            this.comboBoxTemplateState.Size = new System.Drawing.Size(150, 21);
            this.comboBoxTemplateState.TabIndex = 80;
            // 
            // comboTemplateDFN
            // 
            this.comboTemplateDFN.FormattingEnabled = true;
            this.comboTemplateDFN.Items.AddRange(new object[] {
            "YES",
            "NO"});
            this.comboTemplateDFN.Location = new System.Drawing.Point(356, 201);
            this.comboTemplateDFN.Name = "comboTemplateDFN";
            this.comboTemplateDFN.Size = new System.Drawing.Size(47, 21);
            this.comboTemplateDFN.TabIndex = 79;
            // 
            // comboboxTemplateEmploy
            // 
            this.comboboxTemplateEmploy.FormattingEnabled = true;
            this.comboboxTemplateEmploy.Location = new System.Drawing.Point(357, 174);
            this.comboboxTemplateEmploy.Name = "comboboxTemplateEmploy";
            this.comboboxTemplateEmploy.Size = new System.Drawing.Size(121, 21);
            this.comboboxTemplateEmploy.TabIndex = 78;
            // 
            // comboBoxTemplateMarStat
            // 
            this.comboBoxTemplateMarStat.FormattingEnabled = true;
            this.comboBoxTemplateMarStat.Location = new System.Drawing.Point(356, 144);
            this.comboBoxTemplateMarStat.Name = "comboBoxTemplateMarStat";
            this.comboBoxTemplateMarStat.Size = new System.Drawing.Size(121, 21);
            this.comboBoxTemplateMarStat.TabIndex = 77;
            // 
            // comboBoxTemplateSex
            // 
            this.comboBoxTemplateSex.FormattingEnabled = true;
            this.comboBoxTemplateSex.Location = new System.Drawing.Point(135, 146);
            this.comboBoxTemplateSex.Name = "comboBoxTemplateSex";
            this.comboBoxTemplateSex.Size = new System.Drawing.Size(50, 21);
            this.comboBoxTemplateSex.TabIndex = 76;
            // 
            // comboBoxTemplatePntType
            // 
            this.comboBoxTemplatePntType.FormattingEnabled = true;
            this.comboBoxTemplatePntType.Location = new System.Drawing.Point(135, 55);
            this.comboBoxTemplatePntType.Name = "comboBoxTemplatePntType";
            this.comboBoxTemplatePntType.Size = new System.Drawing.Size(121, 21);
            this.comboBoxTemplatePntType.TabIndex = 75;
            // 
            // comboBoxTemplate3
            // 
            this.comboBoxTemplate3.FormattingEnabled = true;
            this.comboBoxTemplate3.Location = new System.Drawing.Point(134, 21);
            this.comboBoxTemplate3.Name = "comboBoxTemplate3";
            this.comboBoxTemplate3.Size = new System.Drawing.Size(121, 21);
            this.comboBoxTemplate3.TabIndex = 74;
            this.comboBoxTemplate3.SelectedIndexChanged += new System.EventHandler(this.comboBoxTemplate3_SelectedIndexChanged);
            // 
            // label44
            // 
            this.label44.AutoSize = true;
            this.label44.Location = new System.Drawing.Point(542, 142);
            this.label44.Name = "label44";
            this.label44.Size = new System.Drawing.Size(46, 13);
            this.label44.TabIndex = 73;
            this.label44.Text = "Service:";
            // 
            // label27
            // 
            this.label27.AutoSize = true;
            this.label27.Location = new System.Drawing.Point(68, 208);
            this.label27.Name = "label27";
            this.label27.Size = new System.Drawing.Size(64, 13);
            this.label27.TabIndex = 71;
            this.label27.Text = "Email Mask:";
            // 
            // textBoxTemplateEmailMask
            // 
            this.textBoxTemplateEmailMask.Location = new System.Drawing.Point(135, 205);
            this.textBoxTemplateEmailMask.Name = "textBoxTemplateEmailMask";
            this.textBoxTemplateEmailMask.Size = new System.Drawing.Size(123, 20);
            this.textBoxTemplateEmailMask.TabIndex = 70;
            // 
            // buttonSaveTemplate
            // 
            this.buttonSaveTemplate.Location = new System.Drawing.Point(356, 246);
            this.buttonSaveTemplate.Name = "buttonSaveTemplate";
            this.buttonSaveTemplate.Size = new System.Drawing.Size(75, 23);
            this.buttonSaveTemplate.TabIndex = 65;
            this.buttonSaveTemplate.Text = "save";
            this.buttonSaveTemplate.UseVisualStyleBackColor = true;
            this.buttonSaveTemplate.Click += new System.EventHandler(this.buttonSaveTemplate_Click);
            // 
            // label29
            // 
            this.label29.AutoSize = true;
            this.label29.Location = new System.Drawing.Point(284, 177);
            this.label29.Name = "label29";
            this.label29.Size = new System.Drawing.Size(67, 13);
            this.label29.TabIndex = 62;
            this.label29.Text = "Employment:";
            // 
            // label30
            // 
            this.label30.AutoSize = true;
            this.label30.Location = new System.Drawing.Point(272, 204);
            this.label30.Name = "label30";
            this.label30.Size = new System.Drawing.Size(78, 13);
            this.label30.TabIndex = 60;
            this.label30.Text = "DFN for Name:";
            // 
            // label31
            // 
            this.label31.AutoSize = true;
            this.label31.Location = new System.Drawing.Point(512, 115);
            this.label31.Name = "label31";
            this.label31.Size = new System.Drawing.Size(76, 13);
            this.label31.TabIndex = 58;
            this.label31.Text = "Veteran (Y/N):";
            // 
            // label32
            // 
            this.label32.AutoSize = true;
            this.label32.Location = new System.Drawing.Point(553, 56);
            this.label32.Name = "label32";
            this.label32.Size = new System.Drawing.Size(35, 13);
            this.label32.TabIndex = 56;
            this.label32.Text = "State:";
            // 
            // label33
            // 
            this.label33.AutoSize = true;
            this.label33.Location = new System.Drawing.Point(561, 24);
            this.label33.Name = "label33";
            this.label33.Size = new System.Drawing.Size(27, 13);
            this.label33.TabIndex = 54;
            this.label33.Text = "City:";
            // 
            // textBoxTemplateCity
            // 
            this.textBoxTemplateCity.Location = new System.Drawing.Point(594, 21);
            this.textBoxTemplateCity.Name = "textBoxTemplateCity";
            this.textBoxTemplateCity.Size = new System.Drawing.Size(150, 20);
            this.textBoxTemplateCity.TabIndex = 53;
            // 
            // label34
            // 
            this.label34.AutoSize = true;
            this.label34.Location = new System.Drawing.Point(60, 172);
            this.label34.Name = "label34";
            this.label34.Size = new System.Drawing.Size(70, 13);
            this.label34.TabIndex = 52;
            this.label34.Text = "Phone Mask:";
            // 
            // textBoxTemplatePHMask
            // 
            this.textBoxTemplatePHMask.Location = new System.Drawing.Point(135, 173);
            this.textBoxTemplatePHMask.Name = "textBoxTemplatePHMask";
            this.textBoxTemplatePHMask.Size = new System.Drawing.Size(120, 20);
            this.textBoxTemplatePHMask.TabIndex = 51;
            // 
            // label35
            // 
            this.label35.AutoSize = true;
            this.label35.Location = new System.Drawing.Point(532, 86);
            this.label35.Name = "label35";
            this.label35.Size = new System.Drawing.Size(56, 13);
            this.label35.TabIndex = 50;
            this.label35.Text = "ZIP Mask:";
            // 
            // textBoxTemplateZIPMask
            // 
            this.textBoxTemplateZIPMask.Location = new System.Drawing.Point(594, 83);
            this.textBoxTemplateZIPMask.Name = "textBoxTemplateZIPMask";
            this.textBoxTemplateZIPMask.Size = new System.Drawing.Size(72, 20);
            this.textBoxTemplateZIPMask.TabIndex = 49;
            // 
            // label36
            // 
            this.label36.AutoSize = true;
            this.label36.Location = new System.Drawing.Point(277, 148);
            this.label36.Name = "label36";
            this.label36.Size = new System.Drawing.Size(74, 13);
            this.label36.TabIndex = 48;
            this.label36.Text = "Marital Status:";
            // 
            // label37
            // 
            this.label37.AutoSize = true;
            this.label37.Location = new System.Drawing.Point(290, 117);
            this.label37.Name = "label37";
            this.label37.Size = new System.Drawing.Size(61, 13);
            this.label37.TabIndex = 46;
            this.label37.Text = "DOB (End):";
            // 
            // textBoxTemplateUDOB
            // 
            this.textBoxTemplateUDOB.Location = new System.Drawing.Point(357, 114);
            this.textBoxTemplateUDOB.Name = "textBoxTemplateUDOB";
            this.textBoxTemplateUDOB.Size = new System.Drawing.Size(120, 20);
            this.textBoxTemplateUDOB.TabIndex = 45;
            // 
            // label38
            // 
            this.label38.AutoSize = true;
            this.label38.Location = new System.Drawing.Point(290, 86);
            this.label38.Name = "label38";
            this.label38.Size = new System.Drawing.Size(64, 13);
            this.label38.TabIndex = 44;
            this.label38.Text = "DOB (Start):";
            // 
            // textBoxTemplateLDOB
            // 
            this.textBoxTemplateLDOB.Location = new System.Drawing.Point(357, 83);
            this.textBoxTemplateLDOB.Name = "textBoxTemplateLDOB";
            this.textBoxTemplateLDOB.Size = new System.Drawing.Size(120, 20);
            this.textBoxTemplateLDOB.TabIndex = 43;
            // 
            // label39
            // 
            this.label39.AutoSize = true;
            this.label39.Location = new System.Drawing.Point(101, 149);
            this.label39.Name = "label39";
            this.label39.Size = new System.Drawing.Size(28, 13);
            this.label39.TabIndex = 42;
            this.label39.Text = "Sex:";
            // 
            // label40
            // 
            this.label40.AutoSize = true;
            this.label40.Location = new System.Drawing.Point(71, 120);
            this.label40.Name = "label40";
            this.label40.Size = new System.Drawing.Size(61, 13);
            this.label40.TabIndex = 40;
            this.label40.Text = "SSN Mask:";
            // 
            // textBoxTemplateSSNMask
            // 
            this.textBoxTemplateSSNMask.Location = new System.Drawing.Point(138, 117);
            this.textBoxTemplateSSNMask.Name = "textBoxTemplateSSNMask";
            this.textBoxTemplateSSNMask.Size = new System.Drawing.Size(120, 20);
            this.textBoxTemplateSSNMask.TabIndex = 39;
            // 
            // label41
            // 
            this.label41.AutoSize = true;
            this.label41.Location = new System.Drawing.Point(43, 90);
            this.label41.Name = "label41";
            this.label41.Size = new System.Drawing.Size(86, 13);
            this.label41.TabIndex = 38;
            this.label41.Text = "Pnt Name Mask:";
            // 
            // label42
            // 
            this.label42.AutoSize = true;
            this.label42.Location = new System.Drawing.Point(50, 58);
            this.label42.Name = "label42";
            this.label42.Size = new System.Drawing.Size(82, 13);
            this.label42.TabIndex = 37;
            this.label42.Text = "Type of Patient:";
            // 
            // textBoxTemplateNameMask
            // 
            this.textBoxTemplateNameMask.Location = new System.Drawing.Point(135, 87);
            this.textBoxTemplateNameMask.Name = "textBoxTemplateNameMask";
            this.textBoxTemplateNameMask.Size = new System.Drawing.Size(123, 20);
            this.textBoxTemplateNameMask.TabIndex = 36;
            // 
            // label43
            // 
            this.label43.AutoSize = true;
            this.label43.Location = new System.Drawing.Point(73, 24);
            this.label43.Name = "label43";
            this.label43.Size = new System.Drawing.Size(54, 13);
            this.label43.TabIndex = 34;
            this.label43.Text = "Template:";
            // 
            // textBatchLogDisp
            // 
            this.textBatchLogDisp.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.textBatchLogDisp.BackColor = System.Drawing.SystemColors.ActiveCaption;
            this.textBatchLogDisp.Location = new System.Drawing.Point(-1, 307);
            this.textBatchLogDisp.Multiline = true;
            this.textBatchLogDisp.Name = "textBatchLogDisp";
            this.textBatchLogDisp.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.textBatchLogDisp.Size = new System.Drawing.Size(797, 80);
            this.textBatchLogDisp.TabIndex = 3;
            // 
            // CreateAccessVerify
            // 
            this.CreateAccessVerify.AutoSize = true;
            this.CreateAccessVerify.Location = new System.Drawing.Point(614, 124);
            this.CreateAccessVerify.Name = "CreateAccessVerify";
            this.CreateAccessVerify.Size = new System.Drawing.Size(164, 17);
            this.CreateAccessVerify.TabIndex = 105;
            this.CreateAccessVerify.Text = "Auto Generate Access/Verify";
            this.CreateAccessVerify.UseVisualStyleBackColor = true;
            // 
            // Form_BatchUtil
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(794, 386);
            this.Controls.Add(this.textBatchLogDisp);
            this.Controls.Add(this.BatchImport);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "Form_BatchUtil";
            this.Text = "Batch Import Utility";
            this.BatchImport.ResumeLayout(false);
            this.tab1_Patients.ResumeLayout(false);
            this.tab1_Patients.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericBatchNum)).EndInit();
            this.tab2_Users.ResumeLayout(false);
            this.tab2_Users.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericUserBatchNum)).EndInit();
            this.tab3_Template.ResumeLayout(false);
            this.tab3_Template.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TabControl BatchImport;
        private System.Windows.Forms.TabPage tab1_Patients;
        private System.Windows.Forms.TabPage tab2_Users;
        private System.Windows.Forms.TabPage tab3_Template;
        private System.Windows.Forms.TextBox textBatchLogDisp;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label labelSex;
        private System.Windows.Forms.Label labelSSNMask;
        private System.Windows.Forms.TextBox textBoxSSNMask;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label labelTypeofPnt;
        private System.Windows.Forms.TextBox textBoxNameMask;
        private System.Windows.Forms.Label labelTemplate;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.TextBox textBoxCity;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.TextBox textBoxPhone;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.TextBox textBoxZip;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Button buttonPntSubmit;
        private System.Windows.Forms.Label labelNumber;
        private System.Windows.Forms.NumericUpDown numericBatchNum;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.Label label21;
        private System.Windows.Forms.Label label16;
        private System.Windows.Forms.TextBox textBoxUserEmail;
        private System.Windows.Forms.Label label14;
        private System.Windows.Forms.Button buttonsubmitUser;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.NumericUpDown numericUserBatchNum;
        private System.Windows.Forms.Label label17;
        private System.Windows.Forms.Label label18;
        private System.Windows.Forms.TextBox textBoxUserCity;
        private System.Windows.Forms.Label labelUserPhoneMask;
        private System.Windows.Forms.TextBox textBoxUserPhone;
        private System.Windows.Forms.Label label20;
        private System.Windows.Forms.TextBox textBoxUserZip;
        private System.Windows.Forms.Label label22;
        private System.Windows.Forms.TextBox textBoxUserEDOB;
        private System.Windows.Forms.Label label23;
        private System.Windows.Forms.TextBox textBoxUserLDOB;
        private System.Windows.Forms.Label labelUserSex;
        private System.Windows.Forms.Label label25;
        private System.Windows.Forms.TextBox textBoxUserSSN;
        private System.Windows.Forms.Label label26;
        private System.Windows.Forms.TextBox textBoxUserNameMask;
        private System.Windows.Forms.Label label28;
        private System.Windows.Forms.Label label44;
        private System.Windows.Forms.Label label27;
        private System.Windows.Forms.TextBox textBoxTemplateEmailMask;
        private System.Windows.Forms.Button buttonSaveTemplate;
        private System.Windows.Forms.Label label29;
        private System.Windows.Forms.Label label30;
        private System.Windows.Forms.Label label31;
        private System.Windows.Forms.Label label32;
        private System.Windows.Forms.Label label33;
        private System.Windows.Forms.TextBox textBoxTemplateCity;
        private System.Windows.Forms.Label label34;
        private System.Windows.Forms.TextBox textBoxTemplatePHMask;
        private System.Windows.Forms.Label label35;
        private System.Windows.Forms.TextBox textBoxTemplateZIPMask;
        private System.Windows.Forms.Label label36;
        private System.Windows.Forms.Label label37;
        private System.Windows.Forms.TextBox textBoxTemplateUDOB;
        private System.Windows.Forms.Label label38;
        private System.Windows.Forms.TextBox textBoxTemplateLDOB;
        private System.Windows.Forms.Label label39;
        private System.Windows.Forms.Label label40;
        private System.Windows.Forms.TextBox textBoxTemplateSSNMask;
        private System.Windows.Forms.Label label41;
        private System.Windows.Forms.Label label42;
        private System.Windows.Forms.TextBox textBoxTemplateNameMask;
        private System.Windows.Forms.Label label43;
        private System.Windows.Forms.ComboBox comboBoxTemplate1;
        private System.Windows.Forms.ComboBox comboBoxTemplate2;
        private System.Windows.Forms.ComboBox comboBoxTemplate3;
        private System.Windows.Forms.ComboBox comboBoxSex;
        private System.Windows.Forms.ComboBox comboBoxEmploy;
        private System.Windows.Forms.ComboBox comboBoxVeteran;
        private System.Windows.Forms.ComboBox comboBoxPntDFN;
        private System.Windows.Forms.ComboBox comboBoxPntType;
        private System.Windows.Forms.TextBox textBoxUDOB;
        private System.Windows.Forms.TextBox textBoxLDOB;
        private System.Windows.Forms.ComboBox comboBoxState;
        private System.Windows.Forms.ComboBox comboBoxMarStat;
        private System.Windows.Forms.ComboBox comboBoxUserSex;
        private System.Windows.Forms.ComboBox comboBoxUserState;
        private System.Windows.Forms.ComboBox comboBoxService;
        private System.Windows.Forms.ComboBox comboBoxSourceUser;
        private System.Windows.Forms.ComboBox comboBoxTemplateService;
        private System.Windows.Forms.ComboBox comboBoxTemplateVeteran;
        private System.Windows.Forms.ComboBox comboBoxTemplateState;
        private System.Windows.Forms.ComboBox comboTemplateDFN;
        private System.Windows.Forms.ComboBox comboboxTemplateEmploy;
        private System.Windows.Forms.ComboBox comboBoxTemplateMarStat;
        private System.Windows.Forms.ComboBox comboBoxTemplateSex;
        private System.Windows.Forms.ComboBox comboBoxTemplatePntType;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox textBoxTemplateUserNameMask;
        private System.Windows.Forms.ComboBox comboBoxUserDFN;
        private System.Windows.Forms.Label label15;
        private System.Windows.Forms.Label labelTemplateName;
        private System.Windows.Forms.TextBox textBoxNewTemplate;
        private System.Windows.Forms.Label label19;
        private System.Windows.Forms.TextBox textBoxSourcePatient;
        private System.Windows.Forms.Label label45;
        private System.Windows.Forms.TextBox textBoxTemplateVerifyAppnd;
        private System.Windows.Forms.Label label24;
        private System.Windows.Forms.TextBox textBoxTemplateAccessApnd;
        private System.Windows.Forms.Label label46;
        private System.Windows.Forms.TextBox textBoxTemplateESAppend;
        private System.Windows.Forms.Label label47;
        private System.Windows.Forms.TextBox textBoxUserESAppend;
        private System.Windows.Forms.Label label48;
        private System.Windows.Forms.TextBox textBoxUserVerifyAppend;
        private System.Windows.Forms.Label label49;
        private System.Windows.Forms.TextBox textBoxUserAccessAppend;
        private System.Windows.Forms.CheckBox CreateAccessVerify;
    }
}