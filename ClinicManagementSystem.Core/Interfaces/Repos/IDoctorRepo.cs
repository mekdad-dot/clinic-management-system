using ClinicManagementSystem.Core.Entities;
using ClinicManagementSystem.Core.Interfaces.BaseRepos;

namespace ClinicManagementSystem.Core.Interfaces.Repos
{
    public interface IDoctorRepo : IRepo<Doctor>
    {
        ValueTask<IEnumerable<Doctor>> GetAvailableDoctorsAsync(int skip, int take);
        ValueTask<IEnumerable<Doctor>> GetMostMetDoctors(DateTimeOffset dateTime, int skip, int take);
        ValueTask<IEnumerable<Doctor>> GetAllDoctorsAppointments(int skip, int take);
        ValueTask<IEnumerable<Doctor>> GetDoctorsByWorkingHours(DateTimeOffset dateTime, int workingHours, int skip, int take);
    }
}
