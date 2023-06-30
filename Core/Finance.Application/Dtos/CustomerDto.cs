using Finance.Domain.Entities;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.ComponentModel.DataAnnotations.Schema;

namespace Finance.Application.Dtos
{
    public class CustomerDto
    {
        public int Id { get; set; }

        public string Name { get; set; }
        public string TaxNumber { get; set; }
        public string TaxOffice { get; set; }


        //public int CompanyId { get; set; }
        //public string CompanyName { get; set; }


        [Column(TypeName = "decimal(18, 2)")]
        public decimal BalanceDebt { get; set; }
    }
}
