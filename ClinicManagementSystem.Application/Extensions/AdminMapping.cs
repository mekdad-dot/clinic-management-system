using ClinicManagementSystem.Core.DTOs.AdminDtos;
using ClinicManagementSystem.Core.Entities;


namespace ClinicManagementSystem.Application.Extensions
{
    public static class AdminMapping
    {
        public static AdminReadResponse AdminToReadResponse(this Admin admin) =>
            new AdminReadResponse
            {
                Id = admin.Id,
                Email = admin.Email,
                FirstName = admin.FirstName,
                LastName = admin.LastName,
                UserName = admin.UserName,
                PhoneNumber = admin.PhoneNumber
            };

        public static void UpdateRequestToAdmin(this AdminUpdateRequest updateRequest, Admin admin)
        {
            if (!string.IsNullOrEmpty(updateRequest.PhoneNumber))
            {
                admin.PhoneNumber = updateRequest.PhoneNumber;
            }
        }

        public static AdminUpdateResponse AdminToUpdateResponse(this Admin admin) =>
            new AdminUpdateResponse
            {
                Id = admin.Id,
                Email = admin.Email,
                FirstName = admin.FirstName,
                LastName = admin.LastName,
                UserName = admin.UserName,
                PhoneNumber = admin.PhoneNumber
            };

    }
}
