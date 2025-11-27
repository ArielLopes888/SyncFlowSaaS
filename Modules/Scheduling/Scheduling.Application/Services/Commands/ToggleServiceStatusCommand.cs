namespace Scheduling.Application.Services.Commands
{
    public record ToggleServiceStatusCommand(Guid ServiceId, bool Active);
    public record ToggleServiceRequest(bool Active);
}
