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
    public class VExamMapping
    {
        private string[,] map = new string[,]
        {
            {"Case name","CASE"},
            {"Exam","EXAM"},
            {"Provider","PROVIDER"},
            {"Comment","COMMENT"},
            {"DateTime","DATETIME"},
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

        public string QRDParentNode = "observation";

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

            maplist.Add("templateId/@root^F^^2.16.840.1.113883.10.20.22.4.13^^^"); 
            maplist.Add("templateId/@root^F^^2.16.840.1.113883.10.20.24.3.59^^^");  //Physical Exam, Performed template
            maplist.Add("code/@code^D^Exam^^codeSystem^^Exam");
            maplist.Add("^S^Provider^^^CQM,HISTORICAL MD^");
            maplist.Add("effectiveTime/low/@value^D^DateTime^^^^");
            maplist.Add("^S^Pat SSN^^^^");
            return maplist;
        }

        public List<string> qrdmapping1()
        {
            List<string> maplist = new List<string>();
            maplist.Add("templateId/@root^F^^2.16.840.1.113883.10.20.22.4.2^^^");
            maplist.Add("templateId/@root^F^^2.16.840.1.113883.10.20.24.3.20^^^");  //Diagnostic Study Result template
            maplist.Add("code/@code^D^Exam^^codeSystem^^Exam");
            maplist.Add("^S^Provider^^^CQM,HISTORICAL MD^");
            maplist.Add("effectiveTime/low/@value^D^DateTime^^^^");
            maplist.Add("^S^Pat SSN^^^^");
            return maplist;
        }

        public string GetVExamType(string codesystem, string code)
        {
            string result = "";
            string icode = "";
            string iExam = "";
            string[,] list = null;
            //
            if (codesystem == "2.16.840.1.113883.6.1")
            {
                list = LoincMap;
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
                iExam = list[i, 1];
                if (icode == code)
                {
                    result = iExam;
                    break;
                }
            }
            return result;
        }

        private string[,] LoincMap = new string[,]
        {
            {"24604-1","MAMMOGRAM"},
            {"32451-7","MACULAR EXAM"},
            {"44249-1","PHQ-9 RESULT"},
            {"54108-6","NEONATAL HEARING EXAM"},
            {"54109-4","NEONATAL HEARING EXAM"},
            {"57254-5","FALL RISK ASSESSMENT"},
            {"58151-2","BIMS SCORE"},
            {"65853-4","CARDIOVASCULAR RISK"},
            {"71484-0","CUP TO DISK RATIO EXAM"},
            {"71486-5","OPTIC DISK EXAM"},
            {"71938-5","MLHFQ"},
            {"71955-9","PROMIS-29:"},
            {"73831-0","ADOLESCENT DEPRESSION SCREEN NEGATIVE"}
        };

        private string[,] SnomedMap = new string[,]
        {
            {"225337009","SUICIDE RISK ASSESSMENT"},
            {"91161007","PULSE FOOT EXAM"},
            {"134388005","DIABETIC FOOT EXAM"},
            {"252779009","DILATED EYE EXAM"},
            {"401191002","DIABETIC FOOT CHECK"},
            {"419775003","VISION EXAM"},
            {"412726003","LENGTH OF GESTATION AT BIRTH"},
            {"444135009","ESTIMATED FETAL GESTATIONAL AGE"},
            {"417491009","NEONATAL HEARING EXAM"}
            //{"134388005","MONOFILAMENT FOOT SENSATION"},
            //{"252779009","DILATED EYE EXAM"},
            //{"401191002","VISUAL FOOT EXAM"},
            //{"91161007","PULSE FOOT EXAM"}
        };

    }
    
    
}
