using Model;

namespace InventorySampleServer.Model.State.StateMachine
{
	public class StateMachineEntity : BaseVersionModel
	{
		public string Title { get; set; }  = null!;
		public int EntityId { get; set; } 
		public int? StateMachineTypeEnumId { get; set; }
	}
}
