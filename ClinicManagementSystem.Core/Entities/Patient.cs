using ClinicManagementSystem.Core.Entities.Identities;
using System.ComponentModel.DataAnnotations;


namespace ClinicManagementSystem.Core.Entities
{
    public class Patient : User
    {
        [Required]
        public DateTimeOffset DOB { get; set; } = DateTimeOffset.MinValue;
        public ICollection<Appointment> Appointments { get; set; } = new List<Appointment>();
    }
}
