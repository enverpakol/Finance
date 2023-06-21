using Finance.Application.Dtos;
using FluentValidation;

namespace Finance.Application.Validators
{
    public class StockTransactionDtoValidator : AbstractValidator<StockTransactionDto>
    {
        public StockTransactionDtoValidator()
        {
            RuleFor(x => x.Quantity)
              .NotNull()
              .GreaterThan(0)
              .NotEmpty();


            RuleFor(x => x.StockId)
              .NotEmpty()
              .NotNull();

        }
    }
}
