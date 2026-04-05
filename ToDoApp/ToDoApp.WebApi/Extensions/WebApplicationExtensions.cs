using Microsoft.EntityFrameworkCore;
using ToDoApp.DAL.Persistence;

namespace ToDoApp.WebApi.Extensions;

public static class WebApplicationExtensions
{
    public static async Task ApplyMigrations(this WebApplication app)
    {
        await using var scope = app.Services.CreateAsyncScope();
        var logger = scope.ServiceProvider.GetRequiredService<ILoggerFactory>().CreateLogger("StartupMigration");
        try
        {
            var toDoAppContext = scope.ServiceProvider.GetRequiredService<ToDoAppDbContext>();
            await toDoAppContext.Database.MigrateAsync();
            logger.LogInformation("Database migrations applied successfully");
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "Failed to apply startup migrations");
            throw;
        }
    }
}