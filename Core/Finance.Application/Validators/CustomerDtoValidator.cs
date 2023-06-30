using Finance.Application.Dtos;
using Finance.Application.Repositories;
using FluentValidation;

namespace Finance.Application.Validators
{
    public class CustomerDtoValidator : AbstractValidator<CustomerDto>
    {

        public CustomerDtoValidator()
        {
            RuleFor(x => x.Name)
              .NotEmpty()
              .Length(3, 50);


            RuleFor(x => x.TaxOffice)
              .NotEmpty()
              .Length(3, 50);

            RuleFor(x => x.TaxNumber)
             .NotEmpty()
             .Length(10);


        }

       
    }
}
