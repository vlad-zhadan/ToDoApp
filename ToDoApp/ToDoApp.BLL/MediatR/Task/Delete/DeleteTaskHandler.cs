using FluentResults;
using MediatR;
using ToDoApp.DAL.Repositories.Interfaces.Base;

namespace ToDoApp.BLL.MediatR.Task.Delete;

internal class DeleteTaskHandler : IRequestHandler<DeleteTaskCommand, Result<int>>
{
    private readonly IRepositoryWrapper _repositoryWrapper;

    public DeleteTaskHandler(IRepositoryWrapper repositoryWrapper)
    {
        _repositoryWrapper = repositoryWrapper;
    }
    
    public async Task<Result<int>> Handle(DeleteTaskCommand request, CancellationToken cancellationToken)
    {
        var taskToDelete = await _repositoryWrapper.TaskRepository.GetFirstOrDefaultAsync(predicate: t => t.TaskId == request.TaskId);

        if (taskToDelete is null)
        {
            string errorMessage = $"Task with id: {request.TaskId} does not exist";
            return Result.Fail(errorMessage);
        }

        try
        {
            _repositoryWrapper.TaskRepository.Delete(taskToDelete);
            await _repositoryWrapper.SaveChangesAsync();
            
            return Result.Ok(taskToDelete.TaskId);
        }
        catch (Exception ex)
        {
            return Result.Fail(ex.Message);
        }
    }
}