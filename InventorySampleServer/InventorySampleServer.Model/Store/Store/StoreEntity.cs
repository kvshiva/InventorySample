using Model;

namespace InventorySampleServer.Model.Store.Store
{
	public class StoreEntity : BaseVersionModel
	{
		public string Title { get; set; }  = null!;
		public string Code { get; set; }  = null!;
		public string? Comment { get; set; }
		public bool Disabled { get; set; } 
		public string? Jsonfield { get; set; }
		public int StoreTypeEnumId { get; set; } 
	}
}
