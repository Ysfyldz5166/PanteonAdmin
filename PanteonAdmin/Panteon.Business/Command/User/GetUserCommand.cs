using MediatR;
using Panteon.Entities.Dto;
using Panteon.Helper;

namespace Panteon.Business.Command.User
{
    public class GetUserCommand : IRequest<ServiceResponse<UserDto>>
    {
        public string EmailOrUsername { get; set; }
        public string Password { get; set; }
    }
}
