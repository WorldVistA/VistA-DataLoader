# ISI IMPORT ADMIT

## Description
Creates admission records in VistA for inpatient stays. This RPC handles patient admissions to wards/rooms/beds and optionally processes discharges if a discharge date is provided.

## RPC Name
`ISI IMPORT ADMIT`

## Entry Point
[`ADMIT^ISIIMPR3`](../../VistA/Routines/ISIIMPR3.m)

## API Entry Points
- [`ADMIT^ISIIMP25`](../../VistA/Routines/ISIIMP25.m) - Creates admission
- [`DISCHARG^ISIIMP26`](../../VistA/Routines/ISIIMP26.m) - Processes discharge (if DDATE provided)

## Status
**⚠️ DEVELOPMENT STATUS**: According to source documentation, this API is marked as "DO NOT USE (*** still in development***)". Use with caution in production environments.

## Input Parameters

### MISC Array
Format: `MISC(n) = "PARAMETER^VALUE"`

| Parameter | Type | VistA File | Required | Description |
|-----------|------|------------|----------|-------------|
| **PATIENT** | FIELD | #2,.02 | Yes | PATIENT SSN or DFN |
| **PAT_SSN** | FIELD | #2,.09 | Yes | PATIENT SSN or DFN (alternate) |
| **ADATE** | FIELD | - | Yes | ADMIT DATE/TIME (must include time) |
| **DDATE** | FIELD | - | No | Discharge/Disposition DATE/TIME |
| **WARD** | FIELD | #42 | Yes | ADMITTING WARD name or IEN |
| **RMBD** | FIELD | #405.4 | Yes | ROOM-BED name or IEN |
| **ATYPE** | FIELD | #405.1 | No | Admission type (default: "NON-VETERAN") |
| **DTYPE** | FIELD | - | No | Disposition Type |
| **ADMREG** | FIELD | - | No | ADMITTING Regulations |
| **PROVIDER** | FIELD | #200 | No | ADMITTING PHYSICIAN name or IEN |
| **FDEXC** | FIELD | - | No | Facility Directory Exclude |
| **FTSPEC** | FIELD | #45.7 | No | Facility Treating Specialty (default: "MEDICAL") |
| **SHDIAG** | FIELD | - | No | Brief description of the diagnosis |

## Parameter Details

### PATIENT/PAT_SSN
- **Required**: Yes
- **Input**: SSN (9 digits) or DFN (internal entry number)
- **Priority**: SSN lookup takes priority if provided
- **Validation**: Must exist in PATIENT file (#2)

### ADATE (Admission Date/Time)
- **Required**: Yes
- **Format**: VistA FileMan date/time
- **Must include time**: Time component (after decimal) is required
- **Conversion**: Supports relative dates (e.g., "T-1@12:00")

### DDATE (Discharge Date/Time)
- **Required**: No
- **Format**: VistA FileMan date/time
- **Must include time**: Time component is required if provided
- **Processing**: If provided, discharge processing is automatically performed after admission
- **Validation**: Must be after ADATE

### WARD
- **Required**: Yes
- **Input**: Ward name or IEN from file #42
- **Validation**: 
  - Must exist in WARD LOCATION file (#42)
  - Must be active on the admission date
  - Associated Hospital Location must be active

### RMBD (Room-Bed)
- **Required**: Yes
- **Input**: Room-Bed designation or IEN from file #405.4
- **Format**: Typically "WARD-ROOM-BED" (e.g., "3E-100-5")
- **Validation**: Must exist in ROOM-BED file (#405.4)

### ATYPE (Admission Type)
- **Required**: No
- **Default**: "NON-VETERAN"
- **Input**: Admission type from file #405.1
- **File**: FACILITY MOVEMENT TYPE (#405.1)
- **Common values**: "NON-VETERAN", "VETERAN", "SERVICE CONNECTED"

### FTSPEC (Facility Treating Specialty)
- **Required**: No
- **Default**: "MEDICAL"
- **Input**: Treating specialty from file #45.7
- **File**: FACILITY TREATING SPECIALTY (#45.7)
- **Common values**: "MEDICAL", "SURGICAL", "PSYCHIATRIC", "REHABILITATION"

### PROVIDER
- **Required**: No
- **Input**: Provider name or IEN from file #200
- **Validation**: Must exist in NEW PERSON file (#200)

## Output

### Success
- `ISIRESUL(0) = 1`
- Admission movement created
- If DDATE provided: Discharge movement also created

### Error
- `ISIRESUL(0) = -1^ERROR_MESSAGE`

Possible error messages:
- `-1^Bad parameter title passed: [PARAM]`
- `-1^No data provided for parameter: [PARAM]`
- `-1^Invalid date`
- `-1^No entry found for PATIENT (#2)`
- `-1^Missing/invalid time for admit`
- `-1^Cannot determine FACILITY IEN (Admit)`
- `-1^Invalid WARD (#42)`
- `-1^WARD location inactive on admit date (#42)`
- `-1^Invalid ROOM-BED (#405.4)`
- `-1^Cannot determine Admission Type (#405.1)`
- `-1^Cannot determine Facility treating specialty`
- `-1^ERROR IN ADMMISC~ISIIMPUF:[error]`
- `-1^ERROR IN VALADMIT~ISIIMPUF:[error]`
- `-1^ERROR IN ADMIT~ISIIMP25:[error]`
- `-1^DERROR IN DISCH^DGPMAPI3:[error]`

## Example Usage

### Example 1: Basic Admission
```
MISC(1) = "PATIENT^123456789"
MISC(2) = "ADATE^3000101.1200"
MISC(3) = "WARD^3E NORTH"
MISC(4) = "RMBD^3E-100-5"
```

### Example 2: Admission with Provider and Specialty
```
MISC(1) = "PAT_SSN^555001234"
MISC(2) = "ADATE^T-7@08:00"
MISC(3) = "WARD^CARDIOLOGY"
MISC(4) = "RMBD^CARD-200-1"
MISC(5) = "PROVIDER^SMITH,JOHN"
MISC(6) = "FTSPEC^SURGICAL"
MISC(7) = "ATYPE^VETERAN"
```

### Example 3: Admission with Immediate Discharge
```
MISC(1) = "PATIENT^987654321"
MISC(2) = "ADATE^3000115.0900"
MISC(3) = "DDATE^3000118.1500"
MISC(4) = "WARD^GENERAL MEDICINE"
MISC(5) = "RMBD^GM-150-3"
MISC(6) = "DTYPE^DISCHARGED"
```

### Example 4: Complete Admission Details
```
MISC(1) = "PATIENT^111223333"
MISC(2) = "ADATE^3000201.1430"
MISC(3) = "WARD^ICU"
MISC(4) = "RMBD^ICU-001-1"
MISC(5) = "PROVIDER^DOE,JANE"
MISC(6) = "FTSPEC^MEDICAL"
MISC(7) = "ATYPE^SERVICE CONNECTED"
MISC(8) = "SHDIAG^CHEST PAIN"
MISC(9) = "ADMREG^EMERGENCY"
```

## Related Files

- **#2**: PATIENT file
- **#42**: WARD LOCATION file
- **#405.4**: ROOM-BED file
- **#405.1**: FACILITY MOVEMENT TYPE file
- **#45.7**: FACILITY TREATING SPECIALTY file
- **#200**: NEW PERSON file (providers)
- **#44**: HOSPITAL LOCATION file (linked from ward)
- **#405**: PATIENT MOVEMENT file

## Validation

### Admission Validation: `VALADMIT^ISIIMPUF`

Validations include:
- Patient existence
- Admission date/time format and time component
- Facility determination
- Ward existence and active status
- Room-Bed existence
- Admission type validity
- Facility treating specialty validity
- Ward's Hospital Location active status on admission date

### Discharge Validation: `VALDSCHG^ISIIMPUF`

Validations include:
- Discharge date after admission date
- Discharge date/time format

## Processing Flow

1. **Parse Parameters**: Convert MISC array via `ADMMISC^ISIIMPUF`
2. **Validate Admission**: Verify parameters via `VALADMIT^ISIIMPUF`
3. **Create Admission**: Process admission via `ADMIT^ISIIMP25`
4. **Create Discharge** (optional): If DDATE provided:
   - Validate discharge via `VALDSCHG^ISIIMPUF`
   - Process discharge via `DISCHARG^ISIIMP26`
5. **Return Result**: Success or error code

## Notes

- **Development Status**: This RPC is marked as "still in development" in the source code
- The facility IEN is automatically determined from the VistA site configuration
- Ward must have an associated active Hospital Location
- Default admission type is "NON-VETERAN" if not specified
- Default treating specialty is "MEDICAL" if not specified
- Both admission and discharge can be processed in a single RPC call
- Discharge is only processed if DDATE is provided
- The SHDIAG parameter provides a brief diagnosis description for the admission
- Movement records are created in the PATIENT MOVEMENT file (#405)
