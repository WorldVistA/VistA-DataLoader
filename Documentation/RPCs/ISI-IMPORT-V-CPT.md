# ISI IMPORT V CPT

## Description
Creates V CPT (Current Procedural Terminology) entries for visit procedures. Part of the V-File RPC family that stores encounter-level clinical data.

## RPC Name
`ISI IMPORT V CPT`

## Entry Point
[`VCPT^ISIIMPR3`](../../VistA/Routines/ISIIMPR3.m)

## API Entry Point
[`$$VCPT^ISIIMP27`](../../VistA/Routines/ISIIMP27.m) - Main API

## Parameters

| Parameter | Type | VistA File | Required | Description |
|-----------|------|------------|----------|-------------|
| **PAT_SSN** | FIELD | #2,.09 | Yes | Patient SSN or DFN |
| **PROVIDER** | FIELD | #200,.01 | Yes | Provider IEN or name |
| **DATETIME** | FIELD | - | Yes | Visit date/time for encounter resolution |
| **CPT** | FIELD | #81,.01 | Yes | CPT code or description |
| **PROVIDER_NARRATIVE** | FIELD | #9999999.27,.01 | Yes | Provider narrative |
| **MODIFIER** | FIELD | #9000010.181,.01 | No | CPT modifier |
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

#### CPT
- Input: CPT code or description from CPT file (#81)
- Example: "99213", "OFFICE VISIT"
- Converted to: IEN

#### PROVIDER_NARRATIVE
- Input: Narrative description from PROVIDER NARRATIVE file (#9999999.27)
- Required for documentation
- Example: "OFFICE VISIT", "PROCEDURE"

#### MODIFIER
- Input: CPT modifier
- Optional
- Example: "25", "59"

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
- `-1^Invalid CPT`
- `-1^Invalid PROVIDER_NARRATIVE`
- `-9^CPT/VISIT combo already exists` (if ALLOWDUPS=0)

## Examples

### Example 1: Office Visit CPT
```
MISC(1) = "PAT_SSN^123456789"
MISC(2) = "PROVIDER^SMITH,JOHN"
MISC(3) = "DATETIME^T@14:00"
MISC(4) = "CPT^99213"
MISC(5) = "PROVIDER_NARRATIVE^OFFICE VISIT"
```

### Example 2: With Modifier
```
MISC(1) = "PAT_SSN^123456789"
MISC(2) = "PROVIDER^SMITH,JOHN"
MISC(3) = "DATETIME^T@14:00"
MISC(4) = "CPT^99213"
MISC(5) = "PROVIDER_NARRATIVE^OFFICE VISIT"
MISC(6) = "MODIFIER^25"
```

### Example 3: Allow Duplicates
```
MISC(1) = "PAT_SSN^123456789"
MISC(2) = "PROVIDER^SMITH,JOHN"
MISC(3) = "DATETIME^T@14:00"
MISC(4) = "CPT^99213"
MISC(5) = "PROVIDER_NARRATIVE^OFFICE VISIT"
MISC(6) = "ALLOWDUPS^1"
```

## Processing Flow

1. **Parse Parameters**: Convert MISC array via `ENMISC^ISIIMPUG`
2. **Validate Encounter**: Call `$$CHECKENC(.ISIMISC)` to:
   - Validate patient (resolve to DFN)
   - Validate provider (check keys/authorization)
   - Resolve or create visit (VIEN)
3. **Validate CPT**: Verify CPT code exists and is valid
4. **Validate Narrative**: Check PROVIDER_NARRATIVE file
5. **Duplicate Check**: Unless ALLOWDUPS=1, prevent duplicate CPT/VISIT combinations
6. **Create Entry**: Store in V CPT file (#9000010.18)
7. **Return Result**: Success or error message

## Related VistA Files

- **#9000010.18**: V CPT (target file)
- **#81**: CPT (source for CPT codes)
- **#9999999.27**: PROVIDER NARRATIVE
- **#9000010**: VISIT/ENCOUNTER
- **#2**: PATIENT
- **#200**: NEW PERSON

## Validation Routine

[`ISIIMPUG`](../../VistA/Routines/ISIIMPUG.m) - Common validation for all V-File RPCs

## Notes

- Part of V-File RPC family - all share common encounter resolution
- Visit (VIEN) automatically created if doesn't exist for date/time
- Provider must be linked to the visit
- Duplicate prevention checks CPT + VISIT combination
- CPT modifiers are optional but recommended when applicable
