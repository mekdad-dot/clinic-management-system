using ClinicManagementSystem.Core.DTOs;
using ClinicManagementSystem.Core.Entities;
using ClinicManagementSystem.Core.Interfaces.BaseRepos;

namespace ClinicManagementSystem.Core.Interfaces.Repos
{
    public interface IAppointmentRepo : IRepo<Appointment>
    {
        ValueTask<List<Appointment>> GetDoctorSchedule(Guid doctorId, DateTimeOffset dateTime);
        ValueTask<IEnumerable<Appointment>> GetDoctorPatientAppointments(Guid doctorId, Guid patientId, PaginRequest paginRequest);
        ValueTask<IEnumerable<Appointment>> GetPatientUpcomingAppointments(Guid patientId);
        ValueTask<IEnumerable<Appointment>> GetPatientPreviousAppointments(Guid patientId, PaginRequest paginRequest);
    }
}
