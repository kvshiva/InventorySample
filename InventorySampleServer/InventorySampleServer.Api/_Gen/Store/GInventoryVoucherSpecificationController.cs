using Common;
using Newtonsoft.Json;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using InventorySampleServer.Api._Base;
using Model.Custom.Other;
using InventorySampleServer.Model.Store.InventoryVoucherSpecification;
using InventorySampleServer.BLL.Store.InventoryVoucherSpecification;

namespace InventorySampleServer.Api._Gen.Store
{
	public class GInventoryVoucherSpecificationController : BaseController
	{
		public GInventoryVoucherSpecificationController(IConfiguration Configuration) : base(Configuration) { }

		public override async Task<ActionResult<ResultDto>> GetById(int Id)
		{
			#region GetById
			var BLL = new InventoryVoucherSpecificationBLL<InventoryVoucherSpecificationListDto>(ConnectionString, CClaim);
			var Result = await BLL.GetById(Id);
			return Ok(Result);
			#endregion
		}

		public override async Task<ActionResult<ResultDto>> GetList(string? SearchValue = null, string? SortField = null, string? Direction = null, int? PageNumber = null, int? PageSize = null, int? Language = null, bool? EditMode = null)
		{
			#region GetList
			var BLL = new InventoryVoucherSpecificationBLL<InventoryVoucherSpecificationListDto>(ConnectionString, CClaim);
			var Result = await BLL.GetList(SearchValue, SortField, Direction, PageNumber, PageSize, Language, EditMode);
			return Ok(Result);
			#endregion
		}

		public override async Task<ActionResult<ResultDto>> Add()
		{
			#region Add
			var Entity = JsonConvert.DeserializeObject<InventoryVoucherSpecificationEntity>(Request.Form["Data"]) ?? new InventoryVoucherSpecificationEntity();

			var BLL = new InventoryVoucherSpecificationBLL<InventoryVoucherSpecificationEntity>(ConnectionString, CClaim);
			var Result = await BLL.Add(Entity);
			return Ok(Result);
			#endregion
		}

		public override async Task<ActionResult<ResultDto>> Edit()
		{
			#region Edit
			var Entity = JsonConvert.DeserializeObject<InventoryVoucherSpecificationEntity>(Request.Form["Data"]) ?? new InventoryVoucherSpecificationEntity();

			var BLL = new InventoryVoucherSpecificationBLL<InventoryVoucherSpecificationEntity>(ConnectionString, CClaim);
			var Result = await BLL.Edit(Entity);
			return Ok(Result);
			#endregion
		}

		public override async Task<ActionResult<ResultDto>> Delete(int Id)
		{
			#region Delete
			var BLL = new InventoryVoucherSpecificationBLL<InventoryVoucherSpecificationEntity>(ConnectionString, CClaim);
			var Result = await BLL.Delete(Id);
			return Ok(Result);
			#endregion
		}

		[HttpGet("GetListByInventoryVoucherSpecificationTypeEnumId")]
		public virtual async Task<ActionResult<ResultDto>> GetListByInventoryVoucherSpecificationTypeEnumId(int InventoryVoucherSpecificationTypeEnumId, string? SearchValue = null, string? SortField = null, string? Direction = null, int? PageNumber = null, int? PageSize = null, int? Language = null, bool? EditMode = null)
		{
			#region GetListByInventoryVoucherSpecificationTypeEnumId
			var BLL = new InventoryVoucherSpecificationBLL<InventoryVoucherSpecificationListDto>(ConnectionString, CClaim);
			var Result = await BLL.GetListByInventoryVoucherSpecificationTypeEnumId(InventoryVoucherSpecificationTypeEnumId, SearchValue, SortField, Direction, PageNumber, PageSize, Language, EditMode);
			return Ok(Result);
			#endregion
		}

		[HttpGet("GetInventoryVoucherSpecificationInventoryVoucherSpecificationTypeEnumList")]
		public virtual async Task<ActionResult<ResultDto>> GetInventoryVoucherSpecificationInventoryVoucherSpecificationTypeEnumList(bool? EditMode = null)
		{
			#region GetInventoryVoucherSpecificationInventoryVoucherSpecificationTypeEnumList
			var BLL = new InventoryVoucherSpecificationBLL<dynamic>(ConnectionString, CClaim);
			var Result = await BLL.GetInventoryVoucherSpecificationInventoryVoucherSpecificationTypeEnumList(EditMode);
			return Ok(Result);
			#endregion
		}

		[HttpGet("GetInventoryVoucherSpecificationInventoryVoucherSpecificationList")]
		public virtual async Task<ActionResult<ResultDto>> GetInventoryVoucherSpecificationInventoryVoucherSpecificationList(bool? EditMode = null)
		{
			#region GetInventoryVoucherSpecificationInventoryVoucherSpecificationList
			var BLL = new InventoryVoucherSpecificationBLL<dynamic>(ConnectionString, CClaim);
			var Result = await BLL.GetInventoryVoucherSpecificationInventoryVoucherSpecificationList(EditMode);
			return Ok(Result);
			#endregion
		}

	}
}
