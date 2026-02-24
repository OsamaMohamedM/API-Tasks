# Task 1 – Patients REST API

A **.NET 10** Web API built with **Entity Framework Core** and the **Repository + Unit of Work** pattern, exposing full CRUD operations for patients along with partial-update (PATCH) and paginated listing endpoints.

---

## Tech Stack

| Layer | Technology |
|---|---|
| Framework | ASP.NET Core (.NET 10) |
| ORM | Entity Framework Core |
| Database | SQL Server |
| Pattern | Repository + Unit of Work |
| Docs | Swagger / OpenAPI |

---

## Project Structure

```
Task1/
├── Controllers/
│   └── PatientsController.cs     # All patient API endpoints
├── Data/
│   ├── AppDbContext.cs            # EF Core DbContext with seed data
│   └── PatientData.cs
├── DTO/
│   ├── PatientDTO.cs              # Read / create DTO
│   └── UpdatePatientDTO.cs        # Partial-update (PATCH) DTO
├── Helpers/
│   ├── PaginationParams.cs        # Query parameters for paging
│   └── PagedResult.cs             # Generic paged response wrapper
├── Models/
│   ├── Patient.cs                 # Patient entity
│   └── Doctor.cs                  # Doctor entity
├── Repository/
│   ├── IRepository.cs             # Generic repository interface
│   ├── Repository.cs              # Generic repository implementation
│   ├── IUnitOfWork.cs             # Unit of Work interface
│   └── UnitOfWork.cs              # Unit of Work implementation
└── Program.cs
```

---

## Domain Models

### Patient
| Property | Type | Notes |
|---|---|---|
| `Id` | `int` | Primary key |
| `Name` | `string` | Max 50 chars, required |
| `Age` | `int` | Required |
| `PhoneNumber` | `string` | Max 50 chars, required |
| `CheckInDate` | `DateTime` | |
| `DoctorId` | `int?` | FK → Doctor |
| `isRegistered` | `bool` | |

### Doctor
| Property | Type |
|---|---|
| `Id` | `int` |
| `Name` | `string` |
| `PhoneNumber` | `string` |
| `Patients` | `List<Patient>` |

---

## API Endpoints

Base URL: `/api/patients`

### GET `/api/patients`
Returns a list of all patients.

**Response `200 OK`**
```json
[
  { "id": 1, "name": "John Doe", "age": 15, "phoneNumber": "123-456-7890" }
]
```

---

### GET `/api/patients/{id}`
Returns a single patient by ID.

**Response `200 OK`**
```json
{ "id": 1, "name": "John Doe", "age": 15, "phoneNumber": "123-456-7890" }
```

**Response `404 Not Found`**
```json
{ "message": "Invalid 99" }
```

---

### GET `/api/patients/paged`
Returns a paginated list of patients.

**Query Parameters**

| Parameter | Type | Default | Max | Description |
|---|---|---|---|---|
| `pageNumber` | `int` | `1` | – | Page to retrieve |
| `pageSize` | `int` | `10` | `50` | Number of items per page |

**Example:** `GET /api/patients/paged?pageNumber=1&pageSize=5`

**Response `200 OK`**
```json
{
  "data": [
    { "id": 1, "name": "John Doe", "age": 15, "phoneNumber": "123-456-7890" }
  ],
  "pageNumber": 1,
  "pageSize": 5,
  "totalCount": 20,
  "totalPages": 4,
  "hasPrevious": false,
  "hasNext": true
}
```

---

### POST `/api/patients`
Creates a new patient.

**Request Body**
```json
{ "name": "Jane Smith", "age": 30, "phoneNumber": "555-000-1111" }
```

**Response `200 OK`** – returns the created patient with its assigned `id`.

---

### PUT `/api/patients?id={id}`
Fully replaces an existing patient record.

**Request Body** – full `Patient` object including all fields.

**Response `200 OK`** – returns the updated patient.

---

### PATCH `/api/patients/{id}`
Partially updates a patient. Only the fields provided in the request body are changed; omitted fields remain unchanged.

**Request Body** – all fields are optional:
```json
{
  "name": "Updated Name",
  "age": 35,
  "phoneNumber": "999-888-7777"
}
```

**Response `200 OK`**
```json
{ "id": 1, "name": "Updated Name", "age": 35, "phoneNumber": "999-888-7777" }
```

**Response `404 Not Found`**
```json
{ "message": "Invalid 99" }
```

---

### DELETE `/api/patients?id={id}`
Deletes a patient by ID.

**Response `200 OK`**

**Response `404 Not Found`**
```json
{ "message": "Invalid 99" }
```

---

## Pagination

### `PaginationParams`
Passed as query parameters on the paged endpoint.

```csharp
public class PaginationParams
{
    public const int MaxPageSize = 50;   // hard cap
    public int PageNumber { get; set; } = 1;
    public int PageSize   { get; set; }  // capped at MaxPageSize
}
```

### `PagedResult<T>`
Returned by the paged endpoint.

| Property | Type | Description |
|---|---|---|
| `Data` | `IEnumerable<T>` | Items for the current page |
| `PageNumber` | `int` | Current page number |
| `PageSize` | `int` | Items per page |
| `TotalCount` | `int` | Total number of records |
| `TotalPages` | `int` | Computed: `⌈TotalCount / PageSize⌉` |
| `HasPrevious` | `bool` | `true` when `PageNumber > 1` |
| `HasNext` | `bool` | `true` when `PageNumber < TotalPages` |

---

## Repository & Unit of Work Pattern

### `IRepository<T>`
| Method | Description |
|---|---|
| `GetByIdAsync(id)` | Fetch single entity by PK |
| `GetAllAsync()` | Fetch all entities |
| `FindAsync(predicate)` | Fetch entities matching a condition |
| `GetPagedAsync(params)` | Fetch a paginated slice |
| `AddAsync(entity)` | Add a new entity |
| `Update(entity)` | Mark entity as modified |
| `Delete(entity)` | Remove entity |

### `IUnitOfWork`
Exposes `IRepository<Patient> Patients`, `IRepository<Doctor> Doctors`, and `SaveChangesAsync()` to commit all changes in a single transaction.

---

## Getting Started

### Prerequisites
- [.NET 10 SDK](https://dotnet.microsoft.com/download)
- SQL Server (local or remote)

### Configuration
Set your connection string in `appsettings.json`:
```json
{
  "ConnectionStrings": {
    "LocalConnection": "Server=.;Database=Task1DB;Trusted_Connection=True;"
  }
}
```

### Run Migrations & Start
```bash
cd Task1
dotnet ef database update
dotnet run
```

Swagger UI is available at `https://localhost:{port}/swagger`.

---

## Seed Data

The database is pre-seeded with:

**Doctors**
| Id | Name | PhoneNumber |
|---|---|---|
| 1 | Dr. Alice Carter | 100-200-3000 |
| 2 | Dr. Bob Harris | 100-200-4000 |

**Patients** – sample patients linked to the doctors above.
