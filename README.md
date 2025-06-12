# CodexSample

This repository now includes a simple clinic management API built with .NET 8 and a vertical slice architecture. The API allows creating patients and appointments and stores data in SQL Server using Entity Framework Core.

## Building
```
dotnet build ClinicManagement.sln
```

## Running
```
dotnet run --project src/ClinicManagement.Api

It includes pages for patient and appointment management as well as simple
registration and login forms so the UI can grow as the API expands.
