using Common;
using Newtonsoft.Json;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using InventorySampleServer.Api._Base;
using Model.Custom.Other;
using InventorySampleServer.Model.Store.Store;
using InventorySampleServer.BLL.Store.Store;

namespace InventorySampleServer.Api._Gen.Store
{
	public class GStoreController : BaseController
	{
		public GStoreController(IConfiguration Configuration) : base(Configuration) { }

		public override async Task<ActionResult<ResultDto>> GetById(int Id)
		{
			#region GetById
			var BLL = new StoreBLL<StoreListDto>(ConnectionString, CClaim);
			var Result = await BLL.GetById(Id);
			return Ok(Result);
			#endregion
		}

		public override async Task<ActionResult<ResultDto>> GetList(string? SearchValue = null, string? SortField = null, string? Direction = null, int? PageNumber = null, int? PageSize = null, int? Language = null, bool? EditMode = null)
		{
			#region GetList
			var BLL = new StoreBLL<StoreListDto>(ConnectionString, CClaim);
			var Result = await BLL.GetList(SearchValue, SortField, Direction, PageNumber, PageSize, Language, EditMode);
			return Ok(Result);
			#endregion
		}

		public override async Task<ActionResult<ResultDto>> Add()
		{
			#region Add
			var Entity = JsonConvert.DeserializeObject<StoreEntity>(Request.Form["Data"]) ?? new StoreEntity();

			var BLL = new StoreBLL<StoreEntity>(ConnectionString, CClaim);
			var Result = await BLL.Add(Entity);
			return Ok(Result);
			#endregion
		}

		public override async Task<ActionResult<ResultDto>> Edit()
		{
			#region Edit
			var Entity = JsonConvert.DeserializeObject<StoreEntity>(Request.Form["Data"]) ?? new StoreEntity();

			var BLL = new StoreBLL<StoreEntity>(ConnectionString, CClaim);
			var Result = await BLL.Edit(Entity);
			return Ok(Result);
			#endregion
		}

		public override async Task<ActionResult<ResultDto>> Delete(int Id)
		{
			#region Delete
			var BLL = new StoreBLL<StoreEntity>(ConnectionString, CClaim);
			var Result = await BLL.Delete(Id);
			return Ok(Result);
			#endregion
		}

		[HttpGet("GetStoreStoreTypeEnumList")]
		public virtual async Task<ActionResult<ResultDto>> GetStoreStoreTypeEnumList(bool? EditMode = null)
		{
			#region GetStoreStoreTypeEnumList
			var BLL = new StoreBLL<dynamic>(ConnectionString, CClaim);
			var Result = await BLL.GetStoreStoreTypeEnumList(EditMode);
			return Ok(Result);
			#endregion
		}

	}
}
