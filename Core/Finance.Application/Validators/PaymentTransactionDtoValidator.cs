using Finance.Application.Dtos;
using Finance.Application.Repositories;
using FluentValidation;

namespace Finance.Application.Validators
{
    public class PaymentTransactionDtoValidator : AbstractValidator<PaymentTransactionDto>
    {
        private readonly IAppUserRepository _repoAppUser;
        public PaymentTransactionDtoValidator(IAppUserRepository repoAppUser)
        {
            _repoAppUser = repoAppUser;

            RuleFor(x => x.Price)
              .NotNull()
              .GreaterThan(0)
              .NotEmpty();


            RuleFor(x => x.ClientId)
              .NotEmpty()
              .NotNull()
              .Must(ValidateClient).WithMessage("2");
        }

        private bool ValidateClient(int clientId)
        {
            var activeUser = _repoAppUser.GetActiveUser().Result;
            var clientUser = _repoAppUser.GetItemAsync(clientId).Result;
            if (activeUser.CompanyId != clientUser.CompanyId)
            {
                return false;
            }
            return true;
        }
    }
}
