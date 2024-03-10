using System.ComponentModel.DataAnnotations;


namespace ClinicManagementSystem.Core.DTOs.AdminDtos
{
    public class AdminUpdateRequest
    {
        [Required]
        public string PhoneNumber { get; set; } = null!;
    }
}
