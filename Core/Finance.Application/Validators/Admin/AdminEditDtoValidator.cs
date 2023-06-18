using Finance.Application.Dtos;
using FluentValidation;

namespace Finance.Application.Validators
{
    public class AdminEditDtoValidator : AbstractValidator<AdminEditDto>
    {
        public AdminEditDtoValidator()
        {
            RuleFor(x => x.UserName)
                .NotEmpty()
                .EmailAddress()
                .Length(3, 50);

            RuleFor(x => x.FirstName)
                .NotEmpty()
                .Length(3, 50);

            RuleFor(x => x.LastName)
                .NotEmpty()
                .Length(3, 50);

            RuleFor(x => x.Password)
                .NotEmpty().When(x => x.Id == 0)
                .Length(6, 30).When(x => x.Id == 0);

            RuleFor(x => x.ConfirmPassword)
            .NotEmpty().When(x => x.Id == 0)
            .Equal(x => x.Password).When(x => x.Id == 0)
            .Length(6, 30).When(x => x.Id == 0);

        }
    }
}
