ISIIMPL6 ;ISI GROUP/MLS -- LABS IMPORT CONT.
 ;;3.0;ISI_DATA_LOADER;;Jun 26, 2019;Build 59
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
LRZVER2 ;SLC/CJS - LAB ROUTINE DATA VERIFICATION ; 12/7/06 08:51
 ;;5.2;LAB SERVICE;**153,201**;Sep 27, 1994
 S LRSPEC="",X=DUZ D DUZ^LRX S:'$D(LREAL) LREAL=1
V3 G:$D(^LR(LRDFN,LRSS,LRIDT,0)) V5 G:"AP EM"[LRSS V4
V3A IF LRSAMP'="" S LRSPEC=$P(^LAB(62,LRSAMP,0),U,2) G:$D(^LAB(61,+LRSPEC,0)) V4
 I LRDPF'=62.3 Q:$D(LRGVP)  S DIC="^LAB(61,",DIC(0)="AEOQ" D ^DIC S LRSPEC=+Y IF LRSPEC=-1 Q ;***MLS MOD. *** W !,"The specimen MUST be defined." Q
V4 I '$D(^LR(LRDFN,LRSS,0)) S ^LR(LRDFN,LRSS,0)=U_$P(^DD(63,$O(^DD(63,"GL",LRSS,0,0)),0),U,2)_U
 L +^LR(LRDFN,LRSS) S ^LR(LRDFN,LRSS,0)=$P(^LR(LRDFN,LRSS,0),U,1,2)_U_LRIDT_U_(1+$P(^(0),U,4))
 IF "AP EM"[LRSS S ^LR(LRDFN,LRSS,LRIDT,0)=LRCDT_U_LREAL L -^LR(LRDFN,LRSS) G V5
 S LRVOL="" S:$D(^LRO(69,LRODT,1,LRSN,1)) LRVOL=$P(^(1),U,5) S ^LR(LRDFN,LRSS,LRIDT,0)=LRCDT_U_LREAL_U_U_U_LRSPEC_U_LRAN_U_LRVOL_U_LRMETH_U L -^LR(LRDFN,LRSS)
V5 I LRDPF=62.3 S LRSPEC=$S($D(^LR(LRDFN,LRSS,LRIDT,0)):$P(^(0),U,5),1:"")
 S LRLDT=LRIDT,LRVF=0 G V6:'$L($P(^LR(LRDFN,LRSS,LRIDT,0),U,3)) S LRVF=1,X=$P(^(0),U,4),T=$P(^(0),U,3)
 ;***MLS MOD.*** W:'X&(LRDPF=62.3) !,"This control has been automatically verified" W:'X&(LRDPF'=62.3) !,"Verified"
 ;***MLS MOD.*** I X W !,"These results have been approved by ",$S($D(^VA(200,X,0)):$P(^(0),"^",1),1:"Unknown"),!," on ",$$FMTE^XLFDT(T)
V6 I LRDPF'=62.3 S LRSPEC=$P(^LR(LRDFN,LRSS,LRIDT,0),U,5) G:'+LRSPEC V3A
 ;W:$D(^LAB(61,+LRSPEC,0)) !,"Specimen: ",$P(^(0),U) ; JFR comment
 K LRNOVER I LRSS="CH",'LRVF S LRNOVER=""
 K ^TMP("LR",$J,"VTO") S LRCFL="" D ^ISIIMPL7 ;D ^LRZVER3
 K LRSA,LRSB,LRNOVER,LRACC,LRCAPWA,Y,Z,Z1,Z2,K1,LRURG
 K DA,DIC,DIE,LRNG,LRNG2,LRNG3,LRNG4,LRNG5,LREDIT,LREXEC,DR
 Q  ;LEAVE LRVER2, BACK TO LRVER1
V7 ;from LRVER3
 S LRLDT=$O(^LR(LRDFN,LRSS,LRLDT)) G V8:LRLDT<1 S:LRLDT>LRTM60 LRLDT=-1 G V8:LRLDT=-1,V7:'$D(^LR(LRDFN,LRSS,LRLDT,0)) D V9 G:$P(^LR(LRDFN,LRSS,LRLDT,0),U,5)'=LRSPEC!'$P(^(0),U,3)!'$D(LRMA) V7
V8 S LRDAT(2)="",Z2="" I LRLDT>0 S Z2=^LR(LRDFN,"CH",LRLDT,0),X=+Z2,Z=Z2 D DAT S LRDAT(2)=LRDAT
 S Z1=^LR(LRDFN,"CH",LRIDT,0),X=+Z1,Z=Z1 D DAT
 Q
DAT N LRX
 S LRX=$$FMTE^XLFDT(X,"5M")
 S LRDAT=$P(LRX,"/",1,2)_" "_$P(LRX,"@",2)_$S($P(Z,U,2)!(X'["."):"r",1:"d") Q
V9 K LRMA S I=0 F  S I=$O(^TMP("LR",$J,"TMP",I)) Q:I<1  I $D(^LR(LRDFN,LRSS,LRLDT,I)) S LRMA=1 Q
 Q
