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
    public class ProblemMapping
    {
        private string[,] map = new string[,]
        {
            {"Case name","CASE"},
            {"Dr name","PROVIDER"},
            {"Problem","PROBLEM"},
            {"ICD9 description","PROBLEM"},
            {"Entry date","ENTERED"},
            {"Problem onset date","ONSET"},
            {"Resolved date","RESOLVED"},
            {"Problem status","STATUS"},
            {"Problem type","TYPE"},
            {"VPov","VPOV"},
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
            maplist.Add("templateId/@root^F^^2.16.840.1.113883.10.20.22.4.4^^^");
            maplist.Add("templateId/@root^F^^2.16.840.1.113883.10.20.24.3.11^^^");
            //maplist.Add("value/@codesystem^F^^2.16.840.1.113883.6.96^^^"); // maybe
            maplist.Add("value/@code^D^Problem^^^^Problem");
            maplist.Add("entryRelationship/observation/templateId/@root^D^Problem status^2.16.840.1.113883.10.20.24.3.94^^A^");
            maplist.Add("^S^Problem type^^^A^");
            maplist.Add("^S^Dr name^^^CQM,HISTORICAL MD^");
            maplist.Add("effectiveTime/low/@value^D^Problem onset date^^^^");
            maplist.Add("effectiveTime/low/@value^D^Entry date^^^^");
            maplist.Add("effectiveTime/high/@value^D^Resolved date^^^^");
            maplist.Add("^S^VPov^^^Y^"); // designates to attempt to populate VPOV file entry
            maplist.Add("^S^Pat SSN^^^^");
            return maplist;
        }

        public string GetProblemType(string codesystem, string code)
        {
            string result = "";
            string icode = "";
            string iProblem = "";
            string[,] list = null;
            //
            if (codesystem == "2.16.840.1.113883.6.103")
            {
                list = ICD9Map;
                result = code;
            }
            if (codesystem == "2.16.840.1.113883.6.96")
            {
                list = SnomedMap;
                result = code;
            }
            //
            if (list == null) return result;
            if (result != "") return result;
            //
            for (int i = 0; i <= list.GetUpperBound(0); i++)
            {
                icode = list[i, 0];
                iProblem = list[i, 1];
                if (icode == code)
                {
                    result = iProblem;
                    break;
                }
            }
            return result;
        }

        private string[,] ICD9Map = new string[,]
        {
            {"427.31","Atrial fibrillation"},
            {"093.20","Syphilitic endocarditis of valve, unspecified"},
            {"428","Congestive heart failure, unspecified"}, 
            {"411","Postmyocardial infarction syndrome"}, 
            {"401.1","Benign essential hypertension"}, 	
            {"305","Alcohol abuse, unspecified"}, 
            {"296.21","Major depressive affective disorder, single episode, mild"}, 
            {"042","Human immunodeficiency virus [HIV] disease"},
            {"362.02","Proliferative diabetic retinopathy"}, 	
            {"94.1","General paresis"}, 
            {"415.11","Iatrogenic pulmonary embolism and infarction"}, 	
            {"634.6","Spontaneous abortion, complicated by embolism, unspecified"}, 
            {"174","Malignant neoplasm of nipple and areola of female breast"}, 
            {"493","Extrinsic asthma, unspecified"}, 
            {"433.01","Occlusion and stenosis of basilar artery with cerebral infarction"}, 
            {"153","Malignant neoplasm of hepatic flexure"}, 
            {"185","Malignant neoplasm of prostate"}, 
            {"250","Diabetes mellitus without mention of complication, type II or unspecified type, not stated as uncontrolled"}, 
            {"250.4","Diabetes with renal manifestations, type II or unspecified type, not stated as uncontrolled"}, 
            {"721.3","Lumbosacral spondylosis without myelopathy"},
            {"250.4","Diabetes with renal manifestations, type II or unspecified type, not stated as uncontrolled"}, 
            {"305","Alcohol abuse, unspecified"}, 
            {"O62.3","Precipitate labor"},
            {"V27.0","Outcome of delivery, single liveborn"},
            {"460","Acute nasopharyngitis [common cold]"}, 
            {"34","Streptococcal sore throat"},
            {"765.29","37 or more completed weeks of gestation"}, 
            {"634.6","Spontaneous abortion, complicated by embolism, unspecified"}, 
            {"141","Malignant neoplasm of base of tongue"}, 
            {"633.11","Tubal pregnancy with intrauterine pregnancy"}, 
            {"277","Cystic fibrosis without mention of meconium ileus"}, 
            {"185","Malignant neoplasm of prostate"}, 
            {"296.2","Major depressive affective disorder, single episode, unspecified"}, 
            {"521","Dental caries, unspecified"}
        };

        private string[,] SnomedMap = new string[,]
        {
            {"1532007","Viral pharyngitis (disorder)"}, 
            {"3950001","stuff"},
            {"6475002","stuff"},
            {"4855003","Diabetic retinopathy (disorder)"},
            {"7200002","Alcoholism (disorder)"}, 
            {"8722008","Aortic valve disorder (disorder)"}, 
            {"10091002","High output heart failure (disorder)"}, 
            {"10273003","stuff"},
            {"10326007","stuff"},
            {"10725009","Benign hypertension (disorder)"},
            {"11029002","Pulmonary apoplexy (disorder)"},
            {"12428000","Intrinsic asthma without status asthmaticus (disorder)"}, 
            {"14183003","stuff"},
            {"13798002","Gestation period, 38 weeks (finding)"},
            {"13943000","Failed attempted abortion complicated by embolism (disorder)"}, 
            {"14183003","Chronic major depressive disorder, single episode (disorder)"}, 
            {"15167005","stuff"},
            {"15639000","stuff"},
            {"20301004","Dysphasia (finding)"},
            {"23397002","stuff"},
            {"46635009","stuff"},
            {"51928006","General paresis - neurosyphilis (disorder)"},
            {"57834008","stuff"},
            {"63161005","stuff"},
            {"75524006","Malnutrition related diabetes mellitus (disorder)"}, 
            {"102872000","Pregnancy on oral contraceptive (finding)"}, 
            {"109267002","Overlapping malignant melanoma of skin (disorder)"}, 
            {"109564008","Dental caries associated with enamel hypomineralization (disorder)"}, 
            {"109838007","Overlapping malignant neoplasm of colon (disorder)"},
            {"109886000","Overlapping malignant neoplasm of female breast (disorder)"}, 
            {"111297002","Nonparalytic stroke (disorder)"},
            {"111513000","Advanced open-angle glaucoma (disorder)"}, 
            {"111880001","Acute HIV infection (disorder)"},
            {"123641001","Left coronary artery occlusion (disorder)"}, 
            {"127013003","Diabetic renal disease (disorder)"}, 
            {"160603005","Light cigarette smoker (1-9 cigs/day) (finding)"}, 
            {"161894002","Complaining of low back pain (finding)"}, 
            {"162607003","Terminal illness - early stage (finding)"}, 
            {"169734005","Spontaneous rupture of membranes (finding)"}, 
            {"169826009","Single live birth (finding)"},
            {"188147009","stuff"},
            {"190330002","Diabetes mellitus, juvenile type, with hyperosmolar coma (disorder)"}, 
            {"190905008","Cystic fibrosis (disorder)"}, 
            {"193349004","stuff"},
            {"195080001","Atrial fibrillation and flutter (disorder)"},
            {"195708003","Recurrent upper respiratory tract infection (disorder)"},
            {"233607000","stuff"},
            {"233970002","stuff"},
            {"236973005","stuff"},
            {"237240001","stuff"},
            {"237244005","stuff"},
            {"254900004","Carcinoma of prostate (disorder)"},
            {"371804009","stuff"},
            {"416053008","Estrogen receptor positive tumor (disorder)"}, 
            {"426656000","Severe persistent asthma (disorder)"},
            {"426979002","stuff"},
            {"441924001","Gestational age unknown (finding)"}, 
            {"442311008","Liveborn born in hospital (situation)"},
            {"562722000","stuff"},
            {"981000124106","Moderate left ventricular systolic dysfunction (disorder)"}
        };

    }
    
    
}
