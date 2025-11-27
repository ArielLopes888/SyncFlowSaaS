using Shared.Core.Abstractions;
using Scheduling.Domain.Events;
using Shared.Infrastructure.Events;

namespace Scheduling.Infrastructure.Handlers
{
    public class NotifyCancelHandler : IHandle<AppointmentCanceled>
    {
        public async Task Handle(AppointmentCanceled notification, CancellationToken cancellationToken)
        {
            
            // Por enquanto, apenas mantém o fluxo assíncrono sem lógica.

            await Task.CompletedTask;
        }
    }
}
