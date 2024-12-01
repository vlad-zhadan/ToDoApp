using FluentResults;
using MediatR;
using ToDoApp.BLL.DTO.Task;

namespace ToDoApp.BLL.MediatR.Task.Update;

public record UpdateTaskCommand(TaskUpdateDto UpdetedTask) : IRequest<Result<TaskDto>>;