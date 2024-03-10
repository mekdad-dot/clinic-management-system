using ClinicManagementSystem.Core.Entities;
using ClinicManagementSystem.Core.Interfaces.Repos;
using ClinicManagementSystem.Infrastructure.Persistence.Data;
using Microsoft.EntityFrameworkCore;

namespace ClinicManagementSystem.Infrastructure.Persistence
{
    public class AdminRepo : Repo<Admin> , IAdminRepo
    {
        public AdminRepo(ApplicationDbContext context) : base (context) { }
    }
}
