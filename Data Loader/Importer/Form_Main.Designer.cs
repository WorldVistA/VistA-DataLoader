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
    partial class Form_Main
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form_Main));
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.fileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.loginToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.connectToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.disconnectToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.settingsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.configureToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.excellToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.viewToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.clearToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.exitToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.exportTablesToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.allergenToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.booleenToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.consultToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItemCpt = new System.Windows.Forms.ToolStripMenuItem();
            this.druglistToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.employStatusToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.ethnicityToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItemExam = new System.Windows.Forms.ToolStripMenuItem();
            this.genderToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItemHFactor = new System.Windows.Forms.ToolStripMenuItem();
            this.imagLoc791ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItemImmz = new System.Windows.Forms.ToolStripMenuItem();
            this.insuranceToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.labtestToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.locationToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.noteToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.personToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.problemstatusToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.problemTypeToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem2 = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItemProvNarr = new System.Windows.Forms.ToolStripMenuItem();
            this.raceToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.radProc71ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.siglistToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.symptomToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.vitaltypeToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.iCD9SearchToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.batchImportUtilityToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.helpToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.aboutToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.label1 = new System.Windows.Forms.Label();
            this.sheetlistBox1 = new System.Windows.Forms.ListBox();
            this.label2 = new System.Windows.Forms.Label();
            this.import = new System.Windows.Forms.Button();
            this.conn_status = new System.Windows.Forms.Label();
            this.conn_info = new System.Windows.Forms.Label();
            this.toolStripMenuItemEdTopic = new System.Windows.Forms.ToolStripMenuItem();
            this.menuStrip1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.SuspendLayout();
            // 
            // menuStrip1
            // 
            this.menuStrip1.BackColor = System.Drawing.SystemColors.GradientActiveCaption;
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fileToolStripMenuItem,
            this.toolsToolStripMenuItem,
            this.helpToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(910, 24);
            this.menuStrip1.TabIndex = 5;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // fileToolStripMenuItem
            // 
            this.fileToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.loginToolStripMenuItem,
            this.configureToolStripMenuItem,
            this.toolStripMenuItem1,
            this.exitToolStripMenuItem});
            this.fileToolStripMenuItem.Name = "fileToolStripMenuItem";
            this.fileToolStripMenuItem.Size = new System.Drawing.Size(37, 20);
            this.fileToolStripMenuItem.Text = "File";
            // 
            // loginToolStripMenuItem
            // 
            this.loginToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.connectToolStripMenuItem,
            this.disconnectToolStripMenuItem1,
            this.settingsToolStripMenuItem});
            this.loginToolStripMenuItem.Name = "loginToolStripMenuItem";
            this.loginToolStripMenuItem.Size = new System.Drawing.Size(134, 22);
            this.loginToolStripMenuItem.Text = "VistA Login";
            this.loginToolStripMenuItem.Click += new System.EventHandler(this.loginToolStripMenuItem_Click);
            // 
            // connectToolStripMenuItem
            // 
            this.connectToolStripMenuItem.Name = "connectToolStripMenuItem";
            this.connectToolStripMenuItem.Size = new System.Drawing.Size(133, 22);
            this.connectToolStripMenuItem.Text = "Connect";
            this.connectToolStripMenuItem.Click += new System.EventHandler(this.connectToolStripMenuItem_Click);
            // 
            // disconnectToolStripMenuItem1
            // 
            this.disconnectToolStripMenuItem1.Enabled = false;
            this.disconnectToolStripMenuItem1.Name = "disconnectToolStripMenuItem1";
            this.disconnectToolStripMenuItem1.Size = new System.Drawing.Size(133, 22);
            this.disconnectToolStripMenuItem1.Text = "Disconnect";
            this.disconnectToolStripMenuItem1.Click += new System.EventHandler(this.disconnectToolStripMenuItem1_Click);
            // 
            // settingsToolStripMenuItem
            // 
            this.settingsToolStripMenuItem.Name = "settingsToolStripMenuItem";
            this.settingsToolStripMenuItem.Size = new System.Drawing.Size(133, 22);
            this.settingsToolStripMenuItem.Text = "Settings";
            this.settingsToolStripMenuItem.Click += new System.EventHandler(this.settingsToolStripMenuItem_Click);
            // 
            // configureToolStripMenuItem
            // 
            this.configureToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.excellToolStripMenuItem});
            this.configureToolStripMenuItem.Name = "configureToolStripMenuItem";
            this.configureToolStripMenuItem.Size = new System.Drawing.Size(134, 22);
            this.configureToolStripMenuItem.Text = "Load Data";
            // 
            // excellToolStripMenuItem
            // 
            this.excellToolStripMenuItem.Name = "excellToolStripMenuItem";
            this.excellToolStripMenuItem.Size = new System.Drawing.Size(92, 22);
            this.excellToolStripMenuItem.Text = "File";
            this.excellToolStripMenuItem.Click += new System.EventHandler(this.excellToolStripMenuItem_Click);
            // 
            // toolStripMenuItem1
            // 
            this.toolStripMenuItem1.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.viewToolStripMenuItem,
            this.clearToolStripMenuItem});
            this.toolStripMenuItem1.Name = "toolStripMenuItem1";
            this.toolStripMenuItem1.Size = new System.Drawing.Size(134, 22);
            this.toolStripMenuItem1.Text = "Log";
            // 
            // viewToolStripMenuItem
            // 
            this.viewToolStripMenuItem.Name = "viewToolStripMenuItem";
            this.viewToolStripMenuItem.Size = new System.Drawing.Size(101, 22);
            this.viewToolStripMenuItem.Text = "View";
            this.viewToolStripMenuItem.Click += new System.EventHandler(this.viewToolStripMenuItem_Click);
            // 
            // clearToolStripMenuItem
            // 
            this.clearToolStripMenuItem.Name = "clearToolStripMenuItem";
            this.clearToolStripMenuItem.Size = new System.Drawing.Size(101, 22);
            this.clearToolStripMenuItem.Text = "Clear";
            this.clearToolStripMenuItem.Click += new System.EventHandler(this.clearToolStripMenuItem_Click);
            // 
            // exitToolStripMenuItem
            // 
            this.exitToolStripMenuItem.Name = "exitToolStripMenuItem";
            this.exitToolStripMenuItem.Size = new System.Drawing.Size(134, 22);
            this.exitToolStripMenuItem.Text = "Quit";
            this.exitToolStripMenuItem.Click += new System.EventHandler(this.exitToolStripMenuItem_Click);
            // 
            // toolsToolStripMenuItem
            // 
            this.toolsToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.exportTablesToolStripMenuItem,
            this.iCD9SearchToolStripMenuItem,
            this.batchImportUtilityToolStripMenuItem});
            this.toolsToolStripMenuItem.Name = "toolsToolStripMenuItem";
            this.toolsToolStripMenuItem.Size = new System.Drawing.Size(48, 20);
            this.toolsToolStripMenuItem.Text = "Tools";
            // 
            // exportTablesToolStripMenuItem
            // 
            this.exportTablesToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.allergenToolStripMenuItem,
            this.booleenToolStripMenuItem,
            this.consultToolStripMenuItem,
            this.toolStripMenuItemCpt,
            this.toolStripMenuItemEdTopic,
            this.druglistToolStripMenuItem,
            this.employStatusToolStripMenuItem,
            this.ethnicityToolStripMenuItem,
            this.toolStripMenuItemExam,
            this.genderToolStripMenuItem,
            this.toolStripMenuItemHFactor,
            this.imagLoc791ToolStripMenuItem,
            this.toolStripMenuItemImmz,
            this.insuranceToolStripMenuItem,
            this.labtestToolStripMenuItem,
            this.locationToolStripMenuItem,
            this.noteToolStripMenuItem,
            this.personToolStripMenuItem,
            this.problemstatusToolStripMenuItem,
            this.problemTypeToolStripMenuItem,
            this.toolStripMenuItem2,
            this.toolStripMenuItemProvNarr,
            this.raceToolStripMenuItem,
            this.radProc71ToolStripMenuItem,
            this.siglistToolStripMenuItem,
            this.symptomToolStripMenuItem,
            this.vitaltypeToolStripMenuItem});
            this.exportTablesToolStripMenuItem.Enabled = false;
            this.exportTablesToolStripMenuItem.Name = "exportTablesToolStripMenuItem";
            this.exportTablesToolStripMenuItem.Size = new System.Drawing.Size(177, 22);
            this.exportTablesToolStripMenuItem.Text = "Export VistA Tables";
            this.exportTablesToolStripMenuItem.Click += new System.EventHandler(this.exportTablesToolStripMenuItem_Click);
            // 
            // allergenToolStripMenuItem
            // 
            this.allergenToolStripMenuItem.Name = "allergenToolStripMenuItem";
            this.allergenToolStripMenuItem.Size = new System.Drawing.Size(244, 22);
            this.allergenToolStripMenuItem.Text = "Allergen (#120.82)";
            this.allergenToolStripMenuItem.Click += new System.EventHandler(this.allergenToolStripMenuItem_Click);
            // 
            // booleenToolStripMenuItem
            // 
            this.booleenToolStripMenuItem.Name = "booleenToolStripMenuItem";
            this.booleenToolStripMenuItem.Size = new System.Drawing.Size(244, 22);
            this.booleenToolStripMenuItem.Text = "Boolean";
            this.booleenToolStripMenuItem.Click += new System.EventHandler(this.booleenToolStripMenuItem_Click);
            // 
            // consultToolStripMenuItem
            // 
            this.consultToolStripMenuItem.Name = "consultToolStripMenuItem";
            this.consultToolStripMenuItem.Size = new System.Drawing.Size(244, 22);
            this.consultToolStripMenuItem.Text = "Consult (#123.5)";
            this.consultToolStripMenuItem.Click += new System.EventHandler(this.consultToolStripMenuItem_Click);
            // 
            // toolStripMenuItemCpt
            // 
            this.toolStripMenuItemCpt.Name = "toolStripMenuItemCpt";
            this.toolStripMenuItemCpt.Size = new System.Drawing.Size(244, 22);
            this.toolStripMenuItemCpt.Text = "CPT (#81)";
            this.toolStripMenuItemCpt.Click += new System.EventHandler(this.toolStripMenuItemCpt_Click);
            // 
            // druglistToolStripMenuItem
            // 
            this.druglistToolStripMenuItem.Name = "druglistToolStripMenuItem";
            this.druglistToolStripMenuItem.Size = new System.Drawing.Size(244, 22);
            this.druglistToolStripMenuItem.Text = "Drug_list (#50)";
            this.druglistToolStripMenuItem.Click += new System.EventHandler(this.druglistToolStripMenuItem_Click);
            // 
            // employStatusToolStripMenuItem
            // 
            this.employStatusToolStripMenuItem.Name = "employStatusToolStripMenuItem";
            this.employStatusToolStripMenuItem.Size = new System.Drawing.Size(244, 22);
            this.employStatusToolStripMenuItem.Text = "Employ_Status (#2, .31115)";
            this.employStatusToolStripMenuItem.Click += new System.EventHandler(this.employStatusToolStripMenuItem_Click);
            // 
            // ethnicityToolStripMenuItem
            // 
            this.ethnicityToolStripMenuItem.Name = "ethnicityToolStripMenuItem";
            this.ethnicityToolStripMenuItem.Size = new System.Drawing.Size(244, 22);
            this.ethnicityToolStripMenuItem.Text = "Ethnicity (#10.2)";
            this.ethnicityToolStripMenuItem.Click += new System.EventHandler(this.ethnicityToolStripMenuItem_Click);
            // 
            // toolStripMenuItemExam
            // 
            this.toolStripMenuItemExam.Name = "toolStripMenuItemExam";
            this.toolStripMenuItemExam.Size = new System.Drawing.Size(244, 22);
            this.toolStripMenuItemExam.Text = "Exam (#9999999.15)";
            this.toolStripMenuItemExam.Click += new System.EventHandler(this.toolStripMenuItemExam_Click);
            // 
            // genderToolStripMenuItem
            // 
            this.genderToolStripMenuItem.Name = "genderToolStripMenuItem";
            this.genderToolStripMenuItem.Size = new System.Drawing.Size(244, 22);
            this.genderToolStripMenuItem.Text = "Gender (#2, .02)";
            this.genderToolStripMenuItem.Click += new System.EventHandler(this.genderToolStripMenuItem_Click);
            // 
            // toolStripMenuItemHFactor
            // 
            this.toolStripMenuItemHFactor.Name = "toolStripMenuItemHFactor";
            this.toolStripMenuItemHFactor.Size = new System.Drawing.Size(244, 22);
            this.toolStripMenuItemHFactor.Text = "Health Factor (#9999999.64)";
            this.toolStripMenuItemHFactor.Click += new System.EventHandler(this.toolStripMenuItemHFactor_Click);
            // 
            // imagLoc791ToolStripMenuItem
            // 
            this.imagLoc791ToolStripMenuItem.Name = "imagLoc791ToolStripMenuItem";
            this.imagLoc791ToolStripMenuItem.Size = new System.Drawing.Size(244, 22);
            this.imagLoc791ToolStripMenuItem.Text = "Imag_Loc (#79.1)";
            this.imagLoc791ToolStripMenuItem.Click += new System.EventHandler(this.imagLoc791ToolStripMenuItem_Click);
            // 
            // toolStripMenuItemImmz
            // 
            this.toolStripMenuItemImmz.Name = "toolStripMenuItemImmz";
            this.toolStripMenuItemImmz.Size = new System.Drawing.Size(244, 22);
            this.toolStripMenuItemImmz.Text = "Immunization (#9999999.14)";
            this.toolStripMenuItemImmz.Click += new System.EventHandler(this.toolStripMenuItemImmz_Click);
            // 
            // insuranceToolStripMenuItem
            // 
            this.insuranceToolStripMenuItem.Name = "insuranceToolStripMenuItem";
            this.insuranceToolStripMenuItem.Size = new System.Drawing.Size(244, 22);
            this.insuranceToolStripMenuItem.Text = "Insurance (#36)";
            this.insuranceToolStripMenuItem.Click += new System.EventHandler(this.insuranceToolStripMenuItem_Click);
            // 
            // labtestToolStripMenuItem
            // 
            this.labtestToolStripMenuItem.Name = "labtestToolStripMenuItem";
            this.labtestToolStripMenuItem.Size = new System.Drawing.Size(244, 22);
            this.labtestToolStripMenuItem.Text = "Lab_test (#60)";
            this.labtestToolStripMenuItem.Click += new System.EventHandler(this.labtestToolStripMenuItem_Click);
            // 
            // locationToolStripMenuItem
            // 
            this.locationToolStripMenuItem.Name = "locationToolStripMenuItem";
            this.locationToolStripMenuItem.Size = new System.Drawing.Size(244, 22);
            this.locationToolStripMenuItem.Text = "Location (#44)";
            this.locationToolStripMenuItem.Click += new System.EventHandler(this.locationToolStripMenuItem_Click);
            // 
            // noteToolStripMenuItem
            // 
            this.noteToolStripMenuItem.Name = "noteToolStripMenuItem";
            this.noteToolStripMenuItem.Size = new System.Drawing.Size(244, 22);
            this.noteToolStripMenuItem.Text = "Note_title (#8925.1)";
            this.noteToolStripMenuItem.Click += new System.EventHandler(this.noteToolStripMenuItem_Click);
            // 
            // personToolStripMenuItem
            // 
            this.personToolStripMenuItem.Name = "personToolStripMenuItem";
            this.personToolStripMenuItem.Size = new System.Drawing.Size(244, 22);
            this.personToolStripMenuItem.Text = "Person (#200)";
            this.personToolStripMenuItem.Click += new System.EventHandler(this.personToolStripMenuItem_Click);
            // 
            // problemstatusToolStripMenuItem
            // 
            this.problemstatusToolStripMenuItem.Name = "problemstatusToolStripMenuItem";
            this.problemstatusToolStripMenuItem.Size = new System.Drawing.Size(244, 22);
            this.problemstatusToolStripMenuItem.Text = "Problem_status (#9000011,.12)";
            this.problemstatusToolStripMenuItem.Click += new System.EventHandler(this.problemstatusToolStripMenuItem_Click);
            // 
            // problemTypeToolStripMenuItem
            // 
            this.problemTypeToolStripMenuItem.Name = "problemTypeToolStripMenuItem";
            this.problemTypeToolStripMenuItem.Size = new System.Drawing.Size(244, 22);
            this.problemTypeToolStripMenuItem.Text = "Problem_Type (#9000011,1.14)";
            this.problemTypeToolStripMenuItem.Click += new System.EventHandler(this.problemTypeToolStripMenuItem_Click);
            // 
            // toolStripMenuItem2
            // 
            this.toolStripMenuItem2.Name = "toolStripMenuItem2";
            this.toolStripMenuItem2.Size = new System.Drawing.Size(244, 22);
            this.toolStripMenuItem2.Text = "Provider (#200)";
            this.toolStripMenuItem2.Click += new System.EventHandler(this.toolStripMenuItem2_Click);
            // 
            // toolStripMenuItemProvNarr
            // 
            this.toolStripMenuItemProvNarr.Name = "toolStripMenuItemProvNarr";
            this.toolStripMenuItemProvNarr.Size = new System.Drawing.Size(244, 22);
            this.toolStripMenuItemProvNarr.Text = "Provider Narrative (#9999999.27)";
            this.toolStripMenuItemProvNarr.Click += new System.EventHandler(this.toolStripMenuItemProvNarr_Click);
            // 
            // raceToolStripMenuItem
            // 
            this.raceToolStripMenuItem.Name = "raceToolStripMenuItem";
            this.raceToolStripMenuItem.Size = new System.Drawing.Size(244, 22);
            this.raceToolStripMenuItem.Text = "Race (#10)";
            this.raceToolStripMenuItem.Click += new System.EventHandler(this.raceToolStripMenuItem_Click);
            // 
            // radProc71ToolStripMenuItem
            // 
            this.radProc71ToolStripMenuItem.Name = "radProc71ToolStripMenuItem";
            this.radProc71ToolStripMenuItem.Size = new System.Drawing.Size(244, 22);
            this.radProc71ToolStripMenuItem.Text = "Rad_Proc (#71)";
            this.radProc71ToolStripMenuItem.Click += new System.EventHandler(this.radProc71ToolStripMenuItem_Click);
            // 
            // siglistToolStripMenuItem
            // 
            this.siglistToolStripMenuItem.Name = "siglistToolStripMenuItem";
            this.siglistToolStripMenuItem.Size = new System.Drawing.Size(244, 22);
            this.siglistToolStripMenuItem.Text = "siglist (#51)";
            this.siglistToolStripMenuItem.Click += new System.EventHandler(this.siglistToolStripMenuItem_Click);
            // 
            // symptomToolStripMenuItem
            // 
            this.symptomToolStripMenuItem.Name = "symptomToolStripMenuItem";
            this.symptomToolStripMenuItem.Size = new System.Drawing.Size(244, 22);
            this.symptomToolStripMenuItem.Text = "Symptom (#120.83)";
            this.symptomToolStripMenuItem.Click += new System.EventHandler(this.symptomToolStripMenuItem_Click);
            // 
            // vitaltypeToolStripMenuItem
            // 
            this.vitaltypeToolStripMenuItem.Name = "vitaltypeToolStripMenuItem";
            this.vitaltypeToolStripMenuItem.Size = new System.Drawing.Size(244, 22);
            this.vitaltypeToolStripMenuItem.Text = "Vital_type (#120.51)";
            this.vitaltypeToolStripMenuItem.Click += new System.EventHandler(this.vitaltypeToolStripMenuItem_Click);
            // 
            // iCD9SearchToolStripMenuItem
            // 
            this.iCD9SearchToolStripMenuItem.Enabled = false;
            this.iCD9SearchToolStripMenuItem.Name = "iCD9SearchToolStripMenuItem";
            this.iCD9SearchToolStripMenuItem.Size = new System.Drawing.Size(177, 22);
            this.iCD9SearchToolStripMenuItem.Text = "ICD9 Name Search";
            this.iCD9SearchToolStripMenuItem.Click += new System.EventHandler(this.iCD9SearchToolStripMenuItem_Click);
            // 
            // batchImportUtilityToolStripMenuItem
            // 
            this.batchImportUtilityToolStripMenuItem.Enabled = false;
            this.batchImportUtilityToolStripMenuItem.Name = "batchImportUtilityToolStripMenuItem";
            this.batchImportUtilityToolStripMenuItem.Size = new System.Drawing.Size(177, 22);
            this.batchImportUtilityToolStripMenuItem.Text = "Batch Import Utility";
            this.batchImportUtilityToolStripMenuItem.Click += new System.EventHandler(this.batchImportUtilityToolStripMenuItem_Click);
            // 
            // helpToolStripMenuItem
            // 
            this.helpToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.aboutToolStripMenuItem});
            this.helpToolStripMenuItem.Name = "helpToolStripMenuItem";
            this.helpToolStripMenuItem.Size = new System.Drawing.Size(44, 20);
            this.helpToolStripMenuItem.Text = "Help";
            // 
            // aboutToolStripMenuItem
            // 
            this.aboutToolStripMenuItem.Name = "aboutToolStripMenuItem";
            this.aboutToolStripMenuItem.Size = new System.Drawing.Size(107, 22);
            this.aboutToolStripMenuItem.Text = "About";
            this.aboutToolStripMenuItem.Click += new System.EventHandler(this.aboutToolStripMenuItem_Click);
            // 
            // dataGridView1
            // 
            this.dataGridView1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.dataGridView1.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.AllCells;
            this.dataGridView1.AutoSizeRowsMode = System.Windows.Forms.DataGridViewAutoSizeRowsMode.AllCells;
            this.dataGridView1.Location = new System.Drawing.Point(23, 50);
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.RowHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.None;
            this.dataGridView1.RowHeadersVisible = false;
            this.dataGridView1.Size = new System.Drawing.Size(862, 318);
            this.dataGridView1.TabIndex = 6;
            this.dataGridView1.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridView1_CellContentClick);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(20, 34);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(114, 13);
            this.label1.TabIndex = 7;
            this.label1.Text = "Staged data for import:";
            this.label1.Click += new System.EventHandler(this.label1_Click_1);
            // 
            // sheetlistBox1
            // 
            this.sheetlistBox1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.sheetlistBox1.Enabled = false;
            this.sheetlistBox1.FormattingEnabled = true;
            this.sheetlistBox1.Location = new System.Drawing.Point(135, 374);
            this.sheetlistBox1.Name = "sheetlistBox1";
            this.sheetlistBox1.Size = new System.Drawing.Size(120, 43);
            this.sheetlistBox1.Sorted = true;
            this.sheetlistBox1.TabIndex = 8;
            this.sheetlistBox1.SelectedIndexChanged += new System.EventHandler(this.sheetlistBox1_SelectedIndexChanged);
            // 
            // label2
            // 
            this.label2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(34, 374);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(95, 13);
            this.label2.TabIndex = 9;
            this.label2.Text = "Select Worksheet:";
            this.label2.Click += new System.EventHandler(this.label2_Click_1);
            // 
            // import
            // 
            this.import.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.import.Enabled = false;
            this.import.Location = new System.Drawing.Point(498, 374);
            this.import.Name = "import";
            this.import.Size = new System.Drawing.Size(75, 23);
            this.import.TabIndex = 10;
            this.import.Text = "submit";
            this.import.UseVisualStyleBackColor = true;
            this.import.Click += new System.EventHandler(this.import_Click);
            // 
            // conn_status
            // 
            this.conn_status.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.conn_status.AutoSize = true;
            this.conn_status.BackColor = System.Drawing.SystemColors.Control;
            this.conn_status.Font = new System.Drawing.Font("Arial Narrow", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.conn_status.ForeColor = System.Drawing.Color.Red;
            this.conn_status.Location = new System.Drawing.Point(787, 374);
            this.conn_status.Name = "conn_status";
            this.conn_status.Size = new System.Drawing.Size(80, 16);
            this.conn_status.TabIndex = 11;
            this.conn_status.Text = "disconnected";
            // 
            // conn_info
            // 
            this.conn_info.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.conn_info.AutoSize = true;
            this.conn_info.Location = new System.Drawing.Point(787, 390);
            this.conn_info.Name = "conn_info";
            this.conn_info.Size = new System.Drawing.Size(0, 13);
            this.conn_info.TabIndex = 12;
            // 
            // toolStripMenuItemEdTopic
            // 
            this.toolStripMenuItemEdTopic.Name = "toolStripMenuItemEdTopic";
            this.toolStripMenuItemEdTopic.Size = new System.Drawing.Size(244, 22);
            this.toolStripMenuItemEdTopic.Text = "Education_Topic (#9999999.09)";
            this.toolStripMenuItemEdTopic.Click += new System.EventHandler(this.toolStripMenuItemEdTopic_Click);
            // 
            // Form_Main
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.Control;
            this.ClientSize = new System.Drawing.Size(910, 426);
            this.Controls.Add(this.conn_info);
            this.Controls.Add(this.conn_status);
            this.Controls.Add(this.import);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.sheetlistBox1);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.dataGridView1);
            this.Controls.Add(this.menuStrip1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "Form_Main";
            this.Text = "VistA Data Loader Tool";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        //private System.Windows.Forms.TextBox server;
        //private System.Windows.Forms.TextBox port;
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem fileToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem loginToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem configureToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem exitToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem connectToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem disconnectToolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem settingsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem excellToolStripMenuItem;
        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ListBox sheetlistBox1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button import;
        private System.Windows.Forms.Label conn_status;
        private System.Windows.Forms.Label conn_info;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem viewToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem clearToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem toolsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem exportTablesToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem genderToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem booleenToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem raceToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem ethnicityToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem employStatusToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem insuranceToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem locationToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem personToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem problemstatusToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem problemTypeToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem vitaltypeToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem allergenToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem symptomToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem labtestToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem noteToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem helpToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem druglistToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem siglistToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem aboutToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem consultToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem imagLoc791ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem radProc71ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem2;
        private System.Windows.Forms.ToolStripMenuItem iCD9SearchToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem batchImportUtilityToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItemHFactor;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItemCpt;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItemProvNarr;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItemImmz;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItemExam;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItemEdTopic;
    }
}

