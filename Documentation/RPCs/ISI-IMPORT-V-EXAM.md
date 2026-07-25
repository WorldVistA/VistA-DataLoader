# ISI IMPORT V EXAM

## Description
Creates V EXAM entries documenting physical examination findings. Part of the V-File RPC family that stores encounter-level clinical data.

## RPC Name
`ISI IMPORT V EXAM`

## Entry Point
[`VEXAM^ISIIMPR3`](../../VistA/Routines/ISIIMPR3.m)

## API Entry Point
[`$$VEXAM^ISIIMP27`](../../VistA/Routines/ISIIMP27.m) - Main API

## Parameters

| Parameter | Type | VistA File | Required | Description |
|-----------|------|------------|----------|-------------|
| **PAT_SSN** | FIELD | #2,.09 | Yes | Patient SSN or DFN |
| **PROVIDER** | FIELD | #200,.01 | Yes | Provider IEN or name |
| **DATETIME** | FIELD | - | Yes | Visit date/time for encounter resolution |
| **EXAM** | FIELD | #9999999.15,.01 | Yes | Exam finding |
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
- Example: "T@14:00" (today at 2 PM)

#### EXAM
- Input: Exam description from EXAM file (#9999999.15)
- Examples: 
  - "CARDIOVASCULAR EXAM NORMAL"
  - "RESPIRATORY EXAM ABNORMAL"
  - "NEUROLOGICAL EXAM WITHIN NORMAL LIMITS"
  - "ABDOMINAL EXAM TENDERNESS RUQ"
- Validated against file #9999999.15

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
- `-1^Invalid EXAM`
- `-9^EXAM/VISIT combo already exists` (if ALLOWDUPS=0)

## Examples

### Example 1: Normal Cardiovascular Exam
```
MISC(1) = "PAT_SSN^123456789"
MISC(2) = "PROVIDER^SMITH,JOHN"
MISC(3) = "DATETIME^T@14:00"
MISC(4) = "EXAM^CARDIOVASCULAR EXAM NORMAL"
```

### Example 2: Abnormal Respiratory Exam
```
MISC(1) = "PAT_SSN^123456789"
MISC(2) = "PROVIDER^SMITH,JOHN"
MISC(3) = "DATETIME^T@14:00"
MISC(4) = "EXAM^RESPIRATORY EXAM WHEEZING"
```

### Example 3: Neurological Exam
```
MISC(1) = "PAT_SSN^123456789"
MISC(2) = "PROVIDER^SMITH,JOHN"
MISC(3) = "DATETIME^T@14:00"
MISC(4) = "EXAM^NEUROLOGICAL EXAM WITHIN NORMAL LIMITS"
```

### Example 4: Abdominal Exam with Findings
```
MISC(1) = "PAT_SSN^123456789"
MISC(2) = "PROVIDER^SMITH,JOHN"
MISC(3) = "DATETIME^T@14:00"
MISC(4) = "EXAM^ABDOMINAL EXAM TENDERNESS RLQ"
```

## Processing Flow

1. **Parse Parameters**: Convert MISC array via `ENMISC^ISIIMPUG`
2. **Validate Encounter**: Call `$$CHECKENC(.ISIMISC)` to:
   - Validate patient (resolve to DFN)
   - Validate provider (check keys/authorization)
   - Resolve or create visit (VIEN)
3. **Validate Exam**: Check EXAM file (#9999999.15)
4. **Duplicate Check**: Unless ALLOWDUPS=1, prevent duplicate exam/visit combinations
5. **Create Entry**: Store in V EXAM file (#9000010.13)
6. **Return Result**: Success or error message

## Related VistA Files

- **#9000010.13**: V EXAM (target file)
- **#9999999.15**: EXAM (source for valid exam findings)
- **#9000010**: VISIT/ENCOUNTER
- **#2**: PATIENT
- **#200**: NEW PERSON

## Common Exam Types

### System-Based Exams
- Cardiovascular
- Respiratory
- Gastrointestinal/Abdominal
- Neurological
- Musculoskeletal
- Skin/Integumentary
- HEENT (Head, Eyes, Ears, Nose, Throat)
- Genitourinary

### Typical Findings
- Normal/Within Normal Limits (WNL)
- Abnormal with specific findings
- Deferred/Not Examined
- See Note/Comments

## Validation Routine

[`ISIIMPUG`](../../VistA/Routines/ISIIMPUG.m) - Common validation for all V-File RPCs

## Notes

- Part of V-File RPC family - all share common encounter resolution
- Visit (VIEN) automatically created if doesn't exist for date/time
- Provider must be linked to the visit
- Multiple exam findings can be documented for same visit
- Exam findings should be specific and clinically relevant
- Use ALLOWDUPS=1 if documenting multiple findings for same body system

## Version History

- V.1.0 (June 2012): Initial implementation
- V.2.0 (June 2014): Updates
- V.3.0 (2018): License change to Apache 2.0
- V.3.1 (2024-2025): Continued maintenance
