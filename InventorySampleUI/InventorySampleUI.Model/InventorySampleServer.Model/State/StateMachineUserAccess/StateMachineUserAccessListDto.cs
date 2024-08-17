using Model;

namespace InventorySampleServer.Model.State.StateMachineUserAccess
{
	public class StateMachineUserAccessListDto : BaseVersionModel
	{
		public int StateTransitionId { get; set; } 
		public string StateTransitionTitle { get; set; } = null!;
		public int EntityEnumId { get; set; } 
		public string EntityEnumTitle { get; set; } = null!;
		public int? UserId { get; set; }
		public string UserFullName { get; set; } = null!;
		public int? OperationRoleTypeEnumId { get; set; }
		public string OperationRoleTypeEnumTitle { get; set; } = null!;
		public int ItemCount { get; set; }
		public bool Editable { get; set; }
	}
}
