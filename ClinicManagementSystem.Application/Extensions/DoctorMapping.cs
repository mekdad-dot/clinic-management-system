using ClinicManagementSystem.Core.DTOs.DoctorDtos;
using ClinicManagementSystem.Core.Entities;
using ClinicManagementSystem.Core.Entities.Identities;

namespace ClinicManagementSystem.Application.Extensions
{
    public static class DoctorMapping
    {
        public static DoctorReadResponse DoctorToReadResponse(this Doctor doctor) =>
            new DoctorReadResponse
            {
                Id = doctor.Id,
                Email = doctor.Email,
                FirstName = doctor.FirstName,
                LastName = doctor.LastName,
                UserName = doctor.UserName,
                PhoneNumber = doctor.PhoneNumber,
                Specialization= doctor.Specialization,
            };

        public static DoctorAvailabilityResponse DoctorToAvailabilityResponse(this Doctor doctor) =>
            new DoctorAvailabilityResponse
            {
                Id = doctor.Id,
                Email = doctor.Email,
                FirstName = doctor.FirstName,
                LastName = doctor.LastName,
                UserName = doctor.UserName,
                PhoneNumber = doctor.PhoneNumber,
                Specialization = doctor.Specialization,
            };



        public static void UpdateRequestToDoctor(this DoctorUpdateRequest updateRequest,Doctor doctor)
        {
            if(!string.IsNullOrEmpty(updateRequest.PhoneNumber))
            {
                doctor.PhoneNumber= updateRequest.PhoneNumber;
            } 

            if(!string.IsNullOrEmpty(updateRequest.Specialization))
            {
                doctor.Specialization= updateRequest.Specialization;
            }
        }

        public static DoctorUpdateResponse DoctorToUpdateResponse(this Doctor doctor) =>
            new DoctorUpdateResponse
            {
                Id = doctor.Id,
                Email = doctor.Email,
                FirstName = doctor.FirstName,
                LastName = doctor.LastName,
                UserName = doctor.UserName,
                PhoneNumber = doctor.PhoneNumber,
                Specialization= doctor.Specialization,
            };

    }
}
