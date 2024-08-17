using Model;

namespace InventorySampleServer.Model.Part.Category
{
	public class CategoryListDto : BaseVersionModel
	{
		public string Title { get; set; }  = null!;
		public int ItemCount { get; set; }
		public bool Editable { get; set; }
	}
}
