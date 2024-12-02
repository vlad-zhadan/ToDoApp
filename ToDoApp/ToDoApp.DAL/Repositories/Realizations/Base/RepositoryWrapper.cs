using Microsoft.EntityFrameworkCore;
using ToDoApp.DAL.Persistence;
using ToDoApp.DAL.Repositories.Interfaces.Base;
using ToDoApp.DAL.Repositories.Interfaces.Task;
using ToDoApp.DAL.Repositories.Realizations.Task;

namespace ToDoApp.DAL.Repositories.Realizations.Base;

public class RepositoryWrapper : IRepositoryWrapper
{
    private readonly ToDoAppDbContext _context;

    public RepositoryWrapper(ToDoAppDbContext context)
    {
        _context = context;
    }

    private ITaskRepository _taskRepository;

    public ITaskRepository TaskRepository
    {
        get
        {
            if(_taskRepository == null)
            {
                _taskRepository = new TaskRepository(_context);
            }
            return _taskRepository;
        }
    }
    
    public int SaveChanges()
    {
        return _context.SaveChanges();
    }

    public async Task<int> SaveChangesAsync()
    {
        return await _context.SaveChangesAsync();
    }
}