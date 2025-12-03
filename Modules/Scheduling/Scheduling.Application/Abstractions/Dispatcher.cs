using Microsoft.Extensions.DependencyInjection;
using Scheduling.Application.Abstractions.Handlers;

namespace Scheduling.Application.Abstractions
{
    public class Dispatcher : IDispatcher
    {
        private readonly IServiceProvider _serviceProvider;

        public Dispatcher(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }

        public async Task<TResult> SendAsync<TCommand, TResult>(TCommand command, CancellationToken ct = default)
        {
            var handler = _serviceProvider.GetRequiredService<ICommandHandler<TCommand, TResult>>();
            return await handler.HandleAsync(command, ct);
        }

        public async Task SendAsync<TCommand>(TCommand command, CancellationToken ct = default)
        {
            var handler = _serviceProvider.GetRequiredService<ICommandHandler<TCommand>>();
            await handler.HandleAsync(command, ct);
        }

        public async Task<TResult> QueryAsync<TQuery, TResult>(TQuery query, CancellationToken ct = default)
        {
            var handler = _serviceProvider.GetRequiredService<IQueryHandler<TQuery, TResult>>();
            return await handler.HandleAsync(query, ct);
        }
    }
}
