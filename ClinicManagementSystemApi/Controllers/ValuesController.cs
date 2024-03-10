using ClinicManagementSystem.Core.Entities;
using ClinicManagementSystem.Core.Interfaces.Repos;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ClinicManagementSystemApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ValuesController : ControllerBase
    {
        private readonly IAdminRepo _adminRepo;
        private readonly IDoctorRepo _doctorRepo;
        private readonly IPatientRepo _patientRepo;

        public ValuesController(IAdminRepo adminRepo, IDoctorRepo doctorRepo , IPatientRepo patientRepo)
        {
            this._adminRepo = adminRepo;
            this._doctorRepo = doctorRepo;
            this._patientRepo = patientRepo;
        }
        [HttpGet]
        public async Task<IActionResult> Add()
        {
            for (int i = 100; i < 200; i++)
            {
                _adminRepo.Add(new Admin
                {
                    Email = "email" + i + "@gmail.com",
                    FirstName = "firstName" + i ,
                    LastName = "lastName" + i,
                    PhoneNumber = i.ToString(),
                    UserName = "userName" + i + "@gmail.com",
                });
            }

            for (int i = 100; i < 200; i++)
            {
                _doctorRepo.Add(new Doctor
                {
                    Email = "email" + i + "@gmail.com",
                    FirstName = "firstName" + i,
                    LastName = "lastName" + i,
                    PhoneNumber = i.ToString(),
                    UserName = "userName" + i + "@gmail.com",
                    Specialization = "level" + i
                });
            }

            for (int i = 100; i < 200; i++)
            {
                _patientRepo.Add(new Patient
                {
                    Email = "email" + i + "@gmail.com",
                    FirstName = "firstName" + i,
                    LastName = "lastName" + i,
                    PhoneNumber = i.ToString(),
                    UserName = "userName" + i + "@gmail.com",
                    DOB = DateTimeOffset.Now.AddDays(i),
                });
            }

            await _adminRepo.SaveChangesAsync();
            //await _patientRepo.SaveChangesAsync();
            //await _doctorRepo.SaveChangesAsync();

            return Ok();
        }
    }
}
