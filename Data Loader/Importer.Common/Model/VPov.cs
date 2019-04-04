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
    public class VPovMapping
    {
        private string[,] map = new string[,]
        {
            {"Case name","CASE"},
            {"ICD9","ICD9"},
            {"DateTime","DATETIME"},
            {"Provider","PROVIDER"},
            {"Primary-Secondary","PRIMSEC"},
            {"Modifier","MODIFIER"},
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

        // ****************************************************************************
        // Note -- For QRDA files, VPOV is also fed directly through the PROBLEMS entries
        //         See ProblemMapping.cs for qrda mappings. The VPOV="Y" entry toggles
        //         whether attempts to populate the V POV file are made.  Like all
        //         OP Encounter (V) related entries an exact DATETIME-to-VISIT match is
        //         required.
        // ****************************************************************************

        public string QRDParentNode = "observation";

        // Diagnostic Study Performed
        public List<string> qrdmapping()
        {
            List<string> maplist = new List<string>();
            maplist.Add("templateId/@root^F^^2.16.840.1.113883.10.20.22.4.13^^^");
            maplist.Add("templateId/@root^F^^2.16.840.1.113883.10.20.24.3.18^^^");
            maplist.Add("code/@code^D^ICD9^^codeSystem^^Diagnosis");
            maplist.Add("effectiveTime/low/@value^D^DateTime^^^^");
            maplist.Add("^S^Provider^^^CQM,HISTORICAL MD^");
            maplist.Add("^S^Primary-Secondary^^^S^"); //secondary
            maplist.Add("code/@code^D^Comment^^^^");
            maplist.Add("^S^Pat SSN^^^^");
            return maplist;
        }

        // Diagnostic Study Result
        public List<string> qrdmapping1()
        {
            List<string> maplist = new List<string>();
            maplist.Add("templateId/@root^F^^2.16.840.1.113883.10.20.22.4.2^^^");
            maplist.Add("templateId/@root^F^^2.16.840.1.113883.10.20.24.3.20^^^");
            maplist.Add("code/@code^D^ICD9^^codeSystem^^Diagnosis");
            maplist.Add("effectiveTime/low/@value^D^DateTime^^^^");
            maplist.Add("^S^Provider^^^CQM,HISTORICAL MD^");
            maplist.Add("^S^Primary-Secondary^^^S^");
            maplist.Add("code/@code^D^Comment^^^^");
            maplist.Add("^S^Pat SSN^^^^");
            return maplist;
        }

        public string GetCID9Type(string codesystem, string code)
        {
            string result = "";
            string icode = "";
            string iICD9 = "";
            string[,] list = null;
            //
            if (codesystem == "2.16.840.1.113883.6.96")
            {
                list = SnomedMap;
            }
            //
            if (codesystem == "2.16.840.1.113883.6.103") result = code;
            //
            if (list == null) return result;
            //
            for (int i = 0; i <= list.GetUpperBound(0); i++)
            {
                icode = list[i, 0];
                iICD9 = list[i, 1];
                if (icode == code)
                {
                    result = iICD9;
                    break;
                }
            }
            return result;
        }

        private string[,] SnomedMap = new string[,]
        {
            {"10273003","410.00"}
        };
    }
    
    
}
