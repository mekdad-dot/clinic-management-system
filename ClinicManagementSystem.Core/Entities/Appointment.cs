using System.ComponentModel.DataAnnotations;

namespace ClinicManagementSystem.Core.Entities
{
    public class Appointment
    {
        [Required]
        [Key]
        public Guid AppointmentId { get; set; }
        [Required]
        public DateTimeOffset StartDate { get; set; }
        [Required]
        public DateTimeOffset EndDate { get; set; }

        [Required]
        public int Duration { get; set; }
        [Required]
        public bool IsCancelled { get; set; } = false;

        [Required]
        public string Description { get; set; } = null!;
       
        public Guid DoctorId { get; set; }
        public Doctor Doctor { get; set; } = null!;
        
        public Guid PatientId { get; set; }
        public Patient Patient { get; set; } = null!;
    }
}
