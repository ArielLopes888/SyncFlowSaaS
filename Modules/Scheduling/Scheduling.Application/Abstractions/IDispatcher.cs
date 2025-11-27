using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Scheduling.Application.Abstractions
{
    public interface IDispatcher
    {
        Task<TResult> SendAsync<TCommand, TResult>(TCommand command, CancellationToken ct = default);
        Task SendAsync<TCommand>(TCommand command, CancellationToken ct = default);

        Task<TResult> QueryAsync<TQuery, TResult>(TQuery query, CancellationToken ct = default);
    }
}
