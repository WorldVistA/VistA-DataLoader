ISIIMPR2 ;ISI GROUP/MLS -- DATA LOADER RPC (2)
 ;;1.0;;;Jun 26,2012
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
ALGMAKE(ISIRESUL,MISC)
 N $ETRAP,$ESTACK S $ETRAP="D ERR^ISIIMPER"
 N:'$G(ISIPARAM("DEBUG")) ISIPARAM
 K ISIRESUL S (ISIRESUL(0),ISIRC)=0
 ;
 D:$G(ISIPARAM("DEBUG"))>0
 . ;Write out input parameters
 . W !,"+++Raw input parameters+++",!
 . I $D(MISC) S X="" F  S X=$O(MISC(X)) Q:X=""  W !,$G(MISC(X))
 . W !,"<HIT RETURN TO PROCEED>" R X:5
 . Q
 ;
 D
 . S ISIRC=$$ALGMISC^ISIIMPU6(.MISC,.ISIMISC) Q:ISIRC<0
 . I $G(ISIPARAM("DEBUG"))>0 D
 . . W !,"++Read in values+++",!
 . . I $D(ISIMISC) S X="" F  S X=$O(ISIMISC(X)) Q:X=""  W !,$G(ISIMISC(X))
 . . Q
 . K MISC
 . S ISIRC=$$ALLERGY^ISIIMP10(.ISIRESUL,.ISIMISC)
 . Q
 ;
 I +ISIRC<0 S ISIRESUL(0)=ISIRC ;W !,"ERROR" Q
 Q
 ;
LABMAKE(ISIRESUL,MISC)
 N $ETRAP,$ESTACK S $ETRAP="D ERR^ISIIMPER"
 N:'$G(ISIPARAM("DEBUG")) ISIPARAM
 K ISIRESUL S (ISIRESUL(0),ISIRC)=0
 D:$G(ISIPARAM("DEBUG"))>0
 . ;Write out input parameters
 . W !,"+++Raw input parameters+++",!
 . I $D(MISC) S X="" F  S X=$O(MISC(X)) Q:X=""  W !,$G(MISC(X))
 . W !,"<HIT RETURN TO PROCEED>" R X:5
 . Q
 ;
 D
 . S ISIRC=$$LABMISC^ISIIMPU7(.MISC,.ISIMISC) Q:ISIRC<0
 . I $G(ISIPARAM("DEBUG"))>0 D
 . . W !,"++Read in values+++",!
 . . I $D(ISIMISC) S X="" F  S X=$O(ISIMISC(X)) Q:X=""  W !,$G(ISIMISC(X))
 . . Q
 . K MISC
 . S ISIRC=$$LAB^ISIIMP12(.ISIRESUL,.ISIMISC)
 . Q
 ;
 I +ISIRC<0 S ISIRESUL(0)=ISIRC ; W !,"ERROR" Q
 Q
 ;
NOTEMAKE(ISIRESUL,MISC)
 N $ETRAP,$ESTACK S $ETRAP="D ERR^ISIIMPER"
 N:'$G(ISIPARAM("DEBUG")) ISIPARAM
 K ISIRESUL S (ISIRESUL(0),ISIRC)=0
 ;
 D:$G(ISIPARAM("DEBUG"))>0
 . ;Write out input parameters
 . W !,"+++Raw input parameters+++",!
 . I $D(MISC) S X="" F  S X=$O(MISC(X)) Q:X=""  W !,$G(MISC(X))
 . W !,"<HIT RETURN TO PROCEED>" R X:5
 . Q
 ;
 D
 . S ISIRC=$$NOTMISC^ISIIMPU8(.MISC,.ISIMISC) Q:ISIRC<0
 . I $G(ISIPARAM("DEBUG"))>0 D
 . . W !,"++Read in values+++",!
 . . I $D(ISIMISC) S X="" F  S X=$O(ISIMISC(X)) Q:X=""  W !,$G(ISIMISC(X))
 . . Q
 . K MISC
 . S ISIRC=$$NOTES^ISIIMP14(.ISIRESUL,.ISIMISC)
 . Q
 ;
 I +ISIRC<0 S ISIRESUL(0)=ISIRC ;W !,"ERROR" Q
 Q
 ;
MEDMAKE(ISIRESUL,MISC)
 N $ETRAP,$ESTACK S $ETRAP="D ERR^ISIIMPER"
 N:'$G(ISIPARAM("DEBUG")) ISIPARAM
 K ISIRESUL S (ISIRESUL(0),ISIRC)=0
 ;
 D:$G(ISIPARAM("DEBUG"))>0
 . ;Write out input parameters
 . W !,"+++Raw input parameters+++",!
 . I $D(MISC) S X="" F  S X=$O(MISC(X)) Q:X=""  W !,$G(MISC(X))
 . W !,"<HIT RETURN TO PROCEED>" R X:5
 . Q
 ;
 D
 . S ISIRC=$$MEDMISC^ISIIMPU9(.MISC,.ISIMISC) Q:ISIRC<0
 . I $G(ISIPARAM("DEBUG"))>0 D
 . . W !,"++Read in values+++",!
 . . I $D(ISIMISC) S X="" F  S X=$O(ISIMISC(X)) Q:X=""  W !,$G(ISIMISC(X))
 . . Q
 . K MISC
 . S ISIRC=$$MEDS^ISIIMP16(.ISIRESUL,.ISIMISC)
 . Q
 ;
 I +ISIRC<0 S ISIRESUL(0)=ISIRC ;W !,"ERROR" Q
 Q
 ;
TABLEGET(ISIRESUL,TABLE)
 ;
 N $ETRAP,$ESTACK S $ETRAP="D ERR^ISIIMPER"
 K ARRAY S ISIRESUL(0)=0
 ;
 I $G(TABLE)="" S ISIRESUL(0)="-1^Incorrect parameter passed" Q
 S TABLE=$$PARAM^ISIIMPUA(TABLE)
 I TABLE=-1 S ISIRESUL(0)="-1^Incorrect parameter passed" Q
 ;
 D ENTRY^ISIIMPUA(.ISIRESUL,.TABLE)
 Q
 ;
CONMAKE(ISIRESUL,MISC)
 N $ETRAP,$ESTACK S $ETRAP="D ERR^ISIIMPER"
 N:'$G(ISIPARAM("DEBUG")) ISIPARAM
 K ISIRESUL S (ISIRESUL(0),ISIRC)=0
 ;
 D:$G(ISIPARAM("DEBUG"))>0
 . ;Write out input parameters
 . W !,"+++Raw input parameters+++",!
 . I $D(MISC) S X="" F  S X=$O(MISC(X)) Q:X=""  W !,$G(MISC(X))
 . W !,"<HIT RETURN TO PROCEED>" R X:5
 . Q
 ;
 D
 . S ISIRC=$$CONMISC^ISIIMPUB(.MISC,.ISIMISC) Q:ISIRC<0
 . I $G(ISIPARAM("DEBUG"))>0 D
 . . W !,"++Read in values+++",!
 . . I $D(ISIMISC) S X="" F  S X=$O(ISIMISC(X)) Q:X=""  W !,$G(ISIMISC(X))
 . . Q
 . K MISC
 . S ISIRC=$$CONSULTS^ISIIMP18(.ISIRESUL,.ISIMISC)
 . Q
 ;
 I +ISIRC<0 S ISIRESUL(0)=ISIRC ;W !,"ERROR" Q
 Q
 ;
ICD9GET(ISIRESUL,TXT)
 ;
 N $ETRAP,$ESTACK S $ETRAP="D ERR^ISIIMPER"
 K ARRAY S ISIRESUL(0)=0
 I $G(TXT)="" S ISIRESUL(0)="-1^Incorrect parameter passed" Q
 S TXT=$$UP^XLFSTR(TXT)
 D ICD9^ISIIMPUA(.ISIRESUL,.TXT)
 Q
 ;
TMPSAVE(ISIRESUL,MISC)
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
 . S ISIRC=$$TMPMISC^ISIIMPUE(.MISC,.ISIMISC) Q:ISIRC<0
 . K MISC
 . S ISIRC=$$TMPSAVE^ISIIMP24(.ISIRESUL,.ISIMISC)
 . Q
 ;
 I +ISIRC<0 S ISIRESUL(0)=ISIRC ;W !,"ERROR"
 Q
 ;
NVAMED(ISIRESUL,MISC) ; Non-VA Meds Make
 N $ETRAP,$ESTACK S $ETRAP="D ERR^ISIIMPER"
 N:'$G(ISIPARAM("DEBUG")) ISIPARAM
 K ISIRESUL S (ISIRESUL(0),ISIRC)=0
 ;
 D SETDBG^ISIIMPR1("NVAMED",.MISC)
 ;
 D:$G(ISIPARAM("DEBUG"))>0
 . ;Write out input parameters
 . W !,"+++Raw input parameters+++",!
 . I $D(MISC) S X="" F  S X=$O(MISC(X)) Q:X=""  W !,$G(MISC(X))
 . W !,"<HIT RETURN TO PROCEED>" R X:5
 . Q
 ;
 D
 . S ISIRC=$$MEDMISC^ISIIMPUI(.MISC,.ISIMISC) Q:ISIRC<0
 . I $G(ISIPARAM("DEBUG"))>0 D
 . . W !,"++Read in values+++",!
 . . I $D(ISIMISC) S X="" F  S X=$O(ISIMISC(X)) Q:X=""  W !,$G(ISIMISC(X))
 . . Q
 . K MISC
 . S ISIRC=$$NVAMEDS^ISIIMP16(.ISIRESUL,.ISIMISC)
 . Q
 ;
 I +ISIRC<0 S ISIRESUL(0)=ISIRC ;W !,"ERROR" Q
 D SETDBG^ISIIMPR1("NVAMED_RETURN",.ISIRESUL)
 Q
 ;
