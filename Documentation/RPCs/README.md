# VistA Data Loader RPC Reference

## Overview
This directory contains documentation for all Remote Procedure Calls (RPCs) provided by the VistA Data Loader package (version 3.1). These RPCs enable external applications to import clinical and administrative data into VistA.

## Package Information
- **Package**: VISTA DATALOADER
- **Version**: 3.1
- **Build**: 70 (Dec 23, 2024)
- **Namespace**: ISI*
- **License**: Apache 2.0

## RPC Categories

### Patient Management
- [**ISI IMPORT PAT**](ISI-IMPORT-PAT.md) - Create patient records
- [**ISI IMPORT APPT**](ISI-IMPORT-APPT.md) - Create appointments and encounters
- [**ISI IMPORT ADMIT**](ISI-IMPORT-ADMIT.md) - Create inpatient admissions ⚠️ *Development*

### Clinical Data
- [**ISI IMPORT PROB**](ISI-IMPORT-PROB.md) - Create problem list entries
- [**ISI IMPORT VITALS**](ISI-IMPORT-VITALS.md) - Create vital signs measurements
- [**ISI IMPORT ALLERGY**](ISI-IMPORT-ALLERGY.md) - Create allergy/adverse reaction entries
- [**ISI IMPORT LAB**](ISI-IMPORT-LAB.md) - Create individual laboratory test results
- [**ISI IMPORT LAB PANEL**](ISI-IMPORT-LAB-PANEL.md) - Create laboratory panels

### Orders
- [**ISI IMPORT MED**](ISI-IMPORT-MED.md) - Create medication orders
- [**ISI IMPORT NONVA MED**](ISI-IMPORT-NONVA-MED.md) - Create non-VA medication entries
- [**ISI IMPORT CONSULT**](ISI-IMPORT-CONSULT.md) - Create consult requests
- [**ISI IMPORT RAD ORDER**](ISI-IMPORT-RAD-ORDER.md) - Create radiology orders

### Visit/Encounter CPT & Diagnosis
- [**ISI IMPORT V CPT**](ISI-IMPORT-V-CPT.md) - Create V CPT (procedure) entries
- [**ISI IMPORT V POV**](ISI-IMPORT-V-POV.md) - Create V POV (diagnosis) entries
- [**ISI IMPORT V EXAM**](ISI-IMPORT-V-EXAM.md) - Create V Exam entries
- [**ISI IMPORT V PATIENT ED**](ISI-IMPORT-V-PATIENT-ED.md) - Create V Patient Education entries
- [**ISI IMPORT HFACTOR**](ISI-IMPORT-HFACTOR.md) - Create V Health Factor entries
- [**ISI IMPORT IMMUNIZATIONS**](ISI-IMPORT-IMMUNIZATIONS.md) - Create V Immunization entries

### Documentation
- [**ISI IMPORT NOTE**](ISI-IMPORT-NOTE.md) - Create TIU/Progress notes

### Administrative
- [**ISI IMPORT USER**](ISI-IMPORT-USER.md) - Create user accounts
- [**ISI IMPORT TFL**](ISI-IMPORT-TFL.md) - Create Treating Facility List entries

### Templates
- [**ISI IMPORT GET TEMPLATES**](ISI-IMPORT-GET-TEMPLATES.md) - Fetch template list
- [**ISI IMPORT GET TEMPLATE DETLS**](ISI-IMPORT-GET-TEMPLATE-DETLS.md) - Fetch template details
- [**ISI IMPORT SAVE TEMPLATE**](ISI-IMPORT-SAVE-TEMPLATE.md) - Save template updates

### Utilities
- [**ISI IMPORT TABLEFETCH**](ISI-IMPORT-TABLEFETCH.md) - Export select VistA tables/files
- [**ISI IMPORT ICDFIND**](ISI-IMPORT-ICDFIND.md) - Fetch ICD code descriptions

## Quick Reference

| RPC Name | Entry Point | API | Purpose |
|----------|-------------|-----|---------|
| [ISI IMPORT PAT](ISI-IMPORT-PAT.md) | [PNTIMPRT^ISIIMPR1](../../VistA/Routines/ISIIMPR1.m) | [IMPORTPT^ISIIMP03](../../VistA/Routines/ISIIMP03.m) | Patient creation |
| [ISI IMPORT APPT](ISI-IMPORT-APPT.md) | [APPMAKE^ISIIMPR1](../../VistA/Routines/ISIIMPR1.m) | [APPOINT^ISIIMP04](../../VistA/Routines/ISIIMP04.m) | Appointments |
| [ISI IMPORT ADMIT](ISI-IMPORT-ADMIT.md) | [ADMIT^ISIIMPR3](../../VistA/Routines/ISIIMPR3.m) | [ADMIT^ISIIMP25](../../VistA/Routines/ISIIMP25.m) | Admissions ⚠️ |
| [ISI IMPORT PROB](ISI-IMPORT-PROB.md) | [PROBMAKE^ISIIMPR1](../../VistA/Routines/ISIIMPR1.m) | [PROBLEM^ISIIMP06](../../VistA/Routines/ISIIMP06.m) | Problem list |
| [ISI IMPORT VITALS](ISI-IMPORT-VITALS.md) | [VITMAKE^ISIIMPR1](../../VistA/Routines/ISIIMPR1.m) | [VITALS^ISIIMP08](../../VistA/Routines/ISIIMP08.m) | Vital signs |
| [ISI IMPORT ALLERGY](ISI-IMPORT-ALLERGY.md) | [ALGMAKE^ISIIMPR2](../../VistA/Routines/ISIIMPR2.m) | [ALLERGY^ISIIMP10](../../VistA/Routines/ISIIMP10.m) | Allergies |
| [ISI IMPORT LAB](ISI-IMPORT-LAB.md) | [LABMAKE^ISIIMPR2](../../VistA/Routines/ISIIMPR2.m) | [LAB^ISIIMP12](../../VistA/Routines/ISIIMP12.m) | Lab tests |
| [ISI IMPORT LAB PANEL](ISI-IMPORT-LAB-PANEL.md) | [LABPANEL^ISIIMPR2](../../VistA/Routines/ISIIMPR2.m) | [LAB^ISIIMP12](../../VistA/Routines/ISIIMP12.m) | Lab panels |
| [ISI IMPORT MED](ISI-IMPORT-MED.md) | [MEDMAKE^ISIIMPR2](../../VistA/Routines/ISIIMPR2.m) | [MEDS^ISIIMP16](../../VistA/Routines/ISIIMP16.m) | Medications |
| [ISI IMPORT NONVA MED](ISI-IMPORT-NONVA-MED.md) | [NVAMED^ISIIMPR2](../../VistA/Routines/ISIIMPR2.m) | [NVAMEDS^ISIIMP16](../../VistA/Routines/ISIIMP16.m) | Non-VA meds |
| [ISI IMPORT NOTE](ISI-IMPORT-NOTE.md) | [NOTEMAKE^ISIIMPR2](../../VistA/Routines/ISIIMPR2.m) | [NOTES^ISIIMP14](../../VistA/Routines/ISIIMP14.m) | TIU notes |
| [ISI IMPORT CONSULT](ISI-IMPORT-CONSULT.md) | [CONMAKE^ISIIMPR2](../../VistA/Routines/ISIIMPR2.m) | [CONSULTS^ISIIMP18](../../VistA/Routines/ISIIMP18.m) | Consults |
| [ISI IMPORT RAD ORDER](ISI-IMPORT-RAD-ORDER.md) | [RADOMAKE^ISIIMPR1](../../VistA/Routines/ISIIMPR1.m) | [RADORDER^ISIIMP20](../../VistA/Routines/ISIIMP20.m) | Rad orders |
| [ISI IMPORT USER](ISI-IMPORT-USER.md) | [USRCREAT^ISIIMPR1](../../VistA/Routines/ISIIMPR1.m) | [USER^ISIIMP22](../../VistA/Routines/ISIIMP22.m) | User accounts |
| [ISI IMPORT V CPT](ISI-IMPORT-V-CPT.md) | [VCPT^ISIIMPR3](../../VistA/Routines/ISIIMPR3.m) | [VCPT^ISIIMP27](../../VistA/Routines/ISIIMP27.m) | CPT codes |
| [ISI IMPORT V EXAM](ISI-IMPORT-V-EXAM.md) | [VEXAM^ISIIMPR3](../../VistA/Routines/ISIIMPR3.m) | [VEXAM^ISIIMP27](../../VistA/Routines/ISIIMP27.m) | Exams |
| [ISI IMPORT V POV](ISI-IMPORT-V-POV.md) | [VPOV^ISIIMPR3](../../VistA/Routines/ISIIMPR3.m) | [VPOV^ISIIMP27](../../VistA/Routines/ISIIMP27.m) | Diagnoses |
| [ISI IMPORT HFACTOR](ISI-IMPORT-HFACTOR.md) | [HFACTOR^ISIIMPR3](../../VistA/Routines/ISIIMPR3.m) | [VHF^ISIIMP27](../../VistA/Routines/ISIIMP27.m) | Health factors |
| [ISI IMPORT IMMUNIZATIONS](ISI-IMPORT-IMMUNIZATIONS.md) | [VIMMZ^ISIIMPR3](../../VistA/Routines/ISIIMPR3.m) | [VIMMZ^ISIIMP27](../../VistA/Routines/ISIIMP27.m) | Immunizations |
| [ISI IMPORT V PATIENT ED](ISI-IMPORT-V-PATIENT-ED.md) | [VPTEDU^ISIIMPR3](../../VistA/Routines/ISIIMPR3.m) | [VPNTED^ISIIMP27](../../VistA/Routines/ISIIMP27.m) | Patient ed |
| [ISI IMPORT TFL](ISI-IMPORT-TFL.md) | [TRTFACLS^ISIIMPR1](../../VistA/Routines/ISIIMPR1.m) | [TFL^ISIIMP28](../../VistA/Routines/ISIIMP28.m) | Facility list |
| [ISI IMPORT TABLEFETCH](ISI-IMPORT-TABLEFETCH.md) | [TABLEGET^ISIIMPR2](../../VistA/Routines/ISIIMPR2.m) | [ENTRY^ISIIMPUA](../../VistA/Routines/ISIIMPUA.m) | Table export |
| [ISI IMPORT ICDFIND](ISI-IMPORT-ICDFIND.md) | [ICD9GET^ISIIMPR2](../../VistA/Routines/ISIIMPR2.m) | [ICD9^ISIIMPUA](../../VistA/Routines/ISIIMPUA.m) | ICD lookup |
| [ISI IMPORT GET TEMPLATES](ISI-IMPORT-GET-TEMPLATES.md) | [FETCHTMP^ISIIMPUA](../../VistA/Routines/ISIIMPUA.m) | N/A | Fetch template list |
| [ISI IMPORT GET TEMPLATE DETLS](ISI-IMPORT-GET-TEMPLATE-DETLS.md) | [TEMPLATE^ISIIMPUA](../../VistA/Routines/ISIIMPUA.m) | N/A | Fetch template details |
| [ISI IMPORT SAVE TEMPLATE](ISI-IMPORT-SAVE-TEMPLATE.md) | [TMPUPDTE^ISIIMPR1](../../VistA/Routines/ISIIMPR1.m) | [TEMPLATE^ISIIMP24](../../VistA/Routines/ISIIMP24.m) | Save template updates |

## Routine Organization

### RPC Entry Points
- **ISIIMPR1**: Patient, Appointments, Problems, Vitals, Rad Orders, User, TFL, Templates
- **ISIIMPR2**: Allergies, Labs, Meds, Notes, Consults, Table Fetch, ICD Find
- **ISIIMPR3**: Admit, V-File entries (CPT, POV, Exam, Immunizations, Health Factors)

### API Implementation
- **ISIIMP##**: Individual API implementations (numbered 02-28)
  - 02: Patient (PATIENT^ISIIMP02)
  - 04: Appointments (APPOINT^ISIIMP04)
  - 06: Problems (PROBLEM^ISIIMP06)
  - 08: Vitals (VITALS^ISIIMP08)
  - 10: Allergies (ALLERGY^ISIIMP10)
  - 12: Labs (LAB^ISIIMP12)
  - 14: Notes (NOTES^ISIIMP14)
  - 16: Meds (MEDS^ISIIMP16, NVAMEDS^ISIIMP16)
  - 18: Consults (CONSULTS^ISIIMP18)
  - 20: Rad Orders (RADORDER^ISIIMP20)
  - 22: User (USER^ISIIMP22)
  - 24: Templates (TEMPLATE^ISIIMP24)
  - 25-26: Admit/Discharge (ADMIT^ISIIMP25, DISCHARG^ISIIMP26)
  - 27: V-File entries (VEXAM, VIMMZ, VCPT, VHF, VPOV, VPNTED)
  - 28: Treating Facility List (TFL^ISIIMP28)

### Utilities
- **ISIIMPU#**: Parameter parsing and validation utilities
  - ISIIMPU1: Patient utilities
  - ISIIMPU2: Appointment utilities
  - ISIIMPU4: Problem utilities
  - ISIIMPU5: Vitals utilities
  - ISIIMPU6: Allergy utilities
  - ISIIMPU7: Lab utilities
  - ISIIMPU8: Notes utilities
  - ISIIMPU9: Medication utilities
  - ISIIMPUA: File fetch utilities
  - ISIIMPUB: Consult utilities
  - ISIIMPUC: Rad order utilities
  - ISIIMPUD: User utilities
  - ISIIMPUE: Template utilities
  - ISIIMPUF: Admit utilities
  - ISIIMPUG: V-File (encounter) utilities
  - ISIIMPUH: Treating facility utilities
  - ISIIMPUI: Non-VA med utilities

### Error Handling
- **ISIIMPER**: Error processing routine

### Lab Spill-Over
- **ISIIMPL#**: Lab import spill-over routines (ISIIMPL1-ISIIMPL9)

### Unit Tests
- **ISIIMPLT**: Lab unit tests (not in KIDS build)
- **ISIIMPT1**: General unit tests (not in KIDS build)

## Common Input Format

All RPCs use a similar input structure:

```
MISC(1) = "PARAMETER1^VALUE1"
MISC(2) = "PARAMETER2^VALUE2"
MISC(3) = "PARAMETER3^VALUE3"
...
```

## Common Output Format

### Success
```
ISIRESUL(0) = 1  (or positive value)
ISIRESUL(1) = IEN or success message
```

### Error
```
ISIRESUL(0) = -1^ERROR_MESSAGE
```

## Parameter Types

- **FIELD**: Literal value to import directly into VistA field
- **PARAM**: Internal parameter used for processing logic
- **MASK**: Dynamic value with asterisk (*) wildcard for random generation
- **MULTIPLE**: Array of values (e.g., symptoms delimited by "|")

## General Workflow

1. **Receive Input**: RPC receives MISC array from client
2. **Parse Parameters**: Utility routine converts to indexed ISIMISC array
3. **Validate Input**: Validation routine checks all parameters
4. **Execute API**: API routine performs the actual data creation
5. **Return Result**: Success/failure returned in ISIRESUL array

## Key VistA Files Referenced

| File # | File Name | Used By |
|--------|-----------|---------|
| #2 | PATIENT | All patient-related RPCs |
| #44 | HOSPITAL LOCATION | Appointments, Vitals, Labs |
| #60 | LABORATORY TEST | Labs |
| #200 | NEW PERSON | Provider/User references |
| #9000010 | OUTPATIENT ENCOUNTER | V-File entries |
| #9000011 | PROBLEM | Problems |
| #120.5 | GMRV VITAL MEASUREMENT | Vitals |
| #120.8 | ADVERSE REACTION TRACKING | Allergies |
| #8925.1 | TIU DOCUMENT DEFINITION | Notes |

## Development Status Warnings

⚠️ **The following APIs are marked as "still in development":**
- ISI IMPORT ADMIT (ADMIT^ISIIMP25)
- DISCHARGE^ISIIMP26

Use these with caution in production environments.

## Version History

- **V.1.0** (June 2012): Initial implementation by Johns Hopkins University
- **V.2.0** (June 2014): Updates by University of Michigan
- **V.2.1** (Nov 2014): QRDA support by Oroville Hospital
- **V.2.2-2.5** (2015): Incremental updates and bug fixes
- **V.3.0** (2018): Merge updates, Apache 2.0 license
- **V.3.1** (2024-2025): Lab Panel API, continued maintenance by DocMe360 LLC

## Credits

This software package leverages utilities originally developed for the "CAMP MASTER" VistA training system. Component authors include:
- LAB utility (LRZORD, LRZORD1, LRZOE, LRZOE2, LRZVER*): DALOI/CJS, NTEO/JFR
- VITAL utility (ZGMRVPOP): SLC/DAN
- PATIENT utility (ZVHDPT): DALOI/RM
- PROBLEM utility (ZVHGMPL): NTEO/JFR
- APPOINTMENTS utility (ZVHZSDM): SLC/DAN

## Support & Contact

For questions or collaboration:
- Mike Stark: starklogic@gmail.com
- ISI GROUP, LLC, Bethesda, MD

## License

Licensed under the Apache License, Version 2.0
http://www.apache.org/licenses/LICENSE-2.0

## Important Notice

**NOT FOR PRODUCTION CLINICAL USE**

This software has NOT been designed, coded, or tested for use in any clinical or production setting. It should be considered a work in progress suitable for development and training environments only.

## Related Projects

- **VistA FHIR Data Loader**: https://github.com/WorldVistA/VistA-FHIR-Data-Loader
  - Synthea-based FHIR data import using these RPCs
  
## See Also

- [API_Detail.txt](../API_Detail.txt) - Original API documentation
- [Declarations_API_and_RPC.txt](../Declarations_API_and_RPC.txt) - RPC declarations
- [DataLoader_User_Setup.txt](../DataLoader_User_Setup.txt) - Setup instructions
