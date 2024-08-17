using Model;

namespace InventorySampleServer.Model.Store.TransferVoucher
{
	public class TransferVoucherListDto : BaseVersionModel
	{
		public int SourceStoreId { get; set; } 
		public string SourceStoreTitle { get; set; } = null!;
		public int TargetStoreId { get; set; } 
		public string TargetStoreTitle { get; set; } = null!;
		public int? SourceInventoryVoucherId { get; set; }
		public int? TargetInventoryVoucherId { get; set; }
		public int InventoryVoucherSpecificationId { get; set; } 
		public string InventoryVoucherSpecificationTitle { get; set; } = null!;
		public string TransferVoucherNo { get; set; }  = null!;
		public string? Comment { get; set; }
		public string? JsonField { get; set; }
		public int UserId { get; set; } 
		public string UserFullName { get; set; } = null!;
		public int? StateEnumId { get; set; }
		public string StateEnumTitle { get; set; } = null!;
		public int ItemCount { get; set; }
		public bool Editable { get; set; }
	}
}
