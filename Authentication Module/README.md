# Authentication Module API

A comprehensive ASP.NET Core Web API providing secure user authentication with JWT tokens, refresh tokens, two-factor authentication (2FA), and password reset functionality.

## 🚀 Features

- **User Registration & Login** - Secure user account creation and authentication
- **JWT Authentication** - Stateless authentication using JSON Web Tokens
- **Refresh Tokens** - Long-lived sessions with secure token refresh mechanism
- **Two-Factor Authentication (2FA)** - Enhanced security with TOTP-based 2FA
- **Password Reset** - Email-based password recovery system
- **BCrypt Password Hashing** - Industry-standard password security
- **Swagger Documentation** - Interactive API documentation with authentication support

## 🛠️ Technology Stack

- **.NET 10.0** - Latest .NET framework
- **ASP.NET Core Web API** - RESTful API framework
- **Entity Framework Core 10.0** - ORM for database access
- **SQL Server** - Relational database (LocalDB for development)
- **JWT Bearer Authentication** - Token-based security
- **BCrypt.Net** - Password hashing
- **MailKit** - Email sending functionality
- **OTP.NET** - TOTP implementation for 2FA
- **QRCoder** - QR code generation for 2FA setup
- **Swashbuckle** - Swagger/OpenAPI documentation

## 📋 Prerequisites

Before running this project, ensure you have:

- **.NET 10 SDK** or later installed
- **SQL Server** or SQL Server Express with LocalDB
- **Visual Studio 2022** or **VS Code** (optional but recommended)
- **SMTP Email Account** (for password reset emails)

## 🔧 Installation & Setup

### 1. Clone the Repository

```bash
git clone https://github.com/OsamaMohamedM/API-Tasks.git
cd "Authentication Module"
```

### 2. Configure Application Settings

Update `appsettings.json` with your configuration:

```json
{
  "ConnectionStrings": {
    "DefaultConnection": "Server=(localdb)\\mssqllocaldb;Database=AuthenticationModuleDb;Trusted_Connection=true;MultipleActiveResultSets=true"
  },
  "Jwt": {
    "SecretKey": "YourSuperSecretKeyThatIsAtLeast32CharactersLong!",
    "Issuer": "AuthenticationModule",
    "Audience": "AuthenticationModuleUsers",
    "ExpirationMinutes": 60
  },
  "Email": {
    "SmtpServer": "smtp.gmail.com",
    "SmtpPort": 587,
    "SenderEmail": "your-email@gmail.com",
    "SenderPassword": "your-app-password"
  }
}
```

⚠️ **Important**: For Gmail, you need to use an [App Password](https://support.google.com/accounts/answer/185833), not your regular password.

### 3. Install Entity Framework Tools (if not already installed)

```bash
dotnet tool install --global dotnet-ef
```

### 4. Create Database Migration

```bash
dotnet ef migrations add InitialCreate
```

### 5. Update Database

```bash
dotnet ef database update
```

### 6. Run the Application

```bash
dotnet run
```

The API will be available at:
- **HTTPS**: `https://localhost:5001`
- **HTTP**: `http://localhost:5000`
- **Swagger UI**: `https://localhost:5001/` (root path)

## 📚 API Endpoints

### Authentication Endpoints

#### Register New User
```http
POST /api/auth/register
Content-Type: application/json

{
  "userName": "johndoe",
  "email": "john@example.com",
  "password": "SecurePass123!"
}
```

#### Login
```http
POST /api/auth/login
Content-Type: application/json

{
  "username": "johndoe",
  "password": "SecurePass123!"
}
```

**Response:**
```json
{
  "token": "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9...",
  "refreshToken": "AbC123XyZ..."
}
```

#### Refresh Token
```http
POST /api/auth/refresh
Content-Type: application/json

{
  "refreshToken": "AbC123XyZ..."
}
```

#### Forgot Password
```http
POST /api/auth/forgot-password
Content-Type: application/json

{
  "email": "john@example.com"
}
```

#### Reset Password
```http
POST /api/auth/reset-password
Content-Type: application/json

{
  "token": "reset-token-from-email",
  "newPassword": "NewSecurePass123!"
}
```

### Two-Factor Authentication Endpoints

#### Setup 2FA (Requires Authentication)
```http
POST /api/twofactor/setup
Authorization: Bearer {your-jwt-token}
```

**Response:**
```json
{
  "qrCode": "base64-encoded-qr-image",
  "manualKey": "JBSWY3DPEHPK3PXP"
}
```

#### Verify and Enable 2FA (Requires Authentication)
```http
POST /api/twofactor/verify-and-enable
Authorization: Bearer {your-jwt-token}
Content-Type: application/json

{
  "code": "123456"
}
```

## 🏗️ Project Architecture

The project follows **Clean Architecture** principles with clear separation of concerns:

```
Authentication Module/
├── Controllers/              # API endpoints
│   ├── AuthController.cs
│   └── TwoFactorController.cs
├── Domain/                   # Core business logic
│   ├── Common/
│   │   └── Result.cs        # Result pattern for error handling
│   ├── DTO/                 # Data Transfer Objects
│   ├── Entities/            # Domain entities (User, RefreshToken)
│   └── Interfaces/          # Service and repository contracts
├── Infrastructure/          # External dependencies implementation
│   ├── Data/
│   │   └── ApplicationDbContext.cs
│   ├── Repositories/        # Data access layer
│   ├── AuthService.cs       # Authentication logic
│   ├── TokenService.cs      # JWT generation
│   ├── EmailService.cs      # Email sending
│   └── TwoFactorService.cs  # 2FA implementation
└── Program.cs               # Application startup & DI configuration
```

## 🔒 Security Features

1. **Password Hashing** - BCrypt with automatic salt generation
2. **JWT Tokens** - Short-lived access tokens (60 minutes by default)
3. **Refresh Tokens** - Secure, long-lived tokens (7 days) stored in database
4. **Token Revocation** - Refresh tokens are marked as used/revoked
5. **2FA Support** - TOTP-based two-factor authentication
6. **Email Verification** - Password reset via email tokens (1 hour expiry)
7. **SQL Injection Protection** - Entity Framework parameterized queries
8. **HTTPS Enforcement** - Secure communication in production

## 🧪 Testing with Swagger

1. Run the application
2. Navigate to `https://localhost:5001/`
3. Try the `/api/auth/register` endpoint to create a user
4. Use `/api/auth/login` to get your JWT token
5. Click the "Authorize" button (🔒) at the top right
6. Enter: `Bearer {your-token-here}`
7. Now you can test protected endpoints like `/api/twofactor/*`

## 📦 Database Schema

### Users Table
| Column | Type | Description |
|--------|------|-------------|
| Id | int | Primary key |
| Username | string(100) | Unique username |
| Email | string(200) | Unique email |
| PasswordHash | string | BCrypt hashed password |
| Role | string(50) | User role (default: "User") |
| TwoFactorEnabled | bool | 2FA status |
| TwoFactorSecret | string | TOTP secret key |
| PasswordResetToken | string | Password reset token |
| PasswordResetTokenExpires | DateTime | Token expiration |

### RefreshTokens Table
| Column | Type | Description |
|--------|------|-------------|
| Id | int | Primary key |
| UserId | int | Foreign key to Users |
| Token | string(500) | Refresh token value |
| CreatedAt | DateTime | Creation timestamp |
| ExpiresAt | DateTime | Expiration timestamp |
| IsUsed | bool | Token usage status |
| IsRevoked | bool | Token revocation status |

## 🤝 Contributing

Contributions are welcome! Please follow these steps:

1. Fork the repository
2. Create a feature branch (`git checkout -b feature/AmazingFeature`)
3. Commit your changes (`git commit -m 'Add some AmazingFeature'`)
4. Push to the branch (`git push origin feature/AmazingFeature`)
5. Open a Pull Request

## 👨‍💻 Author

**Osama Mohamed**
- GitHub: [@OsamaMohamedM](https://github.com/OsamaMohamedM)

## 🙏 Acknowledgments

- ASP.NET Core Team for the excellent framework
- Community contributors to all the NuGet packages used
- Clean Architecture principles by Robert C. Martin

---

**Note**: Remember to change all default secrets and passwords before deploying to production!
