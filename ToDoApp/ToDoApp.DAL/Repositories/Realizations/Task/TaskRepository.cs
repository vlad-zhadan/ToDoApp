using Microsoft.EntityFrameworkCore;
using ToDoApp.DAL.Persistence;
using ToDoApp.DAL.Repositories.Interfaces.Task;
using ToDoApp.DAL.Repositories.Realizations.Base;

namespace ToDoApp.DAL.Repositories.Realizations.Task;

public class TaskRepository : BaseRepository<Entities.ToDoTask>, ITaskRepository
{
    public TaskRepository(ToDoAppDbContext dbContext) : base(dbContext)
    {
    }
}