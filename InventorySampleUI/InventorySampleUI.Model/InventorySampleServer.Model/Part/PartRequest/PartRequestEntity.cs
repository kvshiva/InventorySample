using Model;

namespace InventorySampleServer.Model.Part.PartRequest
{
	public class PartRequestEntity : BaseVersionModel
	{
		public int StoreId { get; set; } 
		public string NeedDate { get; set; }  = null!;
		public DateTime? DateTime { get; set; }
		public int UserId { get; set; } 
		public string PartRequestNo { get; set; }  = null!;
		public int? StateEnumId { get; set; }
		public string? Comment { get; set; }
		public string? SystemComment { get; set; }
		public string PersianDate { get; set; }  = null!;
		public string Time { get; set; }  = null!;
		public string? JsonField { get; set; }
	}
}
