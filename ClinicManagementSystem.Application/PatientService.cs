using ClinicManagementSystem.Core.DTOs;
using ClinicManagementSystem.Core.DTOs.PatientDtos;
using ClinicManagementSystem.Core.Interfaces.Repos;
using ClinicManagementSystem.Application.CustomExceptions;
using ClinicManagementSystem.Application.Extensions;
using System.Net;
using ClinicManagementSystem.Core.Interfaces;

namespace ClinicManagementSystem.Application
{
    public class PatientService : IPatientService
    {
        private readonly IPatientRepo _patientRepo;

        public PatientService(IPatientRepo patientRepo)
        {
            _patientRepo = patientRepo;
        }

        public async ValueTask<IEnumerable<PatientReadResponse>> GetAllAsync(PaginRequest paginRequest)
        {
            var patients = await _patientRepo.GetAllAsync(paginRequest.Skip, paginRequest.Take);

            if (patients == null || !patients.Any())
                return new List<PatientReadResponse>();

            return patients.Select(patient => patient.PatientToReadResponse());
        }

        public async ValueTask<PatientReadResponse> GetByIdAsync(Guid patientId)
        {
            var patient = await this._patientRepo.GetByIdAsync(patientId);

            if (patient == null)
                throw new ServiceException("Patient Not Found", HttpStatusCode.NotFound);

            return patient.PatientToReadResponse();
        }

        public async ValueTask<PatientUpdateResponse> UpdateAsync(Guid patientId, PatientUpdateRequest patientUpdateRequest)
        {
            var patient = await this._patientRepo.GetByIdAsync(patientId);


            if (patient == null)
                throw new ServiceException("Patient Not Found", HttpStatusCode.NotFound);

            patientUpdateRequest.UpdateRequestToPatient(patient);

            this._patientRepo.Update(patient);

            await this._patientRepo.SaveChangesAsync();

            return patient.PatientToUpdateResponse();
        }
    }
}
