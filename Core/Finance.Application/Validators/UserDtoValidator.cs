using Finance.Application.Dtos;
using FluentValidation;

namespace Finance.Application.Validators
{
    public class UserDtoValidator : AbstractValidator<UserDto>
    {
        public UserDtoValidator()
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
                .NotEmpty()
                .Length(6, 30);

            RuleFor(x => x.ConfirmPassword)
                .NotEmpty()
                .Equal(x => x.Password)
                .Length(6, 30);

            RuleFor(x => x.CompanyId)
                .NotNull();

        }
    }
}
