using AutoMapper;
using FluentResults;
using MediatR;
using ToDoApp.BLL.DTO.Task;
using ToDoApp.DAL.Repositories.Interfaces.Base;

namespace ToDoApp.BLL.MediatR.Task.Update;

internal class UpdateTaskHandler : IRequestHandler<UpdateTaskCommand, Result<TaskDto>>
{
    private readonly IRepositoryWrapper _repositoryWrapper;
    private readonly IMapper _mapper;

    public UpdateTaskHandler(IRepositoryWrapper repositoryWrapper, IMapper mapper)
    {
        _repositoryWrapper = repositoryWrapper;
        _mapper = mapper;
    }
    
    public async Task<Result<TaskDto>> Handle(UpdateTaskCommand request, CancellationToken cancellationToken)
    {
        var existedTask =
            await _repositoryWrapper.TaskRepository.GetFirstOrDefaultAsync(t => t.Id == request.UpdetedTask.Id);
        
        if (existedTask is null)
        {
            string errorMessage = $"ToDoTask with id: {request.UpdetedTask.Id} was not found";
            return Result.Fail(errorMessage);
        }

        try
        {
            var taskToChange = _mapper.Map<DAL.Entities.ToDoTask>(request.UpdetedTask);
            _repositoryWrapper.TaskRepository.Update(taskToChange);
            await _repositoryWrapper.SaveChangesAsync();
            
            var updatedTaskDto = _mapper.Map<TaskDto>(taskToChange);
            return Result.Ok(updatedTaskDto);
        }
        catch (Exception ex)
        {
            return Result.Fail(ex.Message);
        }
    }
}