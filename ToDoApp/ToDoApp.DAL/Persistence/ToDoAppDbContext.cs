using Microsoft.EntityFrameworkCore;

namespace ToDoApp.DAL.Persistence;

public class ToDoAppDbContext : DbContext
{
    public ToDoAppDbContext(DbContextOptions<ToDoAppDbContext> options) 
        : base(options)
    {
    }
    
    public DbSet<Task> Tasks { get; set; }
}