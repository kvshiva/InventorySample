using Common;
using Newtonsoft.Json;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using InventorySampleServer.Api._Base;
using Model.Custom.Other;
using InventorySampleServer.Model.Part.Part;
using InventorySampleServer.BLL.Part.Part;

namespace InventorySampleServer.Api._Gen.Part
{
	public class GPartController : BaseController
	{
		public GPartController(IConfiguration Configuration) : base(Configuration) { }

		public override async Task<ActionResult<ResultDto>> GetById(int Id)
		{
			#region GetById
			var BLL = new PartBLL<HdPartDto>(ConnectionString, CClaim);
			var Result = await BLL.GetById(Id);
			return Ok(Result);
			#endregion
		}

		public override async Task<ActionResult<ResultDto>> GetList(string? SearchValue = null, string? SortField = null, string? Direction = null, int? PageNumber = null, int? PageSize = null, int? Language = null, bool? EditMode = null)
		{
			#region GetList
			var BLL = new PartBLL<PartListDto>(ConnectionString, CClaim);
			var Result = await BLL.GetList(SearchValue, SortField, Direction, PageNumber, PageSize, Language, EditMode);
			return Ok(Result);
			#endregion
		}

		public override async Task<ActionResult<ResultDto>> Add()
		{
			#region Add
			var Entity = JsonConvert.DeserializeObject<HdPartDto>(Request.Form["Data"]) ?? new HdPartDto();

			var BLL = new PartBLL<HdPartDto>(ConnectionString, CClaim);
			var Result = await BLL.Add(Entity);
			return Ok(Result);
			#endregion
		}

		public override async Task<ActionResult<ResultDto>> Edit()
		{
			#region Edit
			var Entity = JsonConvert.DeserializeObject<HdPartDto>(Request.Form["Data"]) ?? new HdPartDto();

			var BLL = new PartBLL<HdPartDto>(ConnectionString, CClaim);
			var Result = await BLL.Edit(Entity);
			return Ok(Result);
			#endregion
		}

		public override async Task<ActionResult<ResultDto>> Delete(int Id)
		{
			#region Delete
			var BLL = new PartBLL<PartEntity>(ConnectionString, CClaim);
			var Result = await BLL.Delete(Id);
			return Ok(Result);
			#endregion
		}

		[HttpGet("GetPartCountUnitList")]
		public virtual async Task<ActionResult<ResultDto>> GetPartCountUnitList(bool? EditMode = null)
		{
			#region GetPartCountUnitList
			var BLL = new PartBLL<dynamic>(ConnectionString, CClaim);
			var Result = await BLL.GetPartCountUnitList(EditMode);
			return Ok(Result);
			#endregion
		}

		[HttpGet("GetPartCategoryList")]
		public virtual async Task<ActionResult<ResultDto>> GetPartCategoryList(bool? EditMode = null)
		{
			#region GetPartCategoryList
			var BLL = new PartBLL<dynamic>(ConnectionString, CClaim);
			var Result = await BLL.GetPartCategoryList(EditMode);
			return Ok(Result);
			#endregion
		}

		[HttpGet("GetPartStoreStoreList")]
		public virtual async Task<ActionResult<ResultDto>> GetPartStoreStoreList(bool? EditMode = null)
		{
			#region GetPartStoreStoreList
			var BLL = new PartBLL<Model.Store.Store.StoreListDto>(ConnectionString, CClaim);
			var Result = await BLL.GetPartStoreStoreList(EditMode);
			return Ok(Result);
			#endregion
		}

	}
}
