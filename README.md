# CodexSample

This repository now includes a simple clinic management API built with .NET 8 and a vertical slice architecture. The API allows creating patients and appointments and stores data in SQL Server using Entity Framework Core.

## Building
```
dotnet build ClinicManagement.sln
```

## Running
```
dotnet run --project src/ClinicManagement.Api
```

### Frontend
An Angular client using Bootstrap is located under `src/ClinicManagement.Client`.

To install dependencies and build the client:
```
cd src/ClinicManagement.Client
npm install
npm run build
```
