# ISI IMPORT LAB PANEL

## Description
Creates complete laboratory panels with multiple test results in VistA. This RPC adds an entire panel of lab tests (e.g., Basic Metabolic Panel, Complete Blood Count) to a patient's laboratory record in a single operation.

## RPC Name
`ISI IMPORT LAB PANEL`

## Entry Point
`LABPANEL^ISIIMPR2`

## API Entry Point
`$$LAB^ISIIMP12(.RC,.ISIMISC)`

*Note: Same API as individual labs, distinguished by presence of LAB_PANEL parameter*

## Input Parameters

### MISC Array for Lab Panels
Format: `MISC(n) = "PARAMETER^VALUE"`

| Parameter | Type | VistA File | Required | Description |
|-----------|------|------------|----------|-------------|
| **PAT_SSN** | FIELD | #2,.09 | Yes | PATIENT SSN or DFN |
| **LAB_PANEL** | FIELD | - | Yes | Laboratory panel name |
| **LAB_TEST** | MULTIPLE | #60,.01 | Yes | Test name^value (multiple entries) |
| **RESULT_DT** | FIELD | - | Yes | Date/Time of panel collection |
| **LOCATION** | FIELD | #44 | Yes | Hospital Location name or IEN |
| **COLLECTION_SAMPLE** | FIELD | - | No | Sample type (BLOOD, SERUM, etc.) |

## Parameter Details

### PAT_SSN
- **Required**: Yes
- **Input**: SSN (9 digits) or DFN (internal entry number)
- **Priority**: SSN lookup takes priority
- **Validation**: Must exist in PATIENT file (#2)
- **Converted to**: DFN and LRDFN

### LAB_PANEL
- **Required**: Yes
- **Input**: Panel name
- **Common panels**:
  - `BASIC METABOLIC PANEL` (BMP)
  - `COMPREHENSIVE METABOLIC PANEL` (CMP)
  - `COMPLETE BLOOD COUNT` (CBC)
  - `LIPID PANEL`
  - `HEPATIC FUNCTION PANEL`
  - `RENAL PANEL`
  - `THYROID PANEL`
  - `ELECTROLYTE PANEL`
- **Purpose**: Groups related tests together logically
- **Note**: Panel name is used for organization; individual tests are what get filed

### LAB_TEST (Multiple)
- **Required**: Yes (at least one, typically multiple)
- **Format**: `"LAB_TEST^TEST_NAME^VALUE"`
- **Input method**: Multiple MISC entries, each with test name and value
- **Structure**: 
  ```
  MISC(n) = "LAB_TEST^TEST_NAME1^VALUE1"
  MISC(n+1) = "LAB_TEST^TEST_NAME2^VALUE2"
  MISC(n+2) = "LAB_TEST^TEST_NAME3^VALUE3"
  ```
- **Internal storage**: `ISIMISC("LAB_TEST",TEST_NAME) = VALUE`
- **Validation**: Each test name must exist in LABORATORY TEST file (#60)

### RESULT_DT
- **Required**: Yes
- **Format**: VistA FileMan date/time
- **Default time**: If time component missing, defaults to ".12" (12:00)
- **Applies to**: All tests in the panel (same collection time)

### LOCATION
- **Required**: Yes
- **Input**: Hospital location name or IEN
- **Validation**: Must exist in HOSPITAL LOCATION file (#44)

### COLLECTION_SAMPLE
- **Required**: No
- **Common values**: BLOOD, SERUM, PLASMA, URINE, etc.
- **Applies to**: All tests in the panel

## Common Lab Panels

### Basic Metabolic Panel (BMP)
```
LAB_PANEL: BASIC METABOLIC PANEL
Tests: GLUCOSE, CALCIUM, SODIUM, POTASSIUM, CO2, CHLORIDE, BUN, CREATININE
```

### Complete Blood Count (CBC)
```
LAB_PANEL: COMPLETE BLOOD COUNT
Tests: WBC, RBC, HEMOGLOBIN, HEMATOCRIT, MCV, MCH, MCHC, PLATELET COUNT
```

### Comprehensive Metabolic Panel (CMP)
```
LAB_PANEL: COMPREHENSIVE METABOLIC PANEL
Tests: GLUCOSE, CALCIUM, SODIUM, POTASSIUM, CO2, CHLORIDE, BUN, CREATININE,
       ALBUMIN, TOTAL PROTEIN, ALT, AST, ALKALINE PHOSPHATASE, TOTAL BILIRUBIN
```

### Lipid Panel
```
LAB_PANEL: LIPID PANEL
Tests: CHOLESTEROL, TRIGLYCERIDE, HDL, LDL, VLDL
```

## Output

### Success
- Returns: `1`
- `RC(0) = 1`
- `RC(1) = "success"`
- All panel tests created in laboratory files

### Error
- Returns: `-1^ERROR_REASON`
- `RC(0) = 0`
- Possible partial creation if some tests succeed before error

Possible error messages:
- `-1^Bad parameter title passed: [PARAM]`
- `-1^No data provided for parameter: [PARAM]`
- `-1^Invalid RESULT_DT date/time`
- `-1^Missing LAB_PANEL`
- `-1^Invalid LAB_TEST with test [TEST_NAME]`
- `-1^Invalid LAB_TEST with lab value [VALUE]`
- `-1^Invalid PAT_SSN (#2,.09)`
- `-1^Missing Patient SSN`
- `-1^Unable to create lab panel`

## Example Usage

### Example 1: Basic Metabolic Panel
```
MISC(1) = "PAT_SSN^123456789"
MISC(2) = "LAB_PANEL^BASIC METABOLIC PANEL"
MISC(3) = "LAB_TEST^GLUCOSE^95"
MISC(4) = "LAB_TEST^CALCIUM^9.2"
MISC(5) = "LAB_TEST^SODIUM^140"
MISC(6) = "LAB_TEST^POTASSIUM^4.0"
MISC(7) = "LAB_TEST^CO2^24"
MISC(8) = "LAB_TEST^CHLORIDE^102"
MISC(9) = "LAB_TEST^BUN^15"
MISC(10) = "LAB_TEST^CREATININE^1.0"
MISC(11) = "RESULT_DT^T@08:00"
MISC(12) = "LOCATION^LABORATORY"
MISC(13) = "COLLECTION_SAMPLE^BLOOD"
```

### Example 2: Complete Blood Count
```
MISC(1) = "PAT_SSN^555001234"
MISC(2) = "LAB_PANEL^COMPLETE BLOOD COUNT"
MISC(3) = "LAB_TEST^WBC^7.5"
MISC(4) = "LAB_TEST^RBC^4.8"
MISC(5) = "LAB_TEST^HEMOGLOBIN^14.5"
MISC(6) = "LAB_TEST^HEMATOCRIT^42"
MISC(7) = "LAB_TEST^MCV^88"
MISC(8) = "LAB_TEST^MCH^30"
MISC(9) = "LAB_TEST^MCHC^34"
MISC(10) = "LAB_TEST^PLATELET COUNT^250"
MISC(11) = "RESULT_DT^3000115.0730"
MISC(12) = "LOCATION^PRIMARY CARE"
MISC(13) = "COLLECTION_SAMPLE^BLOOD"
```

### Example 3: Lipid Panel
```
MISC(1) = "PAT_SSN^987654321"
MISC(2) = "LAB_PANEL^LIPID PANEL"
MISC(3) = "LAB_TEST^CHOLESTEROL^180"
MISC(4) = "LAB_TEST^TRIGLYCERIDE^120"
MISC(5) = "LAB_TEST^HDL^55"
MISC(6) = "LAB_TEST^LDL^110"
MISC(7) = "LAB_TEST^VLDL^24"
MISC(8) = "RESULT_DT^T-30@09:00"
MISC(9) = "LOCATION^CARDIOLOGY"
MISC(10) = "COLLECTION_SAMPLE^SERUM"
```

### Example 4: Hepatic Function Panel
```
MISC(1) = "PAT_SSN^111223333"
MISC(2) = "LAB_PANEL^HEPATIC FUNCTION PANEL"
MISC(3) = "LAB_TEST^ALBUMIN^4.0"
MISC(4) = "LAB_TEST^TOTAL PROTEIN^7.2"
MISC(5) = "LAB_TEST^ALT^25"
MISC(6) = "LAB_TEST^AST^22"
MISC(7) = "LAB_TEST^ALKALINE PHOSPHATASE^75"
MISC(8) = "LAB_TEST^TOTAL BILIRUBIN^0.8"
MISC(9) = "RESULT_DT^T-7"
MISC(10) = "LOCATION^GASTROENTEROLOGY"
MISC(11) = "COLLECTION_SAMPLE^BLOOD"
```

## Related Files

- **#60**: LABORATORY TEST file
- **#63**: LAB DATA file (top level)
- **#63.04**: LAB CHEMISTRY subfile
- **#63.05**: LAB HEMATOLOGY subfile
- **#2**: PATIENT file
- **#44**: HOSPITAL LOCATION file

## Validation

### Performed by: `VALPANEL^ISIIMPU7`

Validations include:
- LAB_PANEL parameter presence
- Patient SSN existence
- Each test name validity
- Each test value presence
- Result date format
- Location existence
- Collection sample validity (if provided)

## Processing Flow

1. **Parse Parameters**: Convert MISC array via `LABPMISC^ISIIMPU7`
   - Special handling for multiple LAB_TEST entries
   - Creates indexed array: `ISIMISC("LAB_TEST",TEST_NAME) = VALUE`
2. **Validate Panel**: Check LAB_PANEL parameter exists
3. **Validate Tests**: Verify each test in the panel
4. **Validate Common Params**: Patient, date, location
5. **Create Results**: Call API `$$LAB^ISIIMP12` once for entire panel
6. **File Tests**: Each test filed individually with same metadata
7. **Return Result**: Success or error code

## Panel vs Individual Tests

### Use Panel RPC When:
- Importing a standard clinical panel (BMP, CBC, CMP, etc.)
- All tests collected at the same time from the same specimen
- Want logical grouping of related tests
- Processing data from lab instruments that report panels

### Use Individual Lab RPC When:
- Importing single, standalone tests
- Tests collected at different times
- Different specimens for different tests
- Processing individual test orders

## MISC Array Parsing Difference

The panel routine uses special parsing for LAB_TEST:
```mumps
S TESTNAME=$P(VALUE,U,1)
S LABVALUE=$P(VALUE,U,2)
S @DSTNODE@(PARAM,TESTNAME)=LABVALUE
```

This creates a subscripted array structure allowing multiple tests under one parameter name.

## Unit Tests

Example unit tests available in routine ISIIMPLT (not in KIDS build):
```mumps
DO LABMAKE^ISIIMPR2(.RC,.SAM)
```

## Limitations

**Important**: The .NET executable in the Data Loader application does NOT support lab panels. To use lab panels, you must:
1. Use the RPC directly from another client
2. Use the VistA FHIR Data Loader (Synthea importer)
3. Create custom integration code

## Notes

- All tests in a panel share the same RESULT_DT, LOCATION, and COLLECTION_SAMPLE
- Panel name is organizational; VistA files individual test results
- Each test must exist in LABORATORY TEST file (#60)
- Default time ".12" (noon) added if not specified in RESULT_DT
- Tests are validated individually before panel creation begins
- Empty LAB_PANEL parameter causes specific error: "Missing LAB_PANEL"
- The same API ($$LAB^ISIIMP12) handles both individual and panel imports
- Panel member check function available: `$$PMEM^ISIIMPU7(PANEL,TEST)`

## Version History
- V.2.1 (November 2014): Initial panel support for QRDA
- V.2.5 (2015): Enhancements and bug fixes
- V.3.0 (2018): License change to Apache 2.0
- V.3.1 (2024-2025): Complete panel API implementation, panel member function

## See Also
- [ISI IMPORT LAB](ISI-IMPORT-LAB.md) - For importing individual lab tests
