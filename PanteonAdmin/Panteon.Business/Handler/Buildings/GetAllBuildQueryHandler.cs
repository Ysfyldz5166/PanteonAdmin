using AutoMapper;
using MediatR;
using Panteon.Business.Queries.Buildings;
using Panteon.Entities.Dto;
using Panteon.Helper;
using Panteon.Repository.Buildings;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Panteon.Business.Handlers.Buildings
{
    public class GetAllBuildQueryHandler : IRequestHandler<GetAllBuildQuery, ServiceResponse<List<BuildDto>>>
    {
        private readonly IBuildingRepository _buildingRepository;
        private readonly IMapper _mapper;

        public GetAllBuildQueryHandler(IBuildingRepository buildingRepository, IMapper mapper)
        {
            _buildingRepository = buildingRepository;
            _mapper = mapper;
        }

        public async Task<ServiceResponse<List<BuildDto>>> Handle(GetAllBuildQuery request, CancellationToken cancellationToken)
        {
            var builds = await _buildingRepository.GetAllAsync();
            var buildDtos = _mapper.Map<List<BuildDto>>(builds);

            return ServiceResponse<List<BuildDto>>.ReturnResultWith200(buildDtos);
        }
    }
}
