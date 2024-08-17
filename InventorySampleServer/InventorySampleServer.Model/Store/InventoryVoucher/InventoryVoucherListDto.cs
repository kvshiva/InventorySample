using Model;

namespace InventorySampleServer.Model.Store.InventoryVoucher
{
	public class InventoryVoucherListDto : BaseVersionModel
	{
		public string InventoryVoucherNo { get; set; }  = null!;
		public DateTime? DateTime { get; set; }
		public string PersianDate { get; set; }  = null!;
		public string Time { get; set; }  = null!;
		public string? Comment { get; set; }
		public string? SystemComment { get; set; }
		public int StoreId { get; set; } 
		public string StoreTitle { get; set; } = null!;
		public int InventoryVoucherSpecificationId { get; set; } 
		public string InventoryVoucherSpecificationTitle { get; set; } = null!;
		public int UserId { get; set; } 
		public string UserFullName { get; set; } = null!;
		public string? JsonField { get; set; }
		public int? BaseEntity { get; set; }
		public string BaseEntityTitle { get; set; } = null!;
		public int? BaseEntityRef { get; set; }
		public int? StateEnumId { get; set; }
		public string StateEnumTitle { get; set; } = null!;
		public int ItemCount { get; set; }
		public bool Editable { get; set; }
	}
}
