# Template Management RPCs

## Overview
Three RPCs manage patient import templates stored in ISI PT IMPORT TEMPLATE file (#9001). Templates allow batch creation of patients with predefined demographics and masks for random value generation.

---

# ISI IMPORT GET TEMPLATES

## Description
Fetches the list of available patient import templates.

## RPC Name
`ISI IMPORT GET TEMPLATES`

## Entry Point
`FETCHTMP^ISIIMPUA`

## Output
- Array of template names from file #9001

---

# ISI IMPORT GET TEMPLATE DETLS

## Description
Retrieves detailed configuration for a specific patient import template.

## RPC Name
`ISI IMPORT GET TEMPLATE DETLS`

## Entry Point
`TEMPLATE^ISIIMPUA`

## Input
- Template name or IEN

## Output
- Template field values:
  - TYPE (Patient Type)
  - NAME_MASK
  - SSN_MASK
  - SEX
  - LOW_DOB / UP_DOB
  - MARITAL_STATUS
  - ZIP_4_MASK
  - PH_NUM_MASK
  - CITY
  - STATE
  - VETERAN
  - DFN_NAME
  - EMPLOY_STAT
  - SERVICE

## Template File Structure

### ISI PT IMPORT TEMPLATE (#9001)

| Field | Number | Description |
|-------|--------|-------------|
| NAME | .01 | Template name |
| TYPE | 1 | TYPE OF PATIENT (#391) |
| NAME MASK | 2 | Last name mask |
| SSN MASK | 4 | SSN mask (5 digits max) |
| SEX | 5 | M or F |
| EARLIEST DATE OF BIRTH | 6 | Low DOB limit |
| LATEST DATE OF BIRTH | 7 | High DOB limit |
| MARITAL STATUS | 8 | Pointer to #11 |
| ZIP+4 MASK | 9 | Zip code mask |
| PHONE NUMBER MASK | 10 | Phone mask |
| CITY | 11 | City name |
| STATE | 12 | Pointer to #5 |
| VETERAN | 13 | Y/N |
| DFN_NAME | 14 | Y/N |
| EMPLOYMENT STATUS | 15 | Set value |
| SERVICE | 16 | Pointer to #49 |

---

# ISI IMPORT SAVE TEMPLATE

## Description
Updates an existing patient import template or creates a new one.

## RPC Name
`ISI IMPORT SAVE TEMPLATE`

## Entry Point
`TMPUPDTE^ISIIMPR1`

## API Entry Point
`$$TEMPLATE^ISIIMP24(.ISIRESUL,.ISIMISC)`

## Input Parameters

### MISC Array
Format: `MISC(n) = "PARAMETER^VALUE"`

All fields from ISI PT IMPORT TEMPLATE file (#9001) can be passed.

## Output
- Success: `ISIRESUL(0) = 1`
- Error: `ISIRESUL(0) = -1^ERROR_MESSAGE`

## Example: Create Template
```
MISC(1) = "NAME^DEFAULT_VETERAN"
MISC(2) = "TYPE^VETERAN"
MISC(3) = "NAME_MASK^*,PATIENT"
MISC(4) = "SSN_MASK^00012*"
MISC(5) = "SEX^M"
MISC(6) = "LOW_DOB^2900101"
MISC(7) = "UP_DOB^3000101"
MISC(8) = "MARITAL_STATUS^MARRIED"
MISC(9) = "VETERAN^Y"
MISC(10) = "EMPLOY_STAT^RETIRED"
```

## Mask Format

Masks use asterisk (*) as wildcard for random value generation:

- **NAME_MASK**: `"*,PATIENT"` → Random last name + ",PATIENT"
- **SSN_MASK**: `"00012*"` → "00012" + random 4 digits
- **ZIP_4_MASK**: `"2210*"` → "2210" + random digit

## Processing Flow

1. Parse template parameters via `TMPMISC^ISIIMPUE`
2. Validate template structure via `VALIDATE^ISIIMPUE`
3. Create or update template in file #9001
4. Return success or error

## Related RPCs
- Used by ISI IMPORT PAT when TEMPLATE parameter specified
- Templates support batch patient creation with consistent demographics

## Notes
- Templates are stored in file #9001
- Masks enable random value generation for testing
- Templates overlay with explicit parameters in patient creation
- DFN_NAME controls whether patient name derived from DFN

## Version History
- V.1.0 (June 2012): Initial implementation
- V.2.0 (June 2014): Updates
- V.3.0 (2018): License change to Apache 2.0
- V.3.1 (2024-2025): Continued maintenance
