using ClinicManagementSystem.Core.Interfaces;
using ClinicManagementSystem.Core.DTOs;
using ClinicManagementSystem.Core.DTOs.PatientDtos;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;

namespace ClinicManagementSystemApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PatientsController : ControllerBase
    {
        private readonly IPatientService _patientService;

        public PatientsController(IPatientService patientService)
        {
            _patientService = patientService;
        }

        //[Authorize(Roles = "Admin", AuthenticationSchemes = "Bearer")]
        [HttpGet]
        public async Task<ActionResult<PatientReadResponse>> Get([FromQuery] PaginRequest paginRequest)
        {
            if (!CustomValidator.IsValidPaging(paginRequest))
                return BadRequest("Invalid request");

            return Ok(await this._patientService.GetAllAsync(paginRequest));
        }
               

        //[Authorize(Roles = "Admin,Doctor", AuthenticationSchemes = "Bearer")]
        [HttpGet("{id}")]
        public async Task<ActionResult<PatientReadResponse>> GetById(Guid id) =>
             Ok(await this._patientService.GetByIdAsync(id));

        //[Authorize(Roles = "Admin", AuthenticationSchemes = "Bearer")]
        [HttpPatch("{id}")]
        public async Task<ActionResult<PatientUpdateResponse>> Patch(Guid id, [FromBody] JsonPatchDocument<PatientUpdateRequest> jsonPatchDocument)
        {
            if (jsonPatchDocument == null)
            {
                return BadRequest("Cannot update null object");
            }

            var serUpdateReq = new PatientUpdateRequest();

            jsonPatchDocument.ApplyTo(serUpdateReq, ModelState);

            return Ok(await this._patientService.UpdateAsync(id, serUpdateReq));
        }
    }
}
