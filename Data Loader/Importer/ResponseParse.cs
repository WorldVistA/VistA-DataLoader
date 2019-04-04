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

namespace DataLoader
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using DataLoader.Common.Model;
    using System.Text.RegularExpressions;
    using System.IO;

    internal class ResponseParser
    {

        internal static List<string> ParseCreatePatientResponse(string rawData, Patient pnt)
        {
            List<string> rlist = new List<string>();
            if (rawData == null)
            {
                rlist.Add("error");
                rlist.Add("No reply from VistA.");
                return rlist;
            }
            string results = rawData.ToString();
            string[] lines;
            try
            {
                lines = Regex.Split(results, "\r\n");
            }
            catch
            {
                rlist.Add("error");
                rlist.Add("No expected reply from VistA.");
                return rlist;
            }
            pnt.Clear();
            if ((lines != null) && (lines.Length > 0))
            {
                if (lines[0] == "1")
                {
                    string[] fields = lines[1].Split('^');
                    if ((fields != null) && (fields.Length > 0))
                    {
                        pnt.PatientDFN = fields[0];
                        pnt.SSN = fields[1];
                        pnt.PatientName = fields[2];
                        pnt.CaseName = Case.CaseName;
                        rlist.Add("success");
                        rlist.Add(pnt.PatientName);
                        rlist.Add(pnt.PatientDFN);
                        rlist.Add(pnt.SSN);
                    }
                }
                else
                {
                    string[] fields = lines[0].Split('^');
                    if ((fields != null) && (fields.Length > 0))
                    {
                        if (fields[0] == "-1")
                        {
                            // error
                            rlist.Add("error");
                            if (fields[1] != null)
                            {
                                rlist.Add(fields[1].ToString());
                            }
                        }
                        else
                        {
                            rlist.Add("success");
                            foreach (string line in lines)
                            {
                                fields = line.Split('^');
                                try
                                {
                                    string check = fields[1];
                                }
                                catch
                                {
                                    //filter past header count record
                                    continue;
                                }
                                rlist.Add("Patient Name:" + fields[2] + " SSN:" + fields[1] + " DFN:" + fields[0]);
                            }
                        }
                    }
                }
            } 
            return rlist;
        }

        internal static List<string> ParseCreateUserResponse(string rawData, UserLoad user)
        {
            List<string> rlist = new List<string>();
            if (rawData == null)
            {
                rlist.Add("error");
                rlist.Add("No reply from VistA.");
                return rlist;
            }
            string results = rawData.ToString();
            string[] lines;
            try
            {
                lines = Regex.Split(results, "\r\n");
            }
            catch
            {
                rlist.Add("error");
                rlist.Add("No expected reply from VistA.");
                return rlist;
            }
            user.Clear();
            if ((lines != null) && (lines.Length > 0))
            {
                if (lines[0] == "1")
                {
                    string[] fields = lines[1].Split('^');
                    if ((fields != null) && (fields.Length > 0))
                    {

                        rlist.Add("success");
                        try
                        {
                            rlist.Add("User Name:" + fields[2] + " SSN:" + fields[1] + " DFN:" + fields[0]);
                        }
                        catch
                        {
                        }
                    }
                }
                else
                {
                    string[] fields = lines[0].Split('^');
                    if ((fields != null) && (fields.Length > 0))
                    {
                        if (fields[0] == "-1")
                        {
                            // error
                            rlist.Add("error");
                            if (fields[1] != null)
                            {
                                rlist.Add(fields[1].ToString());
                            }
                        }
                        else
                        {
                            rlist.Add("success");
                            foreach (string line in lines)
                            {
                                fields = line.Split('^');
                                try
                                {
                                    string check = fields[1];
                                }
                                catch
                                {
                                    //filter past header count record
                                    continue;
                                }
                                rlist.Add("User Name:" + fields[2] + " SSN:" + fields[1] + " DFN:" + fields[0]);
                            }
                        }
                    }
                }
            }
            return rlist;
        }

        internal static List<string> ParseCreateApptResponse(string rawData)
        {
            List<string> rlist = new List<string>();
            if (rawData == null)
            {
                rlist.Add("error");
                rlist.Add("VistA error!");
                return rlist;
            }
            string results = rawData.ToString();
            string[] lines = Regex.Split(results, "\r\n");
            if ((lines != null) && (lines.Length > 0))
            {
                if (lines[0] == "1")
                {
                    string[] fields = lines[1].Split('^');
                    if ((fields != null) && (fields.Length > 0))
                    {
                        rlist.Add("success");
                        //rlist.Add(fields[1].ToString());
                    }
                }
                else
                {
                    string[] fields = lines[0].Split('^');
                    if ((fields != null) && (fields.Length > 0))
                    {
                        if (fields[0] == "-1")
                        {
                            rlist.Add("error");
                            if (fields[1] != null)
                            {
                                rlist.Add(fields[1].ToString());
                            }
                        }
                        if (fields[0] == "-9")
                        {
                            rlist.Add("duplicate");
                            if (fields[1] != null)
                            {
                                rlist.Add(fields[1].ToString());
                            }
                        }
                    }
                }
            }
            return rlist;
        }

        internal static List<string> ParseTemplateSaveResponse(string rawData)
        {
            List<string> rlist = new List<string>();
            if (rawData == null)
            {
                rlist.Add("error");
                rlist.Add("VistA error!");
                return rlist;
            }
            string results = rawData.ToString();
            string[] lines = Regex.Split(results, "\r\n");
            if ((lines != null) && (lines.Length > 0))
            {
                if (lines[0] == "0")
                {
                    string[] fields = lines[1].Split('^');
                    if ((fields != null) && (fields.Length > 0))
                    {
                        rlist.Add("success");
                    }
                }
                else
                {
                    string[] fields = lines[0].Split('^');
                    if ((fields != null) && (fields.Length > 0))
                    {
                        if (fields[0] == "-1")
                        {
                            rlist.Add("error");
                            if (fields[1] != null)
                            {
                                rlist.Add(fields[1].ToString());
                            }
                        }
                    }
                }
            }
            return rlist;
        }

        internal static List<string> ParseCreateProbResponse(string rawData)
        {
            List<string> rlist = new List<string>();
            if (rawData == null)
            {
                rlist.Add("error");
                rlist.Add("VistA error!");
                return rlist;
            }
            string results = rawData.ToString();
            string[] lines = Regex.Split(results, "\r\n");
            if ((lines != null) && (lines.Length > 0))
            {
                if (lines[0] == "1")
                {
                    string[] fields = lines[1].Split('^');
                    if ((fields != null) && (fields.Length > 0))
                    {
                        rlist.Add("success");
                        //rlist.Add(fields[1].ToString());
                    }
                }
                else
                {
                    string[] fields = lines[0].Split('^');
                    if ((fields != null) && (fields.Length > 0))
                    {
                        if (fields[0] == "-1")
                        {
                            rlist.Add("error");
                            if (fields[1] != null)
                            {
                                rlist.Add(fields[1].ToString());
                            }
                        }
                        if (fields[0] == "-9")
                        {
                            rlist.Add("duplicate");
                            if (fields[1] != null)
                            {
                                rlist.Add(fields[1].ToString());
                            }
                        }
                    }
                }
            }
            return rlist;
        }

        internal static List<string> ParseCreateAllergyResponse(string rawData)
        {
            List<string> rlist = new List<string>();
            if (rawData == null)
            {
                rlist.Add("error");
                rlist.Add("VistA error!");
                return rlist;
            }
            string results = rawData.ToString();
            string[] lines = Regex.Split(results, "\r\n");
            if ((lines != null) && (lines.Length > 0))
            {
                if (lines[0] == "1")
                {
                    string[] fields = lines[1].Split('^');
                    if ((fields != null) && (fields.Length > 0))
                    {
                        rlist.Add("success");
                        //rlist.Add(fields[1].ToString());
                    }
                }
                else
                {
                    string[] fields = lines[0].Split('^');
                    if ((fields != null) && (fields.Length > 0))
                    {
                        if (fields[0] == "-1")
                        {
                            rlist.Add("error");
                            if (fields[1] != null)
                            {
                                rlist.Add(fields[1].ToString());
                            }
                        }
                    }
                }
            }
            return rlist;
        }

        internal static List<string> ParseCreateVitalResponse(string rawData)
        {
            List<string> rlist = new List<string>();
            if (rawData == null)
            {
                rlist.Add("error");
                rlist.Add("VistA error!");
                return rlist;
            }
            string results = rawData.ToString();
            string[] lines = Regex.Split(results, "\r\n");
            if ((lines != null) && (lines.Length > 0))
            {
                if (lines[0] == "1")
                {
                    string[] fields = lines[1].Split('^');
                    if ((fields != null) && (fields.Length > 0))
                    {
                        rlist.Add("success");
                        //rlist.Add(fields[1].ToString());
                    }
                }
                else
                {
                    string[] fields = lines[0].Split('^');
                    if ((fields != null) && (fields.Length > 0))
                    {
                        if (fields[0] == "-1")
                        {
                            rlist.Add("error");
                            if (fields[1] != null)
                            {
                                rlist.Add(fields[1].ToString());
                            }
                        }
                    }
                }
            }
            return rlist;
        }

        internal static List<string> ParseCreateLabResponse(string rawData)
        {
            List<string> rlist = new List<string>();
            if (rawData == null)
            {
                //There is an issue with GT.M where the connection is dropping, despite no error on the LABS
                rlist.Add("confirm");
                //rlist.Add("no");
                return rlist;
            }
            string results = rawData.ToString();
            string[] lines;
            try
            {
                lines = Regex.Split(results, "\r\n");
            }
            catch
            {
                lines = null;
                rlist.Add("confirm");
            }
            if ((lines != null) && (lines.Length > 0))
            {
                if (lines[0] == "1")
                {
                    string[] fields = lines[1].Split('^');
                    if ((fields != null) && (fields.Length > 0))
                    {
                        rlist.Add("success");
                        //rlist.Add(fields[1].ToString());
                    }
                }
                else
                {
                    string[] fields = lines[0].Split('^');
                    if ((fields != null) && (fields.Length > 0))
                    {
                        if (fields[0] == "-1")
                        {
                            rlist.Add("error");
                            if (fields[1] != null)
                            {
                                rlist.Add(fields[1].ToString());
                            }
                        }
                    }
                }
            }
            return rlist;
        }

        internal static List<string> ParseCreateNoteResponse(string rawData)
        {
            List<string> rlist = new List<string>();
            if (rawData == null)
            {
                rlist.Add("error");
                rlist.Add("VistA error!");
                return rlist;
            }
            string results = rawData.ToString();
            string[] lines = Regex.Split(results, "\r\n");
            if ((lines != null) && (lines.Length > 0))
            {
                if (lines[0] == "1")
                {
                    string[] fields = lines[1].Split('^');
                    if ((fields != null) && (fields.Length > 0))
                    {
                        rlist.Add("success");
                        //rlist.Add(fields[1].ToString());
                    }
                }
                else
                {
                    string[] fields = lines[0].Split('^');
                    if ((fields != null) && (fields.Length > 0))
                    {
                        if (fields[0] == "-1")
                        {
                            rlist.Add("error");
                            rlist.Add(fields[1].ToString());
                        }
                    }
                }
            }
            return rlist;
        }

        internal static List<string> ParseCreateMedResponse(string rawData)
        {
            List<string> rlist = new List<string>();
            if (rawData == null)
            {
                rlist.Add("error");
                rlist.Add("VistA error!");
                return rlist;
            }
            string results = rawData.ToString();
            string[] lines = Regex.Split(results, "\r\n");
            if ((lines != null) && (lines.Length > 0))
            {
                if (lines[0] == "1")
                {
                    string[] fields = lines[1].Split('^');
                    if ((fields != null) && (fields.Length > 0))
                    {
                        rlist.Add("success");
                        //rlist.Add(fields[1].ToString());
                    }
                }
                else
                {
                    string[] fields = lines[0].Split('^');
                    if ((fields != null) && (fields.Length > 0))
                    {
                        if (fields[0] == "-1")
                        {
                            rlist.Add("error");
                            rlist.Add(fields[1].ToString());
                        }
                    }
                    if (fields[0] == "-9")
                    {
                        rlist.Add("duplicate");
                        if (fields[1] != null)
                        {
                            rlist.Add(fields[1].ToString());
                        }
                    }
                }
            }
            return rlist;
        }

        internal static List<string> ParseCreateNonVAMedResponse(string rawData)
        {
            List<string> rlist = new List<string>();
            if (rawData == null)
            {
                rlist.Add("error");
                rlist.Add("VistA error!");
                return rlist;
            }
            string results = rawData.ToString();
            string[] lines = Regex.Split(results, "\r\n");
            if ((lines != null) && (lines.Length > 0))
            {
                if (lines[0] == "1")
                {
                    string[] fields = lines[1].Split('^');
                    if ((fields != null) && (fields.Length > 0))
                    {
                        rlist.Add("success");
                        //rlist.Add(fields[1].ToString());
                    }
                }
                else
                {
                    string[] fields = lines[0].Split('^');
                    if ((fields != null) && (fields.Length > 0))
                    {
                        if (fields[0] == "-1")
                        {
                            rlist.Add("error");
                            rlist.Add(fields[1].ToString());
                        }
                    }
                }
            }
            return rlist;
        }

        internal static List<string> ParseCreateConsultResponse(string rawData)
        {
            List<string> rlist = new List<string>();
            if (rawData == null)
            {
                rlist.Add("error");
                rlist.Add("VistA error!");
                return rlist;
            }
            string results = rawData.ToString();
            string[] lines = Regex.Split(results, "\r\n");
            if ((lines != null) && (lines.Length > 0))
            {
                if (lines[0] == "1")
                {
                    string[] fields = lines[1].Split('^');
                    if ((fields != null) && (fields.Length > 0))
                    {
                        rlist.Add("success");
                        //rlist.Add(fields[1].ToString());
                    }
                }
                else
                {
                    string[] fields = lines[0].Split('^');
                    if ((fields != null) && (fields.Length > 0))
                    {
                        if (fields[0] == "-1")
                        {
                            rlist.Add("error");
                            rlist.Add(fields[1].ToString());
                        }
                    }
                }
            }
            return rlist;
        }

        internal static List<string> ParseCreateRadResponse(string rawData)
        {
            List<string> rlist = new List<string>();
            if (rawData == null)
            {
                rlist.Add("error");
                rlist.Add("VistA error!");
                return rlist;
            }
            string results = rawData.ToString();
            string[] lines = Regex.Split(results, "\r\n");
            if ((lines != null) && (lines.Length > 0))
            {
                if (lines[0] == "1")
                {
                    string[] fields = lines[1].Split('^');
                    if ((fields != null) && (fields.Length > 0))
                    {
                        rlist.Add("success");
                        //rlist.Add(fields[1].ToString());
                    }
                }
                else
                {
                    string[] fields = lines[0].Split('^');
                    if ((fields != null) && (fields.Length > 0))
                    {
                        if (fields[0] == "-1")
                        {
                            rlist.Add("error");
                            rlist.Add(fields[1].ToString());
                        }
                    }
                }
            }
            return rlist;
        }

        internal static List<string> ParseGenericResponse(string rawData)
        {
            List<string> rlist = new List<string>();
            if (rawData == null)
            {
                rlist.Add("error");
                rlist.Add("VistA error!");
                return rlist;
            }
            string results = rawData.ToString();
            string[] lines = Regex.Split(results, "\r\n");
            if ((lines != null) && (lines.Length > 0))
            {
                if (lines[0] == "1")
                {
                    string[] fields = lines[1].Split('^');
                    if ((fields != null) && (fields.Length > 0))
                    {
                        rlist.Add("success");
                        //rlist.Add(fields[1].ToString());
                    }
                }
                else
                {
                    string[] fields = lines[0].Split('^');
                    if ((fields != null) && (fields.Length > 0))
                    {
                        if (fields[0] == "-1")
                        {
                            rlist.Add("error");
                            if (fields[1] != null)
                            {
                                rlist.Add(fields[1].ToString());
                            }
                        }
                        if (fields[0] == "-9")
                        {
                            rlist.Add("duplicate");
                            if (fields[1] != null)
                            {
                                rlist.Add(fields[1].ToString());
                            }
                        }
                    }
                }
            }
            return rlist;
        }

        internal static List<string> ParseFetchTableResponse(string rawData)
        {
            List<string> rlist = new List<string>();
            if (rawData == null)
            {
                rlist.Add("error");
                rlist.Add("VistA error!");
                return rlist;
            }
            string results = rawData.ToString();
            string[] lines = Regex.Split(results, "\r\n");
            if ((lines != null) && (lines.Length > 0))
            {
                string[] fields = lines[0].Split('^');
                if (fields[0] == "-1")
                {
                    rlist.Add("error");
                    rlist.Add(fields[1].ToString());
                    return rlist;
                }
                // foreach (string line in lines)
                for (int i = 1; lines[i].Length > 0; i++)
                {
                    rlist.Add(lines[i].ToString());
                }
            }
            return rlist;
        }
        //
        internal static StringItemList ParseFetchTemplateResponse(string rawData)
        {
            StringItemList list = new StringItemList();
            if (rawData == null)
            {
                return list;
            }
            string results = rawData.ToString();
            string[] lines;
            try
            {
                lines = Regex.Split(results, "\r\n");
            }
            catch
            {
                return list;
            }
            if ((lines != null) && (lines.Length > 0))
            {
                foreach (string line in lines)
                {
                    string[] fields = line.Split('^');
                    try
                    {
                        string check = fields[1];
                    }
                    catch
                    {
                        continue;
                    }
                    if ((fields != null) && (fields.Length > 0))
                    {
                        if (fields[0] != "")
                        {
                            StringItem item = new StringItem();
                            item.id = fields[0];
                            item.name = fields[1];
                            list.items.Add(item);
                        }
                    }
                }
            }
            return list;
        }

        public static StringItemList ParseTemplateDetailResponse(string rawData)
        {
            StringItemList list = new StringItemList();
            if (rawData == null)
            {
                return list;
            }
            //
            string results = rawData.ToString();
            string[] lines;
            try
            {
                lines = Regex.Split(results, "\r\n");
            }
            catch
            {
                return list;
            }

            if ((lines != null) && (lines.Length > 0))
            {
                foreach (string line in lines)
                {
                    string[] fields = line.Split('^');
                    try
                    {
                        string check = fields[1];
                    }
                    catch
                    {
                        continue;
                    }
                    if ((fields != null) && (fields.Length > 0))
                    {
                        if (fields[0] != "")
                        {
                            StringItem item = new StringItem();
                            item.id = fields[0]; //field
                            item.name = fields[1] + "|" + fields[2]; //external|internal
                            list.items.Add(item);
                        }
                    }
                }
            }
            return list;
        }

        internal static List<string> ParseCreateTreatingFacility(string rawData)
        {
            List<string> rlist = new List<string>();
            if (rawData == null)
            {
                rlist.Add("error");
                rlist.Add("VistA error!");
                return rlist;
            }
            string results = rawData.ToString();
            string[] lines = Regex.Split(results, "\r\n");
            if ((lines != null) && (lines.Length > 0))
            {
                if (lines[0] == "1")
                {
                    string[] fields = lines[1].Split('^');
                    if ((fields != null) && (fields.Length > 0))
                    {
                        rlist.Add("success");
                        //rlist.Add(fields[1].ToString());
                    }
                }
                else
                {
                    string[] fields = lines[0].Split('^');
                    if ((fields != null) && (fields.Length > 0))
                    {
                        if (fields[0] == "-1")
                        {
                            rlist.Add("error");
                            if (fields[1] != null)
                            {
                                rlist.Add(fields[1].ToString());
                            }
                        }
                    }
                }
            }
            return rlist;
        }

    }
}