using Microsoft.EntityFrameworkCore;
using ToDoApp.DAL.Repositories.Interfaces.Task;
using ToDoApp.DAL.Repositories.Realizations.Base;

namespace ToDoApp.DAL.Repositories.Realizations.Task;

public class TaskRepository : BaseRepository<Entities.Task>, ITaskRepository
{
    public TaskRepository(DbContext dbContext) : base(dbContext)
    {
    }
}