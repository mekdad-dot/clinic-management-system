namespace ClinicManagementSystem.Core.DTOs.PatientDtos
{
    public class PatientReadResponse
    {
        public Guid Id { get; set; }
        public string FirstName { get; init; } = null!;
        public string LastName { get; init; } = null!;
        public string UserName { get; init; } = null!;
        public string Email { get; init; } = null!;
        public string PhoneNumber { get; init; } = null!;
        public DateTimeOffset DOB { get; set; }
    }
}
