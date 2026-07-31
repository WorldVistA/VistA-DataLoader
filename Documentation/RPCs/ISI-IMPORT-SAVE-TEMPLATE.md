# ISI IMPORT SAVE TEMPLATE

## Description
Updates an existing patient import template or creates a new one in ISI PT IMPORT TEMPLATE file (#9001). Templates enable batch patient creation with predefined demographics and random value generation masks.

## RPC Name
`ISI IMPORT SAVE TEMPLATE`

## Entry Point
[`TMPUPDTE^ISIIMPR1`](../../VistA/Routines/ISIIMPR1.m)

## API Entry Point
[`$$TEMPLATE^ISIIMP24`](../../VistA/Routines/ISIIMP24.m) - Main API

## Parameters

### MISC Array
Format: `MISC(n) = "PARAMETER^VALUE"`

All fields from ISI PT IMPORT TEMPLATE file (#9001) can be passed.

| Parameter | VistA File Field | Required | Description |
|-----------|------------------|----------|-------------|
| **NAME** | #9001,.01 | Yes | Template name (unique identifier) |
| **TYPE** | #9001,1 | No | TYPE OF PATIENT (#391) |
| **NAME_MASK** | #9001,2 | No | Last name mask |
| **SSN_MASK** | #9001,4 | No | SSN mask (5 digits max) |
| **SEX** | #9001,5 | No | M or F |
| **LOW_DOB** | #9001,6 | No | Earliest date of birth |
| **UP_DOB** | #9001,7 | No | Latest date of birth |
| **MARITAL_STATUS** | #9001,8 | No | Pointer to #11 |
| **ZIP_4_MASK** | #9001,9 | No | Zip code mask |
| **PH_NUM_MASK** | #9001,10 | No | Phone number mask |
| **CITY** | #9001,11 | No | City name |
| **STATE** | #9001,12 | No | Pointer to #5 |
| **VETERAN** | #9001,13 | No | Y or N |
| **DFN_NAME** | #9001,14 | No | Y or N |
| **EMPLOY_STAT** | #9001,15 | No | Employment status (SET) |
| **SERVICE** | #9001,16 | No | Military service (#49) |

### Parameter Details

#### NAME
- Template name (unique identifier)
- If exists, template is updated
- If new, template is created
- Examples: "DEFAULT_VETERAN", "TEST_PATIENT"

#### TYPE
- Patient type from TYPE OF PATIENT file (#391)
- Examples: "VETERAN", "EMPLOYEE"

#### NAME_MASK
- Last name pattern with wildcards
- `*` = random characters
- Examples: "*,PATIENT", "TEST*", "*"

#### SSN_MASK
- SSN pattern (max 5 fixed digits)
- Examples: "00012*" (00012 + 4 random), "999*" (999 + 6 random)

#### SEX
- Values: "M" (Male) or "F" (Female)

#### LOW_DOB / UP_DOB
- Date range for random DOB generation
- Format: FileMan date (YYYMMDD)
- Examples: "2900101" (Jan 1, 1990), "3000101" (Jan 1, 2000)

#### MARITAL_STATUS
- Pointer to MARITAL STATUS file (#11)
- Examples: "MARRIED", "SINGLE", "DIVORCED"

#### ZIP_4_MASK
- Zip code pattern
- Examples: "2210*" (2210 + 1 random), "90210" (fixed)

#### PH_NUM_MASK
- Phone number pattern
- Examples: "555*", "410*"

#### CITY
- City name
- Examples: "BALTIMORE", "BOSTON"

#### STATE
- Pointer to STATE file (#5)
- Examples: "MARYLAND", "TEXAS"

#### VETERAN
- Veteran status: "Y" or "N"

#### DFN_NAME
- Use DFN (patient IEN) in name generation
- Values: "Y" or "N"

#### EMPLOY_STAT
- Employment status (SET values)
- Examples: "EMPLOYED", "UNEMPLOYED", "RETIRED"

#### SERVICE
- Military service from SERVICE file (#49)
- Examples: "ARMY", "NAVY", "AIR FORCE", "MARINES"

## Output

### Success
```
ISIRESUL(0) = "1"
```

### Error
```
ISIRESUL(0) = "-1^ERROR_MESSAGE"
```

### Common Errors
- `-1^Missing NAME parameter`
- `-1^Invalid TYPE value`
- `-1^Invalid STATE value`
- `-1^Invalid SERVICE value`
- `-1^SSN_MASK too long (max 5 fixed digits)`

## Examples

### Example 1: Create Veteran Template
```
MISC(1) = "NAME^DEFAULT_VETERAN"
MISC(2) = "TYPE^VETERAN"
MISC(3) = "NAME_MASK^*,PATIENT"
MISC(4) = "SSN_MASK^00012*"
MISC(5) = "SEX^M"
MISC(6) = "LOW_DOB^2900101"
MISC(7) = "UP_DOB^3000101"
MISC(8) = "MARITAL_STATUS^MARRIED"
MISC(9) = "VETERAN^Y"
MISC(10) = "EMPLOY_STAT^RETIRED"
MISC(11) = "SERVICE^ARMY"
MISC(12) = "CITY^BALTIMORE"
MISC(13) = "STATE^MARYLAND"
MISC(14) = "ZIP_4_MASK^2210*"
```

### Example 2: Create Test Patient Template
```
MISC(1) = "NAME^TEST_PATIENT"
MISC(2) = "NAME_MASK^TEST*"
MISC(3) = "SSN_MASK^999*"
MISC(4) = "SEX^F"
MISC(5) = "LOW_DOB^2950101"
MISC(6) = "UP_DOB^3050101"
MISC(7) = "VETERAN^N"
MISC(8) = "DFN_NAME^Y"
```

### Example 3: Update Existing Template
```
MISC(1) = "NAME^DEFAULT_VETERAN"
MISC(2) = "CITY^WASHINGTON"
MISC(3) = "STATE^DC"
MISC(4) = "ZIP_4_MASK^2000*"
```

### Example 4: Minimal Template
```
MISC(1) = "NAME^MINIMAL_TEMPLATE"
MISC(2) = "SEX^M"
MISC(3) = "VETERAN^N"
```

## Mask Format Examples

### NAME_MASK
- `"*,PATIENT"` → [Random Last],PATIENT
- `"TEST*"` → TEST[Random]
- `"SMITH,*"` → SMITH,[Random First]
- `"*"` → [Completely Random]

### SSN_MASK
- `"00012*"` → 000121234 (00012 + 4 random digits)
- `"999*"` → 999123456 (999 + 6 random)
- `"12345*"` → 123451234 (max 5 fixed allowed)

### ZIP_4_MASK
- `"2210*"` → 22101, 22102, etc.
- `"*"` → Random 5-digit zip
- `"90210"` → Fixed zip (no randomization)

### PH_NUM_MASK
- `"555*"` → 5551234567
- `"410*"` → 4101234567
- `"*"` → Completely random phone

## Processing Flow

1. **Parse Parameters**: Convert MISC array via `TMPMISC^ISIIMPUE`
2. **Validate Template**: Call `VALIDATE^ISIIMPUE` to check:
   - Required NAME parameter present
   - Valid values for TYPE, STATE, SERVICE
   - SSN_MASK not too long (≤5 fixed digits)
   - Valid date ranges
3. **Lookup Template**: Check if NAME exists in file #9001
4. **Create or Update**:
   - If exists: Update existing template
   - If new: Create new template entry
5. **Save Fields**: Store all parameters in file #9001
6. **Return Result**: Success or error message

## Related VistA Files

- **#9001**: ISI PT IMPORT TEMPLATE (target file)
- **#391**: TYPE OF PATIENT
- **#11**: MARITAL STATUS
- **#5**: STATE
- **#49**: SERVICE (military)

## Validation Routines

- [`ISIIMPUE`](../../VistA/Routines/ISIIMPUE.m) - Template validation
  - `TMPMISC^ISIIMPUE` - Parse parameters
  - `VALIDATE^ISIIMPUE` - Validate template structure

## Use Cases

### Create Standard Templates
- Define commonly used patient demographics
- Set up test data generation templates
- Configure batch creation templates

### Update Templates
- Modify existing template values
- Adjust masks for different scenarios
- Update location/demographic data

### Template Management
- Maintain library of templates
- Version control for test scenarios
- Share templates across systems

## Related RPCs

- **[ISI IMPORT GET TEMPLATES](ISI-IMPORT-GET-TEMPLATES.md)** - List all templates
- **[ISI IMPORT GET TEMPLATE DETLS](ISI-IMPORT-GET-TEMPLATE-DETLS.md)** - Get template details
- **[ISI IMPORT PAT](ISI-IMPORT-PAT.md)** - Use templates to create patients

## Notes

- Templates persist in file #9001
- NAME parameter identifies template (create if new, update if exists)
- Masks enable random value generation for batch testing
- Templates provide defaults that can be overridden during patient creation
- DFN_NAME=Y incorporates patient IEN into generated name
- SSN_MASK limited to 5 fixed digits to ensure uniqueness
- All date fields use FileMan format (YYYMMDD)
- Missing parameters leave corresponding template fields unchanged (update mode)

## Best Practices

1. **Naming**: Use descriptive template names
2. **Masks**: Keep fixed portions minimal for maximum randomization
3. **Dates**: Use realistic date ranges
4. **Testing**: Create separate templates for different test scenarios
5. **Documentation**: Comment template purpose in NAME
