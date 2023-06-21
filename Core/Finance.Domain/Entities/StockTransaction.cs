using Finance.Domain.Entities.Common;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.ComponentModel.DataAnnotations.Schema;

namespace Finance.Domain.Entities
{
    public class StockTransaction : BaseEntity
    {
        [Column(TypeName = "decimal(18, 2)")]
        public decimal Quantity { get; set; }

        public int StockId { get; set; }
        public Stock Stock { get; set; }





        public int? InvoiceId{ get; set; }
        public Invoice Invoice { get; set; }
    }
}
