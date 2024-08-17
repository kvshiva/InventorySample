using Model;

namespace InventorySampleServer.Model.Part.CountUnit
{
	public class CountUnitListDto : BaseVersionModel
	{
		public string Title { get; set; }  = null!;
		public int ItemCount { get; set; }
		public bool Editable { get; set; }
	}
}
