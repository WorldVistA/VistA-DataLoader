ISIIMPER ;ISI GROUP/MLS -- ERROR PROCESSING;2025-01-13
 ;;3.1;VISTA DATALOADER;;Dec 23, 2024
 ;
 ; VistA Data Loader 3.1
 ;
 ; Copyright (C) 2012 Johns Hopkins University
 ; Copyright (C) 2025 DocMe360 LLC
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
ERR ;
 S ISIRESUL(0)="-1^ERROR "_$$EC^%ZOSV
 S (ISIRC,ISIRESUL)=ISIRESUL(0)
 D ^%ZTER
 G UNWIND^%ZTER
