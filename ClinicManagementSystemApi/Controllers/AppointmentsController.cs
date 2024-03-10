using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using ClinicManagementSystem.Core.Interfaces;
using ClinicManagementSystem.Core.DTOs.AppointmentDtos;
using ClinicManagementSystem.Core.DTOs;

namespace ClinicManagementSystemApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AppointmentsController : ControllerBase
    {
        private readonly IAppointmentService _appointmentService;

        public AppointmentsController(IAppointmentService appointmentService)
        {
            _appointmentService = appointmentService;
        }


        //[Authorize(Roles = "Admin")]
        [HttpGet]
        public async Task<ActionResult<IEnumerable<AppointmentReadResponse>>> Get([FromQuery] PaginRequest paginRequest)
        {
            if(!CustomValidator.IsValidPaging(paginRequest)) 
            {
                return BadRequest("Invalid Request");
            }

            return Ok(await _appointmentService.GetAllAsync(paginRequest));   
        }

        // [Authorize(Roles = "Admin" ,AuthenticationSchemes ="Bearer")]
        [HttpGet("{id}")]
        public async Task<ActionResult<AppointmentReadResponse>> GetById(Guid id) =>
            Ok(await this._appointmentService.GetByIdAsync(id));

        // [Authorize(Roles = "Doctor", AuthenticationSchemes = "Bearer")]
        [HttpGet("{appointmentId}/{doctorId}")]
        public async Task<ActionResult<AppointmentReadResponse>> GetDoctorAppointment(Guid appointmentId) =>
            await this._appointmentService.GetDoctorAppointment(appointmentId);

        [HttpGet("{doctorId}/{DateTimeOffset}/Schedule")]
        public async Task<ActionResult<AppointmentReadResponse>> GetDoctorSchedule(Guid doctorId, DateTimeOffset dateTime) =>
            Ok(await this._appointmentService.GetDoctorSchedule(doctorId, dateTime));


        // [Authorize(Roles = "Patient",AuthenticationSchemes = "Bearer")]
        [HttpGet("{appointmentId}/{patientId}")]
        public async Task<ActionResult<AppointmentReadResponse>> GetPatientAppointment(Guid appointmentId) =>
            Ok(await this._appointmentService.GetPatientAppointment(appointmentId));
            
       // [Authorize(Roles = "Admin", AuthenticationSchemes = "Bearer")]
        [HttpPut("{appointmentId}/Cancel")]
        public async Task<ActionResult<AppointmentUpdateResponse>> Cancel(Guid appointmentId) =>
            await this._appointmentService.Cancel(appointmentId);


        //[Authorize(Roles = "Patient,Doctor", AuthenticationSchemes = "Bearer")]
        [HttpGet("{patientId}/PreviousAppointments")]
        public async Task<ActionResult<AppointmentReadResponse>> GetPatientPreviousAppointments(Guid patientId,[FromQuery] PaginRequest paginRequest)
        {
            if (!CustomValidator.IsValidPaging(paginRequest))
                return BadRequest("Invalid Request");


            return Ok(await _appointmentService.GetPatientPreviousAppointments(patientId, paginRequest));
        }

        //[Authorize(Roles = "Doctor", AuthenticationSchemes = "Bearer")]
        [HttpGet("{doctorId}/{patientId}/DoctorPatientHistory")]
        public async Task<ActionResult<AppointmentReadResponse>> GetDoctorPatientPrevAppointments(Guid doctorId, Guid patientId, [FromQuery] PaginRequest paginRequest)
        {
            if (!CustomValidator.IsValidPaging(paginRequest))
                return BadRequest("Invalid Request");

            return Ok(await this._appointmentService.GetDoctorPatientAppointments(doctorId, patientId, paginRequest));
        }
           

       // [Authorize(Roles = "Patient", AuthenticationSchemes = "Bearer")]
        [HttpPost]
        public async Task<ActionResult<AppointmentUpdateRequest>> Post([FromBody] AppointmentCreateRequest appointmentCreate)
        {
            var duration = appointmentCreate.EndDate.Minute - appointmentCreate.StartDate.Minute;
            
            if (appointmentCreate== null)
            {
                return BadRequest("Cannot insert null object");
            }
            
            if (duration < 15 || duration > 120)
            {
                return BadRequest("Appointment duration not valid");
            }

            if (appointmentCreate.StartDate < DateTimeOffset.UtcNow || appointmentCreate.EndDate <= DateTimeOffset.UtcNow || appointmentCreate.StartDate >= appointmentCreate.EndDate)
            {
                return BadRequest("Appointment date not valid");
            }


            var postedAppointment = await this._appointmentService.AddAsync(appointmentCreate);


            return CreatedAtAction(nameof(GetById), new { id = postedAppointment.AppointmentId}, postedAppointment);
        }

       // [Authorize(Roles = "Admin,Doctor", AuthenticationSchemes = "Bearer")]
        [HttpPatch("{id}")]
        public async Task<ActionResult<AppointmentUpdateResponse>> Patch(Guid id, [FromBody] JsonPatchDocument<AppointmentUpdateRequest> jsonPatchDocument)
        {
            if (jsonPatchDocument == null)
            {
                return BadRequest("Cannot update null object");
            }

            var appointmentUpdate = new AppointmentUpdateRequest();

            jsonPatchDocument.ApplyTo(appointmentUpdate);

            var updatedAppointment = await this._appointmentService.Update(id, appointmentUpdate);

            return Ok(updatedAppointment);
        }
    }
}
