using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared.Infrastructure.Observability
{
    public class ObservabilityOptions
    {
        public bool UseOpenTelemetry { get; set; } = false;
        public bool UseSerilog { get; set; } = false;
    }

}
