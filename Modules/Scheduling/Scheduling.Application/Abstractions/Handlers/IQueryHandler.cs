using System.Threading;

namespace Scheduling.Application.Abstractions.Handlers
{
    /// <summary>
    /// Handler for queries that return a result.
    /// </summary>
    /// <typeparam name="TQuery">Query type</typeparam>
    /// <typeparam name="TResult">Result type</typeparam>
    public interface IQueryHandler<TQuery, TResult>
    {
        Task<TResult> HandleAsync(TQuery query, CancellationToken cancellationToken = default);
    }
}
