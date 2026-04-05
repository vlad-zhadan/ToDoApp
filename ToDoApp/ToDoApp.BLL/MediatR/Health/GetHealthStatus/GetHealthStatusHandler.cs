using FluentResults;
using MediatR;
using ToDoApp.DAL.Repositories.Interfaces.Base;

namespace ToDoApp.BLL.MediatR.Health.GetHealthStatus;

internal class GetHealthStatusHandler : IRequestHandler<GetHealthStatusQuery, Result<bool>>
{
    private readonly IRepositoryWrapper _repositoryWrapper;

    public GetHealthStatusHandler(IRepositoryWrapper repositoryWrapper)
    {
        _repositoryWrapper = repositoryWrapper;
    }

    public async Task<Result<bool>> Handle(GetHealthStatusQuery request, CancellationToken cancellationToken)
    {
        try
        {
            var canConnect = await _repositoryWrapper.CanConnectAsync(cancellationToken);
            return canConnect ? Result.Ok(true) : Result.Fail("Database connection failed");
        }
        catch (Exception ex)
        {
            return Result.Fail($"Database connection failed: {ex.Message}");
        }
    }
}
