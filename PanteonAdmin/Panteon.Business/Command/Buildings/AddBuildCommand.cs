using Amazon.Runtime.Internal;
using MediatR;
using Panteon.Entities.Dto;
using Panteon.Entities.Entities;
using Panteon.Helper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Panteon.Business.Command.Buildings
{
    public class AddBuildCommand : IRequest<ServiceResponse<BuildDto>>
    {
        public string BuildingType { get; set; }
        public decimal BuildingCost { get; set; }
        public int ConstructionTime { get; set; }
    }
}
