using ClinicManagementSystem.Core.DTOs.PatientDtos;
using ClinicManagementSystem.Core.Entities;

namespace ClinicManagementSystem.Application.Extensions
{
    public static class PatientMapping
    {
        public static PatientReadResponse PatientToReadResponse(this Patient patient) =>
            new PatientReadResponse
            {
                Id = patient.Id,
                Email = patient.Email,
                FirstName = patient.FirstName,
                LastName = patient.LastName,
                UserName = patient.UserName,
                PhoneNumber = patient.PhoneNumber,
                DOB= patient.DOB,
            };

        public static void UpdateRequestToPatient(this PatientUpdateRequest updateRequest, Patient patient)
        {

            if (!string.IsNullOrEmpty(updateRequest.PhoneNumber))
            {
                patient.PhoneNumber = updateRequest.PhoneNumber;
            }

            patient.DOB = updateRequest.DOB;
        }

        public static PatientUpdateResponse PatientToUpdateResponse(this Patient patient) =>
            new PatientUpdateResponse
            {
                Id = patient.Id,
                Email = patient.Email,
                FirstName = patient.FirstName,
                LastName = patient.LastName,
                UserName = patient.UserName,
                PhoneNumber = patient.PhoneNumber,
                DOB = patient.DOB,
            };
    }
}
