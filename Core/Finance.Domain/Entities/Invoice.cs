using Finance.Domain.Entities.Common;
using Finance.Domain.Entities.Enums;
using Finance.Domain.Entities.Identity;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.ComponentModel.DataAnnotations.Schema;

namespace Finance.Domain.Entities
{
    public class Invoice : BaseEntity
    {
        public string No { get; set; }
        public int CustomerId { get; set; }
        public Customer Customer { get; set; }

        [Column(TypeName = "decimal(18, 2)")]
        public decimal TotalAmount { get; set; }



        public InvoicePaymentEnum InvoicePaymentEnum { get; set; }
        public ICollection<InvoiceDetail> InvoiceDetails { get; set; }
        public ICollection<StockTransaction> StockTransactions { get; set; }
    }
}
