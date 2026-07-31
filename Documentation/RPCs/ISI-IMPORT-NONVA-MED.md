# ISI IMPORT NONVA MED

## Description
Creates Non-VA medication orders in the patient's medication profile. This RPC handles medications obtained outside the VA system that need to be documented in the patient's record.

## RPC Name
`ISI IMPORT NONVA MED`

## Entry Point
[`NVAMED^ISIIMPR2`](../../VistA/Routines/ISIIMPR2.m#L216)

## API Entry Point
[`$$NVAMEDS^ISIIMP16`](../../VistA/Routines/ISIIMP16.m#L37) - Main API  
[`$$VALIDATE^ISIIMP29`](../../VistA/Routines/ISIIMP29.m) - Validation  
[`$$MAKENVA^ISIIMP29`](../../VistA/Routines/ISIIMP29.m) - Creation

## Input Parameters

### MISC Array
Format: `MISC(n) = "PARAMETER^VALUE"`

| Parameter | Type | File/Field | Required | Description |
|-----------|------|------------|----------|-------------|
| PAT_SSN | FIELD | #2,.09 | ✅ Yes | Patient SSN (identifier) |
| DRUG | FIELD | #55,52.2,.01 | ✅ Yes | Orderable Item name |
| DATE | FIELD | #55,52.2,8 | ✅ Yes | Start Date |
| DOSAGE | FIELD | #55,52.2,2 | ✅ Yes | Medication Dosage |
| SIG | FIELD | #55,52.2,4 | ✅ Yes | Medication Schedule |
| ROUTE | FIELD | #55,52.2,3 | ✅ Yes | Medication Route |
| DISC | FIELD | #55,52.2,10 | No | Disclaimer Text |

## Parameter Details

### PAT_SSN
- **Format**: 9-digit SSN (no dashes)
- **Example**: `"666112222"`
- **Validation**: Must exist in PATIENT file (#2)

### DRUG
- **Format**: Orderable Item name
- **Example**: `"ASPIRIN 81MG TAB"`
- **Validation**: Must exist in Non-VA Medication Quick Order list
- **File**: Looked up in PROTOCOL file (#101.44) under "ORWDSET NV RX"

### DATE
- **Format**: FileMan date/time
- **Example**: `"3250101"` (Jan 1, 2025) or `"3250101.1400"` (Jan 1, 2025 @ 2PM)
- **Auto-append**: If no time provided, defaults to `.1200` (noon)
- **Validation**: Valid FileMan date format

### DOSAGE
- **Format**: Free text dosage instructions
- **Example**: `"81 MG"`, `"1 TAB"`
- **Required**: Yes

### SIG
- **Format**: Schedule/directions for use
- **Example**: `"TAKE ONE TABLET BY MOUTH EVERY DAY"`
- **Required**: Yes

### ROUTE
- **Format**: Route of administration
- **Example**: `"ORAL"`, `"TOPICAL"`
- **Required**: Yes

### DISC
- **Format**: Disclaimer text (optional)
- **Example**: `"Patient self-reported"`
- **Required**: No

## Output

### Success
```
ISIRESUL(0) = 1
```

### Error
```
ISIRESUL(0) = "-1^ERROR_MESSAGE"
```

## Example Usage

### Example 1: Basic Non-VA Medication
```
MISC(1) = "PAT_SSN^666001001"
MISC(2) = "DRUG^ASPIRIN 81MG TAB"
MISC(3) = "DATE^3250101"
MISC(4) = "DOSAGE^81 MG"
MISC(5) = "SIG^TAKE ONE TABLET BY MOUTH EVERY DAY"
MISC(6) = "ROUTE^ORAL"

Returns: ISIRESUL(0) = 1
```

### Example 2: With Disclaimer
```
MISC(1) = "PAT_SSN^666002002"
MISC(2) = "DRUG^IBUPROFEN 200MG TAB"
MISC(3) = "DATE^3250115.0900"
MISC(4) = "DOSAGE^200 MG"
MISC(5) = "SIG^TAKE 1-2 TABLETS BY MOUTH EVERY 6 HOURS AS NEEDED"
MISC(6) = "ROUTE^ORAL"
MISC(7) = "DISC^Patient purchased over-the-counter"

Returns: ISIRESUL(0) = 1
```

### Example 3: Topical Medication
```
MISC(1) = "PAT_SSN^666003003"
MISC(2) = "DRUG^HYDROCORTISONE 1% CREAM"
MISC(3) = "DATE^3250201"
MISC(4) = "DOSAGE^APPLY THIN LAYER"
MISC(5) = "SIG^APPLY TO AFFECTED AREA TWICE DAILY"
MISC(6) = "ROUTE^TOPICAL"

Returns: ISIRESUL(0) = 1
```

### Example 4: Supplement
```
MISC(1) = "PAT_SSN^666004004"
MISC(2) = "DRUG^VITAMIN D3 1000 UNIT TAB"
MISC(3) = "DATE^3250301"
MISC(4) = "DOSAGE^1000 UNITS"
MISC(5) = "SIG^TAKE ONE TABLET BY MOUTH DAILY"
MISC(6) = "ROUTE^ORAL"
MISC(7) = "DISC^Over-the-counter supplement"

Returns: ISIRESUL(0) = 1
```

## Common Error Messages

| Error Message | Cause | Solution |
|---------------|-------|----------|
| `-1^Missing Patient SSN` | PAT_SSN not provided | Include PAT_SSN parameter |
| `-1^Invalid PAT_SSN` | SSN not found in file #2 | Verify patient exists |
| `-1^Invalid Pharmacy Orderable Item value` | DRUG name not in Quick Order | Check available Non-VA meds via TABLEFETCH |
| `-1^Missing Start Date` | DATE not provided | Include DATE parameter |
| `-1^Missing Schedule value` | SIG not provided | Include SIG parameter |
| `-1^Missing Dosage value` | DOSAGE not provided | Include DOSAGE parameter |
| `-1^Missing Route value` | ROUTE not provided | Include ROUTE parameter |
| `-1^Invalid DATE date/time` | Invalid date format | Use FileMan date format |
| `-1^Bad parameter title passed` | Invalid parameter name | Check spelling of parameter names |

## Related Files
- **#2**: PATIENT file
- **#55**: PATIENT MOVEMENT file (medication profile sub-files)
- **#55,52.2**: NON-VA MEDS sub-file
- **#101.44**: PROTOCOL file (Quick Order list)

## Validation Routine
- **ISIIMPUI**: Parameter parsing (`MEDMISC^ISIIMPUI`)
- **ISIIMPUI**: Validation (`VALMEDS^ISIIMPUI`)
- **ISIIMP29**: Non-VA Med validation (`VALIDATE^ISIIMP29`)

## API Routine
- **ISIIMP16**: Main API (`NVAMEDS^ISIIMP16`)
- **ISIIMP29**: Non-VA Med creation (`MAKENVA^ISIIMP29`)

## Processing Flow

1. Parse parameters via `MEDMISC^ISIIMPUI`
2. Validate PAT_SSN and convert to DFN
3. Validate DRUG name against Quick Order list
4. Convert DRUG name to Orderable Item IEN
5. Validate DATE format
6. Validate required fields (DOSAGE, SIG, ROUTE)
7. Call `MAKENVA^ISIIMP29` to create Non-VA med entry
8. Return success or error

## Key Differences from ISI IMPORT MED

| Feature | ISI IMPORT MED | ISI IMPORT NONVA MED |
|---------|----------------|----------------------|
| Medication Source | VA Pharmacy | Non-VA (OTC, outside pharmacy) |
| File | #55 sub-files (Outpatient) | #55,52.2 (Non-VA Meds) |
| Drug Lookup | #50.7 Pharmacy Orderable Items | #101.44 Quick Order list |
| Prescription Info | Full (refills, supply, etc.) | Simplified (documentation only) |
| Disclaimer Field | Not applicable | Available (DISC parameter) |

## Notes
- Non-VA medications are for documentation purposes only
- They do not generate prescriptions or pharmacy orders
- Used to track patient's complete medication list including OTC and non-VA sources
- The DRUG parameter must match entries in the Non-VA Medication Quick Order list
- Use `ISI IMPORT TABLEFETCH` with `TABLE=NVAMEDS` to retrieve available medications

## Retrieving Available Non-VA Medications
```
Call: ISI IMPORT TABLEFETCH
MISC(1) = "TABLE^NVAMEDS"

Returns list of available Non-VA Orderable Items for the DRUG parameter
```
