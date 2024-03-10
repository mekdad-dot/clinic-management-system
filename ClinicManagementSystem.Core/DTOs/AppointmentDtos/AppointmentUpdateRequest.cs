using System.ComponentModel.DataAnnotations;

namespace ClinicManagementSystem.Core.DTOs.AppointmentDtos
{
    public class AppointmentUpdateRequest
    {
        [Required]
        public string Description { get; set; } = null!;
    }
}
