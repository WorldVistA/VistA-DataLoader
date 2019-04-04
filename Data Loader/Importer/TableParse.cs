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
using System.Linq;
using System.Text;
using System.Data;
using DataLoader.Common.Model;

namespace DataLoader
{
    class TableParse
    {
        public List<string> ParsePatientRPCData(DataTable table, DataRow row)
        {
            bool check = false;
            bool flag = false;
            string name = "";
            Column column = new Column();
            PntMapping pntmap = new PntMapping();
            List<string> patient = new List<string>();
            patient.Add("TEMPLATE^DEFAULT");
            patient.Add("IMP_TYPE^I");
            // check column definitions
            int colcnt = 0;
            foreach (DataColumn col in table.Columns)
            {
                colcnt++;
                check = column.PatientColumnArraySet(col.ToString(), colcnt);
            }
            //build RPC list
            colcnt = 0;
            foreach (DataColumn col in table.Columns)
            {
                colcnt++;
                check = column.ColumnCheck(colcnt);
                if (check == true)
                {
                    string param = pntmap.ExcellMapping(column.ColumnArray[colcnt]);
                    if (param != "skip")
                    {
                        string stuff = row[col].ToString();
                        stuff = stuff.Replace("\r", string.Empty).Replace("\n", string.Empty).Trim();
                        if (param == "CASE" && stuff.Length >0)
                        {
                            Case.CaseName = stuff;
                            flag = true;
                            stuff = "";
                        }
                        if (stuff.Length>0)
                        {
                            flag = true;
                            if (param == "NAME")
                            {
                                if (column.ColumnArray[colcnt] == "Last name" && stuff.Length > 0)
                                {
                                    name = stuff;
                                }
                                if (column.ColumnArray[colcnt] == "First name" && stuff.Length > 0)
                                {
                                    name = name + "," + stuff;
                                    if (name != null)
                                        patient.Add("NAME^" + name);
                                }
                            }
                            else
                            {
                                patient.Add(param + "^" + stuff);
                            }
                        }
                    }
                }
            }
            if (flag == false)
            {
                patient.Clear();
                patient.Add("-1");
            }
            return patient;
        }

        public List<string> ParseApptRPCData(DataTable table, DataRow row, PatientList pntlist)
        {
            bool check = false;
            Column column = new Column();
            ApptMapping apptmap = new ApptMapping();
            List<string> appt = new List<string>();
            int colcnt = 0;
            string ssn = "";
            foreach (DataColumn col in table.Columns)
            {
                colcnt++;
                check = column.ApptColumnArraySet(col.ToString(), colcnt);
            }
            //build RPC list
            colcnt = 0;
            bool flag = false;
            foreach (DataColumn col in table.Columns)
            {
                
                colcnt++;
                check = column.ColumnCheck(colcnt);
                if (check == true)
                {
                    string param = apptmap.ExcellMapping(column.ColumnArray[colcnt]);
                    if (param == "CASE")
                    {
                        string stuff = row[col].ToString();
                        stuff = stuff.Replace("\r", string.Empty).Replace("\n", string.Empty).Trim();
                        foreach (Patient pnt in pntlist)
                        {
                            if (pnt.CaseName == stuff && stuff.Length>0)
                            {
                                appt.Add("PATIENT^" + pnt.SSN);
                                ssn = pnt.SSN;
                                flag = true;
                                break;
                            }
                        }
                        param = "skip";
                    }
                    if (param == "PATIENT")
                    {
                        string stuff = row[col].ToString();
                        stuff = stuff.Replace("\r", string.Empty).Replace("\n", string.Empty).Trim();
                        if (stuff.Length == 9)
                            flag = true;
                        if (stuff.Length != 9)
                        {
                            if (ssn != "") row[col] = ssn;
                        }
                    }
                    if (param != "skip")
                    {
                        string stuff = row[col].ToString();
                        if (stuff != null && stuff.Length > 0)
                        {
                            stuff = stuff.Replace("\r", string.Empty).Replace("\n", string.Empty).Trim();
                            //if (param == "ADATE")
                            //if (stuff.IndexOf(" ") != -1)
                            //stuff = stuff.Substring(0, (stuff.IndexOf(" ")));
                            appt.Add(param + "^" + stuff);
                        }
                    }
                }
            }
            if (flag == false)
            {
                appt.Clear();
                appt.Add("-1");
            }
            return appt;
        }

        public List<string> ParseOPEncounterRPCData(DataTable table, DataRow row, PatientList pntlist)
        {
            bool check = false;
            Column column = new Column();
            ApptMapping apptmap = new ApptMapping();
            List<string> appt = new List<string>();
            int colcnt = 0;
            string ssn = "";
            foreach (DataColumn col in table.Columns)
            {
                colcnt++;
                string colname = col.ToString();
                switch (colname)
                {
                    case "Location":
                        colname = "Appt clinic";
                        break;
                    case "Begin DateTime":
                        colname = "Appt date";
                        break;
                    case "End DateTime":
                        colname = "Check out date";
                        break;
                    default:
                        break;
                }
                check = column.ApptColumnArraySet(colname, colcnt);
                if (colname == "Type of Encounter")
                {
                    string item = row[col].ToString();
                    if (item.Length > 2) item = item.Substring(0, 2); //only count first two chars
                    if (item != "OP" && item != "ER" && item !="PS")
                    {
                        appt.Clear();
                        appt.Add("-1");
                        return appt;
                    }
                }
            }
            //build RPC list
            colcnt = 0;
            bool flag = false;
            foreach (DataColumn col in table.Columns)
            {

                colcnt++;
                check = column.ColumnCheck(colcnt);
                if (check == true)
                {
                    string param = apptmap.ExcellMapping(column.ColumnArray[colcnt]);
                    if (param == "CASE")
                    {
                        string stuff = row[col].ToString();
                        stuff = stuff.Replace("\r", string.Empty).Replace("\n", string.Empty).Trim();
                        foreach (Patient pnt in pntlist)
                        {
                            if (pnt.CaseName == stuff && stuff.Length > 0)
                            {
                                appt.Add("PATIENT^" + pnt.SSN);
                                ssn = pnt.SSN;
                                flag = true;
                                break;
                            }
                        }
                        param = "skip";
                    }
                    if (param == "PATIENT")
                    {
                        string stuff = row[col].ToString();
                        stuff = stuff.Replace("\r", string.Empty).Replace("\n", string.Empty).Trim();
                        if (stuff.Length == 9)
                            flag = true;
                        if (stuff.Length != 9)
                        {
                            if (ssn != "") row[col] = ssn;
                        }
                    }
                    if (param != "skip")
                    {
                        string stuff = row[col].ToString();
                        if (stuff != null && stuff.Length > 0)
                        {
                            stuff = stuff.Replace("\r", string.Empty).Replace("\n", string.Empty).Trim();
                            //if (param == "ADATE")
                            //if (stuff.IndexOf(" ") != -1)
                            //stuff = stuff.Substring(0, (stuff.IndexOf(" ")));
                            appt.Add(param + "^" + stuff);
                        }
                    }
                }
            }
            if (flag == false)
            {
                appt.Clear();
                appt.Add("-1");
            }
            return appt;
        }

        public List<string> ParseIPEncounterRPCData(DataTable table, DataRow row, PatientList pntlist)
        {
            bool check = false;
            Column column = new Column();
            EncounterMapping encountermap = new EncounterMapping();
            List<string> admit = new List<string>();
            int colcnt = 0;
            string ssn = "";
            foreach (DataColumn col in table.Columns)
            {
                colcnt++;
                string colname = col.ToString();
                //switch (colname)
                //{
                //    case "Location":
                //        colname = "Ward";
                //        break;
                //    default:
                //        break;
                //}
                check = column.AdmitColumnArraySet(colname, colcnt);
                if (colname == "Type of Encounter")
                {
                    string item = row[col].ToString();
                    if (item.Length > 2) item = item.Substring(0, 2);
                    if (item != "IP")
                    {
                        admit.Clear();
                        admit.Add("-1");
                        return admit;
                    }
                }
            }
            //build RPC list
            colcnt = 0;
            bool flag = false;
            foreach (DataColumn col in table.Columns)
            {
                colcnt++;
                check = column.ColumnCheck(colcnt);
                if (check == true)
                {
                    string param = encountermap.ExcellMapping(column.ColumnArray[colcnt]);
                    if (param == "CASE")
                    {
                        string stuff = row[col].ToString();
                        stuff = stuff.Replace("\r", string.Empty).Replace("\n", string.Empty).Trim();
                        foreach (Patient pnt in pntlist)
                        {
                            if (pnt.CaseName == stuff && stuff.Length > 0)
                            {
                                admit.Add("PATIENT^" + pnt.SSN);
                                ssn = pnt.SSN;
                                flag = true;
                                break;
                            }
                        }
                        param = "skip";
                    }
                    if (param == "PATIENT" || param == "PAT_SSN")
                    {
                        string stuff = row[col].ToString();
                        stuff = stuff.Replace("\r", string.Empty).Replace("\n", string.Empty).Trim();
                        if (stuff.Length == 9)
                            flag = true;
                        if (stuff.Length != 9)
                        {
                            if (ssn != "") row[col] = ssn;
                        }
                    }
                    if (param != "skip")
                    {
                        string stuff = row[col].ToString();
                        if (stuff != null && stuff.Length > 0)
                        {
                            stuff = stuff.Replace("\r", string.Empty).Replace("\n", string.Empty).Trim();
                            if (param == "LOCATION") param = "WARD";
                            admit.Add(param + "^" + stuff);
                        }
                    }
                }
            }
            if (flag == false)
            {
                admit.Clear();
                admit.Add("-1");
            }
            return admit;
        }

        public List<string> ParseProbRPCData(DataTable table, DataRow row, PatientList pntlist)
        {
            bool check = false;
            Column column = new Column();
            ProblemMapping probmap = new ProblemMapping();
            List<string> prob = new List<string>();
            int colcnt = 0;
            string ssn = "";
            foreach (DataColumn col in table.Columns)
            {
                colcnt++;
                check = column.ProbColumnArrarySet(col.ToString(), colcnt);
            }
            //build RPC list
            colcnt = 0;
            bool flag = false;
            foreach (DataColumn col in table.Columns)
            {
                colcnt++;
                check = column.ColumnCheck(colcnt);
                if (check == true)
                {
                    string param = probmap.ExcellMapping(column.ColumnArray[colcnt]);
                    if (param == "CASE")
                    {
                        string stuff = row[col].ToString();
                        stuff = stuff.Replace("\r", string.Empty).Replace("\n", string.Empty).Trim();
                        foreach (Patient pnt in pntlist)
                        {
                            if (pnt.CaseName == stuff && stuff.Length > 0)
                            {
                                prob.Add("PAT_SSN^" + pnt.SSN);
                                ssn = pnt.SSN;
                                flag = true;
                                break;
                            }
                        }
                        param = "skip";
                    }
                    if (param == "PAT_SSN")
                    {
                        string stuff = row[col].ToString();
                        stuff = stuff.Replace("\r", string.Empty).Replace("\n", string.Empty).Trim();
                        if (stuff.Length == 9)
                            flag = true;
                        if (stuff.Length != 9)
                        {
                            if (ssn != "") row[col] = ssn;
                        }
                    }
                    //
                    if (param != "skip")
                    {
                        string stuff = row[col].ToString();
                        if (stuff != null && stuff.Length > 0)
                        {
                            stuff = stuff.Replace("\r", string.Empty).Replace("\n", string.Empty).Trim();
                            prob.Add(param + "^" + stuff);
                        }
                    }
                }
            }
            if (flag == false)
            {
                prob.Clear();
                prob.Add("-1");
            }
            return prob;
        }

        public List<string> ParseVitalRPCData(DataTable table, DataRow row, PatientList pntlist)
        {
            bool check = false;
            Column column = new Column();
            VitalMapping vitalmap = new VitalMapping();
            List<string> vital = new List<string>();
            string rate = "";
            int colcnt = 0;
            string ssn = "";
            foreach (DataColumn col in table.Columns)
            {
                colcnt++;
                check = column.VitalColumnArraySet(col.ToString(), colcnt);
            }
            //build RPC list
            colcnt = 0;
            bool flag = false;
            foreach (DataColumn col in table.Columns)
            {
                colcnt++;
                check = column.ColumnCheck(colcnt);
                if (check == true)
                {
                    string param = vitalmap.ExcellMapping(column.ColumnArray[colcnt]);
                    if (param == "CASE")
                    {
                        string stuff = row[col].ToString();
                        stuff = stuff.Replace("\r", string.Empty).Replace("\n", string.Empty).Trim();
                        foreach (Patient pnt in pntlist)
                        {
                            if (pnt.CaseName == stuff && stuff.Length > 0)
                            {
                                vital.Add("PAT_SSN^" + pnt.SSN);
                                ssn = pnt.SSN;
                                flag = true;
                                break;
                            }
                        }
                        param = "skip";
                    }
                    if (param == "PAT_SSN")
                    {
                        string stuff = row[col].ToString();
                        stuff = stuff.Replace("\r", string.Empty).Replace("\n", string.Empty).Trim();
                        if (stuff.Length == 9)
                            flag = true;
                        if (stuff.Length != 9)
                        {
                            if (ssn != "") row[col] = ssn;
                        }
                    }
                    if (param == "RATE")
                    {
                        string stuff = row[col].ToString();
                        stuff = stuff.Replace("\r", string.Empty).Replace("\n", string.Empty).Trim();
                        if (stuff != null)
                        {
                            if (column.ColumnArray[colcnt] == "Rate 1" && stuff.Length > 0)
                            {
                                rate = stuff;
                            }
                            if (column.ColumnArray[colcnt] == "Rate 2")
                            {
                                if (stuff.Length > 0)
                                    rate = rate + "/" + stuff;
                                if (rate != null)
                                    vital.Add("RATE^" + rate);
                            }
                        }
                    }
                    else
                    {
                        if (param != "skip")
                        {
                            string stuff = row[col].ToString();
                            if (stuff != null && stuff.Length > 0)
                            {
                                stuff = stuff.Replace("\r", string.Empty).Replace("\n", string.Empty).Trim();
                                //if (param == "DT_TAKEN")
                                    //if (stuff.IndexOf(" ") != -1)
                                        //stuff = stuff.Substring(0, (stuff.IndexOf(" ")));
                                vital.Add(param + "^" + stuff);
                            }
                        }
                    }
                }
            }
            if (flag == false)
            {
                vital.Clear();
                vital.Add("-1");
            }
            return vital;
        }

        public List<string> ParseAllergyRPCData(DataTable table, DataRow row, PatientList pntlist)
        {
            bool check = false;
            Column column = new Column();
            AllergyMapping alergmap = new AllergyMapping();
            List<string> allergy = new List<string>();
            int colcnt = 0;
            string ssn = "";
            foreach (DataColumn col in table.Columns)
            {
                colcnt++;
                check = column.AllergyColumnArraySet(col.ToString(), colcnt);
            }
            //build RPC list
            colcnt = 0;
            bool flag = false;
            foreach (DataColumn col in table.Columns)
            {
                colcnt++;
                check = column.ColumnCheck(colcnt);
                if (check == true)
                {
                    string param = alergmap.ExcellMapping(column.ColumnArray[colcnt]);
                    if (param == "CASE")
                    {
                        string stuff = row[col].ToString();
                        stuff = stuff.Replace("\r", string.Empty).Replace("\n", string.Empty).Trim();
                        foreach (Patient pnt in pntlist)
                        {
                            if (pnt.CaseName == stuff && stuff.Length > 0)
                            {
                                allergy.Add("PAT_SSN^" + pnt.SSN);
                                ssn = pnt.SSN;
                                allergy.Add("HISTORIC^1");
                                flag = true;
                                break;
                            }
                        }
                        param = "skip";
                    }
                    if (param == "PAT_SSN")
                    {
                        string stuff = row[col].ToString();
                        stuff = stuff.Replace("\r", string.Empty).Replace("\n", string.Empty).Trim();
                        if (stuff.Length == 9)
                        {
                            flag = true;
                            allergy.Add("PAT_SSN^" + stuff);
                            allergy.Add("HISTORIC^1");
                            param = "skip";
                        }
                        if (stuff.Length != 9)
                        {
                            if (ssn != "") row[col] = ssn;
                        }
                    }
                    //
                    if (param != "skip")
                    {
                        string stuff = row[col].ToString();
                        if (stuff != null && stuff.Length > 0)
                        {
                            stuff = stuff.Replace("\r", string.Empty).Replace("\n", string.Empty).Trim();
                            //if (param == "ORIG_DATE")
                                //if (stuff.IndexOf(" ") != -1)
                                    //stuff = stuff.Substring(0, (stuff.IndexOf(" ")));
                            allergy.Add(param + "^" + stuff);
                        }
                    }
                }
            }
            if (flag == false)
            {
                allergy.Clear();
                allergy.Add("-1");
            }
            return allergy;
        }

        public List<string> ParseLabRPCData(DataTable table, DataRow row, PatientList pntlist)
        {
            bool check = false;
            Column column = new Column();
            LabMapping labmap = new LabMapping();
            List<string> labs = new List<string>();
            int colcnt = 0;
            string ssn = "";
            foreach (DataColumn col in table.Columns)
            {
                colcnt++;
                check = column.LabColumnArraySet(col.ToString(), colcnt);
            }
            //build RPC list
            colcnt = 0;
            bool flag = false;
            foreach (DataColumn col in table.Columns)
            {
                colcnt++;
                check = column.ColumnCheck(colcnt);
                if (check == true)
                {
                    string param = labmap.ExcellMapping(column.ColumnArray[colcnt]);
                    if (param == "CASE")
                    {
                        string stuff = row[col].ToString();
                        stuff = stuff.Replace("\r", string.Empty).Replace("\n", string.Empty).Trim();
                        foreach (Patient pnt in pntlist)
                        {
                            if (pnt.CaseName == stuff && stuff.Length > 0)
                            {
                                labs.Add("PAT_SSN^" + pnt.SSN);
                                ssn = pnt.SSN;
                                flag = true;
                                break;
                            }
                        }
                        param = "skip";
                    }
                    if (param == "PAT_SSN")
                    {
                        string stuff = row[col].ToString();
                        stuff = stuff.Replace("\r", string.Empty).Replace("\n", string.Empty).Trim();
                        if (stuff.Length == 9)
                        {
                            flag = true;
                            labs.Add("PAT_SSN^" + stuff);
                            param = "skip";
                        }
                        if (stuff.Length != 9)
                        {
                            if (ssn != "") row[col] = ssn;
                        }
                    }
                    //
                    if (param != "skip")
                    {
                        string stuff = row[col].ToString();
                        if (stuff != null && stuff.Length > 0)
                        {
                            stuff = stuff.Replace("\r", string.Empty).Replace("\n", string.Empty).Trim();
                            labs.Add(param + "^" + stuff);
                        }
                    }
                }
            }
            if (flag == false)
            {
                labs.Clear();
                labs.Add("-1");
            }
            return labs;
        }

        public List<string> ParseNoteRPCData(DataTable table, DataRow row, PatientList pntlist)
        {
            bool check = false;
            Column column = new Column();
            NoteMapping notemap = new NoteMapping();
            List<string> notes = new List<string>();
            int colcnt = 0;
            string ssn = "";
            foreach (DataColumn col in table.Columns)
            {
                colcnt++;
                check = column.NoteColumnArraySet(col.ToString(), colcnt);
            }
            //build RPC list
            colcnt = 0;
            bool flag = false;
            foreach (DataColumn col in table.Columns)
            {
                colcnt++;
                check = column.ColumnCheck(colcnt);
                if (check == true)
                {
                    string param = notemap.ExcellMapping(column.ColumnArray[colcnt]);
                    if (param == "CASE")
                    {
                        string stuff = row[col].ToString();
                        stuff = stuff.Replace("\r", string.Empty).Replace("\n", string.Empty).Trim();
                        foreach (Patient pnt in pntlist)
                        {
                            if (pnt.CaseName == stuff && stuff.Length > 0)
                            {
                                notes.Add("PAT_SSN^" + pnt.SSN);
                                ssn = pnt.SSN;
                                flag = true;
                                break;
                            }
                        }
                        param = "skip";
                    }
                    if (param == "PAT_SSN")
                    {
                        string stuff = row[col].ToString();
                        stuff = stuff.Replace("\r", string.Empty).Replace("\n", string.Empty).Trim();
                        if (stuff.Length == 9)
                        {
                            flag = true;
                            notes.Add("PAT_SSN^" + stuff);
                            param = "skip";
                        }
                        if (stuff.Length != 9)
                        {
                            if (ssn != "") row[col] = ssn;
                        }
                    }
                    //
                    if (param != "skip")
                    {
                        string stuff = row[col].ToString();
                        if (stuff != null && stuff.Length > 0)
                        {
                            stuff = stuff.Replace("\r", string.Empty).Replace("\n", string.Empty).Trim();
                            //if (param == "ORIG_DATE")
                            //    if (stuff.IndexOf(" ") != -1)
                            //        stuff = stuff.Substring(0, (stuff.IndexOf(" ")));
                            notes.Add(param + "^" + stuff);
                        }
                    }
                }
            }
            if (flag == false)
            {
                notes.Clear();
                notes.Add("-1");
            }
            return notes;
        }

        public List<string> ParseMedRPCData(DataTable table, DataRow row, PatientList pntlist)
        {
            bool check = false;
            Column column = new Column();
            MedMapping medmap = new MedMapping();
            List<string> meds = new List<string>();
            int colcnt = 0;
            string ssn = "";
            foreach (DataColumn col in table.Columns)
            {
                colcnt++;
                check = column.MedColumnArraySet(col.ToString(), colcnt);
            }
            //build RPC list
            colcnt = 0;
            bool flag = false;
            foreach (DataColumn col in table.Columns)
            {
                colcnt++;
                check = column.ColumnCheck(colcnt);
                if (check == true)
                {
                    string param = medmap.ExcellMapping(column.ColumnArray[colcnt]);
                    if (param == "CASE")
                    {
                        string stuff = row[col].ToString();
                        stuff = stuff.Replace("\r", string.Empty).Replace("\n", string.Empty).Trim();
                        foreach (Patient pnt in pntlist)
                        {
                            if (pnt.CaseName == stuff && stuff.Length > 0)
                            {
                                meds.Add("PAT_SSN^" + pnt.SSN);
                                ssn = pnt.SSN;
                                flag = true;
                                break;
                            }
                        }
                        param = "skip";
                    }
                    if (param == "PAT_SSN")
                    {
                        string stuff = row[col].ToString();
                        stuff = stuff.Replace("\r", string.Empty).Replace("\n", string.Empty).Trim();
                        if (stuff.Length == 9)
                        {
                            flag = true;
                            meds.Add("PAT_SSN^" + stuff);
                            param = "skip";
                        }
                        if (stuff.Length != 9)
                        {
                            if (ssn != "") row[col] = ssn;
                        }
                    }
                    //
                    if (param != "skip")
                    {
                        string stuff = row[col].ToString();
                        if (stuff != null && stuff.Length > 0)
                        {
                            stuff = stuff.Replace("\r", string.Empty).Replace("\n", string.Empty).Trim();
                            meds.Add(param + "^" + stuff);
                        }
                    }
                }
            }
            if (flag == false)
            {
                meds.Clear();
                meds.Add("-1");
            }
            return meds;
        }

        public List<string> ParseConsultRPCData(DataTable table, DataRow row, PatientList pntlist)
        {
            bool check = false;
            Column column = new Column();
            ConsultMapping consultmap = new ConsultMapping();
            List<string> consults = new List<string>();
            int colcnt = 0;
            string ssn = "";
            foreach (DataColumn col in table.Columns)
            {
                colcnt++;
                check = column.ConsultColumnArraySet(col.ToString(), colcnt);
            }
            //build RPC list
            colcnt = 0;
            bool flag = false;
            foreach (DataColumn col in table.Columns)
            {
                colcnt++;
                check = column.ColumnCheck(colcnt);
                if (check == true)
                {
                    string param = consultmap.ExcellMapping(column.ColumnArray[colcnt]);
                    if (param == "CASE")
                    {
                        string stuff = row[col].ToString();
                        stuff = stuff.Replace("\r", string.Empty).Replace("\n", string.Empty).Trim();
                        foreach (Patient pnt in pntlist)
                        {
                            if (pnt.CaseName == stuff && stuff.Length > 0)
                            {
                                consults.Add("PAT_SSN^" + pnt.SSN);
                                ssn = pnt.SSN;
                                flag = true;
                                break;
                            }
                        }
                        param = "skip";
                    }
                    if (param == "PAT_SSN")
                    {
                        string stuff = row[col].ToString();
                        stuff = stuff.Replace("\r", string.Empty).Replace("\n", string.Empty).Trim();
                        if (stuff.Length == 9)
                        {
                            flag = true;
                            consults.Add("PAT_SSN^" + stuff);
                            param = "skip";
                        }
                        if (stuff.Length != 9)
                        {
                            if (ssn != "") row[col] = ssn;
                        }
                    }
                    //
                    if (param != "skip")
                    {
                        string stuff = row[col].ToString();
                        if (stuff != null && stuff.Length > 0)
                        {
                            stuff = stuff.Replace("\r", string.Empty).Replace("\n", string.Empty).Trim();
                            consults.Add(param + "^" + stuff);
                        }
                    }
                }
            }
            if (flag == false)
            {
                consults.Clear();
                consults.Add("-1");
            }
            return consults;
        }

        public List<string> ParseRadRPCData(DataTable table, DataRow row, PatientList pntlist)
        {
            bool check = false;
            Column column = new Column();
            RadMapping radmap = new RadMapping();
            List<string> rads = new List<string>();
            int colcnt = 0;
            string ssn = "";
            foreach (DataColumn col in table.Columns)
            {
                colcnt++;
                check = column.RadColumnArraySet(col.ToString(), colcnt);
            }
            //build RPC list
            colcnt = 0;
            bool flag = false;
            foreach (DataColumn col in table.Columns)
            {
                colcnt++;
                check = column.ColumnCheck(colcnt);
                if (check == true)
                {
                    string param = radmap.ExcellMapping(column.ColumnArray[colcnt]);
                    if (param == "CASE")
                    {
                        string stuff = row[col].ToString();
                        stuff = stuff.Replace("\r", string.Empty).Replace("\n", string.Empty).Trim();
                        foreach (Patient pnt in pntlist)
                        {
                            if (pnt.CaseName == stuff && stuff.Length > 0)
                            {
                                rads.Add("PAT_SSN^" + pnt.SSN);
                                ssn = pnt.SSN;
                                flag = true;
                                break;
                            }
                        }
                        param = "skip";
                    }
                    if (param == "PAT_SSN")
                    {
                        string stuff = row[col].ToString();
                        stuff = stuff.Replace("\r", string.Empty).Replace("\n", string.Empty).Trim();
                        if (stuff.Length == 9)
                        {
                            flag = true;
                            rads.Add("PAT_SSN^" + stuff);
                            param = "skip";
                        }
                        if (stuff.Length != 9)
                        {
                            if (ssn != "") row[col] = ssn;
                        }
                    }
                    //
                    if (param != "skip")
                    {
                        string stuff = row[col].ToString();
                        if (stuff != null && stuff.Length > 0)
                        {
                            stuff = stuff.Replace("\r", string.Empty).Replace("\n", string.Empty).Trim();
                            rads.Add(param + "^" + stuff);
                        }
                    }
                }
            }
            if (flag == false)
            {
                rads.Clear();
                rads.Add("-1");
            }
            return rads;
        }

        public List<string> ParseGenericRPCData(DataTable table, DataRow row, PatientList pntlist, String worksheet)
        {
            bool check = false;
            List<string> list = new List<string>();
            Column column = new Column();
            dynamic map;
            switch (worksheet)
            {
                case "VCpt":
                    {
                        map = new VCptMapping();
                        break;
                    }
                case "VImmunization":
                    {
                        map = new VImmunizationMapping();
                        break;
                    }
                case "HFactors":
                    {
                        map = new HealthFactorMapping();
                        break;
                    }
                case "VExam":
                    {
                        map = new VExamMapping();
                        break;
                    }
                case "VPov":
                    {
                        map = new VPovMapping();
                        break;
                    }
                case "VPatientEd":
                    {
                        map = new VPntEducationMapping();
                        break;
                    }
                default:
                    {
                        return list;
                    }
            }
            int colcnt = 0;
            string ssn = "";
            foreach (DataColumn col in table.Columns)
            {
                colcnt++;
                check = column.GenColumnArrarySet(col.ToString(), colcnt);
            }
            //build RPC list
            colcnt = 0;
            bool flag = false;
            foreach (DataColumn col in table.Columns)
            {
                colcnt++;
                check = column.ColumnCheck(colcnt);
                if (check == true)
                {
                    string param = map.ExcellMapping(column.ColumnArray[colcnt]);
                    if (param == "CASE")
                    {
                        string stuff = row[col].ToString();
                        stuff = stuff.Replace("\r", string.Empty).Replace("\n", string.Empty).Trim();
                        foreach (Patient pnt in pntlist)
                        {
                            if (pnt.CaseName == stuff && stuff.Length > 0)
                            {
                                list.Add("PAT_SSN^" + pnt.SSN);
                                ssn = pnt.SSN;
                                flag = true;
                                break;
                            }
                        }
                        param = "skip";
                    }
                    if (param == "PAT_SSN")
                    {
                        string stuff = row[col].ToString();
                        stuff = stuff.Replace("\r", string.Empty).Replace("\n", string.Empty).Trim();
                        if (stuff.Length == 9)
                            flag = true;
                        if (stuff.Length != 9)
                        {
                            if (ssn != "") row[col] = ssn;
                        }
                    }
                    //
                    if (param != "skip")
                    {
                        string stuff = row[col].ToString();
                        if (stuff != null && stuff.Length > 0)
                        {
                            stuff = stuff.Replace("\r", string.Empty).Replace("\n", string.Empty).Trim();
                            list.Add(param + "^" + stuff);
                        }
                    }
                }
            }
            if (flag == false)
            {
                list.Clear();
                list.Add("-1");
            }
            return list;
        }

        public List<string> ParseTreatingFacilityRPCData(DataTable table, DataRow row, PatientList pntlist)
        {
            bool check = false;
            Column column = new Column();
            TreatingFacilityMapping tflmap = new Common.Model.TreatingFacilityMapping();
            List<string> tfl = new List<string>();
            int colcnt = 0;
            string ssn = "";
            foreach (DataColumn col in table.Columns)
            {
                colcnt++;
                check = column.TreatingFacilityColumnArraySet(col.ToString(), colcnt);
            }
            //build RPC list
            colcnt = 0;
            bool flag = false;
            foreach (DataColumn col in table.Columns)
            {
                colcnt++;
                check = column.ColumnCheck(colcnt);
                if (check == true)
                {
                    string param = tflmap.ExcellMapping(column.ColumnArray[colcnt]);
                    if (param == "CASE")
                    {
                        string stuff = row[col].ToString();
                        stuff = stuff.Replace("\r", string.Empty).Replace("\n", string.Empty).Trim();
                        foreach (Patient pnt in pntlist)
                        {
                            if (pnt.CaseName == stuff && stuff.Length > 0)
                            {
                                tfl.Add("PAT_SSN^" + pnt.SSN);
                                ssn = pnt.SSN;
                                flag = true;
                                break;
                            }
                        }
                        param = "skip";
                    }
                    if (param == "PAT_SSN")
                    {
                        string stuff = row[col].ToString();
                        stuff = stuff.Replace("\r", string.Empty).Replace("\n", string.Empty).Trim();
                        if (stuff.Length == 9)
                        {
                            flag = true;
                            tfl.Add("PAT_SSN^" + stuff);
                            param = "skip";
                        }
                        if (stuff.Length != 9)
                        {
                            if (ssn != "") row[col] = ssn;
                        }
                    }
                    //
                    if (param != "skip")
                    {
                        string stuff = row[col].ToString();
                        if (stuff != null && stuff.Length > 0)
                        {
                            stuff = stuff.Replace("\r", string.Empty).Replace("\n", string.Empty).Trim();
                            tfl.Add(param + "^" + stuff);
                        }
                    }
                }
            }
            if (flag == false)
            {
                tfl.Clear();
                tfl.Add("-1");
            }
            return tfl;
        }

        public List<string> ParseNonVAMedsRPCData(DataTable table, DataRow row, PatientList pntlist)
        {
            bool check = false;
            Column column = new Column();
            NonVAMedMapping nvamap = new Common.Model.NonVAMedMapping();
            List<string> nva = new List<string>();
            int colcnt = 0;
            string ssn = "";
            foreach (DataColumn col in table.Columns)
            {
                colcnt++;
                check = column.NonVAMedColumnArraySet(col.ToString(), colcnt);
            }
            //build RPC list
            colcnt = 0;
            bool flag = false;
            foreach (DataColumn col in table.Columns)
            {
                colcnt++;
                check = column.ColumnCheck(colcnt);
                if (check == true)
                {
                    string param = nvamap.ExcellMapping(column.ColumnArray[colcnt]);
                    if (param == "CASE")
                    {
                        string stuff = row[col].ToString();
                        stuff = stuff.Replace("\r", string.Empty).Replace("\n", string.Empty);
                        foreach (Patient pnt in pntlist)
                        {
                            if (pnt.CaseName == stuff && stuff.Length > 0)
                            {
                                nva.Add("PAT_SSN^" + pnt.SSN);
                                ssn = pnt.SSN;
                                flag = true;
                                break;
                            }
                        }
                        param = "skip";
                    }
                    if (param == "PAT_SSN")
                    {
                        string stuff = row[col].ToString();
                        stuff = stuff.Replace("\r", string.Empty).Replace("\n", string.Empty).Trim();
                        if (stuff.Length == 9)
                        {
                            flag = true;
                            nva.Add("PAT_SSN^" + stuff);
                            param = "skip";
                        }
                        if (stuff.Length != 9)
                        {
                            if (ssn != "") row[col] = ssn;
                        }
                    }
                    if (param == "DRUG")
                    {
                        string stuff = row[col].ToString();
                        stuff = stuff.Replace("\r", string.Empty).Replace("\n", string.Empty);
                        nva.Add("DRUG^" + stuff);
                        param = "skip";
                    }
                    //
                    if (param != "skip")
                    {
                        string stuff = row[col].ToString();
                        if (stuff != null && stuff.Length > 0)
                        {
                            stuff = stuff.Replace("\r", string.Empty).Replace("\n", string.Empty).Trim();
                            nva.Add(param + "^" + stuff);
                        }
                    }
                }
            }
            if (flag == false)
            {
                nva.Clear();
                nva.Add("-1");
            }
            return nva;
        }
    }
}
