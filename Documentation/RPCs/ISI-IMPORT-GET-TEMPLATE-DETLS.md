# ISI IMPORT GET TEMPLATE DETLS

## Description
Retrieves detailed configuration for a specific patient import template. Returns all field values from ISI PT IMPORT TEMPLATE file (#9001) for use in patient creation.

## RPC Name
`ISI IMPORT GET TEMPLATE DETLS`

## Entry Point
[`TEMPLATE^ISIIMPUA`](../../VistA/Routines/ISIIMPUA.m)

## Input

Template name or IEN from ISI PT IMPORT TEMPLATE file (#9001)

## Output

### Success
Array of template field values:
```
ISIRESUL("TYPE") = "VETERAN"
ISIRESUL("NAME_MASK") = "*,PATIENT"
ISIRESUL("SSN_MASK") = "00012*"
ISIRESUL("SEX") = "M"
ISIRESUL("LOW_DOB") = "2900101"
ISIRESUL("UP_DOB") = "3000101"
ISIRESUL("MARITAL_STATUS") = "MARRIED"
ISIRESUL("ZIP_4_MASK") = "2210*"
ISIRESUL("PH_NUM_MASK") = "555*"
ISIRESUL("CITY") = "BALTIMORE"
ISIRESUL("STATE") = "MARYLAND"
ISIRESUL("VETERAN") = "Y"
ISIRESUL("DFN_NAME") = "N"
ISIRESUL("EMPLOY_STAT") = "RETIRED"
ISIRESUL("SERVICE") = "ARMY"
```

### Template Not Found
```
ISIRESUL(0) = "-1^Template not found"
```

## Template Fields Returned

| Field Name | Description | Example Value |
|------------|-------------|---------------|
| **TYPE** | Patient type | "VETERAN", "EMPLOYEE" |
| **NAME_MASK** | Last name mask | "*,PATIENT", "TEST*" |
| **SSN_MASK** | SSN mask (5 digits max) | "00012*", "999*" |
| **SEX** | Gender | "M", "F" |
| **LOW_DOB** | Earliest date of birth | "2900101" (Jan 1, 1990) |
| **UP_DOB** | Latest date of birth | "3000101" (Jan 1, 2000) |
| **MARITAL_STATUS** | Marital status | "MARRIED", "SINGLE" |
| **ZIP_4_MASK** | Zip code mask | "2210*", "90210" |
| **PH_NUM_MASK** | Phone number mask | "555*", "410*" |
| **CITY** | City name | "BALTIMORE", "BOSTON" |
| **STATE** | State name | "MARYLAND", "TEXAS" |
| **VETERAN** | Veteran flag | "Y", "N" |
| **DFN_NAME** | Use DFN as name | "Y", "N" |
| **EMPLOY_STAT** | Employment status | "EMPLOYED", "RETIRED" |
| **SERVICE** | Military service | "ARMY", "NAVY" |

## Examples

### Example 1: Get Template by Name
```
S TEMPLATE="DEFAULT_VETERAN"
D TEMPLATE^ISIIMPUA(TEMPLATE,.ISIRESUL)
```

### Example 2: Get Template by IEN
```
S TEMPLATE=1
D TEMPLATE^ISIIMPUA(TEMPLATE,.ISIRESUL)
```

### Example 3: Using Template with Patient Creation
```
; Get template
S TEMPLATE="DEFAULT_VETERAN"
D TEMPLATE^ISIIMPUA(TEMPLATE,.TMPRESUL)

; Use template values
S MISC(1)="TEMPLATE^"_TEMPLATE
S MISC(2)="NAME_MASK^"_TMPRESUL("NAME_MASK")
; ... additional parameters

; Create patient
D PAT^ISIIMPR1(.MISC,.ISIRESUL)
```

## Mask Format

Masks use asterisk (*) as wildcard for random value generation:

### NAME_MASK
- `"*,PATIENT"` → Random last name + ",PATIENT"
- `"TEST*"` → "TEST" + random characters
- `"*"` → Completely random name

### SSN_MASK
- `"00012*"` → "00012" + random 4 digits (max 5 fixed digits)
- `"999*"` → "999" + random 6 digits
- Fixed portion must be ≤5 digits

### ZIP_4_MASK
- `"2210*"` → "2210" + random digit
- `"90210"` → Fixed zip code (no randomization)
- `"*"` → Completely random zip

### PH_NUM_MASK
- `"555*"` → "555" + random 7 digits
- `"410*"` → "410" + random 7 digits
- Must result in valid 10-digit phone

## Processing Flow

1. **Lookup Template**: Find template by name or IEN in file #9001
2. **Retrieve Fields**: Extract all template field values
3. **Build Array**: Populate ISIRESUL with field name/value pairs
4. **Return Result**: Complete template configuration

## Related VistA Files

- **#9001**: ISI PT IMPORT TEMPLATE (source file)
- **#391**: TYPE OF PATIENT
- **#11**: MARITAL STATUS
- **#5**: STATE
- **#49**: SERVICE (military)

## Use Cases

### Template-Based Patient Creation
1. Get template details with this RPC
2. Optionally override specific fields
3. Pass to ISI IMPORT PAT for patient creation

### Template Validation
- Verify template exists
- Review template configuration
- Check mask patterns

### Template Display
- Show template details in UI
- Allow users to preview before use

## Related RPCs

- **[ISI IMPORT GET TEMPLATES](ISI-IMPORT-GET-TEMPLATES.md)** - List all available templates
- **[ISI IMPORT SAVE TEMPLATE](ISI-IMPORT-SAVE-TEMPLATE.md)** - Create/update templates
- **[ISI IMPORT PAT](ISI-IMPORT-PAT.md)** - Use templates to create patients

## Notes

- Templates define defaults that can be overridden
- Masks enable random value generation for testing
- Template values cascade with explicit parameters
- DFN_NAME=Y uses patient IEN as part of name
- All date fields in FileMan format (YYYMMDD)
- Masks support batch creation with varied demographics
