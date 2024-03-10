using ClinicManagementSystem.Core.DTOs.AdminDtos;
using ClinicManagementSystem.Core.Interfaces;
using ClinicManagementSystem.Core.Interfaces.Repos;
using ClinicManagementSystem.Application.CustomExceptions;
using ClinicManagementSystem.Application.Extensions;
using Microsoft.Extensions.Logging;
using System.Net;

namespace ClinicManagementSystem.Application
{
    public class AdminService : IAdminService
    {
        private readonly IAdminRepo _adminRepo;
        private readonly ILogger<AdminService> _logger;

        public AdminService(IAdminRepo adminRepo,ILogger<AdminService> logger)
        {
            _adminRepo = adminRepo;
            _logger = logger;
        }

        public async ValueTask<AdminReadResponse> GetByIdAsync(Guid adminId)
        {
            var admin = await this._adminRepo.GetByIdAsync(adminId);

            if (admin == null)
            {
                _logger.LogInformation($"User with Id {adminId} not found");
                throw new ServiceException("User not found,Wrong Email", HttpStatusCode.NotFound);
            }

            return admin.AdminToReadResponse();
        }

        public async ValueTask<AdminUpdateResponse> Update(Guid adminId, AdminUpdateRequest adminUpdateRequest)
        {
            var admin = await this._adminRepo.GetByIdAsync(adminId);

            if (admin == null)
            {
                _logger.LogInformation($"User with Id {adminId} not found");
                throw new ServiceException("User not found,Wrong Email", HttpStatusCode.NotFound);
            }

            this._adminRepo.Update(admin);

            await this._adminRepo.SaveChangesAsync();

            return admin.AdminToUpdateResponse();
        }
    }
}
