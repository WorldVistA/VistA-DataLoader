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
    public class PntMapping
    {
        private string[,] map = new string[,]
        {
            {"Case name","CASE"},
            {"Last name","NAME"},
            {"First name","NAME"},
            {"Sex","SEX"},
            {"Gender","SEX"},
            {"dob","DOB"},
            {"DOB","DOB"},
            {"Race","RACE"},
            {"Ethnicity","ETHNICITY"},
            {"Employment status","EMPLOY_STAT"},
            {"Insurance","INSUR_TYPE"},
            {"Occupation","OCCUPATION"},
            {"US veteran","VETERAN"},
            {"Marital status","MARITAL_STATUS"},
            {"SSN","SSN"},
            {"Addr1","STREET_ADD1"},
            {"Addr2","STREET_ADD2"},
            {"city","CITY"},
            {"City","CITY"},
            {"State","STATE"},
            {"Zip","ZIP_4"},
            {"Phone","PH_NUM"}
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

        public string[,] QrdaMapping()
        {

            return QRDmap;
        }

        public string QRDParentNode = "patientRole/";

        private string[,] QRDmap = new string[,]
        {
            //{xpath , column }
            {"","Case name"},
            {"patient/name/family","Last name"},
            {"patient/name/given","First name"},
            {"patient/administrativeGenderCode/@code","Gender"},
            {"patient/birthTime/@value","DOB"},
            {"patient/raceCode/@code","Race"},
            {"patient/ethnicGroupCode/@displayName","Ethnicity"},
            {"addr/city","city"},
            {"addr/state","State"},
            {"addr/postalCode","Zip"},
            {"telecom/@value","Phone"},
            {"patient/languageCommunication/languageCode/@code","Language"},
            {"","Insurance"}
        };


        public string DemographMapping(string type, string value)
        {
            string invalue;
            string outvalue;
            string result = "";
            string[,] list = null;
            if (type == "Ethnicity") list = EthnicityMap;
            if (type == "Race") list = RaceHLCode;
            if (list == null) return result;
            //
            for (int i = 0; i <= list.GetUpperBound(0); i++)
            {
                invalue = list[i, 0];
                outvalue = list[i, 1];
                if (value == invalue)
                {
                    result = outvalue;
                    break;
                }
            }
            if (result == "") result = "DECLINED TO SPECIFY";
            return result;
        }

        private string[,] EthnicityMap = new string[,]
        {
            {"Declined to answer","DECLINED TO ANSWER"},
            {"Hispanic or Latino","HISPANIC OR LATINO"},
            {"Not Hispanic or Latino","NOT HISPANIC OR LATINO"},
            {"Unknown","UNKNOWN BY PATIENT"}
        };

        // by 'display name' in Cypress set
        private string[,] RaceMap = new string[,]
        {
            {"American Indian or Alaska Native","AMERICAN INDIAN OR ALASKA NATI"},
            {"Asian","ASIAN"},
            {"Black or African American","BLACK OR AFRICAN AMERICAN"},
            {"Native Hawaiian or other Pacific Islander","NATIVE HAWAIIAN OR OTHER PACIFIC ISLANDER"},
            {"White","WHITE"}
        };

        private string[,] RaceHLCode = new string[,]
        {
            //hl7 code, vista value
            {"2054-5","BLACK OR AFRICAN AMERICAN"},
            {"1002-5","AMERICAN INDIAN OR ALASKA NATIVE"},
            {"2076-8","NATIVE HAWAIIAN OR OTHER PACIFIC ISLANDER"},
            {"2028-9","ASIAN"},
            {"2106-3","WHITE"},
            {"9999-4","UNKNOWN BY PATIENT"}
        };
    }
}
