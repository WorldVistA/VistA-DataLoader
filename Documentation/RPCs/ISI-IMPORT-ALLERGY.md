# ISI IMPORT ALLERGY

## Description
Creates allergy entries in a patient's allergy/adverse reaction record. This RPC documents allergens, reactions/symptoms, severity, and observation details.

## RPC Name
`ISI IMPORT ALLERGY`

## Entry Point
[`ALGMAKE^ISIIMPR2`](../../VistA/Routines/ISIIMPR2.m)

## API Entry Point
[`$$ALLERGY^ISIIMP10`](../../VistA/Routines/ISIIMP10.m) - Main API  
[`$$VALIDATE^ISIIMP11`](../../VistA/Routines/ISIIMP11.m) - Validation

## Input Parameters

### MISC Array
Format: `MISC(n) = "PARAMETER^VALUE"`

| Parameter | Type | VistA File | Required | Description |
|-----------|------|------------|----------|-------------|
| **ALLERGEN** | FIELD | #120.82,.01 | Yes | Allergen name (pointer to #120.82) |
| **SYMPTOM** | MULTIPLE | #120.83,.01 | Yes | Symptoms/Reactions (pipe "|" delimited list) |
| **PAT_SSN** | FIELD | #120.86,.01 | Yes | PATIENT SSN or DFN |
| **ORIG_DATE** | FIELD | #120.8,4 | Yes | Origination date (when allergy was documented) |
| **ORIGINTR** | FIELD | #120.8,5 | Yes | Originator - PERSON who documented (#200) |
| **HISTORIC** | BOOLEAN | - | Yes | 1 = HISTORICAL, 0 = OBSERVED |
| **OBSRV_DT** | FIELD | - | No | Observation date (if HISTORIC=0) |

## Parameter Details

### ALLERGEN
- **Required**: Yes
- **Input**: Allergen name from file #120.82
- **File**: GMRD ALLERGIES/REACTIONS FILE (#120.82)
- **Common categories**:
  - Foods (e.g., "PEANUTS", "SHELLFISH", "MILK")
  - Drugs (e.g., "PENICILLIN", "ASPIRIN", "CODEINE")
  - Environmental (e.g., "POLLEN", "DUST", "LATEX")
- **Auto-conversion**: Routine automatically determines allergen type and creates proper GMRAGNT value
- **Format created**: `"ALLERGEN^IEN;GMRD(120.82,"`

### SYMPTOM
- **Required**: Yes
- **Input**: Pipe-delimited ("|") list of symptom names
- **File**: GMRD SIGNS/SYMPTOMS FILE (#120.83)
- **Format**: `"SYMPTOM1|SYMPTOM2|SYMPTOM3"`
- **Common symptoms**:
  - "ANXIETY"
  - "DIARRHEA"
  - "NAUSEA"
  - "RASH"
  - "HIVES"
  - "ITCHING"
  - "SHORTNESS OF BREATH"
  - "ANAPHYLAXIS"
  - "SWELLING"
- **Processing**: Each symptom creates array entry in ISIMISC("GMRASYMP",n)
- **Auto-conversion**: Creates structured format: `"IEN^NAME^^^"`

### PAT_SSN
- **Required**: Yes
- **Input**: SSN (9 digits) or DFN (internal entry number)
- **Priority**: SSN lookup takes priority
- **Validation**: Must exist in PATIENT file (#2)
- **Converted to**: DFN (stored in ISIMISC("DFN"))

### ORIG_DATE (Origination Date)
- **Required**: Yes
- **Format**: VistA FileMan date or date/time
- **Default time**: If time component missing, defaults to "12:00" (noon)
- **Purpose**: When the allergy was first documented
- **Validation**: Must be valid FileMan date/time
- **Used as**: GMRAORDT in processing

### ORIGINTR (Originator)
- **Required**: Yes
- **Input**: Person name or IEN from file #200
- **File**: NEW PERSON file (#200)
- **Purpose**: Person who documented/originated the allergy entry
- **Validation**: Must exist in file #200
- **Converted to**: GMRAORIG (IEN)

### HISTORIC
- **Required**: Yes
- **Input**: Numeric boolean value
  - `1` = HISTORICAL (patient reported, not directly observed)
  - `0` = OBSERVED (directly observed reaction)
- **Validation**: Must be 0 or 1
- **Auto-conversion**:
  - If 1: Creates `GMRAOBHX = "h^HISTORICAL"`
  - If 0: Creates `GMRAOBHX = "o^OBSERVED"`

### OBSRV_DT (Observation Date)
- **Required**: No (typically used when HISTORIC=0)
- **Format**: VistA FileMan date/time
- **Default time**: If time component missing, defaults to "12:00" (noon)
- **Purpose**: When the reaction was observed

## Auto-Set Parameters

The routine automatically sets these internal parameters:

- **GMRANATR**: Always set to `"A^Allergy"` (nature of reaction)
- **GMRASEVR**: Always set to `2` (severity level)
- **GMRATYPE**: Auto-determined from allergen's type field (#120.82,1)
- **GMRASYMP(0)**: Count of symptoms
- **GMRASYMP(n)**: Individual symptom entries

## Output

### Success
- `ISIRESUL(0) = 1`
- `ISIRESUL(1) = "success"`
- Allergy entry created in ADVERSE REACTION TRACKING file (#120.8)

### Error
- `ISIRESUL(0) = -1^ERROR_MESSAGE`

Possible error messages:
- `-1^Bad parameter title passed: [PARAM]`
- `-1^No data provided for parameter: [PARAM]`
- `-1^Invalid ORIG_DATE date/time`
- `-1^Invalid OBSRV_DT date/time`
- `-1^Missing ALLERGEN`
- `-1^Invalid ALLERGEN (#120.82)`
- `-1^Missing SYMPTOM`
- `-1^Invalid SYMPTOM (#120.83)`
- `-1^Missing Patient SSN`
- `-1^Invalid PAT_SSN (#2,.09)`
- `-1^Missing ORIGINTR entry`
- `-1^Invalid ORIGINTR (#120.8,5)`
- `-1^Missing ORIG_DATE entry`
- `-1^Invalid ORIG_DATE (#120.8,4)`
- `-1^Missing HISTORIC entry`
- `-1^Invalid HISTORIC value (0/1)`

## Example Usage

### Example 1: Drug Allergy with Multiple Symptoms
```
MISC(1) = "ALLERGEN^PENICILLIN"
MISC(2) = "SYMPTOM^RASH|HIVES|ITCHING"
MISC(3) = "PAT_SSN^123456789"
MISC(4) = "ORIG_DATE^T"
MISC(5) = "ORIGINTR^SMITH,JOHN"
MISC(6) = "HISTORIC^0"
MISC(7) = "OBSRV_DT^T"
```

### Example 2: Historical Food Allergy
```
MISC(1) = "ALLERGEN^PEANUTS"
MISC(2) = "SYMPTOM^ANAPHYLAXIS|SWELLING|SHORTNESS OF BREATH"
MISC(3) = "PAT_SSN^555001234"
MISC(4) = "ORIG_DATE^3000101.1400"
MISC(5) = "ORIGINTR^DOE,JANE"
MISC(6) = "HISTORIC^1"
```

### Example 3: Environmental Allergy
```
MISC(1) = "ALLERGEN^LATEX"
MISC(2) = "SYMPTOM^RASH|ITCHING"
MISC(3) = "PAT_SSN^987654321"
MISC(4) = "ORIG_DATE^T-30"
MISC(5) = "ORIGINTR^NURSE,SALLY"
MISC(6) = "HISTORIC^0"
MISC(7) = "OBSRV_DT^T-30"
```

### Example 4: Observed Reaction with Single Symptom
```
MISC(1) = "ALLERGEN^ASPIRIN"
MISC(2) = "SYMPTOM^NAUSEA"
MISC(3) = "PAT_SSN^111223333"
MISC(4) = "ORIG_DATE^T@08:30"
MISC(5) = "ORIGINTR^126"
MISC(6) = "HISTORIC^0"
MISC(7) = "OBSRV_DT^T@08:00"
```

## Symptom Delimiter

Use the pipe character "|" to separate multiple symptoms:
```
"SYMPTOM^RASH|HIVES|ITCHING|SWELLING"
```

Each symptom in the delimited list is:
1. Validated against GMRD SIGNS/SYMPTOMS file (#120.83)
2. Converted to internal format
3. Added to the GMRASYMP array

## Related Files

- **#120.8**: ADVERSE REACTION TRACKING file
- **#120.82**: GMRD ALLERGIES/REACTIONS FILE
- **#120.83**: GMRD SIGNS/SYMPTOMS FILE
- **#120.86**: PATIENT ALLERGIES file (subfile of #2)
- **#2**: PATIENT file
- **#200**: NEW PERSON file

## Validation

### Performed by: `VALALG^ISIIMPU6`

Validations include:
- Allergen existence in file #120.82
- Allergen type determination
- Each symptom existence in file #120.83
- Patient SSN existence
- Originator existence in NEW PERSON file
- Origination date format
- Observation date format (if provided)
- HISTORIC value is 0 or 1

## Processing Flow

1. **Parse Parameters**: Convert MISC array via `ALGMISC^ISIIMPU6`
2. **Validate Input**: Verify all parameters via `VALALG^ISIIMPU6`
3. **Resolve Allergen**:
   - Lookup allergen in file #120.82
   - Create GMRAGNT: `"NAME^IEN;GMRD(120.82,"`
   - Determine allergen type (GMRATYPE)
4. **Parse Symptoms**:
   - Split pipe-delimited list
   - Validate each symptom in file #120.83
   - Create GMRASYMP array
5. **Set Auto-Values**:
   - GMRANATR = "A^Allergy"
   - GMRASEVR = 2
   - GMRAOBHX based on HISTORIC value
6. **Create Allergy**: Call API `ALLERGY^ISIIMP10`
7. **Return Result**: Success or error code

## Auto-Set Internal Variables

These are automatically created by the routine:

```
ISIMISC("GMRAGNT") = "ALLERGEN_NAME^IEN;GMRD(120.82,"
ISIMISC("GMRATYPE") = "TYPE_IEN^TYPE_NAME"
ISIMISC("GMRANATR") = "A^Allergy"
ISIMISC("GMRASEVR") = 2
ISIMISC("GMRAOBHX") = "h^HISTORICAL" or "o^OBSERVED"
ISIMISC("GMRAORIG") = IEN (from ORIGINTR)
ISIMISC("GMRAORDT") = FileMan date/time (from ORIG_DATE)
ISIMISC("GMRASYMP",0) = Count
ISIMISC("GMRASYMP",1..n) = "IEN^NAME^^^"
ISIMISC("DFN") = Patient DFN
```

## Notes

- Multiple symptoms must be pipe-delimited in a single parameter
- The severity is always set to 2 (moderate) by default
- The nature of reaction is always set to "A^Allergy"
- Historical vs. Observed affects how the allergy is categorized
- If time component is missing from dates, 12:00 (noon) is added
- The allergen type is automatically determined from the allergen entry
- Chart edit datetime tracking is handled by the API
- This creates entries in both ADVERSE REACTION TRACKING and PATIENT ALLERGIES files
