using MediatR;
using Panteon.Entities.Dto;
using Panteon.Helper;
using System.Collections.Generic;

namespace Panteon.Business.Queries.Buildings
{
    public class GetAllBuildQuery : IRequest<ServiceResponse<List<BuildDto>>>
    {
    }
}
