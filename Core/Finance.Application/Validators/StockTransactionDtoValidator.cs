using Finance.Application.Dtos;
using Finance.Application.Repositories;
using FluentValidation;

namespace Finance.Application.Validators
{
    public class StockTransactionDtoValidator : AbstractValidator<StockTransactionDto>
    {
        private readonly IStockRepository _repoStock;
        private readonly IAppUserRepository _repoAppUser;
        public StockTransactionDtoValidator(IStockRepository repoStock, IAppUserRepository repoAppUser)
        {
            _repoStock = repoStock;
            _repoAppUser = repoAppUser;

            RuleFor(x => x.Quantity)
              .NotNull()
              .GreaterThan(0)
              .NotEmpty();


            RuleFor(x => x.StockId)
              .NotEmpty()
              .NotNull()
              .Must(ValidateStock).WithMessage("2");
        }


        private bool ValidateStock(int stockId)
        {
            var activeUser = _repoAppUser.GetActiveUser().Result;
            var stock = _repoStock.GetItemAsync(stockId).Result;
            if (activeUser.CompanyId != stock.CompanyId)
            {
                return false;
            }
            return true;
        }
    }
}
