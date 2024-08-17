using InventorySampleServer.Api._Gen.Store;
using Microsoft.Extensions.Configuration;

namespace InventorySampleServer.Api.Controllers.Store
{
	public class StoreController : GStoreController
	{
		public StoreController(IConfiguration Configuration) : base(Configuration) { }
	}
}
