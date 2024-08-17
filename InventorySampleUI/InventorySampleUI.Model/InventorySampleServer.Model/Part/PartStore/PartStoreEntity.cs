using Model;

namespace InventorySampleServer.Model.Part.PartStore
{
	public class PartStoreEntity : BaseVersionModel
	{
		public int PartId { get; set; } 
		public int? StoreId { get; set; }
		public bool IsActive { get; set; } 
		public bool IsDefault { get; set; } 
		public string? Comment { get; set; }
	}
}
