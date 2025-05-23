# C# Web API Project – User and Post Management

## Overview

This is a Web API project built with **.NET 9** and **Entity Framework Core** using an **Oracle database**. The project manages two main entities: `User` and `Post`, and supports full CRUD operations, validation, authentication, and audit logging.

## Technologies Used

* **.NET 9**
* **Entity Framework Core**
* **Oracle Database (XEPDB1)**
* **Docker Compose**
* **JWT Authentication**
* **FluentValidation**
* **AutoMapper**
* **Clean Architecture**

## Project Structure

```
WebApiProject
│
├── backend
│   ├── backend.sln
│   ├── Program.cs
│   ├── appsettings.json
│   ├── appsettings.Development.json
│   └── src
│       ├── Application
│       │   ├── Configuration
│       │   │   └── DependencyInjection
│       │   │   └── Interceptors
│       │   ├── Core
│       │   │   └── Shared (Audited Entities)
│       │   ├── Data
│       │   │   └── Seed
│       │   │   └── Repository
│       │   ├── Helpers
│       │   ├── Models
│       │   │   ├── Common
│       │   │   └── Pagination
│       │   │   └── Response
│       │   └── Service
│       │       ├── Auth
│       │       ├── Posts (DTOs, Mapper, Validators, Interface and Service)
│       │       └── Users (DTOs, Mapper, Validators, Interface and Service)
│       ├── Domain
│       │   └── Entities
│       ├── Infrastructure
│       │   ├── Middleware (Exception)
│       │   └── Persistence (AppDbContext)
│       └── WebApi
│           └── Controller
│
├── docker-compose.yml
└── .gitignore
```

## Features

* **User Management**: Register, update, list, soft-delete users.
* **Post Management**: Create, update, soft-delete, and list posts linked to users.
* **JWT Authentication**: Secure API access via login tokens.
* **Validation**: Input validation with FluentValidation.
* **Audit Fields**: Tracks who created, updated, or deleted records and when.
* **Pagination**: Custom pagination ABP style.
* **Global Exception Middleware** and **EF Interceptors**.
* **Service Installer Pattern** for clean dependency injection.

## How to Start the Application

### Prerequisites

Make sure you have the following installed:

- [.NET 9 SDK](https://dotnet.microsoft.com/en-us/download/dotnet/9.0)
- [Docker Desktop](https://www.docker.com/products/docker-desktop)
- (Optional) [Visual Studio 2022+ Preview](https://visualstudio.microsoft.com/vs/preview/) with .NET 9 support

### Running Entirely in Docker

1. Open a terminal in the root directory (`WebApiProject`)
2. Run:
   ```bash
   docker-compose up --build
   ```
3. Wait for logs to show:
   ```
   Now listening on: http://[::]:80
   ```
4. Open Swagger UI at:
   ```
   http://localhost:5000/swagger
   ```

> When debugging locally, the app connects to Docker's Oracle via `localhost:1521`.

### Optional: Running the App Locally (Backend runs on your machine, Oracle runs in Docker)

1. **Start Oracle DB via Docker:**
   ```bash
   docker-compose up -d oracle-db
   ```

2. **Wait ~30 seconds** until Oracle is fully initialized (container must stay green in Docker Desktop).

3. **Run the backend API locally:**
   - Open the solution (`backend.sln`) in Visual Studio
   - Set `backend` as the startup project
   - Press **F5**.
   - Launch settings start: [http://localhost:5233/swagger](http://localhost:5233/swagger)

   > If port `5233` is unavailable, Visual Studio may use `https://localhost:7272/swagger`.

4. **Run locally through command line**:
   ```bash
   dotnet run --project backend --launch-profile http
   ```
   - Open Swagger UI at:
   Visit: [http://localhost:5233/swagger](http://localhost:5233/swagger)
   or [https://localhost:7272/swagger](https://localhost:7272/swagger)

## JWT Authentication Workflow

1. A seed user is created automatically:

   * **Username:** `admin`
   * **Password:** `admin123!`

2. Authenticate with:
   ```http
   POST /api/auth/login
   ```

   ```json
   {
     "username": "admin",
     "password": "admin123!"
   }
   ```

3. Copy the returned token and click **Authorize** in  and enter your token there:
   ```
   YOUR_TOKEN_HERE
   ```

4. Access protected endpoints like:
   - `GET /api/users`
   - `POST /api/posts`

## Database Setup

* Oracle XE is run via Docker:
  ```
  docker-compose up -d oracle-db
  ```

* Connection string used in **Docker**:
  ```
  User Id=appuser;Password=AppUserPass123;Data Source=oracle-db:1521/XEPDB1
  ```

* Connection string used **locally** (in `appsettings.Development.json`):
  ```
  User Id=appuser;Password=AppUserPass123;Data Source=localhost:1521/XEPDB1
  ```

You can inspect the Oracle DB using DBeaver or Oracle SQL Developer:
- Host: `localhost`
- Port: `1521`
- Service: `XEPDB1`
- User: `appuser`
- Password: `AppUserPass123`

### Implementation
Now that you are done with the set up here is more information about implementation:

## Endpoints

* `POST /api/auth/login`
* `POST /api/users`
* `GET /api/users`
* `PUT /api/users/{id}`
* `DELETE /api/users/{id}` (Soft delete)
* `POST /api/posts`
* `GET /api/posts`
* `PUT /api/posts/{id}`
* `DELETE /api/posts/{id}` (Soft delete)

## Soft Delete Behavior

- Records are **not permanently deleted**
- `IsDeleted = 1`, `DeletionTime`, and `DeleterUserId` are used for audit
- Deleted entities are excluded from normal queries

## Interceptors and Audit Logging

The project includes two EF Core interceptors to enhance data layer logging and auditing:

### DbOperationTimingInterceptor

Logs performance metrics and summarizes entity operations performed during `SaveChangesAsync`. It detects added, updated, and soft-deleted records and logs the operation time.

- Logs `[DB] SaveChanges started` and completed timestamps.
- Summarizes entity operations grouped by type.

### UpdateAuditableEntitiesInterceptor

Automatically fills audit fields by implementing `IAuditableEntity`, `ISoftDelete`, and `IFullAudited` interfaces:

- **On create:** fills `CreationTime`, `CreatorUserId`
- **On update:** fills `LastModificationTime`, `LastModifierUserId`
- **On soft delete:** sets `IsDeleted`, `DeletionTime`, `DeleterUserId`

These interceptors enhance traceability and are plugged into `DbContext` via DI.

### How to Observe Audit Fields

#### 1. Swagger (Runtime)

- `GET /api/users` and `GET /api/posts`:
  - Show `CreationTime`, `CreatorUserId`, etc.
  - Fields like `LastModificationTime`, `LastModifierUserId` appear after updates.
  - Soft-deleted records show too: `DeletionTime` and `DeleterUserId`.

## JWT Authentication Implementation

This project uses a custom implementation of JWT-based authentication without relying on ASP.NET Core Identity or scaffolding.

### Key Highlights

- **JWT Tokens** are generated manually using `JwtSecurityTokenHandler` and standard claims.
- **Password hashing** is handled using SHA256 via a static `PasswordHasher` class (no ASP.NET Identity).
- **No refresh tokens** are used in this project.
- Tokens expire after **1 hour** and contain the following claims:
  - `sub`: the user’s unique ID
  - `unique_name`: the username
  - `role`: a fixed `"User"` role

### Token Generation Flow

1. On login (`POST /api/auth/login`), the system:
   - Verifies the user credentials (username + SHA256 hashed password).
   - If valid, generates a JWT using the `JwtService` class.

2. The `JwtService` signs the token using a secret from `appsettings.json`:

```json
"JWT": {
  "Key": "your-secret-key",
  "Issuer": "your-app",
  "Audience": "your-app-users"
}
```

3. The token is then returned in the response body and can be used by the client for authenticated requests.

### Example Login Response

```json
{
  "success": true,
  "result": "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9..." // JWT token
}
```
## Global Exception Middleware

This project uses a custom `ExceptionMiddleware` to handle unhandled exceptions globally and return consistent, structured error responses.
- Middleware catches exceptions via try/catch in the `Invoke` method.

### Response Structure

All error responses follow the custom format defined in `Response<T>`:

```json
{
  "succeeded": false,
  "message": "An unexpected error occurred.",
  "statusCode": 500,
  "errorCode": "SERVER_ERROR",
  "errors": {}
}
```

### Error Code Mapping

Error codes like `FORBIDDEN`, `NOT_FOUND`, `BAD_REQUEST`, and `UNAUTHORIZED` are centralized in `ErrorCodeStatic` and returned from services using the `Response<T>` builder methods such as `.InternalServerError()` or `.BadRequest()`.

## Pagination Strategy

This project uses an **ABP-style pagination model**, cleanly abstracted with generic interfaces:

- `IListResult<T>` – wraps a list of items.
- `IHasTotalCount` – exposes `TotalCount`.
- `IPagedResult<T>` – combines both.

DTOs:
- `PagedResultDto<T>`: returned in paginated responses. Contains `Items` and `TotalCount`.
- `PagedResultRequestDto`: input with `SkipCount` and `MaxResultCount`.

The logic is implemented generically to return efficient, paged datasets from the database, especially visible in endpoints like `GET /api/users` or `GET /api/posts`.