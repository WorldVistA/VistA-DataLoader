ISIIMP05 ;ISI Group/MLS -- Appointment Create Utility
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
VALIDATE()
 ;
 S ADATE=$G(ISIMISC("ADATE"))
 S SC=$G(ISIMISC("CLIN"))
 S DFN=$G(ISIMISC("PATIENT"))
 I 'DFN S DFN=$G(ISIMISC("PAT_SSN"))
 S CDATE=$G(ISIMISC("CDATE")) ; a bit confusing, need to fix
 ;
 D:$G(ISIPARAM("DEBUG"))>0
 .W !,"ADATE:",$G(ADATE)," SC:",$G(SC)," DFN:",DFN
 .W !,"<HIT RETURN TO PROCEED>" R X
 .Q
 ;
 ; Validate import array contents
 S ISIRC=$$VALAPPT^ISIIMPU2
 Q ISIRC
 ;
MAKEAPPT()      ;
 ; Create Appointment
 S ISIRC=$$APPT(ADATE,SC,DFN,CDATE)
 Q ISIRC
 ;
APPT(ADATE,SC,DFN,CDATE)
 ; Input -  ADATE (Appointment Date [internal fileman format])
 ;          SC    (Hospital Location #44)
 ;          DFN   (Patient DFN #2)
 ;
 ; Output - ISIRC [return code]
 ;
 N COLLAT,SDY,COV,SDYC,OEPTR,ISIVIEN
 S ADATE=$G(ADATE),SC=$G(SC),DFN=$G(DFN),CDATE=$G(CDATE)
 ;
 I $D(^DPT(DFN,"S",ADATE,0)),$P($G(^DPT(DFN,"S",ADATE,0)),U,2)'="C" Q "-9^Duplicate Appointment"
 ;
 S ^DPT(DFN,"S",ADATE,0)=SC
 S ^SC(SC,"S",ADATE,0)=ADATE
 S:'$D(^DPT(DFN,"S",0)) ^(0)="^2.98P^^"
 S:'$D(^SC(SC,"S",0)) ^(0)="^44.001DA^^"
 ;
 F SDY=1:1 I '$D(^SC(SC,"S",ADATE,1,SDY)) S:'$D(^(0)) ^(0)="^44.003PA^^" S ^(SDY,0)=DFN_U_15 Q
 S COLLAT=0,COV=3,SDYC="",COV=$S(COLLAT=1:1,1:3),SDYC=$S(COLLAT=7:1,1:"")
 S:ADATE<DT SDSRTY="W" ; Walk-In
 S ^DPT(DFN,"S",ADATE,0)=SC_"^"_""_"^^^^^"_COV_"^^^^"_SDYC_"^^^^^"_9_U_$G(SD17)_"^^"_DT_"^^^^^"_$G(SDXSCAT)_U_$P($G(SDSRTY),U,2)_U_$$NAVA^SDMANA(SC,ADATE,$P($G(SDSRTY),U,2))
 S ^DPT(DFN,"S",ADATE,1)=$G(ADATE)_U_$G(SDSRFU)
 ;xref DATE APPT. MADE field
 D
 .N DIV,DA,DIK
 .S DA=ADATE,DA(1)=DFN,DIK="^DPT(DA(1),""S"",",DIK(1)=20 D EN1^DIK
 .Q
 ;Check in appt
 Q:ISIRC<0 ISIRC ; in case error during reindex
 D ONE^SDAM2(DFN,SC,ADATE,SDY,0,ADATE)
 Q:+ISIRC<0 ISIRC
 ;
 I 'CDATE S CDATE=ADATE+.01
 ;
 ; Create Visit File Entry
 S ISIVIEN=$$VISIT(DFN,SC,ADATE,"O")
 ;
 ;Create encounter
 D ENCOUNTER Q:+ISIRC<0 ISIRC
 ;
 D PCE(DFN,VISIT,PROVIEN,ICD,CPT,SC,ADATE)
 ;
 ;Check out
 D DT^SDCO1(DFN,ADATE,SC,SDY,0,CDATE)
 Q:+ISIRC<0 ISIRC ;in case error
 ;
 ;Finish up
 D PATAPPT
 Q:+ISIRC<0 ISIRC
 ;
 ; Create V PROVIDER entry
 I $G(ISIMISC("PROVIDER")),ISIVIEN S ISIRC=$$VPROV^ISIIMP27(.ISIMISC)
 ;
 Q ISIRC
 ;
VISIT(DFN,SC,ADATE,TYPE) ;
 N VSIT,ZINST,ZTYPE
 K VSIT
 S ISIVIEN=0
 S ZTYPE=$S(TYPE="O":"OUTPATIENT","I":"INPATIENT",1:"OUTPATIENT")
 S VSIT("IO")=ZTYPE ;"OUTPATIENT"
 I TYPE="I" S VSIT("SVC")="H"
 S VSIT("PAT")=DFN
 S VSIT("LOC")=SC
 S VSIT("VDT")=ADATE
 S VSIT("PKG")="PX"
 S VSIT("TYP")=TYPE
 S VSIT(0)="F"
 S ZINST=$$GET1^DIQ(44,SC,3,"I")
 S VSIT("INS")=$S('ZINST:$G(DUZ(2)),1:ZINST)
 I $$CHKVST() Q  ;duplicate visit, don't create
 S ISIVIEN=0 D ^VSIT I VSIT("IEN")>0 S ISIVIEN=+VSIT("IEN")
 S ISIMISC("VISIT_IEN")=ISIVIEN
 Q ISIVIEN
 ;
CHKVST() ;Chec for duplicate entries
 N ZRDATE,VIEN,VDATE S (VIEN,VDATE)=0
 S ZRDATE=9999999-$P(ADATE,".")_"."_$P(ADATE,".",2)
 F  S VIEN=$O(^AUPNVSIT("AA",DFN,ZRDATE,VIEN)) Q:'VIEN!VDATE  D
 . I SC'=$P($G(^AUPNVSIT(VIEN,0)),U,22) Q
 . S VDATE=VIEN
 . Q
 Q VDATE
 ;
ENCOUNTER ;
 N DIE,FDA,MSG,NIEN
 I '$D(^SCE(0)) S ISIRC="-1^VistA Error.  No top level node for OUTPATIENT ENCOUNTER (SCE(0))" Q
 S OEPTR=$P($G(^SCE(0)),U,3)
 K DIE,FDA,MSG
 S FDA(409.68,"+1,",.01)=ADATE
 S FDA(409.68,"+1,",.02)=DFN
 S FDA(409.68,"+1,",.03)=$P($G(^SC(SC,0)),U,7)
 S FDA(409.68,"+1,",.05)=ISIVIEN
 S FDA(409.68,"+1,",.04)=SC
 S FDA(409.68,"+1,",.07)=CDATE
 S FDA(409.68,"+1,",.08)=1
 S FDA(409.68,"+1,",.09)=SDY
 S FDA(409.68,"+1,",.1)=9
 S FDA(409.68,"+1,",.11)=$S($P(^SC(SC,0),U,15):$P(^(0),"^",15),1:+$O(^DG(40.8,0)))
 D UPDATE^DIE("","FDA","NIEN","MSG")
 I $D(MSG) S ISIRC="-1^Problem saving Outpatient Encounter information (#409.68) "_$G(MSG("DIERR",1,"TEXT",1))
 Q:+ISIRC<0
 S OEPTR=+NIEN(1)
 S ISIRC=1
 Q
 ;
PATAPPT ;
 N FDA,MSG,IENS
 K FDA,MSG
 S IENS=ADATE_","_DFN_","
 S FDA(2.98,IENS,3)="O" ;maybe check status and toggle
 S FDA(2.98,IENS,9)=3
 S FDA(2.98,IENS,19)=DUZ
 S FDA(2.98,IENS,21)=OEPTR
 S FDA(2.98,IENS,22)=1
 S FDA(2.98,IENS,25)="O"
 S FDA(2.98,IENS,26)=0
 D FILE^DIE(,"FDA","MSG")
 I $D(MSG) S ISIRC="-1^Problem saving  Appointment info (#2.98) - "_$G(MSG("DIERR",1,"TEXT",1))
 Q:+ISIRC<0
 S ISIRC=1
 Q
 ;
PCE(DFN,VISIT,PROVIEN,ICD,CPT,ISC,APTDT)
 I '$G(DFN) Q
 I '$G(APTDT) Q
 S ISC=$G(ISC),PROVIEN=$G(PROVIEN),ICD=$G(ICD),CPT=$G(CPT)
 N DATA,ERROR,ARRAY,Y,PACKAGE,SOURCE
 S PACKAGE=35 ;ORDER ENTRY/RESULTS REPORTING
 S SOURCE=36 ;TEXT INTEGRATION UTILITIES
 K DATA
 S DATA("ENCOUNTER",1,"ENC D/T")=APTDT,DATA("ENCOUNTER",1,"HOS LOC")=ISC
 S DATA("ENCOUNTER",1,"PATIENT")=DFN,DATA("ENCOUNTER",1,"SERVICE CATEGORY")="A"
 S DATA("ENCOUNTER",1,"ENCOUNTER TYPE")="P"
 ;
 S:PROVIEN DATA("PROVIDER",1,"NAME")=PROVIEN,DATA("PROVIDER",1,"PRIMARY")=1 ;Primary provider
 ;
 I ICD D
 . S DATA("DX/PL",1,"PRIMARY")=1 D
 . S DATA("DX/PL",1,"DIAGNOSIS")=ICD
 ;
 I CPT D
 . I PROVIEN D
 . . S DATA("PROCEDURE",1,"ENC PROVIDER")=PROVIEN
 . . S DATA("PROCEDURE",1,"ORD PROVIDER")=PROVIEN
 . . Q
 . S DATA("PROCEDURE",1,"EVENT D/T")=APTDT
 . S DATA("PROCEDURE",1,"PROCEDURE")=CPT
 . I ICD S DATA("PROCEDURE",1,"DIAGNOSIS")=ICD
 . S DATA("PROCEDURE",1,"QTY")=1
 ;
 S Y=$$DATA2PCE^PXAPI($NA(DATA),PACKAGE,SOURCE,VISIT,,,.ERROR,,.ARRAY)
 Q $S(Y<-1:"D2P: ~PXAPI Err "_$E(Y,1,4),1:"")
