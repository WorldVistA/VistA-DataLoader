# ISI IMPORT PROB

## Description
Creates problem list entries in VistA. This RPC adds diagnoses/problems to a patient's problem list, including ICD-9/ICD-10 code resolution and SNOMED mapping.

## RPC Name
`ISI IMPORT PROB`

## Entry Point
`PROBMAKE^ISIIMPR1`

## API Entry Point
`PROBLEM^ISIIMP06(.ISIRESUL,.ISIMISC)`

## Input Parameters

### MISC Array
Format: `MISC(n) = "PARAMETER^VALUE"`

| Parameter | Type | VistA File | Required | Description |
|-----------|------|------------|----------|-------------|
| **PROBLEM** | FIELD | #757.01,.01 | Yes | PROBLEM Description or ICD code |
| **PROVIDER** | FIELD | #9000011,1.04 | Yes | PROVIDER NAME or IEN |
| **PAT_SSN** | FIELD | #2,.09 | Yes | PATIENT SSN or DFN |
| **STATUS** | FIELD | #9000011,.12 | Yes | 'A' (Active) or 'I' (Inactive) |
| **ENTERED** | FIELD | #9000011,.8 | No | DATE ENTERED (also sets 1.09 DATE RECORDED if not supplied) |
| **ONSET** | FIELD | #9000011,.13 | No | Onset DATE |
| **RESOLVED** | FIELD | #9000011,1.07 | No | Date Resolved |
| **LOCATION** | FIELD | #9000011,1.08 | No | CLINIC LOCATION name or IEN |
| **RECORDED** | FIELD | #9000011,1.09 | No | DATE RECORDED |
| **TYPE** | FIELD | #9000011,1.14 | Yes | PRIORITY: 'A' (Acute) or 'C' (Chronic) |
| **VPOV** | PARAM | - | No | 'Y' or 'N' to try to create V POV entry |

## Parameter Details

### PROBLEM
- **Required**: Yes
- **Input**: Problem description, ICD-9 code, ICD-10 code, or SNOMED code
- **Resolution**: 
  - Searches CLINICAL LEXICON file (#757.01)
  - Supports ICD-9 format (e.g., "250.00")
  - Supports ICD-10 format (e.g., "E11.9")
  - Supports SNOMED codes
  - Auto-maps to ICD codes via Lexicon
- **Fallback**: If SNOMED found but no ICD mapping, defaults to "799.9" (hard coded)

### PROVIDER
- **Required**: Yes
- **Input**: Provider name or IEN from file #200
- **Validation**: Must exist in NEW PERSON file (#200)

### PAT_SSN
- **Required**: Yes
- **Input**: SSN (9 digits) or DFN (internal entry number)
- **Priority**: SSN lookup takes priority if provided
- **Validation**: Must exist in PATIENT file (#2)

### STATUS
- **Required**: Yes
- **Values**: 
  - `A` = Active
  - `I` = Inactive

### ENTERED, ONSET, RESOLVED, RECORDED
- **Format**: VistA FileMan date or date/time
- **Validation**: Must be valid VistA date format
- **Conversion**: Supports relative dates (e.g., "T-30" for 30 days ago)

### TYPE (Priority)
- **Required**: Yes
- **Values**:
  - `A` = Acute
  - `C` = Chronic

### VPOV
- **Required**: No
- **Values**: 'Y' or 'N'
- **Purpose**: If 'Y', attempts to also create a V POV (Purpose of Visit) entry

## Output

### Success
- `ISIRESUL(0) = 1`
- `ISIRESUL(1) = IEN` (Internal entry number in PROBLEM file #9000011)

### Error
- `ISIRESUL(0) = -1^ERROR_MESSAGE`

Possible error messages:
- `-1^Bad parameter title passed: [PARAM]`
- `-1^No data provided for parameter: [PARAM]`
- `-1^Invalid ONSET, ENTERED, or RESOLVED date`
- `-1^Missing PROBLEM param`
- `-1^Missing value for PROBLEM`
- `-1^Invalid PAT_SSN (#2,.09)`
- `-1^Missing PROVIDER param`
- `-1^Missing STATUS param`
- `-1^Invalid STATUS value`
- `-1^Missing TYPE param`
- `-1^Invalid TYPE value`
- `-1^Problem not found in Lexicon`
- `-1^ICD code mapping failed`

## Example Usage

### Example 1: Basic Problem Entry
```
MISC(1) = "PROBLEM^DIABETES MELLITUS"
MISC(2) = "PROVIDER^SMITH,JOHN"
MISC(3) = "PAT_SSN^123456789"
MISC(4) = "STATUS^A"
MISC(5) = "TYPE^C"
```

### Example 2: Problem with ICD-9 Code
```
MISC(1) = "PROBLEM^250.00"
MISC(2) = "PROVIDER^126"
MISC(3) = "PAT_SSN^555001234"
MISC(4) = "STATUS^A"
MISC(5) = "TYPE^C"
MISC(6) = "ONSET^T-90"
```

### Example 3: Complete Problem Entry
```
MISC(1) = "PROBLEM^HYPERTENSION"
MISC(2) = "PROVIDER^DOE,JANE"
MISC(3) = "PAT_SSN^987654321"
MISC(4) = "STATUS^A"
MISC(5) = "TYPE^C"
MISC(6) = "ONSET^3000101"
MISC(7) = "ENTERED^T"
MISC(8) = "LOCATION^PRIMARY CARE"
MISC(9) = "VPOV^Y"
```

### Example 4: Resolved Problem
```
MISC(1) = "PROBLEM^ACUTE BRONCHITIS"
MISC(2) = "PROVIDER^BROWN,ROBERT"
MISC(3) = "PAT_SSN^111223333"
MISC(4) = "STATUS^I"
MISC(5) = "TYPE^A"
MISC(6) = "ONSET^T-14"
MISC(7) = "RESOLVED^T"
```

## Related Files

- **#9000011**: PROBLEM file (V PROBLEM LIST)
- **#757.01**: EXPRESSIONS file (Clinical Lexicon)
- **#757.02**: CODES file (Lexicon codes including ICD)
- **#757.03**: CODE SYSTEMS file
- **#80**: ICD DIAGNOSIS file
- **#2**: PATIENT file
- **#200**: NEW PERSON file (providers)
- **#44**: HOSPITAL LOCATION file

## Validation

### Performed by: `VALPROB^ISIIMPU4`

Validations include:
- Problem existence/resolution in Clinical Lexicon
- ICD code mapping (ICD-9 or ICD-10)
- SNOMED code resolution
- Patient SSN existence
- Provider existence
- Status value ('A' or 'I')
- Type value ('A' or 'C')
- Date format validation
- Location existence (if provided)

## Processing Flow

1. **Parse Parameters**: Convert MISC array via `PROBMISC^ISIIMPU4`
2. **Validate Input**: Verify parameters via `VALPROB^ISIIMPU4`
3. **Resolve Problem**:
   - Search Clinical Lexicon (#757.01) by description
   - If ICD code format, lookup in ICD files
   - Map SNOMED codes to ICD equivalents
   - Use Lexicon cross-references for mapping
4. **Create Problem**: Add to PROBLEM file (#9000011)
5. **Create VPOV** (optional): If VPOV='Y', create V POV entry
6. **Return Result**: IEN or error code

## Lexicon Mapping Logic

The routine uses sophisticated logic to resolve problems:
1. Searches by CODE index if value looks like ICD code
2. Searches by ACODE index for alternate codes
3. Checks SNOMED mappings in Lexicon
4. Falls back to description search in EXPRESSIONS file
5. Uses `$$ICD^VPRDVSIT` for ICD code validation
6. Hard-coded fallback to "799.9" for unmapped SNOMED codes

## Notes

- The routine performs extensive lexicon mapping to ensure proper ICD code assignment
- SNOMED codes are automatically mapped to ICD equivalents when possible
- The VPOV parameter allows simultaneous creation of visit diagnosis
- Both ICD-9 and ICD-10 codes are supported
- The ENTERED date also populates DATE RECORDED if RECORDED not explicitly provided
- Problems are stored in the V PROBLEM LIST file (#9000011)
- The routine validates that ICD codes are active/valid for the date entered

## Version History
- V.1.0 (June 2012): Initial implementation
- V.2.0 (June 2014): ICD-10 support added
- V.3.0 (2018): License change to Apache 2.0
- V.3.1 (2024-2025): Continued maintenance and SNOMED enhancements
