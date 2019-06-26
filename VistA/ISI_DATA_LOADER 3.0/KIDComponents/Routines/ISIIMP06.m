ISIIMP06 ;ISI GROUP/MLS -- Problem Import API
 ;;3.0;ISI_DATA_LOADER;;Jun 26, 2019
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
PROBLEM(ISIRESUL,ISIMISC)
 ;Validate input array
 S ISIRC=$$VALIDATE^ISIIMP07 Q:+ISIRC<0 ISIRC
 ;
 D:$G(ISIPARAM("DEBUG"))>0
 . W !,"+++Post validated/updated array+++ (06)"
 . I $D(ISIMISC) W $G(ISIMISC) S X="" F  S X=$O(ISIMISC(X)) Q:X=""  W !,ISIMISC(X)
 . Q
 ;
 ;Create Appointment
 S ISIRC=$$MAKEPROB^ISIIMP07
 Q ISIRC
 ;
