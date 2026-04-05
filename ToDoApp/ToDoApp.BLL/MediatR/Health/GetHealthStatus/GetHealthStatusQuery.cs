using FluentResults;
using MediatR;

namespace ToDoApp.BLL.MediatR.Health.GetHealthStatus;

public record GetHealthStatusQuery : IRequest<Result<bool>>;
