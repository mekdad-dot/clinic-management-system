
namespace ClinicManagementSystem.Core.DTOs.AppointmentDtos
{
    public class AppointmentCreateResponse
    {
        public Guid AppointmentId { get; set; }
        public DateTimeOffset StartDate { get; set; }
        public DateTimeOffset EndDate { get; set; }
        public int Duration { get; set; }
        public string Description { get; set; } = null!;
        public bool IsCancelled { get; set; }
        public Guid DoctorId { get; set; }
        public Guid PatientId { get; set; }
    }
}
