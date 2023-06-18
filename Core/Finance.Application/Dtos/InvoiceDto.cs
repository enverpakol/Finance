﻿using Finance.Domain.Entities.Identity;
using Finance.Domain.Entities;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.ComponentModel.DataAnnotations.Schema;

namespace Finance.Application.Dtos
{
    public class InvoiceDto
    {
        public int Id { get; set; }
        public string No { get; set; }
        public int ClientId { get; set; }
        public string ClientName { get; set; }

        [Column(TypeName = "decimal(18, 2)")]
        public decimal TotalAmount { get; set; }
        public ICollection<InvoiceDetailDto> Details { get; set; }
    }
}