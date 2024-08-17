using Model;

namespace InventorySampleServer.Model.General.UserOperationRole
{
	public class UserOperationRoleEntity : BaseVersionModel
	{
		public bool? IsActive { get; set; }
		public int UserId { get; set; } 
		public int OperationRoleTypeEnumId { get; set; } 
	}
}
