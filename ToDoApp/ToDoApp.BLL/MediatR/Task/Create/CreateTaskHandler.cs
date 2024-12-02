using AutoMapper;
using FluentResults;
using MediatR;
using ToDoApp.BLL.DTO.Task;
using ToDoApp.DAL.Repositories.Interfaces.Base;

namespace ToDoApp.BLL.MediatR.Task.Create;

internal class CreateTaskHandler : IRequestHandler<CreateTaskCommand, Result<TaskDto>>
{
    private readonly IRepositoryWrapper _repositoryWrapper;
    private readonly IMapper _mapper;

    public CreateTaskHandler(IRepositoryWrapper repositoryWrapper, IMapper mapper)
    {
        _repositoryWrapper = repositoryWrapper;
        _mapper = mapper;
    }

    public async Task<Result<TaskDto>> Handle(CreateTaskCommand request, CancellationToken cancellationToken)
    {
        try
        {
            var task = _mapper.Map<DAL.Entities.ToDoTask>(request.Task);
            var createdTask = await _repositoryWrapper.TaskRepository.CreateAsync(task);
            await _repositoryWrapper.SaveChangesAsync();

            var createdTaskDto = _mapper.Map<TaskDto>(createdTask);
            return Result.Ok(createdTaskDto);
        }
        catch(Exception ex)
        {
            return Result.Fail(ex.Message);
        }
    }
}