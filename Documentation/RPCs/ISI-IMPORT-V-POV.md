# ISI IMPORT V POV

## Description
Creates V POV (Purpose of Visit) entries for visit diagnoses. Part of the V-File RPC family that stores encounter-level clinical data.

## RPC Name
`ISI IMPORT V POV`

## Entry Point
[`VPOV^ISIIMPR3`](../../VistA/Routines/ISIIMPR3.m)

## API Entry Point
[`$$VPOV^ISIIMP27`](../../VistA/Routines/ISIIMP27.m) - Main API

## Parameters

| Parameter | Type | VistA File | Required | Description |
|-----------|------|------------|----------|-------------|
| **PAT_SSN** | FIELD | #2,.09 | Yes | Patient SSN or DFN |
| **PROVIDER** | FIELD | #200,.01 | Yes | Provider IEN or name |
| **DATETIME** | FIELD | - | Yes | Visit date/time for encounter resolution |
| **ICD9** | FIELD | #80,.01 | Yes | ICD code or description |
| **PRIMSEC** | FIELD | #9000010.07,.12 | Yes | Primary (P) or Secondary (S) |
| **MODIFIER** | FIELD | - | No | Diagnosis modifier |
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

#### ICD9
- Input: ICD-9/ICD-10 code or description
- Supports both ICD-9 and ICD-10 coding systems
- Examples: "250.00", "E11.9", "DIABETES"
- Validated against ICD DIAGNOSIS file (#80)

#### PRIMSEC
- Values: "P" (Primary) or "S" (Secondary)
- Required for proper diagnosis ordering
- Primary diagnosis should be listed first

#### MODIFIER
- Optional diagnosis modifier
- Example: Laterality, severity indicators

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
- `-1^Invalid ICD9 code`
- `-1^Invalid PRIMSEC value`
- `-9^POV/VISIT combo already exists` (if ALLOWDUPS=0)

## Examples

### Example 1: Primary Diagnosis (ICD-9)
```
MISC(1) = "PAT_SSN^123456789"
MISC(2) = "PROVIDER^SMITH,JOHN"
MISC(3) = "DATETIME^T@14:00"
MISC(4) = "ICD9^250.00"
MISC(5) = "PRIMSEC^P"
```

### Example 2: Secondary Diagnosis (ICD-10)
```
MISC(1) = "PAT_SSN^123456789"
MISC(2) = "PROVIDER^SMITH,JOHN"
MISC(3) = "DATETIME^T@14:00"
MISC(4) = "ICD9^E11.9"
MISC(5) = "PRIMSEC^S"
```

### Example 3: With Diagnosis Name
```
MISC(1) = "PAT_SSN^123456789"
MISC(2) = "PROVIDER^SMITH,JOHN"
MISC(3) = "DATETIME^T@14:00"
MISC(4) = "ICD9^DIABETES MELLITUS TYPE 2"
MISC(5) = "PRIMSEC^P"
```

### Example 4: With Modifier
```
MISC(1) = "PAT_SSN^123456789"
MISC(2) = "PROVIDER^SMITH,JOHN"
MISC(3) = "DATETIME^T@14:00"
MISC(4) = "ICD9^S52.501A"
MISC(5) = "PRIMSEC^P"
MISC(6) = "MODIFIER^A"
```

## Processing Flow

1. **Parse Parameters**: Convert MISC array via `ENMISC^ISIIMPUG`
2. **Validate Encounter**: Call `$$CHECKENC(.ISIMISC)` to:
   - Validate patient (resolve to DFN)
   - Validate provider (check keys/authorization)
   - Resolve or create visit (VIEN)
3. **Validate ICD Code**: Check ICD DIAGNOSIS file (#80)
   - Supports both ICD-9 and ICD-10
   - Can lookup by code or description
4. **Validate PRIMSEC**: Ensure value is P or S
5. **Duplicate Check**: Unless ALLOWDUPS=1, prevent duplicate diagnosis/visit combinations
6. **Create Entry**: Store in V POV file (#9000010.07)
7. **Return Result**: Success or error message

## Related VistA Files

- **#9000010.07**: V POV (target file)
- **#80**: ICD DIAGNOSIS (source for diagnosis codes)
- **#9000010**: VISIT/ENCOUNTER
- **#2**: PATIENT
- **#200**: NEW PERSON

## Validation Routine

[`ISIIMPUG`](../../VistA/Routines/ISIIMPUG.m) - Common validation for all V-File RPCs

## Notes

- Part of V-File RPC family - all share common encounter resolution
- Visit (VIEN) automatically created if doesn't exist for date/time
- Provider must be linked to the visit
- Supports both ICD-9 and ICD-10 coding systems
- Primary diagnosis (P) indicates main reason for visit
- Secondary diagnoses (S) indicate additional conditions addressed
- Each visit should have at least one primary diagnosis

## Version History

- V.1.0 (June 2012): Initial implementation
- V.2.0 (June 2014): Updates
- V.3.0 (2018): License change to Apache 2.0
- V.3.1 (2024-2025): Continued maintenance
