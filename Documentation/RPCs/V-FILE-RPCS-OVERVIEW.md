# V-File RPCs Overview

## Description
The V-File RPCs create entries in VistA's encounter-related "V" files. These files store visit-level clinical data including CPT codes, diagnoses, health factors, immunizations, exams, and patient education. All V-File RPCs share common encounter resolution logic.

## Common V-File RPCs

- [ISI IMPORT V CPT](#isi-import-v-cpt) - CPT procedure codes
- [ISI IMPORT V POV](#isi-import-v-pov) - Purpose of Visit (diagnoses)
- [ISI IMPORT HFACTOR](#isi-import-hfactor) - Health factors
- [ISI IMPORT IMMUNIZATIONS](#isi-import-immunizations) - Immunizations
- [ISI IMPORT V EXAM](#isi-import-v-exam) - Physical exam findings
- [ISI IMPORT V PATIENT ED](#isi-import-v-patient-ed) - Patient education

## Common Entry Points
- **RPC Entry**: Various in ISIIMPR3
- **API Entry**: Various in ISIIMP27
- **Utility**: ISIIMPUG (shared validation)

## Common Parameters

All V-File RPCs share these base parameters:

| Parameter | Type | Required | Description |
|-----------|------|----------|-------------|
| **PAT_SSN** | FIELD | Yes | PATIENT SSN or DFN |
| **PROVIDER** | FIELD | Yes | Provider IEN or name |
| **DATETIME** | FIELD | Yes | Visit date/time for encounter resolution |
| **ALLOWDUPS** | PARAM | No | 1=allow duplicates, 0=prevent (default 0) |

## Common Processing

All V-File RPCs follow this pattern:

1. **Parse Parameters**: Convert MISC array via `ENMISC^ISIIMPUG`
2. **Validate Encounter**: Call `$$CHECKENC(.ISIMISC)` to:
   - Validate patient
   - Validate provider
   - Resolve or create visit (VIEN)
3. **Specific Validation**: Each RPC validates its unique parameters
4. **Duplicate Check**: Unless ALLOWDUPS=1
5. **Create Entry**: In appropriate V-file
6. **Return Result**: Success or error

---

# ISI IMPORT V CPT

## Description
Creates V CPT (Current Procedural Terminology) entries for visit procedures.

## RPC Name
`ISI IMPORT V CPT`

## Entry Point
`VCPT^ISIIMPR3`

## API Entry Point
`$$VCPT^ISIIMP27(.ISIMISC)`

## Additional Parameters

| Parameter | Type | VistA File | Required | Description |
|-----------|------|------------|----------|-------------|
| **CPT** | FIELD | #81,.01 | Yes | CPT code or description |
| **PROVIDER_NARRATIVE** | FIELD | #9999999.27,.01 | Yes | Provider narrative |
| **MODIFIER** | FIELD | #9000010.181,.01 | No | CPT modifier |

### CPT
- Input: CPT code or description from CPT file (#81)
- Example: "99213", "OFFICE VISIT"
- Converted to: IEN

### PROVIDER_NARRATIVE
- Input: Narrative description from PROVIDER NARRATIVE file (#9999999.27)
- Required for documentation
- Example: "OFFICE VISIT", "PROCEDURE"

### MODIFIER
- Input: CPT modifier
- Optional
- Example: "25", "59"

## Output
- Success: `ISIRESUL(0) = "1"`
- Error: `-1^ERROR_MESSAGE`

## Example
```
MISC(1) = "PAT_SSN^123456789"
MISC(2) = "PROVIDER^SMITH,JOHN"
MISC(3) = "DATETIME^T@14:00"
MISC(4) = "CPT^99213"
MISC(5) = "PROVIDER_NARRATIVE^OFFICE VISIT"
```

## Related Files
- **#9000010.18**: V CPT file
- **#81**: CPT file

---

# ISI IMPORT V POV

## Description
Creates V POV (Purpose of Visit) entries for visit diagnoses.

## RPC Name
`ISI IMPORT V POV`

## Entry Point
`VPOV^ISIIMPR3`

## API Entry Point
`$$VPOV^ISIIMP27(.ISIMISC)`

## Additional Parameters

| Parameter | Type | VistA File | Required | Description |
|-----------|------|------------|----------|-------------|
| **ICD9** | FIELD | #80,.01 | Yes | ICD code or description |
| **PRIMSEC** | FIELD | #9000010.07,.12 | Yes | Primary (P) or Secondary (S) |
| **MODIFIER** | FIELD | - | No | Diagnosis modifier |

### ICD9
- Input: ICD-9/ICD-10 code or description
- Supports both ICD-9 and ICD-10
- Example: "250.00", "E11.9", "DIABETES"

### PRIMSEC
- Values: "P" (Primary) or "S" (Secondary)
- Required for proper diagnosis ordering

## Example
```
MISC(1) = "PAT_SSN^123456789"
MISC(2) = "PROVIDER^SMITH,JOHN"
MISC(3) = "DATETIME^T@14:00"
MISC(4) = "ICD9^250.00"
MISC(5) = "PRIMSEC^P"
```

## Related Files
- **#9000010.07**: V POV file
- **#80**: ICD DIAGNOSIS file

---

# ISI IMPORT HFACTOR

## Description
Creates V HEALTH FACTORS entries documenting health-related behaviors and conditions.

## RPC Name
`ISI IMPORT HFACTOR`

## Entry Point
`HFACTOR^ISIIMPR3`

## API Entry Point
`$$VHF^ISIIMP27(.ISIMISC)`

## Additional Parameters

| Parameter | Type | VistA File | Required | Description |
|-----------|------|------------|----------|-------------|
| **HFACTOR** | FIELD | #9999999.64,.01 | Yes | Health factor name |
| **COMMENT** | FIELD | #9000010.23,81101 | No | Free text comment |
| **SEVERITY** | FIELD | #9000010.23,.04 | No | Level/severity (SET) |

### HFACTOR
- Input: Health factor from HEALTH FACTORS file (#9999999.64)
- Examples: "TOBACCO", "ALCOHOL USE", "OBESITY"

### COMMENT
- Free text elaboration
- Example: "2 packs per day"

### SEVERITY
- SET values for applicable factors
- Example: "MILD", "MODERATE", "SEVERE"

## Example
```
MISC(1) = "PAT_SSN^123456789"
MISC(2) = "PROVIDER^SMITH,JOHN"
MISC(3) = "DATETIME^T@14:00"
MISC(4) = "HFACTOR^TOBACCO"
MISC(5) = "COMMENT^1 pack per day for 20 years"
```

## Related Files
- **#9000010.23**: V HEALTH FACTORS file
- **#9999999.64**: HEALTH FACTORS file

---

# ISI IMPORT IMMUNIZATIONS

## Description
Creates V IMMUNIZATION entries documenting vaccines administered.

## RPC Name
`ISI IMPORT IMMUNIZATIONS`

## Entry Point
`VIMMZ^ISIIMPR3`

## API Entry Point
`$$VIMMZ^ISIIMP27(.ISIMISC)`

## Additional Parameters

| Parameter | Type | VistA File | Required | Description |
|-----------|------|------------|----------|-------------|
| **IZ** | FIELD | #9999999.14,.01 | Yes | Immunization name |
| **CONTRAINDICATED** | FIELD | #9000010.11,.07 | No | Contraindication flag |

### IZ
- Input: Immunization from IMMUNIZATION file (#9999999.14)
- Examples: "INFLUENZA", "COVID-19", "PNEUMOCOCCAL"

### CONTRAINDICATED
- Optional flag
- Indicates if vaccine is contraindicated

## Example
```
MISC(1) = "PAT_SSN^123456789"
MISC(2) = "PROVIDER^SMITH,JOHN"
MISC(3) = "DATETIME^T@14:00"
MISC(4) = "IZ^INFLUENZA"
```

## Related Files
- **#9000010.11**: V IMMUNIZATION file
- **#9999999.14**: IMMUNIZATION file

---

# ISI IMPORT V EXAM

## Description
Creates V EXAM entries documenting physical examination findings.

## RPC Name
`ISI IMPORT V EXAM`

## Entry Point
`VEXAM^ISIIMPR3`

## API Entry Point
`$$VEXAM^ISIIMP27(.ISIMISC)`

## Additional Parameters

| Parameter | Type | VistA File | Required | Description |
|-----------|------|------------|----------|-------------|
| **EXAM** | FIELD | #9999999.15,.01 | Yes | Exam finding |

### EXAM
- Input: Exam description from EXAM file (#9999999.15)
- Examples: "ABNORMAL", "NORMAL", specific findings

## Example
```
MISC(1) = "PAT_SSN^123456789"
MISC(2) = "PROVIDER^SMITH,JOHN"
MISC(3) = "DATETIME^T@14:00"
MISC(4) = "EXAM^CARDIOVASCULAR EXAM NORMAL"
```

## Related Files
- **#9000010.13**: V EXAM file
- **#9999999.15**: EXAM file

---

# ISI IMPORT V PATIENT ED

## Description
Creates V PATIENT EDUCATION entries documenting patient teaching.

## RPC Name
`ISI IMPORT V PATIENT ED`

## Entry Point
`VPTEDU^ISIIMPR3`

## API Entry Point
`$$VPNTED^ISIIMP27(.ISIMISC)`

## Additional Parameters

| Parameter | Type | VistA File | Required | Description |
|-----------|------|------------|----------|-------------|
| **ED_TOPIC** | FIELD | #9999999.09,.01 | Yes | Education topic |
| **LEVEL_OF_UNDERSTANDING** | FIELD | #9000010.16,.06 | No | Understanding level |

### ED_TOPIC
- Input: Topic from PATIENT EDUCATION file (#9999999.09)
- Examples: "DIABETES EDUCATION", "MEDICATION INSTRUCTIONS"

### LEVEL_OF_UNDERSTANDING
- Optional assessment of patient comprehension
- Example: "GOOD", "FAIR", "POOR"

## Example
```
MISC(1) = "PAT_SSN^123456789"
MISC(2) = "PROVIDER^SMITH,JOHN"
MISC(3) = "DATETIME^T@14:00"
MISC(4) = "ED_TOPIC^DIABETES EDUCATION"
MISC(5) = "LEVEL_OF_UNDERSTANDING^GOOD"
```

## Related Files
- **#9000010.16**: V PATIENT EDUCATION file
- **#9999999.09**: PATIENT EDUCATION TOPICS file

---

## Common Validation Function

All V-File RPCs use:
```mumps
$$CHECKENC(.ISIMISC)
```

This validates:
- Patient exists and resolves to DFN
- Provider exists and has proper keys
- DateTime is valid
- Visit (VIEN) is resolved or created
- Provider is linked to visit

## Duplicate Prevention

Unless `ALLOWDUPS=1`, checks prevent duplicate entries:
```
-9^[ITEM]/VISIT combo already exists
```

Examples:
- `-9^HF/VISIT combo already exists`
- `-9^IZ/VISIT combo already exists`
- `-9^CPT/VISIT combo already exists`

## Version History
- V.1.0 (June 2012): Initial implementation
- V.2.0 (June 2014): Updates
- V.3.0 (2018): License change to Apache 2.0
- V.3.1 (2024-2025): Continued maintenance
