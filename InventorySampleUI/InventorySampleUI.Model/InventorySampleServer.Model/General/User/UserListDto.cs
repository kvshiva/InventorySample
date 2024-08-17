using Model;

namespace InventorySampleServer.Model.General.User
{
	public class UserListDto : BaseVersionModel
	{
		public string FullName { get; set; }  = null!;
		public string? Picture { get; set; }
		public string Guid { get; set; }  = null!;
		public bool IsImage { get; set; }
		public int Code { get; set; } 
		public string Mobile { get; set; }  = null!;
		public int ItemCount { get; set; }
		public bool Editable { get; set; }
	}
}
