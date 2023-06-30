using Finance.Application.Dtos;
using Finance.Application.Dtos.FilterDtos;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Finance.Application.Validators.FilterDtosValidator
{

    public class InvoiceFilterDtoValidator : AbstractValidator<InvoiceFilterDto>
    {

        public InvoiceFilterDtoValidator()
        {


            RuleFor(x => x.BeginDate)
              .Must(DateCheck).WithMessage("The begin date cannot be greater than the end date !")
              .Must(MaxDateDiff).WithMessage("You can filter for a maximum of 60 days !")
              .NotEmpty();

            RuleFor(x => x.EndDate)
              .NotEmpty();



        }
        private bool DateCheck(InvoiceFilterDto item, DateTime BeginDate)
        {
            if(item.EndDate.Date<BeginDate) 
                return false;

            return true;
        }
        private bool MaxDateDiff(InvoiceFilterDto item, DateTime BeginDate)
        {
            var dif=item.EndDate - BeginDate;
            if (dif.TotalDays>60)
                return false;

            return true;
        }
    }
}
