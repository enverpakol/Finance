using Finance.Application.Dtos;
using Finance.Application.Repositories;
using FluentValidation;

namespace Finance.Application.Validators
{
    public class PaymentTransactionDtoValidator : AbstractValidator<PaymentTransactionDto>
    {
        private readonly IAppUserRepository _repoAppUser;
        private readonly ICustomerRepository _repoCustomer;
        public PaymentTransactionDtoValidator(IAppUserRepository repoAppUser, ICustomerRepository repoCustomer)
        {
            _repoAppUser = repoAppUser;
            _repoCustomer = repoCustomer;

            RuleFor(x => x.Price)
              .NotNull()
              .GreaterThan(0)
              .NotEmpty();


            RuleFor(x => x.CustomerId)
              .NotEmpty()
              .NotNull()
              .Must(ValidateClient).WithMessage("2");
        }

        private bool ValidateClient(int clientId)
        {
            var activeUser = _repoAppUser.GetActiveUser().Result;
            var customer = _repoCustomer.GetItemAsync(clientId).Result;
            if (activeUser.CompanyId != customer.CompanyId)
            {
                return false;
            }

            return true;
        }
    }
}
