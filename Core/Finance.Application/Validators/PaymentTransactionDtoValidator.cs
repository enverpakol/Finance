using Finance.Application.Dtos;
using FluentValidation;

namespace Finance.Application.Validators
{
    public class PaymentTransactionDtoValidator : AbstractValidator<PaymentTransactionDto>
    {
        public PaymentTransactionDtoValidator()
        {
            RuleFor(x => x.Price)
              .NotNull()
              .GreaterThan(0)
              .NotEmpty();


            RuleFor(x => x.ClientId)
              .NotEmpty()
              .NotNull();

        }
    }
}
