using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Scheduling.Application.Availability.Responses
{
    public record TimeSlot(
        DateTime Start, 
        DateTime End   
    );
}
