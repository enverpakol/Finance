using Finance.Application.Exceptions;
using Finance.Application.Repositories;
using Finance.Domain.Entities;
using Finance.Domain.Entities.Enums;
using Finance.Domain.Entities.Identity;
using Finance.Persistence.Contexts;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace Finance.Persistence.Repositories
{
    public class InvoiceRepository : Repository<Invoice>, IInvoiceRepository
    {

        private readonly IAppUserRepository _userRepository;
        private readonly IStockRepository _stockRepository;
        private readonly IStockTransactionRepository _stockTransactionRepository;
        private readonly IPaymentTransactionRepository _paymentTransactionRepository;
        public InvoiceRepository(AppData context, IAppUserRepository userRepository, IStockRepository stockRepository, IStockTransactionRepository stockTransactionRepository, IPaymentTransactionRepository paymentTransactionRepository) : base(context)
        {
            _userRepository = userRepository;
            _stockRepository = stockRepository;
            _stockTransactionRepository = stockTransactionRepository;
            _paymentTransactionRepository = paymentTransactionRepository;
        }


        public override IQueryable<Invoice> GetList(Expression<Func<Invoice, bool>> filter = null)
        {
            var activeUser= _userRepository.GetActiveUser().Result;
            var query = base.GetList(filter)
                .Include(x=>x.Customer)
                .Where(x=>x.Customer.CompanyId==activeUser.CompanyId);

            return query;
        }

        public override async Task<bool> CreateAsync(Invoice item)
        {
            var activeUser = await _userRepository.GetActiveUser();
            item.No = await GetInvoiceNo();
            foreach (var detail in item.InvoiceDetails)
            {
                var stock = await _stockRepository.GetItemAsync(detail.StockId);
                if (stock == null)
                    throw new NotFoundException($"stock {detail.Id}");
                detail.UnitPrice = stock.Price;
                detail.Price = stock.Price * detail.Quantity;
            }

            item.TotalAmount = item.InvoiceDetails.Sum(x => x.Price);

            var result = await base.CreateAsync(item);

            foreach (var detail in item.InvoiceDetails)
            {
                _ = await _stockTransactionRepository.CreateAsync(new()
                {
                    StockId = detail.StockId,
                    Quantity = detail.Quantity * -1,
                    InvoiceId = item.Id,
                });
            }

            if (item.InvoicePaymentEnum == InvoicePaymentEnum.BALANCE)
            {
                _ = await _paymentTransactionRepository.CreateAsync(new()
                {
                    CustomerId = item.CustomerId,
                    Price = item.TotalAmount * -1,
                    InvoiceId = item.Id,
                });
            }
            return result;
        }

        public async Task<string> GetInvoiceNo()
        {
            var lastInvoice = await Table.OrderByDescending(x => x.Id).FirstOrDefaultAsync();
            if (lastInvoice != null)
            {
                int.TryParse(lastInvoice.No, out int lastInvoiceNo);

                return (lastInvoiceNo + 1).ToString();
            }
            else
                return "10000";
        }

        public override Task<Invoice> GetItemAsync(int id)
        {
            return Table
                .Include(x => x.Customer).ThenInclude(x=>x.Company)
                .Include(x => x.InvoiceDetails).ThenInclude(x => x.Stock)
                .FirstOrDefaultAsync(x => x.Id == id);
        }
    }
}
