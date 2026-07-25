# ISI IMPORT GET TEMPLATES

## Description
Fetches the list of available patient import templates. Templates are stored in ISI PT IMPORT TEMPLATE file (#9001) and define predefined demographics and masks for random value generation during patient creation.

## RPC Name
`ISI IMPORT GET TEMPLATES`

## Entry Point
[`FETCHTMP^ISIIMPUA`](../../VistA/Routines/ISIIMPUA.m)

## Parameters

None required.

## Output

### Success
Array of template names from file #9001:
```
ISIRESUL(1) = "TEMPLATE_NAME_1"
ISIRESUL(2) = "TEMPLATE_NAME_2"
...
```

### Empty Result
```
ISIRESUL(0) = ""
```

## Example

### Call
```
D FETCHTMP^ISIIMPUA(.ISIRESUL)
```

### Sample Output
```
ISIRESUL(1) = "DEFAULT_VETERAN"
ISIRESUL(2) = "DEFAULT_EMPLOYEE"
ISIRESUL(3) = "TEST_PATIENT"
ISIRESUL(4) = "CLINIC_PATIENT"
```

## Processing Flow

1. **Query File #9001**: Retrieve all template names
2. **Build Array**: Populate ISIRESUL with template names
3. **Return Result**: Array of available templates

## Related VistA Files

- **#9001**: ISI PT IMPORT TEMPLATE (source file)

## Template File Structure

### ISI PT IMPORT TEMPLATE (#9001)

| Field | Number | Description |
|-------|--------|-------------|
| NAME | .01 | Template name (unique identifier) |
| TYPE | 1 | TYPE OF PATIENT (#391) |
| NAME MASK | 2 | Last name mask for random generation |
| SSN MASK | 4 | SSN mask (5 digits max) |
| SEX | 5 | M or F |
| EARLIEST DATE OF BIRTH | 6 | Low DOB limit |
| LATEST DATE OF BIRTH | 7 | High DOB limit |
| MARITAL STATUS | 8 | Pointer to #11 |
| ZIP+4 MASK | 9 | Zip code mask |
| PHONE NUMBER MASK | 10 | Phone mask |
| CITY | 11 | City name |
| STATE | 12 | Pointer to #5 |
| VETERAN | 13 | Y/N |
| DFN_NAME | 14 | Y/N |
| EMPLOYMENT STATUS | 15 | Set value |
| SERVICE | 16 | Pointer to #49 |

## Use Cases

### List Available Templates
Use this RPC to:
- Display available templates in UI
- Validate template exists before use
- Provide selection list to users

### Template Selection
Retrieved template names can be used with:
- [ISI IMPORT GET TEMPLATE DETLS](ISI-IMPORT-GET-TEMPLATE-DETLS.md) - Get template details
- [ISI IMPORT PAT](ISI-IMPORT-PAT.md) - Create patients using template

## Related RPCs

- **[ISI IMPORT GET TEMPLATE DETLS](ISI-IMPORT-GET-TEMPLATE-DETLS.md)** - Get specific template configuration
- **[ISI IMPORT SAVE TEMPLATE](ISI-IMPORT-SAVE-TEMPLATE.md)** - Create/update templates
- **[ISI IMPORT PAT](ISI-IMPORT-PAT.md)** - Use templates to create patients

## Notes

- Returns only template names, not full configuration
- Templates enable batch patient creation with consistent demographics
- Use ISI IMPORT GET TEMPLATE DETLS to retrieve full template details
- Empty result indicates no templates are defined
- Template names must be unique within file #9001

## Version History

- V.1.0 (June 2012): Initial implementation
- V.2.0 (June 2014): Updates
- V.3.0 (2018): License change to Apache 2.0
- V.3.1 (2024-2025): Continued maintenance
