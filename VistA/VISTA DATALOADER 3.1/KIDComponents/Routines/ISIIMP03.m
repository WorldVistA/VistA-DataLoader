ISIIMP03 ;ISI GROUP/MLS -- PATIENT IMPORT CONT.
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
 ;
 D:$G(ISIPARAM("DEBUG"))>0
 . W !,"+++Template merged params+++",!
 . I $D(ISIMISC) S X="" F  S X=$O(ISIMISC(X)) Q:X=""  W !,X," ",$G(ISIMISC(X))
 . W !,"<HIT RETURN TO PROCEED>" R X:5
 . Q
 ;
 ; Validate import array contents
 S ISIRC=$$VALIDATE^ISIIMPU1(.ISIMISC)
 Q ISIRC
 ;
CREATEPTS() ;
 ; Create patient(s)
 S ISIRC=$$IMPORTPT(.ISIMISC)
 Q ISIRC
 ;
IMPORTPT(ISIMISC)
 ; Input - ISIMISC(ARRAY)
 ; Format:  ISIMISC(PARAM)=VALUE
 ;     eg:  ISIMISC("NAME")="FIRST,LAST"
 ;
 ; Output - ISIRC [return code]
 ;          ISIRESUL(0) = CNT
 ;          ISIRESUL(1) = DFN^SSN^NAME
 ;
 I ISIMISC("IMP_TYPE")="B" D BATCH
 I ISIMISC("IMP_TYPE")="I" D INDIVIDUAL
 Q ISIRC
 ;
INDIVIDUAL
 N SSN,SSNMASK,RETURN,STRTSSN,ENDSSN,ISINUM,ISIINCR
 N NAME,SEX,DOB,STRT1,STRT2,CITY,STATE,ZIP,MARSTAT,PHON,VETERAN
 N RACE,ETHN,INSUR,OCCUP,EMPLOY,MERGE
 ;
 D:$G(ISIPARAM("DEBUG"))>0
 . W !,"+++Starting Individual PT Create+++",!
 . I $D(ISIMISC) S X="" F  S X=$O(ISIMISC(X)) Q:X=""  W !,X," ",$G(ISIMISC(X))
 . W !,"<HIT RETURN TO PROCEED>" R X:5
 . Q
 ;
 S ISIRC=0,ISIINCR=1
 S SSN=$G(ISIMISC("SSN"))
 I SSN'="" D  Q
 . I $D(^DPT("SSN",SSN)) S ISIRC="-1^Duplicate SSN" Q
 . S STRTSSN="9"_SSN
 . D PREPVAL I +ISIRC<0 Q
 . D CREATEPNT
 . Q
 I SSN="" D
 . S SSNMASK=$G(ISIMISC("SSN_MASK"))
 . I SSNMASK="" S SSNMASK="000"
 . S RETURN=$$EVALSSNMASK(SSNMASK)
 . I (+RETURN)<1 S SSNMASK="666" S RETURN=$$EVALSSNMASK(SSNMASK)
 . I (+RETURN)<1 S ISIRC="-1^Error: unable to create SSN (ISIIMP03)" Q  ; We've run out of non-standard SSN's! Time to refresh your database.
 . S STRTSSN="9"_$P(RETURN,"|",2),ENDSSN="9"_$P(RETURN,"|",3)
 . F  Q:'$D(^DPT("SSN",$E(STRTSSN,2,10)))  S STRTSSN=STRTSSN+1
 . I STRTSSN>ENDSSN S ISIRC="-1^Problem generating SSN" Q
 . S SSN=$E(STRTSSN,2,10)
 . D PREPVAL I +ISIRC<0 Q ;in case of error
 . D CREATEPNT
 . Q
 Q
 ;
BATCH ;
 N ISIINCR,I,ISINUM,RETURN,SSNMASK,SSN,STRTSSN,ENDSSN,EXIT
 N NAME,SEX,DOB,STRT1,STRT2,CITY,STATE,ZIP,MARSTAT,PHON,VETERAN
 N RACE,ETHN,INSUR,OCCUP,EMPLOY,MERGE
 ;
 D:$G(ISIPARAM("DEBUG"))>0
 . W !,"+++Starting Batch PT Creation+++",!
 . I $D(ISIMISC) W $G(ISIMISC) S X="" F  S X=$O(ISIMISC(X)) Q:X=""  W !,ISIMISC(X)
 . W !,"<HIT RETURN TO PROCEED>" R X:5
 . Q
 ;
 S EXIT=0,ISIRC=0
 S ISINUM=$G(ISIMISC("IMP_BATCH_NUM"))
 S SSNMASK=$G(ISIMISC("SSN_MASK"))
 S RETURN=$$EVALSSNMASK(SSNMASK)
 I (+RETURN)<ISINUM S ISIRC="-1^No. of patients requested exceeds SSN MASK" Q
 S STRTSSN="9"_$P(RETURN,"|",2),ENDSSN="9"_$P(RETURN,"|",3)
 F ISIINCR=1:1:ISINUM D  Q:EXIT=1
 . F  Q:'$D(^DPT("SSN",$E(STRTSSN,2,10)))  S STRTSSN=STRTSSN+1
 . I STRTSSN>ENDSSN S EXIT=1,ISIRC="-1^Problem generating SSNs" Q
 . S SSN=$E(STRTSSN,2,10)
 . D PREPVAL I +ISIRC<0 S EXIT=1 Q
 . D CREATEPNT
 . I +ISIRC<0 S EXIT=1 Q
 . Q
 Q
PREPVAL ;Prep import values
 N LDOB,UDOB
 S (NAME,SEX,DOB,STRT1,STRT2,CITY,STATE,ZIP,MARSTAT,PHON,VETERAN,TYPE,RACE,ETHN,INSUR,OCCUP,EMPLOY)=""
 S NAME=$G(ISIMISC("NAME")) I NAME="" S NAME=$$MASK("NAME",$G(ISIMISC("NAME_MASK")),ISIINCR)
 S SEX=$G(ISIMISC("SEX"))
 I SEX="" S SEX=$$SEX
 S DOB=$G(ISIMISC("DOB")) I DOB="" S LDOB=$G(ISIMISC("LOW_DOB")),UDOB=$G(ISIMISC("UP_DOB")) S DOB=$$DOB
 S STRT1=$G(ISIMISC("STREET_ADD1"))
 I STRT1="" S STRT1=$$STREET
 S STRT2=$G(ISIMISC("STREET_ADD2"))
 S CITY=$G(ISIMISC("CITY")) I CITY="" S CITY=$$CITY
 S STATE=$G(ISIMISC("STATE"))
 I '+STATE,STATE'="" D  ;DHP/ART fix to use state codes as input
 . S STATE=$O(^DIC(5,"C",$$UP^XLFSTR(STATE),""))
 . Q:STATE'=""
 . S STATE=$G(ISIMISC("STATE"))
 . S STATE=$O(^DIC(5,"B",$$UP^XLFSTR(STATE),""))
 I STATE="" S STATE=$$STATE
 S ZIP=$G(ISIMISC("ZIP")) I ZIP="" S ZIP=$$MASK("ZIP",$G(ISIMISC("ZIP_4_MASK")))
 S MARSTAT=$G(ISIMISC("MARITAL_STATUS")) I MARSTAT="" S MARSTAT=$$MARSTAT
 S PHON=$G(ISIMISC("PH_NUM")) I PHON="" S PHON=$$MASK("PHONE",$G(ISIMISC("PH_NUM_MASK")))
 S VETERAN=$G(ISIMISC("VETERAN")) I VETERAN="" S VETERAN="N"
 S TYPE=$G(ISIMISC("TYPE")) I 'TYPE S TYPE=$O(^DG(391,"B","NON-VETERAN (OTHER)",""))
 S RACE=$G(ISIMISC("RACE"))
 S ETHN=$G(ISIMISC("ETHNICITY"))
 S INSUR=$G(ISIMISC("INSUR_TYPE"))
 S OCCUP=$G(ISIMISC("OCCUPATION"))
 S EMPLOY=$G(ISIMISC("EMPLOY_STAT"))
 S MERGE=$G(ISIMISC("MRG_SOURCE"))
 Q
CREATEPNT ;
 N FDA,MSG,ZIEN,DFN
 K FDA
 ; Check for dups
 S DFN=$O(^DPT("B",NAME,0))
 I DFN D
 . I $P(^DPT(DFN,0),U,2)'=SEX S DFN=0
 . I $P(^DPT(DFN,0),U,3)'=DOB S DFN=0
 . S ISIRESUL(ISIINCR)=DFN_"^"_$P(^DPT(DFN,0),U,9)_"^"_NAME
 . S ISIRESUL(0)=ISIINCR
 ;
 I 'DFN D
 . S FDA(2,"+1,",.01)=NAME
 . S FDA(2,"+1,",.02)=SEX
 . S FDA(2,"+1,",.03)=DOB
 . S FDA(2,"+1,",.05)=MARSTAT
 . I $G(OCCUP)'="" S FDA(2,"+1,",.07)=OCCUP
 . S FDA(2,"+1,",.09)=SSN
 . ;S FDA(2,"+1,",400000000)=SSN ;NATIONAL ID field used in EHR
 . S FDA(2,"+1,",.111)=STRT1
 . S FDA(2,"+1,",.112)=STRT2
 . S FDA(2,"+1,",.114)=CITY
 . S FDA(2,"+1,",.115)=STATE
 . S FDA(2,"+1,",.1112)=ZIP
 . S FDA(2,"+1,",.131)=PHON
 . S FDA(2,"+1,",391)=TYPE
 . S FDA(2,"+1,",1901)=VETERAN
 . S FDA(2,"+1,",.12105)="N"     ; TEMPORARY ADD ACTIVE
 . ;S FDA(2,"+1,",.14105)="N"     ; CONFIDENTIAL ADD ACTIVE
 . S FDA(2,"+1,",.2125)="N"      ; K-ADD SAME AS PNT'S
 . S FDA(2,"+1,",.21925)="N"     ; K2-ADD SAME AS PNT'S
 . S FDA(2,"+1,",.2515)="1"      ; SPOUSE EMPLOYMENT STATUS
 . S FDA(2,"+1,",.301)="N"       ; SERVICE CONNECTED
 . I $G(EMPLOY)'="" S FDA(2,"+1,",.31115)=EMPLOY
 . E  S FDA(2,"+1,",.31115)="1"     ; EMPLOYMENT STATUS
 . S FDA(2,"+1,",.3192)="Y"      ; COVERED BY HEALTH INSURANCE
 . S FDA(2,"+1,",.32101)="Y"     ; VIETNAM SERVICE INDICATED
 . S FDA(2,"+1,",.32102)="N"     ; AGENT ORANGE EXPOS. INDICATED
 . S FDA(2,"+1,",.32103)="N"     ; RADIATION EXPOSURE INDICATED
 . S FDA(2,"+1,",.32201)="N"     ; PERSIAN GULF SERVICE
 . S FDA(2,"+1,",.322013)="N"    ; ENVIRONMENTAL CONTAMINANTS
 . S FDA(2,"+1,",.322016)="N"    ; SOMALIA SERVICE INDICATED
 . S FDA(2,"+1,",.3221)="N"      ; LEBANON SERVICE INDICATED
 . S FDA(2,"+1,",.3224)="N"      ; GRENEDA SERVICE INDICATED
 . S FDA(2,"+1,",.3227)="N"      ; PANAMA SERVICE INDICATED
 . S FDA(2,"+1,",.3285)="N"      ; SERVICE SECOND EPISODE
 . S FDA(2,"+1,",.32945)="N"     ; SERVICE THIRD EPISODE
 . S FDA(2,"+1,",.3305)="Y"      ; E-EMER. CONTACT SAME AS NOK
 . S FDA(2,"+1,",.3405)="Y"      ; D-DESIGNEE SAME AS NOK
 . S FDA(2,"+1,",.362)="0"       ; DISABILITY RET. FROM MILITARY
 . S FDA(2,"+1,",.381)="0"       ; ELIGIBLE FOR MEDICAID
 . ;S FDA(2,"+1,",.382)="T-"_($R(100)+1) ; DATE MEDICAID LAST ASKED
 . S FDA(2,"+1,",.525)="N"       ; POW STATUS INDICATED
 . S FDA(2,"+1,",.5291)="N"      ; COMBAT SERVICE INDICATED
 . ;S FDA(2,"+1,",401.4)="T-"_($R(100)+1) ;DATE ENTERED ON SI LIST
 . S FDA(2,"+1,",1010.15)="Y"    ; RECIEVED VA CARE PREVIOUSLY
 . S FDA(2,"+1,",994)="N"        ; MULTIPLE BIRTH INDICATOR
 . ;S FDA(2.03,"+1,+1,",.01)=$P(DATA,"^",6)
 . I RACE'="" D
 . . S FDA(2.02,"+2,+1,",.01)=RACE
 . . S FDA(2.02,"+2,+1,",.02)="S"
 . I ETHN'="" D
 . . S FDA(2.06,"+3,+1,",.01)=ETHN
 . . S FDA(2.06,"+3,+1,",.02)="S"
 . I INSUR'="" D
 . . S FDA(2.312,"+4,+1,",.01)=INSUR
 . I $G(ISIPARAM("DEBUG"))>0 D
 . . W !,"+++ FDA Array before patient set +++"
 . . ;ZW FDA
 . . W !,"<HIT RETURN TO PROCEED>" R X:5
 . . Q
 . D UPDATE^DIE("","FDA","ZIEN","MSG")
 . I $D(MSG) S ISIRC="-1^"_$G(MSG("DIERR",1,"TEXT",1)) Q
 . S DFN=+$G(ZIEN(1)) D IHSPNT(DFN) ;create IHS Patient File entry
 . ;
 . I '$D(^DPT("SSN",$E(STRTSSN,2,10))) S ISIRC="-1^Problem generating pt." Q
 . I $G(ISIMISC("DFN_NAME"))="Y" I $G(ISIMISC("NAME_MASK"))'="" I $G(ISIMISC("NAME"))="" D
 . . S NAME=$$MASK("NAME",$G(ISIMISC("NAME_MASK")),$O(^DPT("SSN",$E(STRTSSN,2,10),"")))
 . . S ISIRC=$$CHNGNAME^ISIIMPU3($O(^DPT("SSN",$E(STRTSSN,2,10),"")),NAME)
 . . Q
 . I +ISIRC<0 Q
 . S ISIRESUL(ISIINCR)=DFN_"^"_$E(STRTSSN,2,10)_"^"_NAME
 . S ISIRESUL(0)=ISIINCR
 . ;I +$G(MERGE) D COPYPNT^ISIIMP23(MERGE,DFN) ;Copy Patient (not in use)
 . Q
 Q
 ;Q ISIRC
 ;
MASK(TYPE,VALUE,ISIINCR)
 N X,L,I,CNT,NUMCONV,RETURN
 S RETURN=""
 S TYPE=$G(TYPE),VALUE=$G(VALUE),ISIINCR=$G(ISIINCR)
 ;
 I TYPE="ZIP" D
 . I VALUE="" S VALUE="00000"
 . S I="" F X=$L(VALUE)+1:1:9 S I=I_"9"
 . S L=$L(I),I=$R(I)+1 F X=$L(I)+1:1:L S I="0"_I
 . S RETURN=VALUE_"-"_I
 . Q
 I TYPE="PHONE" D
 . I VALUE="" S VALUE="555555"
 . S I="" F X=$L(VALUE)+1:1:10 S I=I_"9"
 . S L=$L(I),I=$R(I)+1 F X=$L(I)+1:1:L S I="0"_I
 . S RETURN=VALUE_I
 . Q
 I TYPE="NAME" D
 . D NUMTBL
 . S I="" F X=1:1:$L(ISIINCR) S I=I_NUMCONV($E(ISIINCR,X))
 . S L=I
 . I VALUE="" S VALUE="*,PATIENT"
 . F  D  Q:VALUE'["*"
 . . F X=1:1:$L(VALUE) I $E(VALUE,X)="*" D  Q
 . . . S VALUE=$E(VALUE,0,(X-1))_L_$E(VALUE,(X+1),9999)
 . . . Q
 . . Q
 . S RETURN=VALUE
 . Q
 I TYPE="EMAIL" D
 . N ZNAME,ZEMAIL S ZNAME=$G(NAME) I ZNAME="" S ZNAME="USER,USER"
 . D STDNAME^XLFNAME(.ZNAME,"C")
 . I VALUE="" S VALUE="HOSP.NET"
 . S ZEMAIL=$E(ZNAME("GIVEN"))_"."_ZNAME("FAMILY")_"@"_VALUE
 . S RETURN=ZEMAIL
 . Q
 I $S(TYPE="ELSIG":1,TYPE="ACCESS":1,TYPE="VERIFY":1,1:0),VALUE["*",ISIINCR'="" D
 . F  D  Q:VALUE'["*"
 . . F X=1:1:$L(VALUE) I $E(VALUE,X)="*" D  Q
 . . . S VALUE=$E(VALUE,0,(X-1))_ISIINCR_$E(VALUE,(X+1),9999)
 . . . Q
 . . Q
 . S RETURN=VALUE
 . Q
 ;
 Q RETURN
 ;
EVALSSNMASK(VALUE)      ;
 N I,II,X,CNT
 S I=VALUE F X=$L(VALUE)+1:1:9 S $E(I,X)="0"
 S I="9"_I
 S II=VALUE F X=$L(VALUE)+1:1:9 S $E(II,X)="9"
 S II="9"_II
 S CNT=0 F X=I:1:II I '$D(^DPT("SSN",$E(X,2,10))) S CNT=CNT+1
 S I=$E(I,2,10),II=$E(II,2,10)
 Q CNT_"|"_I_"|"_II
 ;
DOB()
 N X,X1,X2,DIFF,TDAY,RESULT
 D NOW^%DTC S TDAY=X
 I $G(LDOB)'="" D
 . D DT^DILF("E",LDOB,.RESULT)
 . S LDOB=RESULT
 I $G(LDOB)="" D  ; Generate Lower limit for DOB
 . S X1=TDAY,X2=-(365*90) D C^%DTC S LDOB=X Q
 I $G(HDOB)="" D  ; Generate Uppoer limit for DOB
 . S X1=TDAY,X2=-(365*10) D C^%DTC S HDOB=X Q
 ; Gererate random DOB between upper and lower limits
 S X1=HDOB,X2=LDOB D ^%DTC S DIFF=X
 S X1=LDOB S X2=$R(DIFF) D C^%DTC S DOB=X
 Q DOB
 ;
SEX()
 N Y S Y=$R(2) S SEX=$S(Y=0:"F",1:"M")
 Q SEX
 ;
CITY()
 N Y K Y
 S Y(1)="ANYTOWN"
 S Y(2)="SMALLVILLE"
 S Y(3)="GOTHAM"
 S Y(4)="CAPITOL CITY"
 S Y(5)="WHOVILLE"
 S Y(6)="METROPOLIS"
 S Y(7)="SPRINGFIELD"
 S Y(8)="ATLANTIS"
 S Y(9)="VILLAGE"
 S Y(10)="EMERALD CITY"
 S Y(11)="CITY ON HILL"
 S Y(12)="SHINING CITY"
 S Y(13)="MOS EISELY"
 S Y(14)="ZION"
 S Y(15)="MAYBERRY"
 S Y(16)="SUNNYDALE"
 S Y(17)="SOUTH PARK"
 S Y(18)="SIN CITY"
 S Y(19)="BEDFORD FALLS"
 S Y(20)="POTTERSVILLE"
 S Y(21)="PLEASANTVILLE"
 S Y(22)="ROCK RIDGE"
 S Y(23)="BRIGADOON"
 S Y=$R(23)+1 S CITY=Y(Y)
 Q CITY
 ;
STATE()
 N R,Y,EXIT
 S EXIT=0,R=$P(^DIC(5,0),"^",3)
 F  Q:EXIT  S Y=$R(R)+1 I $P($G(^DIC(5,Y,0)),U)'="" I $P($G(^DIC(5,Y,0)),U,6)=1 S STATE=Y,EXIT=1
 Q STATE
 ;
STREET()
 N Y,YY
 S Y(1)="LANE"
 S Y(2)="STREET"
 S Y(3)="ROAD"
 S Y(4)="ALLEY"
 S Y(5)="WAY"
 S Y(6)="DRIVE"
 S Y(7)="AVENUE"
 S Y(8)="PARKWAY"
 S Y(9)="COURT"
 ;
 S YY(1)="FIRST"
 S YY(2)="SECOND"
 S YY(3)="THIRD"
 S YY(4)="FOURTH"
 S YY(5)="FIFTH"
 S YY(6)="SIXTH"
 S YY(7)="SEVENTH"
 S YY(8)="EIGHTH"
 S YY(9)="NINTH"
 ;
 Q $R(1000)+1_" "_YY($R(7)+1)_" "_Y($R(9)+1)
 ;
MARSTAT()
 N R,Y,EXIT
 S EXIT=0,R=$P(^DIC(11,0),"^",3)
 F  Q:EXIT  S Y=$R(R)+1 I $P($G(^DIC(11,Y,0)),U)'="" S MARSTAT=Y,EXIT=1
 Q MARSTAT
 ;
NUMTBL  ;
 S NUMCONV(1)="ONE"
 S NUMCONV(2)="TWO"
 S NUMCONV(3)="THREE"
 S NUMCONV(4)="FOUR"
 S NUMCONV(5)="FIVE"
 S NUMCONV(6)="SIX"
 S NUMCONV(7)="SEVEN"
 S NUMCONV(8)="EIGHT"
 S NUMCONV(9)="NINE"
 S NUMCONV(0)="ZERO"
 Q
 ;
IHSPNT(DFN)
 S DFN=+$G(DFN) I 'DFN Q
 I '$D(^DPT(DFN,0)) Q
 I $D(^AUPNPAT(DFN)) Q
 N FDA,MSG,ZIEN
 S FDA(9000001,"?+1,",.01)=DFN
 S FDA(9000001,"?+1,",.02)=$G(DT)
 S FDA(9000001,"?+1,",.03)=$G(DT)
 D UPDATE^DIE("","FDA","ZIEN","MSG")
 Q
