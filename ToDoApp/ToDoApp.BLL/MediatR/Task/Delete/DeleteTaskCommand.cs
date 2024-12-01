using FluentResults;
using MediatR;
using ToDoApp.BLL.DTO.Task;

namespace ToDoApp.BLL.MediatR.Task.Delete;

public record DeleteTaskCommand(int TaskId) : IRequest<Result<int>>;