# WebAppDockerize - Project Information
  
Overview
- ASP.NET Core web application targeting .NET 9 (net9.0).
- Razor/Views-based web UI with static assets in `wwwroot`.
- Project is configured for containerization with Visual Studio container tools and Docker Compose.

Key information
- Project path (workspace): F:\VS2026Projects\DockerizeProjects\WebAppDockerize
- Repository remote: https://github.com/MahafuzulHuq/WebAppDockerize (branch: main)
- Project file: `WebAppDockerize/WebAppDockerize.csproj`
- Notable NuGet packages (from csproj): EF Core, EF Core SQL Server, Swashbuckle (Swagger), Visual Studio Azure Containers tools.

Getting started (local)
1. Prerequisites
   - .NET 9 SDK
   - Docker Desktop (or another Docker runtime)
   - Microsoft Visual Studio 2026 (recommended for integrated container support)

2. Run without containers
   - From solution root:
     - dotnet restore
     - dotnet build
     - dotnet run --project WebAppDockerize\WebAppDockerize.csproj
   - App will listen on the configured ASP.NET Core ports (see `appsettings.*` or launch profile).

3. Run with Docker (CLI)
   - Build an image (adjust Dockerfile path if different):
     - docker build -t webappdockerize:dev -f WebAppDockerize/Dockerfile .
   - Run the container:
     - docker run -p 8080:80 --env ASPNETCORE_ENVIRONMENT=Development webappdockerize:dev
   - If a `docker-compose.yml` exists:
     - docker compose up --build -d

4. Run from Visual Studio
   - Open the solution in Visual Studio 2026.
   - Choose the __Docker__ or __Docker Compose__ run profile and press F5.
   - Container output and logs are visible in the __Output__ window and the __Containers__ tool windows.

Swagger and API docs
- Swashbuckle packages are included; if Swagger is enabled in startup code, visit `/swagger` (or configured path) to view API docs.

Database and EF Core
- EF Core packages are referenced (including SQL Server provider).
- Configure your connection string in `appsettings.Development.json` or environment variables.
- Typical EF Core workflow:
  - dotnet tool install --global dotnet-ef
  - dotnet ef migrations add InitialCreate --project WebAppDockerize\WebAppDockerize.csproj
  - dotnet ef database update --project WebAppDockerize\WebAppDockerize.csproj

Project structure (high level)
- `WebAppDockerize/`
  - `Controllers` / `Views` / `Pages` (UI)
  - `wwwroot` (static files: css, js, images)
  - `appsettings*.json` (configuration)
  - `Dockerfile`, `docker-compose.yml` (if present) — containerization artifacts

Development tips
- Use the __Output__ window for build and container logs when debugging from Visual Studio.
- When working with EF Core migrations, run commands from the project directory or specify the `--project` and `--startup-project` flags.
- Expose and map ports explicitly during docker run to avoid conflicts (e.g., `-p 8080:80`).

Contributing
- Create feature branches off `main`.
- Follow repository coding conventions and add/update migrations when schema changes are made.
- Open pull requests that describe the change and how to run/test it.

Contact / repo
- Remote origin: https://github.com/MahafuzulHuq/WebAppDockerize
- Workspace root: F:\VS2026Projects\DockerizeProjects\WebAppDockerize

Notes
- This README provides a concise starting point. Check the project for a `Dockerfile`, `docker-compose.yml`, and `appsettings.*.json` files for project-specific configuration and ports.
- 
Generated for developer session in Visual Studio 2026.
