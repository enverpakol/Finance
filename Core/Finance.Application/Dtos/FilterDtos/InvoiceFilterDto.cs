using Finance.Application.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Finance.Application.Dtos.FilterDtos
{
    public class InvoiceFilterDto: ListRequestDto
    {
        public int? CustomerId{ get; set; }
        public DateTime BeginDate { get; set; }
        public DateTime EndDate { get; set; }
        public string InvoiceNo{ get; set; }
    }
}
