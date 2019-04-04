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
    public class PatientLoad
    {
        public string Template { get; set; }
        public string ImpType { get; set; }
        public string ImpBatchNum { get; set; }
        public string DfnName { get; set; }
        public string Type { get; set; }
        public string Name { get; set; }
        public string NameMask { get; set; }
        public string Sex { get; set; }
        public string DOB { get; set; }
        public string Race { get; set; }
        public string Ethnicity { get; set; }
        public string LowDob { get; set; }
        public string UpDob { get; set; }
        public string MaritalStatus { get; set; }
        public string Occupation { get; set; }
        public string SSN { get; set; }
        public string SSNMask { get; set; }
        public string StreetAdd1 { get; set; }
        public string StreetAdd2 { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string Zip4 { get; set; }
        public string Zip4Mask { get; set; }
        public string PhNum { get; set; }
        public string PhNumMask { get; set; }
        public string EmployStat { get; set; }
        public string InsurType { get; set; }
        public string Veteran { get; set; }

        public void Clear()
        {
            Template = null;
            ImpType = null;
            ImpBatchNum = null;
            DfnName = null;
            Type = null;
            Name = null;
            NameMask = null;
            Sex = null;
            DOB = null;
            Race = null;
            Ethnicity = null;
            LowDob = null;
            UpDob = null;
            MaritalStatus = null;
            Occupation = null;
            SSN = null;
            SSNMask = null;
            StreetAdd1 = null;
            StreetAdd2 = null;
            City = null;
            State = null;
            Zip4 = null;
            Zip4Mask = null;
            PhNum = null;
            PhNumMask = null;
            EmployStat = null;
            InsurType = null;
            Veteran = null;
        }
    }

}
