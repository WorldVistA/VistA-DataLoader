# ISI IMPORT HFACTOR

## Description
Creates V HEALTH FACTORS entries documenting health-related behaviors and conditions. Part of the V-File RPC family that stores encounter-level clinical data.

## RPC Name
`ISI IMPORT HFACTOR`

## Entry Point
[`HFACTOR^ISIIMPR3`](../../VistA/Routines/ISIIMPR3.m)

## API Entry Point
[`$$VHF^ISIIMP27`](../../VistA/Routines/ISIIMP27.m) - Main API

## Parameters

| Parameter | Type | VistA File | Required | Description |
|-----------|------|------------|----------|-------------|
| **PAT_SSN** | FIELD | #2,.09 | Yes | Patient SSN or DFN |
| **PROVIDER** | FIELD | #200,.01 | Yes | Provider IEN or name |
| **DATETIME** | FIELD | - | Yes | Visit date/time for encounter resolution |
| **HFACTOR** | FIELD | #9999999.64,.01 | Yes | Health factor name |
| **COMMENT** | FIELD | #9000010.23,81101 | No | Free text comment |
| **SEVERITY** | FIELD | #9000010.23,.04 | No | Level/severity (SET) |
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

#### HFACTOR
- Input: Health factor from HEALTH FACTORS file (#9999999.64)
- Examples: "TOBACCO", "ALCOHOL USE", "OBESITY", "EXERCISE"
- Validated against file #9999999.64

#### COMMENT
- Free text elaboration on health factor
- Examples: "2 packs per day", "Social drinker", "Walks 30 min daily"
- Optional but recommended for detailed documentation

#### SEVERITY
- SET values for applicable factors
- Examples: "MILD", "MODERATE", "SEVERE"
- Varies by health factor type

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
- `-1^Invalid HFACTOR`
- `-9^HF/VISIT combo already exists` (if ALLOWDUPS=0)

## Examples

### Example 1: Tobacco Use
```
MISC(1) = "PAT_SSN^123456789"
MISC(2) = "PROVIDER^SMITH,JOHN"
MISC(3) = "DATETIME^T@14:00"
MISC(4) = "HFACTOR^TOBACCO"
MISC(5) = "COMMENT^1 pack per day for 20 years"
```

### Example 2: Alcohol Use with Severity
```
MISC(1) = "PAT_SSN^123456789"
MISC(2) = "PROVIDER^SMITH,JOHN"
MISC(3) = "DATETIME^T@14:00"
MISC(4) = "HFACTOR^ALCOHOL USE"
MISC(5) = "COMMENT^3-4 drinks per week"
MISC(6) = "SEVERITY^MILD"
```

### Example 3: Exercise
```
MISC(1) = "PAT_SSN^123456789"
MISC(2) = "PROVIDER^SMITH,JOHN"
MISC(3) = "DATETIME^T@14:00"
MISC(4) = "HFACTOR^EXERCISE"
MISC(5) = "COMMENT^Walks 30 minutes 5 days per week"
```

### Example 4: Multiple Health Factors (Allow Duplicates)
```
MISC(1) = "PAT_SSN^123456789"
MISC(2) = "PROVIDER^SMITH,JOHN"
MISC(3) = "DATETIME^T@14:00"
MISC(4) = "HFACTOR^OBESITY"
MISC(5) = "ALLOWDUPS^1"
```

## Processing Flow

1. **Parse Parameters**: Convert MISC array via `ENMISC^ISIIMPUG`
2. **Validate Encounter**: Call `$$CHECKENC(.ISIMISC)` to:
   - Validate patient (resolve to DFN)
   - Validate provider (check keys/authorization)
   - Resolve or create visit (VIEN)
3. **Validate Health Factor**: Check HEALTH FACTORS file (#9999999.64)
4. **Validate Severity**: If provided, check valid SET value
5. **Duplicate Check**: Unless ALLOWDUPS=1, prevent duplicate HF/visit combinations
6. **Create Entry**: Store in V HEALTH FACTORS file (#9000010.23)
7. **Return Result**: Success or error message

## Related VistA Files

- **#9000010.23**: V HEALTH FACTORS (target file)
- **#9999999.64**: HEALTH FACTORS (source for valid factors)
- **#9000010**: VISIT/ENCOUNTER
- **#2**: PATIENT
- **#200**: NEW PERSON

## Common Health Factors

- TOBACCO
- ALCOHOL USE
- OBESITY
- EXERCISE
- DIET
- STRESS
- SLEEP DISORDERS
- SUBSTANCE ABUSE

## Validation Routine

[`ISIIMPUG`](../../VistA/Routines/ISIIMPUG.m) - Common validation for all V-File RPCs

## Notes

- Part of V-File RPC family - all share common encounter resolution
- Visit (VIEN) automatically created if doesn't exist for date/time
- Provider must be linked to the visit
- Health factors support behavioral and lifestyle documentation
- Comments are critical for detailed clinical documentation
- Some health factors support severity levels
- Multiple health factors can be documented for same visit
