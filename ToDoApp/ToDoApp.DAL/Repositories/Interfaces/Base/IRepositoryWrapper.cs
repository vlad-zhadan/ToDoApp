using ToDoApp.DAL.Repositories.Interfaces.Task;

namespace ToDoApp.DAL.Repositories.Interfaces.Base;

public interface IRepositoryWrapper
{
    ITaskRepository TaskRepository { get; }

    Task<bool> CanConnectAsync(CancellationToken cancellationToken = default);

    public int SaveChanges();

    public Task<int> SaveChangesAsync();
}