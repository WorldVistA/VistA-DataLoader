# ISI IMPORT VITALS

## Description
Creates vital signs measurements in VistA. This RPC records patient vital signs including temperature, blood pressure, pulse, respiration, height, weight, pain, and other measurements.

## RPC Name
`ISI IMPORT VITALS`

## Entry Point
[`VITMAKE^ISIIMPR1`](../../VistA/Routines/ISIIMPR1.m)

## API Entry Point
[`$$VITALS^ISIIMP08`](../../VistA/Routines/ISIIMP08.m) - Main API  
[`$$VALIDATE^ISIIMP09`](../../VistA/Routines/ISIIMP09.m) - Validation

## Input Parameters

### MISC Array
Format: `MISC(n) = "PARAMETER^VALUE"`

| Parameter | Type | VistA File | Required | Description |
|-----------|------|------------|----------|-------------|
| **DT_TAKEN** | FIELD | #120.5,.01 | Yes | DATE/TIME vital measurement taken |
| **PAT_SSN** | FIELD | #2,.09 | Yes | PATIENT SSN or DFN |
| **VITAL_TYPE** | FIELD | #120.5,.03 | Yes | VITAL TYPE (pointer to #120.51) |
| **RATE** | FIELD | #120.5,1.2 | Yes | Rate/value for vital measurement |
| **LOCATION** | FIELD | #120.5,.05 | Yes | HOSPITAL LOCATION name or IEN |
| **ENTERED_BY** | FIELD | #120.5,.06 | Yes | PERSON who entered vital (name or IEN from #200) |

## Parameter Details

### DT_TAKEN (Date/Time Taken)
- **Required**: Yes
- **Format**: VistA FileMan date/time
- **Default time**: If time component missing, defaults to "12:00" (noon)
- **Conversion**: Supports relative dates (e.g., "T@08:30" for today at 8:30 AM)
- **Validation**: Must be valid FileMan date/time

### PAT_SSN
- **Required**: Yes
- **Input**: SSN (9 digits) or DFN (internal entry number)
- **Priority**: SSN lookup takes priority if provided
- **Validation**: Must exist in PATIENT file (#2)
- **Converted to**: DFN (stored in ISIMISC("DFN"))

### VITAL_TYPE
- **Required**: Yes
- **Input**: Vital type name or abbreviation from file #120.51
- **Common values**:
  - `TEMPERATURE` or `T`
  - `PULSE` or `P`
  - `RESPIRATION` or `R`
  - `BLOOD PRESSURE` or `BP`
  - `HEIGHT` or `HT`
  - `WEIGHT` or `WT`
  - `PAIN` or `PN`
  - `PULSE OXIMETRY` or `PO2`
  - `CENTRAL VENOUS PRESSURE` or `CVP`
  - `CIRCUMFERENCE/GIRTH` or `CG`
- **Resolution**: Searches by name ("B" index) or abbreviation ("C" index)
- **Converted to**: Internal IEN

### RATE
- **Required**: Yes
- **Format**: Numeric value appropriate for the vital type
- **Validation**: 
  - Must match RATE INPUT TRANSFORM for the specific VITAL_TYPE
  - Vital type must accept RATE entries
  - Each vital type has specific valid ranges
- **Examples**:
  - Temperature: "98.6" (Fahrenheit) or "37.0" (Celsius)
  - Pulse: "72"
  - Blood Pressure: "120/80"
  - Weight: "175" (pounds)
  - Height: "70" (inches)

### LOCATION
- **Required**: Yes
- **Input**: Hospital location name or IEN from file #44
- **Validation**:
  - Must exist in HOSPITAL LOCATION file (#44)
  - Must be active on the date/time vital was taken
  - TYPE field (#44,2) cannot equal 'Z' (OTHER)
- **Converted to**: Internal IEN
- **Activity check**: Verifies location not inactive on DT_TAKEN

### ENTERED_BY
- **Required**: Yes
- **Input**: Person name or IEN from file #200
- **Validation**: Must exist in NEW PERSON file (#200)
- **Converted to**: Internal IEN

## Output

### Success
- `ISIRESUL(0) = 1` (or positive value)
- Vital measurement created in GMRV VITAL MEASUREMENT file (#120.5)

### Error
- `ISIRESUL(0) = -1^ERROR_MESSAGE`

Possible error messages:
- `-1^Bad parameter title passed: [PARAM]`
- `-1^No data provided for parameter: [PARAM]`
- `-1^Invalid DT_TAKEN date/time`
- `-1^Missing DT_TAKEN entry`
- `-1^Invalid DT_TAKEN (#120.5,.01)`
- `-1^Missing Patient SSN`
- `-1^Invalid PAT_SSN (#2,.09)`
- `-1^Missing VITAL_TYPE entry`
- `-1^Invalid VITAL_TYPE (#120.5,.03)`
- `-1^Unable to find internal value for VITAL_TYPE (#120.5,.03)`
- `-1^Missing RATE entry`
- `-1^VITAL_TYPE does not accept RATE entries`
- `-1^Invalid RATE value for VITAL_TYPE`
- `-1^Missing ENTERED_BY entry`
- `-1^Invalid ENTERED_BY (#120.5,.06)`
- `-1^Missing LOCATION entry`
- `-1^LOCATION, TYPE field (#44,2) cannot equal 'Z' [OTHER]`
- `-1^Invalid LOCATION (#120.5,.05)`
- `-1^Location inactive on date vital taken (#44)`

## Example Usage

### Example 1: Basic Temperature Reading
```
MISC(1) = "DT_TAKEN^T@08:30"
MISC(2) = "PAT_SSN^123456789"
MISC(3) = "VITAL_TYPE^TEMPERATURE"
MISC(4) = "RATE^98.6"
MISC(5) = "LOCATION^PRIMARY CARE"
MISC(6) = "ENTERED_BY^NURSE,SALLY"
```

### Example 2: Blood Pressure
```
MISC(1) = "DT_TAKEN^3000101.0900"
MISC(2) = "PAT_SSN^555001234"
MISC(3) = "VITAL_TYPE^BP"
MISC(4) = "RATE^120/80"
MISC(5) = "LOCATION^CARDIOLOGY"
MISC(6) = "ENTERED_BY^126"
```

### Example 3: Complete Vital Signs Set
```
; Weight
MISC(1) = "DT_TAKEN^T@14:00"
MISC(2) = "PAT_SSN^987654321"
MISC(3) = "VITAL_TYPE^WEIGHT"
MISC(4) = "RATE^175"
MISC(5) = "LOCATION^GENERAL MEDICINE"
MISC(6) = "ENTERED_BY^SMITH,JOHN"

; Followed by additional calls for other vitals...
```

### Example 4: Using Abbreviations
```
MISC(1) = "DT_TAKEN^T-1@12:00"
MISC(2) = "PAT_SSN^111223333"
MISC(3) = "VITAL_TYPE^P"
MISC(4) = "RATE^72"
MISC(5) = "LOCATION^ER"
MISC(6) = "ENTERED_BY^DOE,JANE"
```

## Vital Types and Typical Ranges

| Vital Type | Abbreviation | Typical Range | Units |
|-----------|--------------|---------------|-------|
| TEMPERATURE | T | 96.0-104.0 | °F |
| PULSE | P | 40-200 | beats/min |
| RESPIRATION | R | 8-40 | breaths/min |
| BLOOD PRESSURE | BP | 60/40-250/150 | mmHg |
| HEIGHT | HT | 20-95 | inches |
| WEIGHT | WT | 0-999 | pounds |
| PAIN | PN | 0-10 | scale |
| PULSE OXIMETRY | PO2 | 0-100 | % |

## Related Files

- **#120.5**: GMRV VITAL MEASUREMENT file
- **#120.51**: GMRV VITAL TYPE file
- **#2**: PATIENT file
- **#44**: HOSPITAL LOCATION file
- **#200**: NEW PERSON file

## Validation

### Performed by: `VALVITAL^ISIIMPU5`

Validations include:
- Date/time format and validity
- Patient SSN existence
- Vital type existence and validity
- Rate value against vital type's input transform
- Hospital location existence and active status
- Entered by person existence
- Location type not equal to 'Z' (OTHER)
- Location active on measurement date

## Processing Flow

1. **Parse Parameters**: Convert MISC array via `VITMISC^ISIIMPU5`
2. **Default Time**: If no time provided, add ".1200" (noon)
3. **Validate Input**: Verify all parameters via `VALVITAL^ISIIMPU5`
4. **Resolve References**:
   - Convert PAT_SSN to DFN
   - Convert VITAL_TYPE name/abbreviation to IEN
   - Convert LOCATION name to IEN
   - Convert ENTERED_BY name to IEN
5. **Validate Rate**: Check against vital type's RATE INPUT TRANSFORM
6. **Create Vital**: Add measurement to GMRV VITAL MEASUREMENT file
7. **Return Result**: Success or error code

## Notes

- Default time of 12:00 (noon) is added if only date provided for DT_TAKEN
- The routine checks that the vital type accepts RATE entries
- Each vital type has specific input transforms that validate the rate value
- Hospital location must be active on the date the vital was taken
- Location TYPE cannot be 'Z' (OTHER category)
- Vital types can be referenced by either full name or abbreviation
- The system validates ranges specific to each vital type
- Multiple vitals can be entered for the same patient/date/time combination
