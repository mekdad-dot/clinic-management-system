using ClinicManagementSystem.Application.Extensions;
using ClinicManagementSystem.Core.Entities;
using ClinicManagementSystem.Application.CustomExceptions;
using System.Net;
using ClinicManagementSystem.Core.Interfaces.Repos;
using ClinicManagementSystem.Core.DTOs.DoctorDtos;
using ClinicManagementSystem.Core.DTOs;
using ClinicManagementSystem.Core.Interfaces;

namespace ClinicManagementSystem.Application
{
    public class DoctorSerivce : IDoctorService
    {
        private readonly IDoctorRepo _doctorRepo;

        public DoctorSerivce(IDoctorRepo doctorRepo)
        {
            _doctorRepo = doctorRepo;
        }

        public async ValueTask<IEnumerable<DoctorAvailabilityResponse>> GetDoctorsByAvailability(bool isPatient,PaginRequest paginRequest)
        {
            var rDoctors = new List<DoctorAvailabilityResponse>();
            
            var doctors = await this._doctorRepo.GetAllDoctorsAppointments(paginRequest.Skip,paginRequest.Take);

            if (doctors == null || !doctors.Any())
                return new List<DoctorAvailabilityResponse>();

            foreach (var doctor in doctors)
            {
                var rDoctor = doctor.DoctorToAvailabilityResponse();

                if (CheckDoctorAvailability(doctor))
                {
                    rDoctor.IsAvailable = true;
                }
                else
                {
                    rDoctor.IsAvailable = false;
                }

                rDoctors.Add(rDoctor);
            }
                        
            if (isPatient)
            {
                return rDoctors.Where(d=>  d.IsAvailable);
            }

            return rDoctors;
        }

        private bool CheckDoctorAvailability(Doctor doctor)
        {
            if ((doctor.Appointments.Count() < 12 && doctor.Appointments.Sum(a => a.Duration) < 480 && !doctor.Appointments.Any(a => a.StartDate <= DateTimeOffset.UtcNow && a.EndDate > DateTimeOffset.UtcNow)) ||
                    doctor.Appointments.Count == 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public async ValueTask<DoctorReadResponse> GetByIdAsync(Guid doctorId)
        {
            var doctor = await this._doctorRepo.GetByIdAsync(doctorId);

            if (doctor == null)
            {
                throw new ServiceException("Doctor not found",HttpStatusCode.NotFound);
            }

            return doctor.DoctorToReadResponse();
        }

        public async ValueTask<IEnumerable<DoctorReadResponse>> GetDoctorsOnWorkingHours(DateTimeOffset dateTime,int workingHours,PaginRequest paginRequest)
        {
            var doctors = await this._doctorRepo.GetDoctorsByWorkingHours(dateTime, workingHours,paginRequest.Skip, paginRequest.Take);

            if (doctors == null || !doctors.Any())
                return new List<DoctorReadResponse>();

            return doctors.Select(d => d.DoctorToReadResponse());
        }

        public async ValueTask<IEnumerable<DoctorReadResponse>> GetMostMetDoctors(DateTimeOffset dateTime, PaginRequest paginRequest)
        {
            var doctors = await this._doctorRepo.GetMostMetDoctors(dateTime,paginRequest.Skip,paginRequest.Take);

            if(doctors == null || !doctors.Any())
                return new List<DoctorReadResponse>();

            return doctors.Select(d => d.DoctorToReadResponse());
        }

        public async ValueTask<DoctorUpdateResponse> Update(Guid doctorId, DoctorUpdateRequest doctorUpdateRequest)
        {
            var doctor = await this._doctorRepo.GetByIdAsync(doctorId);

            if (doctor == null)
            {
                throw new ServiceException("Doctor not found",HttpStatusCode.NotFound);
            }

            doctorUpdateRequest.UpdateRequestToDoctor(doctor);

            this._doctorRepo.Update(doctor);

            await _doctorRepo.SaveChangesAsync();

            return doctor.DoctorToUpdateResponse();
        }

        public async ValueTask<IEnumerable<DoctorReadResponse>> GetAllAsync(PaginRequest paginRequest)
        {
            var doctors = await _doctorRepo.GetAllAsync(paginRequest.Skip, paginRequest.Take);

            if (doctors == null || !doctors.Any())
                return new List<DoctorReadResponse>();

            return doctors.Select(d => d.DoctorToReadResponse());
        }
    }
}
