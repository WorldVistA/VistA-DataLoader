ISIIMPLT ; DOC360ME/SMH - Lab Regression Test Suite; 2024-01-13
 ;;3.1;ISI_DATA_LOADER;;Jun 26, 2019;Build 70
 ;
 ; VistA Data Loader 3.1
 ;
 ; Copyright (C) 2024-2025 DocMe360 LLC
 ;
 ;Licensed under the Apache License, Version 2.0 (the "License");
 ;you may not use this file except in compliance with the License.
 ;You may obtain a copy of the License at
 ;
 ;    http://www.apache.org/licenses/LICENSE-2.0
 ;
 ;Unless required by applicable law or agreed to in writing, software
 ;distributed under the License is distributed on an "AS IS" BASIS,
 ;WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
 ;See the License for the specific language governing permissions and
 ;limitations under the License.
 ;
 do en^%ut($text(+0),3)
 quit
 ;
STARTUP ;
 S DFN=$O(^DPT("SSN","000000068",""))
 S PATSSN="000000068"
 QUIT
 ;
SHUTDOWN
 K DFN
 K PATSSN
 QUIT
 ;
FLAB ; @TEST Full Lab RPC regression
 D WAIT
 N SAM
 S SAM(1)="PAT_SSN^"_PATSSN
 S SAM(2)="LAB_TEST^LEAD"
 N DATE S DATE=$$NOW^XLFDT()
 S SAM(3)="RESULT_DT^"_DATE
 S SAM(4)="RESULT_VAL^189"
 S SAM(5)="LOCATION^3E NORTH"
 D LABMAKE^ISIIMPR2(.RC,.SAM) 
 D eq^%ut($$LABDUP^ISIIMPU7(DFN,DATE,"LEAD"),1)
 D eq^%ut(RC(0),1)
 QUIT
 ;
FLABNOSECE ; @TEST Full Lab RPC regression (no seconds external)
 D WAIT
 N SAM
 S SAM(1)="PAT_SSN^"_PATSSN
 S SAM(2)="LAB_TEST^LEAD"
 N DATE,IDATE,EDATE S DATE=$$NOW^XLFDT()
 S IDATE=$P(DATE,".")_"."_$E($P(DATE,".",2),1,4)
 S EDATE=$$FMTE^XLFDT(IDATE)
 S SAM(3)="RESULT_DT^"_EDATE
 S SAM(4)="RESULT_VAL^189"
 S SAM(5)="LOCATION^3E NORTH"
 D LABMAKE^ISIIMPR2(.RC,.SAM) 
 D eq^%ut($$LABDUP^ISIIMPU7(DFN,IDATE,"LEAD"),1)
 D eq^%ut(RC(0),1)
 QUIT
 ;
LSSNE ; @TEST SSN Error
 N SAM
 S SAM(1)="PAT_SSN^999999999"
 S SAM(2)="LAB_TEST^GLUCOSE"
 S SAM(3)="RESULT_DT^NOW"
 S SAM(4)="RESULT_VAL^189"
 S SAM(5)="LOCATION^3E NORTH"
 D LABMAKE^ISIIMPR2(.RC,.SAM) 
 do tf^%ut(RC(0)["Invalid PAT_SSN")
 S SAM(1)="PAT_SSN^"
 D LABMAKE^ISIIMPR2(.RC,.SAM) 
 do tf^%ut(RC(0)["No data provided for parameter: PAT_SSN")
 K SAM(1)
 D LABMAKE^ISIIMPR2(.RC,.SAM) 
 do tf^%ut(RC(0)["Missing Patient SSN")
 QUIT
 ;
LTESTE ; @TEST Lab Test various errors
 N SAM
 S SAM(1)="PAT_SSN^"_PATSSN
 S SAM(2)="LAB_TEST^GLUCOSEFOO"
 S SAM(3)="RESULT_DT^NOW"
 S SAM(4)="RESULT_VAL^189"
 S SAM(5)="LOCATION^3E NORTH"
 D LABMAKE^ISIIMPR2(.RC,.SAM)
 do tf^%ut(RC(0)["Couldn't find ien for LAB_TEST","error 1")
 S SAM(2)="LAB_TEST^"
 D LABMAKE^ISIIMPR2(.RC,.SAM)
 do tf^%ut(RC(0)["No data provided for parameter: LAB_TEST","error 2")
 S SAM(1)="PAT_SSN^"_PATSSN
 K SAM(2)
 S SAM(3)="RESULT_DT^NOW"
 S SAM(4)="RESULT_VAL^189"
 S SAM(5)="LOCATION^3E NORTH"
 D LABMAKE^ISIIMPR2(.RC,.SAM)
 do tf^%ut(RC(0)["Missing LAB_TEST","error 3")
 S SAM(1)="PAT_SSN^"_PATSSN
 S SAM(2)="LAB_TEST^RENAL BIOPSY"
 S SAM(3)="RESULT_DT^NOW"
 S SAM(4)="RESULT_VAL^189"
 S SAM(5)="LOCATION^3E NORTH"
 D LABMAKE^ISIIMPR2(.RC,.SAM)
 do tf^%ut(RC(0)["must by 'CH'","error 4")
 QUIT
 ;
RVE ; @TEST Result Value errors
 N SAM
 S SAM(1)="PAT_SSN^"_PATSSN
 S SAM(2)="LAB_TEST^GLUCOSE"
 S SAM(3)="RESULT_DT^NOW"
 ;S SAM(4)="RESULT_VAL^189"
 S SAM(5)="LOCATION^3E NORTH"
 D LABMAKE^ISIIMPR2(.RC,.SAM)
 do tf^%ut(RC(0)["Missing RESULT_VAL")
 S SAM(4)="RESULT_VAL^"
 D LABMAKE^ISIIMPR2(.RC,.SAM)
 do tf^%ut(RC(0)["No data provided for parameter: RESULT_VAL")
 QUIT
 ;
RDE ; @TEST Result Date error
 N SAM
 S SAM(1)="PAT_SSN^"_PATSSN
 S SAM(2)="LAB_TEST^GLUCOSE"
 ;S SAM(3)="RESULT_DT^NOW"
 S SAM(4)="RESULT_VAL^189"
 S SAM(5)="LOCATION^3E NORTH"
 D LABMAKE^ISIIMPR2(.RC,.SAM)
 do tf^%ut(RC(0)["Missing RESULT_DT entry")
 S SAM(3)="RESULT_DT^foo"
 D LABMAKE^ISIIMPR2(.RC,.SAM)
 do tf^%ut(RC(0)["Invalid RESULT_DT date/time")
 QUIT
 ;
EEE ; @TEST Entered by errors
 N OLDDUZ M OLDDUZ=DUZ
 S DUZ=5 D DUZ^XUP(DUZ)
 N SAM
 S SAM(1)="PAT_SSN^"_PATSSN
 S SAM(2)="LAB_TEST^GLUCOSE"
 S SAM(3)="RESULT_DT^NOW"
 S SAM(4)="RESULT_VAL^189"
 S SAM(5)="LOCATION^3E NORTH"
 D LABMAKE^ISIIMPR2(.RC,.SAM) 
 do tf^%ut(RC(0)["Invalid ENTERED_BY (#200,.01).  Insufficient privilages.")
 K DUZ M DUZ=OLDDUZ
 QUIT
 ;
LE ; @TEST Location errors
 N SAM
 S SAM(1)="PAT_SSN^"_PATSSN
 S SAM(2)="LAB_TEST^GLUCOSE"
 S SAM(3)="RESULT_DT^NOW"
 S SAM(4)="RESULT_VAL^189"
 S SAM(5)="LOCATION^LINDA TWO" ; inactive location
 D LABMAKE^ISIIMPR2(.RC,.SAM) 
 do tf^%ut(RC(0)["Invalid LOCATION value")
 QUIT
 ;
PANELRPC1 ; @TEST Load Partial Panel
 D WAIT
 K SAM,RC
 N DATE S DATE=$$NOW^XLFDT()
 S SAM(1)="PAT_SSN^"_PATSSN
 S SAM(2)="LAB_PANEL^CHEM 7"
 S SAM(3)="LAB_TEST^GLUCOSE^180"
 S SAM(4)="LAB_TEST^CO2^34"
 S SAM(5)="LAB_TEST^BUN^10"
 S SAM(6)="LAB_TEST^CHLORIDE^100"
 S SAM(7)="RESULT_DT^"_DATE
 S SAM(8)="LOCATION^3E NORTH"
 DO LABPANEL^ISIIMPR2(.RC,.SAM)
 N I,LAB F I=0:0 S I=$O(SAM(I)) Q:'I  I $P(SAM(I),U)="LAB_TEST" S LAB=$P(SAM(I),U,2) D eq^%ut($$LABDUP^ISIIMPU7(DFN,DATE,LAB),1)
 D eq^%ut(RC(0),1)
 QUIT
 ;
PANELRPC2 ; @TEST Load Full Panel
 D WAIT
 K SAM,RC
 N DATE S DATE=$$NOW^XLFDT()
 S SAM(1)="PAT_SSN^"_PATSSN
 S SAM(2)="LAB_PANEL^CHEM 7"
 S SAM(3)="LAB_TEST^GLUCOSE^180"
 S SAM(4)="LAB_TEST^CO2^34"
 S SAM(5)="LAB_TEST^BUN^10"
 S SAM(6)="LAB_TEST^CHLORIDE^100"
 S SAM(7)="LAB_TEST^CREATININE^1.1"
 S SAM(8)="LAB_TEST^POTASSIUM^4.7"
 S SAM(9)="LAB_TEST^SODIUM^150" 
 S SAM(10)="RESULT_DT^"_DATE
 S SAM(11)="LOCATION^3E NORTH"
 DO LABPANEL^ISIIMPR2(.RC,.SAM)
 N I,LAB F I=0:0 S I=$O(SAM(I)) Q:'I  I $P(SAM(I),U)="LAB_TEST" S LAB=$P(SAM(I),U,2) D eq^%ut($$LABDUP^ISIIMPU7(DFN,DATE,LAB),1)
 D eq^%ut(RC(0),1)
 QUIT
 ;
LABDUP ; @TEST Test lab duplicate
 D WAIT
 N SAM
 S SAM(1)="PAT_SSN^"_PATSSN
 S SAM(2)="LAB_TEST^LEAD"
 N DATE S DATE=$$NOW^XLFDT()
 S SAM(3)="RESULT_DT^"_DATE
 S SAM(4)="RESULT_VAL^189"
 S SAM(5)="LOCATION^3E NORTH"
 D LABMAKE^ISIIMPR2(.RC,.SAM) 
 D eq^%ut($$LABDUP^ISIIMPU7(DFN,DATE,"LEAD"),1,"error 0")
 D eq^%ut(RC(0),1,"error 1")
 S SAM(1)="PAT_SSN^"_PATSSN
 S SAM(2)="LAB_TEST^LEAD"
 S SAM(3)="RESULT_DT^"_DATE
 S SAM(4)="RESULT_VAL^189"
 S SAM(5)="LOCATION^3E NORTH"
 D LABMAKE^ISIIMPR2(.RC,.SAM) 
 D tf^%ut(RC(0)["Duplicate Lab Test entry for patient.","error 2")
 QUIT
 ;
PANELDUP ; @TEST Load panel duplicate
 D WAIT
 K SAM,RC
 N DATE S DATE=$$NOW^XLFDT()
 S SAM(1)="PAT_SSN^"_PATSSN
 S SAM(2)="LAB_PANEL^CHEM 7"
 S SAM(3)="LAB_TEST^GLUCOSE^180"
 S SAM(4)="LAB_TEST^CO2^34"
 S SAM(5)="LAB_TEST^BUN^10"
 S SAM(6)="LAB_TEST^CHLORIDE^100"
 S SAM(7)="LAB_TEST^CREATININE^1.1"
 S SAM(8)="LAB_TEST^POTASSIUM^4.7"
 S SAM(9)="LAB_TEST^SODIUM^150" 
 S SAM(10)="RESULT_DT^"_DATE
 S SAM(11)="LOCATION^3E NORTH"
 DO LABPANEL^ISIIMPR2(.RC,.SAM)
 N I,LAB F I=0:0 S I=$O(SAM(I)) Q:'I  I $P(SAM(I),U)="LAB_TEST" S LAB=$P(SAM(I),U,2) D eq^%ut($$LABDUP^ISIIMPU7(DFN,DATE,LAB),1)
 D eq^%ut(RC(0),1,"error 1")
 S SAM(1)="PAT_SSN^"_PATSSN
 S SAM(2)="LAB_PANEL^CHEM 7"
 S SAM(3)="LAB_TEST^GLUCOSE^180"
 S SAM(4)="LAB_TEST^CO2^34"
 S SAM(5)="LAB_TEST^BUN^10"
 S SAM(6)="LAB_TEST^CHLORIDE^100"
 S SAM(7)="LAB_TEST^CREATININE^1.1"
 S SAM(8)="LAB_TEST^POTASSIUM^4.7"
 S SAM(9)="LAB_TEST^SODIUM^150" 
 S SAM(10)="RESULT_DT^"_DATE
 S SAM(11)="LOCATION^3E NORTH"
 DO LABPANEL^ISIIMPR2(.RC,.SAM)
 D tf^%ut(RC(0)["Duplicate Lab Test UREA NITROGEN for patient","error 2")
 QUIT
 ;
INVPANEL ; @TEST Test that a non-existent panel throws an error
 N SAM
 S SAM(1)="PAT_SSN^"_PATSSN
 S SAM(2)="LAB_PANEL^CHEM-7FOO"
 S SAM(3)="LAB_TEST^GLUCOSE^180"
 S SAM(4)="LAB_TEST^CO2^34"
 S SAM(5)="LAB_TEST^BUN^10"
 S SAM(6)="LAB_TEST^CHLORIDE^100"
 S SAM(7)="RESULT_DT^NOW"
 S SAM(8)="LOCATION^3E NORTH"
 K RC
 DO LABPANEL^ISIIMPR2(.RC,.SAM)
 do tf^%ut(RC(0)["Couldn't find ien for LAB_PANEL",1)
 S SAM(2)="LAB_PANEL^"
 K RC
 DO LABPANEL^ISIIMPR2(.RC,.SAM)
 do tf^%ut(RC(0)["No data provided for parameter: LAB_PANEL",2)
 K SAM
 S SAM(1)="PAT_SSN^"_PATSSN
 S SAM(2)="LAB_TEST^GLUCOSE^180"
 K RC
 DO LABPANEL^ISIIMPR2(.RC,.SAM)
 do tf^%ut(RC(0)["Missing LAB_PANEL",3)
 QUIT
 ;
PANELNOLAB ; @TEST Test for valid labs but don't belong to a panel
 N SAM
 S SAM(1)="PAT_SSN^"_PATSSN
 S SAM(2)="LAB_PANEL^CHEM 7"
 S SAM(3)="LAB_TEST^GLUCOSE^180"
 S SAM(4)="LAB_TEST^CO2^34"
 S SAM(5)="LAB_TEST^BUN^10"
 S SAM(6)="LAB_TEST^LEAD^100"
 S SAM(7)="LAB_TEST^COPPER^100"
 S SAM(8)="RESULT_DT^NOW"
 S SAM(9)="LOCATION^3E NORTH"
 DO LABPANEL^ISIIMPR2(.RC,.SAM)
 do tf^%ut(RC(0)["Labs supplied (COPPER,LEAD) are not in panel CHEM 7")
 QUIT
 ;
PANELINVLAB ; @TEST Test for invalid labs in a valid panel
 N SAM
 S SAM(1)="PAT_SSN^"_PATSSN
 S SAM(2)="LAB_PANEL^CHEM 7"
 S SAM(3)="LAB_TEST^GLUCOSE^180"
 S SAM(4)="LAB_TEST^CO2^34"
 S SAM(5)="LAB_TEST^BUNQQ^10"
 S SAM(6)="LAB_TEST^LEADFF^100"
 S SAM(8)="RESULT_DT^NOW"
 S SAM(9)="LOCATION^3E NORTH"
 DO LABPANEL^ISIIMPR2(.RC,.SAM)
 do tf^%ut(RC(0)["BUNQQ: Couldn't find ien for LAB_TEST (#60).;LEADFF: Couldn't find ien for LAB_TEST (#60).")
 QUIT
 ;
 ; We already have a test for panels to verify that BUN will file correcty using the name BUN
LABSYN ; @TEST Test lab entered using its synonym files properly
 D WAIT
 N SAM
 N DATE S DATE=$$NOW^XLFDT()
 S SAM(1)="PAT_SSN^"_PATSSN
 S SAM(2)="LAB_TEST^BUN" ; The .01 is "UREA NITROGEN"
 S SAM(3)="RESULT_DT^"_DATE
 S SAM(4)="RESULT_VAL^20"
 S SAM(5)="LOCATION^3E NORTH"
 D LABMAKE^ISIIMPR2(.RC,.SAM)
 D eq^%ut($$LABDUP^ISIIMPU7(DFN,DATE,"BUN"),1)
 D eq^%ut(RC(0),1)
 QUIT
 ;
LABNOCOLL ; @TEST Test lab w/o config'd coll sample files properly
 D WAIT
 N SAM
 N DATE S DATE=$$NOW^XLFDT()
 S SAM(1)="PAT_SSN^"_PATSSN
 S SAM(2)="LAB_TEST^QUANTIFERON GOLD"
 S SAM(3)="RESULT_DT^"_DATE
 S SAM(4)="RESULT_VAL^0"
 S SAM(5)="LOCATION^3E NORTH"
 S SAM(6)="COLLECTION_SAMPLE^BLOOD"
 D LABMAKE^ISIIMPR2(.RC,.SAM)
 D eq^%ut($$LABDUP^ISIIMPU7(DFN,DATE,"QUANTIFERON GOLD"),1,"error 1")
 D eq^%ut(RC(0),1,"error 2")
 QUIT
 ;
CBCNOCOLL ; @TEST CBC Panel (one item is missing a collection sample)
 D WAIT
 N SAM
 N DATE S DATE=$$NOW^XLFDT()
 S SAM(1)="PAT_SSN^"_PATSSN
 S SAM(2)="LAB_PANEL^CBC"
 S SAM(3)="LAB_TEST^WBC^4.5"
 S SAM(4)="LAB_TEST^RBC^5.4"
 S SAM(5)="LAB_TEST^HGB^14.5"
 S SAM(6)="LAB_TEST^HCT^45"
 S SAM(7)="LAB_TEST^MCV^90"
 S SAM(8)="LAB_TEST^MCH^28"
 S SAM(9)="LAB_TEST^MCHC^36"
 S SAM(10)="LAB_TEST^RDW^14"
 S SAM(11)="LAB_TEST^MPV^8.0"
 S SAM(12)="LAB_TEST^PLATELET COUNT^180"
 S SAM(13)="RESULT_DT^"_DATE
 S SAM(14)="LOCATION^3E NORTH"
 D LABPANEL^ISIIMPR2(.RC,.SAM)
 N I,LAB F I=0:0 S I=$O(SAM(I)) Q:'I  I $P(SAM(I),U)="LAB_TEST" S LAB=$P(SAM(I),U,2) D eq^%ut($$LABDUP^ISIIMPU7(DFN,DATE,LAB),1)
 D eq^%ut(RC(0),1)
 QUIT
 ;
CBCCOLL ; @TEST CBC Panel with an explicit Collection Sample
 D WAIT
 N SAM
 N DATE S DATE=$$NOW^XLFDT()
 S SAM(1)="PAT_SSN^"_PATSSN
 S SAM(2)="LAB_PANEL^CBC"
 S SAM(3)="LAB_TEST^WBC^4.5"
 S SAM(4)="LAB_TEST^RBC^5.4"
 S SAM(5)="LAB_TEST^HGB^14.5"
 S SAM(6)="LAB_TEST^HCT^45"
 S SAM(7)="LAB_TEST^MCV^90"
 S SAM(8)="LAB_TEST^MCH^28"
 S SAM(9)="LAB_TEST^MCHC^36"
 S SAM(10)="LAB_TEST^RDW^14"
 S SAM(11)="LAB_TEST^MPV^8.0"
 S SAM(12)="LAB_TEST^PLATELET COUNT^180"
 S SAM(13)="RESULT_DT^NOW"
 S SAM(14)="LOCATION^3E NORTH"
 S SAM(15)="COLLECTION_SAMPLE^BLOOD"
 D LABPANEL^ISIIMPR2(.RC,.SAM)
 N I,LAB F I=0:0 S I=$O(SAM(I)) Q:'I  I $P(SAM(I),U)="LAB_TEST" S LAB=$P(SAM(I),U,2) D eq^%ut($$LABDUP^ISIIMPU7(DFN,DATE,LAB),1)
 D eq^%ut(RC(0),1)
 QUIT
 ;
MODALLAB ; @TEST Test a Modal Lab
 D WAIT
 N SAM
 N DATE S DATE=$$NOW^XLFDT()
 S SAM(1)="PAT_SSN^"_PATSSN
 S SAM(2)="LAB_TEST^RPR"
 S SAM(3)="RESULT_DT^"_DATE
 S SAM(4)="RESULT_VAL^Reactive"
 S SAM(5)="LOCATION^3E NORTH"
 D LABMAKE^ISIIMPR2(.RC,.SAM)
 D eq^%ut($$LABDUP^ISIIMPU7(DFN,DATE,"RPR"),1,"error 1")
 D eq^%ut(RC(0),1,"error 2")
 QUIT
 ;
DECIMAL1 ; @TEST Numeric lab with various proactive rounding
 D WAIT
 N SAM
 N DATE S DATE=$$NOW^XLFDT()
 S SAM(1)="PAT_SSN^"_PATSSN
 S SAM(2)="LAB_TEST^GLUCOSE"
 S SAM(3)="RESULT_DT^"_DATE
 S SAM(4)="RESULT_VAL^189.3322" ; <--unacceptable in lab package
 S SAM(5)="LOCATION^3E NORTH"
 D LABMAKE^ISIIMPR2(.RC,.SAM)
 D eq^%ut($$LABDUP^ISIIMPU7(DFN,DATE,"GLUCOSE"),1,"error 1")
 D eq^%ut(RC(0),1,"error 2")
 QUIT
 ;
DECMIAL2 ; @TEST Rounding SPECIFIC GRAVITY causes a crash
 D WAIT
 N SAM
 N DATE S DATE=$$NOW^XLFDT()
 S SAM(1)="PAT_SSN^"_PATSSN
 S SAM(2)="LAB_TEST^SPECIFIC GRAVITY"
 S SAM(3)="RESULT_DT^"_DATE
 S SAM(4)="RESULT_VAL^1.0299" ; <--unacceptable in lab package
 S SAM(5)="LOCATION^3E NORTH"
 D LABMAKE^ISIIMPR2(.RC,.SAM)
 D eq^%ut($$LABDUP^ISIIMPU7(DFN,DATE,"SPECIFIC GRAVITY"),1,"error 1")
 D eq^%ut(RC(0),1,"error 2")
 QUIT
 ;
UA ; @TEST Urine Analysis
 D WAIT
 N SAM
 N DATE S DATE=$$NOW^XLFDT()
 S SAM(1)="PAT_SSN^"_PATSSN
 S SAM(2)="LAB_PANEL^UA"
 S SAM(3)="LAB_TEST^URINE COLOR^BROWN"
 S SAM(4)="LAB_TEST^APPEARANCE^CLOUDY"
 S SAM(5)="LAB_TEST^SPECIFIC GRAVITY^1.030"
 S SAM(6)="LAB_TEST^URINE PH^7.2"
 S SAM(7)="LAB_TEST^URINE BLOOD^NEG"
 S SAM(8)="LAB_TEST^URINE BILIRUBIN^NEG"
 S SAM(9)="LAB_TEST^URINE KETONES^NEG"
 S SAM(10)="LAB_TEST^URINE GLUCOSE^NEG"
 S SAM(11)="LAB_TEST^URINE PROTEIN^NEG"
 S SAM(12)="LAB_TEST^UROBILINOGEN^1+"
 S SAM(13)="LAB_TEST^URINE EPITH CELLS^NONEOBS"
 S SAM(14)="LAB_TEST^URINE CRYSTALS^NONEOBS"
 S SAM(15)="LAB_TEST^URINE YEAST^NONEOBS"
 S SAM(16)="LAB_TEST^URINE BACTERIA^NONEOBS"
 S SAM(17)="LAB_TEST^URINE CASTS^NONEOBS"
 S SAM(18)="LAB_TEST^URINE MUCUS^NONE"
 S SAM(19)="RESULT_DT^"_DATE
 S SAM(20)="LOCATION^3E NORTH"
 D LABPANEL^ISIIMPR2(.RC,.SAM)
 D eq^%ut(RC(0),1,"error 1")
 D eq^%ut($$LABDUP^ISIIMPU7(DFN,DATE,"URINE COLOR"),1,"error 2")
 QUIT
 ;
UAP ; @TEST Partial Urine Analysis
 D WAIT
 N SAM
 N DATE S DATE=$$NOW^XLFDT()
 S SAM(1)="PAT_SSN^"_PATSSN
 S SAM(2)="LAB_PANEL^UA"
 S SAM(3)="LAB_TEST^URINE COLOR^YELLOW"
 S SAM(4)="LAB_TEST^APPEARANCE^CLEAR"
 S SAM(5)="LAB_TEST^SPECIFIC GRAVITY^1.031"
 S SAM(6)="LAB_TEST^URINE PH^7.3933"
 S SAM(7)="RESULT_DT^"_DATE
 S SAM(8)="LOCATION^3E NORTH"
 D LABPANEL^ISIIMPR2(.RC,.SAM)
 D eq^%ut(RC(0),1,"error 1")
 D eq^%ut($$LABDUP^ISIIMPU7(DFN,DATE,"URINE COLOR"),1,"error 2")
 QUIT
 ;
INVLVAL ; @TEST Test Invalid atomic values
 N SAM
 N DATE S DATE=$$NOW^XLFDT()
 S SAM(1)="PAT_SSN^"_PATSSN
 S SAM(2)="LAB_TEST^GLUCOSE"
 S SAM(3)="RESULT_DT^"_DATE
 S SAM(4)="RESULT_VAL^positive" ; <--unacceptable in lab package
 S SAM(5)="LOCATION^3E NORTH"
 D LABMAKE^ISIIMPR2(.RC,.SAM)
 D eq^%ut(RC(0),"-1^GLUCOSE result validation error: TYPE A WHOLE NUMBER BETWEEN 0 AND 2500","error 1")
 D eq^%ut($$LABDUP^ISIIMPU7(DFN,DATE,"GLUCOSE"),0,"error 2")
 QUIT
 ;
INVPVAL ; @TEST Test Invalid panel values
 N SAM
 N DATE S DATE=$$NOW^XLFDT()
 S SAM(1)="PAT_SSN^"_PATSSN
 S SAM(2)="LAB_PANEL^UA"
 S SAM(3)="LAB_TEST^URINE COLOR^YELLOW"
 S SAM(4)="LAB_TEST^APPEARANCE^CLEAR"
 S SAM(5)="LAB_TEST^SPECIFIC GRAVITY^CLEAR" ; <--INVALID
 S SAM(6)="LAB_TEST^URINE PH^WHITE"         ; <--INVALID
 S SAM(7)="RESULT_DT^"_DATE
 S SAM(8)="LOCATION^3E NORTH"
 D LABPANEL^ISIIMPR2(.RC,.SAM)
 ; INVPVAL - Test Invalid panel valuesRC(0)="-1^SPECIFIC GRAVITY: SPECIFIC GRAVITY result validation error: If no ""." " is entered, value will be assumed to be nn/1000+1.000;URINE PH: URINE PH resul t validation error: TYPE A NUMBER BETWEEN 1 AND 14"
 D tf^%ut(RC(0)["SPECIFIC GRAVITY result validation error")
 D tf^%ut(RC(0)["URINE PH result validation error")
 QUIT
 ;
INVMLAB ; @TEST Test Invalid modal lab
 N SAM
 N DATE S DATE=$$NOW^XLFDT()
 S SAM(1)="PAT_SSN^"_PATSSN
 S SAM(2)="LAB_TEST^RPR"
 S SAM(3)="RESULT_DT^"_DATE
 S SAM(4)="RESULT_VAL^foofoo"
 S SAM(5)="LOCATION^3E NORTH"
 D LABMAKE^ISIIMPR2(.RC,.SAM)
 D tf^%ut(RC(0)["RPR result validation error")
 QUIT
 ;
INVTLAB ; @TEST Invalid lab text value
 N SAM
 N DATE S DATE=$$NOW^XLFDT()
 S SAM(1)="PAT_SSN^"_PATSSN
 S SAM(2)="LAB_TEST^URINE COLOR"
 S SAM(3)="RESULT_DT^"_DATE
 S SAM(4)="RESULT_VAL^Brown Color (Finding)"
 S SAM(5)="LOCATION^3E NORTH"
 S SAM(6)="COLLECTION_SAMPLE^URINE"
 D LABMAKE^ISIIMPR2(.RC,.SAM)
 D tf^%ut(RC(0)["ANSWER MUST BE 3-7 CHARACTERS IN LENGTH")
 QUIT
 ;
KEEPME1 ; Code to keep that examines all data points in lab for me to look at all input transforms
 N LRTS,TEST,LRXD,LRXDP
 F LRTS=0:0 S LRTS=$O(^LAB(60,LRTS)) Q:'LRTS  D
 . ;W ^LAB(60,LRTS,0),!
 . S TEST=$P(^LAB(60,LRTS,0),U)
 . S LRXD=U_$P(^LAB(60,LRTS,0),U,12)
 . W $J(LRTS,4)," ",TEST," ",LRXD
 . I LRXD=U W ! QUIT  ; no data name
 . S LRXDP=LRXD_"0)",LRXDP=@LRXDP
 . W " ",LRXDP,!
 QUIT
 ;
KEEPME2 ; List of labs that have LOINC codes
 F IX="AI","AH" F LOINC=0:0 S LOINC=$O(^LAM(IX,LOINC)) Q:'LOINC  F WLIEN=0:0 S WLIEN=$O(^LAM(IX,LOINC,WLIEN)) Q:'WLIEN  D
 . S RNLT=$P(^LAM(WLIEN,0),U,2)
 . S LIEN=$O(^LAB(60,"AE",RNLT,""))
 . Q:'LIEN
 . W $P(^LAB(60,LIEN,0),U)," ",LOINC,!
 QUIT
 ;
LOINC ; @TEST Lookup lab by LOINC
 ; FHS says this is too primitve
 D WAIT
 S LOINC="1558-6"
 S SAM(1)="PAT_SSN^"_PATSSN
 S SAM(2)="LAB_TEST^"_LOINC
 N DATE S DATE=$$NOW^XLFDT()
 S SAM(3)="RESULT_DT^"_DATE
 S SAM(4)="RESULT_VAL^189"
 S SAM(5)="LOCATION^3E NORTH"
 D LABMAKE^ISIIMPR2(.RC,.SAM)
 D eq^%ut($$LABDUP^ISIIMPU7(DFN,DATE,LOINC),1,"failure 1")
 D eq^%ut(RC(0),1,"failure 2")
 QUIT
 ;
PLASMA ; @TEST Test Blood/Serum/Plasma Collection Sample
 N LRDFN,RVDT
 N SAMPLE F SAMPLE="BLOOD","SERUM","PLASMA" D
 . D WAIT
 . N SAM
 . S SAM(1)="PAT_SSN^"_PATSSN
 . S SAM(2)="LAB_TEST^TRIGLYCERIDE"
 . N DATE S DATE=$$NOW^XLFDT()
 . S SAM(3)="RESULT_DT^"_DATE
 . S SAM(4)="RESULT_VAL^189"
 . S SAM(5)="LOCATION^3E NORTH"
 . S SAM(6)="COLLECTION_SAMPLE^"_SAMPLE
 . D LABMAKE^ISIIMPR2(.RC,.SAM)
 . S LRDFN=$$LRDFN^LRPXAPIU(DFN)
 . S RVDT=$$LRIDT^LRPXAPIU(DATE)
 . N SPEC S SPEC=$P(^LR(LRDFN,"CH",RVDT,0),U,5)
 . D eq^%ut($$GET1^DIQ(61,SPEC_",",.01),SAMPLE,"failure 0")
 . D eq^%ut($$LABDUP^ISIIMPU7(DFN,DATE,"TRIGLYCERIDE"),1,"failure 1")
 . D eq^%ut(RC(0),1,"failure 2")
 QUIT
 ;
GLUURINE ; @TEST Urine Glucose (to test a non-blood collection sample)
 D WAIT
 N SAM
 N DATE S DATE=$$NOW^XLFDT()
 S SAM(1)="PAT_SSN^"_PATSSN
 S SAM(2)="LAB_TEST^GLUCOSE"
 S SAM(3)="RESULT_DT^"_DATE
 S SAM(4)="RESULT_VAL^189"
 S SAM(5)="LOCATION^3E NORTH"
 S SAM(6)="COLLECTION_SAMPLE^URINE"
 D LABMAKE^ISIIMPR2(.RC,.SAM)
 D eq^%ut($$LABDUP^ISIIMPU7(DFN,DATE,"GLUCOSE"),1,"error 1")
 D eq^%ut(RC(0),1,"error 2")
 S LRDFN=$$LRDFN^LRPXAPIU(DFN)
 S RVDT=$$LRIDT^LRPXAPIU(DATE)
 N SPEC S SPEC=$P(^LR(LRDFN,"CH",RVDT,0),U,5)
 D eq^%ut($$GET1^DIQ(61,SPEC_",",.01),"URINE","failure 0")
 QUIT
 ;
SPUTUM ; @TEST Sputum Sample
 D WAIT
 K SAM,RC
 N DATE S DATE=$$NOW^XLFDT()
 S SAM(1)="PAT_SSN^"_PATSSN
 S SAM(2)="LAB_PANEL^CIE SCREEN"
 S SAM(3)="LAB_TEST^CIE: GROUP B STREPTOCOCCUS^POS"
 S SAM(4)="LAB_TEST^CIE: PNEUMOCOCCUS^NEG"
 S SAM(5)="LAB_TEST^CIE: MENINGOCOCCUS^NEG"
 S SAM(6)="RESULT_DT^"_DATE
 S SAM(7)="LOCATION^3E NORTH"
 S SAM(8)="COLLECTION_SAMPLE^SPUTUM"
 DO LABPANEL^ISIIMPR2(.RC,.SAM)
 N I,LAB F I=0:0 S I=$O(SAM(I)) Q:'I  I $P(SAM(I),U)="LAB_TEST" S LAB=$P(SAM(I),U,2) D eq^%ut($$LABDUP^ISIIMPU7(DFN,DATE,LAB),1)
 D eq^%ut(RC(0),1)
 S LRDFN=$$LRDFN^LRPXAPIU(DFN)
 S RVDT=$$LRIDT^LRPXAPIU(DATE)
 N SPEC S SPEC=$P(^LR(LRDFN,"CH",RVDT,0),U,5)
 D eq^%ut($$GET1^DIQ(61,SPEC_",",.01),"SPUTUM","failure 0")
 QUIT
 ;
CSF ; @TEST Test CSF Sample
 D WAIT
 K SAM,RC
 N DATE S DATE=$$NOW^XLFDT()
 S SAM(1)="PAT_SSN^"_PATSSN
 S SAM(2)="LAB_PANEL^CIE SCREEN"
 S SAM(3)="LAB_TEST^CIE: GROUP B STREPTOCOCCUS^POS"
 S SAM(4)="LAB_TEST^CIE: PNEUMOCOCCUS^NEG"
 S SAM(5)="LAB_TEST^CIE: MENINGOCOCCUS^NEG"
 S SAM(6)="RESULT_DT^"_DATE
 S SAM(7)="LOCATION^3E NORTH"
 S SAM(8)="COLLECTION_SAMPLE^CSF"
 DO LABPANEL^ISIIMPR2(.RC,.SAM)
 N I,LAB F I=0:0 S I=$O(SAM(I)) Q:'I  I $P(SAM(I),U)="LAB_TEST" S LAB=$P(SAM(I),U,2) D eq^%ut($$LABDUP^ISIIMPU7(DFN,DATE,LAB),1)
 D eq^%ut(RC(0),1)
 S LRDFN=$$LRDFN^LRPXAPIU(DFN)
 S RVDT=$$LRIDT^LRPXAPIU(DATE)
 N SPEC S SPEC=$P(^LR(LRDFN,"CH",RVDT,0),U,5)
 D eq^%ut($$GET1^DIQ(61,SPEC_",",.01),"CEREBROSPINAL FLUID","failure 0")
 QUIT
 ;
TDAPIE1 ; @TEST Test direct API individual lab no errors
 D WAIT
 K SAM,RC
 N DATE S DATE=$$NOW^XLFDT()
 S SAM("PAT_SSN")=PATSSN
 S SAM("LAB_TEST")="GLUCOSE"
 S SAM("RESULT_DT")=DATE
 S SAM("RESULT_VAL")=89
 S SAM("LOCATION")="3E NORTH"
 N % S %=$$LAB^ISIIMP12(.RC,.SAM)
 D eq^%ut($$LABDUP^ISIIMPU7(DFN,DATE,"GLUCOSE"),1)
 D eq^%ut(RC(0),1)
 D eq^%ut(%,1)
 QUIT
 ;
TDAPIE2 ; @TEST Test direct API no errors
 D WAIT
 K SAM,RC
 N DATE S DATE=$$NOW^XLFDT()
 S SAM("PAT_SSN")=PATSSN
 S SAM("LAB_PANEL")="CHEM 7"
 S SAM("LAB_TEST","GLUCOSE")=180
 S SAM("LAB_TEST","CO2")=34
 S SAM("LAB_TEST","BUN")=10
 S SAM("LAB_TEST","CHLORIDE")=100
 S SAM("LAB_TEST","CREATININE")=1.1
 S SAM("LAB_TEST","POTASSIUM")=4.7
 S SAM("LAB_TEST","SODIUM")=150
 S SAM("RESULT_DT")=DATE
 S SAM("LOCATION")="3E NORTH"
 N % S %=$$LAB^ISIIMP12(.RC,.SAM)
 N LAB S LAB=""
 F  S LAB=$O(SAM("LAB_TEST",LAB)) Q:LAB=""  D eq^%ut($$LABDUP^ISIIMPU7(DFN,DATE,LAB),1)
 D eq^%ut(RC(0),1)
 D eq^%ut(%,1)
 QUIT
 ;
TDAPIE3 ; @TEST Test direct API Missing Panel Name
 K SAM,RC
 N DATE S DATE=$$NOW^XLFDT()
 S SAM("PAT_SSN")=PATSSN
 S SAM("LAB_PANEL")=""
 S SAM("LAB_TEST","GLUCOSE")=180
 S SAM("LAB_TEST","CO2")=34
 S SAM("LAB_TEST","BUN")=10
 S SAM("LAB_TEST","CHLORIDE")=100
 S SAM("LAB_TEST","CREATININE")=1.1
 S SAM("LAB_TEST","POTASSIUM")=4.7
 S SAM("LAB_TEST","SODIUM")=150
 S SAM("RESULT_DT")=DATE
 S SAM("LOCATION")="3E NORTH"
 N % S %=$$LAB^ISIIMP12(.RC,.SAM)
 D eq^%ut(RC(0),0)
 D eq^%ut(%,"-1^Missing LAB_PANEL.")
 K SAM("LAB_PANEL")
 N % S %=$$LAB^ISIIMP12(.RC,.SAM)
 D eq^%ut(RC(0),0)
 D eq^%ut(%,"-1^Missing value for LAB_TEST (#60).")
 QUIT
 ;
TDAPIE4 ; @TEST Test direct API Missing SSN
 K SAM,RC
 N DATE S DATE=$$NOW^XLFDT()
 S SAM("PAT_SSN")=""
 S SAM("LAB_PANEL")="CHEM 7"
 S SAM("LAB_TEST","GLUCOSE")=180
 S SAM("LAB_TEST","CO2")=34
 S SAM("LAB_TEST","BUN")=10
 S SAM("LAB_TEST","CHLORIDE")=100
 S SAM("LAB_TEST","CREATININE")=1.1
 S SAM("LAB_TEST","POTASSIUM")=4.7
 S SAM("LAB_TEST","SODIUM")=150
 S SAM("RESULT_DT")=DATE
 S SAM("LOCATION")="3E NORTH"
 N % S %=$$LAB^ISIIMP12(.RC,.SAM)
 D eq^%ut(RC(0),0)
 D eq^%ut(%,"-1^Invalid PAT_SSN (#2,.09).")
 K SAM("PAT_SSN")
 N % S %=$$LAB^ISIIMP12(.RC,.SAM)
 D eq^%ut(RC(0),0)
 D eq^%ut(%,"-1^Missing Patient SSN.")
 QUIT
 ;
TDAPISYN1 ; @TEST Synthea BMP
 D WAIT
 K SAM,RC
 N DATE S DATE=$$NOW^XLFDT()
 S SAM("COLLECTION_SAMPLE")="SERUM"
 S SAM("LAB_PANEL")="BASIC METABOLIC PANEL"
 S SAM("LAB_TEST","CALCIUM")=8.82
 S SAM("LAB_TEST","CHLORIDE")=110.65
 S SAM("LAB_TEST","CO2")=28.58
 S SAM("LAB_TEST","CREATININE")=2.0732
 S SAM("LAB_TEST","GLUCOSE")=100.35
 S SAM("LAB_TEST","POTASSIUM")=4.61
 S SAM("LAB_TEST","SODIUM")=140.69
 S SAM("LAB_TEST","UREA NITROGEN")=9.16
 S SAM("LOCATION")="GENERAL MEDICINE"
 S SAM("PAT_SSN")=PATSSN
 S SAM("RESULT_DT")=DATE
 N % S %=$$LAB^ISIIMP12(.RC,.SAM)
 N LAB S LAB=""
 F  S LAB=$O(SAM("LAB_TEST",LAB)) Q:LAB=""  D eq^%ut($$LABDUP^ISIIMPU7(DFN,DATE,LAB),1)
 D eq^%ut(RC(0),1)
 D eq^%ut(%,1)
 QUIT
 ;
TDAPISYN2 ; @TEST Synthea UA
 D WAIT
 K SAM,RC
 N DATE S DATE=$$NOW^XLFDT()
 S SAM("COLLECTION_SAMPLE")="URINE"
 S SAM("LAB_PANEL")="URINALYSIS"
 S SAM("LAB_TEST","APPEARANCE")="CLOUDY"
 S SAM("LAB_TEST","URINE COLOR")="BROWN"
 ;S SAM("LAB_TEST","LEUCOCYTE ESTERASE, URINE")="NEG"
 ;S SAM("LAB_TEST","NITRITE, URINE")="NEG"
 S SAM("LAB_TEST","SPECIFIC GRAVITY")=1.0343
 S SAM("LAB_TEST","URINE BILIRUBIN")="1+"
 S SAM("LAB_TEST","URINE GLUCOSE")="2+"
 S SAM("LAB_TEST","URINE KETONES")="1+"
 S SAM("LAB_TEST","URINE PH")=5.9757
 S SAM("LAB_TEST","URINE PROTEIN")="2+"
 S SAM("LOCATION")="GENERAL MEDICINE"
 S SAM("PAT_SSN")=PATSSN
 S SAM("RESULT_DT")=DATE
 N % S %=$$LAB^ISIIMP12(.RC,.SAM)
 N LAB S LAB=""
 F  S LAB=$O(SAM("LAB_TEST",LAB)) Q:LAB=""  D eq^%ut($$LABDUP^ISIIMPU7(DFN,DATE,LAB),1)
 D eq^%ut(RC(0),1)
 D eq^%ut(%,1)
 QUIT
 ;
TDAPISYN3 ; @TEST Synthea Lipid Panel
 D WAIT
 K SAM,RC
 N DATE S DATE=$$NOW^XLFDT()
 S SAM("COLLECTION_SAMPLE")="SERUM"
 S SAM("LAB_PANEL")="LIPID PROFILE"
 S SAM("LAB_TEST","CHOLESTEROL")=223.71
 S SAM("LAB_TEST","HDL")=52.78
 S SAM("LAB_TEST","LDL CHOLESTEROL")=134.37
 S SAM("LAB_TEST","TRIGLYCERIDE")=182.77
 S SAM("LOCATION")="GENERAL MEDICINE"
 S SAM("PAT_SSN")=PATSSN
 S SAM("RESULT_DT")=DATE
 N % S %=$$LAB^ISIIMP12(.RC,.SAM)
 N LAB S LAB=""
 F  S LAB=$O(SAM("LAB_TEST",LAB)) Q:LAB=""  D eq^%ut($$LABDUP^ISIIMPU7(DFN,DATE,LAB),1)
 D eq^%ut(RC(0),1)
 D eq^%ut(%,1)
 QUIT
 ;
TDAPISYN4 ; @TEST Synthea CBC Panel
 D WAIT
 K SAM,RC
 N DATE S DATE=$$NOW^XLFDT()
 S SAM("COLLECTION_SAMPLE")="BLOOD"
 S SAM("LAB_PANEL")="CBC"
 S SAM("LAB_TEST","HCT")=42.062
 S SAM("LAB_TEST","HGB")=14.282
 S SAM("LAB_TEST","MCH")=28.734
 S SAM("LAB_TEST","MCHC")=34.805
 S SAM("LAB_TEST","MCV")=84.09
 S SAM("LAB_TEST","MPV")=9.6203
 S SAM("LAB_TEST","PLATELET COUNT")=240.87
 S SAM("LAB_TEST","RBC")=4.4941
 S SAM("LAB_TEST","RDW")=42.843
 S SAM("LAB_TEST","WBC")=4.979
 S SAM("LOCATION")="GENERAL MEDICINE"
 S SAM("PAT_SSN")=PATSSN
 S SAM("RESULT_DT")=DATE
 N % S %=$$LAB^ISIIMP12(.RC,.SAM)
 N LAB S LAB=""
 F  S LAB=$O(SAM("LAB_TEST",LAB)) Q:LAB=""  D eq^%ut($$LABDUP^ISIIMPU7(DFN,DATE,LAB),1)
 D eq^%ut(RC(0),1)
 D eq^%ut(%,1)
 QUIT
 ;
TLABSEQ ; @TEST Two labs in order; lab package does timeshift
 D WAIT
 N SAM
 N DATE S DATE=$$NOW^XLFDT()
 S SAM(1)="PAT_SSN^"_PATSSN
 S SAM(2)="LAB_TEST^LEAD"
 S SAM(3)="RESULT_DT^"_DATE
 S SAM(4)="RESULT_VAL^189"
 S SAM(5)="LOCATION^3E NORTH"
 D LABMAKE^ISIIMPR2(.RC,.SAM)
 D eq^%ut($$LABDUP^ISIIMPU7(DFN,DATE,"LEAD"),1)
 D eq^%ut(RC(0),1)
 K SAM
 S SAM(1)="PAT_SSN^"_PATSSN
 S SAM(2)="LAB_TEST^GLUCOSE"
 S SAM(3)="RESULT_DT^"_DATE
 S SAM(4)="RESULT_VAL^189"
 S SAM(5)="LOCATION^3E NORTH"
 D LABMAKE^ISIIMPR2(.RC,.SAM)
 S DATE=$$FMADD^XLFDT(DATE,0,0,0,1)
 D eq^%ut($$LABDUP^ISIIMPU7(DFN,DATE,"GLUCOSE"),1)
 D eq^%ut(RC(0),1)
 QUIT
 ;
TBLOODGAS ; @TEST Blood Gases
 D WAIT
 N SAM,RC
 N DATE S DATE=$$NOW^XLFDT()
 S SAM("LAB_PANEL")="BLOOD GASES"
 S SAM("LAB_TEST","FIO2")=83.82
 S SAM("LAB_TEST","HB (HGB)")=15.62
 S SAM("LAB_TEST","O2HB%  (SAT)")=95.53
 S SAM("LAB_TEST","COHB%")=1.34
 S SAM("LAB_TEST","METHB%")=1.45
 S SAM("LAB_TEST","O2CT.")=19.78
 S SAM("LAB_TEST","PH ")=7.37
 S SAM("LAB_TEST","PCO2")=37.02
 S SAM("LAB_TEST","PO2")=96.26
 S SAM("LAB_TEST","BASE EXCESS")=0.26
 S SAM("LAB_TEST","BICARBONATE (SBC)")=26.7
 S SAM("LAB_TEST","CO2CT. (TCO2)")=24.4
 S SAM("LAB_TEST","PT. TEMP")=36.81
 S SAM("LAB_TEST","PH AT PT. TEMP")=7.38
 S SAM("LAB_TEST","PCO2 AT PT. TEMP")=35.92
 S SAM("LAB_TEST","PO2 AT PT. TEMP")=84.64
 S SAM("LOCATION")="GENERAL MEDICINE"
 S SAM("PAT_SSN")=PATSSN
 S SAM("RESULT_DT")=DATE
 N % S %=$$LAB^ISIIMP12(.RC,.SAM)
 N LAB S LAB=""
 F  S LAB=$O(SAM("LAB_TEST",LAB)) Q:LAB=""  D eq^%ut($$LABDUP^ISIIMPU7(DFN,DATE,LAB),1)
 D eq^%ut(RC(0),1)
 D eq^%ut(%,1)
 QUIT
 ;
TCARENZ ; @TEST Cardiac Enzymes
 D WAIT
 N SAM,RC
 N DATE S DATE=$$NOW^XLFDT()
 S SAM("LAB_PANEL")="CARDIAC ENZYMES"
 S SAM("LAB_TEST","ALK PHOS")=145.0
 S SAM("LAB_TEST","LDH")=240.61
 S SAM("LAB_TEST","CPK ")=112.02
 S SAM("LOCATION")="GENERAL MEDICINE"
 S SAM("PAT_SSN")=PATSSN
 S SAM("RESULT_DT")=DATE
 N % S %=$$LAB^ISIIMP12(.RC,.SAM)
 N LAB S LAB=""
 F  S LAB=$O(SAM("LAB_TEST",LAB)) Q:LAB=""  D eq^%ut($$LABDUP^ISIIMPU7(DFN,DATE,LAB),1)
 D eq^%ut(RC(0),1)
 D eq^%ut(%,1)
 QUIT
 ;
TCRCL ; @TEST Creatine Clearance
 D WAIT
 N SAM
 S SAM(1)="PAT_SSN^"_PATSSN
 S SAM(2)="LAB_TEST^COMPUTED CREATININE CLEARANCE"
 N DATE S DATE=$$NOW^XLFDT()
 S SAM(3)="RESULT_DT^"_DATE
 S SAM(4)="RESULT_VAL^112.23"
 S SAM(5)="LOCATION^3E NORTH"
 S SAM(6)="COLLECTION_SAMPLE^BLOOD"
 D LABMAKE^ISIIMPR2(.RC,.SAM) 
 D eq^%ut($$LABDUP^ISIIMPU7(DFN,DATE,"COMPUTED CREATININE CLEARANCE"),1)
 D eq^%ut(RC(0),1)
 QUIT
 ;
WAIT ; Wait till a second passes so we can enter a new lab
 N LRDFN,RVDT
 S LRDFN=$$LRDFN^LRPXAPIU(DFN)
 F  S RVDT=$$LRIDT^LRPXAPIU($$NOW^XLFDT()) Q:'$D(^LR(LRDFN,"CH",RVDT))  H .001
 QUIT
