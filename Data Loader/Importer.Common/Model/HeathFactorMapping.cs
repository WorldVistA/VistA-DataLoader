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
using System.Linq;
using System.Text;

namespace DataLoader.Common.Model
{
    public class HealthFactorMapping
    {
        private string[,] map = new string[,]
        {
            {"Case name","CASE"},
            {"Health Factor","HFACTOR"},
            {"DateTime","DATETIME"},
            {"Provider","PROVIDER"},
            {"Comment","COMMENT"},
            {"Severity","SEVERITY"},
            {"Pat SSN","PAT_SSN"}
        };

        public string ExcellMapping(string header)
        {
            string excell;
            string vista;
            string result = "";
            for (int i = 0; i <= map.GetUpperBound(0); i++)
            {
                excell = map[i, 0];
                vista = map[i, 1];
                if (header == excell)
                {
                    result = vista;
                    break;
                }
            }
            return result;
        }

        public string QRDParentNode = "encounter";    // paired with qrdmapping
        public string QRDParentNode2 = "observation"; // paired with qrdmapping2-4, 8
        public string QRDParentNode5 = "procedure";   // paired with qrdmapping5
        public string QRDParentNode6 = "act";         // paired with qrdmapping6,7
        public string QRDParentNode9 = "encounter";   // paried with qrdmaping9

        public List<string> qrdmapping() //22
        {
            //xpath^type^column^match^matchAttrib^matchSubValue^matchFunction
            // where:   xpath   (required) is relative xpath for data
            //           type   (required) 'F' for filter, 'D' for data
            //         column   (required) is the datagrid column where data is place in client
            //          match   (optional) is a value to used to confirm specified element or position
            //    matchAttrib   (optional) is the attribute used to confirm matches
            //    matchSubval   (optional) is the value stored if match is made
            //  matchFunction   (optional) for complex operations, calls special functions
            //
            List<string> maplist = new List<string>();
            maplist.Add("templateId/@root^F^^2.16.840.1.113883.10.20.22.4.40^^^"); //Plan of Care Activity Encounter template
            maplist.Add("templateId/@root^F^^2.16.840.1.113883.10.20.24.3.22^^^"); //Encounter order template
            maplist.Add("code/@code^D^Health Factor^^^^HealthFactor");
            maplist.Add("author/time/@value^D^DateTime^^^^");
            maplist.Add("author/time/@value^D^Comment^^^^");
            maplist.Add("^S^Provider^^^CQM,HISTORICAL MD^");
            maplist.Add("^S^Pat SSN^^^^");
            return maplist;
        }

        public List<string> qrdmapping2() //16
        {
            //
            List<string> maplist = new List<string>();
            maplist.Add("templateId/@root^F^^2.16.840.1.113883.10.20.22.4.2^^^");
            maplist.Add("templateId/@root^F^^2.16.840.1.113883.10.20.24.3.20^^^");
            maplist.Add("code/@code^D^Health Factor^^^^HealthFactor");
            //maplist.Add("value/@code^D^Health Factor^^^^HealthFactor");
            maplist.Add("effectiveTime/low/@value^D^DateTime^^^^");
            maplist.Add("text^D^Comment^^^^");
            maplist.Add("^S^Provider^^^CQM,HISTORICAL MD^");
            maplist.Add("^S^Pat SSN^^^^");
            return maplist;
        }

        public List<string> qrdmapping3()
        {
            List<string> maplist = new List<string>();
            maplist.Add("templateId/@root^F^^2.16.840.1.113883.10.20.22.4.67^^^"); // Functional Status Result Observation
            maplist.Add("templateId/@root^F^^2.16.840.1.113883.10.20.24.3.28^^^"); // Functional Status, Result template
            maplist.Add("code/@code^D^Health Factor^^^^HealthFactor");
            maplist.Add("effectiveTime/low/@value^D^DateTime^^^^");
            maplist.Add("text^D^Comment^^^^");
            maplist.Add("^S^Provider^^^CQM,HISTORICAL MD^");
            maplist.Add("^S^Pat SSN^^^^");
            return maplist;
        }

        public List<string> qrdmapping4()
        {
            List<string> maplist = new List<string>();
            maplist.Add("templateId/@root^F^^2.16.840.1.113883.10.20.22.4.69^^^"); // Assessment Scale Observation
            maplist.Add("templateId/@root^F^^2.16.840.1.113883.10.20.24.3.69^^^"); // Risk Category Assessment
            maplist.Add("code/@code^D^Health Factor^^^^HealthFactor");
            maplist.Add("effectiveTime/low/@value^D^DateTime^^^^");
            maplist.Add("text^D^Comment^^^^");
            maplist.Add("^S^Provider^^^CQM,HISTORICAL MD^");
            maplist.Add("^S^Pat SSN^^^^");
            return maplist;
        }

        public List<string> qrdmapping5()
        {
            List<string> maplist = new List<string>();
            maplist.Add("templateId/@root^F^^2.16.840.1.113883.10.20.24.3.64^^^"); // Procedure Performed template
            maplist.Add("templateId/@root^F^^2.16.840.1.113883.10.20.22.4.14^^^"); // Procedure Activity
            maplist.Add("code/@code^D^Health Factor^^^^HealthFactor");
            maplist.Add("effectiveTime/low/@value^D^DateTime^^^^");
            maplist.Add("text^D^Comment^^^^");
            maplist.Add("^S^Provider^^^CQM,HISTORICAL MD^");
            maplist.Add("^S^Pat SSN^^^^");
            return maplist;
        }

        public List<string> qrdmapping6()
        {
            List<string> maplist = new List<string>();
            maplist.Add("templateId/@root^F^^2.16.840.1.113883.10.20.22.3.4^^^"); // Communication to Provider
            maplist.Add("code/@code^D^Health Factor^^^^HealthFactor");
            maplist.Add("effectiveTime/low/@value^D^DateTime^^^^");
            maplist.Add("text^D^Comment^^^^");
            maplist.Add("^S^Provider^^^CQM,HISTORICAL MD^");
            maplist.Add("^S^Pat SSN^^^^");
            return maplist;
        }

        public List<string> qrdmapping7()
        {
            List<string> maplist = new List<string>();
            maplist.Add("templateId/@root^F^^2.16.840.1.113883.10.20.22.4.12^^^"); // Consolidation CDA: Procedure Activity Act template
            maplist.Add("templateId/@root^F^^2.16.840.1.113883.10.20.24.3.32^^^");
            maplist.Add("code/@code^D^Health Factor^^^^HealthFactor");
            maplist.Add("effectiveTime/low/@value^D^DateTime^^^^");
            maplist.Add("text^D^Comment^^^^");
            maplist.Add("^S^Provider^^^CQM,HISTORICAL MD^");
            maplist.Add("^S^Pat SSN^^^^");
            return maplist;
        }

        public List<string> qrdmapping8() //44, parentnode=observation
        {
            List<string> maplist = new List<string>();
            maplist.Add("templateId/@root^F^^2.16.840.1.113883.10.20.24.3.88^^^"); // REASON template
            maplist.Add("code/@code^F^^410666004^^^"); //'Reason for' snomed code
            maplist.Add("value/@code^D^Health Factor^^^^HealthFactor");
            maplist.Add("effectiveTime/@value^D^DateTime^^^^");
            maplist.Add("text^D^Comment^^^^");
            maplist.Add("^S^Provider^^^CQM,HISTORICAL MD^");
            maplist.Add("^S^Pat SSN^^^^");
            return maplist;
        }

        public List<string> qrdmapping9() //22,44 ER Encounters (ED ARRIVAL TIME, ED DEPARTURE TIME)
        {
            List<string> maplist = new List<string>();
            maplist.Add("templateId/@root^F^^2.16.840.1.113883.10.20.22.4.49^^^");
            maplist.Add("templateId/@root^F^^2.16.840.1.113883.10.20.24.3.23^^^");
            maplist.Add("code/@code^F^^4525004^^^");
            maplist.Add("^S^Health Factor^^^ED ARRIVAL TIME^");
            maplist.Add("effectiveTime/low/@value^D^DateTime^^^^");
            maplist.Add("effectiveTime/low/@value^D^Comment^^^^");
            maplist.Add("^S^Provider^^^CQM,HISTORICAL MD^");
            maplist.Add("^S^Pat SSN^^^^");
            return maplist;
        }

        public List<string> qrdmapping10() //22,44 ER Encounters (ED ARRIVAL TIME, ED DEPARTURE TIME)
        {
            List<string> maplist = new List<string>();
            maplist.Add("templateId/@root^F^^2.16.840.1.113883.10.20.22.4.49^^^");
            maplist.Add("templateId/@root^F^^2.16.840.1.113883.10.20.24.3.23^^^");
            maplist.Add("code/@code^F^^4525004^^^");
            maplist.Add("^S^Health Factor^^^ED DEPARTURE TIME^");
            maplist.Add("effectiveTime/high/@value^D^DateTime^^^^");
            maplist.Add("effectiveTime/high/@value^D^Comment^^^^");
            maplist.Add("^S^Provider^^^CQM,HISTORICAL MD^");
            maplist.Add("^S^Pat SSN^^^^");
            return maplist;
        }
        public string GetHealthFactor(string codesystem, string code)
        {
            string result = "";
            string icode = "";
            string iHFactor = "";
            string[,] list = null;
            if (code == "") return result;
            //
            if (codesystem == "2.16.840.1.113883.6.96")
            {
                list = SnomedMap;
            }
            if (codesystem == "2.16.840.1.113883.6.1")
            {
                list = LoincMap;
            }
            //
            if (list == null) return result;
            //
            for (int i = 0; i <= list.GetUpperBound(0); i++)
            {
                icode = list[i, 0];
                iHFactor = list[i, 1];
                if (icode == code)
                {
                    result = iHFactor;
                    break;
                }
            }
            return result;
        }

        private string[,] SnomedMap = new string[,]
        {
            //{"312903003","?"},
            {"4525004","ED [ARRIVAL-DEPARTURE] TIME"},
            {"1748006","VTE COMFIRMED"},
            {"10378005","TIME DECISION TO ADMIT MADE"},
            {"225337009","SUICIDE RISK ASSESSMENT"},
            {"428191000124101","DOCUMENTATION OF CURRENT MEDS"},
            {"371530004","CLINICAL CONSULTATION REPORT"},
            {"428171000124102","ADOLESCENT DEPRESSION SCREEN NEGATIVE"},
            {"428231000124106","MATERNAL POSTPARTUM DEPRESSION CARE"},
            {"428341000124108","MACULAR EDEMA ABSENT"},
            {"193350004","MACULAR EDEMA PRESENT"},
            {"417886001","TREATMENT ADJUSTED PER PROTOCOL"},
            {"105480006","REFUSAL OF TREATMENT BY PATIENT"},
            {"413318004","PATIENT GIVEN WRITTEN INFORMATION"},
            {"226789007","BREAST MILK ADMINISTERED"},
            {"105480006","PATIENT REFUSED TREATMENT"},
            {"428171000124102","ADULT DEPRESSION SCREEN"},
            {"183932001","PROCEDURE CONTRAINDICATED"}
        };

        private string[,] LoincMap = new string[,]
        {
            {"38208-5","STANDARD PAIN ASSMNT TOOL"},
            {"44249-1","PHQ-9 RESULT"},
            {"71955-9","PROMIS-29: "},
            {"71938-5","MLHFQ"},
            {"71955-9","PROMIS-29"}, 
            {"58151-2","BIMS SCORE"},
            {"73830-2","FALL RISK ASSESSMENT"},
            {"73831-0","ADOLESCENT DEPRESSION SCREEN"},
            {"73832-8","ADULT DEPRESSION SCREEN"},
            {"69981-9","ASTHMA ACTION PLAN"}
        };
    } 
}
