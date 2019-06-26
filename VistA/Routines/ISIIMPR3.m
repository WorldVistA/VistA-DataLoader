ISIIMPR3 ;ISI GROUP/MLS -- ISI DATA LOADER 2.0 RPC handlers
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
ADMIT(ISIRESUL,MISC)
 ;
 N $ETRAP,$ESTACK S $ETRAP="D ERR^ISIIMPER"
 N DFN,ADATE,ISIFAC,ISIWARD,ISIWARDIEN,ISIRMBD,ISIRMBDIEN,ISITYPE,ISITYPEIEN
    N ISIFTS,ISIFTSIEN,ISIMAS,ISIMASIEN,ISIPROV,ISIREG,ISIREGI,ISIFDEXC
 N ISIMISC K ISIMISC
 N:'$G(ISIPARAM("DEBUG")) ISIPARAM
 K ISIRESUL S (ISIRESUL(0),ISIRC)=0
 D:$G(ISIPARAM("DEBUG"))>0
 . ;Write out input parameters
 . W !,"+++Raw input params+++",!
 . I $D(MISC) S X="" F  S X=$O(MISC(X)) Q:X=""  W !,$G(MISC(X))
 . W !,"<HIT RETURN TO PROCEED>" R X:5
 . Q
 ;
 S ISIRC=$$ADMMISC^ISIIMPUF(.MISC,.ISIMISC)
 I (+ISIRC<0) S ISIRESUL(0)="-1^ERROR IN ADMMISC~ISIIMPUF:"_$TR(ISIRC,"^","-") Q
 ;
 I $G(ISIPARAM("DEBUG"))>0 D
 . W !,"+++Read in values+++",!
 . I $D(PARAM) S X="" F  S X=$O(PARAM(X)) Q:X=""  W !,$G(PARAM(X))
 . W !,"<HIT RETURN TO PROCEED>" R X:5
 . Q
 K MISC
 ;
 S ISIRC=$$VALADMIT^ISIIMPUF(.ISIMISC)
 I (+ISIRC<0) S ISIRESUL(0)="-1^ERROR IN VALADMIT~ISIIMPUF:"_$TR($G(ISIRC),"^","-") Q
 ;
 S ISIRC=$$ADMIT^ISIIMP25(.ISIMISC)
 I (+ISIRC<0) S ISIRESUL(0)="-1^ERROR IN ADMIT~ISIIMP25:"_$TR($G(ISIRC),"^","-") Q
 ;
 ; Discharge if DDATE provided
 I $G(ISIMISC("DDATE"))'="" D
 . I '$$VALDSCHG^ISIIMPUF(.ISIMISC) Q
 . S ISIRC=$$DISCHARG^ISIIMP26(.ISIMISC)
 . I (+ISIRC<0) S ISIRESUL(0)="-1^DERROR IN DISCH^DGPMAPI3:"_$TR($G(ISIRC),"^","-") Q
 . S ISIRESUL(0)=1,ISIRC=1
 . Q
 ;
 I (+ISIRC<0) Q
 S ISIRESUL(0)=1,ISIRC=1
 Q
 ;
HFACTOR(ISIRESUL,MISC)
 N $ETRAP,$ESTACK S $ETRAP="D ERR^ISIIMPER"
 N:'$G(ISIPARAM("DEBUG")) ISIPARAM
 K ISIRESUL S (ISIRESUL(0),ISIRC)=0
 D:$G(ISIPARAM("DEBUG"))>0
 . ;Write out input parameters
 . W !,"+++Raw input params+++",!
 . I $D(MISC) S X="" F  S X=$O(MISC(X)) Q:X=""  W !,$G(MISC(X))
 . W !,"<HIT RETURN TO PROCEED>" R X:5
 . Q
 D
 . S ISIRC=$$ENMISC^ISIIMPUG(.MISC,.ISIMISC)  Q:ISIRC<0
 . I $G(ISIPARAM("DEBUG"))>0 D
 . . W !,"+++Read in values+++",!
 . . I $D(ISIMISC) S X="" F  S X=$O(ISIMISC(X)) Q:X=""  W !,$G(ISIMISC(X))
 . . W !,"<HIT RETURN TO PROCEED>" R X:5
 . . Q
 . K MISC
 . S ISIRC=$$VHF^ISIIMP27(.ISIMISC)
 . Q
 I (+ISIRC<0) S ISIRESUL(0)=ISIRC Q ;W !,"ERROR"
 S ISIRESUL(0)="1"
 Q
 ;
VEXAM(ISIRESUL,MISC)
 N $ETRAP,$ESTACK S $ETRAP="D ERR^ISIIMPER"
 N:'$G(ISIPARAM("DEBUG")) ISIPARAM
 K ISIRESUL S (ISIRESUL(0),ISIRC)=0
 D:$G(ISIPARAM("DEBUG"))>0
 . ;Write out input parameters
 . W !,"+++Raw input params+++",!
 . I $D(MISC) S X="" F  S X=$O(MISC(X)) Q:X=""  W !,$G(MISC(X))
 . W !,"<HIT RETURN TO PROCEED>" R X:5
 . Q
 D
 . S ISIRC=$$ENMISC^ISIIMPUG(.MISC,.ISIMISC)  Q:ISIRC<0
 . I $G(ISIPARAM("DEBUG"))>0 D
 . . W !,"+++Read in values+++",!
 . . I $D(ISIMISC) S X="" F  S X=$O(ISIMISC(X)) Q:X=""  W !,$G(ISIMISC(X))
 . . W !,"<HIT RETURN TO PROCEED>" R X:5
 . . Q
 . K MISC
 . S ISIRC=$$VEXAM^ISIIMP27(.ISIMISC)
 . Q
 I (+ISIRC<0) S ISIRESUL(0)=ISIRC Q ;W !,"ERROR"
 S ISIRESUL(0)="1"
 Q
 ;
 ;
VCPT(ISIRESUL,MISC)
 N $ETRAP,$ESTACK S $ETRAP="D ERR^ISIIMPER"
 N:'$G(ISIPARAM("DEBUG")) ISIPARAM
 K ISIRESUL S (ISIRESUL(0),ISIRC)=0
 D:$G(ISIPARAM("DEBUG"))>0
 . ;Write out input parameters
 . W !,"+++Raw input params+++",!
 . I $D(MISC) S X="" F  S X=$O(MISC(X)) Q:X=""  W !,$G(MISC(X))
 . W !,"<HIT RETURN TO PROCEED>" R X:5
 . Q
 D
 . S ISIRC=$$ENMISC^ISIIMPUG(.MISC,.ISIMISC)  Q:ISIRC<0
 . I $G(ISIPARAM("DEBUG"))>0 D
 . . W !,"+++Read in values+++",!
 . . I $D(ISIMISC) S X="" F  S X=$O(ISIMISC(X)) Q:X=""  W !,$G(ISIMISC(X))
 . . W !,"<HIT RETURN TO PROCEED>" R X:5
 . . Q
 . K MISC
 . S ISIRC=$$VCPT^ISIIMP27(.ISIMISC)
 . Q
 I (+ISIRC<0) S ISIRESUL(0)=ISIRC Q ;W !,"ERROR"
 S ISIRESUL(0)="1"
 Q
 ;
VIMMZ(ISIRESUL,MISC)
 N $ETRAP,$ESTACK S $ETRAP="D ERR^ISIIMPER"
 N:'$G(ISIPARAM("DEBUG")) ISIPARAM
 K ISIRESUL S (ISIRESUL(0),ISIRC)=0
 D:$G(ISIPARAM("DEBUG"))>0
 . ;Write out input parameters
 . W !,"+++Raw input params+++",!
 . I $D(MISC) S X="" F  S X=$O(MISC(X)) Q:X=""  W !,$G(MISC(X))
 . W !,"<HIT RETURN TO PROCEED>" R X:5
 . Q
 D
 . S ISIRC=$$ENMISC^ISIIMPUG(.MISC,.ISIMISC)  Q:ISIRC<0
 . I $G(ISIPARAM("DEBUG"))>0 D
 . . W !,"+++Read in values+++",!
 . . I $D(ISIMISC) S X="" F  S X=$O(ISIMISC(X)) Q:X=""  W !,$G(ISIMISC(X))
 . . W !,"<HIT RETURN TO PROCEED>" R X:5
 . . Q
 . K MISC
 . S ISIRC=$$VIMMZ^ISIIMP27(.ISIMISC)
 . Q
 I (+ISIRC<0) S ISIRESUL(0)=ISIRC Q ;W !,"ERROR"
 S ISIRESUL(0)="1"
 Q
 ;
VPOV(ISIRESUL,MISC)
 N $ETRAP,$ESTACK S $ETRAP="D ERR^ISIIMPER"
 N:'$G(ISIPARAM("DEBUG")) ISIPARAM
 K ISIRESUL S (ISIRESUL(0),ISIRC)=0
 D:$G(ISIPARAM("DEBUG"))>0
 . ;Write out input parameters
 . W !,"+++Raw input params+++",!
 . I $D(MISC) S X="" F  S X=$O(MISC(X)) Q:X=""  W !,$G(MISC(X))
 . W !,"<HIT RETURN TO PROCEED>" R X:5
 . Q
 D
 . S ISIRC=$$ENMISC^ISIIMPUG(.MISC,.ISIMISC)  Q:ISIRC<0
 . I $G(ISIPARAM("DEBUG"))>0 D
 . . W !,"+++Read in values+++",!
 . . I $D(ISIMISC) S X="" F  S X=$O(ISIMISC(X)) Q:X=""  W !,$G(ISIMISC(X))
 . . W !,"<HIT RETURN TO PROCEED>" R X:5
 . . Q
 . K MISC
 . S ISIRC=$$VPOV^ISIIMP27(.ISIMISC)
 . Q
 I (+ISIRC<0) S ISIRESUL(0)=ISIRC Q ;W !,"ERROR"
 S ISIRESUL(0)="1"
 Q
 ;
VPTEDU(ISIRESUL,MISC)
 N $ETRAP,$ESTACK S $ETRAP="D ERR^ISIIMPER"
 N:'$G(ISIPARAM("DEBUG")) ISIPARAM
 K ISIRESUL S (ISIRESUL(0),ISIRC)=0
 D:$G(ISIPARAM("DEBUG"))>0
 . ;Write out input parameters
 . W !,"+++Raw input params+++",!
 . I $D(MISC) S X="" F  S X=$O(MISC(X)) Q:X=""  W !,$G(MISC(X))
 . W !,"<HIT RETURN TO PROCEED>" R X:5
 . Q
 D
 . S ISIRC=$$ENMISC^ISIIMPUG(.MISC,.ISIMISC)  Q:ISIRC<0
 . I $G(ISIPARAM("DEBUG"))>0 D
 . . W !,"+++Read in values+++",!
 . . I $D(ISIMISC) S X="" F  S X=$O(ISIMISC(X)) Q:X=""  W !,$G(ISIMISC(X))
 . . W !,"<HIT RETURN TO PROCEED>" R X:5
 . . Q
 . K MISC
 . S ISIRC=$$VPNTED^ISIIMP27(.ISIMISC)
 . Q
 I (+ISIRC<0) S ISIRESUL(0)=ISIRC Q ;W !,"ERROR"
 S ISIRESUL(0)="1"
 Q
 ;
