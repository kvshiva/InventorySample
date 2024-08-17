using Model;

namespace InventorySampleServer.Model.Store.InventoryVoucherItem
{
	public class InventoryVoucherItemEntity : BaseVersionModel
	{
		public int InventoryVoucherId { get; set; } 
		public int PartId { get; set; } 
		public decimal Value1 { get; set; } 
		public decimal? Value2 { get; set; }
		public string? Comment { get; set; }
		public string? SystemComment { get; set; }
		public string? JsonField { get; set; }
	}
}
