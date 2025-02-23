ISIIMP17 ;ISI GROUP/MLS -- MEDS IMPORT CONT.
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
 S ISIRC=$$VALMEDS^ISIIMPU9(.ISIMISC)
 Q ISIRC
 ;
MAKEMEDS() ;
 ; Create patient(s)
 S ISIRC=$$MEDS(.ISIMISC)
 Q ISIRC
 ;
MEDS(ISIMISC) ;Create med order entry
 ; Input - ISIMISC(ARRAY)
 ; Format:  ISIMISC(PARAM)=VALUE
 ;     eg:  ISIMISC("DFN")=123455
 ;
 ; Output - ISIRC [return code]
 ;          ISIRESUL(0)=1 [if successful]
 ;          ISIRESUL(1)=PSOIEN [if successful]
 ;
 N ORZPT,PNTSTAT,PROV,PSODRUG,QTY,DAYSUPLY,REFIL,ORDCONV,RXNUM,PSOIEN
 N COPIES,MLWIND,ENTERBY,UNITPRICE,PSOSITE,LOGDT,DISPDT,ISSDT,SIG
 N X1,X2,EXPIRDT,STATUS,TRNSTYP,LDISPDT,FILLDT,PORDITM,REASON
 N INIT,COM
 ;
 S ISIRC=1
 D PREP
 I +ISIRC<0 Q ISIRC
 D CREATE
 I +ISIRC<0 Q ISIRC
 S ISIRESUL(0)=1
 S ISIRESUL(1)=PSOIEN
 Q ISIRC
 ;
PREP
 ;
 N EXIT
 S ORZPT=ISIMISC("DFN") ;"" ;POINTER TO PATIENT FILE (#2)
 S PSODFN=ORZPT
 S PNTSTAT=$O(^PS(53,"B","NON-VA","")) ; NON-VA ;RX PATIENT STATUS FILE (#53)
 I 'PNTSTAT S PNTSTAT=$O(^PS(53,"B","OTHER",""))
 S PROV=ISIMISC("PROV") ;NEW PERSON FILE (#200)
 S PSODRUG=ISIMISC("DRUG") ;"" ;POINTER TO DRUG FILE (#50)
 S PSODRUG("DEA")=$P($G(^PSDRUG(PSODRUG,0)),U,3)
 S QTY=ISIMISC("QTY") ;NUMBER ;0;7 NUMBER (Required)
 S DAYSUPLY=ISIMISC("SUPPLY") ;NUMBER ; 0;8 NUMBER (Required)
 S REFIL=ISIMISC("REFILL") ;NUMBER ; 0;9 NUMBER (Required)
 S ORDCONV=1 ;'1' FOR ORDER CONVERTED;'2' FOR EXPIRATION TO CPRS;
 S COPIES=1 ;NUMBER
 S MLWIND="W" ;'M' or 'W'
 S ENTERBY=DUZ ;NEW PERSON FILE (#200)
 S UNITPRICE=$P(^PSDRUG(PSODRUG,660),U,6) ;0.009 ;"" ;NUMBER
 S PSOSITE=ISIMISC("PSOSITE") ; OUTPATIENT SITE FILE (#59)
 D NOW^%DTC S LOGDT=% ;LOGIN DATE ; 2;1 DATE (Required)
 S FILLDT=ISIMISC("DATE") ;DATE
 S ISSDT=FILLDT ;DATE
 S DISPDT=ISSDT ;DATE
 S EXPIRDT=$G(ISIMISC("EXPIRDT"))
 I 'EXPIRDT D
 . S X1=DISPDT,X2=180 D C^%DTC ;Default expiration of T+180
 . S EXPIRDT=X ;
 . Q
 S PORDITM=$P($G(^PSDRUG(PSODRUG,2)),U,1) ;PHARMACY ORDERABLE ITEM FILE (#50.7)
 S STATUS=0 ;STA;1 SET (Required) ; '0' FOR ACTIVE;
 S TRNSTYP=1 ; IB ACTION TYPE FILE (#350.1)
 S LDISPDT=FILLDT ;    3;1 DATE
 S REASON="E" ;Activity log ; SET ([E]dit)
 S INIT=DUZ ;NEW PERSON FILE (#200)
 S COM="Oupatient medication order." ;TEXT
 S SIG=ISIMISC("SIG") ;#51,.01
 I $$DUPCHECK() S ISIRC="-9^Duplicate MED/Prescription found."
 Q
 ;
DUPCHECK()
 I '$D(^PSRX("AC",$G(ISSDT))) Q 0
 N X,EXIT S (X,EXIT)=0 F  S X=$O(^PSRX("AC",$G(ISSDT),X)) Q:'X!EXIT  D
 . I $P($G(^PSRX(X,0)),U,6)=$G(PSODRUG),$P($G(^PSRX(X,0)),U,2)=PSODFN S EXIT=X Q
 . Q
 ;
 Q EXIT
 ;
CREATE
 D AUTO^PSONRXN	;RX auto number
 I $G(PSONEW("RX #"))="" S ISIRC="-1^RX Auto number error." Q
 S RXNUM=PSONEW("RX #")
 ;
 S PSOIEN=$P($G(^PSRX(0)),"^",3)+1
 I $D(^PSRX(PSOIEN)) S ISIRC="-1^Problem with PSRX (#50) internal counter" Q ;pointer error
 S $P(^PSRX(0),U,3)=PSOIEN
 ;
 S $P(^PSRX(PSOIEN,0),"^",1)=RXNUM ; 0;1 FREE TEXT (Required)
 S $P(^PSRX(PSOIEN,0),"^",13)=ISSDT ; 0;13 DATE (Required)
 S $P(^PSRX(PSOIEN,0),"^",2)=ORZPT ;POINTER TO PATIENT FILE (#2)
 S $P(^PSRX(PSOIEN,0),"^",3)=PNTSTAT ;RX PATIENT STATUS FILE (#53)
 S $P(^PSRX(PSOIEN,0),"^",4)=PROV ;NEW PERSON FILE (#200)
 S $P(^PSRX(PSOIEN,0),"^",5)="" ; Outpatient ; LOC ;HOSPITAL LOCATION FILE (#44)
 S $P(^PSRX(PSOIEN,0),"^",6)=PSODRUG ;POINTER TO DRUG FILE (#50)
 S $P(^PSRX(PSOIEN,0),"^",7)=QTY ;NUMBER ;0;7 NUMBER (Required)
 S $P(^PSRX(PSOIEN,0),"^",8)=DAYSUPLY ;NUMBER ; 0;8 NUMBER (Required)
 S $P(^PSRX(PSOIEN,0),"^",9)=REFIL ;NUMBER ; 0;9 NUMBER (Required)
 S $P(^PSRX(PSOIEN,0),"^",11)=MLWIND ;'M' or 'W'
 S $P(^PSRX(PSOIEN,0),"^",16)=ENTERBY ;NEW PERSON FILE (#200)
 S $P(^PSRX(PSOIEN,0),"^",17)=UNITPRICE ;NUMBER
 S $P(^PSRX(PSOIEN,0),"^",18)=COPIES ;COPIES
 S $P(^PSRX(PSOIEN,0),"^",19)=ORDCONV ;ORDER CONVERTED        0;19 SET ['1' FOR ORDER CONVERTED;'2' FOR EXPIRATION TO CPRS;]
 ;
 S $P(^PSRX(PSOIEN,2),"^",1)=LOGDT ;LOGIN DATE ; 2;1 DATE (Required)
 S $P(^PSRX(PSOIEN,2),"^",2)=FILLDT ;FILL DATE
 ;S $P(^PSRX(PSOIEN,2),"^",3)=PHARMACIST ; "" ; PHARMACIST ;2;3 POINTER TO NEW PERSON FILE (#200)
 ;S $P(^PSRX(PSOIEN,2),"^",4)="" ; LOT #                  2;4 FREE TEXT
 S $P(^PSRX(PSOIEN,2),"^",5)=DISPDT ; DISPENSED DATE         2;5 DATE (Required)
 S $P(^PSRX(PSOIEN,2),"^",6)=EXPIRDT ;"" ; EXPIRATION DATE
 S $P(^PSRX(PSOIEN,2),"^",9)=PSOSITE ;2;9 POINTER TO OUTPATIENT SITE FILE (#59)
 ;
 S $P(^PSRX(PSOIEN,3),U,1)=DISPDT ;LAST DISPENSED DATE    3;1 DATE
 ;
 S ^PSRX(PSOIEN,"A",0)="^52.3DA^1^1"
 S $P(^PSRX(PSOIEN,"A",1,0),"^",1)=LOGDT	;DATE
 S $P(^PSRX(PSOIEN,"A",1,0),"^",2)=REASON ;SET
 S $P(^PSRX(PSOIEN,"A",1,0),"^",3)=INIT ;NEW PERSON FILE (#200)
 S $P(^PSRX(PSOIEN,"A",1,0),"^",4)=0 ;NUMBER - RX REFERENCE
 S $P(^PSRX(PSOIEN,"A",1,0),"^",5)="ISI automated entry." ;TEXT
 ;
 S ^PSRX(PSOIEN,"OR1")=PORDITM ;PHARMACY ORDERABLE ITEM FILE (#50.7)
 ;
 S $P(^PSRX(PSOIEN,"POE"),"^",1)=1 ; POE RX                 POE;1 SET ['1' FOR YES;]
 ;
 S $P(^PSRX(PSOIEN,"SIG"),"^",1)=SIG ;SIG;1 FREE TEXT (Required)  medication instruction DIC(51)
 S $P(^PSRX(PSOIEN,"SIG"),"^",2)=0 ;OERR SIG (SET: 0 for NO; 1 for YES)
 ;
 S $P(^PSRX(PSOIEN,"STA"),"^",1)=STATUS ;STA;1 SET (Required) ; '0' FOR ACTIVE;
 ;
 ;S ^PSRX(PSOIEN,"IB")=TRNSTYP ;COPAY TRANSACTION TYPE   IB ACTION TYPE FILE (#350.1)
 S ^PSRX(PSOIEN,"TYPE")=0	;TYPE OF RX             TYPE;1 NUMBER
 D OERR,F55,F52,F525
 Q
 ;
OERR ;UPDATES OR1 NODE
 ;THE SECOND PIECE IS KILLED BEFORE MAKING THE CALL
 S $P(^PSRX(PSOIEN,"OR1"),"^",2)=""
 S PSXRXIEN=PSOIEN,STAT="SN",PSSTAT="CM",COMM="",PSNOO="W"
 D EN^PSOHLSN1(PSXRXIEN,STAT,PSSTAT,COMM,PSNOO)
F55 ; - File data into ^PS(55)
 ;S PSODFN=DFN
 S:'$D(^PS(55,PSODFN,"P",0)) ^(0)="^55.03PA^^"
 F PSOX1=$P(^PS(55,PSODFN,"P",0),"^",3):1 Q:'$D(^PS(55,PSODFN,"P",PSOX1))
 S ^PS(55,PSODFN,"P",PSOX1,0)=PSOIEN,$P(^PS(55,PSODFN,"P",0),"^",3,4)=PSOX1_"^"_($P(^PS(55,PSODFN,"P",0),"^",4)+1)
 S ^PS(55,PSODFN,"P","A",$P($G(^PSRX(PSOIEN,2)),"^",6),PSOIEN)=""
 K PSOX1
 Q
F52 ;; - Re-indexing file 52 entry
 K DIK,DA S DIK="^PSRX(",DA=PSOIEN D IX1^DIK K DIK
 Q
 ;
F525 ;UPDATE SUSPENSE FILE
 Q:$G(^PSRX(PSOIEN,"STA"))'=5
 S DA=PSOIEN,X=PSOIEN,FDT=$P($G(^PSRX(PSOIEN,2)),"^",2),TYPE=$P($G(^PSRX(PSOIEN,0)),"^",11)
 S DIC="^PS(52.5,",DIC(0)="L",DLAYGO=52.5,DIC("DR")=".02///"_FDT_";.03////"_$P(^PSRX(PSOIEN,0),"^",2)_";.04////"_TYPE_";.05///0;.06////"_DIV_";2///0" K DD,D0 D FILE^DICN K DD,D0
 Q
 ;
