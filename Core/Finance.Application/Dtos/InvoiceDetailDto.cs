using Finance.Domain.Entities;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.ComponentModel.DataAnnotations.Schema;

namespace Finance.Application.Dtos
{
    public class InvoiceDetailDto
    {
        public int Id { get; set; }
        public int StockId{ get; set; }
        public string StockName { get; set; }


        [Column(TypeName = "decimal(18, 2)")]
        public decimal Price { get; set; }

        [Column(TypeName = "decimal(18, 2)")]
        public decimal Quantity { get; set; }
    }
}
