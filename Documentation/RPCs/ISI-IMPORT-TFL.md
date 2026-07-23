# ISI IMPORT TFL

## Description
Creates Treating Facility List entries for patients. The Treating Facility List tracks which VA facilities have treated a patient and when they were last seen at each facility. This is used for patient record sharing and transfer between VA facilities.

## RPC Name
`ISI IMPORT TFL`

## Entry Point
[`TRTFACLS^ISIIMPR1`](../../VistA/Routines/ISIIMPR1.m#L194)

## API Entry Point
[`$$TFL^ISIIMP28`](../../VistA/Routines/ISIIMP28.m) - Main API  
[`$$VALTFL^ISIIMPUH`](../../VistA/Routines/ISIIMPUH.m) - Validation

## Input Parameters

### MISC Array
Format: `MISC(n) = "PARAMETER^VALUE"`

| Parameter | Type | File/Field | Required | Description |
|-----------|------|------------|----------|-------------|
| PAT_SSN | FIELD | #2,.09 | ✅ Yes | Patient SSN (identifier) |
| INST | FIELD | #4,.01 | ✅ Yes | Institution where patient was last seen |
| DATE | FIELD | - | ✅ Yes | Date patient was last seen at facility |

## Parameter Details

### PAT_SSN
- **Format**: 9-digit SSN (no dashes)
- **Example**: `"666112222"`
- **Validation**: Must exist in PATIENT file (#2)
- **Purpose**: Identifies the patient

### INST
- **Format**: Institution name
- **Example**: `"LEXINGTON VAMC"`, `"CINCINNATI VAMC"`
- **Validation**: Must exist in INSTITUTION file (#4)
- **Purpose**: The VA facility that treated the patient
- **Note**: Name is converted to IEN internally via "B" cross-reference

### DATE
- **Format**: FileMan date/time
- **Example**: `"3250101"` (Jan 1, 2025) or `"3250101.1400"` (Jan 1, 2025 @ 2PM)
- **Auto-append**: If no time provided, defaults to `.1200` (noon)
- **Validation**: Valid FileMan date format
- **Purpose**: Date of last encounter at the facility

## Output

### Success
```
ISIRESUL(0) = 1
```

### Error
```
ISIRESUL(0) = "-1^ERROR_MESSAGE"
```

## Example Usage

### Example 1: Patient Last Seen at Lexington
```
MISC(1) = "PAT_SSN^666001001"
MISC(2) = "INST^LEXINGTON VAMC"
MISC(3) = "DATE^3250115"

Returns: ISIRESUL(0) = 1
```

### Example 2: Patient Last Seen at Cincinnati with Specific Time
```
MISC(1) = "PAT_SSN^666002002"
MISC(2) = "INST^CINCINNATI VAMC"
MISC(3) = "DATE^3250201.0930"

Returns: ISIRESUL(0) = 1
```

### Example 3: Multiple Facilities for Same Patient
```
; First facility
MISC(1) = "PAT_SSN^666003003"
MISC(2) = "INST^BOSTON VAMC"
MISC(3) = "DATE^3241201"
Do RPC call...

; Second facility
MISC(1) = "PAT_SSN^666003003"
MISC(2) = "INST^PROVIDENCE VAMC"
MISC(3) = "DATE^3250115"
Do RPC call...

Returns: Patient now has two treating facilities in their TFL
```

### Example 4: Update Last Seen Date for Existing Facility
```
MISC(1) = "PAT_SSN^666004004"
MISC(2) = "INST^ATLANTA VAMC"
MISC(3) = "DATE^3250301"

Returns: Updates the last seen date for ATLANTA VAMC (if already in TFL)
        or adds new entry (if not in TFL)
```

## Common Error Messages

| Error Message | Cause | Solution |
|---------------|-------|----------|
| `-1^Missing Patient SSN` | PAT_SSN not provided | Include PAT_SSN parameter |
| `-1^Invalid PAT_SSN` | SSN not found in file #2 | Verify patient exists, create if needed |
| `-1^Missing Date Last Treated` | DATE not provided | Include DATE parameter |
| `-1^Invalid DATE date/time` | Invalid date format | Use FileMan date format |
| `-1^Bad parameter title passed` | Invalid parameter name | Check spelling of parameter names |
| `-1^No data provided for parameter` | Parameter has no value | Provide value for all parameters |

## Related Files
- **#2**: PATIENT file
- **#4**: INSTITUTION file
- **#391.91**: TREATING FACILITY LIST sub-file (under PATIENT file)

## Validation Routine
- **ISIIMPUH**: Parameter parsing (`TFLMISC^ISIIMPUH`)
- **ISIIMPUH**: Validation (`VALTFL^ISIIMPUH`)

## API Routine
- **ISIIMP28**: Main API (`TFL^ISIIMP28`)

## Processing Flow

1. Parse parameters via `TFLMISC^ISIIMPUH`
2. Validate PAT_SSN exists in file #2
3. Convert PAT_SSN to patient DFN
4. Validate INST name and convert to IEN from file #4
5. Validate DATE format
6. Auto-append `.1200` to DATE if no time specified
7. Check if facility already in patient's TFL
8. If exists, update last seen date
9. If new, add facility to TFL with date
10. Return success or error

## File Structure

### TREATING FACILITY LIST (#391.91)
This is a sub-file under the PATIENT file (#2) that stores treating facility information.

**Fields:**
- Institution (pointer to #4)
- Date Last Treated
- Status
- Comments

**Purpose:**
- Tracks all VA facilities that have treated a patient
- Records when patient was last seen at each facility  
- Used for patient record sharing across VA system
- Supports continuity of care during transfers

## Use Cases

### 1. Patient Transfer Tracking
Document when a patient moves from one VA facility to another:
```
; Patient moved from Dallas to Houston
MISC(1) = "PAT_SSN^666005005"
MISC(2) = "INST^DALLAS VAMC"
MISC(3) = "DATE^3241215"  ; Last seen in Dallas

MISC(1) = "PAT_SSN^666005005"
MISC(2) = "INST^HOUSTON VAMC"
MISC(3) = "DATE^3250110"  ; First seen in Houston
```

### 2. Multi-Facility Care
Track patients who receive care at multiple VA facilities:
```
; Veteran receives specialty care at multiple sites
MISC(1) = "PAT_SSN^666006006"
MISC(2) = "INST^RALEIGH VAMC"
MISC(3) = "DATE^3250201"  ; Primary care

MISC(1) = "PAT_SSN^666006006"
MISC(2) = "INST^DURHAM VAMC"
MISC(3) = "DATE^3250215"  ; Specialty clinic
```

### 3. Batch Import Historical TFL Data
Import treating facility history for multiple patients:
```
Loop through patient records:
  For each patient's facility history:
    MISC(1) = "PAT_SSN^" + patient SSN
    MISC(2) = "INST^" + facility name
    MISC(3) = "DATE^" + last seen date
    Call ISI IMPORT TFL
```

## Integration with Other RPCs

The Treating Facility List is automatically updated by:
- **ISI IMPORT APPT**: May update TFL when appointments created
- **ISI IMPORT ADMIT**: May update TFL when admissions created

This RPC provides **manual/batch control** over TFL entries for:
- Historical data import
- Data migration between systems
- Testing scenarios
- Correcting TFL data

## Institution Name Format

### Valid Institution Names
Institution names must match entries in file #4 INSTITUTION exactly.

**Common VA Medical Centers:**
- `"LEXINGTON VAMC"`
- `"CINCINNATI VAMC"`
- `"ATLANTA VAMC"`
- `"BOSTON VAMC"`
- `"CHICAGO VAMC"`
- etc.

### Finding Institution Names
Use `ISI IMPORT TABLEFETCH` to retrieve available institutions:
```
MISC(1) = "TABLE^HOSP_LOC"  or appropriate table
Returns: List of institution names/codes
```

Or query file #4 directly in VistA:
```
D LIST^DIC(4)
```

## Date Format Details

### FileMan Date Format
- **YYYYMMDD**: `3250115` = January 15, 2025
- **YYYYMMDD.HHMM**: `3250115.1430` = January 15, 2025 @ 2:30 PM
- **Omit time**: Defaults to `.1200` (noon)

### Date Validation
- Must be valid FileMan date
- Can be past, present, or future (though typically past)
- Should reflect actual last encounter date

## Notes
- Treating Facility List is part of VistA's multi-facility patient tracking
- Essential for patient record sharing between VA facilities
- Updates can be made multiple times - latest date overwrites previous
- Each facility appears only once per patient with most recent date
- Used by VistA for determining patient's "home facility" and record location

## Best Practices
1. **Use actual encounter dates** - reflects real patient care timeline
2. **Update regularly** - keep TFL current as patients move between facilities
3. **Batch import** - efficient for migrating historical data
4. **Verify institution names** - must match file #4 exactly (case-sensitive)
5. **Document transfers** - create TFL entry when patient transfers between VAMCs

## Relationship to Patient File

The TFL is stored as a multiple (sub-file) under each patient's record in file #2:

```
PATIENT (#2)
  └─ TREATING FACILITY LIST (#391.91) [Multiple]
       ├─ Facility 1: LEXINGTON VAMC, Last Seen: 2025-01-15
       ├─ Facility 2: CINCINNATI VAMC, Last Seen: 2024-12-01
       └─ Facility 3: ATLANTA VAMC, Last Seen: 2024-10-20
```

## Version History
- V.1.0 (June 2012): Initial implementation
- V.2.0 (June 2014): Updates
- V.3.0 (2018): License change to Apache 2.0
- V.3.1 (Build 70, Dec 2024): Continued maintenance
