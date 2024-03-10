using ClinicManagementSystem.Core.DTOs;
using ClinicManagementSystem.Core.DTOs.DoctorDtos;

namespace ClinicManagementSystem.Core.Interfaces
{
    public interface IDoctorService
    {
        ValueTask<IEnumerable<DoctorAvailabilityResponse>> GetDoctorsByAvailability(bool isPatient, PaginRequest paginRequest);
        ValueTask<DoctorReadResponse> GetByIdAsync(Guid doctorId);
        ValueTask<IEnumerable<DoctorReadResponse>> GetDoctorsOnWorkingHours(DateTimeOffset dateTime, int workingHours, PaginRequest paginRequest);
        ValueTask<IEnumerable<DoctorReadResponse>> GetMostMetDoctors(DateTimeOffset dateTime, PaginRequest paginRequest);
        ValueTask<DoctorUpdateResponse> Update(Guid doctorId, DoctorUpdateRequest doctorUpdateRequest);
        ValueTask<IEnumerable<DoctorReadResponse>> GetAllAsync(PaginRequest paginRequest);
    }
}
