using MediatR;
using Microsoft.AspNetCore.Mvc;
using Panteon.Api.Controllers;
using Panteon.Business.Command.Buildings;
using Panteon.Business.Queries.Buildings;
using Panteon.Entities.Dto;
using Panteon.Helper;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Panteon.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BuildingController : BaseController
    {
        private readonly IMediator _mediator;

        public BuildingController(IMediator mediator)
        {
            _mediator = mediator;
        }

        /// <summary>
        /// Create Add Build.
        /// </summary>
        /// <param name="addBuildCommand"></param>
        /// <returns></returns>
        [HttpPost]
        [Produces("application/json", "application/xml", Type = typeof(AddBuildCommand))]
        public async Task<IActionResult> Add(AddBuildCommand addBuildCommand)
        {
            var result = await _mediator.Send(addBuildCommand);
            return ReturnFormattedResponse(result);
        }

        /// <summary>
        /// Get All Buildings.
        /// </summary>
        /// <returns></returns>
        [HttpGet("buildings")]
        [Produces("application/json", "application/xml", Type = typeof(ServiceResponse<List<BuildDto>>))]
        public async Task<IActionResult> GetAllBuildings()
        {
            var result = await _mediator.Send(new GetAllBuildQuery());
            return ReturnFormattedResponse(result);
        }


        /// <summary>
        /// Update a Building.
        /// </summary>
        /// <param name="id"></param>
        /// <param name="updateBuildCommand"></param>
        /// <returns></returns>
        [HttpPut("{id}")]
        [Produces("application/json", "application/xml", Type = typeof(UpdateBuildCommand))]
        public async Task<IActionResult> Update(string id, [FromBody] UpdateBuildCommand updateBuildCommand)
        {
            updateBuildCommand.Id = id;
            var result = await _mediator.Send(updateBuildCommand);
            return ReturnFormattedResponse(result);
        }

        /// <summary>
        /// Delete Build.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete("{id}")]
        [Produces("application/json", "application/xml", Type = typeof(DeleteBuildCommand))]
        public async Task<IActionResult> Delete(string id)
        {
            var result = await _mediator.Send(new DeleteBuildCommand { Id = id });
            return ReturnFormattedResponse(result);
        }
    }
}
