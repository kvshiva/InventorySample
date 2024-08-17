using Model;

namespace InventorySampleServer.Model.Store.InventoryVoucherSpecification
{
	public class InventoryVoucherSpecificationEntity : BaseVersionModel
	{
		public string Title { get; set; }  = null!;
		public string? Comment { get; set; }
		public int InventoryVoucherSpecificationTypeEnumId { get; set; } 
		public int? ReceiptInventoryVoucherSpecificationId { get; set; }
		public int? RemittanceInventoryVoucherSpecificationId { get; set; }
		public bool IsSystemic { get; set; } 
		public string? Jsonfield { get; set; }
	}
}
