ISIIMPL4 ;ISI GROUP/MLS -- LAB IMPORT CONT.
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
LRZOE2
 ;Formerly a part of LROE1
Q15 ;from LROE1
 Q:'$D(^LRO(69,LRODT,1,LRSN,0))
 I $D(^LRO(69,LRODT,1,LRSN,1)),$P(^(1),"^",4)="U" S ^(1)=LRTIM_"^^"_DUZ,DA=LRSN,DA(1)=LRODT,DIE="^LRO(69,"_DA(1)_",1,",DR=16 D ^DIE ;MLS
 I M9>1 D LRSPEC^LROE1 S S1=$S($D(^LAB(61,+LRSPEC,0)):$P(^(0),U),1:""),S2=$P(^LAB(62,LRSAMP,0),U),S4=$P(^(0),U,3),S3=S1_$S(S1'=S2:"  "_S2,1:"") ;W !,"Do you have the  ",S3,"  ",S4 K S1,S2,S3,S4 S %=2 D YN^DICN G Q15:%=0 Q:%'=1
 S DA=DT,LRDFN=+^LRO(69,LRODT,1,LRSN,0),LRDPF=+$P(^LR(LRDFN,0),U,2)
 IF '$D(^LRO(69,LRODT,1,LRSN,1)) S LRSTATUS="C",DA=LRODT I '$D(LRSND) D P15^LROE1 Q:LRCDT<1
 I $D(LRSND),$P(^LRO(69,LRODT,1,LRSN,0),U,4)="LC",$D(^(1)) S LRLLOC=$P(^(0),U,7),LROLLOC=$P(^(0),U,9),LRNT=$S($D(LRNT):LRNT,$D(LRTIM):LRTIM,$D(LRCDT):+LRCDT,1:"") D P15^LRPHITEM G PH
 I $D(LRSND) N COMB S COMB=$P($G(^LRO(69,LRODT,1,LRSN,1)),"^",7) S ^LRO(69,LRODT,1,LRSN,1)=LRTIM_"^"_LRUN_"^"_DUZ_"^"_LRSTATUS_"^^^"_COMB_"^"_DUZ(2) S:LRSTATUS="C" ^LRO(69,"AA",+$G(^LRO(69,LRODT,1,LRSN,.1)),LRODT_"|"_LRSN)=""
PH G Q16:LRORD D ORDER^LROW2 G Q16A
Q16 S J=0 D CHECK^LROW2 I J D BAD^LROW2
Q16A I $D(LRLONG),$D(LRSND) S LRSN=LRSND,^TMP("LROE",$J,"LRORD")=LRORD_U_LRODT_U_LRTIM_U_PNM_U_SSN
 K DR S LRTSTS=0
 S LRSN=0 F  S LRSN=$O(LRSN(LRSN)) Q:'LRSN  D Q17
 I $D(LRLONG),$D(LRSND) S LRSN=LRSND D LROE S X=^TMP("LROE",$J,"LRORD"),LRORD=+X,LRODT=$P(X,"^",2),LRTIM=$P(X,"^",3),LRLONG="",PNM=$P(X,"^",4),SSN=$P(X,"^",5)
 Q
Q17 S I=$O(^LRO(69,LRODT,1,LRSN,6,0)),J=$O(^(1)) S:'$D(IOM) IOM=80 K LRSPCDSC S:J LRSPCDSC=^(J,0) S:I DA=LRSN,DA(1)=LRODT,DR=6,DIC="^LRO(69,"_LRODT_",1," D EN^DIQ:I D LRSPEC^LROE1
 D OLD^LRORDST K ^TMP("LR",$J,"TMP")
 S $P(^LRO(69,LRODT,1,LRSN,1),U,4)="C",$P(^LRO(69,LRODT,1,LRSN,1),U,8)=DUZ(2),^LRO(69,"AA",+$G(^LRO(69,LRODT,1,LRSN,.1)),LRODT_"|"_LRSN)=""
 Q
LROE ;from LROE1  ;;;;JFR copied from LRFAST to reduce mods
 S LRLLOC=$P(^LRO(69,LRODT,1,LRSN,0),U,7) S:'$L(LRLLOC) LRLLOC=0 K LROE
 S I1=0 F  S I1=$O(^LRO(69,LRODT,1,LRSN,2,I1)) Q:I1<1!($G(LREND))  S X=^(I1,0) I $P(X,U,4) S LRAA=$P(X,U,4),LRAN=$P(X,U,5),LRAD=$P(X,U,3) I '$D(LROE(LRAD_LRAA_LRAN)) S LROE(LRAD_LRAA_LRAN)="" D LROE1
 G QUIT^LRFAST
 ;
 ;
LROE1 S LRX=$G(^LRO(68,LRAA,0))
 S LRIDIV=$S($L($P(LRX,U,19)):$P(LRX,U,19),1:"CP")
 D:$P(LRPARAM,U,14)&($P($G(^LRO(68,LRAA,0)),U,16)) ^LRCAPV
 I $G(LREND) Q
 ; Check for different performing lab.
 I $G(LRPL) N LRDUZ S LRDUZ(2)=LRPL
 ;
 S LRUID=$P(^LRO(68,LRAA,1,LRAD,1,LRAN,.3),"^")
 I $P(LRX,U,2)="CH" D ^ISIIMPL5 ;D ^LRZVER1
 K LRX
 Q
 ;
