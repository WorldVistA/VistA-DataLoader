# ISI IMPORT USER

## Description
Creates new User entries in the NEW PERSON file (#200). This RPC allows batch creation of user accounts with demographics, access codes, and security settings. Supports template-based user creation with mask wildcards for testing environments.

## RPC Name
`ISI IMPORT USER`

## Entry Point
[`USRCREAT^ISIIMPR1`](../../VistA/Routines/ISIIMPR1.m#L147)

## API Entry Point
[`$$USER^ISIIMP22`](../../VistA/Routines/ISIIMP22.m) - Main API  
[`$$VALIDATE^ISIIMPUD`](../../VistA/Routines/ISIIMPUD.m) - Validation

## Input Parameters

### MISC Array
Format: `MISC(n) = "PARAMETER^VALUE"`

| Parameter | Type | File/Field | Required | Description |
|-----------|------|------------|----------|-------------|
| TEMPLATE | PARAM | - | No | Template Name from #9001 |
| IMP_TYPE | PARAM | - | ✅ Yes | 'I' (Individual) or 'B' (Batch) |
| IMP_BATCH_NUM | PARAM | - | Conditional | Batch number (required if IMP_TYPE='B') |
| DFN_NAME | PARAM | - | No | 'Y' or 'N' for DFN-derived NAME |
| NAME | FIELD | .01 | Conditional | User name (LAST,FIRST) |
| NAME_MASK | MASK | .01 | Conditional | Last name mask with wildcards |
| SEX | FIELD | 4 | No | Sex (M/F) |
| DOB | FIELD | 5 | No | Date of Birth |
| LOW_DOB | PARAM | 5 | No | Lower date limit for auto DOB |
| UP_DOB | PARAM | 5 | No | Upper date limit for auto DOB |
| SSN | FIELD | 9 | No | Social Security Number |
| SSN_MASK | MASK | - | No | SSN mask (5 digits max) |
| STREET_ADD1 | FIELD | .111 | No | Street Address Line 1 |
| STREET_ADD2 | FIELD | .112 | No | Street Address Line 2 |
| CITY | FIELD | .114 | No | City |
| STATE | FIELD | .115 | No | State (pointer to #5) |
| ZIP_4 | FIELD | .116 | No | ZIP Code |
| ZIP_4_MASK | MASK | - | No | Zip code mask (5 max) |
| PH_NUM | FIELD | .131 | No | Phone Number |
| PH_NUM_MASK | MASK | - | No | Phone number mask |
| PH_OFFICE | FIELD | .132 | No | Office Phone (4-40 chars) |
| EMAIL | FIELD | .15 | No | Email address (50 max) |
| EMAIL_MASK | MASK | - | No | Email mask (domain name) |
| TERM_DATE | FIELD | 9.2 | No | Date ACCESS code expires |
| USER_CLASS | FIELD | 9.5 | No | User Class (pointer to #201) |
| SERVICE | FIELD | 29 | No | Service/Section (pointer to #49) |
| MRG_SOURCE | FIELD | .01 | No | User to merge profile from |
| ELSIG | FIELD | 20.4 | No | Electronic Signature (non-encrypted) |
| ELSIG_APND | MASK | - | No | Append chars for ELSIG |
| ACCESS | FIELD | 2 | No | Access Code (non-encrypted) |
| ACCESS_APND | MASK | - | No | Append chars for ACCESS |
| VERIFY | FIELD | 11 | No | Verify Code (non-encrypted) |
| VERIFY_APND | MASK | - | No | Append chars for VERIFY |
| GEN_ACCVER | PARAM | - | No | 0=don't generate, 1=generate access/verify |

## Parameter Details

### Required Parameters

#### IMP_TYPE
- **Values**: `"I"` (Individual) or `"B"` (Batch)
- **Example**: `"I"` for single user, `"B"` for batch import
- **Required**: Yes

#### IMP_BATCH_NUM
- **Format**: Numeric
- **Example**: `"1"`, `"25"`
- **Required**: Only if IMP_TYPE="B"
- **Purpose**: Specifies how many users to create in batch mode

#### NAME or NAME_MASK
- **Must provide one or the other**
- **NAME Format**: `"LAST,FIRST"` or `"LAST,FIRST MIDDLE"`
- **NAME_MASK Format**: `"*,PROVIDER"` (wildcard for random last name)
- **Example NAME**: `"SMITH,JOHN"`
- **Example MASK**: `"*,PHYSICIAN"` generates random last names

### Optional Parameters

#### SEX
- **Values**: `"M"` or `"F"`
- **Validation**: Must be M or F

#### DOB / LOW_DOB / UP_DOB
- **Format**: FileMan date
- **Example**: `"2900101"` (Jan 1, 1990)
- **DOB**: Specific date of birth
- **LOW_DOB/UP_DOB**: Range for random DOB generation
- **Validation**: LOW_DOB must be ≤ UP_DOB

#### SSN / SSN_MASK
- **SSN Format**: 9-digit number (no dashes)
- **SSN_MASK Format**: Up to 5 digits + `"*"` wildcard
- **Example SSN**: `"123456789"`
- **Example MASK**: `"00012*"` generates "00012" + 4 random digits

#### Email Parameters
- **EMAIL**: Full email address (max 50 chars)
- **EMAIL_MASK**: Domain name to append to generated usernames
- **Example EMAIL**: `"john.smith@example.com"`
- **Example MASK**: `"@va.gov"`

#### Security Code Parameters
- **ACCESS**: Access code (non-encrypted, for test environments only)
- **VERIFY**: Verify code (non-encrypted, for test environments only)
- **ELSIG**: Electronic signature (non-encrypted, for test environments only)
- **ACCESS_APND/VERIFY_APND/ELSIG_APND**: Characters to append to last name for code generation
- **GEN_ACCVER**: `0` or `1` to control automatic generation

#### USER_CLASS
- **Format**: User class name
- **Example**: `"PROVIDER"`, `"CLERK"`
- **File**: Pointer to USER CLASS file (#201)

#### SERVICE
- **Format**: Service/Section name
- **Example**: `"MEDICINE"`, `"SURGERY"`
- **File**: Pointer to SERVICE/SECTION file (#49)

## Output

### Success
```
ISIRESUL(0) = IEN  (newly created user's IEN in file #200)
```

### Error
```
ISIRESUL(0) = "-1^ERROR_MESSAGE"
```

## Example Usage

### Example 1: Create Single Physician
```
MISC(1) = "IMP_TYPE^I"
MISC(2) = "NAME^JONES,ROBERT"
MISC(3) = "SEX^M"
MISC(4) = "SERVICE^MEDICINE"
MISC(5) = "USER_CLASS^PROVIDER"
MISC(6) = "EMAIL^robert.jones@va.gov"

Returns: ISIRESUL(0) = 523  (new user IEN)
```

### Example 2: Batch Create 10 Providers Using Template
```
MISC(1) = "TEMPLATE^DEFAULT_PROVIDER"
MISC(2) = "IMP_TYPE^B"
MISC(3) = "IMP_BATCH_NUM^10"

Returns: Creates 10 users based on template settings
```

### Example 3: Create User with Masks for Random Values
```
MISC(1) = "IMP_TYPE^I"
MISC(2) = "NAME_MASK^*,PHYSICIAN"
MISC(3) = "SEX^M"
MISC(4) = "SSN_MASK^00012*"
MISC(5) = "LOW_DOB^2900101"
MISC(6) = "UP_DOB^3000101"
MISC(7) = "ZIP_4_MASK^2210*"
MISC(8) = "EMAIL_MASK^@testva.gov"
MISC(9) = "SERVICE^MEDICINE"
MISC(10) = "USER_CLASS^PROVIDER"

Returns: Creates user with random last name, SSN, DOB, ZIP, email
Example result: "ANDERSON,PHYSICIAN", SSN "000125678", DOB random between 1990-2000
```

### Example 4: Create User with Access Codes (TEST ONLY)
```
MISC(1) = "IMP_TYPE^I"
MISC(2) = "NAME^TEST,USER"
MISC(3) = "ACCESS^TESTACCESS123"
MISC(4) = "VERIFY^TESTVERIFY456"
MISC(5) = "ELSIG^TESTUSER123"

⚠️ WARNING: Non-encrypted codes - TEST ENVIRONMENTS ONLY
```

### Example 5: Merge User Profile from Existing User
```
MISC(1) = "IMP_TYPE^I"
MISC(2) = "NAME^NEWUSER,JOHN"
MISC(3) = "MRG_SOURCE^TEMPLATEUSER,JANE"
MISC(4) = "EMAIL^john.newuser@va.gov"

Returns: Creates new user with profile copied from TEMPLATEUSER,JANE
```

## Common Error Messages

| Error Message | Cause | Solution |
|---------------|-------|----------|
| `-1^Missing IMP_TYPE` | IMP_TYPE not provided | Include IMP_TYPE=I or B |
| `-1^Invalid IMP_TYPE` | IMP_TYPE not 'I' or 'B' | Use 'I' or 'B' only |
| `-1^Invalid IMP_BATCH_NUM` | Non-numeric batch number | Provide numeric value |
| `-1^Invalid DFN_NAME ('Y' or 'N')` | DFN_NAME not Y or N | Use 'Y' or 'N' |
| `-1^Invalid NAME` | Name format invalid | Use "LAST,FIRST" format |
| `-1^Must have either NAME or NAME_MASK` | Neither provided | Include NAME or NAME_MASK |
| `-1^Invalid SEX` | SEX not M or F | Use 'M' or 'F' |
| `-1^Invalid DOB` | Date format invalid | Use FileMan date format |
| `-1^Invalid LOW_DOB` | LOW_DOB > UP_DOB | Ensure LOW_DOB ≤ UP_DOB |
| `-1^Invalid TEMPLATE name` | Template not in #9001 | Check template exists |
| `-1^Bad parameter title passed` | Invalid parameter name | Check spelling |

## Related Files
- **#200**: NEW PERSON file (User file)
- **#201**: USER CLASS file
- **#49**: SERVICE/SECTION file
- **#5**: STATE file
- **#9001**: ISI PT IMPORT TEMPLATE file

## Validation Routine
- **ISIIMPUD**: Parameter parsing (`USRMISC^ISIIMPUD`)
- **ISIIMPUD**: Validation (`VALIDATE^ISIIMPUD`)

## API Routine
- **ISIIMP22**: Main API (`USER^ISIIMP22`)

## Processing Flow

1. Parse parameters via `USRMISC^ISIIMPUD`
2. If TEMPLATE specified, load template defaults
3. Overlay template with explicit parameters
4. Validate IMP_TYPE ('I' or 'B')
5. Validate NAME or NAME_MASK provided
6. Validate optional fields (SEX, DOB range, etc.)
7. Generate random values for mask fields (wildcards)
8. If batch mode, iterate IMP_BATCH_NUM times
9. Create user record in file #200
10. Set demographics and contact info
11. Assign USER_CLASS and SERVICE
12. If MRG_SOURCE provided, copy profile
13. Generate/set ACCESS/VERIFY codes if specified
14. Return new user IEN or error

## Template Support

### Using Templates
Templates stored in ISI PT IMPORT TEMPLATE file (#9001) can define default values for user creation. Parameters passed in MISC array overlay template values.

### Template Precedence
1. Template defaults loaded first (if TEMPLATE parameter provided)
2. Explicit MISC parameters overlay template values
3. Missing required fields cause validation errors

## Mask Wildcards

Masks use asterisk (`*`) as wildcard for random value generation:

- **NAME_MASK**: `"*,PROVIDER"` → Random last name + ",PROVIDER"
- **SSN_MASK**: `"00012*"` → "00012" + 4 random digits
- **ZIP_4_MASK**: `"2210*"` → "2210" + 1 random digit
- **EMAIL_MASK**: `"@va.gov"` → username + "@va.gov"
- **ELSIG_APND**: `"##"` → Last name + "##"

## Security Warning

⚠️ **IMPORTANT**: This RPC is intended for **TEST/DEVELOPMENT ENVIRONMENTS ONLY**

- Access codes, verify codes, and electronic signatures are stored **NON-ENCRYPTED**
- Do NOT use in production/clinical environments
- Do NOT use with real patient data or real user accounts
- Designed for creating test users in training/development systems
- Production user creation should use standard VistA menu options with proper encryption

## Notes
- Supports both individual and batch user creation
- Template-based creation enables consistent test data generation
- Mask wildcards allow random value generation for testing
- MRG_SOURCE copies user profile from existing user
- DFN_NAME option derives name from user IEN for unique test users
- All security codes are non-encrypted (test environments only)

## Version History
- V.1.0 (June 2012): Initial implementation
- V.2.0 (June 2014): Template support added
- V.3.0 (2018): License change to Apache 2.0
- V.3.1 (Build 70, Dec 2024): Continued maintenance
