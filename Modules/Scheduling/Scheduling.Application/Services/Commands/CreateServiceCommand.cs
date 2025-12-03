namespace Scheduling.Application.Services.Commands
{
    public record CreateServiceCommand(
        string Name,
        decimal Price, 
        int DurationMinutes
     );
}
