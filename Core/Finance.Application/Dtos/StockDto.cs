using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.ComponentModel.DataAnnotations.Schema;

namespace Finance.Application.Dtos
{
    public class StockDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Code { get; set; }

        [Column(TypeName = "decimal(18, 2)")]
        public decimal Balance { get; set; }

        [Column(TypeName = "decimal(18, 2)")]
        public decimal  Price { get; set; }

    }
}
