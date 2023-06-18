using Finance.Domain.Entities.Common;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.ComponentModel.DataAnnotations.Schema;

namespace Finance.Domain.Entities
{
    public class Stock : BaseEntity
    {
        public string Name { get; set; }
        public string Code { get; set; }


        [Column(TypeName = "decimal(18, 2)")]
        public decimal Balance { get; set; }

        [Column(TypeName = "decimal(18, 2)")]
        public decimal Price { get; set; }

        public int CompanyId { get; set; }
        public Company Company { get; set; }


        public ICollection<InvoiceDetail> InvoiceDetails { get; set; }
    }
}
