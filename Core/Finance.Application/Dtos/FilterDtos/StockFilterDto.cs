using Finance.Application.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Finance.Application.Dtos.FilterDtos
{
    public class StockFilterDto : ListRequestDto
    {
        public string Name{ get; set; }
        public string Code{ get; set; }
    }
}
