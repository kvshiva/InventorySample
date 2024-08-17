using Model;

namespace InventorySampleServer.Model.Part.Part
{
	public class PartEntity : BaseVersionModel
	{
		public string Title { get; set; }  = null!;
		public string Code { get; set; }  = null!;
		public int MainCountUnitId { get; set; } 
		public int? SecondaryCountUnitId { get; set; }
		public int CategoryId { get; set; } 
		public bool HasSerial { get; set; } 
	}
}
