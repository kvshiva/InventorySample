using Common;
using Newtonsoft.Json;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using InventorySampleServer.Api._Base;
using Model.Custom.State;
using Model.Custom.Other;
using InventorySampleServer.Model.Store.InventoryVoucher;
using InventorySampleServer.BLL.Store.InventoryVoucher;

namespace InventorySampleServer.Api._Gen.Store
{
	public class GInventoryVoucherController : BaseController
	{
		public GInventoryVoucherController(IConfiguration Configuration) : base(Configuration) { }

		public override async Task<ActionResult<ResultDto>> GetById(int Id)
		{
			#region GetById
			var BLL = new InventoryVoucherBLL<HddInventoryVoucherDto>(ConnectionString, CClaim);
			var Result = await BLL.GetById(Id);
			return Ok(Result);
			#endregion
		}

		public override async Task<ActionResult<ResultDto>> GetList(string? SearchValue = null, string? SortField = null, string? Direction = null, int? PageNumber = null, int? PageSize = null, int? Language = null, bool? EditMode = null)
		{
			#region GetList
			var BLL = new InventoryVoucherBLL<InventoryVoucherListDto>(ConnectionString, CClaim);
			var Result = await BLL.GetList(SearchValue, SortField, Direction, PageNumber, PageSize, Language, EditMode);
			return Ok(Result);
			#endregion
		}

		public override async Task<ActionResult<ResultDto>> Add()
		{
			#region Add
			var Entity = JsonConvert.DeserializeObject<HddInventoryVoucherDto>(Request.Form["Data"]) ?? new HddInventoryVoucherDto();

			var BLL = new InventoryVoucherBLL<HddInventoryVoucherDto>(ConnectionString, CClaim);
			var Result = await BLL.Add(Entity);
			return Ok(Result);
			#endregion
		}

		public override async Task<ActionResult<ResultDto>> Edit()
		{
			#region Edit
			var Entity = JsonConvert.DeserializeObject<HddInventoryVoucherDto>(Request.Form["Data"]) ?? new HddInventoryVoucherDto();

			var BLL = new InventoryVoucherBLL<HddInventoryVoucherDto>(ConnectionString, CClaim);
			var Result = await BLL.Edit(Entity);
			return Ok(Result);
			#endregion
		}

		public override async Task<ActionResult<ResultDto>> Delete(int Id)
		{
			#region Delete
			var BLL = new InventoryVoucherBLL<InventoryVoucherEntity>(ConnectionString, CClaim);
			var Result = await BLL.Delete(Id);
			return Ok(Result);
			#endregion
		}

		[HttpGet("GetListByInventoryVoucherSpecificationId")]
		public virtual async Task<ActionResult<ResultDto>> GetListByInventoryVoucherSpecificationId(int InventoryVoucherSpecificationId, string? SearchValue = null, string? SortField = null, string? Direction = null, int? PageNumber = null, int? PageSize = null, int? Language = null, bool? EditMode = null)
		{
			#region GetListByInventoryVoucherSpecificationId
			var BLL = new InventoryVoucherBLL<InventoryVoucherListDto>(ConnectionString, CClaim);
			var Result = await BLL.GetListByInventoryVoucherSpecificationId(InventoryVoucherSpecificationId, SearchValue, SortField, Direction, PageNumber, PageSize, Language, EditMode);
			return Ok(Result);
			#endregion
		}

		[HttpGet("GetInventoryVoucherStoreList")]
		public virtual async Task<ActionResult<ResultDto>> GetInventoryVoucherStoreList(bool? EditMode = null)
		{
			#region GetInventoryVoucherStoreList
			var BLL = new InventoryVoucherBLL<dynamic>(ConnectionString, CClaim);
			var Result = await BLL.GetInventoryVoucherStoreList(EditMode);
			return Ok(Result);
			#endregion
		}

		[HttpGet("GetInventoryVoucherInventoryVoucherSpecificationList")]
		public virtual async Task<ActionResult<ResultDto>> GetInventoryVoucherInventoryVoucherSpecificationList(bool? EditMode = null)
		{
			#region GetInventoryVoucherInventoryVoucherSpecificationList
			var BLL = new InventoryVoucherBLL<dynamic>(ConnectionString, CClaim);
			var Result = await BLL.GetInventoryVoucherInventoryVoucherSpecificationList(EditMode);
			return Ok(Result);
			#endregion
		}

		[HttpGet("GetInventoryVoucherUserList")]
		public virtual async Task<ActionResult<ResultDto>> GetInventoryVoucherUserList(bool? EditMode = null)
		{
			#region GetInventoryVoucherUserList
			var BLL = new InventoryVoucherBLL<dynamic>(ConnectionString, CClaim);
			var Result = await BLL.GetInventoryVoucherUserList(EditMode);
			return Ok(Result);
			#endregion
		}

		[HttpGet("GetInventoryVoucherEntityEnumList")]
		public virtual async Task<ActionResult<ResultDto>> GetInventoryVoucherEntityEnumList(bool? EditMode = null)
		{
			#region GetInventoryVoucherEntityEnumList
			var BLL = new InventoryVoucherBLL<dynamic>(ConnectionString, CClaim);
			var Result = await BLL.GetInventoryVoucherEntityEnumList(EditMode);
			return Ok(Result);
			#endregion
		}

		[HttpGet("GetInventoryVoucherStateEnumList")]
		public virtual async Task<ActionResult<ResultDto>> GetInventoryVoucherStateEnumList(bool? EditMode = null)
		{
			#region GetInventoryVoucherStateEnumList
			var BLL = new InventoryVoucherBLL<dynamic>(ConnectionString, CClaim);
			var Result = await BLL.GetInventoryVoucherStateEnumList(EditMode);
			return Ok(Result);
			#endregion
		}

		[HttpGet("GetInventoryVoucherItemPartList")]
		public virtual async Task<ActionResult<ResultDto>> GetInventoryVoucherItemPartList(bool? EditMode = null)
		{
			#region GetInventoryVoucherItemPartList
			var BLL = new InventoryVoucherBLL<Model.Part.Part.PartListDto>(ConnectionString, CClaim);
			var Result = await BLL.GetInventoryVoucherItemPartList(EditMode);
			return Ok(Result);
			#endregion
		}

		[HttpGet("GetInventoryVoucherItemSerialInventoryVoucherItemList")]
		public virtual async Task<ActionResult<ResultDto>> GetInventoryVoucherItemSerialInventoryVoucherItemList(bool? EditMode = null)
		{
			#region GetInventoryVoucherItemSerialInventoryVoucherItemList
			var BLL = new InventoryVoucherBLL<Model.Store.InventoryVoucherItem.InventoryVoucherItemListDto>(ConnectionString, CClaim);
			var Result = await BLL.GetInventoryVoucherItemSerialInventoryVoucherItemList(EditMode);
			return Ok(Result);
			#endregion
		}

		[HttpGet("GetLastStateById")]
		public virtual async Task<ActionResult<ResultDto>> GetLastStateById(int Id)
		{
			#region GetLastStateById
			var BLL = new InventoryVoucherBLL<LastStateDto>(ConnectionString, CClaim);
			var Result = await BLL.GetLastStateById(Id);
			return Ok(Result);
			#endregion
		}
	}
}
