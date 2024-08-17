using Model;

namespace InventorySampleServer.Model.Store.InventoryVoucher
{
	public class InventoryVoucherEntity : BaseVersionModel
	{
		public string InventoryVoucherNo { get; set; }  = null!;
		public DateTime? DateTime { get; set; }
		public string PersianDate { get; set; }  = null!;
		public string Time { get; set; }  = null!;
		public string? Comment { get; set; }
		public string? SystemComment { get; set; }
		public int StoreId { get; set; } 
		public int InventoryVoucherSpecificationId { get; set; } 
		public int UserId { get; set; } 
		public string? JsonField { get; set; }
		public int? BaseEntity { get; set; }
		public int? BaseEntityRef { get; set; }
		public int? StateEnumId { get; set; }
	}
}
