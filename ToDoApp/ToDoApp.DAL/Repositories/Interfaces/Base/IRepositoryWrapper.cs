using ToDoApp.DAL.Repositories.Interfaces.Task;

namespace ToDoApp.DAL.Repositories.Interfaces.Base;

public interface IRepositoryWrapper
{
    ITaskRepository TaskRepository { get; }
    
    public int SaveChanges();
    
    public Task<int> SaveChangesAsync();
}