# Advanced Querying API - Task Management System

This project demonstrates advanced API querying techniques including filtering, sorting, pagination, data shaping, and the specification pattern.

## Features Implemented

### 1. **Data Protection (Entities, DTOs & AutoMapper)**
- **TaskEntity**: Database entity with all fields including `SecretManagerNotes`
- **TaskDto**: Data Transfer Object that excludes `SecretManagerNotes`
- **AutoMapper**: Automatically maps TaskEntity to TaskDto, ensuring sensitive data never reaches the client

### 2. **Advanced Filtering**
Query parameters supported:
- `searchTerm`: Partial text match in Title or Description
- `status`: Exact status match (e.g., "InProgress", "Completed", "Pending")
- `minHours`: Filter tasks with EstimatedHours >= value
- `maxHours`: Filter tasks with EstimatedHours <= value

### 3. **Specification Pattern**
All filtering and querying logic is encapsulated in:
- `ISpecification<T>`: Interface defining specification contract
- `BaseSpecification<T>`: Abstract base class for specifications
- `AdvancedTaskSpecification`: Concrete specification for task filtering
- `SpecificationEvaluator<T>`: Evaluates specifications and builds queries

**No Where/OrderBy clauses in Controller or Service layer!**

### 4. **Dynamic Sorting**
Sort parameter options:
- `sortBy=date`: Sort by DueDate (newest first)
- `sortBy=hours`: Sort by EstimatedHours (ascending)
- Default: Sort by Id (ascending)

### 5. **Pagination & Metadata**
- `pageNumber`: Page number (default: 1)
- `pageSize`: Items per page (default: 10, max: 50)

Response wrapped in `PagedResult<T>`:
```json
{
  "data": [...],
  "totalCount": 100,
  "totalPages": 10,
  "currentPage": 1
}
```

### 6. **Data Shaping**
- `fields`: Comma-separated field names (e.g., "id,title")
- Returns only requested fields to optimize bandwidth
- Uses ExpandoObject for dynamic JSON structure

## API Endpoints

### GET /api/tasks

#### Example Requests:

1. **Basic request with pagination:**
```
GET /api/tasks?pageNumber=1&pageSize=5
```

2. **Search with filtering:**
```
GET /api/tasks?searchTerm=API&status=InProgress&minHours=5&maxHours=15
```

3. **Sort by date:**
```
GET /api/tasks?sortBy=date&pageSize=10
```

4. **Sort by hours:**
```
GET /api/tasks?sortBy=hours&pageSize=20
```

5. **Data shaping (only id and title):**
```
GET /api/tasks?fields=id,title&pageSize=10
```

6. **Combined filters:**
```
GET /api/tasks?searchTerm=database&status=Pending&minHours=10&maxHours=25&sortBy=hours&pageNumber=1&pageSize=5&fields=id,title,status,estimatedHours
```

## Project Structure

```
AdvancedQuerying/
├── Controllers/
│   └── TasksController.cs          # Lean controller delegating to specifications
├── Data/
│   ├── AppDbContext.cs             # Entity Framework DbContext
│   └── DbSeeder.cs                 # Sample data seeder
├── Mappings/
│   └── MappingProfile.cs           # AutoMapper configuration
├── Models/
│   ├── DTOs/
│   │   ├── TaskDto.cs              # DTO without sensitive data
│   │   └── PagedResult.cs          # Pagination wrapper
│   └── Entities/
│       └── TaskEntity.cs           # Database entity
├── Services/
│   ├── IDataShaper.cs              # Data shaping interface
│   └── DataShaper.cs               # Dynamic field selection
└── Specifications/
    ├── ISpecification.cs           # Specification interface
    ├── BaseSpecification.cs        # Base specification class
    ├── AdvancedTaskSpecification.cs # Task filtering specification
    └── SpecificationEvaluator.cs   # Query builder from specification
```

## Technology Stack

- **.NET 10.0**
- **Entity Framework Core** (SQL Server)
- **AutoMapper** (Entity to DTO mapping)
- **Newtonsoft.Json** (JSON serialization for ExpandoObject)

## Setup Instructions

1. **Update connection string** in `appsettings.json`:
```json
{
  "ConnectionStrings": {
    "DefaultConnection": "Server=YOUR_SERVER;Database=AdvancedQueryingDb;Trusted_Connection=true;"
  }
}
```

2. **Run the application**:
The database will be created automatically with sample data.

3. **Test the endpoints** using your preferred tool (Swagger, Postman, curl)

## Key Design Principles

✅ **Separation of Concerns**: Business logic separated into specifications
✅ **Single Responsibility**: Each class has one clear purpose
✅ **Open/Closed Principle**: Easy to add new specifications without modifying existing code
✅ **DRY (Don't Repeat Yourself)**: Reusable specification base class
✅ **Security**: Sensitive data never exposed through DTOs
✅ **Performance**: Efficient querying with EF Core and pagination

## Sample Response

**Request:** `GET /api/tasks?searchTerm=API&pageSize=2&fields=id,title,status`

**Response:**
```json
{
  "data": [
    {
      "Id": 1,
      "Title": "Complete API Documentation",
      "Status": "InProgress"
    }
  ],
  "totalCount": 1,
  "totalPages": 1,
  "currentPage": 1
}
```

**Note:** The `SecretManagerNotes` field is NEVER included in any response!

## Testing Checklist

- [x] SecretManagerNotes never exposed in API response
- [x] Search term filters Title and Description
- [x] Status filter works with exact match
- [x] MinHours and MaxHours range filtering
- [x] Sort by date (newest first)
- [x] Sort by hours (ascending)
- [x] Default sort by Id
- [x] Pagination limits results
- [x] Max page size enforced (50)
- [x] Metadata includes TotalCount, TotalPages, CurrentPage
- [x] Data shaping returns only requested fields
- [x] No Where/OrderBy in Controller
- [x] All queries use Specification pattern
