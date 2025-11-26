namespace SyncFlowSaaS.Api.Controllers
{
    public class CreateAppointmentRequest
    {
        public Guid TenantId { get; set; }
        public Guid ProfessionalId { get; set; }
        public Guid ServiceId { get; set; }
        public DateTime StartAt { get; set; }
        public DateTime EndAt { get; set; }
        public string ClientName { get; set; } = null!;
        public string? ClientPhone { get; set; }
    }


}
