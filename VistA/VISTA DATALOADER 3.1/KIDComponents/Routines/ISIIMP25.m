ISIIMP25 ;ISI Group/MLS -- ADMIT patient
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
ADMIT(ISIMISC)
 ;
 ; ***VERY ROUGH TEST OF CONCEPT ONLY ***
 ;
 S ISIRC=0
 ;
 S VAINDT=ADATE
 D INP^VADPT
 I $G(VAIN(1))]"" S ISIRC="-9^Admit Error:  Patient already admitted"
 I +ISIRC<0 Q ISIRC
 D MAKEADM
 I +ISIRC<0 Q ISIRC
 Q 1
 ;
MAKEADM
 ; Admit the patient
 N %,%H,%I,NOW,NOWI,VAX,VAZ,VAZ2,E,VACC,VAIP,VAQ,VANN,VASET
 N DGPMDA,DGPMSP,DGPMT,DGPMVI,DGUSEOR,DGPMMD,DEF,DGPM1X,DGPMY,DGPMN
 ;
 ; kill, clean-up variables
 D Q^DGPMV3
 D Q^DGPMV2
 D KVAR^VADPT K DGPM2X,DGPMIFN,DGPMDCD,DGPMVI,DGPMY,DIE,DR,I,J,X,X1,Z
 ;
 S (ISIRC,DGPMT)=1,DGUSEOR=0,DGPMN=""
 ;
 I $$GET1^DIQ(2,DFN,.351,"I") S ISIRC="-1^Admit Error: Patient expired" Q
 ; get lodger status
 I $$LODGER^DGPMV(DFN) D
 . S ISIRC="-1^Admit Error: Patient is a lodger...you can not add an admission!"
 . D DISPOQ^DGPMV
 . K DGPMDER
 I ISIRC<1 D EXIT Q
 ;
 ;;; MOVE^DGPMV
 S XQORQUIT=1,DGPME=0 D UC^DGPMV
 ;
 I $G(ADATE) S DGPMY=ADATE
 K VAIP S VAIP("D")="L",VAIP("L")=""
 D INP^DGPMV10,Q^VADPT3
 S X=$P($G(^DPT(DFN,0)),"^",14) I X D
 . D DOM^DGMTR D:'$G(DGDOM) DIS^DGMTU(DFN) K DGDOM
 ;
 ;;; C^DGPMV1
 S DGPM2X=0 ;were DGPMVI variables set 2 times?
 I DGPMT=1,+DGPMVI(2)=4,'$D(^DGPM("APTT1",DFN)) D
 . S ISIRC="-1^Admit Error: THIS PATIENT IS A LODGER AND HAS NO ADMISSIONS ON FILE; YOU MUST CHECK HIM OUT PRIOR TO CONTINUING"
 I (ISIRC<0) D EXIT Q
 ;
 I "^1^2^6"[("^"_+DGPMVI(2)_"^")&("^4^5^"[("^"_DGPMT_"^"))!(+DGPMVI(2)=3&(DGPMT=5)) D LODGER^DGPMV10 S DGPM2X=1
 I +DGPMVI(2)=4&("^1^2^3^6^"[("^"_DGPMT_"^"))!(+DGPMVI(2)=5&(DGPMT=3)) K VAIP S VAIP("D")="L" D INP^DGPMV10 S DGPM2X=1
 ;;; ^DGPMV2
 I '$D(DGPMVI) D
 . S ISIRC="-1^Admit Error: INPATIENT ARRAY NOT DEFINED...MODULE ENTERED INCORRECTLY"
 I (ISIRC<0) D EXIT Q
 ;
 I '$D(DGPMVI) S ISIRC="-1^Admit Error: Missing DGPMVI" D EXIT Q
 ;
 K DGPME S DGPMMD="",DEF="NOW",DGPM1X=0 D S^DGPMV2 I "^1^4^5^"'[("^"_DGPMT_"^") D PTF^DGPMV21
 I $D(DGPME) S ISIRC="-1^Admit Error: Missing PTF" D EXIT Q
 ;
 D NOW^%DTC,S1^DGPMV2
 S DGPML=$S($D(^UTILITY("DGPMVN",$J,1)):$P(^(1),"^",2),1:"") K C,D,I,J,N
 S:$S('DGPMDCD:1,DGPMDCD>%:1,DGPM2X:1,1:0)&$S(DGPMT=1:1,DGPMT=4:1,1:0) DGPMMD=DGPML I $S('DGPMDCD:0,DGPMT=3:1,DGPMT=5:1,DGPMDCD'>%:1,1:0)&$S(DGPMT=1:0,DGPMT=4:0,1:1) S DGPMMD=DGPML,DEF=""
 I $S(DGPMT=2:1,DGPMT=6:1,1:0),DGPMDCD,(DGPMDCD<%) S DEF=""
 ;
 I $D(DGPME),(DGPME="***") D Q^DGPMV2 S ISIRC="-1^Admit Error: No PTF "_$G(DGPME) D EXIT Q
 ;
 K DGPME I DGPMMD S Y=DGPMMD X ^DD("DD") S DEF=Y
 N DGPMN S DGPMN=0
 S DGPMSA=0 D SCHDQ^DGPMV22 ;not a scheduled admission
 K ^UTILITY("DGPM",$J) S (DGPMHY,X)=DGPMY,DGPMOUT=0
 ;S DGPM0ND=DGPMY_"^"_DGPMT_"^"_DFN_"^^^^^^^^^^^"
 S DGPM0ND=DGPMY_"^"_DGPMT_"^"_DFN_"^^^^^^^^^^^"_$S("^1^4^"[("^"_DGPMT_"^"):"",1:DGPMCA)
 I DGPMT=1 S $P(DGPM0ND,"^",25)=$S(DGPMSA:1,1:0)
 D NEW^DGPMV3 ;+10^DGPMV3
 I Y>0 S (DA,DGPMDA)=+Y
 I 'DGPMDA S ISIRC="-1^Admit Error:  Missing movement ien" D EXIT Q
 N ZPMDA S ZPMDA=DGPMDA,DGPMCA=DA,DGPMAN=^DGPM(DA,0) D VAR^DGPMV3
 N SC,ISIVIEN S SC=$$GET1^DIQ(42,ISIWARDIEN,44,"I") I 'SC S ISIRC="-1^ERROR: Can't find Hospital Location." D EXIT Q
 S ISIVIEN=$$VISIT^ISIIMP05(DFN,SC,ADATE,"I")
 I 'ISIVIEN S ISIRC="-1^Admit Error:  Unable to Create Visit IEN" D EXIT Q
 N ZFDA,IENS,ZMSG S IENS=DGPMDA_","
 S ZFDA(405,IENS,.1)="No admit diagnosis text."
 S ZFDA(405,IENS,.04)=ISITYPEIEN
 S ZFDA(405,IENS,.06)=ISIWARDIEN
 S ZFDA(405,IENS,.07)=ISIRMBDIEN ;ISIRMBDIEN
 S ZFDA(405,IENS,.09)=ISIFTSIEN
 S ZFDA(405,IENS,.11)=0
 S ZFDA(405,IENS,.12)=99
 ;S ZFDA(405,IENS,.14)=
 ;S ZFDA(405,IENS,.16)=
 S ZFDA(405,IENS,.18)=ISIMASIEN
 S ZFDA(405,IENS,.19)=ISIPROV
 S ZFDA(405,IENS,.27)=ISIVIEN
 S ZFDA(405,IENS,41)=ISIFAC
 S ZFDA(405,IENS,42)=$$NOW^XLFDT
 S ZFDA(405,IENS,43)=DUZ
 S ZFDA(405,IENS,102)=DUZ ;Last Edited By (Service user)
 S ZFDA(405,IENS,103)=$$NOW^XLFDT
 D FILE^DIE(,"ZFDA","ZMSG") ;File patient movement fields first
 I $G(DIERR) S ISIRC="-1^Admit Error: updating movment file" D EXIT Q
 I $G(ZPMDA),$L($G(ISIWARD)) S ^DGPM("CN",ISIWARD,ZPMDA)=""
 S:DGPMT=1!(DGPMT=4) DGPMCA=+IENS,DGPMAN=^DGPM(+IENS,0) ;D VAR G DR
 D VAR^DGPMV3
 ;
 K DGZ S (^UTILITY("DGPM",$J,DGPMT,DGPMDA,"A"),DGPMA)=$S($D(^DGPM(DGPMDA,0)):^(0),1:"") ;I DGPMT=6 S Y=DGPMDA D AFTER^DGPMV36
 ; go to PTF^DGPMV31
 ;
 K ZFDA S IENS=DFN_","
 S ZFDA(2,IENS,.1)=ISIWARDIEN
 S ZFDA(2,IENS,.101)=ISIRMBDIEN
 S ZFDA(2,IENS,.102)=ZPMDA ;Current movement
 S ZFDA(2,IENS,.105)=ZPMDA ;Current admission
 S ZFDA(2,IENS,.108)=ISIRMBDIEN ;Current room
 D FILE^DIE(,"ZFDA","ZMSG") ;File patient fields
 I $G(DIERR) S ISIRC="-1^Admit Error: updating patient file" D EXIT Q
 I $G(ZPMDA),$L($G(ISIWARD)) S ^DPT("CN",ISIWARD,DFN)=ZPMDA
 ;;; DR^DGPMV3
 S DIE="^DGPM(" I "^1^4^6^"[("^"_DGPMT_"^"),DGPMN S DIE("NO^")=""
 S DGODSPT=$S('$D(^DGPM(DGPMCA,"ODS")):0,^("ODS"):1,1:0)
 S PTF=$P(DGPMA,"^",16)
 N DGELA
 S DGELA=+$P($G(^DGPT(+PTF,101)),U,8)
 S DR="",DIE="^DGPT(" S:$S('$D(^DGPT(+PTF,0)):0,$P(^(0),"^",2)'=+DGPMA:1,1:0) DR=DR_"2////"_+DGPMA_";"
 S DA=PTF I $D(^DGPT(+DA,0)) K DQ,DG D ^DIE
 S Y=+DGPMA
 ;
 ;;; CREATE^DGPTFCR
 S DGPTDATA=U_Y,DIC="^DGPT(",DIC("DR")="[DG PTF CREATE PTF ENTRY]"
 S DIC(0)="FLZ",X=DFN K DD,DO D FILE^DICN S Y=+Y
 S PTF=Y
 S DIE="^DGPM(",DA=DGPMDA,DR=".16////"_+Y K DQ,DG D ^DIE
 ;
 S (^UTILITY("DGPM",$J,DGPMT,DGPMDA,"A"),DGPMA)=$G(^DGPM(DGPMDA,0))_$S($G(^("DIR"))'="":U_^("DIR"),1:"") I DGPMT=6 S Y=DGPMDA D AFTER^DGPMV36
 ;
 ; drop into EVENTS^DGPMV3
 ;;; EVENTS^DGPMV3
 I DGPMT'=4&(DGPMT'=5) D RESET^DGPMDDCN I (DGPMT'=6) D SI^DGPMV33
 S DGOK=0 F I=0:0 S I=$O(^UTILITY("DGPM",$J,I)) Q:'I  F J=0:0 S J=$O(^UTILITY("DGPM",$J,I,J)) Q:'J  I ^(J,"A")'=^("P") S DGOK=1
 I DGOK D ^DGPMEVT ;Invoke Movement Event Driver
 D Q^DGPMV3
 L -^DGPM("C",DFN)
 K ^UTILITY("DGPM",$J)
 ;
 Q
 ;
EXIT ;
 D Q^DGPMV3
 L -^DGPM("C",DFN)
 K ^UTILITY("DGPM",$J)
 Q
