using Finance.Application.Dtos;
using FluentValidation;

namespace Finance.Application.Validators
{
    public class InvoiceDtoValidator : AbstractValidator<InvoiceDto>
    {
        public InvoiceDtoValidator()
        {
            RuleFor(x => x.ClientId)
              .NotEmpty();


            RuleFor(x => x.Details)
             .NotNull();


        }
    }
}
