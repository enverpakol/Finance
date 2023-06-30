using Finance.Domain.Entities.Identity;
using Finance.Domain.Entities;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.ComponentModel.DataAnnotations.Schema;
using Finance.Domain.Entities.Enums;

namespace Finance.Application.Dtos
{
    public class InvoiceDto
    {
        public int Id { get; set; }
        public string No { get; set; }
        public int CustomerId { get; set; }
        public string CustomerName { get; set; }
        public string CompanyName { get; set; }

        [Column(TypeName = "decimal(18, 2)")]
        public decimal TotalAmount { get; set; }
        public InvoicePaymentEnum InvoicePaymentEnum { get; set; }
        public ICollection<InvoiceDetailDto> Details { get; set; }
    }
}
