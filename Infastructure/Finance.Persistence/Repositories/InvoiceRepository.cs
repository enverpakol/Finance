﻿using Finance.Application.Repositories;
using Finance.Domain.Entities;
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
        public InvoiceRepository(AppData context, IAppUserRepository userRepository, IStockRepository stockRepository) : base(context)
        {
            _userRepository = userRepository;
            _stockRepository = stockRepository;
        }




        public override async Task<bool> CreateAsync(Invoice item)
        {
            var activeUser = await _userRepository.GetActiveUser();
            item.No = await GetInvoiceNo();
            foreach (var detail in item.InvoiceDetails)
            {
                var stock = await _stockRepository.GetItemAsync(detail.StockId);
                detail.UnitPrice = stock.Price;
                detail.Price = stock.Price * detail.Quantity;
            }

            item.TotalAmount = item.InvoiceDetails.Sum(x => x.Price);


            return await base.CreateAsync(item);
        }

        public async Task<string> GetInvoiceNo()
        {
            var lastInvoice=await Table.OrderByDescending(x=>x.Id).FirstOrDefaultAsync();
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
                .Include(x=>x.Client)
                .Include(x => x.InvoiceDetails).ThenInclude(x => x.Stock)
                .FirstOrDefaultAsync(x => x.Id == id);
        }
    }
}
