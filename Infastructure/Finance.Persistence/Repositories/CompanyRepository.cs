using Finance.Application.Repositories;
using Finance.Domain.Entities;
using Finance.Persistence.Contexts;

namespace Finance.Persistence.Repositories
{
    public class CompanyRepository : Repository<Company>, ICompanyRepository
    {
        public CompanyRepository(AppData context) : base(context)
        {

        }
    }
}
