# ISI IMPORT ICDFIND

## Description
Searches for ICD (International Classification of Diseases) code descriptions. This utility RPC looks up ICD-9 and ICD-10 codes and returns their textual descriptions.

## RPC Name
`ISI IMPORT ICDFIND`

## Entry Point
`ICD9GET^ISIIMPR2`

## API Entry Point
`ICD9^ISIIMPUA(.ISIRESUL,.TXT)`

## Input Parameters

### Single Parameter
- **TXT**: ICD code or partial description to search for
- Format: Text string (case-insensitive)
- Example: "250.00", "DIABETES", "E11.9"

## Output

### Success
- `ISIRESUL(0) = CNT` (count of matches)
- `ISIRESUL(1..n) = ICD_CODE^DESCRIPTION`

### Error
- `ISIRESUL(0) = "-1^Incorrect parameter passed"`

## Example Usage

### Example 1: Search by ICD-9 Code
```
TXT = "250.00"

Returns:
ISIRESUL(0) = 1
ISIRESUL(1) = "250.00^DIABETES MELLITUS WITHOUT MENTION OF COMPLICATION"
```

### Example 2: Search by ICD-10 Code
```
TXT = "E11.9"

Returns:
ISIRESUL(0) = 1
ISIRESUL(1) = "E11.9^TYPE 2 DIABETES MELLITUS WITHOUT COMPLICATIONS"
```

### Example 3: Search by Description
```
TXT = "DIABETES"

Returns:
ISIRESUL(0) = 15
ISIRESUL(1) = "250.00^DIABETES MELLITUS WITHOUT MENTION OF COMPLICATION"
ISIRESUL(2) = "250.01^DIABETES MELLITUS WITH KETOACIDOSIS"
ISIRESUL(3) = "250.02^DIABETES MELLITUS WITH HYPEROSMOLARITY"
...
```

## Related Files
- **#80**: ICD DIAGNOSIS file
- **#80.1**: ICD-9 DIAGNOSIS file (legacy)
- **#757.01**: EXPRESSIONS file (Clinical Lexicon)

## Processing
1. Convert search text to uppercase
2. Call `ICD9^ISIIMPUA(.ISIRESUL,.TXT)`
3. Search ICD files by code or description
4. Return matches in format: CODE^DESCRIPTION

## Notes
- Search is case-insensitive
- Supports both ICD-9 and ICD-10 codes
- Can search by code or description
- Returns multiple matches if search is partial
- Used to validate ICD codes before creating diagnoses

## Version History
- V.1.0 (June 2012): Initial ICD-9 support
- V.2.0 (June 2014): ICD-10 support added
- V.3.0 (2018): License change to Apache 2.0
- V.3.1 (2024-2025): Continued maintenance
