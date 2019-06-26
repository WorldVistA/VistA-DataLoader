ISIIMPR1 ;ISI GROUP/MLS -- Import RPC ; 17 Sep 2018 4:00 PM
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
PNTIMPRT(ISIRESUL,MISC)
 N $ETRAP,$ESTACK S $ETRAP="D ERR^ISIIMPER"
 N:'$G(ISIPARAM("DEBUG")) ISIPARAM
 K ISIRESUL S (ISIRESUL(0),ISIRC)=0
 ;
 D:$G(ISIPARAM("DEBUG"))>0
 . ;Write out input parameters
 . W !,"+++Raw input params+++",!
 . I $D(MISC) S X="" F  S X=$O(MISC(X)) Q:X=""  W !,$G(MISC(X))
 . W !,"<HIT RETURN TO PROCEED>" R X:5
 . Q
 ;
 D
 . S ISIRC=$$PNTMISC^ISIIMPU1(.MISC,.ISIMISC) Q:ISIRC<0
 . K MISC
 . S ISIRC=$$PATIENT^ISIIMP02(.ISIRESUL,.ISIMISC)
 . Q
 ;
 I +ISIRC<0 S ISIRESUL(0)=ISIRC
 Q
 ;
APPMAKE(ISIRESUL,MISC)
 N $ETRAP,$ESTACK S $ETRAP="D ERR^ISIIMPER"
 N:'$G(ISIPARAM("DEBUG")) ISIPARAM
 K ISIRESUL S (ISIRESUL(0),ISIRC)=0
 ;
 D:$G(ISIPARAM("DEBUG"))>0
 . ;Write out input parameters
 . W !,"+++Raw input params+++",!
 . I $D(MISC) S X="" F  S X=$O(MISC(X)) Q:X=""  W !,$G(MISC(X))
 . W !,"<HIT RETURN TO PROCEED>" R X:5
 . Q
 ;
 D
 . S ISIRC=$$APPTMISC^ISIIMPU2(.MISC,.ISIMISC)  Q:ISIRC<0
 . I $G(ISIPARAM("DEBUG"))>0 D
 . . W !,"+++Read in values+++",!
 . . I $D(ISIMISC) S X="" F  S X=$O(ISIMISC(X)) Q:X=""  W !,$G(ISIMISC(X))
 . . W !,"<HIT RETURN TO PROCEED>" R X:5
 . . Q
 . K MISC
 . S ISIRC=$$APPOINT^ISIIMP04()
 . Q
 ;
 I +ISIRC<0 S ISIRESUL(0)=ISIRC ;W !,"ERROR"
 Q
 ;
PROBMAKE(ISIRESUL,MISC)
 N $ETRAP,$ESTACK S $ETRAP="D ERR^ISIIMPER"
 N:'$G(ISIPARAM("DEBUG")) ISIPARAM
 K ISIRESUL S (ISIRESUL(0),ISIRC)=0
 ;
 D:$G(ISIPARAM("DEBUG"))>0
 . ;Write out input parameters
 . W !,"+++Raw input params (PR1)+++",!
 . I $D(MISC) S X="" F  S X=$O(MISC(X)) Q:X=""  W !,$G(MISC(X))
 . W !,"<HIT RETURN TO PROCEED>" R X:5
 . Q
 ;
 D
 . S ISIRC=$$PROBMISC^ISIIMPU4(.MISC,.ISIMISC)  Q:+ISIRC<0
 . K MISC
 . S ISIRC=$$PROBLEM^ISIIMP06(.ISIRESUL,.ISIMISC)
 . Q
 ;
 I +ISIRC<0 S ISIRESUL(0)=ISIRC ; W !,"ERROR"
 Q
 ;
VITMAKE(ISIRESUL,MISC)
 N $ETRAP,$ESTACK S $ETRAP="D ERR^ISIIMPER"
 N:'$G(ISIPARAM("DEBUG")) ISIPARAM
 K ISIRESUL S (ISIRESUL(0),ISIRC)=0
 ;
 D:$G(ISIPARAM("DEBUG"))>0
 . ;Write out input parameters
 . W !,"+++Raw input params+++",!
 . I $D(MISC) S X="" F  S X=$O(MISC(X)) Q:X=""  W !,$G(MISC(X))
 . W !,"<HIT RETURN TO PROCEED>" R X:5
 . Q
 ;
 D
 . S ISIRC=$$VITMISC^ISIIMPU5(.MISC,.ISIMISC)  Q:ISIRC<0
 . I $G(ISIPARAM("DEBUG"))>0 D
 . . W !,"+++Read in values+++",!
 . . I $D(ISIMISC) S X="" F  S X=$O(ISIMISC(X)) Q:X=""  W !,$G(ISIMISC(X))
 . . W !,"<HIT RETURN TO PROCEED>" R X:5
 . . Q
 . K MISC
 . S ISIRC=$$VITALS^ISIIMP08(.ISIRESUL,.ISIMISC)
 . Q
 ;
 I +ISIRC<0 D  Q
 . S ISIRESUL(0)=ISIRC
 Q
 ;
RADOMAKE(ISIRESUL,MISC)
 N $ETRAP,$ESTACK S $ETRAP="D ERR^ISIIMPER"
 N:'$G(ISIPARAM("DEBUG")) ISIPARAM
 K ISIRESUL S (ISIRESUL(0),ISIRC)=0
 ;
 D:$G(ISIPARAM("DEBUG"))>0
 . ;Write out input parameters
 . W !,"+++Raw input params+++",!
 . I $D(MISC) S X="" F  S X=$O(MISC(X)) Q:X=""  W !,$G(MISC(X))
 . W !,"<HIT RETURN TO PROCEED>" R X:5
 . Q
 ;
 D
 . S ISIRC=$$RADMISC^ISIIMPUC(.MISC,.ISIMISC)  Q:ISIRC<0
 . I $G(ISIPARAM("DEBUG"))>0 D
 . . W !,"+++Read in values+++",!
 . . I $D(ISIMISC) S X="" F  S X=$O(ISIMISC(X)) Q:X=""  W !,$G(ISIMISC(X))
 . . W !,"<HIT RETURN TO PROCEED>" R X:5
 . . Q
 . K MISC
 . S ISIRC=$$RADORDER^ISIIMP20(.ISIRESUL,.ISIMISC)
 . Q
 ;
 I (+ISIRC<0) S ISIRESUL(0)=ISIRC ;W !,"ERROR"
 Q
 ;
USRCREAT(ISIRESUL,MISC)
 N $ETRAP,$ESTACK S $ETRAP="D ERR^ISIIMPER"
 N:'$G(ISIPARAM("DEBUG")) ISIPARAM
 K ISIRESUL S (ISIRESUL(0),ISIRC)=0
 ;
 D:$G(ISIPARAM("DEBUG"))>0
 . ;Write out input parameters
 . W !,"+++Raw input params+++",!
 . I $D(MISC) S X="" F  S X=$O(MISC(X)) Q:X=""  W !,$G(MISC(X))
 . W !,"<HIT RETURN TO PROCEED>" R X:5
 . Q
 ;
 D
 . S ISIRC=$$USRMISC^ISIIMPUD(.MISC,.ISIMISC) Q:ISIRC<0
 . K MISC
 . S ISIRC=$$USER^ISIIMP22(.ISIRESUL,.ISIMISC)
 . Q
 ;
 I +ISIRC<0 S ISIRESUL(0)=ISIRC ;W !,"ERROR"
 Q
 ;
TMPUPDTE(ISIRESUL,MISC)
 N $ETRAP,$ESTACK S $ETRAP="D ERR^ISIIMPER"
 N:'$G(ISIPARAM("DEBUG")) ISIPARAM
 K ISIRESUL S (ISIRESUL(0),ISIRC)=0
 ;
 D:$G(ISIPARAM("DEBUG"))>0
 . ;Write out input parameters
 . W !,"+++Raw MISC input params+++",!
 . I $D(MISC) S X="" F  S X=$O(MISC(X)) Q:X=""  W !,"MISC("_X_")="_$G(MISC(X))
 . W !,"<HIT RETURN TO PROCEED>" R X:5
 . Q
 ;
 D
 . S ISIRC=$$TMPMISC^ISIIMPUE(.MISC,.ISIMISC)  Q:ISIRC<0
 . K MISC
 . I $G(ISIPARAM("DEBUG"))>0 D
 . . W !,"+++Read ISIMISC in values+++",!
 . . I $D(ISIMISC) S X="" F  S X=$O(ISIMISC(X)) Q:X=""  W !,"ISIMISC("_X_")="_$G(ISIMISC(X))
 . . W !,"<HIT RETURN TO PROCEED>" R X:5
 . . Q
 . S ISIRC=$$TEMPLATE^ISIIMP24(.ISIRESUL,.ISIMISC)
 . Q
 ;
 I +ISIRC<0 S ISIRESUL(0)=ISIRC ;W !,"ERROR"
 Q
 ;
TRTFACLS(ISIRESUL,MISC) ; Import Treating Facility List
 N $ETRAP,$ESTACK S $ETRAP="D ERR^ISIIMPER"
 N:'$G(ISIPARAM("DEBUG")) ISIPARAM
 K ISIRESUL S (ISIRESUL(0),ISIRC)=0
 ;
 D SETDBG("TREATFAC",.MISC)
 ;
 D:$G(ISIPARAM("DEBUG"))>0
 . ;Write out input parameters
 . W !,"+++Raw input params+++",!
 . I $D(MISC) S X="" F  S X=$O(MISC(X)) Q:X=""  W !,$G(MISC(X))
 . W !,"<HIT RETURN TO PROCEED>" R X:5
 . Q
 ;
 D
 . S ISIRC=$$TFLMISC^ISIIMPUH(.MISC,.ISIMISC) Q:ISIRC<0
 . N MISC
 . S ISIRC=$$TFL^ISIIMP28(.ISIRESUL,.ISIMISC)
 . Q
 ;
 I +ISIRC<0 S ISIRESUL(0)=ISIRC ;W !,"ERROR"
 D SETDBG("TREATFAC_RETURN",.ISIRESUL)
 Q
 ;
SETDBG(REF,MISC) ; Set a debugging global
 K ^TMP($J,"ISI",REF)
 M ^TMP($J,"ISI",REF)=MISC
 Q
