# ISI IMPORT TABLEFETCH

## Description
Exports select VistA file data for populating external application dropdown lists and selection menus. This utility RPC retrieves commonly needed reference data from VistA files.

## RPC Name
`ISI IMPORT TABLEFETCH`

## Entry Point
`TABLEGET^ISIIMPR2`

## API Entry Point
`ENTRY^ISIIMPUA(.ARRAY,.TABLE)`

## Input Parameters

### Single Parameter
Format: `TABLE = "TABLE_NAME"`

| Table Name | Resolves To | Description |
|------------|-------------|-------------|
| NOTE | File #8925.1 | TIU DOCUMENT DEFINITION (Active DOC types only) |
| DRUG | File #50 | DRUG (with orderable item, DEA, and price) |
| SIG | File #51 | MEDICATION INSTRUCTION (outpatient only) |
| PROV | File #200 | PROVIDER (authorized, active, with PROVIDER key) |
| USER | File #200 | USER (active users) |
| RACE | File #10 | RACE (active entries) |
| ETHN | File #10.2 | ETHNICITY INFORMATION (active entries) |
| EMPLOY | Field #2,.31115 | EMPLOYMENT STATUS (set values) |
| INSUR | File #36 | INSURANCE COMPANY |
| LOC | File #44 | HOSPITAL LOCATION |
| VITAL | File #120.51 | GMRVVITAL TYPE |
| ALLER | File #120.82 | ALLERGEN |
| SYMP | File #120.83 | SYMPTOM (Signs/Symptoms) |
| LAB | File #60 | LABORATORY TEST |
| GENDER | Field #2,.02 | SEX (M/F) |
| BOOL | - | BOOLEAN ("Y","N") |
| PROBSTAT | Field #9000011,.12 | PROBLEM STATUS (A/I) |
| PROBTYPE | Field #9000011,.14 | PROBLEM TYPE (A/C) |
| CONSULT | File #123.5 | REQUEST SERVICES |
| MAGLOC | File #79.1 | IMAGING LOCATIONS |
| RADPROC | File #71 | RADIOLOGY PROCEDURE |
| PNTTYPE | File #391 | TYPE OF PATIENT |
| MARSTAT | File #11 | MARITAL STATUS |
| STATE | File #5 | STATE |
| SERVICE | File #49 | SERVICE/SECTION |
| HFACTOR | File #9999999.64 | HEALTH FACTORS |
| CPT | File #81 | CPT |
| PRVNAR | File #9999999.27 | PROVIDER NARRATIVE |
| IMZ | File #9999999.14 | IMMUNIZATION |
| EXAM | File #9999999.15 | EXAM |
| EDTOPIC | File #9999999.09 | PATIENT EDUCATION TOPICS |
| INST | File #4 | INSTITUTION |
| NVAMEDS | - | NON-VA MEDICATIONS |

## Parameter Details

### TABLE
- **Required**: Yes
- **Input**: One of the table names listed above (case-insensitive)
- **Validation**: Must be a recognized table name
- **Processing**: Trimmed and converted to uppercase

## Output

### Success
- `ISIRESUL(0) = CNT` (numeric count of entries)
- `ISIRESUL(1..n) = VALUE` (text values from file)

### Error
- `ISIRESUL(0) = "-1^Incorrect parameter passed"`
- `ISIRESUL(0) = "-1^No results found."`

## Filtering Rules

Different tables apply different filters:

### NOTE (TIU Documents)
- TYPE = "DOC" (documents only, not folders)
- STATUS = 11 (Active)
- Excludes CONSULT types

### DRUG
- Must have pointer to Orderable Item (#50.7 field .01)
- Must have DEA value (#50 field .01)
- Must have unit price (#50.68 field .06)

### SIG (Med Instructions)
- Intended use NOT "Inpatient only" (#51,30 must be ≤1)

### PROV (Providers)
- Authorized to write medical orders (#200,"PS" = 1)
- Not inactivated (inactivation date check)
- Has PROVIDER security key

### USER
- Not inactivated (inactivation date check)

### RACE, ETHNICITY
- Not inactivated (inactivation date check)

## Example Usage

### Example 1: Get Provider List
```
TABLE = "PROV"

Returns:
ISIRESUL(0) = 25
ISIRESUL(1) = "DOE,JANE"
ISIRESUL(2) = "SMITH,JOHN"
ISIRESUL(3) = "WILSON,ROBERT"
...
ISIRESUL(25) = "BROWN,MARY"
```

### Example 2: Get Lab Tests
```
TABLE = "LAB"

Returns:
ISIRESUL(0) = 150
ISIRESUL(1) = "CHOLESTEROL"
ISIRESUL(2) = "GLUCOSE"
ISIRESUL(3) = "HEMOGLOBIN"
...
```

### Example 3: Get Hospital Locations
```
TABLE = "LOC"

Returns:
ISIRESUL(0) = 45
ISIRESUL(1) = "PRIMARY CARE"
ISIRESUL(2) = "CARDIOLOGY"
ISIRESUL(3) = "EMERGENCY ROOM"
...
```

### Example 4: Get Gender Options
```
TABLE = "GENDER"

Returns:
ISIRESUL(0) = 2
ISIRESUL(1) = "M"
ISIRESUL(2) = "F"
```

### Example 5: Get Boolean Values
```
TABLE = "BOOL"

Returns:
ISIRESUL(0) = 2
ISIRESUL(1) = "Y"
ISIRESUL(2) = "N"
```

## Internal Table Resolution

The `$$PARAM^ISIIMPUA(TABLE)` function converts table names to numeric codes:

```mumps
I TABLE="NOTE" Q 1      ;TIULIST
I TABLE="DRUG" Q 2      ;DRUGLIST
I TABLE="SIG" Q 3       ;SIGLIST
I TABLE="PROV" Q 4      ;PROVLIST
I TABLE="USER" Q 5      ;USERLIST
I TABLE="RACE" Q 6      ;RACE
I TABLE="ETHN" Q 7      ;ETHNICITY
I TABLE="EMPLOY" Q 8    ;EMPLOYSTAT
I TABLE="INSUR" Q 9     ;INSURANCE
I TABLE="LOC" Q 10      ;LOCATION
...
```

Returns -1 for unrecognized table names.

## Related Files

Varies by table - see table name mapping above.

## Use Cases

This RPC is designed to populate:
- Dropdown lists in user interfaces
- Selection menus in forms
- Autocomplete fields
- Reference data caches
- Validation lists

## Processing Flow

1. **Validate Parameter**: Check TABLE parameter provided
2. **Convert to Uppercase**: `$$UP^XLFSTR(TABLE)`
3. **Resolve Table Code**: Call `$$PARAM^ISIIMPUA(TABLE)`
4. **Validate Code**: Return error if -1
5. **Fetch Data**: Call `ENTRY^ISIIMPUA(.ISIRESUL,.TABLE)`
6. **Apply Filters**: Based on table type
7. **Return Results**: Array of values

## Special Filters

### Active Status Checking
Many tables check for active status:
```mumps
D NOW^%DTC S DTC=X  ; Current date/time
S IDT=$P($G(^[file]([ien],"I")),U)        ; Inactive date
S RDT=$P($G(^[file]([ien],"I")),U,2)      ; Reactive date
; Skip if inactivated before current date
```

### Provider Validation
Providers must pass multiple checks:
- Authorized to write orders
- Not inactivated
- Has PROVIDER security key in ^VA(200,"AK.PROVIDER",NAME)

## Notes

- All table names are case-insensitive
- Results are sorted alphabetically (by "B" cross-reference)
- Empty result sets return "-1^No results found."
- The routine applies VistA-specific business rules for each table
- Some tables are hard-coded sets (GENDER, BOOL, PROBSTAT, PROBTYPE)
- Provider and user lists exclude inactivated entries
- Drug list has strict validation requirements
- TIU document list excludes templates and consult types
- Results are returned as display values (names), not IENs
