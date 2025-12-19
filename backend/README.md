# Backend – Teachers TimeTable Management System

This folder contains the **backend services** for the **Teachers TimeTable Management System**, implemented using **ASP.NET Core** and following **Clean Architecture** and **Domain-Driven Design (DDD)** principles.

The backend is responsible for:
- Timetable ingestion and processing
- OCR integration (Tesseract)
- AI integration (Gemini)
- Business rules and persistence
- API exposure for the frontend UI

---
## API Overview

The backend exposes APIs for:
- Timetable upload and extraction
- Timetable management and retrieval
- Health and system status

Refer to Swagger for detailed endpoint definitions.
## Technology Stack

- ASP.NET Core (current version)
- C#
- Clean Architecture
- Domain-Driven Design (DDD)
- NuGet package management
- OCR: Tesseract
- AI Integration: Gemini API

---

## Prerequisites

### 1. Install Visual Studio 2026

- Download **Visual Studio 2026 from below link ( Community is free version, you can download without any subscription). It can work with anyone of three version Community, Professional or Enterprise** 

https://visualstudio.microsoft.com/downloads/

- During installation, select:
  - **ASP.NET and web development**
  - **.NET desktop development** (recommended)

Verify installation by ensuring ASP.NET Core project templates are available.

---

### 2. .NET SDK

Verify .NET SDK installation:

```command prompt(in windows OS) or bash shell
dotnet --version
```

---

## Backend Setup Instructions

### Step 1: Open the Backend Solution

Open the backend solution (`.sln`) using **Visual Studio 2026**.

Example:
```
TeachersTimeTableManagementSystem/backend/TeachersTimeTable.sln
```

---

### Step 2: Restore NuGet Packages

All required NuGet packages must be restored.

**Using Visual Studio**
- Packages restore automatically on solution load

**Using Command Line (This is not required if you are running from Visual Studio IDE)**
```bash
dotnet restore
```

Ensure no restore errors occur.

---

### Step 3: Application Configuration

#### 3.1 Gemini API Key
 you can create your Gemini key from below url (login in with google credentials)

 https://aistudio.google.com/app/api-keys

Update your Gemini API key in `appsettings.json`:

```json
"Gemini": {
  "ApiKey": "<YOUR_GEMINI_API_KEY>"
}
```

⚠️ Do not commit real API keys to source control.

---

#### 3.2 Tesseract OCR Configuration

Install **Tesseract OCR** on your machine and ensure the `tessdata` directory exists.

The link for Tesseract OCR is below, please open this link and keep in the given path.

Example path:
```
C:\tessdata
```
👉 Download from:
https://github.com/tesseract-ocr/tessdata/raw/main/eng.traineddata

👉 Place it in:
C:\tessdata\eng.traineddata



Update `appsettings.json`:

```json
"Tesseract": {
  "TessDataPath": "C:\\tessdata",
  "Language": "eng"
}
```

---

### Step 4: Run the Backend

**Using Visual Studio(Recommended)**
- Set API project as Startup Project
- Press **F5** or click **Run**

**Using Command Line**
```bash 
dotnet run
```

---

## Swagger Usage

Swagger (OpenAPI) is enabled for API exploration and testing.

### Access Swagger UI
When the backend starts successfully, you should see:
- Application running in `Development` environment
- HTTP and HTTPS endpoints listening
- Message indicating the application has started
After starting the backend, navigate to:

```
https://localhost:<port>/swagger
```

Example:
```
https://localhost:7169/swagger

```

Swagger allows you to:
- View all available endpoints
- Inspect request and response schemas
- Execute API calls interactively

Swagger is typically enabled in **Development** environments.

---



## Backend Architecture Overview

The backend follows **Clean Architecture** with **DDD**.

### Project Structure

```
TeachersTimeTableManagementSystem
│
├── TeachersTimeTable.Api
│   └── API layer (Controllers, Middleware, Swagger)
│
├── TeachersTimeTable.Application
│   └── Application layer (Use Cases, Services, DTOs)
│
├── TeachersTimeTable.Domain
│   └── Domain layer (Entities, Value Objects, Aggregates)
│
└── TeachersTimeTable.Infrastructure
    └── Infrastructure layer (OCR, AI, Persistence, External Services)
```

### Dependency Flow

```
API → Application → Domain
Infrastructure → Application → Domain
```

- Domain has no dependencies
- Application depends only on Domain
- Infrastructure implements Application abstractions
- API orchestrates requests

---

## Notes

- Backend APIs are consumed by the React UI in the `ui` folder
- Backend should be running before starting the UI
- But in this project we are not calling api from UI, we have hardcoded value for display purpose.
- Secrets should be managed securely

---

## Troubleshooting

- **NuGet restore issues**:
```bash
dotnet nuget locals all --clear
```

- **Tesseract errors**:
  - Verify tessdata path
  - Ensure language files exist

- **Gemini errors**:
  - Validate API key
  - Check quotas and limits

---

## Next Steps ( This is a POC so we have used inmemory to store the image we are not using Blob Storage ,DB or Log but in real time we use below ). The below section we will implement later. 

- Authentication & Authorization (eg. IDE Microsoft Entra or any other IDE)
- BlobStorage (To store image or we can use AWS S3)
- Database configuration (for storing the timetbale information SQL server or any other DB)
- Observability (logging, metrics) (May be DataDog or Appinsight)
- CI/CD pipeline integration (Azure Devops, )
