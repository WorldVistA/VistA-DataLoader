ISIIMP13 ;ISI GROUP/MLS -- LABS IMPORT CONT.
 ;;1.0;;;Jun 26,2012;Build 31
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
 S ISIRC=$$VALLAB^ISIIMPU7(.ISIMISC)
 Q ISIRC
 ;
MAKELAB() ;
 ; Create patient(s)
 S ISIRC=$$IMPRTLAB(.ISIMISC)
 Q ISIRC
 ;
IMPRTLAB(ISIMISC) ;Create lab entry
 ; Input - ISIMISC(ARRAY)
 ; Format:  ISIMISC(PARAM)=VALUE
 ;     eg:  ISIMISC("RESULT_VAL")=110 
 ;
 ; Output - ISIRC [return code]
 ;          ISIRESUL(0)=1 [if successful]
 ;          ISIRESUL(1)="success" [if successful] 
 ;
 N NODE,DFN
 ;
 I ^%ZOSF("OS")["GT.M" N ZIO K ZIO M ZIO=IO N IO M IO=ZIO S %ZIS="OH",IOP="NULL" D ^%ZIS S XWBNULL=IO,IO(0)=IO O IO(0) U IO(0)
 ;
 D PREP Q:+ISIRC<0 ISIRC
 D LAB
 ;
 I ^%ZOSF("OS")["GT.M" U XWBTDEV:(nowrap:nodelimiter:ioerror="TRAP")
 ;
 Q ISIRC
 ;
PREP
 ;
 I $G(ISIPARAM("DEBUG"))>0 D  
 . W !,"+++ Pre-Prep values +++",!
 . I $D(ISIMISC) W $G(ISIMISC) S X="" F  S X=$O(ISIMISC(X)) Q:X=""  W !,ISIMISC(X)
 . W !,"<HIT RETURN>" R X
 . Q
 D KILL
 S LRWP=1,LRQUIET=1
 S DFN=ISIMISC("DFN")
 S LRZPT=DFN
 S NODE=$NAME(^TMP("LRSTIK",$J))
 S LRLLOC=ISIMISC("LOCATION")
 S ^TMP("LRVEHU",$J,"R")=ISIMISC("RESULT_VAL")
 S ^TMP("LRVEHU",$J,"I")=ISIMISC("INITIALS")
 S ^TMP("LRVEHU",$J,"COLL")=ISIMISC("RESULT_DT")
 S ^TMP("LRVEHU",$J,"PT",DFN)=""
 K ISIMISC("ENTERED_BY"),ISIMISC("LAB_TEST"),ISIMISC("DFN"),ISIMISC("RESULT_VAL")
 K ISIMISC("INITIALS"),ISIMISC("LOCATION")
 K @NODE M @NODE=ISIMISC
 D EN^LRPARAM I $G(LREND) D KILL S ISIRC="-1^Invalid set up detected (ISIIMP13)" Q
 Q
 ;
LAB ;Add LAB for patient
 D EN Q:+ISIRC<0
 D STAT Q:+ISIRC<0
 S ISIRESUL(0)=1
 S ISIRESUL(1)="Success"
 S ISIRC=1
 Q
 ;
EN ;from LROR4
 K DIC,LRURG,LRSAME,LRCOM,LRNATURE,LRTCOM
 S LRORDR="WC"
 S LRORDTIM=""
 I $D(LRADDTST) Q:LRADDTST=""
 S LRFIRST=1,LRODT=DT,U="^",LRECT=0,LROUTINE=$P(^LAB(69.9,1,3),U,2)
 S:$G(LRORDRR)="R" LRECT=1,LRFIRST=0
 I LRORDR="SP" S LRLWC="SP"
 I LRORDR="WC" S LRLWC="WC"
L5 S Y=$$NOW^XLFDT S LRORDTIM=$P(Y,".",2),LRODT=$P(Y,".",1),X1=Y,X2=DT D ^%DTC ;;  JFR  def order time
 G KILL:$G(LRWP)<1!($G(LRWP)="")
 ;S LRWP=1
 S:'$D(^LRO(69,LRODT,0)) ^(0)=$P(^LRO(69,0),U,1,2)_U_LRODT_U_(1+$P(^(0),U,4)),^LRO(69,LRODT,0)=LRODT,^LRO(69,"B",LRODT,LRODT)=""
 S LRURG="",LRAD=DT,LRWPD=LRWP\2+(LRWP#2)
 S LRQUIET=1 
 D LRZORD1^ISIIMPL2
 Q
 ;
KILL D ^LRORDK,LROEND^LRORDK K ^TMP("LRVEHU",$J),^TMP("LRSTIK",$J)
 Q
 ;
STAT ;
 D EN^LRPARAM
 I '$D(LRLABKY) S ISIRC="-1^You do not have the proper security keys" Q
 ;
 S X=DUZ(2)
 I X<1 D END Q
 I X'=DUZ(2) N LRPL S LRPL=X
 ;
 S LRLONG="",LRPANEL=0,LROESTAT=""
 S %H=$H-60 D YMD^LRX S LRTM60=9999999-X
 S LRQUIET=1
 D LRZOE^ISIIMPL1 K LRTM60,LRLONG,LREND,LROESTAT
 ;
 D END
 Q
 ;
END K DIR,DIRUT,GOT
 D ^LRORDK,LROEND^LRORDK,STOP^LRCAPV
 Q