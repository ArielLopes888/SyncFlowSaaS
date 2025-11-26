using Scheduling.Application.Services;

namespace Scheduling.Infrastructure.Services
{
    public class NotificationService : INotificationService
    {
        public Task NotifyProfessionalAsync(Guid professionalId, string message, CancellationToken ct = default)
        {
            Console.WriteLine($"[NOTIFY | Pro] => {professionalId}: {message}");
            return Task.CompletedTask;
        }

        public Task NotifyClientAsync(Guid clientId, string message, CancellationToken ct = default)
        {
            Console.WriteLine($"[NOTIFY | Client] => {clientId}: {message}");
            return Task.CompletedTask;
        }
    }
}
