using FluentValidation;
using Panteon.Business.Command.User;
using Panteon.Repository.Users;
using System;

namespace Panteon.Businnes.Validation.Users
{
    public class AddUserCommandValidator : AbstractValidator<AddUserCommand>
    {
        private readonly IUserRepository _userRepository;

        public AddUserCommandValidator(IUserRepository userRepository)
        {
            _userRepository = userRepository;

            RuleFor(x => x.Username)
                .NotEmpty().WithMessage("Kullanıcı adı boş geçilemez.")
                .Must(BeUniqueUsername).WithMessage("Kullanıcı adı daha önce alınmış");
            RuleFor(x => x.Email)
                .NotEmpty().WithMessage("E-mail boş geçilemez .")
                .EmailAddress().WithMessage("Lütfen E-mail tipinde giriniz.")
                .Must(BeUniqueEmail).WithMessage("E-Mail adresi daha önce kullanılmış.");

            RuleFor(x => x.Password)
                .NotEmpty().WithMessage("Şifre alanı boş geçilemez.")
                .MinimumLength(4).WithMessage("Şifreniz en az 4 karakterden oluşmalıdır.");
        }

        private bool BeUniqueUsername(string username)
        {
            return _userRepository.IsUsernameUnique(username);
        }

        private bool BeUniqueEmail(string email)
        {
            return _userRepository.IsEmailUnique(email);
        }
    }
}
