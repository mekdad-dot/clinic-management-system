using ClinicManagementSystem.Core.DTOs;
using ClinicManagementSystem.Core.DTOs.AppointmentDtos;
using ClinicManagementSystem.Core.Entities;
using ClinicManagementSystem.Core.Interfaces.Repos;
using ClinicManagementSystem.Application.CustomExceptions;
using ClinicManagementSystem.Application.Extensions;
using System.Net;
using ClinicManagementSystem.Core.Interfaces;

namespace ClinicManagementSystem.Application
{
    public class AppointmentService : IAppointmentService
    {
        private readonly IAppointmentRepo _appointmentRepo;
        private readonly IPatientRepo _petientRepo;

        public AppointmentService(IAppointmentRepo appointmentRepo, IPatientRepo petientRepo)
        {
            _appointmentRepo = appointmentRepo;
            _petientRepo = petientRepo;
        }

        public async ValueTask<IEnumerable<Appointment>> GetAllAsync(PaginRequest paginRequest) => await _appointmentRepo.GetAllAsync(paginRequest.Skip,paginRequest.Take);

        public async ValueTask<IEnumerable<AppointmentReadResponse>> GetDoctorSchedule(Guid doctorId, DateTimeOffset dateTime)
        {
            var appointments = await this._appointmentRepo.GetDoctorSchedule(doctorId, dateTime);
            
            if(appointments == null || !appointments.Any())
                return new List<AppointmentReadResponse>();

            return appointments.Select(a => a.AppointmentToReadRes());
        }
        public async ValueTask<AppointmentCreateResponse> AddAsync(AppointmentCreateRequest appointmentCreateRequest)
        {
            var doctorSchedules = await this._appointmentRepo.GetDoctorSchedule(appointmentCreateRequest.DoctorId, appointmentCreateRequest.StartDate.Date);
            
            bool isDoctorEligible;

            if(doctorSchedules != null && doctorSchedules.Any())
                isDoctorEligible = CheckDoctorAppointmentEligibility(doctorSchedules);
            else
                isDoctorEligible = true;


            if (isDoctorEligible)
            {                
                var patientScheules = await this._appointmentRepo.GetPatientUpcomingAppointments(appointmentCreateRequest.PatientId);

                if (doctorSchedules != null  && doctorSchedules.Any(a => a.StartDate <= appointmentCreateRequest.StartDate && 
                                                                         a.EndDate <= appointmentCreateRequest.EndDate && 
                                                                         a.EndDate >= appointmentCreateRequest.StartDate))
                {
                    throw new ServiceException("Appointment conflict", HttpStatusCode.BadRequest);
                }

                if (patientScheules != null)
                {
                    if (patientScheules.Any(a => a.StartDate <= appointmentCreateRequest.StartDate && 
                                                 a.EndDate <= appointmentCreateRequest.EndDate && 
                                                 a.EndDate >= appointmentCreateRequest.StartDate))
                    {
                        throw new ServiceException("Appointment conflict", HttpStatusCode.BadRequest);
                    }
                }

                var appointment = appointmentCreateRequest.CreateReqToAppointment();

                _appointmentRepo.Add(appointment);

                await _appointmentRepo.SaveChangesAsync();

                return appointment.AppointmentToCreateRes();
            }
            else
            {
                throw new ServiceException("Doctor achieved work limits", HttpStatusCode.BadRequest);
            }
        }
        
        public async ValueTask<AppointmentReadResponse> GetByIdAsync(Guid appointmentId)
        {
            var appointment = await this._appointmentRepo.GetByIdAsync(appointmentId);

            if (appointment == null)
            {
                throw new ServiceException("Appointment not found", HttpStatusCode.NotFound);
            }

            return appointment.AppointmentToReadRes();
        }

        public async ValueTask<AppointmentUpdateResponse> Update(Guid appointmentId, AppointmentUpdateRequest appointmentUpdateRequest)
        {
            var appointment = await this._appointmentRepo.GetByIdAsync(appointmentId);

            appointment.Description = appointmentUpdateRequest.Description;

            this._appointmentRepo.Update(appointment);

            await _appointmentRepo.SaveChangesAsync();

            return appointment.AppointmentToUpdateRes();
        }

        public async ValueTask<IEnumerable<AppointmentReadResponse>> GetDoctorPatientAppointments(Guid doctorId,Guid patientId,PaginRequest paginRequest)
        {
            var appointments = await this._appointmentRepo.GetDoctorPatientAppointments(doctorId, patientId, paginRequest);

            if(appointments == null || !appointments.Any())
                return new List<AppointmentReadResponse>();

            return appointments.Select(a => a.AppointmentToReadRes()); 
        }

        public async ValueTask<IEnumerable<AppointmentReadResponse>> GetPatientPreviousAppointments(Guid PatientId,PaginRequest paginRequest)
        {
            var appointments = await this._appointmentRepo.GetPatientPreviousAppointments(PatientId, paginRequest);

            if (appointments == null || !appointments.Any())
                return new List<AppointmentReadResponse>();

            return appointments.Select(a => a.AppointmentToReadRes());
        }

        public async ValueTask<AppointmentReadResponse> GetDoctorAppointment(Guid appointmentId)
        {
            var appointment = await this._appointmentRepo.GetByIdAsync(appointmentId);

            if (appointment == null)
            {
                throw new ServiceException("Appointment not found", HttpStatusCode.NotFound);
            }

            return appointment.AppointmentToReadRes();
        }

        public async ValueTask<AppointmentReadResponse> GetPatientAppointment(Guid appointmentId)
        {
            var appointment = await this._appointmentRepo.GetByIdAsync(appointmentId);

            if (appointment == null)
            {
                throw new ServiceException("Appointment not found", HttpStatusCode.NotFound);
            }

            return appointment.AppointmentToReadRes();
        }
        public async ValueTask<AppointmentUpdateResponse> Cancel(Guid appointmentId)
        {
            var appointment = await this._appointmentRepo.GetByIdAsync(appointmentId);

            if (appointment == null)
            {
                throw new ServiceException("Appointment not found", HttpStatusCode.NotFound);
            }

            appointment.IsCancelled = true;

            this._appointmentRepo.Update(appointment);
             
            await this._petientRepo.SaveChangesAsync();

            return appointment.AppointmentToUpdateRes(); 
        }

        private bool CheckDoctorAppointmentEligibility(IEnumerable<Appointment> doctorAppointments)
        {
            double minutes = 0;
            int count = 0;

            foreach (var appointment in doctorAppointments)
            {
                if (!appointment.IsCancelled)
                {
                    minutes += appointment.Duration;
                    count++;
                }
            }

            double hours = minutes / 60;

            if (hours >= 8 || count >= 12)
            {
                return false;
            }

            return true;
        }
    }
}
