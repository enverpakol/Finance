using Finance.Domain.Entities.Common;
using Finance.Domain.Entities.Identity;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.ComponentModel.DataAnnotations.Schema;

namespace Finance.Domain.Entities
{
    public class Invoice : BaseEntity
    {
        public string No { get; set; }
        public int ClientId { get; set; }
        [ForeignKey(nameof(ClientId))]
        public AppUser AppUser { get; set; }
        public decimal TotalAmount { get; set; }
        public ICollection<InvoiceDetail> InvoiceDetails { get; set; }
    }
}
