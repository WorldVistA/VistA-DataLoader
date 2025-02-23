ISIIMP19 ;ISI GROUP/MLS -- CONSULTS IMPORT CONT.
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
 S ISIRC=$$VALCONS^ISIIMPUB(.ISIMISC)
 Q ISIRC
 ;
MAKECONS() ;
 ; Create patient(s)
 S ISIRC=$$CONS(.ISIMISC)
 Q ISIRC
 ;
CONS(ISIMISC) ;Create and sign Consult entry
 ; Input - ISIMISC(ARRAY)
 ; Format:  ISIMISC(PARAM)=VALUE
 ;     eg:  ISIMISC("DFN")= 123456
 ;
 ; Output - ISIRC [return code]
 ;          ISIRESUL(0)=1 [if successful]
 ;          ISIRESUL(1)="success" [if successful]
 ;
 N ORVP,ORNP,ORL,DLG,ORDG,ORIT,ORIFN,ORDIALOG,ORDEA,ORAPPT,ORSRC,OREVTDF,RESULT
 N ORNP,ORL,ES,ORWREC
 K ORVP,ORNP,ORL,DLG,ORDG,ORIT,ORIFN,ORDIALOG,ORDEA,ORAPPT,ORSRC,OREVTDF,RESULT
 K ORNP,ORL,ES,ORWREC
 ;
 S ISIRC=1
 S ISIRC=$$PREP()
 I +ISIRC<0 Q ISIRC
 S ISIRC=$$CREATE()
 I +ISIRC<0 Q ISIRC
 S ISIRC=$$SIGN()
 I +ISIRC<0 Q ISIRC
 S ISIRESUL(0)=1
 S ISIRESUL(1)="Success"
 Q ISIRC
 ;
PREP()
 ;
 S ORVP=$G(ISIMISC("DFN")) I ORVP="" Q "-1^Missing DFN (PREP ISIIMP19)."
 S ORNP=$G(ISIMISC("PROV")) I ORNP="" Q "-1^Missing PROV (PREP ISIIMP19)."
 S ORL=$G(ISIMISC("LOC")) I ORL="" Q "-1^Missing LOC (PREP ISIIMP19)."
 S DLG="GMRCOR CONSULT"
 S ORIT=$O(^ORD(101.41,"B",DLG,""))
 I ORIT="" Q "-1^Can't locate GMRCOR CONSULT entry in #101.41 (PREP ISIIMP19)"
 S ORDG=$P(^ORD(101.41,ORIT,0),U,5)
 I ORDG="" Q "-1^Missing Order Dialogue value in #101.41 (PREP ISIIMP19)"
 S ORIFN=""
 K ORDIALOG
 S ORDIALOG(4,1)=$G(ISIMISC("ORDERITEM")) I ORDIALOG(4,1)="" Q "-1^Missing ORDERITEM (PREP ISIIMP19)." ; Orderable item
 S ORDIALOG(15,1)="ORDIALOG(""WP"",15,1)"
 S ORDIALOG("WP",15,1,1,0)=$G(ISIMISC("TEXT"))
 S ORDIALOG(10,1)="O" ;outpatient
 S ORDIALOG(7,1)=9 ;URGENCY (#62.05)
 S ORDIALOG(140,1)="C" ;(B:Bedside;E:Emergency Room;C:Consultant's Choice;)
 S ORDIALOG(15820,1)="TODAY" ;Earliest
 S ORDIALOG(20,1)=""  ;Provisional Diagnosis
 S ORDIALOG(173,1)="" ;Diagnosis code from above
 S ORDIALOG("ORCHECK")=0
 S ORDIALOG("ORTS")=0
 S ORDEA=""
 S ORAPPT=""
 S ORSRC=""
 S OREVTDF=""
 S ES=$G(ISIMISC("ES")) I ES="" Q "-1^Missing ES (PREP ISIIMP19)."
 Q 1
 ;
CREATE()
 ;
 S RESULT=""
 D SAVE^ORWDX(.RESULT,ORVP,ORNP,ORL,DLG,ORDG,ORIT,ORIFN,.ORDIALOG,ORDEA,ORAPPT,ORSRC,OREVTDF)
 I +ISIRC<0 Q ISIRC ;in case M error
 I RESULT<1 Q "-1^Unable to create consult." ;error
 S ORDNO=$P(RESULT(1),U),ORDNO=+$E(ORDNO,2,$L(ORDNO))
 Q 1
 ;
SIGN()
 ;
 S ORWLST=0
 S ES=$$ENCRYP^XUSRB1(ES)
 S ORWREC(1)=ORDNO_";1^1^1^E"
 d SEND^ORWDX(ORWLST,ORVP,ORNP,ORL,ES,.ORWREC)
 I +ISIRC<0 Q ISIRC ;in case M error
 Q 1
