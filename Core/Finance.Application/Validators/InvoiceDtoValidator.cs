using Finance.Application.Dtos;
using Finance.Application.Repositories;
using Finance.Domain.Entities.Identity;
using FluentValidation;
using Microsoft.AspNetCore.Identity;

namespace Finance.Application.Validators
{
    public class InvoiceDtoValidator : AbstractValidator<InvoiceDto>
    {
        private readonly ICustomerRepository _repoCustomer;
        private readonly IAppUserRepository _repoAppUser;
        private readonly UserManager<AppUser> _userManager;
        public InvoiceDtoValidator(UserManager<AppUser> userManager, ICustomerRepository repoCustomer, IAppUserRepository repoAppUser)
        {
            _userManager = userManager;
            _repoCustomer = repoCustomer;
            _repoAppUser = repoAppUser;


            RuleFor(x => x.CustomerId)
              .NotEmpty()
              .Must(ValidateClient).WithMessage("2");

            RuleFor(x => x.Details)
             .NotNull();
        }

        private bool ValidateClient(int customerId)
        {
            var activeUser = _repoAppUser.GetActiveUser().Result;
            var customer = _repoCustomer.GetItemAsync(customerId).Result;
            if (activeUser.CompanyId != customer.CompanyId)
                return false;

            return true;
        }
    }
}
