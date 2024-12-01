using FluentResults;
using MediatR;
using ToDoApp.BLL.DTO.Task;

namespace ToDoApp.BLL.MediatR.Task.UpdateStatus;

public record UpdateTaskStatusCommand(TaskUpdateStatusDto TaskUpdatedStatus) : IRequest<Result<int>>;