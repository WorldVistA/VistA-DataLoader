ISIIMPU7 ;ISI GROUP/MLS -- IMPORT Utility LABS ; 2024-01-13
 ;;3.1;VISTA DATALOADER;;Dec 23, 2024
 ;
 ; VistA Data Loader 3.1
 ;
 ; Copyright (C) 2012 Johns Hopkins University
 ; Copyright (C) 2024-2025 DocMe360 LLC
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
MISCDEF ;;+++++ DEFINITIONS OF LAB MISC PARAMETERS +++++
 ;;NAME               |TYPE       |FILE,FIELD |DESC
 ;;-------------------------------------------------------------------------
 ;;PAT_SSN            |FIELD      |           |PATIENT (#2) pointer
 ;;LAB_TEST           |FIELD      |           |Laboratory test name
 ;;RESULT_DT          |FIELD      |           |Date/time of result
 ;;RESULT_VAL         |FIELD      |           |Lab test result value
 ;;LOCATION           |FIELD      |           |Lab test location
 ;;COLLECTION_SAMPLE  |FIELD      |           |Lab test Sample (BLOOD, URINE, etc.)
 Q
 ;
MISCPANEL ;;+++++ DEFINITIONS OF LAB MISC PANEL PARAMETERS +++++
 ;;NAME               |TYPE       |FILE,FIELD |DESC
 ;;-----------------------------------------------------------------------
 ;;PAT_SSN            |FIELD      |           |PATIENT (#2) pointer
 ;;LAB_PANEL          |FIELD      |           |Laboratory panel name
 ;;LAB_TEST           |FIELD      |           |Lab Test^value (multiple)
 ;;RESULT_DT          |FIELD      |           |Date/time of result
 ;;LOCATION           |FIELD      |           |Lab test location
 ;;COLLECTION_SAMPLE  |FIELD      |           |Lab test Sample (BLOOD, URINE, etc.)
 Q
 ;
LABMISC(MISC,ISIMISC) ; MISC parsing
 ;INPUT:
 ;  MISC(0)=PARAM^VALUE - raw list values from RPC client
 ;
 ;OUTPUT:
 ;  ISIMISC(PARAM)=VALUE
 ;
 N MISCDEF
 K ISIMISC
 D LOADMISC(.MISCDEF) ; Load MISC definition params
 S ISIRC=$$LABMISC1("ISIMISC")
 Q ISIRC ;return code
 ;
LABPMISC(MISC,ISIMISC) ; MISC parsing
 ;INPUT:
 ;  MISC(0)=PARAM^VALUE - raw list values from RPC client
 ;
 ;OUTPUT:
 ;  ISIMISC(PARAM)=VALUE
 ;
 N MISCDEF
 K ISIMISC
 D LOADPMISC(.MISCDEF) ; Load MISC definition params
 S ISIRC=$$LABPMISC1("ISIMISC")
 Q ISIRC ;return code
 ;
LABMISC1(DSTNODE) ; Regular labs
 N PARAM,VALUE,DATE,RESULT,MSG,EXIT
 S (EXIT,ISIRC)=0,(I,VALUE)=""
 F  S I=$O(MISC(I))  Q:I=""  D  Q:EXIT
 . S PARAM=$$TRIM^XLFSTR($P(MISC(I),U))  Q:PARAM=""
 . S VALUE=$$TRIM^XLFSTR($P(MISC(I),U,2))
 . I '$D(MISCDEF(PARAM)) S ISIRC="-1^Bad parameter title passed: "_PARAM,EXIT=1 Q
 . I VALUE="" S ISIRC="-1^No data provided for parameter: "_PARAM,EXIT=1 Q
 . I PARAM="RESULT_DT" D
 . . S DATE=VALUE D DT^DILF("TS",DATE,.RESULT,"",.MSG)
 . . I RESULT<0 S EXIT=1,ISIRC="-1^Invalid "_PARAM_" date/time." Q
 . . S VALUE=RESULT
 . . I $P(VALUE,".",2)="" S VALUE=VALUE_".1200"
 . . Q
 . I EXIT Q
 . S @DSTNODE@(PARAM)=VALUE
 . Q
 Q ISIRC ;return code
 ;
LABPMISC1(DSTNODE) ; Panels
 N PARAM,VALUE,DATE,RESULT,MSG,EXIT
 S (EXIT,ISIRC)=0,(I,VALUE)=""
 F  S I=$O(MISC(I))  Q:I=""  D  Q:EXIT
 . S PARAM=$$TRIM^XLFSTR($P(MISC(I),U))  Q:PARAM=""
 . S VALUE=$$TRIM^XLFSTR($P(MISC(I),U,2,99))
 . I '$D(MISCDEF(PARAM)) S ISIRC="-1^Bad parameter title passed: "_PARAM,EXIT=1 Q
 . I VALUE="" S ISIRC="-1^No data provided for parameter: "_PARAM,EXIT=1 Q
 . I PARAM="RESULT_DT" D
 . . S DATE=VALUE D DT^DILF("TS",DATE,.RESULT,"",.MSG)
 . . I RESULT<0 S EXIT=1,ISIRC="-1^Invalid "_PARAM_" date/time." Q
 . . S VALUE=RESULT
 . . I $P(VALUE,".",2)="" S VALUE=VALUE_".12"
 . . Q
 . I EXIT Q
 . I PARAM="LAB_TEST" D  Q
 . . N TESTNAME,LABVALUE
 . . S TESTNAME=$P(VALUE,U,1)
 . . S LABVALUE=$P(VALUE,U,2)
 . . I TESTNAME="" S EXIT=1,ISIRC="-1^Invalid "_PARAM_" with test"_TESTNAME Q
 . . I LABVALUE="" S EXIT=1,ISIRC="-1^Invalid "_PARAM_" with lab value"_LABVALUE Q
 . . S @DSTNODE@(PARAM,TESTNAME)=LABVALUE
 . S @DSTNODE@(PARAM)=VALUE
 . Q
 ; We do this here is we use this as a sentinel flag in ISIIMP12... we need to exit before reaching that point.
 ; EXIT=0 means that LAB_PANEL was supplied, but it was empty. In that case, use the existing error message
 I EXIT=0,'$D(@DSTNODE@("LAB_PANEL")) S ISIRC="-1^Missing LAB_PANEL",EXIT=1
 Q ISIRC ;return code
 ;
LOADMISC(MISCDEF) ;
 N BUF,FIELD,I,NAME,TYPE
 K MISCDEF
 F I=3:1  S BUF=$P($T(MISCDEF+I),";;",2)  Q:BUF=""  D
 . S NAME=$$TRIM^XLFSTR($P(BUF,"|"))  Q:NAME=""
 . S TYPE=$$TRIM^XLFSTR($P(BUF,"|",2))
 . S FIELD=$$TRIM^XLFSTR($P(BUF,"|",3))
 . S MISCDEF(NAME)=TYPE_"|"_FIELD
 Q
 ;
LOADPMISC(MISCDEF) ;
 N BUF,FIELD,I,NAME,TYPE
 K MISCDEF
 F I=3:1  S BUF=$P($T(MISCPANEL+I),";;",2)  Q:BUF=""  D
 . S NAME=$$TRIM^XLFSTR($P(BUF,"|"))  Q:NAME=""
 . S TYPE=$$TRIM^XLFSTR($P(BUF,"|",2))
 . S FIELD=$$TRIM^XLFSTR($P(BUF,"|",3))
 . S MISCDEF(NAME)=TYPE_"|"_FIELD
 Q
 ;
VALLAB(ISIMISC) ; Entry point to validate content of LAB create/array
 ; Input - ISIMISC(ARRAY)
 ; Format:  ISIMISC(PARAM)=VALUE
 ;     eg:  ISIMISC("LAB_TEST")="CHOLESTEROL"
 ;
 ; Output - ISIRC [return code]
 D LOADMISC(.MISCDEF) ; Load MISC definition params
 ;
 N INVALSSN S INVALSSN=$$INVALSSN(.ISIMISC)
 I INVALSSN Q INVALSSN
 ;
 ; -- LAB_TEST (including result validation) --
 N INVALLAB S INVALLAB=$$INVALLAB(.ISIMISC)
 I INVALLAB Q INVALLAB
 ;
 ; -- RESULT_DT --
 N INVRDT S INVRDT=$$INVRDT(.ISIMISC)
 I INVRDT Q INVRDT
 ;
 ; -- ENTERED_BY --
 N INVEBY S INVEBY=$$INVEBY(.ISIMISC)
 I INVEBY Q INVEBY
 ;
 ; -- LOCATION --
 N INVLOC S INVLOC=$$INVLOC(.ISIMISC)
 I INVLOC Q INVLOC
 ;
 ; -- Check for for Duplicate Lab --
 ; $P(ISIMISC(1),U,2) = LABNAME
 I $$LABDUP(ISIMISC("DFN"),ISIMISC("RESULT_DT"),$P(ISIMISC(1),U,2)) Q "-1^Duplicate Lab Test entry for patient."
 Q 1
 ;
LABDUP(DFN,DTTM,LTEST) ; Check if lab entry already exists
 N LRDFN,LRDN,LTIEN,RVDT,EXIT
 S EXIT=0
 S LRDFN=$$LRDFN^LRPXAPIU(DFN)
 I 'LRDFN Q 0
 I LTEST["-" S LTIEN=$$LOINC2L(LTEST) I 1
 E  S LTIEN=$O(^LAB(60,"B",LTEST,0))
 S LRDN=$$LRDN^LRPXAPIU(LTIEN)
 S RVDT=$$LRIDT^LRPXAPIU(DTTM)
 I $D(^LR(LRDFN,"CH",RVDT,LRDN)) S EXIT=1
 Q EXIT
 ;
INVALSSN(ISIMISC) ; -- PAT_SSN_CHECK --
 N VALUE,EXIT,DFN
 S EXIT=0
 I '$D(ISIMISC("PAT_SSN")) Q "-1^Missing Patient SSN."
 I $D(ISIMISC("PAT_SSN")) D
 . S VALUE=$G(ISIMISC("PAT_SSN")) I VALUE="" S EXIT=1 Q
 . I '$D(^DPT("SSN",VALUE)) S EXIT=1 Q
 . S DFN=$O(^DPT("SSN",VALUE,"")) I DFN="" S EXIT=1 Q
 . S ISIMISC("DFN")=DFN
 . Q
 Q:EXIT "-1^Invalid PAT_SSN (#2,.09)."
 Q 0
 ;
INVALLAB(ISIMISC) ; -- LAB CHECK and populate ISIMISC with Lab Information --
 N VALUE,EXIT,MSG,Y,Z,LABNAME,COLLIEN,SPECIEN,LRTS
 N DIQUIET S DIQUIET=1
 S EXIT=0
 N ERRITEM S ERRITEM=$S($D(ISIMISC("IS_PANEL")):"LAB_PANEL",1:"LAB_TEST")
 I '$D(ISIMISC("LAB_TEST")) Q "-1^Missing LAB_TEST."
 I $D(ISIMISC("LAB_TEST")) D
 . S VALUE=$G(ISIMISC("LAB_TEST")) I VALUE="" S EXIT=1,MSG="Missing value for "_ERRITEM_" (#60)." Q
 . ;
 . ; Get Lab IEN (LRTS)
 . S LRTS=0
 . I VALUE["-" S LRTS=$$LOINC2L(VALUE)
 . I 'LRTS D  Q:EXIT
 .. I '$D(^LAB(60,"B",VALUE)) S EXIT=1,MSG="Couldn't find ien for "_ERRITEM_" (#60)." Q
 .. S Y=$O(^LAB(60,"B",VALUE,"")) I Y="" S EXIT=1,MSG="Couldn't find ien for "_ERRITEM_" (#60)." Q
 .. S LRTS=Y
 . ;
 . ; Must be CH
 . S Z=$P($G(^LAB(60,LRTS,0)),U,4) I Z'="CH" S EXIT=1,MSG=ERRITEM_" incorrect. SUBSCRIPT (#60,4) must by 'CH'." Q
 . ;
 . ; Get Collection Sample and default specimen from collection sample
 . I $D(ISIMISC("PANEL_COLLECTION_SAMPLE")) S Z=ISIMISC("PANEL_COLLECTION_SAMPLE")
 . E  I $D(ISIMISC("COLLECTION_SAMPLE")) D
 .. ; NB: This works on VEHU, but FOIA doesn't exactly match
 .. ; https://www.phlebotomyusa.com/blog/phlebotomy/which-color-tube-for-what-test/
 .. N X
 .. S X=ISIMISC("COLLECTION_SAMPLE")
 .. I X="SERUM" S X="GOLD TOP"
 .. I X="PLASMA" S X="GREEN TOP"
 .. I X="BLOOD" S X="RED TOP"
 .. ; URINE, SPUTUM, CSF, and ARTERIAL BLOOD can be looked up directly and don't need transforms
 .. S Z=$$FIND1^DIC(62,,"XM",X)
 .. S:Z=0 Z=""
 . E  D  Q:EXIT
 .. S Z=0,Y=$O(^LAB(60,LRTS,3,Z))
 .. I Y="" S EXIT=1,MSG="Couldn't locate COLLECTION SAMPLE (#60.03) for "_ERRITEM_" value." Q
 .. S Z=+$G(^LAB(60,LRTS,3,Y,0))
 . I Z="" S EXIT=1,MSG="Couldn't locate COLLECTION SAMPLE (#60.03) for "_ERRITEM_" value." Q
 . S COLLIEN=Z,Y=$G(^LAB(62,COLLIEN,0)) S SPECIEN=$P(Y,U,2) I SPECIEN="" S EXIT=1,MSG="Couldn't locate DEFAULT SPECIMIN (#62,2) for "_ERRITEM_" value." Q
 . ;
 . ; We will need this later
 . S LABNAME=$P($G(^LAB(60,LRTS,0)),U)
 . ;
 . ; Validate that the result is valid
 . ; Copied from V45^LRVER5
 . I '$D(ISIMISC("IS_PANEL")) D  Q:EXIT
 .. ; Validate that we have a result
 .. N X S X=$G(ISIMISC("RESULT_VAL"))
 .. I X="" S EXIT=1,MSG="Missing RESULT_VAL." QUIT
 .. ;
 .. I X="pending" QUIT  ; Panel member not supplied. That's okay.
 .. ;
 .. N LRXD,LRXDP,LRXDH,LRSB,LRERR
 .. S LRXD=U_$P(^LAB(60,LRTS,0),U,12),LRXDP=LRXD_"0)",LRXDP=@LRXDP
 .. S LRXDH=LRXD_"3)"
 .. S LRSB=$$LRDN^LRPXAPIU(LRTS)
 .. ;
 .. ; For set of codes tests, input transform is just Q
 .. X $P(LRXDP,U,5,99)
 .. ;
 .. ; If X is gone, then there's a problem
 .. ; If the result is numeric, try to see if it's a problem with demical places
 .. ; Get Q9 from the input transform
 .. I '$D(X),ISIMISC("RESULT_VAL"),$P(LRXDP,U,5,99)["S Q9=" D
 ... ; Remove call to LRNUM
 ... N R S R(" D ^LRNUM")=""
 ... N LRXDPQ,Q9,DP S LRXDPQ=$$REPLACE^XLFSTR($P(LRXDP,U,5,99),.R)
 ... ; Resupply X since the modified input transform may require it again
 ... S X=ISIMISC("RESULT_VAL")
 ... X LRXDPQ
 ... S DP=$P(Q9,",",3)
 ... S X=$J(ISIMISC("RESULT_VAL"),0,DP) ; Round X
 ... X $P(LRXDP,U,5,99) ; Try again
 .. ;
 .. ; Set of codes (copied from LRSET^LRVER5)
 .. I $P(LRXDP,U,2)["S" D
 ... N I,RESULT
 ... D CHK^DIE(63.04,LRSB,"EH",X,.RESULT,"LRERR")
 ... I RESULT'="^" S X=RESULT QUIT
 ... K X
 ... S LRERR=""
 ... F I=1:1:LRERR("DIHELP") S LRERR=LRERR_LRERR("DIHELP",I)_" "
 ... S $E(LRERR,$L(LRERR))=""
 .. ;
 .. I '$D(X) S EXIT=1,MSG=LABNAME_" result validation error: " S MSG=MSG_$S($D(LRERR)#2:LRERR,$D(@LRXDH):@LRXDH,1:@LRXDP) Q
 .. ;
 .. ; Update the result in case we transformed it.
 .. S ISIMISC("RESULT_VAL")=X
 . ;
 . ; Output
 . S ISIMISC(1)=LRTS_U_LABNAME_U_COLLIEN_U_U_SPECIEN
 . Q
 Q:EXIT "-1^"_MSG
 Q 0
 ;
LOINC2L(LOINC) ; [Private] Lookup LOINC to a lab
 ; Strip Checksum
 N LIEN S LIEN=+LOINC
 ;
 ; Check #64 WKLD CODE Default LOINC and LOINC by Sample and get entries and values of NLT codes
 N WINDX,WIEN,WARRAY
 F WINDX="AI","AH" F WIEN=0:0 S WIEN=$O(^LAM(WINDX,LIEN,WIEN)) Q:'WIEN  S WARRAY(WIEN)=$P(^LAM(WIEN,0),U,2)
 ;
 ; Look in the Result NLT code index (AE) to get the labs. Get the first matching lab
 N LRTS S LRTS=0
 S WIEN=0 F  S WIEN=$O(WARRAY(WIEN)) Q:'WIEN  S LRTS=$O(^LAB(60,"AE",WARRAY(WIEN),0)) Q:LRTS
 ;
 Q LRTS
 ;
INVRDT(ISIMISC) ; Validate Result Date
 N FILE,FIELD,FLAG,VALUE,Y,RESULT,MSG
 I $G(ISIMISC("RESULT_DT"))="" Q "-1^Missing RESULT_DT entry."
 Q 0
 ;
INVEBY(ISIMISC) ; Validate DUZ and populate initials
 I '$D(^XUSEC("LRLAB",DUZ))!('$D(^XUSEC("LRVERIFY",DUZ))) Q "-1^Invalid ENTERED_BY (#200,.01).  Insufficient privilages."
 S ISIMISC("INITIALS")=$P($G(^VA(200,DUZ,0)),U,2)
 Q 0
 ;
INVLOC(ISIMISC) ; Validate location
 N VALUE,EXIT,Y,IDT,RDT
 S EXIT=0
 I '$D(ISIMISC("LOCATION")) Q "-1^Missing LOCATION."
 I $D(ISIMISC("LOCATION")) D
 . S VALUE=$G(ISIMISC("LOCATION")) I VALUE="" S EXIT=1 Q
 . S Y=$O(^SC("B",VALUE,"")) I Y="" S EXIT=1 Q
 . S IDT=$P($G(^SC(Y,"I")),U)
 . S RDT=$P($G(^SC(Y,"I")),U,2)
 . I IDT'="" I RDT="" I IDT<DT S EXIT=1 Q
 . I RDT'="" I RDT>IDT I RDT>DT S EXIT=1 Q
 . I RDT'="" I RDT<IDT I IDT<DT S EXIT=1 Q
 . S ISIMISC("LOCATION")=Y
 . Q
 Q:EXIT "-1^Invalid LOCATION value (#44,.01)."
 Q 0
 ;
VALPANEL(ISIMISC) ; Entry point to validate content of LAB panel
 ; Input - ISIMISC(ARRAY)
 ; Format:  ISIMISC(PARAM)=VALUE
 ;     eg:  ISIMISC("LAB_PANEL")="CHEM-7"
 ;
 ; Output - ISIRC [return code]
 D LOADMISC(.MISCDEF) ; Load MISC definition params
 ;
 N INVALSSN S INVALSSN=$$INVALSSN(.ISIMISC)
 I INVALSSN Q INVALSSN
 ;
 ; -- LAB_TEST --
 N INVALLAB S INVALLAB=$$INVALPNL(.ISIMISC)
 I INVALLAB Q INVALLAB
 ;
 ; -- RESULT_DT --
 N INVRDT S INVRDT=$$INVRDT(.ISIMISC)
 I INVRDT Q INVRDT
 ;
 ; -- ENTERED_BY --
 N INVEBY S INVEBY=$$INVEBY(.ISIMISC)
 I INVEBY Q INVEBY
 ;
 ; -- LOCATION --
 N INVLOC S INVLOC=$$INVLOC(.ISIMISC)
 I INVLOC Q INVLOC
 ;
 ; -- Check for for Duplicate Lab --
 N X,FLAG,LABNAME
 S X="",FLAG=0,LABNAME=""
 F  S X=$O(ISIMISC("LAB_TEST",X)) Q:X=""  D  Q:FLAG
 . S LABNAME=$P(ISIMISC("LAB_TEST",X,1),U,2)
 . I $$LABDUP(ISIMISC("DFN"),ISIMISC("RESULT_DT"),LABNAME) S FLAG=1
 Q:FLAG "-1^Duplicate Lab Test "_LABNAME_" for patient."
 Q 1
 ;
INVALPNL(ISIMISC) ; Validate and explode panel into individual tests
 ; ISIMISC("LAB_PANEL")=CHEM-7
 ; ISIMISC("LAB_TEST","BUN")=10
 ; ISIMISC("LAB_TEST","CO2")=34
 ; ISIMISC("LAB_TEST","GLUCOSE")=180
 I $G(ISIMISC("LAB_PANEL"))="" Q "-1^Missing LAB_PANEL."
 N ARR
 S ARR("LAB_TEST")=ISIMISC("LAB_PANEL")
 S ARR("IS_PANEL")=1
 I $D(ISIMISC("COLLECTION_SAMPLE"))#2 S ARR("COLLECTION_SAMPLE")=ISIMISC("COLLECTION_SAMPLE")
 ; Validate that each of the individual items being sent
 S INVALLAB(ARR("LAB_TEST"))=$$INVALLAB(.ARR)
 K ARR("IS_PANEL")
 I INVALLAB(ARR("LAB_TEST")) Q INVALLAB(ARR("LAB_TEST"))
 S ISIMISC("LAB_PANEL",1)=ARR(1)
 S ISIMISC("PANEL_COLLECTION_SAMPLE")=$P(ARR(1),U,3)
 K ARR
 ;
 ; Keep track of IENs for the panel inputs
 ; LABIENS(IEN)=NAME
 N LABIENS
 N X S X=""
 F  S X=$O(ISIMISC("LAB_TEST",X)) Q:X=""  D
 . S ARR("LAB_TEST")=X
 . ; If we explictly specify a collection sample, use that; else, use the panel's collection sample
 . I $D(ISIMISC("COLLECTION_SAMPLE")) S ARR("COLLECTION_SAMPLE")=ISIMISC("COLLECTION_SAMPLE")
 . E  I $D(ISIMISC("PANEL_COLLECTION_SAMPLE")) S ARR("PANEL_COLLECTION_SAMPLE")=ISIMISC("PANEL_COLLECTION_SAMPLE")
 . S ARR("RESULT_VAL")=ISIMISC("LAB_TEST",X)
 . S INVALLAB(ARR("LAB_TEST"))=$$INVALLAB(.ARR)
 . I INVALLAB(ARR("LAB_TEST")) Q  ; Errors, don't continue
 . S ISIMISC("LAB_TEST",X,1)=ARR(1)
 . ; in case the result was rounded, update our value
 . S ISIMISC("LAB_TEST",X)=ARR("RESULT_VAL")
 . S LABIENS(+ARR(1))=X
 . K ARR
 ; INVALLAB("BUN")=0
 ; INVALLAB("CHEM 7")=0
 ; INVALLAB("CO2")=0
 ; INVALLAB("GLUCOSE")=0
 N INVALIDLIST S INVALIDLIST=""
 S X="" F  S X=$O(INVALLAB(X)) Q:X=""  I INVALLAB(X) S INVALIDLIST=INVALIDLIST_X_": "_$P(INVALLAB(X),U,2)_";"
 S $E(INVALIDLIST,$L(INVALIDLIST))="" ; remove trailing semicolon
 I $L(INVALIDLIST) Q "-1^"_INVALIDLIST
 N EXPPANEL D EXPPNL(+ISIMISC("LAB_PANEL",1),.EXPPANEL)
 ;
 N NOTINPANEL S NOTINPANEL=""
 N I F I=0:0 S I=$O(LABIENS(I)) QUIT:'I  D
 . I '$D(EXPPANEL(I)) S NOTINPANEL=NOTINPANEL_LABIENS(I)_","
 S $E(NOTINPANEL,$L(NOTINPANEL))=""
 I $L(NOTINPANEL) Q "-1^Labs supplied ("_NOTINPANEL_") are not in panel "_ISIMISC("LAB_PANEL")
 Q 0
 ;
EXPPNL(PANEL,EXP) ; [Private] Expand panels
 ; Input:
 ; - Panel IEN
 ; Output:
 ; - EXP(lab IEN)=lab name
 N LRIEN,LRTEST,LRCFL,LRNLT,LRSUB,S2,T1,X
 S T1=1
 S LRNLT=+$G(^LAB(60,PANEL,64))
 S LRTEST(T1)=PANEL_U_^LAB(60,PANEL,0)
 S LRTEST(T1,"P")=LRNLT_U_$$NLT^ISIIMPL5(LRNLT)
 DO EX1^ISIIMPL5
 N I F I=0:0 S I=$O(^TMP("LR",$J,"TMP",I)) Q:'I  S X=^(I),EXP(X)=$P(^LAB(60,X,0),U)
 ; Kill temp globals created in lab package
 K ^TMP("LR",$J,"TMP")
 K ^TMP("LR",$J,"VTO")
 QUIT
 ;
LIEN(LNAME) ; [Public] Lab IEN
 I $G(LNAME)="" Q 0
 Q $O(^LAB(60,"B",LNAME,""))
 ;
PMEM(PANEL,LAB) ; [Public] Is lab a member of a panel
 I $G(PANEL)="" Q 0
 I $G(LAB)="" Q 0
 ;
 N PIEN S PIEN=$$LIEN(PANEL)
 I 'PIEN Q 0
 ;
 N EXP D EXPPNL(PIEN,.EXP)
 ; EXP subscript is the lab IEN
 Q ''$D(EXP($$LIEN(LAB)))
