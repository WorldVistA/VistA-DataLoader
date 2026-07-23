# ISI IMPORT CONSULT

## Description
Creates consult requests in VistA. This RPC generates consultation orders for specialist referrals and creates associated documentation.

## RPC Name
`ISI IMPORT CONSULT`

## Entry Point
[`CONMAKE^ISIIMPR2`](../../VistA/Routines/ISIIMPR2.m#L161)

## API Entry Point
[`$$CONSULTS^ISIIMP18`](../../VistA/Routines/ISIIMP18.m#L26) - Main API  
[`$$VALIDATE^ISIIMP19`](../../VistA/Routines/ISIIMP19.m#L27) - Validation  
[`$$MAKECONS^ISIIMP19`](../../VistA/Routines/ISIIMP19.m#L32) - Creation

## Input Parameters

### MISC Array
Format: `MISC(n) = "PARAMETER^VALUE"`

| Parameter | Type | VistA File | Required | Description |
|-----------|------|------------|----------|-------------|
| **PAT_SSN** | FIELD | #2,.09 | Yes | PATIENT SSN or DFN |
| **CONSULT** | FIELD | #123.5,.01 | Yes | Consult service name |
| **LOC** | FIELD | #44,.01 | Yes | Visit/Request location (HOSPITAL LOCATION) |
| **PROV** | FIELD | #200 | No | Provider (defaults to logged-in user if not provided) |
| **TEXT** | FIELD | - | No | Consult request text/reason (defaults to "Consult order.") |

## Parameter Details

### PAT_SSN
- **Required**: Yes
- **Input**: SSN (9 digits) or DFN
- **Priority**: SSN lookup takes priority
- **Validation**: Must exist in PATIENT file (#2)
- **Converted to**: DFN

### CONSULT
- **Required**: Yes
- **Input**: Consult service name from REQUEST SERVICES file (#123.5)
- **Resolution**: Uses `SVCSYN^ORQQCN2` to get valid service synonyms
- **Common values**:
  - CARDIOLOGY
  - DERMATOLOGY
  - NEUROLOGY
  - OPHTHALMOLOGY
  - ORTHOPEDICS
  - PSYCHIATRY
  - SURGERY
  - UROLOGY
- **Validation**: Must exist and have valid ORDERITEM
- **Auto-conversion**: Converts to ORDERITEM for order processing

### LOC (Location)
- **Required**: Yes
- **Input**: Hospital location name or IEN from file #44
- **Validation**:
  - Must exist in HOSPITAL LOCATION file (#44)
  - Must be active (checks inactive date)
- **Converted to**: Internal IEN

### PROV (Provider)
- **Required**: No
- **Input**: Provider name or IEN from NEW PERSON file (#200)
- **Default**: If not provided, uses logged-in user (DUZ)
- **Validation**:
  - Must exist in file #200
  - Must be authorized to write medical orders (field #200,"PS" = 1)
  - Must have PROVIDER security key
  - **Must have Electronic Signature** configured in field #200,20.4
- **Converted to**: Internal IEN
- **Important**: Electronic signature is automatically retrieved from the provider's user record - you do NOT pass it as a parameter

### TEXT
- **Required**: No
- **Input**: Free text describing consult reason
- **Default**: "Consult order." if not provided
- **Purpose**: Clinical reason for consultation request
- **Example**: "Evaluate chronic knee pain", "Pre-operative clearance"

## Output

### Success
- `ISIRESUL(0) = 1` (or positive value)
- Consult request created

### Error
- `ISIRESUL(0) = -1^ERROR_MESSAGE`

Possible error messages:
- `-1^Bad parameter title passed: [PARAM]`
- `-1^No data provided for parameter: [PARAM]`
- `-1^Missing Patient SSN`
- `-1^Invalid PAT_SSN (#2,.09)`
- `-1^Missing CONSULT (#123.5)`
- `-1^Invalid CONSULT (#123.5)`
- `-1^Missing LOC (Hospital Location #44)`
- `-1^Invalid LOCATION value (#44,.01)`
- `-1^Invalid PROV value (#200, .01)`
- `-1^PROVIDER missing Electronic Signature (#200,20.4)`

## Example Usage

### Example 1: Basic Consult Request
```
MISC(1) = "PAT_SSN^123456789"
MISC(2) = "CONSULT^CARDIOLOGY"
MISC(3) = "LOC^PRIMARY CARE"
MISC(4) = "PROV^SMITH,JOHN"
MISC(5) = "TEXT^Evaluate chest pain and abnormal ECG"
```

### Example 2: Using Defaults
```
MISC(1) = "PAT_SSN^555001234"
MISC(2) = "CONSULT^DERMATOLOGY"
MISC(3) = "LOC^GENERAL MEDICINE"
```

### Example 3: Surgical Consult
```
MISC(1) = "PAT_SSN^987654321"
MISC(2) = "CONSULT^SURGERY"
MISC(3) = "LOC^ER"
MISC(4) = "PROV^DOE,JANE"
MISC(5) = "TEXT^Acute appendicitis, evaluate for emergency appendectomy"
```

### Example 4: Pre-op Clearance
```
MISC(1) = "PAT_SSN^111223333"
MISC(2) = "CONSULT^CARDIOLOGY"
MISC(3) = "LOC^SURGICAL CLINIC"
MISC(4) = "PROV^126"
MISC(5) = "TEXT^Pre-operative cardiac clearance for knee replacement"
```

## Related Files

- **#2**: PATIENT file
- **#123.5**: REQUEST SERVICES file
- **#123**: CONSULT/REQUEST TRACKING file
- **#44**: HOSPITAL LOCATION file
- **#200**: NEW PERSON file (providers)
- **#101.41**: ORDERABLE ITEMS file

## Validation

### Performed by: `VALCONS^ISIIMPUB`

Validations include:
- Patient SSN existence
- Consult service existence via synonym lookup
- ORDERITEM resolution for service
- Hospital location existence and active status
- Provider existence (or default to DUZ)
- Provider authorization to write orders
- Provider has PROVIDER security key
- Electronic signature presence for provider

## Electronic Signature Requirement

⚠️ **Important**: The electronic signature is **NOT** passed as a parameter. Instead:

1. The system automatically retrieves it from the provider's user record (field #200,20.4)
2. The provider must have an electronic signature configured in VistA before creating consults
3. If the provider has no electronic signature, validation fails with:
   ```
   -1^PROVIDER missing Electronic Signature (#200,20.4)
   ```

**To configure a provider's electronic signature in VistA:**
- Menu: EVE → User Options → Enter/Edit Electronic Signature
- Or directly edit field #200,20.4 in the NEW PERSON file

## Processing Flow

1. **Parse Parameters**: Convert MISC array via `CONMISC^ISIIMPUB`
2. **Validate Input**: Verify parameters via `VALCONS^ISIIMPUB`
3. **Resolve Service**:
   - Call `SVCSYN^ORQQCN2` to get valid services
   - Search by service name in synonyms
   - Extract ORDERITEM from service definition
4. **Resolve References**:
   - Convert PAT_SSN to DFN
   - Convert LOC name to IEN, check active status
   - Convert PROV name to IEN (or default to DUZ)
   - Validate provider authority and security
5. **Check Electronic Signature**: Verify provider has E-sig
6. **Create Consult**: Call API `$$CONSULTS^ISIIMP18`
7. **Return Result**: Success or error code

## Service Synonym Lookup

The routine uses `SVCSYN^ORQQCN2(.RESULT,1,1,1)` to retrieve valid consultation services. This returns an array with:
- **Electronic signature is NOT a parameter** - it's automatically retrieved from provider's user record (field #200,20.4)
- TEXT parameter defaults to "Consult order." if not provided
- PROV parameter defaults to current user (DUZ) if not provided
- Provider must have authorization to write medical orders
- Provider must have the PROVIDER security key
- Electronic signature is mandatory for the ordering provider (system retrieves it automatically)
- Location must be active at the time of consult request
- The routine uses VistA's standard consult ordering APIs
- Consult requests are created in CONSULT/REQUEST TRACKING file (#123)
- The consult is automatically signed using the provider's electronic signature
- TEXT parameter defaults to "Consult order." if not provided
- PROV parameter defaults to current user (DUZ) if not provided
- Provider must have authorization to write medical orders
- Provider must have the PROVIDER security key
- Electronic signature is mandatory for the ordering provider
- Location must be active at the time of consult request
- The routine uses VistA's standard consult ordering APIs
- Consult requests are created in CONSULT/REQUEST TRACKING file (#123)

## Version History
- V.1.0 (June 2012): Initial implementation
- V.2.0 (June 2014): Updates and enhancements
- V.3.0 (2018): License change to Apache 2.0
- V.3.1 (2024-2025): Continued maintenance
