using Model;

namespace InventorySampleServer.Model.State.StateEnum
{
	public class StateEnumListDto : BaseVersionModel
	{
		public string Title { get; set; }  = null!;
		public int ItemCount { get; set; }
		public bool Editable { get; set; }
	}
}
