using InventorySampleServer.Api._Gen.Part;
using Microsoft.Extensions.Configuration;

namespace InventorySampleServer.Api.Controllers.Part
{
	public class CountUnitController : GCountUnitController
	{
		public CountUnitController(IConfiguration Configuration) : base(Configuration) { }
	}
}
