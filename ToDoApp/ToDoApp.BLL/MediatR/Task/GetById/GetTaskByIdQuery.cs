using FluentResults;
using MediatR;
using ToDoApp.BLL.DTO.Task;

namespace ToDoApp.BLL.MediatR.Task.GetById;

public record GetTaskByIdQuery(int TaskId) : IRequest<Result<TaskDto>>;