using MediatR;
using Panteon.Entities.Dto;
using Panteon.Helper;

namespace Panteon.Business.Command.Buildings
{
    public class UpdateBuildCommand : IRequest<ServiceResponse<BuildDto>>
    {
        public string Id { get; set; } 
        public string BuildingType { get; set; }
        public decimal BuildingCost { get; set; }
        public int ConstructionTime { get; set; }
    }
}
