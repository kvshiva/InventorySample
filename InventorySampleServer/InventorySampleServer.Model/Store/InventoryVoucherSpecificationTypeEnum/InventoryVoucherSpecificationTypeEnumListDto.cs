using Model;

namespace InventorySampleServer.Model.Store.InventoryVoucherSpecificationTypeEnum
{
	public class InventoryVoucherSpecificationTypeEnumListDto : BaseVersionModel
	{
		public string Title { get; set; }  = null!;
		public int? Ratio { get; set; }
		public int ItemCount { get; set; }
		public bool Editable { get; set; }
	}
}
