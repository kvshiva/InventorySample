using Model;

namespace InventorySampleServer.Model.Store.InventoryVoucherSpecification
{
	public class InventoryVoucherSpecificationListDto : BaseVersionModel
	{
		public string Title { get; set; }  = null!;
		public string? Comment { get; set; }
		public int InventoryVoucherSpecificationTypeEnumId { get; set; } 
		public string InventoryVoucherSpecificationTypeEnumTitle { get; set; } = null!;
		public int? ReceiptInventoryVoucherSpecificationId { get; set; }
		public string ReceiptInventoryVoucherSpecificationTitle { get; set; } = null!;
		public int? RemittanceInventoryVoucherSpecificationId { get; set; }
		public string RemittanceInventoryVoucherSpecificationTitle { get; set; } = null!;
		public bool IsSystemic { get; set; } 
		public string? Jsonfield { get; set; }
		public int ItemCount { get; set; }
		public bool Editable { get; set; }
	}
}
