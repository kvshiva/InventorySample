using Model;

namespace InventorySampleServer.Model.Store.InventoryVoucherItemSerial
{
	public class InventoryVoucherItemSerialListDto : BaseVersionModel
	{
		public int InventoryVoucherItemId { get; set; } 
		public string SerialNo { get; set; }  = null!;
		public decimal Value1 { get; set; } 
		public decimal? Value2 { get; set; }
		public string? Comment { get; set; }
		public string? SystemComment { get; set; }
		public string? JsonField { get; set; }
		public int ItemCount { get; set; }
		public bool Editable { get; set; }
	}
}
