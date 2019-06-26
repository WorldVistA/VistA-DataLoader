ISIIMP09 ;ISI GROUP/MLS -- VITALS IMPORT CONT. ;2019-06-26  11:24 AM
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
 S ISIRC=$$VALVITAL^ISIIMPU5(.ISIMISC)
 Q ISIRC
 ;
MAKEVIT() ;
 S ISIRC=0
 ; Create patient(s)
 S ISIRC=$$IMPORTVT(.ISIMISC)
 Q ISIRC
 ;
IMPORTVT(ISIMISC) ;Create Vitals entry
 ; Input - ISIMISC(ARRAY)
 ; Format:  ISIMISC(PARAM)=VALUE
 ;     eg:  ISIMISC("DFN")=12345
 ;
 ; Output - ISIRC [return code]
 N VTKNDT,DFN,TYP,RT,LOC,ENT
 D PREP
 I +$G(ISIRC)<0 Q ISIRC
 D VITALS
 Q ISIRC
 ;
PREP
 S VTKNDT=$G(ISIMISC("DT_TAKEN"))
 S DFN=$G(ISIMISC("DFN"))
 S TYP=$G(ISIMISC("VITAL_TYPE"))
 I $G(ISIMISC("RATE"))="" S ISIMISC("RATE")=$$GEN(TYP)
 S RT=$G(ISIMISC("RATE"))
 S LOC=$G(ISIMISC("LOCATION"))
 S ENT=$G(ISIMISC("ENTERED_BY"))
 ; Check/prevent duplicate entries
 I $$CHECKDUP(DFN,VTKNDT,TYP,LOC) S ISIRC="-1^Duplicate Vital Entry" Q
 Q
 ;
VITALS ;Add vitals for patient
 N RESULT K RESULT
 S DATA=VTKNDT_U_DFN_U_TYP_";"_RT_U_LOC_U_ENT
 D EN1^GMVDCSAV(.RESULT,DATA)
 I $G(RESULT(0))["ERROR" S ISIRC="-1^Error creating Vital entry (ISIIMP09)"
 Q:+ISIRC<0
 S ISIRESUL(0)="1"
 S ISIRESUL(1)="success"
 Q
 ;
GEN(TYPE) ;Generate values for vitals
 N READ
 S:TYPE=1 READ=($R(80)+110)_"/"_($R(30)+55)
 S:TYPE=2 READ=($R(2)+97)_"."_($R(9)+1)
 S:TYPE=3 READ=$R(8)+12
 S:TYPE=5 READ=$R(30)+65
 I TYPE=8 D
 .S HGT=$S($P($G(^GMR(120.5,+$O(^PXRMINDX(120.5,"PI",DFN,8,+$O(^PXRMINDX(120.5,"PI",DFN,8,""),-1),0)),0)),U,8):$P(^(0),U,8),1:(60+$R(18)))
 .S READ=HGT
 I TYPE=9 D
 . S WGT=$S($P($G(^GMR(120.5,+$O(^PXRMINDX(120.5,"PI",DFN,9,+$O(^PXRMINDX(120.5,"PI",DFN,9,""),-1),0)),0)),U,8):$P(^(0),U,8),1:(110+$R(150)))
 . S GORL=$R(2),LBS=$R(5),(READ,WGT)=WGT+($S(GORL=0:"-",1:"+")_LBS)
 S:TYPE=21 READ=$R(9)+91
 S:TYPE=22 READ=$R(3)
 Q READ
 ;
CHECKDUP(DFN,VDATE,VTYPE,VLOC)
 ; INPUT =
 ;    DFN = patient DFN
 ;  VDATE = date taken
 ;  VTYPE = matching vital ien
 ;   VLOC = location
 ;
 N EXIT,VITIEN,IENS,ZBUF,ZMSG
 S (EXIT,VITIEN,ISIRC)=0
 S VTYPE=$G(VTYPE)
 ;
 I 'VTYPE,$L(VTYPE) S VTYPE=$O(^GMRD(120.51,"B",VTYPE,""))
 ;
 F  S VITIEN=$O(^GMR(120.5,"B",VDATE,VITIEN)) Q:('VITIEN!(EXIT!(+$G(ISIRC)<0)))  D
 . S IENS=VITIEN_","
 . D GETS^DIQ(120.5,IENS,".02;.03;.05","I","ZBUF","ZMSG")
 . I $G(DIERR) S ISIRC="-1^Record Vitals Rpt: Fileman error" Q
 . I DFN'=$G(ZBUF(120.5,IENS,.02,"I")) Q
 . I VTYPE'=$G(ZBUF(120.5,IENS,.03,"I")) Q ; Vital Type ien
 . I VLOC'=$G(ZBUF(120.5,IENS,.05,"I")) Q ; location
 . S EXIT=VITIEN
 . Q
 ;
 Q EXIT
