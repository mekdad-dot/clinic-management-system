using ClinicManagementSystem.Core.DTOs;
using ClinicManagementSystem.Core.DTOs.PatientDtos;

namespace ClinicManagementSystem.Core.Interfaces
{
    public interface IPatientService
    {
        ValueTask<PatientUpdateResponse> UpdateAsync(Guid id, PatientUpdateRequest entity);
        ValueTask<PatientReadResponse> GetByIdAsync(Guid entityId);
        ValueTask<IEnumerable<PatientReadResponse>> GetAllAsync(PaginRequest paginRequest);
    }
}
