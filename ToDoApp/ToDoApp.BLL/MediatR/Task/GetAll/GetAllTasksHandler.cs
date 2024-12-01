using AutoMapper;
using FluentResults;
using MediatR;
using ToDoApp.BLL.DTO.Task;
using ToDoApp.DAL.Repositories.Interfaces.Base;

namespace ToDoApp.BLL.MediatR.Task.GetAll;

internal class GetAllTasksHandler : IRequestHandler<GetAllTasksQuery, Result<IEnumerable<TaskDto>>>
{
    private readonly IRepositoryWrapper _repositoryWrapper;
    private readonly IMapper _mapper;

    public GetAllTasksHandler(IRepositoryWrapper repositoryWrapper, IMapper mapper)
    {
        _repositoryWrapper = repositoryWrapper;
        _mapper = mapper;
    }
    
    public async Task<Result<IEnumerable<TaskDto>>> Handle(GetAllTasksQuery request, CancellationToken cancellationToken)
    {
        try
        {
            var tasks = await _repositoryWrapper.TaskRepository.GetAllAsync();
            var tasksDto = _mapper.Map<IEnumerable<TaskDto>>(tasks);
            return Result.Ok(tasksDto);
        }
        catch (Exception ex)
        {
            return Result.Fail(ex.Message);
        }
    }
}