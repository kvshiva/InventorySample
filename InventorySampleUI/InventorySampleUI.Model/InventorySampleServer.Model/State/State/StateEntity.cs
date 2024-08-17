using Model;

namespace InventorySampleServer.Model.State.State
{
	public class StateEntity : BaseVersionModel
	{
		public int StateMachineId { get; set; } 
		public bool? CanEdit { get; set; }
		public bool? CanDelete { get; set; }
		public bool? IsStartState { get; set; }
		public bool? IsFinishState { get; set; }
		public int StateEnumId { get; set; } 
		public int StateOrder { get; set; } 
	}
}
