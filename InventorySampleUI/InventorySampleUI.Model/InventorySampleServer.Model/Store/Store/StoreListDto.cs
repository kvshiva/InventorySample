using Model;

namespace InventorySampleServer.Model.Store.Store
{
	public class StoreListDto : BaseVersionModel
	{
		public string Title { get; set; }  = null!;
		public string Code { get; set; }  = null!;
		public string? Comment { get; set; }
		public bool Disabled { get; set; } 
		public string? Jsonfield { get; set; }
		public int StoreTypeEnumId { get; set; } 
		public string StoreTypeEnumTitle { get; set; } = null!;
		public int ItemCount { get; set; }
		public bool Editable { get; set; }
	}
}
