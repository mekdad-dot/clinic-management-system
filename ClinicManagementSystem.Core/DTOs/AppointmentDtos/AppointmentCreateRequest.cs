using System.ComponentModel.DataAnnotations;


namespace ClinicManagementSystem.Core.DTOs.AppointmentDtos
{
    public class AppointmentCreateRequest
    {

        [Required] 
        public DateTimeOffset StartDate { get; set; }
        [Required]
        public DateTimeOffset EndDate { get; set; }
        [Required]
        public string Description { get; set; } = null!;
        [Required]
        public Guid PatientId { get; set; }
        [Required]
        public Guid DoctorId { get; set; }
    }
}
