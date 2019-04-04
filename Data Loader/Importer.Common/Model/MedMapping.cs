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
    public class MedMapping
    {
        private string[,] map = new string[,]
        {
            {"Case name","CASE"},
            {"Pat SSN","PAT_SSN"},
            {"Medication","DRUG"},
            {"Fill Date","DATE"},
            {"Expire Date","EXPIRDT"},
            {"Schedule","SIG"},
            {"Quantity","QTY"},
            {"Days supply","SUPPLY"},
            {"Number of refills","REFILL"},
            {"Provider","PROV"}
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
            maplist.Add("templateId/@root^F^^2.16.840.1.113883.10.20.24.3.41^^^"); //medication active template
            maplist.Add("consumable/manufacturedProduct/templateId/@root^F^^2.16.840.1.113883.10.20.22.4.23^^^"); //medication information template
            maplist.Add("consumable/manufacturedProduct/manufacturedMaterial/code/@code^D^Medication^2.16.840.1.113883.6.88^codeSystem^^Medication"); //Grab on RXNorm
            maplist.Add("effectiveTime/low/@value^D^Fill Date^^^^");
            //maplist.Add("effectiveTime/low/@value^D^Expire Date^^^^");
            maplist.Add("^S^Expire Date^^^T+30^");
            maplist.Add("^S^Schedule^^^BID^");
            maplist.Add("^S^Quantity^^^1^");
            maplist.Add("^S^Days supply^^^1^");
            maplist.Add("^S^Number of refills^^^0^");
            maplist.Add("^S^Provider^^^CERTIFICATION,PHYSICIAN^");
            maplist.Add("^S^Pat SSN^^^^");
            //
            return maplist;
        }

        public List<string> qrdmapping1()
        {
            List<string> maplist = new List<string>();
            maplist.Add("templateId/@root^F^^2.16.840.1.113883.10.20.22.4.42^^^"); //medication activity template
            maplist.Add("templateId/@root^F^^2.16.840.1.113883.10.20.24.3.47^^^"); //medication active template
            maplist.Add("consumable/manufacturedProduct/templateId/@root^F^^2.16.840.1.113883.10.20.22.4.23^^^"); //medication information template
            maplist.Add("consumable/manufacturedProduct/manufacturedMaterial/code/@code^D^Medication^2.16.840.1.113883.6.88^codeSystem^^Medication"); //Grab on RXNorm
            maplist.Add("effectiveTime/low/@value^D^Fill Date^^^^");
            //maplist.Add("effectiveTime/low/@value^D^Expire Date^^^^");
            maplist.Add("^S^Expire Date^^^T+30^");
            maplist.Add("^S^Schedule^^^BID^");
            maplist.Add("^S^Quantity^^^1^");
            maplist.Add("^S^Days supply^^^1^");
            maplist.Add("^S^Number of refills^^^0^");
            maplist.Add("^S^Provider^^^CERTIFICATION,PHYSICIAN^");
            maplist.Add("^S^Pat SSN^^^^");
            //
            return maplist;
        }

        public string GetRXName(string codesystem, string code)
        {
            string result = "";
            //string icode = "";
            //string iDrug = "";
            string[,] list = null;
            //
            if (codesystem == "2.16.840.1.113883.6.88")
            {
                list = RxnormMap;
            }
            //
            if (list == null) return result;
            //
            result = code;
            return result;
            //
            //for (int i = 0; i <= list.GetUpperBound(0); i++)
            //{
            //    icode = list[i, 0];
            //    iDrug = list[i, 1];
            //    if (icode == code)
            //    {
            //        result = iDrug;
            //        break;
            //    }
            //}
            //return result;
        }

        private string[,] RxnormMap = new string[,]
        {
            {"105152","Amoxicillin 60 MG/ML Oral Suspension"},
            {"142118","Sulfamethoxazole 100 MG / Trimethoprim 20 MG Oral Tablet"},
            {"197374","Aspirin 800 MG Extended Release Tablet"}, 
            {"198211","Simvastatin 40 MG Oral Tablet"}, 
            {"200031","carvedilol 6.25 MG Oral Tablet"}, 
            {"200327","capecitabine 150 MG Oral Tablet"}, 
            {"212033","Aspirin 325 MG Oral Tablet"},
            {"313585","Venlafaxine 75mg Ext Release Cap"},
            {"313586","Venlafaxine 75mg Ext Release Cap"},
            {"757704","Simvastatin 40 MG Disintegrating Tablet"},
            {"855288","Warfarin Sodium 1 MG Oral Tablet"},
            {"855332","Warfarin Sodium 5 MG Oral Tablet"}, 
            {"1000001","Amlodipine 5 MG / Hydrochlorothiazide 25 MG / Olmesartan medoxomil 40 MG Oral Tablet"}, 
            {"1000048","Doxepin Hydrochloride 10 MG Oral Capsule"},
            {"1000097","Doxepin Hydrochloride 75 MG Oral Capsule"}, 
            {"1000351","Estrogens, Conjugated (USP) 0.3 MG / medroxyprogesterone acetate 1.5 MG Oral Tablet"},
            {"1009145","Amphetamine aspartate 1.88 MG / Amphetamine Sulfate 1.88 MG / Dextroamphetamine saccharate 1.88 MG / Dextroamphetamine Sulfate 1.88 MG Oral Tablet"}, 
            {"1046847","24 HR Nicotine 0.313 MG/HR Transdermal Patch"}, 
            {"1085805","240 ACTUAT Triamcinolone Acetonide 0.075 MG/ACTUAT Metered Dose Inhaler"}, 
            {"1098617","14 (anastrozole 1 MG Oral Tablet) Pack"}, 
            {"1361568","heparin sodium, porcine 2000 UNT/ML Injectable Solution"},
            {"198467","Aspirin 325 MG Enteric Coated Tablet"},
            {"1189804","Simvastatin 10 MG / Sitagliptin 100 MG Oral Tablet"},
            {"308056","Alteplase 1 MG/ML Injectable Solution"},
            {"313134","Sulfamethoxazole 40 MG/ML / Trimethoprim 8 MG?ML Oral Suspension"},
            {"311354","Lisinopril 5 MG Oral Tablet"},
            {"1006608","24 hour dexmethylphenidate hydrochloride 40 MG Extended Release Capsule"},
            {"1013662","24 hour minocycline 55 MG Extended Release Tablet"},
            {"197454","Cephalexin 500 MG Oral Tablet"},
            {"1085795","100 ACTUAT Triamcinolone Acetonide 0.055 MG/ACTUAT Nasal Inhaler"},
            {"1049630","Diphenhydramine Hydrochloride 25 MG Oral Tablet"},
            {"899511","24 HR dexmethylphenidate hydrochloride 5 MG Extended Release Capsule"}
        };
    }
}
