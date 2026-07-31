# ISI IMPORT PAT

## Description
Creates patient records in VistA. Supports both individual patient creation and batch imports using templates. Can perform patient profile merging from an existing patient record.

## RPC Name
`ISI IMPORT PAT`

## Entry Point
[`PNTIMPRT^ISIIMPR1`](../../VistA/Routines/ISIIMPR1.m)

## API Entry Point
[`IMPORTPT^ISIIMP03`](../../VistA/Routines/ISIIMP03.m) - Main API  
[`$$PATIENT^ISIIMP02`](../../VistA/Routines/ISIIMP02.m) - Patient creation

## Input Parameters

### MISC Array
Format: `MISC(n) = "PARAMETER^VALUE"`

| Parameter | Type | VistA File | Required | Description |
|-----------|------|------------|----------|-------------|
| **IMP_TYPE** | PARAM | - | No | Import type: 'I' (Individual) or 'B' (Batch) |
| **IMP_BATCH_NUM** | PARAM | - | No | Batch number to be imported |
| **TEMPLATE** | PARAM | #9001 | No | Template name from ISI PT IMPORT TEMPLATE file |
| **DFN_NAME** | PARAM | - | No | 'Y' or 'N' for use of DFN derived NAME |
| **TYPE** | FIELD | #391 | No | TYPE OF PATIENT (pointer to #391) |
| **NAME** | FIELD | #2,.01 | Yes* | Patient NAME (format: LAST,FIRST) |
| **NAME_MASK** | MASK | #2,.01 | No | Last name mask value (with * wildcard) |
| **SEX** | FIELD | #2,.02 | Yes | SEX: 'M' (Male) or 'F' (Female) |
| **DOB** | FIELD | #2,.03 | Yes* | Date of Birth (VistA date format) |
| **RACE** | FIELD | #2,2.02,.01 | No | RACE INFORMATION (pointer to #10) |
| **ETHNICITY** | FIELD | #2,2.06,.01 | No | ETHNICITY INFORMATION (pointer to #10.2) |
| **LOW_DOB** | PARAM | #2,.03 | No | Lower date limit for automatic DOB generation |
| **UP_DOB** | PARAM | #2,.03 | No | Upper date limit for automatic DOB generation |
| **MARITAL_STATUS** | FIELD | #2,.05 | No | MARITAL STATUS (pointer to #11) |
| **OCCUPATION** | FIELD | #2,.07 | No | OCCUPATION (free text) |
| **SSN** | FIELD | #2,.09 | Yes* | Social Security Number (9 digits) |
| **SSN_MASK** | MASK | #2,.09 | No | SSN mask value (5 digit max, with * wildcard) |
| **STREET_ADD1** | FIELD | #2,.111 | No | Street Address Line 1 |
| **STREET_ADD2** | FIELD | #2,.112 | No | Street Address Line 2 |
| **CITY** | FIELD | #2,.114 | No | CITY |
| **STATE** | FIELD | #2,.115 | No | STATE (pointer to #5) |
| **ZIP_4** | FIELD | #2,.1112 | No | ZIP CODE (ZIP+4 format) |
| **ZIP_4_MASK** | MASK | #2,.1112 | No | Zip code mask value (5 max, with * wildcard) |
| **PH_NUM** | FIELD | #2,.131 | No | PHONE NUMBER [RESIDENCE] |
| **PH_NUM_MASK** | MASK | #2,.131 | No | Phone number mask value (with * wildcard) |
| **EMPLOY_STAT** | FIELD | #2,.31115 | No | EMPLOYMENT STATUS (set value) |
| **INSUR_TYPE** | FIELD | #2,2.312,.01 | No | INSURANCE TYPE (pointer to #36) |
| **VETERAN** | FIELD | #2,1901 | No | VETERAN STATUS ('Y' or 'N') |
| **MRG_SOURCE** | FIELD | #2,.01 | No | Patient name to merge profile from |

\* Required unless using TEMPLATE or mask values for dynamic generation

### Parameter Types

- **FIELD**: Literal value to import directly
- **PARAM**: Internal parameter used for processing logic
- **MASK**: Dynamic value with asterisk (*) wildcard for random generation

### Template Support

If **TEMPLATE** parameter is provided with a valid template name from file #9001, the template values will be loaded first, then any explicitly passed parameters will overlay those values.

## Output

### Success
- `ISIRESUL(0) = 1`
- `ISIRESUL(1) = DFN^SSN^NAME`
  - DFN: Internal entry number in PATIENT file (#2)
  - SSN: Patient's Social Security Number
  - NAME: Patient's full name

### Error
- `ISIRESUL(0) = -1^ERROR_MESSAGE`

Possible error messages:
- `-1^Invalid TEMPLATE name`
- `-1^Bad parameter title passed`
- `-1^Invalid date value in DOB, LO_DOB, or UP_DOB field`
- `-1^No entry found for PATIENT`
- Various VistA FileMan errors

## Example Usage

### Example 1: Individual Patient Creation
```
MISC(1) = "NAME^DOE,JOHN"
MISC(2) = "SEX^M"
MISC(3) = "DOB^3000101"
MISC(4) = "SSN^123456789"
MISC(5) = "STREET_ADD1^123 MAIN ST"
MISC(6) = "CITY^ANYTOWN"
MISC(7) = "STATE^VIRGINIA"
MISC(8) = "ZIP_4^22101"
```

### Example 2: Using Template with Overrides
```
MISC(1) = "TEMPLATE^DEFAULT"
MISC(2) = "NAME^SMITH,JANE"
MISC(3) = "SSN^987654321"
```

### Example 3: Using Masks for Dynamic Generation
```
MISC(1) = "NAME_MASK^*,PATIENT"
MISC(2) = "SEX^F"
MISC(3) = "SSN_MASK^00012*"
MISC(4) = "LOW_DOB^2900101"
MISC(5) = "UP_DOB^3000101"
```

## Related Files

- **#2**: PATIENT file
- **#9001**: ISI PT IMPORT TEMPLATE file
- **#391**: TYPE OF PATIENT file
- **#10**: RACE file
- **#10.2**: ETHNICITY INFORMATION file
- **#11**: MARITAL STATUS file
- **#5**: STATE file
- **#36**: INSURANCE COMPANY file

## Validation

### Performed by: [`VALIDATE^ISIIMPU1`](../../VistA/Routines/ISIIMPU1.m)

Validations include:
- Template name validity (if provided)
- Parameter name validity
- Date format validation for DOB fields
- Required field validation (NAME, SSN, DOB or their mask equivalents)
- FileMan data dictionary constraints

## Notes

- The routine first receives input from the RPC, converts it to a usable array, validates the input, and then performs the import via API
- Template values are loaded first, then overlaid with explicit parameters
- Mask values with asterisks allow for random/dynamic value generation
- MRG_SOURCE allows copying demographics from an existing patient
- The DFN_NAME parameter controls whether patient name is derived from DFN
- This API is the foundation for patient record creation and is used by other import functions
