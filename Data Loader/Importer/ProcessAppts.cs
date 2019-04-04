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
    using DataLoader.Broker;
    using DataLoader.Common.Model;
    using System.IO;

    internal class ProcessAppts
    {

        internal static List<int> Process(string DFN, string PatientName, List<string> appt, Client client, StreamWriter log)
        {
            List<string> apptsend = new List<string>();
            List<int> retrn = new List<int>();
            string CLIN = "";
            string DT = "";
            int success = 0;
            int errors = 0;
            foreach (string apptlist in appt)
            {
                apptsend.Clear();
                string[] item = apptlist.Split('^');
                if ((item != null) && (item.Length >= 2))
                {
                    if (item[0].ToString() == "CLIN")
                        CLIN = item[1].ToString();
                    if (item[0].ToString() == "ADATE")
                        DT = item[1].ToString();
                }
                if (CLIN != "" && DT != "")
                {
                    apptsend.Add("PATIENT^" + DFN);
                    apptsend.Add("ADATE^" + DT);
                    apptsend.Add("CLIN^" + CLIN);
                    Response response = client.CurrentConnection.Execute(new Request { MethodName = "ISI IMPORT APPT" }.AddParameter(apptsend));
                    List<string> results = ResponseParser.ParseCreateApptResponse(response.RawData);
                    if (results[0] == "success")
                    {
                        success++;
                        string logtxt = DateTime.Now.ToString() + " Successfully created appt for " + PatientName + " at " + CLIN + " at " + DT + "\r\n";
                        log.Write(logtxt);
                    }
                    if (results[0] == "error")
                    {
                        errors++;
                        string logtxt = DateTime.Now.ToString() + " Error when attempting to create appt " + CLIN + "@" + DT + " -- " + results[1];
                        log.WriteLine(logtxt);
                    }
                }
            }
            retrn.Add(success);
            retrn.Add(errors);
            return retrn;
        }
    }
}