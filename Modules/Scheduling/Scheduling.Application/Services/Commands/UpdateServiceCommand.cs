using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Scheduling.Application.Services.Commands
{
    public record UpdateServiceCommand(
        Guid ServiceId,
        string Name,
        decimal Price,
        int DurationMinutes
    );

}
