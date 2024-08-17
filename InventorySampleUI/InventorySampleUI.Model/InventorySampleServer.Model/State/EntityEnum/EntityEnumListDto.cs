using Model;

namespace InventorySampleServer.Model.State.EntityEnum
{
	public class EntityEnumListDto : BaseVersionModel
	{
		public string Title { get; set; }  = null!;
		public string? EntitySchema { get; set; }
		public string? Prefix { get; set; }
		public int? CounterLength { get; set; }
		public int ItemCount { get; set; }
		public bool Editable { get; set; }
	}
}
