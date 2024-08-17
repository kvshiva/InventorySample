using Common;
using Newtonsoft.Json;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using InventorySampleServer.Api._Base;
using Model.Custom.State;
using Model.Custom.Other;
using InventorySampleServer.Model.Store.TransferVoucher;
using InventorySampleServer.BLL.Store.TransferVoucher;

namespace InventorySampleServer.Api._Gen.Store
{
	public class GTransferVoucherController : BaseController
	{
		public GTransferVoucherController(IConfiguration Configuration) : base(Configuration) { }

		public override async Task<ActionResult<ResultDto>> GetById(int Id)
		{
			#region GetById
			var BLL = new TransferVoucherBLL<TransferVoucherListDto>(ConnectionString, CClaim);
			var Result = await BLL.GetById(Id);
			return Ok(Result);
			#endregion
		}

		public override async Task<ActionResult<ResultDto>> GetList(string? SearchValue = null, string? SortField = null, string? Direction = null, int? PageNumber = null, int? PageSize = null, int? Language = null, bool? EditMode = null)
		{
			#region GetList
			var BLL = new TransferVoucherBLL<TransferVoucherListDto>(ConnectionString, CClaim);
			var Result = await BLL.GetList(SearchValue, SortField, Direction, PageNumber, PageSize, Language, EditMode);
			return Ok(Result);
			#endregion
		}

		public override async Task<ActionResult<ResultDto>> Add()
		{
			#region Add
			var Entity = JsonConvert.DeserializeObject<TransferVoucherEntity>(Request.Form["Data"]) ?? new TransferVoucherEntity();

			var BLL = new TransferVoucherBLL<TransferVoucherEntity>(ConnectionString, CClaim);
			var Result = await BLL.Add(Entity);
			return Ok(Result);
			#endregion
		}

		public override async Task<ActionResult<ResultDto>> Edit()
		{
			#region Edit
			var Entity = JsonConvert.DeserializeObject<TransferVoucherEntity>(Request.Form["Data"]) ?? new TransferVoucherEntity();

			var BLL = new TransferVoucherBLL<TransferVoucherEntity>(ConnectionString, CClaim);
			var Result = await BLL.Edit(Entity);
			return Ok(Result);
			#endregion
		}

		public override async Task<ActionResult<ResultDto>> Delete(int Id)
		{
			#region Delete
			var BLL = new TransferVoucherBLL<TransferVoucherEntity>(ConnectionString, CClaim);
			var Result = await BLL.Delete(Id);
			return Ok(Result);
			#endregion
		}

		[HttpGet("GetTransferVoucherStoreList")]
		public virtual async Task<ActionResult<ResultDto>> GetTransferVoucherStoreList(bool? EditMode = null)
		{
			#region GetTransferVoucherStoreList
			var BLL = new TransferVoucherBLL<dynamic>(ConnectionString, CClaim);
			var Result = await BLL.GetTransferVoucherStoreList(EditMode);
			return Ok(Result);
			#endregion
		}

		[HttpGet("GetTransferVoucherInventoryVoucherList")]
		public virtual async Task<ActionResult<ResultDto>> GetTransferVoucherInventoryVoucherList(bool? EditMode = null)
		{
			#region GetTransferVoucherInventoryVoucherList
			var BLL = new TransferVoucherBLL<dynamic>(ConnectionString, CClaim);
			var Result = await BLL.GetTransferVoucherInventoryVoucherList(EditMode);
			return Ok(Result);
			#endregion
		}

		[HttpGet("GetTransferVoucherInventoryVoucherSpecificationList")]
		public virtual async Task<ActionResult<ResultDto>> GetTransferVoucherInventoryVoucherSpecificationList(bool? EditMode = null)
		{
			#region GetTransferVoucherInventoryVoucherSpecificationList
			var BLL = new TransferVoucherBLL<dynamic>(ConnectionString, CClaim);
			var Result = await BLL.GetTransferVoucherInventoryVoucherSpecificationList(EditMode);
			return Ok(Result);
			#endregion
		}

		[HttpGet("GetTransferVoucherUserList")]
		public virtual async Task<ActionResult<ResultDto>> GetTransferVoucherUserList(bool? EditMode = null)
		{
			#region GetTransferVoucherUserList
			var BLL = new TransferVoucherBLL<dynamic>(ConnectionString, CClaim);
			var Result = await BLL.GetTransferVoucherUserList(EditMode);
			return Ok(Result);
			#endregion
		}

		[HttpGet("GetTransferVoucherStateEnumList")]
		public virtual async Task<ActionResult<ResultDto>> GetTransferVoucherStateEnumList(bool? EditMode = null)
		{
			#region GetTransferVoucherStateEnumList
			var BLL = new TransferVoucherBLL<dynamic>(ConnectionString, CClaim);
			var Result = await BLL.GetTransferVoucherStateEnumList(EditMode);
			return Ok(Result);
			#endregion
		}

		[HttpGet("GetLastStateById")]
		public virtual async Task<ActionResult<ResultDto>> GetLastStateById(int Id)
		{
			#region GetLastStateById
			var BLL = new TransferVoucherBLL<LastStateDto>(ConnectionString, CClaim);
			var Result = await BLL.GetLastStateById(Id);
			return Ok(Result);
			#endregion
		}
	}
}
