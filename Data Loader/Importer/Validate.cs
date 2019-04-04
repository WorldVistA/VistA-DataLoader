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

namespace DataLoader
{
    using DataLoader.Broker;
    using DataLoader.Common.Model;
    using System.Text.RegularExpressions;
    using System.IO;

    class Validate
    {
            
        public string DOBFormat(string DOB)
        {
            if (DOB == "" || DOB.Length == 0) return DOB;
            string dob = DOB.Substring(0, 1);
            switch (dob)
            {
                case "1":
                    {
                        dob = "18";
                        break;
                    }
                case "2":
                    {
                        dob = "19";
                        break;
                    }
                case "3":
                    {
                        dob = "20";
                        break;
                    }
                default:
                    {
                        dob = "";
                        break;
                    }
            }
            dob = DOB.Substring(3, 2) + "/" + DOB.Substring(5, 2) + "/" + dob + DOB.Substring(1, 2);
            int pos = DOB.IndexOf(".");
            if (pos > 0)
            {
                string time = DOB.Substring(8, ((DOB.Length - 1) - pos));
                if (time.Length > 2)
                {
                    string min = time.Insert(2, ":");
                    if (min.Length > 5) min = min.Substring(0, 5);
                    if (min.Length == 4) min = min + "0";
                    dob = dob + " " + min;
                }
                else
                {
                    dob = dob + " " + time + ":00";
                }
            }
            return dob;
        }

        public string VerifyandSetDateTime(Client passclient, string txtDate, bool bolTime)
        {
            Client client = passclient;
            if (txtDate.Length > 0)
            {
                string chkdt;
                Response response = client.CurrentConnection.Execute(new Request { MethodName = "ORWU DT" }.AddParameter(txtDate));
                if (response != null && response.RawData.Length > 0 && response.RawData != "-1")
                {
                    chkdt = DOBFormat(response.RawData);
                    try
                    {
                        DateTime passdate = Convert.ToDateTime(chkdt);
                        if (bolTime == true) chkdt = passdate.ToString("MMM dd, yyyy@HH:mm");
                        else chkdt = passdate.ToString("MMM dd, yyyy");
                        txtDate = chkdt;
                    }
                    catch
                    {
                        txtDate = "";
                        return txtDate;
                    }
                }
                else
                {
                    txtDate = "";
                }
            }

            return txtDate;
        }
    }
}
