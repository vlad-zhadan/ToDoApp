using FluentResults;
using MediatR;
using ToDoApp.BLL.DTO.Task;

namespace ToDoApp.BLL.MediatR.Task.Create;

public record CreateTaskCommand(TaskCreateDto Task) : IRequest<Result<TaskDto>>;
