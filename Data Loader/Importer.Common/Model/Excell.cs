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
    public class Column
    {
        public string[] ColumnArray = new string[100];

        public bool PatientColumnArraySet(string columnname, int columnum)
        {
            switch (columnname)
            {
                case "Case name":
                    {
                        ColumnArray[columnum] = columnname;
                        return true;
                    }
                case "Last name":
                    {
                        ColumnArray[columnum] = columnname;
                        return true;
                    }
                case "First name":
                    {
                        ColumnArray[columnum] = columnname;
                        return true;
                    }
                case "Gender":
                    {
                        ColumnArray[columnum] = columnname;
                        return true;
                    }
                case "DOB":
                    {
                        ColumnArray[columnum] = columnname;
                        return true;
                    }
                case "Race":
                    {
                        ColumnArray[columnum] = columnname;
                        return true;
                    }
                case "Ethnicity":
                    {
                        ColumnArray[columnum] = columnname;
                        return true;
                    }
                case "Employment status":
                    {
                        ColumnArray[columnum] = columnname;
                        return true;
                    }
                case "Insurance":
                    {
                        ColumnArray[columnum] = columnname;
                        return true;
                    }
                case "Occupation":
                    {
                        ColumnArray[columnum] = columnname;
                        return true;
                    }
                case "US veteran":
                    {
                        ColumnArray[columnum] = columnname;
                        return true;
                    }
                case "Marital status":
                    {
                        ColumnArray[columnum] = columnname;
                        return true;
                    }
                case "SSN":
                    {
                        ColumnArray[columnum] = columnname;
                        return true;
                    }
                case "City":
                    {
                        ColumnArray[columnum] = columnname;
                        return true;
                    }
                case "State":
                    {
                        ColumnArray[columnum] = columnname;
                        return true;
                    }
                case "Zip":
                    {
                        ColumnArray[columnum] = columnname;
                        return true;
                    }
                case "Phone":
                    {
                        ColumnArray[columnum] = columnname;
                        return true;
                    }
                default:
                    {
                        ColumnArray[columnum] = "***";
                        return false;
                    }
            }
        }

        public bool ColumnCheck(int columnnum)
        {
            if (ColumnArray[columnnum] != "***")
                return true;
            else
                return false;
        }

        public bool ApptColumnArraySet(string columnname, int columnum)
        {
            switch (columnname)
            {
                case "Case name":
                    {
                        ColumnArray[columnum] = columnname;
                        return true;
                    }
                case "Appt date":
                    {
                        ColumnArray[columnum] = columnname;
                        return true;
                    }
                case "Check out date":
                    {
                        ColumnArray[columnum] = columnname;
                        return true;
                    }
                case "Appt clinic":
                    {
                        ColumnArray[columnum] = columnname;
                        return true;
                    }
                case "Pat SSN":
                    {
                        ColumnArray[columnum] = columnname;
                        return true;
                    }
                case "Provider":
                    {
                        ColumnArray[columnum] = columnname;
                        return true;
                    }
                default:
                    {
                        ColumnArray[columnum] = "***";
                        return false;
                    }
            }
        }
        public bool AdmitColumnArraySet(string columnname, int columnum)
        {
            switch (columnname)
            {
                case "Case name":
                    {
                        ColumnArray[columnum] = columnname;
                        return true;
                    }
                case "Begin DateTime":
                    {
                        ColumnArray[columnum] = columnname;
                        return true;
                    }
                case "End DateTime":
                    {
                        ColumnArray[columnum] = columnname;
                        return true;
                    }
                case "Diagnosis":
                    {
                        ColumnArray[columnum] = columnname;
                        return true;
                    }
                case "Admitting Regulation":
                    {
                        ColumnArray[columnum] = columnname;
                        return true;
                    }
                case "Type of Disposition":
                    {
                        ColumnArray[columnum] = columnname;
                        return true;
                    }
                case "Facility Treating Specialty":
                    {
                        ColumnArray[columnum] = columnname;
                        return true;
                    }
                case "Facility Directory Excluded":
                    {
                        ColumnArray[columnum] = columnname;
                        return true;
                    }
                case "Location":
                    {
                        ColumnArray[columnum] = columnname;
                        return true;
                    }
                case "Room-Bed":
                    {
                        ColumnArray[columnum] = columnname;
                        return true;
                    }
                case "Provider":
                    {
                        ColumnArray[columnum] = columnname;
                        return true;
                    }
                case "Diagnosis Description":
                    {
                        ColumnArray[columnum] = columnname;
                        return true;
                    }
                case "Pat SSN":
                    {
                        ColumnArray[columnum] = columnname;
                        return true;
                    }
                default:
                    {
                        ColumnArray[columnum] = "***";
                        return false;
                    }
            }
        }
        public bool ProbColumnArrarySet(string columnname, int columnum)
        {
            switch (columnname)
            {
                case "Case name":
                    {
                        ColumnArray[columnum] = columnname;
                        return true;
                    }
                case "Problem":
                    {
                        ColumnArray[columnum] = columnname;
                        return true;
                    }
                case "ICD9 description":
                    {
                        ColumnArray[columnum] = columnname;
                        return true;
                    }
                case "Dr name":
                    {
                        ColumnArray[columnum] = columnname;
                        return true;
                    }
                case "Problem onset date":
                    {
                        ColumnArray[columnum] = columnname;
                        return true;
                    }
                case "Resolved date":
                    {
                        ColumnArray[columnum] = columnname;
                        return true;
                    }
                case "Entry date":
                    {
                        ColumnArray[columnum] = columnname;
                        return true;
                    }
                case "Problem status":
                    {
                        ColumnArray[columnum] = columnname;
                        return true;
                    }
                case "Problem type":
                    {
                        ColumnArray[columnum] = columnname;
                        return true;
                    }
                case "VPov": //added to support automated V POV entries
                    {
                        ColumnArray[columnum] = columnname;
                        return true;
                    }
                case "Pat SSN":
                    {
                        ColumnArray[columnum] = columnname;
                        return true;
                    }
                default:
                    {
                        ColumnArray[columnum] = "***";
                        return false;
                    }
            }
        }
        public bool VitalColumnArraySet(string columnname, int columnum)
        {
            switch (columnname)
            {
                case "Case name":
                    {
                        ColumnArray[columnum] = columnname;
                        return true;
                    }
                case "Vital type":
                    {
                        ColumnArray[columnum] = columnname;
                        return true;
                    }
                case "Rate 1":
                    {
                        ColumnArray[columnum] = columnname;
                        return true;
                    }
                case "Rate 2":
                    {
                        ColumnArray[columnum] = columnname;
                        return true;
                    }
                case "Clinic":
                    {
                        ColumnArray[columnum] = columnname;
                        return true;
                    }
                case "Date":
                    {
                        ColumnArray[columnum] = columnname;
                        return true;
                    }
                case "Taken by":
                    {
                        ColumnArray[columnum] = columnname;
                        return true;
                    }
                case "Pat SSN":
                    {
                        ColumnArray[columnum] = columnname;
                        return true;
                    }
                default:
                    {
                        ColumnArray[columnum] = "***";
                        return false;
                    }
            }
        }
        public bool AllergyColumnArraySet(string columnname, int columnum)
        {
            switch (columnname)
            {
                case "Case name":
                    {
                        ColumnArray[columnum] = columnname;
                        return true;
                    }
                case "Allergen":
                    {
                        ColumnArray[columnum] = columnname;
                        return true;
                    }
                case "Symptom":
                    {
                        ColumnArray[columnum] = columnname;
                        return true;
                    }
                case "Origination date":
                    {
                        ColumnArray[columnum] = columnname;
                        return true;
                    }
                case "Entered by":
                    {
                        ColumnArray[columnum] = columnname;
                        return true;
                    }
                case "Pat SSN":
                    {
                        ColumnArray[columnum] = columnname;
                        return true;
                    }
                default:
                    {
                        ColumnArray[columnum] = "***";
                        return false;
                    }
            }
        }
        public bool LabColumnArraySet(string columnname, int columnum)
        {
            switch (columnname)
            {
                case "Case name":
                    {
                        ColumnArray[columnum] = columnname;
                        return true;
                    }
                case "Pat SSN":
                    {
                        ColumnArray[columnum] = columnname;
                        return true;
                    }
                case "Lab test":
                    {
                        ColumnArray[columnum] = columnname;
                        return true;
                    }
                case "Result date":
                    {
                        ColumnArray[columnum] = columnname;
                        return true;
                    }
                case "Result value":
                    {
                        ColumnArray[columnum] = columnname;
                        return true;
                    }
                case "Location":
                    {
                        ColumnArray[columnum] = columnname;
                        return true;
                    }
                default:
                    {
                        ColumnArray[columnum] = "***";
                        return false;
                    }
            }
        }
        public bool NoteColumnArraySet(string columnname, int columnum)
        {
            switch (columnname)
            {
                case "Case name":
                    {
                        ColumnArray[columnum] = columnname;
                        return true;
                    }
                case "Pat SSN":
                    {
                        ColumnArray[columnum] = columnname;
                        return true;
                    }
                case "Note title":
                    {
                        ColumnArray[columnum] = columnname;
                        return true;
                    }
                case "Clinic":
                    {
                        ColumnArray[columnum] = columnname;
                        return true;
                    }
                case "Visit datetime":
                    {
                        ColumnArray[columnum] = columnname;
                        return true;
                    }
                case "Provider":
                    {
                        ColumnArray[columnum] = columnname;
                        return true;
                    }
                case "Note text":
                    {
                        ColumnArray[columnum] = columnname;
                        return true;
                    }
                default:
                    {
                        ColumnArray[columnum] = "***";
                        return false;
                    }
            }
        }
        public bool MedColumnArraySet(string columnname, int columnum)
        {
            switch (columnname)
            {
                case "Case name":
                    {
                        ColumnArray[columnum] = columnname;
                        return true;
                    }
                case "Pat SSN":
                    {
                        ColumnArray[columnum] = columnname;
                        return true;
                    }
                case "Medication":
                    {
                        ColumnArray[columnum] = columnname;
                        return true;
                    }
                case "Fill Date":
                    {
                        ColumnArray[columnum] = columnname;
                        return true;
                    }
                case "Expire Date":
                    {
                        ColumnArray[columnum] = columnname;
                        return true;
                    }
                case "Schedule":
                    {
                        ColumnArray[columnum] = columnname;
                        return true;
                    }
                case "Quantity":
                    {
                        ColumnArray[columnum] = columnname;
                        return true;
                    }
                case "Days supply":
                    {
                        ColumnArray[columnum] = columnname;
                        return true;
                    }
                case "Number of refills":
                    {
                        ColumnArray[columnum] = columnname;
                        return true;
                    }
                case "Provider":
                    {
                        ColumnArray[columnum] = columnname;
                        return true;
                    }
                default:
                    {
                        ColumnArray[columnum] = "***";
                        return false;
                    }
            }
        }
        public bool ConsultColumnArraySet(string columnname, int columnum)
        {
            switch (columnname)
            {
                case "Case name":
                    {
                        ColumnArray[columnum] = columnname;
                        return true;
                    }
                case "Pat SSN":
                    {
                        ColumnArray[columnum] = columnname;
                        return true;
                    }
                case "Consult":
                    {
                        ColumnArray[columnum] = columnname;
                        return true;
                    }
                case "Location":
                    {
                        ColumnArray[columnum] = columnname;
                        return true;
                    }
                case "Provider":
                    {
                        ColumnArray[columnum] = columnname;
                        return true;
                    }
                case "Reason for request":
                    {
                        ColumnArray[columnum] = columnname;
                        return true;
                    }
                default:
                    {
                        ColumnArray[columnum] = "***";
                        return false;
                    }
            }
        }
        public bool RadColumnArraySet(string columnname, int columnum)
        {
            switch (columnname)
            {
                case "Case name":
                    {
                        ColumnArray[columnum] = columnname;
                        return true;
                    }
                case "Pat SSN":
                    {
                        ColumnArray[columnum] = columnname;
                        return true;
                    }
                case "Rad Procedure":
                    {
                        ColumnArray[columnum] = columnname;
                        return true;
                    }
                case "Imaging Location":
                    {
                        ColumnArray[columnum] = columnname;
                        return true;
                    }
                case "Requesting Provider":
                    {
                        ColumnArray[columnum] = columnname;
                        return true;
                    }
                case "Exam DateTime":
                    {
                        ColumnArray[columnum] = columnname;
                        return true;
                    }
                case "Exam Category":
                    {
                        ColumnArray[columnum] = columnname;
                        return true;
                    }
                case "Request Location":
                    {
                        ColumnArray[columnum] = columnname;
                        return true;
                    }
                case "Reason":
                    {
                        ColumnArray[columnum] = columnname;
                        return true;
                    }
                case "Clinical History":
                    {
                        ColumnArray[columnum] = columnname;
                        return true;
                    }
                case "Technician":
                    {
                        ColumnArray[columnum] = columnname;
                        return true;
                    }
                case "Tech Comment":
                    {
                        ColumnArray[columnum] = columnname;
                        return true;
                    }
                case "Exam Status":
                    {
                        ColumnArray[columnum] = columnname;
                        return true;
                    }
                default:
                    {
                        ColumnArray[columnum] = "***";
                        return false;
                    }
            }
        }
        public bool GenColumnArrarySet(string columnname, int columnum)
        {
            //supports Encounter sets: V IMM, V CPT, V HEALTH FACTORS, V POV, V PNT ED, V EXAM
            switch (columnname)
            {
                case "Case name":
                    {
                        ColumnArray[columnum] = columnname;
                        return true;
                    }
                case "Health Factor":
                    {
                        ColumnArray[columnum] = columnname;
                        return true;
                    }
                case "Cpt":
                    {
                        ColumnArray[columnum] = columnname;
                        return true;
                    }
                case "Immunization":
                    {
                        ColumnArray[columnum] = columnname;
                        return true;
                    }
                case "Provider":
                    {
                        ColumnArray[columnum] = columnname;
                        return true;
                    }
                case "DateTime":
                    {
                        ColumnArray[columnum] = columnname;
                        return true;
                    }
                case "Comment":
                    {
                        ColumnArray[columnum] = columnname;
                        return true;
                    }
                case "Severity":
                    {
                        ColumnArray[columnum] = columnname;
                        return true;
                    }
                case "Provider Narrative":
                    {
                        ColumnArray[columnum] = columnname;
                        return true;
                    }
                case "ICD9":
                    {
                        ColumnArray[columnum] = columnname;
                        return true;
                    }
                case "Education Topic":
                    {
                        ColumnArray[columnum] = columnname;
                        return true;
                    }
                case "Level of Understanding":
                    {
                        ColumnArray[columnum] = columnname;
                        return true;
                    }
                case "Exam":
                    {
                        ColumnArray[columnum] = columnname;
                        return true;
                    }
                case "Pat SSN":
                    {
                        ColumnArray[columnum] = columnname;
                        return true;
                    }
                default:
                    {
                        ColumnArray[columnum] = "***";
                        return false;
                    }
            }
        }

        public bool TreatingFacilityColumnArraySet(string columnname, int columnum)
        {
            switch (columnname)
            {
                case "Case name":
                    {
                        ColumnArray[columnum] = columnname;
                        return true;
                    }
                case "Institution":
                    {
                        ColumnArray[columnum] = columnname;
                        return true;
                    }
                case "DateLastTreated":
                    {
                        ColumnArray[columnum] = columnname;
                        return true;
                    }
                case "Pat SSN":
                    {
                        ColumnArray[columnum] = columnname;
                        return true;
                    }
                default:
                    {
                        ColumnArray[columnum] = "***";
                        return false;
                    }
            }
        }

        public bool NonVAMedColumnArraySet(string columnname, int columnum)
        {
            switch (columnname)
            {
                case "Case name":
                    {
                        ColumnArray[columnum] = columnname;
                        return true;
                    }
                case "Pat SSN":
                    {
                        ColumnArray[columnum] = columnname;
                        return true;
                    }
                case "Medication":
                    {
                        ColumnArray[columnum] = columnname;
                        return true;
                    }
                case "Start Date":
                    {
                        ColumnArray[columnum] = columnname;
                        return true;
                    }
                case "Dosage":
                    {
                        ColumnArray[columnum] = columnname;
                        return true;
                    }
                case "Route":
                    {
                        ColumnArray[columnum] = columnname;
                        return true;
                    }
                case "Schedule":
                    {
                        ColumnArray[columnum] = columnname;
                        return true;
                    }
                case "Disclaimer":
                    {
                        ColumnArray[columnum] = columnname;
                        return true;
                    }
                default:
                    {
                        ColumnArray[columnum] = "***";
                        return false;
                    }
            }
        }
    }

    public class Worksheet
    {
        public string TableName(string name)
        {
            switch (name)
            {
                case "Patient":
                    return "Patients$";
                case "Appointment":
                    return "Appts$";
                case "Problem":
                    return "Problems$";
                case "Vital":
                    return "Vitals$";
                case "Allergy":
                    return "Allergies$";
                case "Lab":
                    return "Labs$";
                case "Note":
                    return "Notes$";
                case "Med":
                    return "Meds$";
                case "NonVAMed":
                    return "NonVAMeds$";
                case "Consult":
                    return "Consults$";
                case "Rad":
                    return "RadOrders$";
                case "Encounters":
                    return "Encounters$";
                case "HFactors":
                    return "HFactors$";
                case "VImmunization":
                    return "VImmunization$";
                case "VCpt":
                    return "VCpt$";
                case "VPov":
                    return "VPov$";
                case "VPatientEd":
                    return "VPatientEd$";
                case "VExam":
                    return "VExam$";
                case "TreatingFacility":
                    return "TreatingFacility$";
                default:
                    return "";
            }
        }
    }
}


