using ClinicManagementSystem.Core.DTOs.AdminDtos;

namespace ClinicManagementSystem.Core.Interfaces
{
    public interface IAdminService
    {
        ValueTask<AdminUpdateResponse> Update(Guid adminId, AdminUpdateRequest adminUpdateRequest);
        ValueTask<AdminReadResponse> GetByIdAsync(Guid adminId);
    }
}
