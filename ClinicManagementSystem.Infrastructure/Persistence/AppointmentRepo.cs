using ClinicManagementSystem.Core.DTOs;
using ClinicManagementSystem.Core.Entities;
using ClinicManagementSystem.Core.Interfaces.Repos;
using ClinicManagementSystem.Infrastructure.Persistence.Data;
using Microsoft.EntityFrameworkCore;


namespace ClinicManagementSystem.Infrastructure.Persistence
{
    public class AppointmentRepo : Repo<Appointment> ,IAppointmentRepo
    {      
        public AppointmentRepo(ApplicationDbContext context) : base (context) { }
      

        public async ValueTask<List<Appointment>> GetDoctorSchedule(Guid doctorId,DateTimeOffset dateTime) => await this._table.Include(a => a.Patient)
                                                                                                                         .Where(a => a.DoctorId == doctorId && a.StartDate.Date == dateTime.Date)
                                                                                                                         .ToListAsync();

        public async ValueTask<IEnumerable<Appointment>> GetDoctorPatientAppointments(Guid doctorId, Guid patientId, PaginRequest paginRequest)  => 
            await this._table.Where(a => a.PatientId == patientId && a.DoctorId == doctorId).Skip(paginRequest.Skip).Take(paginRequest.Take).ToListAsync();

        public async ValueTask<IEnumerable<Appointment>> GetPatientUpcomingAppointments(Guid patientId) =>
            await this._table.Where(a => a.StartDate >= DateTimeOffset.UtcNow && a.PatientId == patientId).ToListAsync();

        public async ValueTask<IEnumerable<Appointment>> GetPatientPreviousAppointments(Guid patientId, PaginRequest paginRequest) => await this._table.Include(a => a.Doctor)
                                                                                                                            .Where(a => a.PatientId == patientId && a.StartDate < DateTimeOffset.UtcNow)
                                                                                                                            .Skip(paginRequest.Skip)
                                                                                                                            .Take(paginRequest.Take)
                                                                                                                            .ToListAsync();

    }
}
