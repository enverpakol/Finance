using Finance.Application.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Finance.Application.Dtos.FilterDtos
{
    public class CustomerFilterDto : ListRequestDto
    {
        public string Name{ get; set; }
        public string TaxNumber{ get; set; }
        public string TaxOffice{ get; set; }
    }
}
