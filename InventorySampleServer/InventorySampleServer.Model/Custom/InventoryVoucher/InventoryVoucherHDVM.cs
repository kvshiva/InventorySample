using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InventorySampleServer.Model.Custom.InventoryVoucher
{
    public class InventoryVoucherHDVM
    {
        public InventoryVoucherListVM Voucher { get; set; } = null!;
        public List<InventoryVoucherItemListVM>? Items { get; set; }
    }
}
