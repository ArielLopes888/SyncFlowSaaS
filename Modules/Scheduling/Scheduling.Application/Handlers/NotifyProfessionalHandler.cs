using Scheduling.Domain.Events;
using Shared.Infrastructure.Events;
using Scheduling.Application.Services;

namespace Scheduling.Application.Handlers;

public class NotifyProfessionalHandler : IHandle<AppointmentCreated>
{
    private readonly INotificationService _notificationService;

    public NotifyProfessionalHandler(INotificationService notificationService)
    {
        _notificationService = notificationService;
    }

    public async Task Handle(AppointmentCreated domainEvent, CancellationToken cancellationToken = default)
    {
        var msg = $"New appointment created for client {domainEvent.ClientName}.";
        await _notificationService.NotifyProfessionalAsync(
            domainEvent.ProfessionalId,
            msg,
            cancellationToken
        );
    }
}

