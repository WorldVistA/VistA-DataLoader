ISIIMP21 ;ISI GROUP/MLS -- RAD ORDERS IMPORT CONT.
 ;;3.1;VISTA DATALOADER;;Dec 23, 2024
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
 S ISIRC=$$VALRADO^ISIIMPUC(.ISIMISC)
 Q ISIRC
 ;
MAKERADO() ;
 ; Create patient(s)
 S ISIRC=$$RADO(.ISIMISC)
 Q ISIRC
 ;
RADO(ISIMISC) ;Create Radiology Order
 ; Input - ISIMISC(ARRAY)
 ; Format:  ISIMISC(PARAM)=VALUE
 ;     eg:  ISIMISC("MAG_LOC")= 23
 ;
 ; Output - ISIRC [return code]
 ;          ISIRESUL(0)=1 [if successful]
 ;          ISIRESUL(1)=RAOIFN [if successful]
 ;
 N RADFN,RAPROC,RAMLC,RADTE,RACAT,REQLOC,REQPHYS,RAREASON,RAMISC,RAOIFN,RESTATUS
 ;
 S ISIRC=$$PREP()
 I (+ISIRC<0) Q ISIRC
 ;
 S ISIRC=$$ORDER()
 I (+ISIRC<0) Q ISIRC
 I $G(RESTATUS)="O" Q
 ;
 S ISIRC=$$REGISTER()
 I (+ISIRC<0) Q ISIRC
 I $G(RESTATUS)="R" Q
 ;
 S ISIRC=$$EXAMINE()
 I (+ISIRC<0) Q ISIRC
 I $G(RESTATUS)="E" Q
 ;
 S ISIRC=$$COMPLETE()
 I (+ISIRC<0) Q ISIRC
 ;
 S ISIRESUL(0)=1
 S ISIRESUL(1)=RAOIFN
 Q ISIRC
 ;
PREP()
 ;
 I $G(ISIPARAM("DEBUG"))>0 D
 . W !,"+++ PREP^ISIIMP21)+++",!
 . I $D(ISIMISC) S X="" F  S X=$O(ISIMISC(X)) Q:X=""  W !,"ISIMISC("_X_")="_$G(ISIMISC(X))
 . W !,"<HIT RETURN TO PROCEED>" R X:5
 . Q
 ;
 S RESTATUS=$G(ISIMISC("EXAM_STATUS")) I RESTATUS="" S RESTATUS="O"
 S RADFN=$G(ISIMISC("DFN")) I RADFN="" Q "-1^Missing RADFN (PREP ISIIMP21)."
 S RAPROC=$G(ISIMISC("RAPROC")) I RAPROC="" Q "-1^Missing RAPROC (PREP ISIIMP21)."
 S RAMLC=$G(ISIMISC("MAGLOC")) I RAMLC="" Q "-1^Missing MAGLOC (PREP ISIIMP21)."
 S RADTE=$G(ISIMISC("RADTE")) I RADTE="" Q "-1^Missing RADTE (PREP ISIIMP21)."
 S RACAT=$G(ISIMISC("EXAMCAT")) I RACAT="" Q "-1^Missing EXAMCAT (PREP ISIIMP21)."
 S REQLOC=$G(ISIMISC("REQLOC")) I REQLOC="" Q "-1^Missing REQLOC (PREP ISIIMP21)."
 S REQPHYS=$G(ISIMISC("PROV")) I REQPHYS="" Q "-1^Missing PROV (PREP ISIIMP21)."
 S RAREASON=$G(ISIMISC("REASON")) I RAREASON="" Q "-1^Missing REASON (PREP ISIIMP21)."
 K RAMISC
 S RAMISC("ACLHIST",1)=ISIMISC("HISTORY") I RAMISC("ACLHIST",1)="" Q "-1^Missing HISTORY (PREP ISIIMP21)."
 I $G(RAMISC("PREGNANT"))'="Y" I $P($G(^DPT(RADFN,0)),U,2)="F" S RAMISC("PREGNANT")="N" ; hardcoded
 Q 1
 ;
ORDER()
 ;
 S RAOIFN=$$ORDER^RAMAG02(.RAMAG,RADFN,RAMLC,RAPROC,RADTE,RACAT,REQLOC,REQPHYS,RAREASON,.RAMISC)
 I (+RAOIFN<0) Q "-1^Error creating Rad Order (CREATE ISIIMP21): "_RAOIFN
 Q 1
 ;
REGISTER()
 ;
 N RAMISC K RAMSIC
 S RACAT="O"
 I '$G(RADFN) S RADFN=$G(ISIMISC("DFN")) I 'RADFN Q "-1^Missing RADFN (REGISTER ISIIMP21)."
 I '$G(RADTE) S RADTE=$G(ISIMISC("RADTE")) I 'RADTE Q "-1^Missing RADTE (REGISTER ISIIMP21)."
 I '$G(RAOIFN) Q "-1^Missing RAORINF (Order IEN) in REGISTER^ISIIMP21"
 N RACAT,ISIBUF,ISIMSG D
 . N IENS751 S IENS751=RAOIFN_","
 . D GETS^DIQ(75.1,IENS751,".01;4","I","ISIBUF","ISIMSG")
 . I $G(DIERR) S ISIRC="-1^VistA Error, pulling Order information (REGISTER ISIIMP21):"_DIERR Q
 . S RACAT=$G(ISIBUF(75.1,IENS751,4,"I"))
 . ;S RADFN=$G(ISIBUF(75.1,IENS751,.01,"I"))
 . S RACAT=$S($G(RACAT)'="":RACAT,1:"O")
 . Q
 I (+ISIRC<0) Q ISIRC
 S ISIRC=$$RAPTREG^RAMAGU04(RADFN) I (+ISIRC<0) Q ISIRC
 ;
 K RAMISC
 S RAMISC("FLAGS")="D"
 S RAMISC("EXAMCAT")="O" ;Outpatient CATEGORY OF EXAM field (4) of sub-file #70.03
 S RAMISC("PRINCLIN")=$G(ISIMISC("REQLOC")) ; LOCATION file (#44)
 S RAMISC("CLINHIST",1)=$G(ISIMISC("HISTORY"))_"   "
 S RAMISC("SERVICE")=$O(^DIC(49,"B","RADIOLOGY","")) ;IEN of SERVICE/SECTION (#49)
 S RAMISC("RAPROC")=$G(ISIMISC("RAPROC"))
 S RAMISC("MAGLOC")=$G(ISIMISC("MAGLOC"))
 S RADTE=$G(ISIMISC("RADTE"))
 ;S RAMISC("TECH")=$G(ISIMISC("TECH")) ; Technologist
 ;S RAMISC("TECHCOMM")=$G(ISIMISC("TECHCOM"))  ; Tech comments Captured."
 ;S RAMISC("PRIMINTSTF")=REQPHYS
 I $G(ISIPARAM("DEBUG"))>0 D
 . W !,"+++ REGISTER^ISIIMP21)+++",!
 . I $D(RAMISC) S X="" F  S X=$O(RAMISC(X)) Q:X=""  W !,"RAMISC("_X_")="_$G(RAMISC(X))
 . W !,"<HIT RETURN TO PROCEED>" R X:5
 . Q
 N RAMAG,OUT K RAMAG,OUT S ISIRC=$$REGISTER^RAMAG03(.RAMAG,.OUT,RAOIFN,RADTE,.RAMISC)
 I (+ISIRC<0) Q "-1^Order created, but can't register exam (REGISTER ISIIMP21): "_ISIRC
 ; S RADFN=$P(OUT(1),"^",1)
 S RADTI=$P(OUT(1),"^",2)
 S RACNI=$P(OUT(1),"^",3)
 S RACASE=$P(OUT(1),"^",4)
 S ACNUMB=$P(OUT(1),"^",5)
 S RAINTDT=$P(OUT(1),"^",6)
 Q 1
 ;
EXAMINE()
 ;
 I $G(ISIPARAM("DEBUG"))>0 D
 . W !,"+++ EXAMINE^ISIIMP21)+++",!
 . I $D(ISIMISC) S X="" F  S X=$O(ISIMISC(X)) Q:X=""  W !,"ISIMISC("_X_")="_$G(ISIMISC(X))
 . W !,"RADFN:",$G(RADFN)
 . W !,"RADTI:",$G(RADTI)
 . W !,"RACNT:",$G(RACNI)
 . W !,"<HIT RETURN TO PROCEED>" R X:5
 . Q
 ;
 I $G(RADFN)="" Q "-1^Trying to set to Examined.  Can't locate RADFN."
 I $G(RADTI)="" Q "-1^Trying to set to Examined.  Can't locate RADTI."
 I $G(RACNI)="" Q "-1^Trying to set to Examined.  Can't locate RACNI."
 I '$D(^DPT(RADFN,0)) Q "-1^Trying to set to Examined. Couldn't locate Patient File (#2)"
 ;
 S RACASE=RADFN_U_RADTI_U_RACNI
 I '$G(ISIMISC("TECH")) Q "-1^Trying to set to Examined. Can't locate Tech."
 S RAMISC("TECH",1)=$G(ISIMISC("TECH"))
 S RAMISC("TECHCOMM")=$G(ISIMISC("TECHCOMM"))
 S ISIRC=$$EXAMINED^RAMAG07("",RACASE,.RAMISC)
 I (+ISIRC<0) S ISIRC="-1^Failed to set Rad exam to Examined: "_ISIRC
 Q 1
 ;
COMPLETE()
 ;
 I $G(ISIPARAM("DEBUG"))>0 D
 . W !,"+++ COMPLETE^ISIIMP21 (ISIMISC)+++",!
 . I $D(ISIMISC) S X="" F  S X=$O(ISIMISC(X)) Q:X=""  W !,"ISIMISC("_X_")="_$G(ISIMISC(X))
 . W !,"RADFN:",$G(RADFN)
 . W !,"RADTI:",$G(RADTI)
 . W !,"RACNT:",$G(RACNI)
 . W !,"<HIT RETURN TO PROCEED>" R X:5
 . Q
 S RACASE=RADFN_U_RADTI_U_RACNI
 K RAMISC
 S RAMISC("FLAGS")="F"
 ;S RAMISC("TECH")=$G(ISIMISC("TECH"))
 ;S RAMISC("TECHCOMM")=$G(ISIMISC("TECHCOMM"))
 S RAMISC("REPORT",1)="Electronically signed, 'forced' to complete."
 S RAMISC("RPTDTE")=$P(ISIMISC("RADTE"),".") ;Reported Date field (8) of File #74
 S RAMISC("RPTSTATUS")="EF" ;electronically filed
 ;S RAMISC("IMPRESSION",1)=$G(ISIMISC("IMPRESSION"))
 ;S RAMISC("CLINHIST",1)=$G(ISIMISC("HISTORY"))
 ;S RAMISC("VERDTE")=$P(RADTE,".",1)
 ;S RAMISC("VERPHYS")=REQPHYS
 ;S RAMISC("PRIMDXCODE")=4
 ;S RAMISC("ELSIG")=""
 I $G(ISIPARAM("DEBUG"))>0 D
 . W !,"+++ COMPLETE^ISIIMP21 (RAMISC)+++",!
 . I $D(RAMISC) S X="" F  S X=$O(RAMISC(X)) Q:X=""  W !,"RAMISC("_X_")="_$G(RAMISC(X))
 . W !,"RACASE:",$G(RACASE)
 . W !,"<HIT RETURN TO PROCEED>" R X:5
 . Q
 N RAMAG S ISIRC=$$COMPLETE^RAMAG06(.RAMAG,.RACASE,.RAMISC)
 I (+ISIRC<0) Q "-1^Failed to complete rad exam (ISIIMP21): "_ISIRC
 Q 1
