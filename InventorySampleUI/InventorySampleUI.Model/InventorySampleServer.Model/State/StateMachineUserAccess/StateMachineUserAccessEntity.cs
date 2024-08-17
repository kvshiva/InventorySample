using Model;

namespace InventorySampleServer.Model.State.StateMachineUserAccess
{
	public class StateMachineUserAccessEntity : BaseVersionModel
	{
		public int StateTransitionId { get; set; } 
		public int EntityEnumId { get; set; } 
		public int? UserId { get; set; }
		public int? OperationRoleTypeEnumId { get; set; }
	}
}
