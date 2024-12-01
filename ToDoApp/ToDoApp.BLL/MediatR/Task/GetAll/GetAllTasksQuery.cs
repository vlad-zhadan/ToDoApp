using FluentResults;
using MediatR;
using ToDoApp.BLL.DTO.Task;

namespace ToDoApp.BLL.MediatR.Task.GetAll;

public record GetAllTasksQuery : IRequest<Result<IEnumerable<TaskDto>>>;