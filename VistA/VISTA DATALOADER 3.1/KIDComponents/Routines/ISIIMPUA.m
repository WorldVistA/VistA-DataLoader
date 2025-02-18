ISIIMPUA ;ISI GROUP/MLS -- Data Loader File Fetch
 ;;3.1;VISTA DATALOADER;;Dec 23, 2024
 ; Grabs local VistA file content to populate external import select lists
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
PARAM(TABLE)
 ;INPUT:
 ;  TABLE="NOTE" - txt to designate VistA file fetched
 ;
 ;OUTPUT:
 ;  # -- Number of resolved entry (1-30)
 ;
 S TABLE=$$UP^XLFSTR(TABLE)
 S TABLE=$$TRIM^XLFSTR(TABLE)
 I TABLE="NOTE" Q 1 ;TIULIST
 I TABLE="DRUG" Q 2 ;DRUGLIST
 I TABLE="SIG" Q 3 ;SIGLIST
 I TABLE="PROV" Q 4 ;PROVLIST
 I TABLE="USER" Q 5 ;USERLIST
 I TABLE="RACE" Q 6 ;RACE
 I TABLE="ETHN" Q 7 ;ETHNICITY
 I TABLE="EMPLOY" Q 8 ;EMPLOYSTAT
 I TABLE="INSUR" Q 9 ;INSURANCE
 I TABLE="LOC" Q 10 ;LOCATION
 I TABLE="ICD9" Q 11 ;ICD9
 I TABLE="VITAL" Q 12 ;VITALTYPE
 I TABLE="ALLER" Q 13 ;ALLERGEN
 I TABLE="SYMP" Q 14 ;SYMPTOM
 I TABLE="LAB" Q 15 ;LABTESTS
 I TABLE="GENDER" Q 16 ;GENDER
 I TABLE="BOOL" Q 17 ;BOOLEEN
 I TABLE="PROBSTAT" Q 18 ;PROBSTAT
 I TABLE="PROBTYPE" Q 19 ;PROBTYPE
 I TABLE="CONSULT" Q 20 ;
 I TABLE="MAGLOC" Q 21
 I TABLE="RADPROC" Q 22
 I TABLE="PNTTYPE" Q 23 ;PNTTYPE
 I TABLE="MARSTAT" Q 24 ;MARSTAT
 I TABLE="STATE" Q 25 ;STATE
 I TABLE="SERVICE" Q 26 ;SERVICE
 I TABLE="HFACTOR" Q 27 ;Health Factors
 I TABLE="CPT" Q 28 ;CPT codes
 I TABLE="PRVNAR" Q 29 ; Provider Narrative
 I TABLE="IMZ" Q 30 ;Immunization
 I TABLE="EXAM" Q 31
 I TABLE="EDTOPIC" Q 32
 I TABLE="INST" Q 33
 I TABLE="NVAMEDS" Q 34 ; Non-VA Medications
 ;I TABLE="RB" Q 33 ;Room-Bed
 ;I TABLE="WARD" Q 34 ;WARD
 Q -1
 ;
ENTRY(ARRAY,LIST)
 ;INPUT:
 ;  ARRAY = output Array
 ;  LIST = numeric to choose FILE
 ;
 ;OUTPUT:
 ;  ARRAY(0)=CNT ;numeric
 ;  ARRAY(1)=VALUE ;text
 ;
 K ARRAY
 I LIST'?1N.N S ARRAY(0)="-1^Incorrect parameter passed" Q
 I LIST=1 D TIULIST Q
 I LIST=2 D DRUGLIST Q
 I LIST=3 D SIGLIST Q
 I LIST=4 D PROVLIST Q
 I LIST=5 D USERLIST Q
 I LIST=6 D RACE Q
 I LIST=7 D ETHNICITY Q
 I LIST=8 D EMPLOYSTAT Q
 I LIST=9 D INSURANCE Q
 I LIST=10 D LOCATION Q
 I LIST=11 D ICD9 Q
 I LIST=12 D VITALTYPE Q
 I LIST=13 D ALLERGEN Q
 I LIST=14 D SYMPTOM Q
 I LIST=15 D LABTESTS Q
 I LIST=16 D GENDER Q
 I LIST=17 D BOOLEEN Q
 I LIST=18 D PROBSTAT Q
 I LIST=19 D PROBTYPE Q
 I LIST=20 D CONSULT Q
 I LIST=21 D IMAGLOC Q ;
 I LIST=22 D RAPROC Q ;
 I LIST=23 D PNTTYPE Q
 I LIST=24 D MARSTAT Q
 I LIST=25 D STATE Q
 I LIST=26 D SERVICE Q
 I LIST=27 D HFACTOR Q ;Health Factors
 I LIST=28 D ICPT Q ;CPT codes
 I LIST=29 D PRVNAR Q ; Provider Narrative
 I LIST=30 D IMZ Q ;Immunization
 I LIST=31 D EXAM Q ;EXAM
 I LIST=32 D EDTOPIC Q ;EDUCATION TOPICS
 I LIST=33 D INST Q ;Insitutions
 I LIST=34 D NVAMEDS Q ;Non-VA Meds
 ;I LIST=32 Q ;Room-Bed
 ;I LIST=33 Q ;WARD
 S ARRAY(0)="-1^Incorrect parameter passed" Q
 Q
 ;
HDR ;not used -- was thinking about producing entire "TABLES" worksheet as output
 S HDR="Gender,Booleen,Race,Ethnicity,Employ_Status,Insurance,Location,Person,ICD9_Desc,Problem_status,Problem_Type,Vital_type"
 S HDR=HDR_",Allergen,Symptom,Lab_test,Case,Note_title,drug_list,siglist"
 Q
 ;
TIULIST ;#8925.1
 N VALUE,IEN,RESULT,CNT
 S VALUE="",CNT=0
 F  S VALUE=$O(^TIU(8925.1,"B",VALUE)) Q:VALUE=""  D
 . S IEN=$O(^TIU(8925.1,"B",VALUE,"")) I IEN="" Q
 . N ZREC S ZREC=$G(^TIU(8925.1,IEN,0)) I ZREC="" Q
 . I $P(ZREC,U,4)'="DOC" Q  ; TIU Type of DOC
 . I $P(ZREC,U,7)'=11 Q ;TIU status of Active
 . N RESULT D ISCNSLT^TIUCNSLT(.RESULT,IEN) I RESULT'=0 Q ;No CONSULT types
 . S CNT=CNT+1 S ARRAY(CNT)=VALUE
 S ARRAY(0)=CNT I CNT=0 S ARRAY(0)="-1^No results found."
 Q
 ;
DRUGLIST ;#50
 N VALUE,IEN,CNT
 S VALUE="",CNT=0
 F  S VALUE=$O(^PSDRUG("B",VALUE)) Q:VALUE=""  D
 . S IEN=$O(^PSDRUG("B",VALUE,""))
 . I $P($G(^PSDRUG(IEN,2)),"^",1)="" Q ;Missing pointer to Orderable item #50.7
 . I $P($G(^PSDRUG(IEN,0)),"^",3)="" Q ;Missing DEA value
 . I $P($G(^PSDRUG(IEN,660)),"^",6)="" Q ;Missing unit price
 . S CNT=CNT+1 S ARRAY(CNT)=VALUE
 . Q
 S ARRAY(0)=CNT I CNT=0 S ARRAY(0)="-1^No results found."
 Q
 ;
SIGLIST ;#51
 N VALUE,IEN,CNT
 S VALUE="",CNT=0
 F  S VALUE=$O(^PS(51,"B",VALUE)) Q:VALUE=""  D
 . S IEN=$O(^PS(51,"B",VALUE,""))
 . I $P(^PS(51,IEN,0),U,4)>1 Q ;#51,30 Intended use is Inpatient only
 . S CNT=CNT+1 S ARRAY(CNT)=VALUE
 . Q
 S ARRAY(0)=CNT I CNT=0 S ARRAY(0)="-1^No results found."
 Q
 ;
PROVLIST ;#200
 N VALUE,IEN,DTC,IDT,CNT,NAME
 D NOW^%DTC S DTC=X,VALUE="",CNT=0
 F  S VALUE=$O(^VA(200,"B",VALUE)) Q:VALUE=""  D
 . S IEN=$O(^VA(200,"B",VALUE,""))
 . I +$G(^VA(200,IEN,"PS"))'=1 Q ;Authorized to write medical orders check
 . S IDT=$P($G(^VA(200,IEN,"PS")),U,4) I IDT'="" I IDT<DTC Q
 . S NAME=$P($G(^VA(200,IEN,0)),U)
 . I '$D(^VA(200,"AK.PROVIDER",NAME)) Q ;No PROVIDER Security key
 . S CNT=CNT+1 S ARRAY(CNT)=VALUE
 . Q
 S ARRAY(0)=CNT I CNT=0 S ARRAY(0)="-1^No results found."
 Q
 ;
USERLIST ;#200
 N VALUE,IEN,DTC,IDT,CNT
 D NOW^%DTC S DTC=X,VALUE="",CNT=0
 F  S VALUE=$O(^VA(200,"B",VALUE)) Q:VALUE=""  D
 . S IEN=$O(^VA(200,"B",VALUE,""))
 . S IDT=$P($G(^VA(200,IEN,"PS")),U,4) I IDT'="" I IDT<DTC Q
 . S CNT=CNT+1,ARRAY(CNT)=VALUE
 . Q
 S ARRAY(0)=CNT I CNT=0 S ARRAY(0)="-1^No results found."
 Q
 ;
RACE ;#10
 N VALUE,IEN,DTC,IDT,CNT
 S CNT=0
 D NOW^%DTC S DTC=X,VALUE="",CNT=0
 F  S VALUE=$O(^DIC(10,"B",VALUE)) Q:VALUE=""  D
 . S IEN=$O(^DIC(10,"B",VALUE,""))
 . S IDT=$P($G(^DIC(10,IEN,.02)),U,2) I IDT'="" I IDT<DTC Q
 . S CNT=CNT+1,ARRAY(CNT)=VALUE
 . Q
 S ARRAY(0)=CNT I CNT=0 S ARRAY(0)="-1^No results found."
 Q
 ;
ETHNICITY ;#10.2
 N VALUE,IEN,DTC,IDT,CNT
 D NOW^%DTC S DTC=X,VALUE="",CNT=0
 F  S VALUE=$O(^DIC(10.2,"B",VALUE)) Q:VALUE=""  D
 . S IEN=$O(^DIC(10.2,"B",VALUE,""))
 . S IDT=$P($G(^DIC(10.2,IEN,.02)),U,2) I IDT'="" I IDT<DTC Q
 . S CNT=CNT+1,ARRAY(CNT)=VALUE
 . Q
 S ARRAY(0)=CNT I CNT=0 S ARRAY(0)="-1^No results found."
 Q
 ;
EMPLOYSTAT ;#2,.31115 (TABLE)
 S ARRAY(1)="EMPLOYED FULL TIME"
 S ARRAY(2)="EMPLOYED PART TIME"
 S ARRAY(3)="NOT EMPLOYED"
 S ARRAY(4)="SELF EMPLOYED"
 S ARRAY(5)="RETIRED"
 S ARRAY(6)="ACTIVE MILITARY DUTY"
 S ARRAY(7)="UNKNOWN"
 S ARRAY(0)=7
 Q
 ;
INSURANCE ;INSURANCE COMPANY #36
 N VALUE,IEN,DTC,IDT,CNT
 D NOW^%DTC S DTC=X,VALUE="",CNT=0
 F  S VALUE=$O(^DIC(36,"B",VALUE)) Q:VALUE=""  D
 . S IEN=$O(^DIC(36,"B",VALUE,""))
 . S IDT=$P($G(^DIC(36,IEN,0)),U,5) I IDT'="" I IDT<DTC Q
 . S CNT=CNT+1,ARRAY(CNT)=VALUE
 . Q
 S ARRAY(0)=CNT I CNT=0 S ARRAY(0)="-1^No results found."
 Q
 ;
LOCATION ;HOSPITAL LOCATION #44
 N VALUE,IEN,DTC,RDT,IDT,CNT
 D NOW^%DTC S DTC=X,VALUE="",CNT=0
 F  S VALUE=$O(^SC("B",VALUE)) Q:VALUE=""  D
 . S IEN=$O(^SC("B",VALUE,""))
 . S IDT=$P($G(^SC(IEN,"I")),U)
 . S RDT=$P($G(^SC(IEN,"I")),U,2)
 . I IDT'="" I RDT="" I IDT<DTC Q
 . I RDT'="" I RDT>IDT I RDT>DTC Q
 . I RDT'="" I RDT<IDT I IDT<DTC Q
 . S CNT=CNT+1,ARRAY(CNT)=VALUE
 . Q
 S ARRAY(0)=CNT I CNT=0 S ARRAY(0)="-1^No results found."
 Q
 ;
ICD9(ARRAY,TXT) ; Partially implimented
 ;
 N SCORE,CNT
 K ARRAY
 S SCORE=0,CNT=0
 I $G(TXT)="" S ARRAY(0)="-1^No results found." Q
 D ICDFIND
 D ICDFIND1
 I SCORE=0 S ARRAY(0)="-1^No results found." Q
 S SCORE=1 S ARRAY(0)=CNT
 Q
 ;
ICDFIND ; PERFECT MATCH
 I $D(^LEX(757.01,"B",TXT)) D
 . S X=$$ICD9CHK(TXT)
 . I +X<0 Q
 . S CHECK($P(X,U,2))=""
 . S SCORE=1,ARRAY(1)=TXT,CNT=1 Q
 Q
 ;
ICDFIND1 ; FIRST WORD MATCHES
 N CHECK,PATTERN
 S CHECK=$P(TXT," ",1)
 S PATTERN="1"""_CHECK_""".E"
 F  S CHECK=$O(^LEX(757.01,"B",CHECK)) Q:CHECK=""  Q:CHECK'?@PATTERN  D
 . S X=$$ICD9CHK(CHECK)
 . I +X<0 Q
 . I $D(CHECK($P(X,U,2))) Q
 . S CNT=CNT+1
 . S SCORE=1,ARRAY(CNT)=CHECK
 . s CHECK($P(X,U,2))=""
 . Q
 Q
 ;
ICD9CHK(TXT)
 N OUT,EXPIEN,EXPNM,MAJCON,CODE,ICD,ICDIEN
 S (OUT,EXPIEN)="" F  S EXPIEN=$O(^LEX(757.01,"B",TXT,EXPIEN)) Q:'EXPIEN  D  Q:OUT=1
 . S EXPNM=$G(^LEX(757.01,EXPIEN,0)) Q:EXPNM=""
 . S MAJCON=$P($G(^LEX(757.01,EXPIEN,1)),"^") Q:MAJCON=""
 . S CODE="" F  S CODE=$O(^LEX(757.02,"AMC",MAJCON,CODE)) Q:'CODE  D  Q:OUT=1
 . . S ICD=$P($G(^LEX(757.02,CODE,0)),"^",2) Q:ICD=""
 . . S Y=$P($G(^LEX(757.03,$P($G(^LEX(757.02,CODE,0)),"^",3),0)),"^")
 . . I Y="ICD9" S OUT=1 Q
 . . Q
 I EXPNM="" Q -1
 I EXPIEN="" Q -1
 I MAJCON="" Q -1
 I ICD="" Q -1
 S ICDIEN=$O(^ICD9("AB",ICD_" ","")) I ICDIEN="" Q
 Q "1^"_ICDIEN
 ;
VITALTYPE ;#120.51
 N VALUE,CNT
 S VALUE="",CNT=0
 F  S VALUE=$O(^GMRD(120.51,"B",VALUE)) Q:VALUE=""  D
 . S CNT=CNT+1,ARRAY(CNT)=VALUE
 . Q
 S ARRAY(0)=CNT I CNT=0 S ARRAY(0)="-1^No results found."
 Q
 ;
ALLERGEN ;#120.82
 N VALUE,CNT
 S VALUE="",CNT=0
 F  S VALUE=$O(^GMRD(120.82,"B",VALUE)) Q:VALUE=""  D
 . S CNT=CNT+1,ARRAY(CNT)=VALUE
 S ARRAY(0)=CNT I CNT=0 S ARRAY(0)="-1^No results found."
 Q
 ;
SYMPTOM ;#120.83
 N VALUE,CNT
 S VALUE="",CNT=0
 F  S VALUE=$O(^GMRD(120.83,"B",VALUE)) Q:VALUE=""  D
 . S CNT=CNT+1,ARRAY(CNT)=VALUE
 S ARRAY(0)=CNT I CNT=0 S ARRAY(0)="-1^No results found."
 Q
 ;
LABTESTS ;#60
 N VALUE,IEN,Z,Y,CNT
 S VALUE="",CNT=0
 F  S VALUE=$O(^LAB(60,"B",VALUE)) Q:VALUE=""  D
 . I '$D(^LAB(60,"B",VALUE)) Q
 . S IEN=$O(^LAB(60,"B",VALUE,""))
 . S Z=$P($G(^LAB(60,IEN,0)),U,4) I Z'="CH" Q
 . S Z=0,Y=$O(^LAB(60,IEN,3,Z)) I Y="" Q
 . S Z=+$G(^LAB(60,IEN,3,Y,0)) I Z="" Q
 . S COLLIEN=Z,Y=$G(^LAB(62,COLLIEN,0)) S SPECIEN=$P(Y,U,2) I SPECIEN="" Q
 . S CNT=CNT+1,ARRAY(CNT)=VALUE
 S ARRAY(0)=CNT I CNT=0 S ARRAY(0)="-1^No results found."
 Q
 ;
INST ;#4
 N VALUE,IEN,CNT,FACTYP,FACNAME
 S VALUE="",CNT=0
 F  S VALUE=$O(^DIC(4,"B",VALUE)) Q:VALUE=""  S IEN=$O(^(VALUE,0)) D
 . ; Only return VAMC Facility Types
 . S FACTYP=$P($G(^DIC(4,IEN,3)),"^",1)
 . Q:'FACTYP
 . S FACNAME=$P($G(^DIC(4.1,FACTYP,0)),"^",1)
 . I FACNAME'="VAMC" Q
 . ; Don't return Z'd Facility Types
 . I $E(VALUE,1)="Z" Q
 . S CNT=CNT+1,ARRAY(CNT)=VALUE
 S ARRAY(0)=CNT I CNT=0 S ARRAY(0)="-1^No results found."
 Q
 ;
NVAMEDS ; Non-VA meds from Quick Order
 N LAST,LST,VALUE,OITM S VALUE=""
 S OITM=$O(^ORD(101.44,"B","ORWDSET NV RX",0))
 S LAST=$P(^ORD(101.44,OITM,20,0),U,3)
 D FVSUB^ORWUL(.LST,OITM,1,LAST)
 F  S VALUE=$O(LST(VALUE)) Q:VALUE=""  S ARRAY(VALUE)=$P(LST(VALUE),U,2)
 S ARRAY(0)=LAST I LAST=0 S ARRAY(0)="-1^No results found."
 Q
 ;
 ;
GENDER ;#2,.02
 S ARRAY(0)=2
 S ARRAY(1)="FEMALE"
 S ARRAY(2)="MALE"
 Q
BOOLEEN
 S ARRAY(0)=2
 S ARRAY(1)="Y"
 S ARRAY(2)="N"
 Q
PROBSTAT
 S ARRAY(0)=2
 S ARRAY(1)="A" ; Active
 S ARRAY(2)="I" ; Inactive
 Q
PROBTYPE
 S ARRAY(0)=2
 S ARRAY(1)="A" ;Accute
 S ARRAY(2)="C" ;Chronic
 Q
CONSULT ;#123.5
 N RESULT,ARY K ARY S RESULT=""
 D SVCSYN^ORQQCN2(.RESULT,1,1,1)
 K ARY M ARY=@RESULT
 S X=1 F  S X=$O(ARY(X)) Q:X=""  I $P(ARY(X),U,4)'="+"  S ARRAY(X)=$P(ARY(X),U,2)
 I '$D(ARRAY) S ARRAY(0)="-1^No results found."
 Q
 ;
RAPROC ;#71
 N VALUE,CNT,Y,I
 S VALUE="",CNT=0
 F  S VALUE=$O(^RAMIS(71,"B",VALUE)) Q:VALUE=""  D
 . S Y=$O(^RAMIS(71,"B",VALUE,"")) Q:'Y
 . S I=$P($G(^RAMIS(71,Y,"I")),U) I I I I<DT Q ;Inactive
 . ;I $P($G(^RAMIS(71,Y,0)),U,6)="P" Q ;parent
 . S CNT=CNT+1,ARRAY(CNT)=VALUE
 . Q
 S ARRAY(0)=CNT I CNT=0 S ARRAY(0)="-1^No results found."
 Q
 ;
IMAGLOC ;#79.1
 N VALUE,CNT,Y,I
 S X="",VALUE="",CNT=0
 F  S X=$O(^RA(79.1,"B",X)) Q:'X  D
 . S Y=$O(^RA(79.1,"B",X,""))
 . S I=$P($G(^RA(79.1,Y,0)),U,19) I I I I<DT S EXIT=1 Q ;inactive
 . S VALUE=$P($G(^SC(X,0)),U)
 . S CNT=CNT+1,ARRAY(CNT)=VALUE
 . Q
 S ARRAY(0)=CNT I CNT=0 S ARRAY(0)="-1^No results found."
 Q
 ;
TEMPLATE(ISIRESUL) ;return list of TEMPLATES (#9001 ISI PT IMPORT TEMPLATE)
 ; Used for RPC (#8994):  ISI IMPORT GET TEMPLATES
 N $ETRAP,$ESTACK S $ETRAP="D ERR^ISIIMPER"
 N VALUE,CNT,Y,I
 K ISIRESUL
 S X="",VALUE="",CNT=0,ISIRESUL(0)=CNT
 F  S X=$O(^ISI(9001,"B",X)) Q:X=""  D
 . S Y=$O(^ISI(9001,"B",X,""))
 . S CNT=CNT+1,ISIRESUL(CNT)=Y_U_X
 . Q
 S ISIRESUL(0)=CNT I CNT=0 S ISIRESUL(0)="-1^No results found."
 Q
 ;
FETCHTMP(ISIRESUL,TMPLIEN)
 ; Used for RPC (#8994): ISI IMPORT GET TEMPLATE DETLS
 ; IN:   ISIRESUL -- results array (pointer)
 ;       TMPLIEN  -- ien of #9001 (ISI PT IMPORT TEMPLATE FILE)
 ;
 ; OUT:  ISIRESUL(n)  = NAME^TYPE^NAME MASK^SSN MASK^SEX^EARLIEST DOB^LATEST DOB^MARITAL STATUS^ZIP MASK^
 ;                    ...PH MASK^CITY^STATE^VETERAN^DFN_NAME^EMPLOY STAT^SERVICE^EMAIL MASK^USER MASK
 ;
 N $ETRAP,$ESTACK S $ETRAP="D ERR^ISIIMPER"
 N IENS,ARRAY,X,Y,CNT K ARRAY,ISIRESUL
 S ISIRESUL(0)=0
 S TMPLIEN=+$G(TMPLIEN)
 I '$G(TMPLIEN) S ISIRESUL(0)="-1^Bad/missing Template IEN (FETCHTMP~ISIIMPUA)" Q
 I '$D(^ISI(9001,TMPLIEN,0)) S ISIRESUL(0)="-1^Bad/missing Template IEN (FETCHTMP~ISIIMPUA)" Q
 S IENS=TMPLIEN_","
 D GETS^DIQ(9001,IENS,"*","IE","ARRAY")
 I $G(DIERR) S ISIRESUL(0)="-1^Fileman Error (FETCHTMP+16~ISIIMPUA)" Q
 S (X,CNT)=0
 F  S X=$O(ARRAY(9001,IENS,X)) Q:'X  D
 . I $G(ARRAY(9001,IENS,X,"E"))="" Q
 . S CNT=CNT+1 S ISIRESUL(CNT)=X_U_$G(ARRAY(9001,IENS,X,"E"))_U_$G(ARRAY(9001,IENS,X,"I"))
 . Q
 S ISIRESUL(0)=CNT
 Q
 ;
PNTTYPE ;Patient Type (#391)
 ;
 N VALUE,IEN,CNT
 S VALUE="",CNT=0
 F  S VALUE=$O(^DG(391,"B",VALUE)) Q:VALUE=""  D
 . S IEN=$O(^DG(391,"B",VALUE,""))
 . S CNT=CNT+1 S ARRAY(CNT)=VALUE
 . Q
 S ARRAY(0)=CNT I CNT=0 S ARRAY(0)="-1^No results found."
 Q
 ;
MARSTAT ;Marital Status (#11)
 ;
 N VALUE,IEN,CNT
 S VALUE="",CNT=0
 F  S VALUE=$O(^DIC(11,"B",VALUE)) Q:VALUE=""  D
 . S IEN=$O(^DIC(11,"B",VALUE,""))
 . S CNT=CNT+1 S ARRAY(CNT)=VALUE
 . Q
 S ARRAY(0)=CNT I CNT=0 S ARRAY(0)="-1^No results found."
 Q
 ;
STATE ; STATE (#5)
 ;
 N VALUE,IEN,CNT
 S VALUE="",CNT=0
 F  S VALUE=$O(^DIC(5,"B",VALUE)) Q:VALUE=""  D
 . S IEN=$O(^DIC(5,"B",VALUE,""))
 . S CNT=CNT+1 S ARRAY(CNT)=VALUE
 . Q
 S ARRAY(0)=CNT I CNT=0 S ARRAY(0)="-1^No results found."
 Q
 ;
SERVICE ; SERVICE/SECTION (#49)
 ;
 N VALUE,IEN,CNT,EXIT,DTC
 S VALUE="",CNT=0,EXIT=0
 D NOW^%DTC S DTC=X
 F  S VALUE=$O(^DIC(49,"B",VALUE)) Q:VALUE=""  D
 . S IEN=$O(^DIC(49,"B",VALUE,""))
 . ; Check DATE CLOSED (49.07)
 . I $D(^DIC(49,IEN,3)) N ZDT S ZDT=0 F  S ZDT=$O(^DIC(49,IEN,3,ZDT)) Q:'ZDT  D
 . . I ZDT>DTC Q
 . . N Z S Z=$P($G(^DIC(49,IEN,3,X,0)),U,2) I Z,Z<DTC Q
 . . S EXIT=1
 . . Q
 . I EXIT S EXIT=0 Q ;skip
 . S CNT=CNT+1 S ARRAY(CNT)=VALUE
 . Q
 S ARRAY(0)=CNT I CNT=0 S ARRAY(0)="-1^No results found."
 Q
 ;
HFACTOR ;
 N VALUE,IEN,CNT
 S VALUE="",CNT=0
 F  S VALUE=$O(^AUTTHF("B",VALUE)) Q:VALUE=""  D
 . S IEN=$O(^AUTTHF("B",VALUE,0))
 . I $P($G(^AUTTHF(IEN,0)),U,11) Q ;inactive
 . S CNT=CNT+1 S ARRAY(CNT)=VALUE
 . Q
 S ARRAY(0)=CNT I CNT=0 S ARRAY(0)="-1^No results found."
 Q ;Health Factors
 ;
ICPT ;
 N VALUE,IEN,CNT
 S VALUE="",CNT=0
 F  S VALUE=$O(^ICPT("B",VALUE)) Q:VALUE=""  D
 . S IEN=$O(^ICPT("B",VALUE,""))
 . I $P($G(^ICPT(IEN,0)),U,4) Q  ;Inactive
 . S CNT=CNT+1 S ARRAY(CNT)=VALUE
 . Q
 S ARRAY(0)=CNT I CNT=0 S ARRAY(0)="-1^No results found."
 Q ;CPT codes
 ;
PRVNAR ;
 N VALUE,IEN,CNT
 S VALUE="",CNT=0
 F  S VALUE=$O(^AUTNPOV("B",VALUE)) Q:VALUE=""  D
 . S IEN=$O(^AUTNPOV("B",VALUE,""))
 . S CNT=CNT+1 S ARRAY(CNT)=VALUE
 . Q
 S ARRAY(0)=CNT I CNT=0 S ARRAY(0)="-1^No results found."
 Q ; Provider Narrative
 ;
EDTOPIC ;
 ;
 N VALUE,IEN,CNT
 S VALUE="",CNT=0
 F  S VALUE=$O(^AUTTEDT("B",VALUE)) Q:VALUE=""  D
 . S IEN=$O(^AUTTEDT("B",VALUE,""))
 . Q:$P($G(^AUTTEDT(IEN,0)),U,3)
 . S CNT=CNT+1 S ARRAY(CNT)=VALUE
 . Q
 S ARRAY(0)=CNT I CNT=0 S ARRAY(0)="-1^No results found."
 Q
 ;
IMZ ;
 N VALUE,IEN,CNT
 S VALUE="",CNT=0
 F  S VALUE=$O(^AUTTIMM("B",VALUE)) Q:VALUE=""  D
 . S IEN=$O(^AUTTIMM("B",VALUE,0))
 . I $P($G(^AUTTIM(IEN,0)),U,7) Q ;inactive
 . S CNT=CNT+1 S ARRAY(CNT)=VALUE
 . Q
 S ARRAY(0)=CNT I CNT=0 S ARRAY(0)="-1^No results found."
 Q ;Immunization
 ;
EXAM ;
 N VALUE,IEN,CNT
 S VALUE="",CNT=0
 F  S VALUE=$O(^AUTTEXAM("B",VALUE)) Q:VALUE=""  D
 . S IEN=$O(^AUTTEXAM("B",VALUE,0))
 . I $P($G(^AUTTEXAM(IEN,0)),U,4) Q ;inactive
 . S CNT=CNT+1 S ARRAY(CNT)=VALUE
 . Q
 S ARRAY(0)=CNT I CNT=0 S ARRAY(0)="-1^No results found."
 Q ;Immunization
 Q
 ;
CPT(ARRAY,TXT) ; Not implimented.  No good way to limit list to reasonable size
 ;
 N SCORE,CNT,CHECK
 K ARRAY,CHECK
 S SCORE=0,CNT=0
 I $G(TXT)="" S ARRAY(0)="-1^No results found." Q
 D CPTFIND
 D CPTFIND1
 I SCORE=0 S ARRAY(0)="-1^No results found." Q
 S SCORE=1 S ARRAY(0)=CNT
 Q
 ;
CPTFIND ; PERFECT MATCH
 I $D(^LEX(757.01,"B",TXT)) D
 . S X=$$CPTCHK(TXT)
 . I +X<0 Q
 . S CHECK($P(X,U,3))=""
 . S SCORE=1,ARRAY(1)=$E(X,2,9999),CNT=1 Q
 Q
 ;
CPTFIND1 ; LEADING CHAR MATCHES
 N PATTERN
 S CHECK=$P(TXT," ",1)
 S PATTERN="1"""_CHECK_""".E"
 F  S CHECK=$O(^LEX(757.01,"B",CHECK)) Q:CHECK=""  Q:CHECK'?@PATTERN  D
 . S X=$$CPTCHK(CHECK)
 . I +X<0 Q
 . I $D(CHECK($P(X,U,3))) Q
 . S CNT=CNT+1
 . S SCORE=1,ARRAY(CNT)=$E(X,3,9999)
 . S CHECK($P(X,U,3))=""
 . Q
 Q
 ;
CPTFIND3 ; INTERNAL WORD MATCH
 N Z,W,Y,CHK,CHK1,CHK2
 N ZTMP
 S ZTMP=$NA(^XTMP("ISIIMPUA"))
 I '$D(@ZTMP@(0)) D BLDCPTIN ;build the xtmp index
 ;
 S (CHK,CHK1)=""
 S Z=1,W=0,Y=0
 F Y=1:1:$L(TXT) I $E(TXT,Y)=" " S Z=Z+1
 ;
 F W=1:1:Z S CHK=$P(TXT," ",W) I $D(@ZTMP@(CHK)) D
 . S CHK1=""
 . F  S CHK1=$O(@ZTMP@(CHK,CHK1)) Q:'CHK1  D
 . . S CHK2=""
 . . F  S CHK2=$O(@ZTMP@(CHK,CHK1,CHK2)) Q:'CHK2  D
 . . . I $D(CHECK(@ZTMP@(CHK2))) Q
 . . . S CNT=CNT+1
 . . . ;"1^"_CPT_U_CPTIEN_U_$P($G(^ICPT(CPTIEN,0)),U,2)
 . . . S ARRAY(CNT)=$P($G(^ICPT(@ZTMP@(CHK,CHK1,CHK2))),U,2)
 . . . S SCORE=1
 . . . S CHECK(CHK2)=""
 . . Q
 . Q
 Q
 ;
CPTCHK(TXT)
 N OUT,EXPIEN,EXPNM,MAJCON,CODE,CPT,CPTIEN
 S (OUT,EXPIEN)="" F  S EXPIEN=$O(^LEX(757.01,"B",TXT,EXPIEN)) Q:'EXPIEN  D  Q:OUT=1
 . S EXPNM=$G(^LEX(757.01,EXPIEN,0)) Q:EXPNM=""
 . S MAJCON=$P($G(^LEX(757.01,EXPIEN,1)),"^") Q:MAJCON=""
 . S CODE="" F  S CODE=$O(^LEX(757.02,"AMC",MAJCON,CODE)) Q:'CODE  D  Q:OUT=1
 . . S CPT=$P($G(^LEX(757.02,CODE,0)),"^",2) Q:CPT=""
 . . S Y=$P($G(^LEX(757.03,$P($G(^LEX(757.02,CODE,0)),"^",3),0)),"^")
 . . I Y="CPT4" S OUT=1 Q
 . . Q
 I EXPNM="" Q -1
 I EXPIEN="" Q -1
 I MAJCON="" Q -1
 I CPT="" Q -1
 S CPTIEN=$O(^ICPT("B",CPT,"")) I CPTIEN="" Q
 Q "1^"_CPT_U_CPTIEN_U_$P($G(^ICPT(CPTIEN,0)),U,2)
 ;
BLDCPTIN ;
 ;Building CPT XTMP index
 I '$D(@ZTMP@(0)) D
 . N X1,X2,X S X1=DT,X2=30 D C^%DTC ;calculate fileman date +30
 . S @ZTMP@(0)=(X)_U_DT_U_"BLDCPTIN~ISIIMPUA: Create Index for CPTs"
 . Q
 ;
 N W,X,Y,V,Z
 S X="",Y="",V=""
 F  S X=$O(^ICPT("G",X)) Q:X=""  D
 . F  S V=$O(^ICPT("G",X,V)) Q:V=""  D
 . . S W=0
 . . S Y=$L(X) F Z=1:1:Y I $E(X,Z)=" " s W=W+1
 . . F Y=1:1:W D
 . . . S CHECK=$P(X," ",Y)
 . . . I CHECK="OF"!(CHECK="FOR")!(CHECK="OR")!(CHECK="TO")!(CHECK="AND")!(CHECK="WITH")!(CHECK="W/")!(CHECK="AND")!(CHECK="W/O") Q
 . . . S @ZTMP@($P(X," ",Y),Y,V)=X
 . . Q
 . Q
 Q
