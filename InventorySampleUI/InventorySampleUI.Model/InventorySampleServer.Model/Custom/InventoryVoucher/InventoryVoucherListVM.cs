using Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using InventorySampleServer.Model.Store.InventoryVoucher;
using InventorySampleServer.Model.Store.InventoryVoucherItem;

namespace InventorySampleServer.Model.Custom.InventoryVoucher
{
    public class InventoryVoucherListVM : InventoryVoucherListDto
    {
        public string InventoryVoucherSpecificationTypeEnumTitle { get; set; } = string.Empty;
    }
}
