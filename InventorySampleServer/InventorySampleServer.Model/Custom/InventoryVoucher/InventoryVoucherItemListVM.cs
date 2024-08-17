using Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using InventorySampleServer.Model.Store.InventoryVoucherItem;

namespace InventorySampleServer.Model.Custom.InventoryVoucher
{
    public class InventoryVoucherItemListVM : InventoryVoucherItemListDto
    {
        public string Unit1 { get; set; } = string.Empty;
        public string Unit2 { get; set; } = string.Empty;
        public bool HasSerial { get; set; }
    }
}
