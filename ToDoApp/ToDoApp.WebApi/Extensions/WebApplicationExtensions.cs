using Microsoft.EntityFrameworkCore;
using ToDoApp.DAL.Persistence;

namespace ToDoApp.WebApi.Extensions;

public static class WebApplicationExtensions
{
    public static async Task ApplyMigrations(this WebApplication app)
    {
        var logger = app.Services.GetRequiredService<ILogger<Program>>();
        try
        {
            var toDoAppContext = app.Services.GetRequiredService<ToDoAppDbContext>();
            await toDoAppContext.Database.MigrateAsync();
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "An error occured during startup migration");
        }
    }
}