ISIIMP11 ;;ISI GROUP/MLS -- ALLERGIES IMPORT CONT.
 ;;3.0;ISI_DATA_LOADER;;Jun 26, 2019
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
 S ISIRC=$$VALALG^ISIIMPU6(.ISIMISC)
 Q ISIRC
 ;
MAKEALG() ;
 ; Create patient(s)
 S ISIRC=$$IMPRTALG(.ISIMISC)
 Q ISIRC
 ;
IMPRTALG(ISIMISC) ;Create allergy entry
 ; Input - ISIMISC(ARRAY)
 ; Format:  ISIMISC(PARAM)=VALUE
 ;     eg:  ISIMISC("GMRAORIG")=12345
 ;
 ; Output - ISIRC [return code]
 ;          ISIRESUL(0)=1 [if successful]
 ;          ISIRESUL(1)="success" [if successful]
 ;
 N NODE,DFN,GMRARRAY,GMRAIEN,ORDFN
 ; Process No Known Allergies
 I $G(ISIMISC("GMRANKA"))="YES" D NKA Q ISIRC
 D PREP
 D ALLERGY
 Q ISIRC
 ;
PREP
 S GMRAIEN=0 ; used for update
 S DFN=ISIMISC("DFN")
 K ISIMISC("ALLERGEN"),ISIMISC("DFN"),ISIMISC("HISTORIC"),ISIMISC("ORIGINTR"),ISIMISC("ORIG_DATE")
 K ISIMISC("PAT_SSN"),ISIMISC("SYMPTOM")
 S NODE=$NAME(^TMP("GMRA",$J))
 K @NODE M @NODE=ISIMISC
 I $G(ISIPARAM("DEBUG"))>0 D
 . W !,"+++ Final values +++"
 . W !,"DFN:",DFN,!
 . I $D(ISIMISC) W $G(ISIMISC) S X="" F  S X=$O(ISIMISC(X)) Q:X=""  W !,X,":",ISIMISC(X)
 . Q
 Q
 ;
ALLERGY ;Add Allergies for patient
 D UPDATE^GMRAGUI1(0,DFN,NODE)
 Q:+ISIRC<0 ;error
 S ISIRESUL(0)=1
 S ISIRESUL(1)="Success"
 Q
 ;
NKA ;Add No Known Allergies
 S ORDFN=ISIMISC("DFN") N ORY
 D NKA^GMRAGUI1 S ISIRC=$G(ORY)
 Q:+ISIRC<0 ;error
 S ISIRESUL(0)=1
 S ISIRESUL(1)="Success"
 Q
