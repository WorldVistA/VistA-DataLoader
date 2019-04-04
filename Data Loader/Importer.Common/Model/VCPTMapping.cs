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
    public class VCptMapping
    {
        private string[,] map = new string[,]
        {
            {"Case name","CASE"},
            {"Cpt","CPT"},
            {"Provider Narrative","PROVIDER_NARRATIVE"},
            {"DateTime","DATETIME"},
            {"Provider","PROVIDER"},
            {"Comment","COMMENT"},
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

        public string QRDParentNode = "encounter";
        
        public string QRDParentNode1 = "procedure";

        public List<string> qrdmapping()
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
            maplist.Add("templateId/@root^F^^2.16.840.1.113883.10.20.22.4.49^^^");
            maplist.Add("templateId/@root^F^^2.16.840.1.113883.10.20.24.3.23^^^");
            maplist.Add("code/@code^D^Cpt^^codeSystem^^Procedure");
            maplist.Add("^S^Provider Narrative^^^OTHER^");
            maplist.Add("effectiveTime/low/@value^D^DateTime^^^^");
            maplist.Add("^S^Provider^^^CQM,HISTORICAL MD^");
            maplist.Add("^S^Pat SSN^^^^");
            return maplist;
        }

        public List<string> qrdmapping1()
        {
            List<string> maplist = new List<string>();
            maplist.Add("templateId/@root^F^^2.16.840.1.113883.10.20.24.3.64^^^");
            maplist.Add("templateId/@root^F^^2.16.840.1.113883.10.20.22.4.14^^^");
            maplist.Add("code/@code^D^Cpt^^codeSystem^^Procedure");
            maplist.Add("^S^Provider Narrative^^^OTHER^");
            maplist.Add("effectiveTime/low/@value^D^DateTime^^^^");
            maplist.Add("^S^Provider^^^CQM,HISTORICAL MD^");
            maplist.Add("^S^Pat SSN^^^^");
            return maplist;
        }

        // Diagnostic Study Performed
        //public List<string> qrdmapping2()
        //{
        //    List<string> maplist = new List<string>();
        //    maplist.Add("templateId/@root^F^^2.16.840.1.113883.10.20.22.4.13^^^");
        //    maplist.Add("templateId/@root^F^^2.16.840.1.113883.10.20.24.3.18^^^");
        //    maplist.Add("code/@code^D^Cpt^^codeSystem^^Procedure");
        //    maplist.Add("^S^Provider Narrative^^^OTHER^");
        //    maplist.Add("effectiveTime/low/@value^D^DateTime^^^^");
        //    maplist.Add("^S^Provider^^^CQM,HISTORICAL MD^");
        //    maplist.Add("^S^Pat SSN^^^^");
        //    return maplist;
        //}

        public string GetVCptType(string codesystem, string code)
        {
            string result = "";
            string icode = "";
            string iCPT = "";
            string[,] list = null;
            //
            if (codesystem == "2.16.840.1.113883.6.12")
            {
                list = CPTMap;
            }
            if (codesystem == "2.16.840.1.113883.6.96")
            {
                list = SnomedMap;
            }
            //
            if (list == null) return result;
            //
            for (int i = 0; i <= list.GetUpperBound(0); i++)
            {
                icode = list[i, 0];
                iCPT = list[i, 1];
                if (icode == code)
                {
                    if (codesystem == "2.16.840.1.113883.6.12") result = code;
                    if (codesystem == "2.16.840.1.113883.6.96") result = iCPT;
                    break;
                }
            }
            return result;
        }

        private string[,] CPTMap = new string[,]
        {
            {"99201","OP"},
            {"90791","PS"},
            {"99202","OP"},
            {"99381","OP"}
        };

        private string[,] SnomedMap = new string[,]
        {
            {"10492003","55810"},
            {"84755001","77427"},
            {"234723000","D1206"},
            {"10745001","59400"},
            {"10178000","66840"},
            {"105355005","99408"},
            {"12350003","44388"},
            {"15163009","27130"},
            {"177184002","59409"},
            {"179344006","27447"},
            {"13767004","65235"},
            {"108241001","90937"},
            {"185349003","99202"},
            {"442333005","90653"},
            {"444783004","45378"},
            {"4525004","99285"},
            {"185349003","99381"},
            //{"164847006","794.31"}
        };

    }
    
    
}
