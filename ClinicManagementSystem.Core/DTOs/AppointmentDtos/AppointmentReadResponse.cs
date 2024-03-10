using ClinicManagementSystem.Core.DTOs.DoctorDtos;
using ClinicManagementSystem.Core.DTOs.PatientDtos;

namespace ClinicManagementSystem.Core.DTOs.AppointmentDtos
{
    public class AppointmentReadResponse
    {
        public Guid AppointmentId { get; set; }
        public DateTimeOffset StartDate { get; set; }
        public DateTimeOffset EndDate { get; set; }
        public int Duration { get; set; }
        public string Description { get; set; } = null!;
        public bool IsCancelled { get; set; }
        public PatientReadResponse Patient { get; set; } = null!;
        public DoctorReadResponse Doctor { get; set; } = null!;
    }
}
