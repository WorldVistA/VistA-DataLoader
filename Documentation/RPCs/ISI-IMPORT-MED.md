# ISI IMPORT MED

## Description
Creates medication orders in VistA. This RPC processes outpatient medication prescriptions including drug information, dosage instructions, quantity, supply days, refills, and expiration dates.

## RPC Name
`ISI IMPORT MED`

## Entry Point
[`MEDMAKE^ISIIMPR2`](../../VistA/Routines/ISIIMPR2.m)

## API Entry Point
[`$$MEDS^ISIIMP16`](../../VistA/Routines/ISIIMP16.m) - Main API  
[`$$VALIDATE^ISIIMP17`](../../VistA/Routines/ISIIMP17.m) - Validation  
[`$$MAKEMEDS^ISIIMP17`](../../VistA/Routines/ISIIMP17.m) - Creation

## Input Parameters

### MISC Array
Format: `MISC(n) = "PARAMETER^VALUE"`

| Parameter | Type | VistA File | Required | Description |
|-----------|------|------------|----------|-------------|
| **PAT_SSN** | FIELD | #2,.09 | Yes | PATIENT SSN or DFN |
| **DRUG** | FIELD | #50,.01 | Yes | Generic drug name or IEN |
| **DATE** | FIELD | - | Yes | Fill/Issue/Dispense date |
| **EXPIRDT** | FIELD | #50,17.1 | Yes | Expiration date |
| **SIG** | FIELD | #51,.01 | Yes | Medication instruction/SIG name |
| **QTY** | FIELD | - | Yes | Quantity (must be numeric) |
| **SUPPLY** | FIELD | #52,8 | Yes | Number of days supply (must be numeric) |
| **REFILL** | FIELD | - | Yes | Number of refills (must be numeric) |
| **PROV** | FIELD | #200,.01 | Yes | Provider name or IEN |

## Parameter Details

### PAT_SSN
- **Required**: Yes
- **Input**: SSN (9 digits) or DFN
- **Priority**: SSN lookup takes priority
- **Validation**: Must exist in PATIENT file (#2)
- **Converted to**: DFN

### DRUG
- **Required**: Yes
- **Input**: Drug name or IEN from DRUG file (#50)
- **RxNorm support**: Checks VA PRODUCT file (#50.68) for RxNorm CUI codes
- **Validation**:
  - Must exist in DRUG file (#50)
  - Must have pointer to Orderable Item (#50.7)
  - If by name, uses latest IEN (reverse order search)
- **Converted to**: Internal IEN

### DATE
- **Required**: Yes
- **Format**: VistA FileMan date or date/time
- **Default time**: If time missing, adds ".1200" (noon)
- **Purpose**: Fill/Issue/Dispense date
- **Conversion**: Supports relative dates

### EXPIRDT (Expiration Date)
- **Required**: Yes
- **Format**: VistA FileMan date
- **Purpose**: When the prescription expires
- **Validation**: Must be valid date format

### SIG (Medication Instruction)
- **Required**: Yes
- **Input**: Instruction name from MEDICATION INSTRUCTION file (#51)
- **Validation**:
  - Must exist in file #51
  - Intended use must NOT be "Inpatient only" (field #51,30 must not be >1)
- **Converted to**: Internal IEN

### QTY (Quantity)
- **Required**: Yes
- **Format**: Numeric value
- **Validation**: Must be a number (format: 1N.N)
- **Example**: "30" for 30 pills

### SUPPLY (Days Supply)
- **Required**: Yes
- **Format**: Numeric value
- **Validation**: Must be a number
- **Purpose**: Number of days the medication should last
- **Example**: "30" for 30 days

### REFILL (Number of Refills)
- **Required**: Yes
- **Format**: Numeric value (integer)
- **Validation**: Must be a number
- **Example**: "3" for 3 refills

### PROV (Provider)
- **Required**: Yes
- **Input**: Provider name or IEN from NEW PERSON file (#200)
- **Validation**: Must exist in file #200
- **Converted to**: Internal IEN

## Output

### Success
- `ISIRESUL(0) = 1` (or positive value)
- Medication order created

### Error
- `ISIRESUL(0) = -1^ERROR_MESSAGE`

Possible error messages:
- `-1^Bad parameter title passed: [PARAM]`
- `-1^No data provided for parameter: [PARAM]`
- `-1^Invalid DATE date/time`
- `-1^Invalid EXPIRDT date`
- `-1^Missing Patient SSN`
- `-1^Invalid PAT_SSN (#2,.09)`
- `-1^Missing DRUG (#50,.01) value`
- `-1^Invalid DRUG (#50,.01) value`
- `-1^Missing Fill Date`
- `-1^Missing Expire Date`
- `-1^Missing SIG (#51,.01) value`
- `-1^Invalid Medication Instruction/SIG (#51,.01) value`
- `-1^Missing QTY (quantity) value`
- `-1^Invalid QTY (quantity) value. Must be number`
- `-1^Missing SUPPLY value`
- `-1^Invalid SUPPLY value. Must be number`
- `-1^Missing REFILL value`
- `-1^Invalid REFILL value. Must be number`
- `-1^Missing PROV value`
- `-1^Invalid PROV value`

## Example Usage

### Example 1: Basic Medication Order
```
MISC(1) = "PAT_SSN^123456789"
MISC(2) = "DRUG^ASPIRIN"
MISC(3) = "DATE^T"
MISC(4) = "EXPIRDT^T+30"
MISC(5) = "SIG^TAKE ONE TABLET BY MOUTH DAILY"
MISC(6) = "QTY^30"
MISC(7) = "SUPPLY^30"
MISC(8) = "REFILL^3"
MISC(9) = "PROV^SMITH,JOHN"
```

### Example 2: Using Drug IEN
```
MISC(1) = "PAT_SSN^555001234"
MISC(2) = "DRUG^1234"
MISC(3) = "DATE^3000115.1200"
MISC(4) = "EXPIRDT^3000215"
MISC(5) = "SIG^TAKE TWO TABLETS BY MOUTH TWICE DAILY"
MISC(6) = "QTY^120"
MISC(7) = "SUPPLY^30"
MISC(8) = "REFILL^2"
MISC(9) = "PROV^126"
```

### Example 3: Antibiotic Short Course
```
MISC(1) = "PAT_SSN^987654321"
MISC(2) = "DRUG^AMOXICILLIN"
MISC(3) = "DATE^T"
MISC(4) = "EXPIRDT^T+10"
MISC(5) = "SIG^TAKE ONE CAPSULE BY MOUTH THREE TIMES DAILY"
MISC(6) = "QTY^21"
MISC(7) = "SUPPLY^7"
MISC(8) = "REFILL^0"
MISC(9) = "PROV^DOE,JANE"
```

### Example 4: Chronic Medication
```
MISC(1) = "PAT_SSN^111223333"
MISC(2) = "DRUG^LISINOPRIL 10MG TAB"
MISC(3) = "DATE^3000101"
MISC(4) = "EXPIRDT^3010101"
MISC(5) = "SIG^TAKE ONE TABLET BY MOUTH DAILY"
MISC(6) = "QTY^90"
MISC(7) = "SUPPLY^90"
MISC(8) = "REFILL^11"
MISC(9) = "PROV^BROWN,ROBERT"
```

## Common SIG Values

Common medication instructions from file #51:
- `TAKE ONE TABLET BY MOUTH DAILY`
- `TAKE TWO TABLETS BY MOUTH TWICE DAILY`
- `TAKE ONE CAPSULE BY MOUTH THREE TIMES DAILY`
- `APPLY TO AFFECTED AREA TWICE DAILY`
- `INSTILL ONE DROP IN EACH EYE TWICE DAILY`
- `INJECT SUBCUTANEOUSLY AS DIRECTED`
- `TAKE ONE TABLET BY MOUTH AT BEDTIME`

## Related Files

- **#2**: PATIENT file
- **#50**: DRUG file
- **#50.7**: PHARMACY ORDERABLE ITEM file
- **#50.68**: VA PRODUCT file (RxNorm support)
- **#51**: MEDICATION INSTRUCTION file
- **#52**: PRESCRIPTION file
- **#200**: NEW PERSON file (providers)
- **#55**: PATIENT MEDICATIONS file

## Validation

### Performed by: `VALMEDS^ISIIMPU9`

Validations include:
- Patient SSN existence
- Drug existence and validity
- Orderable item pointer presence
- RxNorm CUI lookup (if applicable)
- SIG existence and not inpatient-only
- All numeric fields are numbers
- Provider existence
- Date format validation
- Expiration date format

## RxNorm Support

The routine includes special handling for RxNorm codes:
- Checks VA PRODUCT file (#50.68) for RxNorm CUI values
- If RxNorm CUI found, maps to corresponding DRUG entry
- Uses "VARXCUI" index for lookup
- Automatically resolves to drug name for VistA processing

This is an Oroville Hospital-specific enhancement for QRDA support.

## Processing Flow

1. **Parse Parameters**: Convert MISC array via `MEDMISC^ISIIMPU9`
2. **Default Time**: Add ".1200" to DATE if time missing
3. **Validate Input**: Verify all parameters via `VALMEDS^ISIIMPU9`
4. **RxNorm Lookup**: Check for RxNorm CUI if applicable
5. **Resolve References**:
   - Convert PAT_SSN to DFN
   - Convert DRUG name to IEN (latest if multiple)
   - Validate orderable item pointer
   - Convert SIG name to IEN
   - Convert PROV name to IEN
6. **Create Order**: Call API `$$MEDS^ISIIMP16`
7. **Return Result**: Success or error code

## Notes

- Default time of 12:00 (noon) added if only date provided for DATE parameter
- Drug search uses reverse order (-1) to get latest entry if multiple matches exist
- SIG instructions marked as "Inpatient only" are rejected
- The routine validates that drug entries have required pointers to orderable items
- Quantity, Supply, and Refill must all be numeric values
- RxNorm CUI support allows importing medications using standardized codes
- Expiration date must be provided separately from fill date
- The routine creates outpatient prescriptions in the PRESCRIPTION file (#52)

## Version History
- V.1.0 (June 2012): Initial implementation
- V.2.0 (June 2014): Updates and enhancements
- V.2.1 (Nov 2014): RxNorm support added (Oroville Hospital)
- V.3.0 (2018): License change to Apache 2.0
- V.3.1 (2024-2025): Continued maintenance
