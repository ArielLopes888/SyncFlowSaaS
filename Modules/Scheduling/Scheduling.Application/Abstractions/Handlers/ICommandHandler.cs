using System.Threading;

namespace Scheduling.Application.Abstractions.Handlers
{
    /// <summary>
    /// Handler for commands that don't return a result.
    /// </summary>
    /// <typeparam name="TCommand">Command type</typeparam>
    public interface ICommandHandler<TCommand>
    {
        Task HandleAsync(TCommand command, CancellationToken cancellationToken = default);
    }
}
