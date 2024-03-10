using System.ComponentModel.DataAnnotations;

namespace ClinicManagementSystem.Core.DTOs.DoctorDtos
{
    public class DoctorUpdateRequest
    {
        [Required]
        public string PhoneNumber { get; set; } = null!;
        [Required]
        public string Specialization { get; set; } = null!;
    }
}
