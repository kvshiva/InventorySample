using Model;

namespace InventorySampleServer.Model.State.StateMachine
{
	public class StateMachineListDto : BaseVersionModel
	{
		public string Title { get; set; }  = null!;
		public int EntityId { get; set; } 
		public string EntityTitle { get; set; } = null!;
		public int? StateMachineTypeEnumId { get; set; }
		public int ItemCount { get; set; }
		public bool Editable { get; set; }
	}
}
