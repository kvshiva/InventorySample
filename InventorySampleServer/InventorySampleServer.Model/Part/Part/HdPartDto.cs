using Model;

namespace InventorySampleServer.Model.Part.Part
{
	public class HdPartDto : BaseVersionModel
	{
		public string Title { get; set; }  = null!;
		public string Code { get; set; }  = null!;
		public int MainCountUnitId { get; set; } 
		public string MainCountUnitTitle { get; set; } = null!;
		public int? SecondaryCountUnitId { get; set; }
		public string SecondaryCountUnitTitle { get; set; } = null!;
		public int CategoryId { get; set; } 
		public string CategoryTitle { get; set; } = null!;
		public bool HasSerial { get; set; } 
		public List<Model.Part.PartStore.HdPartStoreListDto>? PartStoreList { get; set; }
	}
}
