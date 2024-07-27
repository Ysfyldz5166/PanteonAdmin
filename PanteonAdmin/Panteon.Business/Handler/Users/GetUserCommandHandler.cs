using AutoMapper;
using MediatR;
using Panteon.Business.Command.User;
using Panteon.Entities.Dto;
using Panteon.Helper;
using Panteon.Repository.Users;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Panteon.Business.Handlers.User
{
    public class GetUserCommandHandler : IRequestHandler<GetUserCommand, ServiceResponse<UserDto>>
    {
        private readonly IUserRepository _userRepository;
        private readonly IMapper _mapper;

        public GetUserCommandHandler(IUserRepository userRepository, IMapper mapper)
        {
            _userRepository = userRepository;
            _mapper = mapper;
        }

        public async Task<ServiceResponse<UserDto>> Handle(GetUserCommand request, CancellationToken cancellationToken)
        {
            var user = await _userRepository.GetByEmailOrUsernameAsync(request.EmailOrUsername);
            if (user == null || !PasswordHasher.VerifyPassword(user.Password, request.Password))
            {
                return ServiceResponse<UserDto>.ReturnError(new List<string> { "Kullanıcı adı/Email veya şifre hatalı!!" });
            }

            var userDto = _mapper.Map<UserDto>(user);
            return ServiceResponse<UserDto>.ReturnResultWith200(userDto);
        }
    }
}
