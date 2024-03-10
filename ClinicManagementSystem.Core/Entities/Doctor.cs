using ClinicManagementSystem.Core.Entities.Identities;
using System.ComponentModel.DataAnnotations;

namespace ClinicManagementSystem.Core.Entities
{
    public class Doctor : User
    {
        [Required]
        public string Specialization { get; set; } = null!;

        public ICollection<Appointment> Appointments { get; set; } = new List<Appointment>();
    }
}
