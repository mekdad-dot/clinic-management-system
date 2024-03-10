using ClinicManagementSystem.Core.Entities;
using ClinicManagementSystem.Core.Interfaces.Repos;
using ClinicManagementSystem.Infrastructure.Persistence.Data;

namespace ClinicManagementSystem.Infrastructure.Persistence
{
    public class PatientRepo : Repo<Patient> , IPatientRepo
    {
        public PatientRepo(ApplicationDbContext context) : base(context) { }
    }
}
