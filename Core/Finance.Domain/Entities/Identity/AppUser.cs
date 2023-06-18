using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.ComponentModel.DataAnnotations.Schema;

namespace Finance.Domain.Entities.Identity
{
    public class AppUser : IdentityUser<int>
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public int? CompanyId { get; set; }
        public Company Company { get; set; }




        [Column(TypeName = "decimal(18, 2)")]
        public decimal BalanceDebt { get; set; }


        
        public ICollection<Invoice> Invoices { get; set; }



        public string Name => $"{FirstName} {LastName}";
    }
}
