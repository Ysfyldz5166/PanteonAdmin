using AutoMapper;
using MediatR;
using Panteon.Entities.Dto;
using Panteon.Helper;
using Panteon.Repository.Buildings;
using System.Threading;
using System.Threading.Tasks;
using System.Collections.Generic;
using Panteon.Business.Command.Buildings; 

namespace Panteon.Business.Handlers.Buildings
{
    public class UpdateBuildCommandHandler : IRequestHandler<UpdateBuildCommand, ServiceResponse<BuildDto>>
    {
        private readonly IBuildingRepository _buildingRepository;
        private readonly IMapper _mapper;

        public UpdateBuildCommandHandler(IBuildingRepository buildingRepository, IMapper mapper)
        {
            _buildingRepository = buildingRepository;
            _mapper = mapper;
        }

        public async Task<ServiceResponse<BuildDto>> Handle(UpdateBuildCommand request, CancellationToken cancellationToken)
        {
            var building = await _buildingRepository.GetByIdAsync(request.Id);

            if (building == null)
            {
                return ServiceResponse<BuildDto>.ReturnError(new List<string> { "Building not found." });
            }

            building.BuildingType = request.BuildingType;
            building.BuildingCost = request.BuildingCost;
            building.ConstructionTime = request.ConstructionTime;

            await _buildingRepository.UpdateAsync(building);

            var buildingDto = _mapper.Map<BuildDto>(building);
            return ServiceResponse<BuildDto>.ReturnResultWith200(buildingDto);
        }
    }
}
