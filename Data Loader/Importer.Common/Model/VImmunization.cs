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
    public class VImmunizationMapping
    {
        private string[,] map = new string[,]
        {
            {"Case name","CASE"},
            {"Immunization","IZ"},
            {"DateTime","DATETIME"},
            {"Provider","PROVIDER"},
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

        public string QRDParentNode = "substanceAdministration";
        public string QRDParentNode1 = "procedure"; // test pnt 33, for qrdmapping1
        
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
            maplist.Add("templateId/@root^F^^2.16.840.1.113883.10.20.22.4.16^^^"); //medication activity template
            maplist.Add("consumable/manufacturedProduct/templateId/@root^F^^2.16.840.1.113883.10.20.22.4.23^^^"); //medication information template
            maplist.Add("consumable/manufacturedProduct/manufacturedMaterial/code/@code^D^Immunization^^codeSystem^^Immunization");
            maplist.Add("effectiveTime/low/@value^D^DateTime^^^^");
            maplist.Add("^S^Provider^^^CQM,HISTORICAL MD^");
            maplist.Add("^S^Pat SSN^^^^");
            return maplist;
        }

        public List<string> qrdmapping1()
        {
            //<procedure classCode="PROC" moodCode="EVN" >
            //<!--  Procedure performed template -->
            //<templateId root="2.16.840.1.113883.10.20.24.3.64"/>
            //<!-- Procedure Activity Procedure-->
            //<templateId root="2.16.840.1.113883.10.20.22.4.14"/>
            //<id root="1.3.6.1.4.1.115" extension="53ada33b1d41c85827000151"/>
            //<code code="442333005" codeSystem="2.16.840.1.113883.6.96" sdtc:valueSet="2.16.840.1.113883.3.526.3.402"></code>
            //<text>Procedure, Performed: Influenza Vaccination (Code List: 2.16.840.1.113883.3.526.3.402)</text>
            //<statusCode code="completed"/>
            //<effectiveTime>
            //<low value='20120130120000'/>
            List<string> maplist = new List<string>();
            maplist.Add("templateId/@root^F^^2.16.840.1.113883.10.20.24.3.64^^^"); //procedure performed template
            maplist.Add("templateId/@root^F^^2.16.840.1.113883.10.20.22.4.14^^^"); //Procedure Activity Procedure
            maplist.Add("code/@code^D^Immunization^^codeSystem^^Immunization");
            maplist.Add("effectiveTime/low/@value^D^DateTime^^^^");
            maplist.Add("^S^Provider^^^CQM,HISTORICAL MD^");
            maplist.Add("^S^Pat SSN^^^^");
            return maplist;

        }

        public string GetVImmunizationType(string codesystem, string code)
        {
            string result = "";
            string icode = "";
            string iHFactor = "";
            string[,] list = null;
            //
            if (codesystem == "2.16.840.1.113883.12.292")
            {
                list = CvxMap;
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
                iHFactor = list[i, 1];
                if (icode == code)
                {
                    result = iHFactor;
                    break;
                }
            }
            return result;
        }

        private string[,] CvxMap = new string[,]
        {
            {"3","MEASLES,MUMPS,RUBELLA (MMR)"},
            {"8","HEPATITIS B"},
            {"10","POLIOMYELITIS"},
            {"20","DIP-TET-a/PERT"},
            {"21","VARICELLA"},
            {"33","PNEUMOCOCCAL CONJUGATE PCV23"},
            {"48","HIB,PRP-T"},
            {"83","HEPA,PED/ADOL-2"},
            {"104","HEPA/HEPB ADULT"},
            {"111","FLU,NASAL"},
            {"116","ROTOVIRUS,ORAL"},
            {"140","INFLUENZA"}
            //{"100","pneumococcal conjugate vaccine, 7 valent"},
            //{"106","diptheria, tetnus toxoids and acellular pertussis vaccine, 5 pertussis antigens"}
            //{"120","DTaP-Hib - IPV"}
        };

        private string[,] SnomedMap = new string[,]
        {
            {"442333005","INFLUENZA"}
        };

    }
    
    
}
