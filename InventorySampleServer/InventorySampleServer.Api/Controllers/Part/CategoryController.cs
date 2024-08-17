using InventorySampleServer.Api._Gen.Part;
using Microsoft.Extensions.Configuration;

namespace InventorySampleServer.Api.Controllers.Part
{
	public class CategoryController : GCategoryController
	{
		public CategoryController(IConfiguration Configuration) : base(Configuration) { }
	}
}
