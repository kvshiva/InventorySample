using Model;

namespace InventorySampleServer.Model.General.OperationRoleTypeEnum
{
	public class OperationRoleTypeEnumListDto : BaseVersionModel
	{
		public string Title { get; set; }  = null!;
		public int ItemCount { get; set; }
		public bool Editable { get; set; }
	}
}
