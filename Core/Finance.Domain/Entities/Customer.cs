using Finance.Domain.Entities.Common;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.ComponentModel.DataAnnotations.Schema;

namespace Finance.Domain.Entities
{
    public class Customer : BaseEntity
    {
        public string Name { get; set; }
        public string TaxNumber { get; set; }
        public string TaxOffice { get; set; }

        public int CompanyId { get; set; }
        public Company Company { get; set; }


        [Column(TypeName = "decimal(18, 2)")]
        public decimal BalanceDebt { get; set; }
        public ICollection<Invoice> Invoices { get; set; }
    }
}
