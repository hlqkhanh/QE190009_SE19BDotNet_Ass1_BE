# TaskTrack Backend — PRN232 Assignment 1

ASP.NET Core Web API (.NET 8) backend for the public Task and Team Management application.

## Projects

- `TaskTrack.API`: controllers, middleware, Swagger, and CORS.
- `TaskTrack.Service`: DTOs, validation, and business rules.
- `TaskTrack.Repo`: EF Core database-first models, DbContext, and repositories.

## Local Setup

Create a PostgreSQL database and run `database/TaskManagementDB_Postgres.sql` to create the schema and seed data.

Copy the sample environment variables and update them if necessary:

```powershell
Copy-Item .env.example .env
```

Restore, build, and run the API:

```powershell
dotnet restore QE190009_SE19BDotNet_Ass1_BE.sln
dotnet build QE190009_SE19BDotNet_Ass1_BE.sln
dotnet run --project TaskTrack.API --urls http://localhost:5000
```

Open Swagger at http://localhost:5000/swagger.

## Docker

Build the production image from the repository root:

```powershell
docker build -t tasktrack-api .
```

The container listens on port `10000`, matching Render's default web-service port. Run it locally with:

```powershell
docker run --rm -p 10000:10000 `
  -e "DATABASE_URL=Host=host.docker.internal;Port=5433;Database=task_management;Username=taskadmin;Password=taskpassword" `
  -e "ASPNETCORE_ENVIRONMENT=Production" `
  tasktrack-api
```

Open Swagger at http://localhost:10000/swagger.

## Environment Variables

- `DATABASE_URL`: PostgreSQL connection string or Render PostgreSQL URL.
- `ASPNETCORE_ENVIRONMENT`: use `Development` locally and `Production` on Render.
- `Cors__AllowedOrigins__0`: deployed frontend origin, such as `https://your-app.vercel.app`.

