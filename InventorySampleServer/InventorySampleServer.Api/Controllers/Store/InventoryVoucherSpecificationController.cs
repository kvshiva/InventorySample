using InventorySampleServer.Api._Gen.Store;
using Microsoft.Extensions.Configuration;

namespace InventorySampleServer.Api.Controllers.Store
{
	public class InventoryVoucherSpecificationController : GInventoryVoucherSpecificationController
	{
		public InventoryVoucherSpecificationController(IConfiguration Configuration) : base(Configuration) { }
	}
}
