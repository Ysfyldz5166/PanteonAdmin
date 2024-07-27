using AutoMapper;
using MediatR;
using Panteon.Business.Command.Buildings;
using Panteon.Entities.Dto;
using Panteon.Entities.Entities;
using Panteon.Helper;
using Panteon.Repository.Buildings;
using System.Threading;
using System.Threading.Tasks;
using FluentValidation;
using System.Linq;

namespace Panteon.Businnes.Handlers.Buildings
{
    public class AddBuildCommandHandler : IRequestHandler<AddBuildCommand, ServiceResponse<BuildDto>>
    {
        private readonly IBuildingRepository _buildingRepository;
        private readonly IMapper _mapper;
        private readonly IValidator<AddBuildCommand> _validator;

        public AddBuildCommandHandler(IBuildingRepository buildingRepository, IMapper mapper, IValidator<AddBuildCommand> validator)
        {
            _buildingRepository = buildingRepository;
            _mapper = mapper;
            _validator = validator;
        }

        public async Task<ServiceResponse<BuildDto>> Handle(AddBuildCommand request, CancellationToken cancellationToken)
        {
            var validationResult = await _validator.ValidateAsync(request, cancellationToken);
            if (!validationResult.IsValid)
            {
                var errors = validationResult.Errors.Select(e => e.ErrorMessage).ToList();
                return ServiceResponse<BuildDto>.ReturnError(errors);
            }

            var build = new Build
            {
                Id = MongoDB.Bson.ObjectId.GenerateNewId().ToString(), 
                BuildingType = request.BuildingType,
                BuildingCost = request.BuildingCost,
                ConstructionTime = request.ConstructionTime
            };

            await _buildingRepository.AddAsync(build);

            var buildDto = _mapper.Map<BuildDto>(build);
            return ServiceResponse<BuildDto>.ReturnResultWith200(buildDto);
        }
    }
}
