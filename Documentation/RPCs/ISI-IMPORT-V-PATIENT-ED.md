# ISI IMPORT V PATIENT ED

## Description
Creates V PATIENT EDUCATION entries documenting patient teaching. Part of the V-File RPC family that stores encounter-level clinical data.

## RPC Name
`ISI IMPORT V PATIENT ED`

## Entry Point
[`VPTEDU^ISIIMPR3`](../../VistA/Routines/ISIIMPR3.m)

## API Entry Point
[`$$VPNTED^ISIIMP27`](../../VistA/Routines/ISIIMP27.m) - Main API

## Parameters

| Parameter | Type | VistA File | Required | Description |
|-----------|------|------------|----------|-------------|
| **PAT_SSN** | FIELD | #2,.09 | Yes | Patient SSN or DFN |
| **PROVIDER** | FIELD | #200,.01 | Yes | Provider IEN or name |
| **DATETIME** | FIELD | - | Yes | Visit date/time for encounter resolution |
| **ED_TOPIC** | FIELD | #9999999.09,.01 | Yes | Education topic |
| **LEVEL_OF_UNDERSTANDING** | FIELD | #9000010.16,.06 | No | Understanding level |
| **ALLOWDUPS** | PARAM | - | No | 1=allow duplicates, 0=prevent (default 0) |

### Parameter Details

#### PAT_SSN
- Input: Patient SSN or DFN
- Validates against PATIENT file (#2)
- Converted to: DFN

#### PROVIDER
- Input: Provider name or IEN
- Validates against NEW PERSON file (#200)
- Checks provider keys and authorization
- Converted to: IEN

#### DATETIME
- Input: Date/time in FileMan format
- Used for encounter/visit resolution
- Represents when education was provided
- Example: "T@14:00" (today at 2 PM)

#### ED_TOPIC
- Input: Topic from PATIENT EDUCATION TOPICS file (#9999999.09)
- Examples:
  - "DIABETES EDUCATION"
  - "MEDICATION INSTRUCTIONS"
  - "DIET AND NUTRITION"
  - "HYPERTENSION MANAGEMENT"
  - "WOUND CARE"
- Validated against file #9999999.09

#### LEVEL_OF_UNDERSTANDING
- Optional assessment of patient comprehension
- Common values:
  - "GOOD" - Patient demonstrates understanding
  - "FAIR" - Patient partially understands
  - "POOR" - Patient has difficulty understanding
  - "EXCELLENT" - Complete understanding demonstrated
- Used to document teaching effectiveness

#### ALLOWDUPS
- Values: 1 (allow) or 0 (prevent, default)
- Controls duplicate entry prevention

## Output

### Success
```
ISIRESUL(0) = "1"
```

### Error
```
ISIRESUL(0) = "-1^ERROR_MESSAGE"
```

### Common Errors
- `-1^Invalid PAT_SSN`
- `-1^Invalid PROVIDER`
- `-1^Invalid ED_TOPIC`
- `-9^PATIENT ED/VISIT combo already exists` (if ALLOWDUPS=0)

## Examples

### Example 1: Diabetes Education
```
MISC(1) = "PAT_SSN^123456789"
MISC(2) = "PROVIDER^SMITH,JOHN"
MISC(3) = "DATETIME^T@14:00"
MISC(4) = "ED_TOPIC^DIABETES EDUCATION"
MISC(5) = "LEVEL_OF_UNDERSTANDING^GOOD"
```

### Example 2: Medication Instructions
```
MISC(1) = "PAT_SSN^123456789"
MISC(2) = "PROVIDER^SMITH,JOHN"
MISC(3) = "DATETIME^T@14:00"
MISC(4) = "ED_TOPIC^MEDICATION INSTRUCTIONS"
MISC(5) = "LEVEL_OF_UNDERSTANDING^EXCELLENT"
```

### Example 3: Diet Education
```
MISC(1) = "PAT_SSN^123456789"
MISC(2) = "PROVIDER^SMITH,JOHN"
MISC(3) = "DATETIME^T@14:00"
MISC(4) = "ED_TOPIC^DIET AND NUTRITION"
MISC(5) = "LEVEL_OF_UNDERSTANDING^FAIR"
```

### Example 4: Multiple Topics (Allow Duplicates)
```
MISC(1) = "PAT_SSN^123456789"
MISC(2) = "PROVIDER^SMITH,JOHN"
MISC(3) = "DATETIME^T@14:00"
MISC(4) = "ED_TOPIC^WOUND CARE"
MISC(5) = "ALLOWDUPS^1"
```

## Processing Flow

1. **Parse Parameters**: Convert MISC array via `ENMISC^ISIIMPUG`
2. **Validate Encounter**: Call `$$CHECKENC(.ISIMISC)` to:
   - Validate patient (resolve to DFN)
   - Validate provider (check keys/authorization)
   - Resolve or create visit (VIEN)
3. **Validate Education Topic**: Check PATIENT EDUCATION TOPICS file (#9999999.09)
4. **Validate Understanding Level**: If provided, check valid value
5. **Duplicate Check**: Unless ALLOWDUPS=1, prevent duplicate topic/visit combinations
6. **Create Entry**: Store in V PATIENT EDUCATION file (#9000010.16)
7. **Return Result**: Success or error message

## Related VistA Files

- **#9000010.16**: V PATIENT EDUCATION (target file)
- **#9999999.09**: PATIENT EDUCATION TOPICS (source for valid topics)
- **#9000010**: VISIT/ENCOUNTER
- **#2**: PATIENT
- **#200**: NEW PERSON

## Common Education Topics

### Disease Management
- Diabetes Education
- Hypertension Management
- Asthma Care
- Heart Failure Management
- COPD Education

### Medication
- Medication Instructions
- Medication Side Effects
- Medication Compliance

### Lifestyle
- Diet and Nutrition
- Exercise and Activity
- Smoking Cessation
- Weight Management

### Self-Care
- Wound Care
- Foot Care
- Blood Glucose Monitoring
- Blood Pressure Monitoring

## Validation Routine

[`ISIIMPUG`](../../VistA/Routines/ISIIMPUG.m) - Common validation for all V-File RPCs

## Notes

- Part of V-File RPC family - all share common encounter resolution
- Visit (VIEN) automatically created if doesn't exist for date/time
- Provider must be linked to the visit
- Level of understanding helps track teaching effectiveness
- Multiple topics can be documented for same visit using ALLOWDUPS=1
- Important for quality metrics and patient safety
- Documents compliance with teaching requirements

## Version History

- V.1.0 (June 2012): Initial implementation
- V.2.0 (June 2014): Updates
- V.3.0 (2018): License change to Apache 2.0
- V.3.1 (2024-2025): Continued maintenance
