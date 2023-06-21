using Finance.Domain.Entities;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.ComponentModel.DataAnnotations.Schema;

namespace Finance.Application.Dtos
{
    public class StockTransactionDto
    {
        public int Id { get; set; }

        [Column(TypeName = "decimal(18, 2)")]
        public decimal Quantity { get; set; }

        public int StockId { get; set; }
        public string StockName { get; set; }
        public int? InvoiceId { get; set; }
        public string InvoiceNo { get; set; }
    }
}
