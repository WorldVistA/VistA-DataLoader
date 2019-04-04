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
    public class EncounterMapping
    {
        private string[,] map = new string[,]
        {
            {"Case name","CASE"},
            {"Type of Encounter","ETYPE"},
            {"Begin DateTime","ADATE"}, // admit or appt/visit dt-tm
            {"Location","LOCATION"}, //ward or clinic
            {"Provider","PROVIDER"},
            {"Room-Bed","RMBD"},
            {"End DateTime","DDATE"},
            {"Procedure","CPT"},
            {"Diagnosis","ADIAG"},
            {"Admission Type","ATYPE"},
            {"Type of Disposition","DTYPE"},
            {"Pat SSN","PAT_SSN"},
            {"Admitting Regulation","ADMREG"},
            {"Facility Treating Specialty","FTSPEC"},
            {"Facility Directory Excluded","FDEXC"},
            {"Diagnosis Description","SHDIAG"}
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
            maplist.Add("code/@code^D^Type of Encounter^^codeSystem^^EncounterType");
            maplist.Add("effectiveTime/low/@value^D^Begin DateTime^^^^");
            maplist.Add("code/@code^D^Location^^codeSystem^^Location");
            maplist.Add("code/@code^D^Room-Bed^^codeSystem^^Room-Bed");
            maplist.Add("^S^Provider^^^CQM,HISTORICAL MD^");
            maplist.Add("effectiveTime/high/@value^D^End DateTime^^^^");
            maplist.Add("sdtc:dischargeDispositionCode/@code^D^Type of Disposition^^^^");
            maplist.Add("^S^Pat SSN^^^^");
            return maplist;
        }

        public string GetEncounterType(string codesystem, string code)
        {
            string result = "";
            string[,] list = null;
            if (codesystem == "2.16.840.1.113883.6.96")
            {
                list = SnomedMap;
            }
            if (codesystem == "2.16.840.1.113883.6.12")
            {
                list = CPTMap;
            }
            if (list == null) return result;
            FindMatch(code, ref result, list);
            return result;
        }

        public string GetEncounterOther(string type, string code)
        {
            string result = "";
            string[,] list = null;
            if (type == "Location")
            {
                list = LocationMap;
            }
            if (type == "Room-Bed")
            {
                list = RoomBedMap;
            }
            if (type == "Provider")
            {
                list = ProviderMap;
            }
            if (type == "AdmittingRegulation")
            {
                list = AdmittingRegulationMap;
            }
            if (type == "FacilityTreatingSpecialty")
            {
                list = FacilityTreatingSpecialtyMap;
            }

            if (list == null) return result;
            FindMatch(code, ref result, list);
            return result;
        }
        //
        private static void FindMatch(string code, ref string result, string[,] list)
        {
            string icode = "";
            string iEncounter = "";
            for (int i = 0; i <= list.GetUpperBound(0); i++)
            {
                icode = list[i, 0];
                iEncounter = list[i, 1];
                if (icode == code)
                {
                    result = iEncounter;
                    break;
                }
            }
        }

        private string[,] SnomedMap = new string[,]
        {
            {"183452005","IP"}, //emergency hospital admission
            {"185349003","OP"}, //outpatient
            {"4525004","ER"},   //emergency
            {"10197000","PS"},
            {"10378005","IP"},  //Hospital admission, emergency, from emergency room, accidental injury (procedure)
            {"108313002","OP"},
            {"171047005","IO"},
            {"32485007","IP"}, //Hospital admission (procedure)
            {"305351004","ICU"},
            {"112689000","IP"}, //Hospital admission, elective, with complete pre-admission work-up (procedure)
            {"8715000","IP"} //Hospital admission, elective (procedure)
        };

        private string[,] CPTMap = new string[,]
        {
            {"92002","OP"},
            {"96150","PS"},
            {"99201","OP"},
            {"99285","ER"},
            {"90791","PS"},
            {"99202","OP"},
            {"99285","ER"},
            {"99381","OP"}
        };

        private string[,] LocationMap = new string[,]
        {
            {"OP","CLINIC A"},
            {"ER","EMERGENCY DEPT"},
            {"IP","TEST WARD 1"},
            {"ICU","ICU"},
            {"PS","CLINIC A"}
        };

        private string[,] RoomBedMap = new string[,]
        {
            {"OP",""},
            {"ER",""},
            {"IP","1-A"},
            {"ICU","0001-A"},
            {"PS",""}
        };

        private string[,] ProviderMap = new string[,]
        {
            {"OP","CQM,HISTORICAL MD"},
            {"ER","CQM,HISTORICAL MD"},
            {"IP","CQM,HISTORICAL MD"},
            {"ICU","CQM,HISTORICAL MD"},
            {"PS","CQM,HISTORICAL MD"}
        };
        private string[,] AdmittingRegulationMap = new string[,]
        {
            {"OP","Admitting Regulation1"},
            {"ER","Admitting Regulation2"},
            {"IP","Admitting Regulation3"},
            {"ICU","Admitting Regulation4"},
            {"PS","Admitting Regulation5"}
        };
        private string[,] FacilityTreatingSpecialtyMap = new string[,]
        {
            {"OP","FacilityTreatingSpecialty1"},
            {"ER","FacilityTreatingSpecialty2"},
            {"IP","FacilityTreatingSpecialty3"},
            {"ICU","FacilityTreatingSpecialty4"},
            {"PS","FacilityTreatingSpecialty5"}
        };
    }
    
    
}
