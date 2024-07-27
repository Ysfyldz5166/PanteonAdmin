using MediatR;
using Microsoft.AspNetCore.Mvc;
using Panteon.Api.Controllers;
using Panteon.Business.Command.User;
using Panteon.Entities.Dto;
using Panteon.Helper;

namespace Panteon.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : BaseController
    {
        private readonly IMediator _mediator;

        public UserController(IMediator mediator)
        {
            _mediator = mediator;
        }

        /// <summary>
        /// Create Add User.
        /// </summary>
        /// <param name="addUserCommand"></param>
        /// <returns></returns>
        [HttpPost("add")]
        [Produces("application/json", "application/xml", Type = typeof(ServiceResponse<UserDto>))]
        public async Task<IActionResult> Add(AddUserCommand addUserCommand)
        {
            var result = await _mediator.Send(addUserCommand);
            return ReturnFormattedResponse(result);
        }

        /// <summary>
        /// Get User by Email or Username
        /// </summary>
        /// <param name="getUserCommand"></param>
        /// <returns></returns>
        [HttpPost("login")]
        [Produces("application/json", "application/xml", Type = typeof(ServiceResponse<UserDto>))]
        public async Task<IActionResult> Login(GetUserCommand getUserCommand)
        {
            var result = await _mediator.Send(getUserCommand);
            return ReturnFormattedResponse(result);
        }
    }
}
