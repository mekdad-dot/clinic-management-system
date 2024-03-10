using ClinicManagementSystem.Core.Entities;
using ClinicManagementSystem.Core.Interfaces.Repos;
using ClinicManagementSystem.Infrastructure.Persistence.Data;
using Microsoft.EntityFrameworkCore;

namespace ClinicManagementSystem.Infrastructure.Persistence
{
    public class DoctorRepo : Repo<Doctor>, IDoctorRepo
    {
        public DoctorRepo(ApplicationDbContext context) : base(context) { }

        public async ValueTask<IEnumerable<Doctor>> GetAvailableDoctorsAsync(int skip, int take)
        {
            var doctors = await this._table.Where(d => d.Appointments.Where(a => 
                                         a.StartDate.Date == DateTimeOffset.UtcNow.Date && !a.IsCancelled).Count() < 12 &&
                                         d.Appointments.Where(a => a.StartDate.Date == DateTimeOffset.UtcNow.Date && !a.IsCancelled).Sum(a => a.Duration) < 480 &&
                                         d.Appointments.Any(a => a.EndDate <= DateTimeOffset.UtcNow) ||  d.Appointments.Count == 0)
                                        .Skip(skip)
                                        .Take(take)
                                        .ToListAsync();

            return doctors;
        }

        public async ValueTask<IEnumerable<Doctor>> GetMostMetDoctors(DateTimeOffset dateTime, int skip, int take)
        {
            var doctors = await this._table.Include(d => d.Appointments.Where(a => !a.IsCancelled && a.StartDate.Date == dateTime.Date))
                                           .Where(d => d.Appointments.Any(a => a.StartDate.Date == dateTime.Date))
                                           .Skip(skip)
                                           .Take(take)
                                           .OrderByDescending(d => d.Appointments.Count( a => a.StartDate.Date == dateTime.Date))
                                           .ToListAsync();

            return doctors;
        }
        public async ValueTask<IEnumerable<Doctor>> GetAllDoctorsAppointments(int skip,int take) => 
            await _table.Include(d => d.Appointments.Where(a => a.StartDate.Date == DateTimeOffset.UtcNow.Date && !a.IsCancelled))
                        .Skip(skip)
                        .Take(take)
                        .ToListAsync();
        public async ValueTask<IEnumerable<Doctor>> GetDoctorsByWorkingHours(DateTimeOffset dateTime,int workingHours, int skip, int take)
        {
            var doctors = await this._table.Where(d => d.Appointments.Where(a=> (a.StartDate.Date == dateTime.Date && a.StartDate <= dateTime) && !a.IsCancelled)
                                           .Sum(a => a.Duration) / 60.0 > workingHours)
                                           .Skip(skip)
                                           .Take(take)
                                           .ToListAsync();
            
            return doctors;
        }
    }
}
