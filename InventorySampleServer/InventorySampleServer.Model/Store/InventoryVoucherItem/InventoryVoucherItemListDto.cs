using Model;

namespace InventorySampleServer.Model.Store.InventoryVoucherItem
{
	public class InventoryVoucherItemListDto : BaseVersionModel
	{
		public int InventoryVoucherId { get; set; } 
		public int PartId { get; set; } 
		public string PartTitle { get; set; } = null!;
		public decimal Value1 { get; set; } 
		public decimal? Value2 { get; set; }
		public string? Comment { get; set; }
		public string? SystemComment { get; set; }
		public string? JsonField { get; set; }
		public int ItemCount { get; set; }
		public bool Editable { get; set; }
	}
}
