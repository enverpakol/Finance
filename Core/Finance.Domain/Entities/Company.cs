using Finance.Domain.Entities.Common;

namespace Finance.Domain.Entities
{
    public class Company : BaseEntity
    {
        public string Name { get; set; }
        public string TaxNumber { get; set; }
        public string TaxOffice { get; set; }
        public string PhoneNumber { get; set; }
            
        public ICollection<Stock> Stocks { get; set; }
    }
}
