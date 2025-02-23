ISIIMPL2 ;ISI GROUP/MLS -- LABS IMPORT UTILITY;2025/01/13
 ;;3.1;VISTA DATALOADER;;Dec 23, 2024;Build 70
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
LRZORD1 ;
 ;
L2 Q:$G(LREND)
 K LROT,LRSAME,LRKIL,LRGCOM,LRCCOM,LR696IEN,LRNATURE,DGSENFLG
 S LRWPC=LRWP G:$D(LROR) LRFIRST
 ;I '$D(LRADDTST) K DFN,DIC S PNM="",DIC(0)="EMQ"_$S($P(LRPARAM,U,6)&$D(LRLABKY):"L",1:"") W ! D ^LRDPA I (LRDFN=-1)!$D(DUOUT)!$D(DTOUT) Q
 K DFN,DIC,X
 S X="`"_LRZPT
 S PNM="",DIC(0)="MQ",DGSENFLG=1 D EN^LRDPA ; JFR must default patient
 I '+DFN G DROP
 I $D(LRADDTST),LRADDTST="" Q
 S LRORDR="WC"
 S:'$D(LREND) LREND=0 I LRORDR="" S LRLWC="SP" ;D COLTY^LRWU G DROP:LREND
 ;B
 S LRDPF=$P(^LR(LRDFN,0),U,2),DFN=$P(^(0),U,3)
Q12 D LOC
Q11 D PRAC G DROP:LREND
 K T,TT,LRDMAX,LRDTST,LRTMAX
 S DA=0
 F  S DA=$O(^LRO(69,LRODT,1,"AA",LRDFN,DA)) Q:DA<1  I $S($D(^LRO(69,LRODT,1,DA,1)):$P(^(1),U,4)'="U",1:1) S S=$S($D(^LRO(69,LRODT,1,DA,4,1,0)):+^(0),1:0) D
 . S I=0 F  S I=$O(^LRO(69,LRODT,1,DA,2,I)) Q:I<1  I $D(^(I,0)) S T(+^(0),DA)=S,X=+^(0) S:'$D(TT(X,S)) TT(X,S)=0 S TT(X,S)=TT(X,S)+1
 K DIC
 I $D(LRADDTST) S LRORD=+LRADDTST,LRADDTST="" G LRFIRST
 D ORDER^LROW2
 ;
LRFIRST S LRSX=1 G Q13:'LRFIRST!(LRWP<2)
 ;W !,"Choose one (or more, separated by commas)  ('*' AFTER NUMBER TO CHANGE URGENCY) " ;;MLS
 F I=1:1:LRWPD D
 . N X
 . S X=^TMP("LRSTIK",$J,"B",I)
 . ;W !,X,?4,$P(^TMP("LRSTIK",$J,X),U,2) ;;MLS
 . S X=$G(^TMP("LRSTIK",$J,"B",I+LRWPD))
 . ;I X W ?39," ",X,?44,$P(^TMP("LRSTIK",$J,X),U,2)
Q13 S LREDO=0
LEDI ;
 ;
 ; If LEDI accessioning then check for pending orders in file #69.6
 I $G(LRRSTAT)="I",$G(LRRSITE("SMID"))'="",$G(LRSD("RUID"))'="" D  I $O(LROT(0)) G BAR
 . D EN^LRORDB(LRSD("RUID"),LRRSITE("SMID"))
 G:LRWP'>1 Q13A
 ;W ! W:'LRFIRST "'?' for list,  "
 S LRFIRST=0
 ;R "TEST number(s): ",LRSX:DTIME S:LRSX["?" LRFIRST=1 G LRFIRST:LRFIRST
 S LRFIRST=1 ;MLS
 S LRSX=^TMP("LRSTIK",$J,"B","") ;MLS
 I LRSX=""!(LRSX["^") G BAR
 F I=1:1:$L(LRSX,",") D  Q:LREDO
 . S LRSSX=$P(LRSX,",",I)
 . I LRSSX'?1.3N.1"*" S LREDO=1 Q
 . S LRSSX=$P(LRSSX,"*")
 . I '$D(^TMP("LRSTIK",$J,LRSSX)) S LREDO=1
Q13A I LREDO G Q13 ;MLS
 F LRK=1:1 S LRSSX=$P(LRSX,",",LRK) Q:LRSSX=""  D
 . N X
 . S LRST=$S(LRSSX["*":1,1:0),LRSSX=+LRSSX
 . S X=^TMP("LRSTIK",$J,LRSSX)
 . S LRSAMP=$P(X,U,3),LRSPEC=$P(X,U,5),LRTSTS=+X
 . D Q20^LRORDD
BAR ;S LRM=LRWPC+1,K=0 W !,"Other tests? N//" D % G Q14:'(%["Y")
LRM ;D MORE^LRORD2
 ; JFR - changed following to use stuff LRNATURE
Q14 ;D:$P(LRPARAM,U,17) ^LRORDD D ^LRORD2A    ;JFR  testing the max order stuff
 S LRNATURE="4^SERVICE CORRECTION^99ORN",%=1
 G LRM:'$D(%)&($D(LROT)'=11),DROP:$O(LROT(-1))="",LRM:'$D(%),DROP:%[U K DIC G DROP:'$D(LROT)!(%["N")
 ;W !!,"LAB Order number: ",LRORD,!!
 S ^TMP("LRVEHU",$J,LRORD)=""
 S LRZORD=LRORD
 I LRECT D  G DROP:LRCDT<1
 . I $G(LRORDRR)="R",$G(LRSD("CDT")) D  Q
 . . S LRCDT=LRSD("CDT")_"^"
 . . S LRORDTIM=$P(LRSD("CDT"),".",2)
 . . I 'LRORDTIM S $P(LRCDT,"^",2)=1
 . D TIME^LROE
 . I LRCDT<1 Q
 . S LRORDTIM=$P(Y,".",2)
 D NOW^%DTC S LRNT=% S:'LRECT LRCDT=LRNT_"^1"
 S LRIDT=9999999-LRCDT
 ; DOC360ME/SMH - From this code, we cannot input required comments. So just nuke that out from the LROT array
 ; See SETLROT+6^LRORDB
 K LROT(LRSAMP,LRSPEC,1,2)
 ; END change DOC360ME/SMH
 D ^LRORDST Q:$D(LROR)
 Q
% S %="Y" ;***MLS MOD.*** R %:DTIME Q:%=""!(%["N")!(%["Y")  W !,"Answer 'Y' or 'N': " G %
 ;
Q20A ;from LRORD2
MAX ; CHECK FOR MAXIUM ORDER FREQUENCY
 I $D(TT(LRTSTS,LRSPEC)),$D(^LAB(60,LRTSTS,3,"B",LRCS(LRCSN))) D EN2^LRORDD I %'["Y" Q
 S I7=0 F I9=0:0 S I9=$O(T(LRTSTS,I9)) Q:I9=""  I $D(^LAB(60,LRTSTS,3,+$O(^LAB(60,LRTSTS,3,"B",LRSAMP,0)),0)),+$P(^(0),U,5),LRSPEC=T(LRTSTS,I9) S I7=1
 I I7 S LRSN=0 F  S LRSN=$O(T(LRTSTS,LRSN)) Q:LRSN<1  S LRZT=LRTSTS D ORDER^LROS S LRTSTS=LRZT ;MLS
 I I7 S %="Y" Q  ;D % ;MLS
 Q
 ;
URGG ;W !,"For ",$P(^TMP("LRSTIK",$J,LRSSX),U,2)
 D URG^LRORD2
 Q
 ;
 ;
DROP ;W !!,"ORDER CANCELED",$C(7),!!
 Q:$D(LROR)  G L2
 ;
 ;
MICRO ;
 Q
 ;
LOC ;get pt. location, called by LRPDA1
 N %
 I +LRDPF=LRDPF S LRDPF=LRDPF_^DIC(LRDPF,0,"GL")
 S LREND=0,LRCAPLOC="Z"
 I $G(LRLLOC)="" I $D(^LR(LRDFN,.1)) S LRLLOC=^(.1)
 ;S X="`23" ; JFR  whatever location var we use ;MLS
 ;S X="'"_LRLLOC ;MLS
 S X="`"_LRLLOC ;DHP/ART
 K DIC S DIC("S")="I '$G(^(""OOS""))"
 S LROLLOC="",DIC=44,DIC(0)="MOQZ" S:X="" X=LRLLOC D ^DIC K DIC G LOC:X["?"
 S:Y>0 LROLLOC=+Y,LRLLOC=$P(Y(0),U,2),LRCAPLOC=$S($L($P(Y(0),U,3)):$P(Y(0),U,3),1:LRCAPLOC)
 I $L(LRLLOC) S ^LR(LRDFN,.1)=LRLLOC
 S ^LR(LRDFN,.092)=LRCAPLOC
 K LRIA,LRRA I $D(^SC(+Y,"I")) S LRIA=+^("I"),LRRA=$P(^("I"),U,2)
 K DIC,LRIA,LRRA,% Q
 S LREND=1 K DIC,LRIA,LRRE,%
 Q
PRAC ;
 S LRPRAC=DUZ Q  ;MLS
 Q
QUIT S LREND=1 Q
