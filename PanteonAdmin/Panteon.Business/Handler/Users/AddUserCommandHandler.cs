using AutoMapper;
using MediatR;
using Panteon.Entities.Entities;
using Panteon.Repository.Users;
using Panteon.Helper;
using Microsoft.Extensions.Logging;
using Panteon.Business.Command.User;
using Panteon.DataAcces.UnitOfWork;
using Panteon.Entities.Dto;
using System;
using System.Threading;
using System.Threading.Tasks;
using FluentValidation;
using System.Linq;
using System.Collections.Generic;

namespace Panteon.Businnes.Handlers.Users
{
    public class AddUserCommandHandler : IRequestHandler<AddUserCommand, ServiceResponse<UserDto>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IUserRepository _userRepository;
        private readonly IMapper _mapper;
        private readonly ILogger<AddUserCommandHandler> _logger;
        private readonly IValidator<AddUserCommand> _validator;

        public AddUserCommandHandler(
            IUnitOfWork unitOfWork,
            IUserRepository userRepository,
            IMapper mapper,
            ILogger<AddUserCommandHandler> logger,
            IValidator<AddUserCommand> validator)
        {
            _unitOfWork = unitOfWork;
            _userRepository = userRepository;
            _mapper = mapper;
            _logger = logger;
            _validator = validator;
        }

        public async Task<ServiceResponse<UserDto>> Handle(AddUserCommand request, CancellationToken cancellationToken)
        {
            var validationResult = await _validator.ValidateAsync(request, cancellationToken);
            if (!validationResult.IsValid)
            {
                var errors = validationResult.Errors.Select(e => e.ErrorMessage).ToList();
                return ServiceResponse<UserDto>.ReturnError(errors);
            }

            var entity = _mapper.Map<User>(request);

            string salt;
            entity.Password = PasswordHasher.HashPassword(request.Password);

            _userRepository.Add(entity);

            try
            {
                if (await _unitOfWork.SaveAsync() <= 0)
                {
                    _logger.LogError("Error saving user to the database.");
                    return ServiceResponse<UserDto>.Return500();
                }

                var entityDto = _mapper.Map<UserDto>(entity);
                return ServiceResponse<UserDto>.ReturnResultWith200(entityDto);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while saving the user.");
                return ServiceResponse<UserDto>.Return500(ex.Message);
            }
        }
    }
}
