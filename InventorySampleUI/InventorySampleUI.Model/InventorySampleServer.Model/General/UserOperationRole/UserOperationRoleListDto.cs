using Model;

namespace InventorySampleServer.Model.General.UserOperationRole
{
	public class UserOperationRoleListDto : BaseVersionModel
	{
		public bool? IsActive { get; set; }
		public int UserId { get; set; } 
		public string UserFullName { get; set; } = null!;
		public int OperationRoleTypeEnumId { get; set; } 
		public string OperationRoleTypeEnumTitle { get; set; } = null!;
		public int ItemCount { get; set; }
		public bool Editable { get; set; }
	}
}
