using MediatR;
using Panteon.Business.Command.Buildings;
using Panteon.Helper;
using Panteon.Repository.Buildings;
using System.Threading;
using System.Threading.Tasks;

namespace Panteon.Business.Handlers.Buildings
{
    public class DeleteBuildCommandHandler : IRequestHandler<DeleteBuildCommand, ServiceResponse<string>>
    {
        private readonly IBuildingRepository _buildingRepository;

        public DeleteBuildCommandHandler(IBuildingRepository buildingRepository)
        {
            _buildingRepository = buildingRepository;
        }

        public async Task<ServiceResponse<string>> Handle(DeleteBuildCommand request, CancellationToken cancellationToken)
        {
            var existingBuilding = await _buildingRepository.GetByIdAsync(request.Id);

            if (existingBuilding == null)
            {
                return ServiceResponse<string>.ReturnError(new List<string> { "Bina bulunamadı." });
            }

            await _buildingRepository.DeleteAsync(request.Id);
            return ServiceResponse<string>.ReturnResultWith200(request.Id);
        }
    }
}
