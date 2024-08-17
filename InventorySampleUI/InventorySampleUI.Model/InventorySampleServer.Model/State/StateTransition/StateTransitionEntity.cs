using Model;

namespace InventorySampleServer.Model.State.StateTransition
{
	public class StateTransitionEntity : BaseVersionModel
	{
		public string Title { get; set; }  = null!;
		public string? ConfirmMessage { get; set; }
		public bool GetConfirm { get; set; } 
		public bool IsAutomatic { get; set; } 
		public bool CommentNeeded { get; set; } 
		public int SourceStateId { get; set; } 
		public int TargetStateId { get; set; } 
	}
}
