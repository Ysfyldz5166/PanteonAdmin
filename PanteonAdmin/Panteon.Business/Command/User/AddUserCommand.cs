using Amazon.Runtime.Internal;
using MediatR;
using Panteon.Entities.Dto;
using Panteon.Helper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Panteon.Business.Command.User
{
    public class AddUserCommand : IRequest<ServiceResponse<UserDto>>
    {
        public string Username { get; set; }
        public string Password { get; set; }
        public string Email { get; set; }
    }
}
