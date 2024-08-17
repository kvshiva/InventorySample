using Common;
using Newtonsoft.Json;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using InventorySampleServer.Api._Base;
using InventorySampleServer.Model.Part.Category;
using InventorySampleServer.BLL.Part.Category;

namespace InventorySampleServer.Api._Gen.Part
{
	public class GCategoryController : BaseController
	{
		public GCategoryController(IConfiguration Configuration) : base(Configuration) { }

		public override async Task<ActionResult<ResultDto>> GetById(int Id)
		{
			#region GetById
			var BLL = new CategoryBLL<CategoryListDto>(ConnectionString, CClaim);
			var Result = await BLL.GetById(Id);
			return Ok(Result);
			#endregion
		}

		public override async Task<ActionResult<ResultDto>> GetList(string? SearchValue = null, string? SortField = null, string? Direction = null, int? PageNumber = null, int? PageSize = null, int? Language = null, bool? EditMode = null)
		{
			#region GetList
			var BLL = new CategoryBLL<CategoryListDto>(ConnectionString, CClaim);
			var Result = await BLL.GetList(SearchValue, SortField, Direction, PageNumber, PageSize, Language, EditMode);
			return Ok(Result);
			#endregion
		}

		public override async Task<ActionResult<ResultDto>> Add()
		{
			#region Add
			var Entity = JsonConvert.DeserializeObject<CategoryEntity>(Request.Form["Data"]) ?? new CategoryEntity();

			var BLL = new CategoryBLL<CategoryEntity>(ConnectionString, CClaim);
			var Result = await BLL.Add(Entity);
			return Ok(Result);
			#endregion
		}

		public override async Task<ActionResult<ResultDto>> Edit()
		{
			#region Edit
			var Entity = JsonConvert.DeserializeObject<CategoryEntity>(Request.Form["Data"]) ?? new CategoryEntity();

			var BLL = new CategoryBLL<CategoryEntity>(ConnectionString, CClaim);
			var Result = await BLL.Edit(Entity);
			return Ok(Result);
			#endregion
		}

		public override async Task<ActionResult<ResultDto>> Delete(int Id)
		{
			#region Delete
			var BLL = new CategoryBLL<CategoryEntity>(ConnectionString, CClaim);
			var Result = await BLL.Delete(Id);
			return Ok(Result);
			#endregion
		}

	}
}
