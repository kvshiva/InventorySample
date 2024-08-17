using Model;

namespace InventorySampleServer.Model.State.EntityEnum
{
	public class EntityEnumEntity : BaseVersionModel
	{
		public string Title { get; set; }  = null!;
		public string? EntitySchema { get; set; }
		public string? Prefix { get; set; }
		public int? CounterLength { get; set; }
	}
}
