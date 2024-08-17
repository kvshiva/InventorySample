using Model;

namespace InventorySampleServer.Model.State.State
{
	public class StateListDto : BaseVersionModel
	{
		public int StateMachineId { get; set; } 
		public string StateMachineTitle { get; set; } = null!;
		public bool? CanEdit { get; set; }
		public bool? CanDelete { get; set; }
		public bool? IsStartState { get; set; }
		public bool? IsFinishState { get; set; }
		public int StateEnumId { get; set; } 
		public string StateEnumTitle { get; set; } = null!;
		public int StateOrder { get; set; } 
		public int ItemCount { get; set; }
		public bool Editable { get; set; }
	}
}
