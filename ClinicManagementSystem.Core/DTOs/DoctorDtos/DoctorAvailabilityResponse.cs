namespace ClinicManagementSystem.Core.DTOs.DoctorDtos
{
    public class DoctorAvailabilityResponse
    {
        public Guid Id { get; set; }
        public string FirstName { get; init; } = null!;
        public string LastName { get; init; } = null!;
        public string UserName { get; init; } = null!;
        public string Email { get; init; } = null!;
        public string PhoneNumber { get; init; } = null!;
        public string Specialization { get; init; } = null!;
        public bool IsAvailable { get; set; }
    }
}
