using Finance.Application.Dtos;
using Finance.Application.Repositories;
using Finance.Domain.Entities.Identity;
using FluentValidation;
using Microsoft.AspNetCore.Identity;

namespace Finance.Application.Validators
{
    public class InvoiceDtoValidator : AbstractValidator<InvoiceDto>
    {
        private readonly IAppUserRepository _repoAppUser;
        private readonly UserManager<AppUser> _userManager;
        public InvoiceDtoValidator(IAppUserRepository repoAppUser, UserManager<AppUser> userManager)
        {
            _repoAppUser = repoAppUser;
            _userManager = userManager;


            RuleFor(x => x.ClientId)
              .NotEmpty()
              .Must(ValidateClient).WithMessage("2");

            RuleFor(x => x.Details)
             .NotNull();
        }

        private bool ValidateClient(int clientId)
        {
            var activeUser = _repoAppUser.GetActiveUser().Result;
            var clientUser = _repoAppUser.GetItemAsync(clientId).Result;
            if (activeUser.CompanyId != clientUser.CompanyId)
                return false;

            if (_userManager.IsInRoleAsync(clientUser, "client").Result == false)
                return false;



            return true;
        }
    }
}
