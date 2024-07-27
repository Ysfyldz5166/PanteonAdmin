using Microsoft.AspNetCore.Mvc;
using Panteon.Helper;

namespace Panteon.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class BaseController : ControllerBase
    {
        public IActionResult ReturnFormattedResponse<T>(ServiceResponse<T> response)
        {
            if (response.Success)
            {
                return Ok(response.Data);
            }   
            if (response.Errors != null && response.Errors.Count > 0)
            {
                return BadRequest(new { errors = response.Errors });
            }
            return StatusCode(500, "An unexpected error occurred.");
        }
    }
}
