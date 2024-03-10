using ClinicManagementSystem.Core.DTOs;
using ClinicManagementSystem.Core.DTOs.AppointmentDtos;
using ClinicManagementSystem.Core.Entities;


namespace ClinicManagementSystem.Core.Interfaces
{
    public interface IAppointmentService
    {
        ValueTask<IEnumerable<Appointment>> GetAllAsync(PaginRequest paginRequest);
        ValueTask<IEnumerable<AppointmentReadResponse>> GetDoctorSchedule(Guid doctorId, DateTimeOffset dateTime);
        ValueTask<AppointmentCreateResponse> AddAsync(AppointmentCreateRequest appointmentCreateRequest);
        ValueTask<AppointmentReadResponse> GetByIdAsync(Guid appointmentId);
        ValueTask<AppointmentUpdateResponse> Update(Guid appointmentId, AppointmentUpdateRequest appointmentUpdateRequest);
        ValueTask<IEnumerable<AppointmentReadResponse>> GetDoctorPatientAppointments(Guid DoctorId, Guid PatientId,PaginRequest paginRequest);
        ValueTask<IEnumerable<AppointmentReadResponse>> GetPatientPreviousAppointments(Guid PatientId,PaginRequest paginRequest);
        ValueTask<AppointmentReadResponse> GetDoctorAppointment(Guid appointmentId);
        ValueTask<AppointmentReadResponse> GetPatientAppointment(Guid appointmentId);
        ValueTask<AppointmentUpdateResponse> Cancel(Guid appointmentId);
    }
}
