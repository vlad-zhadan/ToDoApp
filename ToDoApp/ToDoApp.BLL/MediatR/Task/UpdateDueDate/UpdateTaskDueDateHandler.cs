using AutoMapper;
using FluentResults;
using MediatR;
using ToDoApp.BLL.DTO.Task;
using ToDoApp.BLL.MediatR.Task.Update;
using ToDoApp.DAL.Repositories.Interfaces.Base;

namespace ToDoApp.BLL.MediatR.Task.UpdateDueDate;

internal class UpdateTaskDueDateHandler : IRequestHandler<UpdateTaskDueDateCommand, Result<int>>
{
    private readonly IRepositoryWrapper _repositoryWrapper;

    public UpdateTaskDueDateHandler(IRepositoryWrapper repositoryWrapper)
    {
        _repositoryWrapper = repositoryWrapper;
    }
    
    public async Task<Result<int>> Handle(UpdateTaskDueDateCommand request, CancellationToken cancellationToken)
    {
        var existedTask =
            await _repositoryWrapper.TaskRepository.GetFirstOrDefaultAsync(t => t.Id == request.TaskUpdatedDueDate.Id);
        
        if (existedTask is null)
        {
            string errorMessage = $"ToDoTask with id: {request.TaskUpdatedDueDate.Id} was not found";
            return Result.Fail(errorMessage);
        }

        try
        {
            if (existedTask.DueDate == request.TaskUpdatedDueDate.DueDate)
            {
                return Result.Ok(request.TaskUpdatedDueDate.Id);
            }
            
            existedTask.DueDate = request.TaskUpdatedDueDate.DueDate;
            _repositoryWrapper.TaskRepository.Update(existedTask);
            await _repositoryWrapper.SaveChangesAsync();
            
            return Result.Ok(request.TaskUpdatedDueDate.Id);
        }
        catch (Exception ex)
        {
            return Result.Fail(ex.Message);
        }
    }
}