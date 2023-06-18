using Finance.Application.Dtos;
using FluentValidation;

namespace Finance.Application.Validators
{
    public class UserLoginDtoValidator : AbstractValidator<UserLoginDto>
    {
        public UserLoginDtoValidator()
        {
            RuleFor(x => x.UserName)
                .NotEmpty()
                .EmailAddress()
                .Length(3, 50);

            RuleFor(x => x.Password)
                .NotEmpty()
                .Length(6, 30);

        }
    }
}
