using Microsoft.AspNetCore.Mvc;

namespace ClinicManagementSystemApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class HealthController : ControllerBase
    {
        [HttpGet]
        public IActionResult Get() => Ok();
    }
}
