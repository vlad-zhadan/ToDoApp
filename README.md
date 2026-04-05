# ToDoApp Backend Readiness and Standardization

## 1. Required Environment Variables

Set these variables before running the API:

- DB_HOST
- DB_PORT
- DB_NAME
- DB_USER
- DB_PASSWORD
- DB_USE_WINDOWS_AUTH (optional, true/false)
- CORS_ORIGIN (optional)

PowerShell example (SQL Authentication):

$env:DB_HOST="localhost"
$env:DB_PORT="1433"
$env:DB_NAME="ToDoApp"
$env:DB_USER="sa"
$env:DB_PASSWORD="YourStrong!Passw0rd"
$env:DB_USE_WINDOWS_AUTH="false"
$env:CORS_ORIGIN="http://localhost:3000"

PowerShell example (LocalDB with Windows Authentication):

$env:DB_HOST="(localdb)\\MSSQLLocalDB"
$env:DB_PORT="1433"
$env:DB_NAME="ToDoApp"
$env:DB_USER=""
$env:DB_PASSWORD=""
$env:DB_USE_WINDOWS_AUTH="true"
$env:CORS_ORIGIN="http://localhost:3000"

Run application:

dotnet run --project ToDoApp/ToDoApp.WebApi/ToDoApp.WebApi.csproj

Script examples:

SQL Authentication:

.\scripts\set-todoapp-env.ps1 -DbPassword "YourStrong!Passw0rd"

LocalDB Windows Authentication:

.\scripts\set-todoapp-env.ps1 -UseWindowsAuth

## 2. One-Command Build and Test

From repository root:

- Build: dotnet build ToDoApp/ToDoApp.sln
- Test: dotnet test ToDoApp/ToDoApp.sln

## 3. Health Check Verification

Health endpoint:

- GET /api/health

With DB connected:

curl -i http://localhost:5201/api/health

Expected status: 200

With DB stopped:

curl -i http://localhost:5201/api/health

Expected status: 503

Add screenshots of both outputs to satisfy lab submission requirements.

## 4. JSON Log Example

Application writes structured JSON logs to STDOUT.

Example startup and runtime logs:

{"timestamp":"2026-04-05T12:00:00.0000000Z","level":"INFORMATION","message":"Database migrations applied successfully"}
{"timestamp":"2026-04-05T12:00:05.0000000Z","level":"WARNING","message":"Health check failed: Database connection failed"}
{"timestamp":"2026-04-05T12:00:10.0000000Z","level":"INFORMATION","message":"SIGTERM received. Starting graceful shutdown..."}

## 5. Graceful Shutdown Verification

1. Start app and find process id.
2. Send termination signal.
3. Verify graceful shutdown log is present.

PowerShell:

$process = Get-Process ToDoApp.WebApi -ErrorAction SilentlyContinue
if (-not $process) { $process = Get-Process dotnet | Select-Object -First 1 }
Stop-Process -Id $process.Id

If you run from a shell supporting kill:

kill <pid>

Expected log message:

SIGTERM received. Starting graceful shutdown...

Add screenshot with this log line for submission.
