using Microsoft.EntityFrameworkCore;
using Serilog;
using Serilog.Events;
using ToDoApp.BLL.Mapping.Task;
using ToDoApp.DAL.Persistence;
using ToDoApp.DAL.Repositories.Interfaces.Base;
using ToDoApp.DAL.Repositories.Realizations.Base;
using ToDoApp.WebApi.Extensions;
using ToDoApp.WebApi.Logging;

Log.Logger = new LoggerConfiguration()
    .MinimumLevel.Information()
    .MinimumLevel.Override("Microsoft", LogEventLevel.Warning)
    .WriteTo.Console(new SimpleJsonLogFormatter())
    .CreateLogger();

try
{
    var builder = WebApplication.CreateBuilder(args);
    builder.Host.UseSerilog();

    var dbHost = GetRequiredEnvironmentVariable("DB_HOST");
    var dbPort = GetRequiredEnvironmentVariable("DB_PORT");
    var dbName = GetRequiredEnvironmentVariable("DB_NAME");
    var dbUser = GetRequiredEnvironmentVariable("DB_USER");
    var dbPassword = GetRequiredEnvironmentVariable("DB_PASSWORD");
    var corsOrigin = Environment.GetEnvironmentVariable("CORS_ORIGIN");

    var connectionString =
        $"Server={dbHost},{dbPort};Database={dbName};User Id={dbUser};Password={dbPassword};Encrypt=False;TrustServerCertificate=True;";

    builder.Services.AddDbContext<ToDoAppDbContext>(options => options.UseSqlServer(connectionString));

    builder.Services.Configure<HostOptions>(options => { options.ShutdownTimeout = TimeSpan.FromSeconds(30); });

    builder.Services.AddCors(opt =>
    {
        opt.AddPolicy("CorsPolicy", policy =>
        {
            if (string.IsNullOrWhiteSpace(corsOrigin))
            {
                policy.AllowAnyHeader().AllowAnyMethod().AllowAnyOrigin();
                return;
            }

            policy.AllowAnyHeader().AllowAnyMethod().WithOrigins(corsOrigin);
        });
    });

    builder.Services.AddEndpointsApiExplorer();
    builder.Services.AddSwaggerGen();

    var currentAssemblies = AppDomain.CurrentDomain.GetAssemblies();
    builder.Services.AddMediatR(cfg => { cfg.RegisterServicesFromAssemblies(currentAssemblies); });

    builder.Services.AddAutoMapper(typeof(TaskProfile));
    builder.Services.AddTransient<IRepositoryWrapper, RepositoryWrapper>();
    builder.Services.AddControllers();

    var app = builder.Build();

    app.Lifetime.ApplicationStopping.Register(() =>
    {
        app.Logger.LogInformation("SIGTERM received. Starting graceful shutdown...");
    });

    await app.ApplyMigrations();

    if (app.Environment.IsDevelopment())
    {
        app.UseSwagger();
        app.UseSwaggerUI();
    }

    app.UseCors("CorsPolicy");
    app.UseHttpsRedirection();

    app.MapControllers();

    app.Run();
}
catch (Exception ex)
{
    Log.Fatal(ex, "Application terminated unexpectedly");
    throw;
}
finally
{
    Log.CloseAndFlush();
}

static string GetRequiredEnvironmentVariable(string variableName)
{
    var value = Environment.GetEnvironmentVariable(variableName);
    if (string.IsNullOrWhiteSpace(value))
    {
        throw new InvalidOperationException($"Missing required environment variable: {variableName}");
    }

    return value;
}
