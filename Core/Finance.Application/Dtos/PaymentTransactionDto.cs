using Finance.Domain.Entities;
using Finance.Domain.Entities.Identity;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.ComponentModel.DataAnnotations.Schema;

namespace Finance.Application.Dtos
{
    public class PaymentTransactionDto
    {
        public int Id { get; set; }

        [Column(TypeName = "decimal(18, 2)")]
        public decimal Price { get; set; }

        public int ClientId { get; set; }
        public string ClientName { get; set; }



        public int? InvoiceId { get; set; }
        public string InvoiceNo { get; set; }
    }
}
