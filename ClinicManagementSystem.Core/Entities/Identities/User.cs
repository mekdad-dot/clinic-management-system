using System.ComponentModel.DataAnnotations;
using ClinicManagementSystem.Core.Entities.Base;

namespace ClinicManagementSystem.Core.Entities.Identities
{
    public abstract class User : BaseEntity
    {
        [Required]
        public string FirstName { get; set; } = null!;
        [Required]
        public string LastName { get; set; } = null!;
        [Required]
        public string UserName { get; set; } = null!;
        [Required]
        public string Email { get; set; } = null!;
        [Required]
        public string PhoneNumber { get; set; } = null!;
    }

    public enum UserType
    {
        Admin,
        Doctor,
        Patient
    }
}
