using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using InventorySampleServer.Model.Store.InventoryVoucher;
using InventorySampleServer.Model.Store.TransferVoucher;


namespace InventorySampleServer.Model.Custom.TransferVoucher
{
    public class TransferVoucherDetailedEntity : TransferVoucherEntity
    {
        public HddInventoryVoucherDto RemittanceEntity { get; set; } = new HddInventoryVoucherDto();
    }
}
