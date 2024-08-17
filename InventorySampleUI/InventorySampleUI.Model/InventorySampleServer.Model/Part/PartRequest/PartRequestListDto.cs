using Model;

namespace InventorySampleServer.Model.Part.PartRequest
{
	public class PartRequestListDto : BaseVersionModel
	{
		public int StoreId { get; set; } 
		public string StoreTitle { get; set; } = null!;
		public string NeedDate { get; set; }  = null!;
		public DateTime? DateTime { get; set; }
		public int UserId { get; set; } 
		public string UserFullName { get; set; } = null!;
		public string PartRequestNo { get; set; }  = null!;
		public int? StateEnumId { get; set; }
		public string StateEnumTitle { get; set; } = null!;
		public string? Comment { get; set; }
		public string? SystemComment { get; set; }
		public string PersianDate { get; set; }  = null!;
		public string Time { get; set; }  = null!;
		public string? JsonField { get; set; }
		public int ItemCount { get; set; }
		public bool Editable { get; set; }
	}
}
