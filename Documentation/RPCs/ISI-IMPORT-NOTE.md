# ISI IMPORT NOTE

## Description
Creates TIU (Text Integration Utilities) progress notes and clinical documents in VistA. This RPC generates signed clinical documentation associated with patient visits.

## RPC Name
`ISI IMPORT NOTE`

## Entry Point
[`NOTEMAKE^ISIIMPR2`](../../VistA/Routines/ISIIMPR2.m)

## API Entry Point
[`$$NOTES^ISIIMP14`](../../VistA/Routines/ISIIMP14.m) - Main API  
[`$$VALIDATE^ISIIMP15`](../../VistA/Routines/ISIIMP15.m) - Validation

## Input Parameters

### MISC Array
Format: `MISC(n) = "PARAMETER^VALUE"`

| Parameter | Type | VistA File | Required | Description |
|-----------|------|------------|----------|-------------|
| **PAT_SSN** | FIELD | #2 | Yes | PATIENT SSN or DFN |
| **TIU_NAME** | FIELD | #8925.1 | Yes | TIU Document Definition name |
| **VDT** | FIELD | - | Yes | Visit Date/Time |
| **VLOC** | FIELD | #44 | Yes | Visit Location (HOSPITAL LOCATION) |
| **PROV** | FIELD | #200 | Yes | Provider (author/signer) |
| **TEXT** | FIELD | - | Yes | Note text content |

## Parameter Details

### PAT_SSN
- **Required**: Yes
- **Input**: SSN (9 digits) or DFN
- **Priority**: SSN lookup takes priority
- **Validation**: Must exist in PATIENT file (#2)
- **Converted to**: DFN

### TIU_NAME (Document Definition)
- **Required**: Yes
- **Input**: Document title from TIU DOCUMENT DEFINITION file (#8925.1)
- **Common values**:
  - PROGRESS NOTE
  - DISCHARGE SUMMARY
  - CONSULTATION
  - PROCEDURE NOTE
  - HISTORY & PHYSICAL
  - OPERATIVE REPORT
- **Validation**:
  - Must exist in file #8925.1
  - TYPE must be "DOC" (document, not folder/template)
  - STATUS must be 11 (Active)
  - Must NOT be a CONSULT type
- **Converted to**: TIU IEN

### VDT (Visit Date/Time)
- **Required**: Yes
- **Format**: VistA FileMan date/time
- **Default time**: If time missing, adds ".1200" (noon)
- **Purpose**: When the clinical encounter occurred
- **Validation**: Must be valid date/time format

### VLOC (Visit Location)
- **Required**: Yes
- **Input**: Hospital location name or IEN from file #44
- **Validation**:
  - Must exist in HOSPITAL LOCATION file (#44)
  - Must be active (inactivation date check)
- **Converted to**: Internal IEN

### PROV (Provider)
- **Required**: Yes
- **Input**: Provider name or IEN from NEW PERSON file (#200)
- **Validation**:
  - Must exist in file #200
  - Must be authorized to write medical orders (field #200,"PS" = 1)
  - Must have electronic signature defined (field #200,20.4)
- **Purpose**: Author and expected signer of the note
- **Converted to**: Internal IEN

### TEXT
- **Required**: Yes
- **Input**: Free text note content
- **Format**: Can be single line or multiple lines
- **Purpose**: The actual clinical documentation
- **Example**: "Patient presents with complaint of chest pain..."

## Output

### Success
- `ISIRESUL(0) = 1`
- `ISIRESUL(1) = TIUDA` (TIU document IEN)
- Note created and signed in TIU DOCUMENT file

### Error
- `ISIRESUL(0) = -1^ERROR_MESSAGE`

Possible error messages:
- `-1^Bad parameter title passed: [PARAM]`
- `-1^No data provided for parameter: [PARAM]`
- `-1^Invalid VDT date/time`
- `-1^Missing Patient SSN`
- `-1^Invalid PAT_SSN (#2,.09)`
- `-1^Missing TIU_NAME`
- `-1^Invalid TIU_NAME (#8925.1)`
- `-1^Missing value for VDT (Visit Date/time)`
- `-1^Missing VLOC (Hospital Location #44)`
- `-1^Invalid LOCATION (VLOC) value (#44,.01)`
- `-1^Missing PROV (#200,.01)`
- `-1^Invalid PROV value (#200,.01)`
- `-1^Missing note text`
- `-1^PROVIDER (#200) missing Electronic Signature`

## Example Usage

### Example 1: Basic Progress Note
```
MISC(1) = "PAT_SSN^123456789"
MISC(2) = "TIU_NAME^PROGRESS NOTE"
MISC(3) = "VDT^T@14:00"
MISC(4) = "VLOC^PRIMARY CARE"
MISC(5) = "PROV^SMITH,JOHN"
MISC(6) = "TEXT^Patient presents for follow-up. Blood pressure well controlled. Continue current medications."
```

### Example 2: Multi-line Note
```
MISC(1) = "PAT_SSN^555001234"
MISC(2) = "TIU_NAME^PROGRESS NOTE"
MISC(3) = "VDT^3000115.0930"
MISC(4) = "VLOC^CARDIOLOGY"
MISC(5) = "PROV^DOE,JANE"
MISC(6) = "TEXT^CHIEF COMPLAINT: Chest pain. HISTORY: 65yo M with h/o CAD presents with substernal chest pain x2 hours. EXAM: BP 145/90, HR 88. ASSESSMENT: Stable angina. PLAN: Adjust medications, schedule stress test."
```

### Example 3: Consultation Note
```
MISC(1) = "PAT_SSN^987654321"
MISC(2) = "TIU_NAME^CONSULTATION"
MISC(3) = "VDT^T-1@10:30"
MISC(4) = "VLOC^NEUROLOGY"
MISC(5) = "PROV^BROWN,ROBERT"
MISC(6) = "TEXT^Thank you for this neurology consultation. Patient evaluated for chronic headaches. Recommend MRI brain and initiation of prophylactic therapy."
```

### Example 4: Procedure Note
```
MISC(1) = "PAT_SSN^111223333"
MISC(2) = "TIU_NAME^PROCEDURE NOTE"
MISC(3) = "VDT^T@15:30"
MISC(4) = "VLOC^PROCEDURE ROOM"
MISC(5) = "PROV^WILSON,MARY"
MISC(6) = "TEXT^PROCEDURE: Joint injection, right knee. INDICATION: Osteoarthritis. TECHNIQUE: Sterile prep and drape. 40mg Kenalog injected into joint space. COMPLICATIONS: None. Patient tolerated well."
```

## Electronic Signature Requirement

The provider MUST have an electronic signature defined in field #200,20.4. This is used to automatically sign the note. Without it, validation fails:
```
-1^PROVIDER (#200) missing Electronic Signature
```

The routine automatically retrieves the E-sig:
```mumps
S ISIMISC("ES")=$P($G(^VA(200,ISIMISC("PROV"),20)),U,4)
```

## TIU Document Type Validation

The TIU_NAME must meet these criteria:
1. **TYPE** = "DOC" (document type, not folder or template)
2. **STATUS** = 11 (Active status)
3. **NOT** a CONSULT type (checked via `ISCNSLT^TIUCNSLT`)

Invalid document types are rejected during validation.

## Related Files

- **#2**: PATIENT file
- **#8925**: TIU DOCUMENT file
- **#8925.1**: TIU DOCUMENT DEFINITION file
- **#44**: HOSPITAL LOCATION file
- **#200**: NEW PERSON file
- **#9000010**: OUTPATIENT ENCOUNTER file (visit linkage)

## Validation

### Performed by: `VALNOTE^ISIIMPU8`

Validations include:
- Patient SSN existence
- TIU document definition existence
- Document type is "DOC"
- Document status is Active (11)
- Document is not CONSULT type
- Visit date/time format
- Hospital location existence and active status
- Provider existence
- Provider authorization to write orders
- Electronic signature presence
- Note text presence

## Processing Flow

1. **Parse Parameters**: Convert MISC array via `NOTMISC^ISIIMPU8`
2. **Default Time**: Add ".1200" to VDT if time missing
3. **Validate Input**: Verify all parameters via `VALNOTE^ISIIMPU8`
4. **Resolve TIU Definition**:
   - Look up by name in file #8925.1
   - Verify TYPE = "DOC"
   - Verify STATUS = 11 (Active)
   - Check not CONSULT type
5. **Resolve References**:
   - Convert PAT_SSN to DFN
   - Convert VLOC name to IEN, check active
   - Convert PROV name to IEN
   - Retrieve provider's electronic signature
6. **Create Note**: Call API `$$NOTES^ISIIMP14`
   - Create TIU document
   - Link to visit
   - Insert text
   - Sign with electronic signature
7. **Return Result**: TIUDA or error code

## Auto-Signing

The routine automatically signs the note using the provider's electronic signature. This is different from unsigned notes that require manual signature. The E-sig is retrieved and applied during note creation.

## Location Active Status

The location is checked for active status using:
```mumps
S IDT=$P($G(^SC(Y,"I")),U)      ; Inactivation date
S RDT=$P($G(^SC(Y,"I")),U,2)    ; Reactivation date
```

The validation ensures the location is active on the visit date (VDT).

## Notes

- Default time of 12:00 (noon) added to VDT if only date provided
- Provider must have authorization to write medical orders
- Electronic signature is mandatory for automatic signing
- Document definition must be active and of type "DOC"
- CONSULT type documents are explicitly excluded
- Location must be active on the visit date
- The note is created and signed in a single operation
- TEXT content can be single or multi-line free text
- The routine creates TIU documents linked to outpatient encounters
