using ClinicManagementSystem.Application;
using ClinicManagementSystem.Core.Interfaces;
using ClinicManagementSystem.Core.Interfaces.Repos;
using ClinicManagementSystem.Infrastructure.Persistence;
using ClinicManagementSystem.Infrastructure.Persistence.Data;
using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using static ClinicManagementSystem.Core.AppContants;

namespace ClinicManagementSystem.Infrastructure
{
    public static class InfrastructureRegistration
    {
        public static void AddInfrastructureRegistration(this WebApplicationBuilder builder) 
        {
            builder.Services.AddScoped<IAdminRepo, AdminRepo>();
            builder.Services.AddScoped<IPatientRepo, PatientRepo>();
            builder.Services.AddScoped<IDoctorRepo,DoctorRepo>();
            builder.Services.AddScoped<IAppointmentRepo,AppointmentRepo>();  
        }

        public static void AddAppServices(this WebApplicationBuilder builder)
        {
            builder.Services.AddScoped<IAdminService, AdminService>();
            builder.Services.AddScoped<IAppointmentService,AppointmentService>();
            builder.Services.AddScoped<IPatientService, PatientService>();
            builder.Services.AddScoped<IDoctorService, DoctorSerivce>();
        }

        public static void AddCustomDbContext(this WebApplicationBuilder builder)
        {
            builder.Services.AddDbContext<ApplicationDbContext>(options =>
            {
                options.UseSqlServer(builder.Configuration[Config.DBConnectionString]);
            });
        }
    }
}
