using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InventorySampleServer.Model.Custom.Part
{
    public class PartQuantityDto
    {
        public string Title { get; set; } = string.Empty;
        public bool HasSerial { get; set; }
        public string MainCountUnitTitle { get; set; } = string.Empty;
        public string? SecondaryCountUnitTitle { get; set; }
        public decimal PartValue1 { get; set; }
        public decimal PartValue2 { get; set; }
        public string?  SerialNo { get; set; }
        public decimal? SerialValue1 { get; set; }
        public decimal? SerialValue2 { get; set; }
    }
}
