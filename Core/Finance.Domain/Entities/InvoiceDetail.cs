﻿using Finance.Domain.Entities.Common;
using Finance.Domain.Entities.Identity;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.ComponentModel.DataAnnotations.Schema;

namespace Finance.Domain.Entities
{
    public class InvoiceDetail : BaseEntity
    {

        public int InvoiceId { get; set; }
        public Invoice Invoice { get; set; }


        public int StockId { get; set; }
        public Stock Stock { get; set; }

        [Column(TypeName = "decimal(18, 2)")]
        public decimal UnitPrice { get; set; }


        [Column(TypeName = "decimal(18, 2)")]
        public decimal Price { get; set; }

        [Column(TypeName = "decimal(18, 2)")]
        public decimal Quantity { get; set; }
    }
}
    