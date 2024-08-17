using Model;

namespace InventorySampleServer.Model.Store.InventoryVoucherSpecificationTypeEnum
{
	public class InventoryVoucherSpecificationTypeEnumEntity : BaseVersionModel
	{
		public string Title { get; set; }  = null!;
		public int? Ratio { get; set; }
	}
}
