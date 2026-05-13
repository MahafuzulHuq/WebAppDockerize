# WebAPIPrime - Project Information

## Overview
WebAPIPrime is an ASP.NET Web API project implemented in C# targeting .NET 10. It provides order/product/inventory management with MediatR-based handlers and an EF Core-backed `AppDbContext`.

## Key Details
- Project name: `WebAPIPrime`
- Language: C# (version 14)
- Target framework: .NET 10
- IDE: Microsoft Visual Studio Community 2026 (18.4.3)
- Workspace root: `F:\VS2026Projects\WebAPIPrime` (local workspace)
- Git: repository at `https://github.com/MahafuzulHuq/WebAPIPrime`, active branch: `master`

## Repository structure (high-level)
- `Controllers/` - API controllers (e.g. `OrderController.cs`, `ProductController.cs`)
- `Services/` - Business/service layer and MediatR handlers (e.g. `ProductService.cs`, `InventoryService.cs`, `Services/Orders/`)
- `Data/` - EF Core `AppDbContext` and data models
- `Models/` - Domain models (e.g. `Product`, `Order`, `Inventory`, `EmailJob`)
- `Modelsdto/` - DTOs
- `Middleware/` - custom middleware (e.g. `ApiKeyAuth`)

## Important open files (developer session)
- `Services/Orders/Queries/GetOrderByQuery.cs`
- `Services/Orders/Commands/CreateOrderCommand.cs`
- `Services/Orders/Commands/CreateOrderResult.cs`
- `Services/Orders/Handlers/GetOrderQueryHandler.cs`
- `Services/Orders/Handlers/CreateOrderCommandHandler.cs` (active)
- `Controllers/OrderController.cs`
- `Services/Service/OrderService.cs`
- `Controllers/ProductController.cs`
- `Services/ProductService.cs`
- `Services/InventoryService.cs`

## Observations & notes
- MediatR is used for request/response flows in `ProductService` and orders handling.
- Inventory is modeled as a transaction log (`Inventory` entries store balance and deltas).
- Several service handlers create inventory entries and should use transactions to keep `Product` and `Inventory` updates atomic.
- The project uses EF Core async APIs and should `await` tasks like `AnyAsync`, `FirstOrDefaultAsync`, and `SaveChangesAsync`.

## Design patterns used

- Mediator (via `MediatR`) — decouples request senders from handlers (commands/queries).
- CQRS (Command / Query separation) — commands and queries are represented by separate MediatR requests.
- Dependency Injection — services, DbContext and handlers are registered and injected by the DI container.
- Repository / Unit of Work (EF Core `AppDbContext`) — DbContext acts as the unit-of-work and data access.
- DTO (Data Transfer Object) pattern — `ProductDto` and other DTOs are used for API contracts.
- Mapping (AutoMapper) — maps DTOs to domain models and back.
- Transaction pattern — explicit database transactions are used when updating `Product` and `Inventory` together.
- Hosted Service / Background Worker — background email processing implemented as a hosted service.
- Health check pattern — application exposes health endpoints and can plug into `IHealthCheck` implementations.

## Build & Run (local)
1. Open the solution in Visual Studio 2026.
2. Restore NuGet packages (Visual Studio usually restores automatically).
3. Build: `dotnet build` (from solution directory)
4. Run: `dotnet run --project <project.csproj>` or press F5 in Visual Studio.

## Common commands (PowerShell)
- Build: `dotnet build` 
- Run tests (if present): `dotnet test`
- Run app: `dotnet run --project WebAPIPrime.csproj`

## Database migrations and seeding

Use EF Core tools to add and apply migrations and to seed the database.

1. Install the tools (if not already installed):

   dotnet tool install --global dotnet-ef

2. Add a migration from the solution root (adjust project path if needed):

   dotnet ef migrations add InitialCreate --project WebAPIPrime.csproj --startup-project WebAPIPrime.csproj

3. Apply migrations to the configured database:

   dotnet ef database update --project WebAPIPrime.csproj --startup-project WebAPIPrime.csproj

4. Seed data: add a seeding routine in `AppDbContext` or in Program/Main. Example approach:

   - In `Program.cs`, after building the app, create a scope and call a seed helper:

     using var scope = app.Services.CreateScope();
     var db = scope.ServiceProvider.GetRequiredService<AppDbContext>();
     await DbSeed.InitializeAsync(db);

   - Implement `DbSeed.InitializeAsync(AppDbContext)` to add initial `Products`, `Orders`, and `Inventory` entries only when tables are empty and call `SaveChanges()`.

Note: For development, use a local SQL Server / SQLite connection string configured in `appsettings.Development.json`.

## Health checks

This project includes a simple health endpoint that verifies the application can connect to the database and returns counts for `Orders` and `Inventory`.

Endpoint: GET `/health`

Response example (200):

{
  "status": "Healthy",
  "orders": 12,
  "inventory": 42
}

If the database is unreachable the endpoint returns 503 Service Unavailable with an error message.

For production-grade checks, consider using `Microsoft.Extensions.Diagnostics.HealthChecks` and registering health checks in `Program.cs`.

## Integration tests (basic)

A sample integration test project `tests/WebAPIPrime.IntegrationTests` is included to demonstrate testing the health endpoint and basic orders/inventory flows.

Running tests:

1. From the solution root run:

   dotnet test

2. The sample tests use `WebApplicationFactory<TEntryPoint>` to host the app in-memory. Configure an in-memory or test database connection in `appsettings.Test.json` or override the `DbContext` in the test factory.

Notes and recommendations:

- Use an in-memory SQLite provider or a test container (Docker) for a transient database during tests.
- Ensure the tests seed necessary data before executing assertions.
- Mark long-running integration tests separately so they can be excluded from quick unit-test runs.

Generated for developer session in Visual Studio 2026.

## Contribution notes
- When adding or updating handlers that modify both `Products` and `Inventory`, wrap database operations in a transaction (`_context.Database.BeginTransactionAsync(...)`) to ensure consistency and enable rollback on failure.
- Always `await` EF Core async methods.
- Use `AsNoTracking()` for read-only queries where appropriate.

## Contact / Remotes
- Remote origin: `https://github.com/MahafuzulHuq/WebAPIPrime`
- Active branch: `master`

## Next steps (suggested)
- Add a top-level `README.md` with instructions to seed the database and run migrations.
- Add health checks and basic integration tests for orders and inventory flows.

Generated for developer session in Visual Studio 2026.
