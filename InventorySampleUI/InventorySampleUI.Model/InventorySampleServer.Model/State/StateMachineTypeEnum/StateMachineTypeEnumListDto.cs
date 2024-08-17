using Model;

namespace InventorySampleServer.Model.State.StateMachineTypeEnum
{
	public class StateMachineTypeEnumListDto : BaseVersionModel
	{
		public string Title { get; set; }  = null!;
		public int ItemCount { get; set; }
		public bool Editable { get; set; }
	}
}
