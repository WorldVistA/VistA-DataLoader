ISIIMPU3 ;ISI GROUP/MLS -- Data Import Utility
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
CHNGNAME(DFN,NAME)
 ;Additonal utility for patient import called by ISIIMP03
 N MSG,FDA,tempFILE,tempFIELD,tempDFN
 K FDA
 S tempDFN=DFN
 S tempFILE="2"
 S tempFIELD=".01"
 S FDA(tempFILE,DFN_",",tempFIELD)=NAME
 D FILE^DIE("K","FDA","MSG")
 I $D(MSG) Q "-1^"_MSG
 Q 1
 ;
ADDALIAS(DFN,ALIAS)     
 ;Additonal utility for patient import called by ISIIMP03
 N MSG,FDA,tempFILE,tempFIELD,tempDFN
 k FDA
 S tempDFN=DFN
 s tempFILE="2"
 s tempFIELD=".01"
 s FDA(42,2.01,"+1,"_tempIEN_",",.01)=ALIAS
 d UPDATE^DIE("","FDA(42)","","MSG")
 I $D(MSG) Q "-1^"_MSG
 Q 1
 ;
CHNGUSER(IEN,NAME)
 ;Additonal utility for User import called by ISIIMP22
 N MSG,FDA,tempFILE,tempFIELD
 Q:'$D(^VA(200,IEN,0))
 K FDA
 S tempFILE="200"
 S tempFIELD=".01"
 S FDA(tempFILE,IEN_",",tempFIELD)=NAME
 D FILE^DIE("K","FDA","MSG")
 I $D(MSG) Q "-1^"_MSG
 Q 1
 