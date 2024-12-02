using Microsoft.EntityFrameworkCore;
using ToDoApp.DAL.Entities;

namespace ToDoApp.DAL.Persistence;

public class ToDoAppDbContext : DbContext
{
    public ToDoAppDbContext(DbContextOptions<ToDoAppDbContext> options)
        : base(options)
    {
    }

    //public ToDoAppDbContext()
    //{
    //}
    //protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    //{
    //    optionsBuilder.UseSqlServer(@"Server=(localdb)\MSSQLLocalDB;Database=ToDoApp;Trusted_Connection=True;");

    //}

    public DbSet<ToDoTask> Tasks { get; set; }
}