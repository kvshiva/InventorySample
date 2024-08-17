using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InventorySampleServer.Model.Custom.Other
{
    public class GetListParamsDto
    {
        public int EntityId { get; set; }
        public string? SearchValue { get; set; } = null;
        public string? SortField { get; set; } = null;
        public string? Direction { get; set; }
        public int PageNumber { get; set; } = 1;
        public int PageSize { get; set; } = int.MaxValue;
        public int Offset { get; set; } = 0;
        bool? EditMode = null;
        //public FiltersEnum? FilterEnumId { get; set; }
        public int? FilterId { get; set; }
    }
}
