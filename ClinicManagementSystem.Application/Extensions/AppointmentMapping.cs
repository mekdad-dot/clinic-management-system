using ClinicManagementSystem.Core.DTOs.AppointmentDtos;
using ClinicManagementSystem.Core.DTOs.DoctorDtos;
using ClinicManagementSystem.Core.DTOs.PatientDtos;
using ClinicManagementSystem.Core.Entities;

namespace ClinicManagementSystem.Application.Extensions
{
    public static class AppointmentMapping
    {
        public static Appointment CreateReqToAppointment(this AppointmentCreateRequest createRequest) =>
            new Appointment
            {
                Duration = createRequest.EndDate.Minute - createRequest.StartDate.Minute,
                PatientId = createRequest.PatientId,
                DoctorId = createRequest.DoctorId,
                Description = createRequest.Description,
                StartDate = createRequest.StartDate.ToUniversalTime(),
                EndDate = createRequest.EndDate.ToUniversalTime(),
            };

        public static AppointmentCreateResponse AppointmentToCreateRes(this Appointment appointment) =>
            new AppointmentCreateResponse
            {
                AppointmentId = appointment.AppointmentId,
                StartDate = appointment.StartDate,
                EndDate = appointment.EndDate,
                Description= appointment.Description,
                Duration = appointment.Duration,
                IsCancelled = appointment.IsCancelled,
                DoctorId= appointment.DoctorId,
                PatientId= appointment.PatientId,
            };

        public static Appointment UpdateReqToAppointment(this AppointmentUpdateRequest updateRequest,Appointment appointment)
        {
            if(!string.IsNullOrEmpty(updateRequest.Description))
            {
                appointment.Description = updateRequest.Description;
            }

            return appointment;
        }

        public static AppointmentUpdateResponse AppointmentToUpdateRes(this Appointment appointment) =>
            new AppointmentUpdateResponse
            {
                AppointmentId = appointment.AppointmentId,
                StartDate = appointment.StartDate,
                EndDate = appointment.EndDate,
                Description = appointment.Description,
                IsCancelled = appointment.IsCancelled,
                Duration = appointment.Duration
            };

        public static AppointmentReadResponse AppointmentToReadRes(this Appointment appointment)
        {
            var readResponse = new AppointmentReadResponse
            {
                AppointmentId = appointment.AppointmentId,
                Duration = appointment.Duration,
                StartDate = appointment.StartDate,
                EndDate = appointment.EndDate,
                Description = appointment.Description,
                IsCancelled = appointment.IsCancelled,
            };

            if(appointment.Patient != null)
            {
                readResponse.Patient = new PatientReadResponse
                {
                    Id = appointment.PatientId,
                    DOB = appointment.Patient.DOB,
                    FirstName = appointment.Patient.FirstName,
                    LastName = appointment.Patient.LastName,
                };
            }

            if(appointment.Doctor != null)
            {
                readResponse.Doctor = new DoctorReadResponse
                {
                    Id = appointment.DoctorId,
                    Specialization = appointment.Doctor.Specialization,
                    FirstName = appointment.Doctor.FirstName,
                    LastName = appointment.Doctor.LastName,
                };
            }

            return readResponse;
        }
    }
}
