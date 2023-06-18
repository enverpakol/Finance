using Finance.Application.Dtos;
using FluentValidation;

namespace Finance.Application.Validators
{
    public class StockDtoValidator : AbstractValidator<StockDto>
    {
        public StockDtoValidator()
        {
            RuleFor(x => x.Name)
              .NotEmpty()
              .Length(3, 50);


            RuleFor(x => x.Code)
              .NotEmpty()
              .Length(3, 50);


        }
    }
}
