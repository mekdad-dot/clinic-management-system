using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using ClinicManagementSystem.Core.Interfaces;
using ClinicManagementSystem.Core.DTOs.DoctorDtos;
using ClinicManagementSystem.Core.DTOs;

namespace ClinicManagementSystemApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DoctorsController : ControllerBase
    {
        private readonly IDoctorService _doctorService;
   
        public DoctorsController(IDoctorService doctorService)
        {
            _doctorService = doctorService;
        }

        //[Authorize(Roles ="Admin,Patient,Doctor" ,AuthenticationSchemes = "Bearer")]
        [HttpGet]
        public async Task<ActionResult<IEnumerable<DoctorReadResponse>>> Get([FromQuery]PaginRequest paginRequest)
        {
            if (!CustomValidator.IsValidPaging(paginRequest))
                return BadRequest("Invalid request");

            return Ok(await this._doctorService.GetAllAsync(paginRequest));
        }
            

        //[Authorize(Roles = "Doctor,Admin,Patient", AuthenticationSchemes = "Bearer")]
        [HttpGet("AvailableDoctors")]
        public async Task<ActionResult<IEnumerable<DoctorReadResponse>>> GetAvailableDoctors([FromQuery] PaginRequest paginRequest)
        {
            if (!CustomValidator.IsValidPaging(paginRequest))
                return BadRequest("Invalid request");

            if (HttpContext.User.IsInRole("Patient"))
            {
                return Ok(await this._doctorService.GetDoctorsByAvailability(isPatient: true, paginRequest));
            } 
            else
            {
                return Ok(await this._doctorService.GetDoctorsByAvailability(isPatient: false, paginRequest));
            }
        }

        //[Authorize(Roles = "Doctor,Admin,Patient", AuthenticationSchemes = "Bearer")]
        [HttpGet("{id}")]
        public async Task<ActionResult<DoctorReadResponse>> GetById(Guid id) =>
            Ok(await this._doctorService.GetByIdAsync(id));


        //[Authorize(Roles = "Admin", AuthenticationSchemes = "Bearer")]
        [HttpGet("DoctorsByWorkingHours")]
        public async Task<ActionResult<IEnumerable<DoctorReadResponse>>> GetDoctorsOnWorkingHours(DateTimeOffset requiredDate, [FromQuery] PaginRequest paginRequest, int workingHour = 6)
        {
            if (requiredDate.Date > DateTimeOffset.UtcNow.Date || !CustomValidator.IsValidPaging(paginRequest) || workingHour < 0)
                return BadRequest("Invalid request");
           
            return Ok(await this._doctorService.GetDoctorsOnWorkingHours(requiredDate, workingHour,paginRequest));
        }


        //[Authorize(Roles = "Admin", AuthenticationSchemes = "Bearer")]
        [HttpGet("MostMetDoctors")]
        public async Task<ActionResult<IEnumerable<DoctorReadResponse>>> GetMostMetDoctors(DateTimeOffset dateTime, [FromQuery] PaginRequest paginRequest)
        {
            if (!CustomValidator.IsValidPaging(paginRequest))
                return BadRequest("Invalid request");

            return Ok(await this._doctorService.GetMostMetDoctors(dateTime, paginRequest));
        }

        //[Authorize(Roles = "Admin", AuthenticationSchemes = "Bearer")]
        [HttpPatch("{id}")]
        public async Task<ActionResult<DoctorUpdateResponse>> Patch(Guid id, [FromBody] JsonPatchDocument<DoctorUpdateRequest> jsonPatchDocument)
        {
            if (jsonPatchDocument == null)
            {
                return BadRequest("Cannot update null object");
            }

            var serUpdateReq = new DoctorUpdateRequest();

            jsonPatchDocument.ApplyTo(serUpdateReq, ModelState);

            return Ok(await this._doctorService.Update(id, serUpdateReq));
        }
    }
}
