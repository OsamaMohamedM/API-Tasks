# API-Tasks

> **Note:** This project is created for learning purposes. These are practical tasks I made to enhance my skills in building ASP.NET Core Web APIs.

## 📖 Overview

This repository contains a collection of ASP.NET Core Web API projects demonstrating various backend development concepts and best practices. Each task focuses on different aspects of API development, from basic CRUD operations to advanced authentication and querying techniques.

## 🛠️ Technologies Used

- **.NET 10.0** - Latest version of the .NET framework
- **ASP.NET Core Web API** - For building RESTful APIs
- **Entity Framework Core** - ORM for database operations
- **SQL Server** - Database management system
- **AutoMapper** - Object-to-object mapping
- **JWT (JSON Web Tokens)** - For authentication and authorization
- **Swagger/OpenAPI** - API documentation and testing
- **Newtonsoft.Json** - JSON serialization

## 📂 Project Structure

The repository is organized into multiple tasks, each focusing on specific learning objectives:

### 1. Task1 - Repository Pattern & Unit of Work
A foundational project implementing CRUD operation with the Repository Pattern and Unit of Work pattern for data access.

**Features:**
- Generic Repository implementation
- Unit of Work pattern for transaction management
- Entity Framework Core integration
- Patient and Doctor models
- RESTful API endpoints

**Key Concepts:**
- Data Access Layer abstraction
- SOLID principles
- Dependency Injection
- DTOs (Data Transfer Objects)

### 2. AdvancedQuerying - Dynamic Filtering & Data Shaping
Advanced API project demonstrating complex querying capabilities and dynamic data manipulation.

**Features:**
- Dynamic data shaping
- Advanced filtering and searching
- Sorting and pagination
- Specification pattern
- AutoMapper for object mapping
- Database seeding

**Key Concepts:**
- LINQ queries
- Expression trees
- Dynamic query building
- API best practices for filtering and pagination
- Data transfer optimization

### 3. Authentication Module - Complete Auth System
A comprehensive authentication and authorization module with modern security features.

**Features:**
- User registration and login
- JWT token-based authentication
- Refresh token mechanism
- Two-Factor Authentication (2FA)
- Password reset functionality
- Email verification
- Secure password hashing

**Key Concepts:**
- JWT authentication
- Token refresh flow
- Security best practices
- Email services integration
- Clean Architecture principles
- Domain-Driven Design patterns

## 🚀 Getting Started

### Prerequisites

- [.NET 10.0 SDK](https://dotnet.microsoft.com/download/dotnet/10.0) or later
- [SQL Server](https://www.microsoft.com/en-us/sql-server/sql-server-downloads) (LocalDB or Express)
- [Visual Studio 2022](https://visualstudio.microsoft.com/) or [Visual Studio Code](https://code.visualstudio.com/)
- [SQL Server Management Studio (SSMS)](https://docs.microsoft.com/en-us/sql/ssms/download-sql-server-management-studio-ssms) (optional)

### Installation & Setup

1. **Clone the repository**
   ```bash
   git clone https://github.com/yourusername/API-Tasks.git
   cd API-Tasks
   ```

2. **Update Connection Strings**

   Update the `appsettings.json` file in each project with your SQL Server connection string:
   ```json
   {
     "ConnectionStrings": {
       "DefaultConnection": "Server=(localdb)\\mssqllocaldb;Database=YourDbName;Trusted_Connection=true;"
     }
   }
   ```

3. **Apply Database Migrations**

   For each project, navigate to the project directory and run:
   ```bash
   dotnet ef database update
   ```

4. **Run the Application**
   ```bash
   dotnet run
   ```

5. **Access Swagger UI**

   Navigate to `https://localhost:<port>/swagger` to view and test the API endpoints.

## 📚 Learning Objectives

Through these tasks, I've learned and practiced:

- ✅ Building RESTful APIs with ASP.NET Core
- ✅ Implementing design patterns (Repository, Unit of Work, Specification)
- ✅ Database operations with Entity Framework Core
- ✅ Clean Architecture and separation of concerns
- ✅ JWT authentication and authorization
- ✅ Security best practices for web APIs
- ✅ Advanced querying techniques (filtering, sorting, pagination)
- ✅ Data transfer optimization and shaping
- ✅ API documentation with Swagger/OpenAPI
- ✅ Dependency Injection and IoC containers
- ✅ Email services integration
- ✅ Two-Factor Authentication implementation
- ✅ Token refresh mechanisms

## 🎯 API Features by Project

### Task1 API Endpoints
- `GET /api/patients` - Get all patients
- `GET /api/patients/{id}` - Get patient by ID
- `POST /api/patients` - Create new patient
- `PUT /api/patients/{id}` - Update patient
- `DELETE /api/patients/{id}` - Delete patient

### AdvancedQuerying API Endpoints
- `GET /api/resource?filter=...&sort=...&fields=...&page=...&pageSize=...`
- Dynamic filtering with query parameters
- Field selection for response shaping
- Sorting and pagination support

### Authentication Module API Endpoints
- `POST /api/auth/register` - User registration
- `POST /api/auth/login` - User login
- `POST /api/auth/refresh` - Refresh access token
- `POST /api/auth/forgot-password` - Request password reset
- `POST /api/auth/reset-password` - Reset password
- `POST /api/auth/enable-2fa` - Enable Two-Factor Authentication
- `POST /api/auth/verify-2fa` - Verify 2FA code

## 🔧 Configuration

Each project requires specific configuration in `appsettings.json`:

### Authentication Module Configuration
```json
{
  "Jwt": {
    "SecretKey": "your-secret-key-here",
    "Issuer": "your-issuer",
    "Audience": "your-audience",
    "ExpiryInMinutes": 60
  },
  "Email": {
    "SmtpServer": "smtp.gmail.com",
    "Port": 587,
    "SenderEmail": "your-email@gmail.com",
    "SenderPassword": "your-app-password"
  }
}
```

## 🧪 Testing

Use the provided `.http` files in each project to test endpoints, or use the Swagger UI interface available at `/swagger`.

## 📝 Notes

- This is a learning project and may not include all production-ready features
- Some implementations are simplified for educational purposes
- Feel free to explore, modify, and extend the code
- Each task builds upon concepts from previous tasks

## 🤝 Contributing

This is a personal learning project, but suggestions and feedback are always welcome! Feel free to:
- Open an issue for bugs or suggestions
- Fork the repository and experiment with the code
- Share your learning experience

## 📄 License

This project is open source and available for educational purposes.

**Happy Learning! 🚀**
