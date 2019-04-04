﻿// VistA Data Loader 2.0
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
    public class Template
    {
        public string strName { get; set; }
        public string strNameInt { get; set; }
        public string strType { get; set; }
        public string strTypeInt { get; set; }
        public string strNameMask { get; set; }
        public string strSsnMask { get; set; }
        public string strSex { get; set; }
        public string strEDOB { get; set; }
        public string strEDOBInt { get; set; }
        public string strLDOB { get; set; }
        public string strLDOBInt { get; set; }
        public string strMaritalStat { get; set; }
        public string strMaritalStatInt { get; set; }
        public string strZIP4 { get; set; }
        public string strPHMask { get; set; }
        public string strCity { get; set; }
        public string strState { get; set; }
        public string strStateInt { get; set; }
        public string strVeteran { get; set; }
        public string strDfnName { get; set; }
        public string strEmployStat { get; set; }
        public string strEmployStatInt { get; set; }
        public string strService { get; set; }
        public string strServiceInt { get; set; }
        public string strEmail { get; set; }
        public string strUserMask { get; set; }
        public string strESigApnd { get; set; }
        public string strAcccessApnd { get; set; }
        public string strVerifyApnd { get; set; }

        public void Clear()
        {
            strName = null;
            strNameInt = null;
            strType = null;
            strTypeInt = null;
            strNameMask = null;
            strSsnMask = null;
            strSex = null;
            strEDOB = null;
            strEDOBInt = null;
            strLDOB = null;
            strLDOBInt = null;
            strMaritalStat = null;
            strMaritalStatInt = null;
            strZIP4 = null;
            strPHMask = null;
            strCity = null;
            strState = null;
            strStateInt = null;
            strVeteran = null;
            strDfnName = null;
            strEmployStat = null;
            strEmployStatInt = null;
            strService = null;
            strServiceInt = null;
            strEmail = null;
            strUserMask = null;
            strESigApnd = null;
            strAcccessApnd = null;
            strVerifyApnd = null;
        }
    }

}
