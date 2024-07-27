using FluentValidation;
using Panteon.Business.Command.Buildings;

namespace Panteon.Business.Validation.Buildings
{
    public class AddBuildingsValidator : AbstractValidator<AddBuildCommand>
    {
        public AddBuildingsValidator()
        {
            RuleFor(x => x.BuildingType)
                .NotEmpty().WithMessage("Bina türü boş geçilemez.");

            RuleFor(x => x.BuildingCost)
                .GreaterThan(0).WithMessage("Bina maliyeti sıfırdan büyük olmalıdır.");

            RuleFor(x => x.ConstructionTime)
                .InclusiveBetween(29, 1801).WithMessage("Yapım süresi 30 ile 1800 saniye arasında ve tam sayı olmalıdır.");
        }
    }
}
