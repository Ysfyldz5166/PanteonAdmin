using MediatR;
using Panteon.Helper;

namespace Panteon.Business.Command.Buildings
{
    public class DeleteBuildCommand : IRequest<ServiceResponse<string>>
    {
        public string Id { get; set; }
    }
}
