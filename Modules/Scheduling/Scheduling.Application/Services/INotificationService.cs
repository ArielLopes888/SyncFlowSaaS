using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Scheduling.Application.Services
{
    public interface INotificationService
    {
        Task NotifyProfessionalAsync(Guid professionalId, string message, CancellationToken ct = default);
        Task NotifyClientAsync(Guid clientId, string message, CancellationToken ct = default);
    }
    
}
