using System.Threading;

namespace Scheduling.Application.Abstractions.Handlers
{
    /// <summary>
    /// Handler for commands that return a result.
    /// </summary>
    /// <typeparam name="TCommand">Command type</typeparam>
    /// <typeparam name="TResult">Return type</typeparam>
    public interface ICommandHandler<TCommand, TResult>
    {
        Task<TResult> HandleAsync(TCommand command, CancellationToken cancellationToken = default);
    }
}
