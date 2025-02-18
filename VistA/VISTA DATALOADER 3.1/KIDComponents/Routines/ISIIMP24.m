ISIIMP24 ;ISI GROUP/MLS - Template Edit
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
TEMPLATE(ISIRESUL,ISIMISC)
 N ERR,VAL
 K ISIRESUL S (ISIRESUL(0),ISIRC)=0
 ;Validate setup & parameters
 S ISIRC=$$VALIDATE() Q:+ISIRC<0 ISIRC
 ;Create patient record
 S ISIRC=$$SAVE(.ISIMISC) Q:+ISIRC<0 ISIRC
 ; Quit with DFN
 Q ISIRC
 ;
VALIDATE()
 ;
 D:$G(ISIPARAM("DEBUG"))>0
 . W !,"+++Template merged params+++",!
 . I $D(ISIMISC) S X="" F  S X=$O(ISIMISC(X)) Q:X=""  W !,"ISIMISC("_X_")=",$G(ISIMISC(X))
 . W !,"<HIT RETURN TO PROCEED>" R X
 . Q
 ;
 ; Validate import array contents
 S ISIRC=$$VALIDATE^ISIIMPUE(.ISIMISC)
 Q ISIRC
 ;
SAVE(ISIMISC)
 ; Input - ISIMISC(ARRAY)
 ; Format:  ISIMISC(PARAM)=VALUE
 ;     eg:  ISIMISC("NAME")="FIRST,LAST"
 ;
 ; Output - ISIRC [return code]
 ;
 D:$G(ISIPARAM("DEBUG"))>0
 . W !,"+++ISIMISC array before SAVE^ISIIMP24+++",!
 . I $D(ISIMISC) S X="" F  S X=$O(ISIMISC(X)) Q:X=""  W !,"ISIMISC("_X_")=",$G(ISIMISC(X))
 . W !,"<HIT RETURN TO PROCEED>" R X
 . Q
 ;
 N ZIEN,ZNAME,ZTYPE,ZNAMEMSK,ZSSNMSK,ZSEX,ZEDOB,ZLDOB,ZMSTAT,ZZIPMSK
 N ZPHMSK,ZCITY,ZSTATE,ZVET,ZDFN,ZEMPLOY,ZSERV,ZEMAIL,ZUSER,ZESIG
 N ZACC,ZVER
 ;
 S (ZIEN,ZNAME,ZTYPE,ZNAMEMSK,ZSSNMSK,ZSEX,ZEDOB,ZLDOB,ZMSTAT,ZZIPMSK)=""
 S (ZPHMSK,ZCITY,ZSTATE,ZVET,ZDFN,ZEMPLOY,ZSERV,ZEMAIL,ZUSER,ZESIG,ZACC,ZVER)=""
 ;
 S ZIEN=0
 ;
 S ISIRC=0,ISIRESUL(0)=0
 ;
 S ZNAME=$G(ISIMISC("NAME"))
 I $D(^ISI(9001,"B",ZNAME)) S ZIEN=$O(^ISI(9001,"B",ZNAME,""))
 I ZIEN D UPDATE
 ;I 'ZIEN D NEW
 ;
 Q ISIRC
 ;
UPDATE ;
 N FDA,IENS,MSG K FDA
 S ISIRC=0
 S IENS=ZIEN_","
 D POPLIST
 D MKARRY
 D FILE^DIE("E","FDA","MSG")
 I $G(DIERR) S ISIRC="-1^"_$G(ERR("DIERR",1,"TEXT",1))
 Q
 ;
NEW ;
 N FDA,IENS K FDA
 S IENS="+1,"
 S ISIRC=0
 D POPLIST
 D MKARRY
 D UPDATE^DIE("E","FDA",,"MSG")
 I $G(DIERR)'="" S ISIRC="-1^"_$G(ERR("DIERR",1,"TEXT",1))
 Q ISIRC
 ;
POPLIST ;
 ;
 S ZNAME=$G(ISIMISC("NAME"))
 S ZTYPE=$G(ISIMISC("TYPE"))
 S ZNAMEMSK=$G(ISIMISC("NAME_MASK"))
 S ZSSNMSK=$G(ISIMISC("SSN_MASK"))
 S ZSEX=$G(ISIMISC("SEX"))
 S ZEDOB=$G(ISIMISC("EDOB"))
 S ZLDOB=$G(ISIMISC("LDOB"))
 S ZMSTAT=$G(ISIMISC("MARITAL_STATUS"))
 S ZZIPMSK=$G(ISIMISC("ZIP_MASK"))
 S ZPHMSK=$G(ISIMISC("PH_NUM"))
 S ZCITY=$G(ISIMISC("CITY"))
 S ZSTATE=$G(ISIMISC("STATE"))
 S ZVET=$G(ISIMISC("VETERAN"))
 S ZDFN=$G(ISIMISC("DFN_NAME"))
 S ZEMPLOY=$G(ISIMISC("EMPLOY_STAT"))
 S ZSERV=$G(ISIMISC("SERVICE"))
 S ZEMAIL=$G(ISIMISC("EMAIL_MASK"))
 S ZUSER=$G(ISIMISC("USER_MASK"))
 S ZESIG=$G(ISIMISC("ESIG_APND"))
 S ZACC=$G(ISIMISC("ACCESS_APND"))
 S ZVER=$G(ISIMISC("VERIFY_APND"))
 Q
 ;
MKARRY
 ;
 S FDA(9001,IENS,.01)=ZNAME
 I $G(ZTYPE)'="" S FDA(9001,IENS,1)=ZTYPE
 I $G(ZNAMEMSK)'="" S FDA(9001,IENS,2)=ZNAMEMSK
 I $G(ZSSNMSK)'="" S FDA(9001,IENS,4)=ZSSNMSK
 I $G(ZSEX)'="" S FDA(9001,IENS,5)=ZSEX
 I $G(ZEDOB)'="" S FDA(9001,IENS,6)=ZEDOB
 I $G(ZLDOB)'="" S FDA(9001,IENS,7)=ZLDOB
 I $G(ZMSTAT)'="" S FDA(9001,IENS,8)=ZMSTAT
 I $G(ZZIPMSK)'="" S FDA(9001,IENS,9)=ZZIPMSK
 I $G(ZPHMSK)'="" S FDA(9001,IENS,10)=ZPHMSK
 I $G(ZCITY)'="" S FDA(9001,IENS,11)=ZCITY
 I $G(ZSTATE)'="" S FDA(9001,IENS,12)=ZSTATE
 I $G(ZVET)'="" S FDA(9001,IENS,13)=ZVET
 I $G(ZDFN)'="" S FDA(9001,IENS,14)=ZDFN
 I $G(ZEMPLOY)'="" S FDA(9001,IENS,15)=ZEMPLOY
 I $G(ZSERV)'="" S FDA(9001,IENS,16)=ZSERV
 I $G(ZEMAIL)'="" S FDA(9001,IENS,17)=ZEMAIL
 I $G(ZUSER)'="" S FDA(9001,IENS,18)=ZUSER
 I $G(ZESIG)'="" S FDA(9001,IENS,19)=ZESIG
 I $G(ZACC)'="" S FDA(9001,IENS,20)=ZACC
 I $G(ZVER)'="" S FDA(9001,IENS,21)=ZVER
 Q
