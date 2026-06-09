# Development Status and Gap Analysis — VistA-DataLoader (ISI)

Status date: 2026-06-09
Branch at time of writing: `fix/patient-state-pointer-file5`

Part of the VistA-on-FHIR workspace. Ecosystem-level context lives in
`VistA-FHIR-Server-Codex/docs/PROJECT_OVERVIEW.md`; the cross-repo roadmap is
`VistA-FHIR-Server-Codex/docs/PATH_FORWARD.md`.

## Role of this repository

The original Johns Hopkins / WorldVistA data loader for demo and teaching
cases (Excel → ISI DATA IMPORT → FileMan). In the current stack it is the
**foundational filing layer** under the FHIR loader: `SYNDHP*` routines in
`VistA-FHIR-Data-Loader` call the `ISIIMP*` APIs here for patient creation,
labs, and other filing operations. Its KIDS build
(`VistA/VISTA_DATALOADER_3P1.KID`) is the required first install before the
SYN KIDS.

## What is working today

- **Stable v3.1** on `master` (lab panels via API, Dec 2024).
- **M filing routines**: `VistA/Routines/ISIIMP.m` + `ISIIMP03.m`–`ISIIMP27.m`
  covering patient, labs, meds, encounters, etc., with validation in
  `ISIIMPU1.m` and templates in file `#9001`.
- **Documented API**: `Documentation/API_Detail.txt`,
  `Declarations_API_and_RPC.txt`, and a user guide PDF.
- **Windows .NET client** (`Data Loader/`) for the classic Excel workflow.

Active branch fixes patient STATE (file #5) pointer normalization in
`ISIIMP03.m`/`ISIIMPU1.m` so imported addresses display correctly.

## Gap analysis

1. **Lab panels are API-only** (v3.1); the .NET client does not expose them.
2. **No FHIR awareness** — by design; all FHIR semantics live upstream in SYN.
   This is the correct separation and should be preserved.
3. **Minimal automated tests** — `ISIIMPLT.m` covers lab RPCs only; no CI.
4. **Windows-centric client** (VS 2017, MSI installer). The Linux/docker
   workflow bypasses it via M APIs, which is the path that matters for the
   FHIR stack; the client is effectively in maintenance mode.
5. **Configuration sensitivity** — provider names, drug file entries, security
   keys (`LRLAB`, `LRVERIFY`, `ORES`, `PROVIDER`) must exist per target
   system; documented in README but easy to miss on fresh containers.
6. **Branch hygiene** — the state-pointer fix branch should be merged and a
   3.1.x patch KIDS cut so SYN installs do not depend on an unmerged branch.

## API stability note

Because `SYNDHP*` (in VistA-FHIR-Data-Loader) calls `ISIIMP*` signatures
directly, any signature change here is a cross-repo breaking change. Treat
the `ISIIMP*` entry points as a published API: additive changes only, or
coordinated version bumps with the SYN KIDS.

## Integration points

| Repo | Relationship |
|---|---|
| VistA-FHIR-Data-Loader (SYN) | Calls `ISIIMP*` via `SYNDHP*` wrappers; requires this KIDS first |
| VistA-FHIR-Server-Codex | Indirect — C0FW `isi` engine policy can target ISI filing |
| vehu10 / OSEHRA containers | KIDS install target |
