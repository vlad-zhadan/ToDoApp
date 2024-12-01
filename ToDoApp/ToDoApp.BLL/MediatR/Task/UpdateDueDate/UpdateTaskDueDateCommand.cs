using FluentResults;
using MediatR;
using ToDoApp.BLL.DTO.Task;

namespace ToDoApp.BLL.MediatR.Task.UpdateDueDate;

public record UpdateTaskDueDateCommand(TaskUpdateDueDateDto TaskUpdatedDueDate) : IRequest<Result<int>> ;