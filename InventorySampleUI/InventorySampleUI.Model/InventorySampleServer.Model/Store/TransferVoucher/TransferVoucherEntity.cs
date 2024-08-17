using Model;

namespace InventorySampleServer.Model.Store.TransferVoucher
{
	public class TransferVoucherEntity : BaseVersionModel
	{
		public int SourceStoreId { get; set; } 
		public int TargetStoreId { get; set; } 
		public int? SourceInventoryVoucherId { get; set; }
		public int? TargetInventoryVoucherId { get; set; }
		public int InventoryVoucherSpecificationId { get; set; } 
		public string TransferVoucherNo { get; set; }  = null!;
		public string? Comment { get; set; }
		public string? JsonField { get; set; }
		public int UserId { get; set; } 
		public int? StateEnumId { get; set; }
	}
}
