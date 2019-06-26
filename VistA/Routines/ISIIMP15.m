ISIIMP15 ;ISI GROUP/MLS -- NOTES IMPORT CONT.
 ;;3.0;ISI_DATA_LOADER;;Jun 26, 2019;Build 59
 ;
 ; VistA Data Loader 2.0
 ;
 ; Copyright (C) 2012 Johns Hopkins University
 ;
 ; VistA Data Loader is provided by the Johns Hopkins University School of
 ; Nursing, and funded by the Department of Health and Human Services, Office
 ; of the National Coordinator for Health Information Technology under Award
 ; Number #1U24OC000013-01.
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
 Q
 ;
VALIDATE()      ;
 ; Validate import array contents
 S ISIRC=$$VALNOTE^ISIIMPU8(.ISIMISC)
 Q ISIRC
 ;
MAKENOTE() ;
 ; Create patient(s)
 S ISIRC=$$IMPRTNOT(.ISIMISC)
 Q ISIRC
 ;
IMPRTNOT(ISIMISC) ;Create Progress Note entry
 ; Input - ISIMISC(ARRAY)
 ; Format:  ISIMISC(PARAM)=VALUE
 ;     eg:  ISIMISC("DFN")=12345
 ;
 ; Output - ISIRC [return code]
 ;          ISIRESUL(0)=1 [if successful]
 ;          ISIRESUL(1)=TIUDA [if successful]
 ;
 N DFN,VDT,ARRAY,SUPPRESS,NOASF,RESULT,VLOC,TIUDA,TITLE,TEXT,SIGN,VSTR,PROV
 S ISIRC=1,RESULT=""
 D PREP
 I +ISIRC<0 Q ISIRC
 D MAKETIU
 I +ISIRC<0 Q ISIRC
 D INSTXT
 I +ISIRC<0 Q ISIRC
 D SIGN
 I +ISIRC<0 Q ISIRC
 S ISIRESUL(0)=1,ISIRESUL(1)=$G(TIUDA)
 Q 1
 ;
PREP
 S DFN=$G(ISIMISC("DFN"))
 S TITLE=$G(ISIMISC("TIU"))
 S VLOC=$G(ISIMISC("VLOC"))
 S PROV=$G(ISIMISC("PROV"))
 S VDT=$G(ISIMISC("VDT"))
 S TEXT=$G(ISIMISC("TEXT"))
 K ARRAY
 S ARRAY(1202)=PROV
 S ARRAY(1301)=VDT
 S ARRAY(1205)=VLOC
 S VSTR=ISIMISC("VISIT")
 S SUPPRESS=0
 S NOASF=""
 S SIGN=$G(ISIMISC("ES"))
 S ISIRC=$S(DFN="":-1,TITLE="":-1,VLOC="":-1,PROV="":-1,VDT="":-1,VSTR="":-1,SIGN="":-1,1:1)
 I ISIRC=-1 S ISIRC="-1^Validation error (PREP ISIIMP15)" Q
 Q
 ;
MAKETIU
 D MAKE^TIUSRVP(.RESULT,DFN,TITLE,VDT,VLOC,"",.ARRAY,VSTR,SUPPRESS,NOASF)
 I +RESULT<1 S ISIRC="-1^Unable to create note" Q
 S TIUDA=RESULT
 Q
 ;
INSTXT
 K ARRAY
 S ARRAY("TEXT",1,0)=TEXT
 S ARRAY("HDR")="1^1"
 D SETTEXT^TIUSRVPT(.RESULT,TIUDA,.ARRAY,0)
 I +RESULT<1 S ISIRC="-1^Unable to insert text into note" Q
 Q
 ;
SIGN
 N ES
 S ES=$$ENCRYP^XUSRB1(SIGN)
 D SIGN^TIUSRVP2(.RESULT,TIUDA,ES)
 I +RESULT<0 S ISIRC="-1^Unable to electronically sign note" Q
 Q
