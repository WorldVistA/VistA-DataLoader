ISIIMP16 ;ISI GROUP/MLS -- MEDS Import API
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
MEDS(ISIRESUL,ISIMISC)       
 N ERR,VAL
 N:'$G(ISIPARAM("DEBUG")) ISIPARAM
 K ISIRESUL S (ISIRESUL(0),ISIRC)=0
 ;
 ;Validate setup & parameters
 S ISIRC=$$VALIDATE^ISIIMP17 Q:+ISIRC<0 ISIRC
 ;Create patient record
 S ISIRC=$$MAKEMEDS^ISIIMP17 Q:+ISIRC<0 ISIRC
 Q ISIRC
 ;
NVAMEDS(ISIRESUL,ISIMISC)       
 N ERR,VAL
 N:'$G(ISIPARAM("DEBUG")) ISIPARAM
 K ISIRESUL S (ISIRESUL(0),ISIRC)=0
 ;
 ;Validate setup & parameters
 S ISIRC=$$VALIDATE^ISIIMP29 Q:+ISIRC<0 ISIRC
 ;Create patient record
 S ISIRC=$$MAKENVA^ISIIMP29 Q:+ISIRC<0 ISIRC
 Q ISIRC