
namespace ClinicManagementSystem.Core.DTOs.PatientDtos
{
    public class PatientUpdateRequest
    { 
        public string PhoneNumber { get; set; } = null!;
        public DateTimeOffset DOB { get; set; } 
    }
}
