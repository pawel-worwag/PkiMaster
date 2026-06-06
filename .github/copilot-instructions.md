# Copilot Instructions for PkiMaster

## Project Context
- The application name is **PkiMaster**.
- PkiMaster is used to manage **Public Key Infrastructure (PKI)**.

## Core Priorities
- Maintain a **high level of RFC compliance** across features and implementations.
- Put **strong emphasis on security** in architecture, coding, and operational decisions.

## Technology Stack
- Use **.NET 10** and **Blazor** as the primary stack.

## Architecture and Design
- Implement the solution using **Onion Architecture**.
- Design and implement features using **Vertical Slices** principles.

## Mandatory Architecture Gate
- Onion/Vertical Slice rules are **non-negotiable** and must not be bypassed for speed.
- Before writing code, explicitly verify and keep this structure:
    - Use cases must be implemented in `Application` as feature slices (for example: feature folder with command/query + handler).
- If a planned change would violate Onion or Vertical Slice rules, stop and redesign first; do not implement the violating version.
- In the final response for implementation tasks, include a short compliance statement with file evidence:
    - `Onion: OK/NOK` + affected files.
    - `Vertical Slice: OK/NOK` + affected files.

## Coding and Language Standards
- Keep all code, comments, file names, and commit descriptions in **English**.
- The only exception is files explicitly related to **internationalization (i18n)**.

## API Style
- Prefer **Minimal APIs** over traditional MVC/API controllers.
- Every contract returned by API endpoints to users must be defined in `PkiMaster.Dto`.
- This DTO package is treated as a reusable NuGet for dependent projects and must stay complete.

## C# Conventions
- Use **primary constructors** instead of traditional constructors whenever practical.
- Use **records** for DTOs.

## Dependency Management
- Use **central package management** for NuGet dependencies.

## Build Verification
- After implementing changes, always verify that the program compiles before reporting task completion.
