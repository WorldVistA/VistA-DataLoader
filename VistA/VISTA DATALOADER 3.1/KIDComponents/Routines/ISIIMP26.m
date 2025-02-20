ISIIMP26 ;ISI Group/MLS -- discharge patient, DG DISCHARGE PATIENT
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
 ; DFN                           ; patient ien (FIFTYNINE,PATIENT)
 ; ISIMISC("admitDate")          ; admit datetime (past)
 ; ISIMISC("dischargeDateTime")  ;
 ; ISIMISC("typeOfMovement")     ; "DISCHARGED TO HOME"
 ; ISIMISC("masMovementType")    ; "REGULAR"
 ;
DISCHARG(ISIMISC) ; discharge patient (DG DISCHARGE PATIENT)
 N ISIDDATE,ISITYPE,ISITYPEIEN,ISIMAS,ISIMASIEN
 S ISIRC=1,DGQUIET=1
 ;
 I DFN<1 D
 . S ISIRC="-1^Input Error: DFN is not valid"
 QUIT:ISIRC'=1 ISIRC
 ;
 S ISIDDATE=$G(ISIMISC("dischargeDateTime"))
 I ISIDDATE="" S ISIDDATE="NOW"
 ;
 S ISITYPE=$G(ISIMISC("typeOfMovement"))
 S ISITYPEIEN=$O(^DG(405.1,"B",ISITYPE,"")) ; TYPE OF MOVEMENT
 I ISITYPEIEN<1 D
 . S ISIRC="-1^Input Error: Type of Movement is not valid"
 QUIT:ISIRC'=1 ISIRC
 ;
 S ISIMAS=$G(ISIMISC("masMovementType"))
 S ISIMASIEN=$O(^DG(405.2,"B",ISIMAS,"")) ; MAS MOVEMENT TYPE
 I ISIMASIEN<1 D
 . S ISIRC="-1^Input Error: MAS Movement Type is not valid"
 QUIT:ISIRC'=1 ISIRC
 ;;; entry action of DG DISCHARGE PATIENT
 S DGPMT=3
 ;;; PAT^DGMPV
 K ORACTION,ORMENU
 ;;; PAT1^DGPMV
 S DGPMN=0
 ;;; MOVE^DGPMV
 S XQORQUIT=1,DGPME=0 D UC^DGPMV
 ;;; CHK^DGPMV
 ; check for patient expiration
 I 'DGPME,$D(^DPT(DFN,.35)),+^(.35) D
 . S Y=+^(.35)
 . S ISIRC="-1^Discharge Error: Patient expired."
 QUIT:ISIRC'=1 ISIRC
 ;;; ^DGPMV1
 K VAIP S VAIP("D")="L",VAIP("L")="" D INP^DGPMV10,Q^VADPT3
 S X=$P($G(^DPT(DFN,0)),"^",14) I X D
 . D DOM^DGMTR D:'$G(DGDOM) DIS^DGMTU(DFN) K DGDOM
 ;;; CS^DGPMV10
 I '$D(^DGPM("C",DFN)) D
 . S ISIRC="-1^Discharge Error: PATIENT HAS NO INPATIENT OR LODGER ACTIVITY IN THE COMPUTER"
 QUIT:ISIRC'=1 ISIRC
 ;
 ; drop to C^DGPMV1
 S DGPM2X=0 ;were DGPMVI variables set 2 times?
 I "^1^2^6"[("^"_+DGPMVI(2)_"^")&("^4^5^"[("^"_DGPMT_"^"))!(+DGPMVI(2)=3&(DGPMT=5)) D LODGER^DGPMV10 S DGPM2X=1
 I +DGPMVI(2)=4&("^1^2^3^6^"[("^"_DGPMT_"^"))!(+DGPMVI(2)=5&(DGPMT=3)) K VAIP S VAIP("D")="L" D INP^DGPMV10 S DGPM2X=1
 ;
 ;;; ^DGPMV2
 I '$D(DGPMVI) D
 . S ISIRC="-1^Discharge Error: INPATIENT ARRAY NOT DEFINED...MODULE ENTERED INCORRECTLY"
 QUIT:ISIRC'=1 ISIRC
 ;
 K DGPME S DGPMMD="",DEF="NOW",DGPM1X=0 D S^DGPMV2
 I DGPMT=3!(DGPMT=5) K DGPME ;G OLD:DGPMDCD S DGPML="",DGPM1X=1 G NEW
 S DGPML="",DGPM1X=1
 ;
 ;;; NEW^DGPMV2
 S DGX=$S(DGPMT=5:7,DGPMT=6:20,1:0) I DGX S DGONE=1 I $O(^DG(405.1,"AM",DGX,+$O(^DG(405.1,"AM",DGX,0)))) S DGONE=0
 I 'DGX S DGONE=0
 ;
 ;;; SEL2^DGPMV2
 S ISITEST=0 ; added by Lenny
 S DGPMN=0
 S X=$G(ISIMISC("dischargeDateTime"))
 D NOW^%DTC S DGPMN=1,(DGZ,Y)=% X ^DD("DD") S Y=DGZ
 ;
 ;;; CONT^DGPMV2
 S DGPMY=+Y,DGPMDA=$S($D(^UTILITY("DGPMVD",$J,+Y)):+^(Y),1:"") I DGPMT=1!(DGPMT=4) S DGPMCA=+DGPMDA,DGPMAN=$S($D(^DGPM(DGPMCA,0)):^(0),1:DGPMY)
 K %DT ;D ^DGPMV21,SCHDADM^DGPMV22:DGPMT=1&DGPMN,^DGPMV3:DGPMY I $D(DGPME) W:DGPME'="***" !,DGPME G SEL
 ;
 ;;; ^DGPMV21
 S DGPME=""
 I $S('$D(DGPMY):1,DGPMY?7N:0,DGPMY'?7N1".".N:1,1:0) S DGPME="DATE EITHER NOT PASSED OR NOT IN EXPECTED VA FILEMANAGER FORMAT"
 I $S('$D(DGPMT):1,'DGPMT:1,1:0) S DGPME="TRANSACTION TYPE IS NOT DEFINED"
 ;
 I DGPME'="" D
 . D Q^DGPMV21
 . S ISIRC="-1^Discharge Error: "_DGPME
 I ISIRC<0 D EXIT Q ISIRC
 ;
 D PTF^DGPMV22(DFN,DGPMDA,.DGPME,DGPMCA) ;G:$G(DGPME)]"" Q K DGPME
 D:$G(DGPME)]""
 . S ISIRC="-1^Discharge Error: "_DGPME
 . K DGPME
 I ISIRC<0 D EXIT Q ISIRC
 ;
 K DGPME ; added by Lenny
 I DGPMN D
 . D CHK^DGPMV21
 . I $D(DGPME) D
 . . S ISIRC="-1^Discharge Error: "_DGPME
 . . D Q^DGPMV21
 I ISIRC<0 D EXIT Q ISIRC
 K DGPME
 D PTF^DGPMV21
 I $D(DGPME) D
 . S ISIRC="-1^Discharge Error: PTF record is closed for this admission...cannot edit"
 I ISIRC<0 D EXIT Q ISIRC
 ;;; ^DGPMV3
 K ^UTILITY("DGPM",$J)
 D NOW^%DTC S DGNOW=%,DGPMHY=DGPMY,DGPMOUT=0 ;G:'DGPMN DT S X=DGPMY
 S X=DGPMY
 S DGPM0ND=DGPMY_"^"_DGPMT_"^"_DFN_"^^^^^^^^^^^"_$S("^1^4^"[("^"_DGPMT_"^"):"",1:DGPMCA)
 I DGPMT=1 S $P(DGPM0ND,"^",25)=$S(DGPMSA:1,1:0)
 ;-- provider change
 I DGPMT=6,$D(DGPMPC) S DGPM0ND=$$PRODAT^DGPMV3(DGPM0ND)
 ;
 D NEW^DGPMV3
 I Y'>0 D
 . D Q^DGPMV3
 . S ISIRC="-1^Discharge Error: Error from NEW^DGPMV3"
 I ISIRC<0 D EXIT Q ISIRC
 S (DA,DGPMDA)=+Y
 S:DGPMT=1!(DGPMT=4) DGPMCA=DA,DGPMAN=^DGPM(DA,0) D VAR^DGPMV3 ;G DR
 ;;; DR^DGPMV3
 S DIE="^DGPM(" I "^1^4^6^"[("^"_DGPMT_"^"),DGPMN S DIE("NO^")=""
 S DGODSPT=$S('$D(^DGPM(DGPMCA,"ODS")):0,^("ODS"):1,1:0)
 ;
 S DR=""
 S DR=".01///"_$G(ISIMISC("dischargeDateTime"))
 S DR=DR_";.04///"_$G(ISIMISC("typeOfMovement"))
 ;S DR=DR_";.18///"_$G(ISIMISC("masMovementType"))
 S DR=DR_";102////"_DUZ
 S DR=DR_";103///NOW"
 K DQ,DG D ^DIE K DIE
 I $D(Y)#2 S DGPMOUT=1
 ;Modified in patch dg*5.3*692 to include privacy indicator node "DIR"
 K DGZ S (^UTILITY("DGPM",$J,DGPMT,DGPMDA,"A"),DGPMA)=$S($D(^DGPM(DGPMDA,0)):^(0)_$S($G(^("DIR"))'="":U_^("DIR"),1:""),1:"")
 D:DGPMT'=4 @("^DGPMV3"_DGPMT)
 I DGPMA="" D
 . S ISIRC="-1^Discharge Error: Incomplete discharge"
 I ISIRC'=1 D EXIT Q ISIRC
 S (^UTILITY("DGPM",$J,DGPMT,DGPMDA,"A"),DGPMA)=$G(^DGPM(DGPMDA,0))_$S($G(^("DIR"))'="":U_^("DIR"),1:"") I DGPMT=6 S Y=DGPMDA D AFTER^DGPMV36
 ;;; EVENTS^DGPMV3
 S DGQUIET=1 ; added by Lenny
 ;;; EVENTS^DGPMV3
 I DGPMT=4!(DGPMT=5) D RESET^DGPMDDLD
 I DGPMT'=4&(DGPMT'=5) D RESET^DGPMDDCN I (DGPMT'=6) D SI^DGPMV33
 D:DGPMA]"" START^DGPWB(DFN)
 D EN^DGPMVBM ;notify building management if room-bed change
 S DGOK=0 F I=0:0 S I=$O(^UTILITY("DGPM",$J,I)) Q:'I  F J=0:0 S J=$O(^UTILITY("DGPM",$J,I,J)) Q:'J  I ^(J,"A")'=^("P") S DGOK=1 Q
 ;I DGOK D ^DGPMEVT ;Invoke Movement Event Driver
 I DGOK K DTOUT,DIROUT
 D EXIT
 Q ISIRC
 ;
EXIT ;
 D Q^DGPMV3
 D Q^DGPMV2
 L -^DGPM("C",DFN) ; from LOCK^DGPMV1
 D KVAR^VADPT K DGPM2X,DGPMIFN,DGPMDCD,DGPMVI,DGPMY,DIE,DR,I,J,X,X1,Z
 Q
