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
    public class LabMapping
    {
        private string[,] map = new string[,]
        {
            {"Case name","CASE"},
            {"Pat SSN","PAT_SSN"},
            {"Lab test","LAB_TEST"},
            {"Result date","RESULT_DT"},
            {"Result value","RESULT_VAL"},
            {"Location","LOCATION"}
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
            //           type   (required) 'F' for filter, 'D' for data, 'S' for static
            //         column   (required) is the datagrid column where data is place in client
            //          match   (optional) is a value to used to confirm specified element or position
            //    matchAttrib   (optional) is the attribute used to confirm matches
            //    matchSubval   (optional) is the value stored if match is made [OR] for Static value
            //  matchFunction   (optional) for complex operations, calls special functions
            //
            List<string> maplist = new List<string>();
            maplist.Add("templateId/@root^F^^2.16.840.1.113883.10.20.22.4.2^^^"); //Result Observation (consolidation) template
            maplist.Add("templateId/@root^F^^2.16.840.1.113883.10.20.24.3.40^^^"); //Laboratory Test, Result template
            maplist.Add("code/@code^D^Lab test^^^^Lab Test");
            maplist.Add("effectiveTime/low/@value^D^Order date^^^^");
            maplist.Add("effectiveTime/high/@value^D^Result date^^^^");
            maplist.Add("value/@value^D^Result value^^^^");
            maplist.Add("^S^Location^^^CLINIC ONE^");
            maplist.Add("^S^Pat SSN^^^^");
            return maplist;
        }

        public string GetLabTest(string codesystem, string code)
        {
            string result = "";
            string icode = "";
            string iLab = "";
            string[,] list = null;
            //
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
                iLab = list[i, 1];
                if (icode == code)
                {
                    result = iLab;
                    break;
                }
            }
            return result;
        }

        private string[,] LoincMap = new string[,]
        {
            {"2085-9","HDL CHOLESTEROL"},
            {"34714-6","PT/INR"},
            {"2093-3","CHOLESTEROL, TOTAL"},
            {"12773-8","LDL CHOLESTEROL"},
            {"13457-7","LDL CHOLESTEROL"},
            {"13056-7","PLATELET COUNT"},
            {"10524-7","PAP TEST"},
            {"17856-6","HEMOGLOBIN A1C"},
            {"17855-8","HEMOGLOBIN A1C"},
            {"10508-0","PSA"},
            {"10351-5","HIV 1 RNA"},
            {"35266-6","GLEASON SCORE"},
            {"24467-3","CD4 COUNT"},
            {"20447-9","HIV 1 RNA"},
            {"19080-1","PREGNANCY TEST"},
            {"10674-0","HEPATITIS B SURFACE ANTIGEN"},
            {"2093-3","CHOLESTEROL, TOTAL"},
            {"12951-0","TRIGLYCERIDES"},
            {"11268-0","STREPTOZYME"},
            {"13217-5","CHLAMYDIA CULTURE"},
            {"14463-4","CHLAMYDIA CULTURE"}
        };

    }
}
