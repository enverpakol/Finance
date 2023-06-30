using Finance.Domain.Entities.Common;
using Finance.Domain.Entities.Identity;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.ComponentModel.DataAnnotations.Schema;

namespace Finance.Domain.Entities
{
    public class PaymentTransaction : BaseEntity
    {
        [Column(TypeName = "decimal(18, 2)")]
        public decimal Price { get; set; }

        public int CustomerId { get; set; }
        public Customer Customer { get; set; }


        public int? InvoiceId { get; set; }
        public Invoice Invoice { get; set; }
    }
}
