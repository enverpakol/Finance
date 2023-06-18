using Finance.Application.Repositories;
using Finance.Domain.Entities;
using Finance.Domain.Entities.Identity;
using Finance.Persistence.Contexts;
using Microsoft.AspNetCore.Identity;
using System.Linq.Expressions;

namespace Finance.Persistence.Repositories
{
    public class InvoiceRepository : Repository<Invoice>, IInvoiceRepository
    {

        private readonly IAppUserRepository _userRepository;
        public InvoiceRepository(AppData context,  IAppUserRepository userRepository) : base(context)
        {
            _userRepository = userRepository;
        }

    
        
    }
}
