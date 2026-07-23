# ISI IMPORT LAB

## Description
Creates individual laboratory test results in VistA. This RPC adds lab test results to a patient's laboratory record, including chemistry, hematology, and other test types.

## RPC Name
`ISI IMPORT LAB`

## Entry Point
[`LABMAKE^ISIIMPR2`](../../VistA/Routines/ISIIMPR2.m)

## API Entry Point
[`$$LAB^ISIIMP12`](../../VistA/Routines/ISIIMP12.m) - Main API  
[`$$VALIDATE^ISIIMP13`](../../VistA/Routines/ISIIMP13.m) - Validation

## Input Parameters

### MISC Array for Individual Labs
Format: `MISC(n) = "PARAMETER^VALUE"`

| Parameter | Type | VistA File | Required | Description |
|-----------|------|------------|----------|-------------|
| **PAT_SSN** | FIELD | #2,.09 | Yes | PATIENT SSN or DFN |
| **LAB_TEST** | FIELD | #60,.01 | Yes | Laboratory test name |
| **RESULT_DT** | FIELD | - | Yes | Date/Time of result |
| **RESULT_VAL** | FIELD | - | Yes | Lab test result value |
| **LOCATION** | FIELD | #44 | Yes | Hospital Location name or IEN |
| **COLLECTION_SAMPLE** | FIELD | - | No | Sample type |

## Parameter Details

### PAT_SSN
- **Required**: Yes
- **Input**: SSN (9 digits) or DFN (internal entry number)
- **Priority**: SSN lookup takes priority
- **Validation**: Must exist in PATIENT file (#2)
- **Converted to**: DFN and LRDFN (lab patient pointer)

### LAB_TEST
- **Required**: Yes
- **Input**: Laboratory test name from LABORATORY TEST file (#60)
- **Format options**:
  - Test name: `"CHOLESTEROL"`
  - LOINC code: `"2093-3"` (format with dash)
- **Common tests**:
  - Chemistry: GLUCOSE, SODIUM, POTASSIUM, CHOLESTEROL, CREATININE
  - Hematology: WBC, RBC, HEMOGLOBIN, HEMATOCRIT, PLATELET COUNT
  - Other: TSH, PSA, HBA1C
- **Resolution**: 
  - Searches by name in file #60
  - If LOINC format (contains "-"), uses LOINC2L conversion
  - Must exist in LABORATORY TEST file

### RESULT_DT
- **Required**: Yes
- **Format**: VistA FileMan date/time
- **Default time**: If time component missing, defaults to "12:00" (noon)
- **Conversion**: Supports relative dates (e.g., "T-7" for 7 days ago)
- **Time precision**: Timestamp format supported
- **Duplicate check**: System checks for duplicate lab on same patient/date/test

### RESULT_VAL
- **Required**: Yes
- **Format**: Numeric or text value depending on test
- **Examples**:
  - Numeric: "150" (for glucose mg/dL)
  - Numeric with decimal: "5.2" (for potassium mEq/L)
  - Text: "POSITIVE" or "NEGATIVE"
- **Validation**: Must match expected format for the specific test type

### LOCATION
- **Required**: Yes
- **Input**: Hospital location name or IEN from file #44
- **Validation**: Must exist in HOSPITAL LOCATION file (#44)
- **Converted to**: Internal IEN

### COLLECTION_SAMPLE (Optional)
- **Required**: No
- **Input**: Sample/specimen type
- **Common values**:
  - `BLOOD`
  - `SERUM`
  - `PLASMA`
  - `SPUTUM`
  - `URINE`
  - `ARTERIAL BLOOD`
  - `CSF` (Cerebrospinal Fluid)
- **Default**: Determined by test if not specified

## Output

### Success (Extrinsic Function)
- Returns: `1`
- `RC(0) = 1`
- `RC(1) = "success"`
- Lab result created in LABORATORY file structure

### Error (Extrinsic Function)
- Returns: `-1^ERROR_REASON`
- `RC(0) = 0`
- `RC(1)` not sent

Possible error messages:
- `-1^Bad parameter title passed: [PARAM]`
- `-1^No data provided for parameter: [PARAM]`
- `-1^Invalid RESULT_DT date/time`
- `-1^Invalid PAT_SSN (#2,.09)`
- `-1^Missing Patient SSN`
- `-1^Lab test not found: [TEST_NAME]`
- `-1^Invalid LOCATION`
- `-1^Duplicate Lab Test entry for patient`
- `-1^Unable to create lab result`

## Example Usage

### Example 1: Basic Chemistry Test
```
MISC(1) = "PAT_SSN^123456789"
MISC(2) = "LAB_TEST^GLUCOSE"
MISC(3) = "RESULT_DT^T@08:00"
MISC(4) = "RESULT_VAL^150"
MISC(5) = "LOCATION^PRIMARY CARE"
```

### Example 2: Test with Sample Type
```
MISC(1) = "PAT_SSN^555001234"
MISC(2) = "LAB_TEST^HEMOGLOBIN"
MISC(3) = "RESULT_DT^3000101.0730"
MISC(4) = "RESULT_VAL^14.5"
MISC(5) = "LOCATION^LABORATORY"
MISC(6) = "COLLECTION_SAMPLE^BLOOD"
```

### Example 3: Using LOINC Code
```
MISC(1) = "PAT_SSN^987654321"
MISC(2) = "LAB_TEST^2093-3"
MISC(3) = "RESULT_DT^T-7@09:30"
MISC(4) = "RESULT_VAL^220"
MISC(5) = "LOCATION^CARDIOLOGY"
```

### Example 4: Text Result
```
MISC(1) = "PAT_SSN^111223333"
MISC(2) = "LAB_TEST^URINE CULTURE"
MISC(3) = "RESULT_DT^T-3"
MISC(4) = "RESULT_VAL^NEGATIVE"
MISC(5) = "LOCATION^INFECTIOUS DISEASE"
MISC(6) = "COLLECTION_SAMPLE^URINE"
```

## Related Files

- **#60**: LABORATORY TEST file
- **#63**: LAB DATA file (top level)
- **#63.04**: LAB CHEMISTRY subfile
- **#63.05**: LAB HEMATOLOGY subfile
- **#2**: PATIENT file
- **#44**: HOSPITAL LOCATION file
- **#62**: TOPOGRAPHY FIELD file (specimens)

## Validation

### Performed by: `VALLAB^ISIIMPU7`

Validations include:
- Patient SSN existence and LRDFN assignment
- Lab test existence in file #60
- LOINC code validation (if applicable)
- Result date format
- Location existence
- Duplicate lab test check (same patient, date, test)
- Test result format validation
- Entered by person (auto-assigned if not provided)

## Duplicate Detection

The routine checks for duplicate labs using:
```
$$LABDUP(DFN,RESULT_DT,LAB_TEST)
```

This prevents creating the same lab test for the same patient on the same date/time.

## Processing Flow

1. **Parse Parameters**: Convert MISC array via `LABMISC^ISIIMPU7`
2. **Validate Input**: Verify parameters via `VALLAB^ISIIMPU7`
3. **Resolve Patient**: Convert SSN to DFN and LRDFN
4. **Resolve Test**:
   - If LOINC format (contains "-"), use `$$LOINC2L`
   - Otherwise search LABORATORY TEST file by name
5. **Check Duplicates**: Verify not already entered
6. **Create Lab Result**: Call API `$$LAB^ISIIMP12`
7. **Return Result**: Success (1) or error (-1^message)

## LOINC Code Support

The routine supports LOINC codes in format:
- Input: "2093-3" (LOINC code with dash)
- Conversion: Uses `$$LOINC2L` function
- Maps to: Internal laboratory test IEN

Common LOINC codes:
- 2093-3: Cholesterol
- 2345-7: Glucose
- 2160-0: Creatinine
- 6690-2: White Blood Count

## Lab Data Storage

Results are stored in the VistA LAB module:
- Patient lab record created if doesn't exist (LRDFN)
- Results filed in appropriate subfile (CH, HE, etc.)
- Specimen information attached
- Collection date/time recorded

## Notes

- Default time of 12:00 (noon) is added if only date provided for RESULT_DT
- The routine creates LRDFN (lab patient pointer) if it doesn't exist
- Duplicate detection prevents accidental re-entry of same test
- LOINC codes are automatically converted to internal test IENs
- Collection sample is optional but recommended for clarity
- Each lab test type may have specific validation rules
- The .Net executable does NOT support lab panels (use ISI IMPORT LAB PANEL instead)
- Results are immediately available in VistA lab reporting
- Auto-verification may occur based on test type and result ranges

## Panel Member Check

The utility routine includes a function to check if a test is part of a panel:
```
$$PMEM^ISIIMPU7("PANEL_NAME","LAB_TEST")
```
Returns: 1 if member, 0 if not

This is used by the Synthea FHIR importer to dynamically decide whether to import individual labs or skip them if they're part of a panel being imported separately.

## Version History
- V.1.0 (June 2012): Initial implementation
- V.2.0 (June 2014): LOINC support added
- V.2.1 (November 2014): Enhancements
- V.3.0 (2018): License change to Apache 2.0
- V.3.1 (2024-2025): Lab Panel API added, panel member check function

## See Also
- [ISI IMPORT LAB PANEL](ISI-IMPORT-LAB-PANEL.md) - For importing complete lab panels
