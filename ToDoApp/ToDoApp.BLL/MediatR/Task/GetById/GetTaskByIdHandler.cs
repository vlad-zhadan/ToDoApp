using AutoMapper;
using FluentResults;
using MediatR;
using ToDoApp.BLL.DTO.Task;
using ToDoApp.BLL.MediatR.Task.GetAll;
using ToDoApp.DAL.Repositories.Interfaces.Base;

namespace ToDoApp.BLL.MediatR.Task.GetById;

internal class GetTaskByIdHandler : IRequestHandler<GetTaskByIdQuery, Result<TaskDto>>
{
    private readonly IRepositoryWrapper _repositoryWrapper;
    private readonly IMapper _mapper;

    public GetTaskByIdHandler(IRepositoryWrapper repositoryWrapper, IMapper mapper)
    {
        _repositoryWrapper = repositoryWrapper;
        _mapper = mapper;
    }
    
    public async Task<Result<TaskDto>> Handle(GetTaskByIdQuery request, CancellationToken cancellationToken)
    {
        var task = await _repositoryWrapper.TaskRepository.GetFirstOrDefaultAsync(predicate: t => t.TaskId == request.TaskId);

        if (task is null)
        {
            string errorMessage = $"Task with id: {request.TaskId} does not exist";
            return Result.Fail(errorMessage);
        }

        try
        {
            var taskDto = _mapper.Map<TaskDto>(task);
            return Result.Ok(taskDto);
        }
        catch (Exception ex)
        {
            return Result.Fail(ex.Message);
        }
    }
}