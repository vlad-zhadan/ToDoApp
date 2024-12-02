using FluentResults;
using MediatR;
using ToDoApp.DAL.Repositories.Interfaces.Base;

namespace ToDoApp.BLL.MediatR.Task.UpdateStatus;


internal class UpdateTaskStatusHandler : IRequestHandler<UpdateTaskStatusCommand, Result<int>>
{
    private readonly IRepositoryWrapper _repositoryWrapper;

    public UpdateTaskStatusHandler(IRepositoryWrapper repositoryWrapper)
    {
        _repositoryWrapper = repositoryWrapper;
    }
    
    public async Task<Result<int>> Handle(UpdateTaskStatusCommand request, CancellationToken cancellationToken)
    {
        var existedTask =
            await _repositoryWrapper.TaskRepository.GetFirstOrDefaultAsync(t => t.Id == request.TaskUpdatedStatus.Id);
        
        if (existedTask is null)
        {
            string errorMessage = $"ToDoTask with id: {request.TaskUpdatedStatus.Id} was not found";
            return Result.Fail(errorMessage);
        }

        try
        {
            if (existedTask.Status == request.TaskUpdatedStatus.Status)
            {
                return Result.Ok(request.TaskUpdatedStatus.Id);
            }
            
            existedTask.Status = request.TaskUpdatedStatus.Status;
            _repositoryWrapper.TaskRepository.Update(existedTask);
            await _repositoryWrapper.SaveChangesAsync();
            
            return Result.Ok(request.TaskUpdatedStatus.Id);
        }
        catch (Exception ex)
        {
            return Result.Fail(ex.Message);
        }
    }
}