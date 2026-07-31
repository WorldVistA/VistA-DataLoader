# ISI IMPORT IMMUNIZATIONS

## Description
Creates V IMMUNIZATION entries documenting vaccines administered. Part of the V-File RPC family that stores encounter-level clinical data.

## RPC Name
`ISI IMPORT IMMUNIZATIONS`

## Entry Point
[`VIMMZ^ISIIMPR3`](../../VistA/Routines/ISIIMPR3.m)

## API Entry Point
[`$$VIMMZ^ISIIMP27`](../../VistA/Routines/ISIIMP27.m) - Main API

## Parameters

| Parameter | Type | VistA File | Required | Description |
|-----------|------|------------|----------|-------------|
| **PAT_SSN** | FIELD | #2,.09 | Yes | Patient SSN or DFN |
| **PROVIDER** | FIELD | #200,.01 | Yes | Provider IEN or name |
| **DATETIME** | FIELD | - | Yes | Visit date/time for encounter resolution |
| **IZ** | FIELD | #9999999.14,.01 | Yes | Immunization name |
| **CONTRAINDICATED** | FIELD | #9000010.11,.07 | No | Contraindication flag |
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
- Represents vaccine administration date/time
- Example: "T@14:00" (today at 2 PM)

#### IZ
- Input: Immunization from IMMUNIZATION file (#9999999.14)
- Examples: "INFLUENZA", "COVID-19", "PNEUMOCOCCAL", "TDAP"
- Validated against file #9999999.14

#### CONTRAINDICATED
- Optional flag indicating contraindication
- Used when documenting why vaccine was not given
- Values vary by VistA configuration

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
- `-1^Invalid IZ (immunization)`
- `-9^IZ/VISIT combo already exists` (if ALLOWDUPS=0)

## Examples

### Example 1: Influenza Vaccine
```
MISC(1) = "PAT_SSN^123456789"
MISC(2) = "PROVIDER^SMITH,JOHN"
MISC(3) = "DATETIME^T@14:00"
MISC(4) = "IZ^INFLUENZA"
```

### Example 2: COVID-19 Vaccine
```
MISC(1) = "PAT_SSN^123456789"
MISC(2) = "PROVIDER^SMITH,JOHN"
MISC(3) = "DATETIME^T@10:30"
MISC(4) = "IZ^COVID-19"
```

### Example 3: Contraindicated Vaccine
```
MISC(1) = "PAT_SSN^123456789"
MISC(2) = "PROVIDER^SMITH,JOHN"
MISC(3) = "DATETIME^T@14:00"
MISC(4) = "IZ^PNEUMOCOCCAL"
MISC(5) = "CONTRAINDICATED^1"
```

### Example 4: Tetanus Booster
```
MISC(1) = "PAT_SSN^123456789"
MISC(2) = "PROVIDER^SMITH,JOHN"
MISC(3) = "DATETIME^T@14:00"
MISC(4) = "IZ^TDAP"
```

## Processing Flow

1. **Parse Parameters**: Convert MISC array via `ENMISC^ISIIMPUG`
2. **Validate Encounter**: Call `$$CHECKENC(.ISIMISC)` to:
   - Validate patient (resolve to DFN)
   - Validate provider (check keys/authorization)
   - Resolve or create visit (VIEN)
3. **Validate Immunization**: Check IMMUNIZATION file (#9999999.14)
4. **Duplicate Check**: Unless ALLOWDUPS=1, prevent duplicate IZ/visit combinations
5. **Create Entry**: Store in V IMMUNIZATION file (#9000010.11)
6. **Return Result**: Success or error message

## Related VistA Files

- **#9000010.11**: V IMMUNIZATION (target file)
- **#9999999.14**: IMMUNIZATION (source for valid vaccines)
- **#9000010**: VISIT/ENCOUNTER
- **#2**: PATIENT
- **#200**: NEW PERSON

## Common Immunizations

- INFLUENZA
- COVID-19
- PNEUMOCOCCAL
- TDAP (Tetanus, Diphtheria, Pertussis)
- MMR (Measles, Mumps, Rubella)
- VARICELLA (Chickenpox)
- HEPATITIS A
- HEPATITIS B
- SHINGLES (Zoster)
- HPV (Human Papillomavirus)

## Validation Routine

[`ISIIMPUG`](../../VistA/Routines/ISIIMPUG.m) - Common validation for all V-File RPCs

## Notes

- Part of V-File RPC family - all share common encounter resolution
- Visit (VIEN) automatically created if doesn't exist for date/time
- Provider must be linked to the visit
- DATETIME represents vaccine administration date/time
- Contraindication flag documents when vaccine should not be given
- Multiple immunizations can be given during same visit
- Use ALLOWDUPS=1 for booster doses of same vaccine
