using Model;

namespace InventorySampleServer.Model.Store.StoreTypeEnum
{
	public class StoreTypeEnumListDto : BaseVersionModel
	{
		public string Title { get; set; }  = null!;
		public int ItemCount { get; set; }
		public bool Editable { get; set; }
	}
}
