using Model;

namespace InventorySampleServer.Model.Part.PartStore
{
	public class PartStoreListDto : BaseVersionModel
	{
		public int PartId { get; set; } 
		public string PartTitle { get; set; } = null!;
		public int? StoreId { get; set; }
		public string StoreTitle { get; set; } = null!;
		public bool IsActive { get; set; } 
		public bool IsDefault { get; set; } 
		public string? Comment { get; set; }
		public int ItemCount { get; set; }
		public bool Editable { get; set; }
	}
}
