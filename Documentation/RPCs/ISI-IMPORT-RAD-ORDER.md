# ISI IMPORT RAD ORDER

## Description
Creates radiology orders in VistA. This RPC generates imaging procedure orders including X-rays, CT scans, MRIs, and other radiological examinations.

## RPC Name
`ISI IMPORT RAD ORDER`

## Entry Point
[`RADOMAKE^ISIIMPR1`](../../VistA/Routines/ISIIMPR1.m)

## API Entry Point
[`$$RADORDER^ISIIMP20`](../../VistA/Routines/ISIIMP20.m) - Main API  
[`$$VALIDATE^ISIIMP21`](../../VistA/Routines/ISIIMP21.m) - Validation

## Input Parameters

### MISC Array
Format: `MISC(n) = "PARAMETER^VALUE"`

| Parameter | Type | VistA File | Required | Description |
|-----------|------|------------|----------|-------------|
| **PAT_SSN** | FIELD | #2,.09 | Yes | PATIENT SSN or DFN |
| **RAPROC** | FIELD | #71,.01 | Yes | Radiology procedure name or IEN |
| **MAGLOC** | FIELD | #79.1,.01 | Yes | Imaging location name or IEN |
| **PROV** | FIELD | #200,.01 | Yes | Provider (ordering physician) |
| **RADTE** | FIELD | #70.02 | Yes | Exam date/time |
| **EXAMCAT** | FIELD | #75.1,4 | Yes | Exam category (I,O,C,S,E,R) |
| **REQLOC** | FIELD | #44,.01 | Yes | Request location (Hospital Location) |
| **REASON** | FIELD | #75.1,1.1 | No | Reason for exam (free text) |
| **HISTORY** | FIELD | #75.1,400 | No | Clinical history (free text) |
| **TECH** | FIELD | #70.12,.01 | No | Technologist (for Examined status) |
| **TECHCOMM** | FIELD | #70.07,4 | No | Technologist comments |
| **EXAM_STATUS** | PARAM | - | No | Order progression: O,R,E,C |

## Parameter Details

### PAT_SSN
- **Required**: Yes
- **Input**: SSN (9 digits) or DFN
- **Priority**: SSN lookup takes priority
- **Validation**: Must exist in PATIENT file (#2)
- **Converted to**: DFN

### RAPROC (Radiology Procedure)
- **Required**: Yes
- **Input**: Procedure name or IEN from RADIOLOGY/NUCLEAR MEDICINE PROCEDURES file (#71)
- **Common procedures**:
  - CHEST 2 VIEWS
  - CT HEAD WITHOUT CONTRAST
  - MRI BRAIN WITH AND WITHOUT CONTRAST
  - ULTRASOUND ABDOMEN COMPLETE
  - MAMMOGRAM BILATERAL
  - XRAY KNEE 2 VIEWS
- **Validation**: Must exist and be active (not inactivated)
- **Converted to**: Internal IEN

### MAGLOC (Imaging Location)
- **Required**: Yes
- **Input**: Imaging location name or IEN from IMAGING LOCATIONS file (#79.1)
- **Validation**:
  - Must exist in file #79.1
  - Must be active (inactivation date check)
  - TYPE OF IMAGING must match procedure's imaging type
- **Type Matching**: Location's imaging type (#79.1,6) must match procedure's type (#71,12)
- **Converted to**: Internal IEN (resolves via Hospital Location first)

### PROV (Provider)
- **Required**: Yes
- **Input**: Provider name or IEN from NEW PERSON file (#200)
- **Validation**:
  - Must exist in file #200
  - Must be authorized to write medical orders
  - Must have PROVIDER security key
- **Converted to**: Internal IEN

### RADTE (Exam Date/Time)
- **Required**: Yes
- **Format**: VistA FileMan date/time
- **Purpose**: When the exam is/was performed
- **Validation**: Must be valid date/time format

### EXAMCAT (Exam Category)
- **Required**: Yes
- **Values**:
  - `I` = Inpatient
  - `O` = Outpatient
  - `C` = Contract
  - `S` = Sharing Agreement
  - `E` = Employee
  - `R` = Research
- **Validation**: Must be one of the valid category codes

### REQLOC (Request Location)
- **Required**: Yes
- **Input**: Hospital location name or IEN from file #44
- **Validation**: Must exist in HOSPITAL LOCATION file (#44)
- **Purpose**: Where the order was requested/originated
- **Converted to**: Internal IEN

### REASON
- **Required**: No
- **Input**: Free text reason for exam
- **Purpose**: Clinical indication for imaging
- **Example**: "Rule out pneumonia", "Evaluate for fracture"

### HISTORY
- **Required**: No
- **Input**: Free text clinical history
- **Purpose**: Patient's relevant medical history for radiologist
- **Example**: "Fall on ice yesterday, unable to bear weight"

### TECH (Technologist)
- **Required**: No (required for EXAM_STATUS='E')
- **Input**: Technologist name or IEN from NEW PERSON file (#200)
- **Validation**: Must have radiology classification in file #200,"RAC"
- **Purpose**: Who performed the exam

### TECHCOMM (Tech Comments)
- **Required**: No
- **Input**: Free text comments from technologist
- **Purpose**: Technical notes about exam performance

### EXAM_STATUS
- **Required**: No
- **Values**:
  - `O` = Ordered (default)
  - `R` = Requested
  - `E` = Examined (requires TECH parameter)
  - `C` = Complete
- **Purpose**: How far to advance the order workflow

## Output

### Success
- `ISIRESUL(0) = 1` (or positive value)
- Radiology order created

### Error
- `ISIRESUL(0) = -1^ERROR_MESSAGE`

Possible error messages:
- `-1^Bad parameter title passed: [PARAM]`
- `-1^No data provided for parameter: [PARAM]`
- `-1^Invalid RADTE (Exam Date/Time)`
- `-1^Missing Patient SSN`
- `-1^Invalid PAT_SSN (#2,.09)`
- `-1^Missing RAPROC (#71,.01)`
- `-1^Invalid RAPROC (#71,.01)`
- `-1^Missing MAGLOC (IMAGING LOCATIONS #79.1)`
- `-1^Invalid MAGLOC (IMAGING LOCATIONS #79.1)`
- `-1^TYPE OF IMAGING (#79.2) and IMAGING LOCATION (#79.1) don't match`
- `-1^Missing PROV`
- `-1^Invalid PROV value (#200, .01)`
- `-1^Missing RADTE`
- `-1^Missing EXAMCAT`
- `-1^Missing REQLOC`

## Example Usage

### Example 1: Basic Chest X-Ray
```
MISC(1) = "PAT_SSN^123456789"
MISC(2) = "RAPROC^CHEST 2 VIEWS"
MISC(3) = "MAGLOC^RADIOLOGY MAIN"
MISC(4) = "PROV^SMITH,JOHN"
MISC(5) = "RADTE^T@14:00"
MISC(6) = "EXAMCAT^O"
MISC(7) = "REQLOC^PRIMARY CARE"
MISC(8) = "REASON^Cough and fever"
MISC(9) = "HISTORY^3 day history of productive cough"
```

### Example 2: CT Scan with Status
```
MISC(1) = "PAT_SSN^555001234"
MISC(2) = "RAPROC^CT HEAD WITHOUT CONTRAST"
MISC(3) = "MAGLOC^CT SCAN"
MISC(4) = "PROV^DOE,JANE"
MISC(5) = "RADTE^3000115.1530"
MISC(6) = "EXAMCAT^I"
MISC(7) = "REQLOC^ER"
MISC(8) = "REASON^Head trauma"
MISC(9) = "HISTORY^MVA with loss of consciousness"
MISC(10) = "EXAM_STATUS^E"
MISC(11) = "TECH^JONES,MARY"
```

### Example 3: MRI Order
```
MISC(1) = "PAT_SSN^987654321"
MISC(2) = "RAPROC^MRI LUMBAR SPINE WITHOUT CONTRAST"
MISC(3) = "MAGLOC^MRI"
MISC(4) = "PROV^BROWN,ROBERT"
MISC(5) = "RADTE^T+7@09:00"
MISC(6) = "EXAMCAT^O"
MISC(7) = "REQLOC^NEUROLOGY"
MISC(8) = "REASON^Chronic low back pain"
MISC(9) = "HISTORY^Failed conservative therapy"
```

### Example 4: Outpatient Mammogram
```
MISC(1) = "PAT_SSN^111223333"
MISC(2) = "RAPROC^MAMMOGRAM BILATERAL"
MISC(3) = "MAGLOC^BREAST IMAGING"
MISC(4) = "PROV^WILSON,SARAH"
MISC(5) = "RADTE^T+30@10:00"
MISC(6) = "EXAMCAT^O"
MISC(7) = "REQLOC^WOMENS HEALTH"
MISC(8) = "REASON^Routine screening"
```

## Exam Categories

| Code | Category | Description |
|------|----------|-------------|
| I | Inpatient | Patient is admitted |
| O | Outpatient | Ambulatory patient |
| C | Contract | External facility |
| S | Sharing Agreement | VA sharing agreement |
| E | Employee | VA employee |
| R | Research | Research protocol |

## Related Files

- **#2**: PATIENT file
- **#71**: RADIOLOGY/NUCLEAR MEDICINE PROCEDURES file
- **#79.1**: IMAGING LOCATIONS file
- **#79.2**: TYPE OF IMAGING file
- **#44**: HOSPITAL LOCATION file
- **#200**: NEW PERSON file
- **#70**: RAD/NUC MED PATIENT file
- **#70.02**: REGISTERED EXAMS subfile
- **#70.07**: EXAMINATION STATUS subfile
- **#70.12**: TECHNOLOGIST subfile
- **#75.1**: RAD/NUC MED ORDERS file

## Validation

### Performed by: `VALRADO^ISIIMPUC`

Validations include:
- Patient SSN existence
- Radiology procedure existence and active status
- Imaging location existence and active status
- TYPE OF IMAGING match between procedure and location
- Provider existence and authority
- Exam date/time format
- Exam category validity
- Request location existence
- Technologist classification (if TECH provided)

## Imaging Type Matching

Critical validation: The procedure's TYPE OF IMAGING must match the location's imaging type:
- Procedure field #71,12 (TYPE OF IMAGING pointer to #79.2)
- Location field #79.1,6 (TYPE OF IMAGING pointer to #79.2)

If mismatch occurs:
```
-1^TYPE OF IMAGING (#79.2) and IMAGING LOCATION (#79.1) don't match
```

## Processing Flow

1. **Parse Parameters**: Convert MISC array via `RADMISC^ISIIMPUC`
2. **Validate Input**: Verify parameters via `VALRADO^ISIIMPUC`
3. **Resolve References**:
   - Convert PAT_SSN to DFN
   - Convert RAPROC name to IEN, check active
   - Convert MAGLOC: Hospital Location → Imaging Location
   - Validate imaging type match
   - Convert PROV name to IEN
   - Convert REQLOC name to IEN
   - Convert TECH name to IEN (if provided)
4. **Create Order**: Call API `$$RADORDER^ISIIMP20`
5. **Advance Status**: Progress to EXAM_STATUS if specified
6. **Return Result**: Success or error code

## Notes

- Imaging location is resolved through Hospital Location (#44) first, then to Imaging Location (#79.1)
- TYPE OF IMAGING must match between procedure and location
- Procedure must be active (inactivation date check)
- Location must be active (inactivation date check)
- TECH parameter is required if EXAM_STATUS is set to 'E' (Examined)
- Default EXAM_STATUS is 'O' (Ordered) if not specified
- Provider must have PROVIDER security key
- Technologist must have radiology classification in file #200,"RAC"
