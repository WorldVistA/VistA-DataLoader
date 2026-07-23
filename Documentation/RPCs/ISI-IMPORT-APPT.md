# ISI IMPORT APPT

## Description
Creates appointment entries and associated outpatient encounters in VistA. This RPC schedules appointments for patients at hospital locations and optionally creates a checkout timestamp.

## RPC Name
`ISI IMPORT APPT`

## Entry Point
`APPMAKE^ISIIMPR1`

## API Entry Point
`APPOINT^ISIIMP04()`

## Input Parameters

### MISC Array
Format: `MISC(n) = "PARAMETER^VALUE"`

| Parameter | Type | VistA File | Required | Description |
|-----------|------|------------|----------|-------------|
| **ADATE** | FIELD | #44.001,.01 | Yes | Appointment DATE/TIME (must include time component) |
| **CDATE** | FIELD | #44.003,303 | No | Appointment DATE/TIME CHECKED OUT (must be after ADATE) |
| **CLIN** | FIELD | #44,.01 | Yes | HOSPITAL LOCATION name or IEN |
| **PATIENT** | FIELD | #2,.09 | Yes | PATIENT SSN or DFN |
| **PAT_SSN** | FIELD | #2,.09 | Yes | PATIENT SSN or DFN (alternate parameter name) |
| **PROVIDER** | FIELD | #9000010.06,.01 | No | PROVIDER name or IEN |

## Parameter Details

### ADATE (Appointment Date/Time)
- **Required**: Yes
- **Format**: VistA FileMan date/time (e.g., "3000101.12" for Jan 1, 3000 at 12:00)
- **Must include time**: The time component (after decimal) is required
- **Cannot be future**: Appointment date cannot be in the future
- **Conversion**: Relative dates supported (e.g., "T-1@12:00" for yesterday at noon)

### CDATE (Checkout Date/Time)
- **Required**: No
- **Format**: VistA FileMan date/time
- **Must include time**: The time component is required if provided
- **Constraint**: Must be after ADATE

### CLIN (Hospital Location)
- **Required**: Yes
- **Input**: Location name or IEN from file #44
- **Validation**: 
  - Must exist in HOSPITAL LOCATION file (#44)
  - Must be active on the appointment date
  - Must have STOP CODE NUMBER defined (#44,8)

### PATIENT/PAT_SSN
- **Required**: Yes (use either PATIENT or PAT_SSN)
- **Input**: SSN (9 digits) or DFN (internal entry number)
- **Priority**: SSN takes priority if provided
- **Validation**: Patient must exist in PATIENT file (#2)

### PROVIDER
- **Required**: No
- **Input**: Provider name or IEN from file #200
- **Validation**: Must exist in NEW PERSON file (#200)

## Output

### Success
- `ISIRESUL(0) = 1` (or positive value)
- Appointment created in HOSPITAL LOCATION file (#44)
- Outpatient encounter created

### Error
- `ISIRESUL(0) = -1^ERROR_MESSAGE`

Possible error messages:
- `-1^Bad parameter title passed: [PARAM]`
- `-1^No data provided for parameter: [PARAM]`
- `-1^Invalid appointment date: [DATE]`
- `-1^Future appointment date not allowed`
- `-1^Missing date/time for appt (ADATE)`
- `-1^Missing appt. location (#44)`
- `-1^Missing patient identifier (#2)`
- `-1^Missing time for appt. (ADATE)`
- `-1^Invalid appt date/time (ADATE)`
- `-1^CDATE must be in datetime format`
- `-1^CDATE must be after ADATE`
- `-1^Invalid Appt. location value (#44)`
- `-1^Appt. location inactive on appt. date (#44)`
- `-1^Appt. location missing STOP CODE NUMBER (#44,8)`
- `-1^No entry found for PATIENT (#2)`
- `-1^Bad Provider entry (#200)`

## Example Usage

### Example 1: Basic Appointment
```
MISC(1) = "ADATE^3000101.1200"
MISC(2) = "CLIN^PRIMARY CARE"
MISC(3) = "PATIENT^123456789"
```

### Example 2: Appointment with Provider
```
MISC(1) = "ADATE^T-1@14:30"
MISC(2) = "CLIN^CARDIOLOGY"
MISC(3) = "PAT_SSN^555001234"
MISC(4) = "PROVIDER^SMITH,JOHN"
```

### Example 3: Appointment with Checkout
```
MISC(1) = "ADATE^3000101.0900"
MISC(2) = "CDATE^3000101.0945"
MISC(3) = "CLIN^GENERAL MEDICINE"
MISC(4) = "PATIENT^987654321"
MISC(5) = "PROVIDER^DOE,JANE"
```

### Example 4: Using DFN Instead of SSN
```
MISC(1) = "ADATE^3000201.1030"
MISC(2) = "CLIN^ORTHOPEDICS"
MISC(3) = "PATIENT^1001"
```

## Date/Time Format Examples

VistA FileMan supports various date/time input formats:
- `3000101.12` - January 1, 3000 at 12:00 PM
- `3000101.1430` - January 1, 3000 at 2:30 PM
- `T-1@12:00` - Yesterday at noon
- `T@15:30` - Today at 3:30 PM

## Related Files

- **#44**: HOSPITAL LOCATION file
- **#44.001**: APPOINTMENT (subfile of #44)
- **#44.003**: DATE/TIME CHECKED OUT (subfile of #44)
- **#2**: PATIENT file
- **#200**: NEW PERSON file (providers)
- **#9000010**: OUTPATIENT ENCOUNTER file
- **#9000010.06**: PROVIDER (subfile of #9000010)
- **#40.7**: CLINIC STOP file

## Validation

### Performed by: `VALAPPT^ISIIMPU2`

Validations include:
- Required field presence (ADATE, CLIN, PATIENT)
- Date/time format validation
- Time component presence in ADATE
- Future date prohibition
- CDATE after ADATE check
- Hospital Location existence and active status
- Stop code presence in Hospital Location
- Patient existence
- Provider existence (if provided)

## Processing Flow

1. **Parse Parameters**: Convert MISC array to ISIMISC indexed array via `APPTMISC^ISIIMPU2`
2. **Validate Input**: Verify all parameters via `VALAPPT^ISIIMPU2`
3. **Create Appointment**: Call API `APPOINT^ISIIMP04`
4. **Create Encounter**: Generate corresponding outpatient encounter
5. **Checkout** (optional): If CDATE provided, mark appointment as checked out

## Notes

- The appointment date must include a time component - appointments without times will be rejected
- The routine checks that the clinic stop code is defined, which is required for encounter processing
- SSN lookup takes priority over DFN if both could match
- Future appointments are not allowed by this RPC
- If CDATE (checkout date) is provided, it automatically marks the appointment as checked out
- The provider parameter is optional but recommended for complete encounter documentation
- Appointments create associated outpatient encounter records automatically

## Version History
- V.1.0 (June 2012): Initial implementation
- V.2.0 (June 2014): Updates and enhancements
- V.3.0 (2018): License change to Apache 2.0
- V.3.1 (2024-2025): Continued maintenance
