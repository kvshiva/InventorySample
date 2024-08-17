using InventorySampleServer.Api._Gen.Store;
using Microsoft.Extensions.Configuration;
using Common;
using Microsoft.AspNetCore.Mvc;
using InventorySampleServer.BLL.Store.InventoryVoucher;
using InventorySampleServer.Model.Store.InventoryVoucher;
using InventorySampleServer.Model.Custom.Other;
using Newtonsoft.Json;
using InventorySampleServer.Model.Custom.InventoryVoucher;

namespace InventorySampleServer.Api.Controllers.Store
{
	public class InventoryVoucherController : GInventoryVoucherController
	{
		public InventoryVoucherController(IConfiguration Configuration) : base(Configuration) { }
        
        [HttpGet("GetListCustom")]
        public async Task<ActionResult<ResultDto>> GetListCustom([FromQuery] GetListParamsDto Params)
        {
            #region GetListCustom
            var BLL = new InventoryVoucherBLL<InventoryVoucherListVM>(ConnectionString, CClaim);
            var Result = await BLL.GetListCustom(Params);
            return Ok(Result);
            #endregion
        }
        [HttpGet("GetByIdCustom/{Id}")]
        public async Task<ActionResult<ResultDto>> GetByIdCustom(int Id)
        {
            #region GetByIdCustom
            var BLL = new InventoryVoucherBLL<InventoryVoucherHDVM>(ConnectionString, CClaim);
            var Result = await BLL.GetByIdCustom(Id);
            return Ok(Result);
            #endregion
        }
        [HttpGet("GetSerialListByInventoryVoucherItemId")]
        public async Task<ActionResult<ResultDto>> GetSerialListByInventoryVoucherItemId([FromQuery] GetListParamsDto Params)
        {
            #region GetSerialListByInventoryVoucherItemId
            var BLL = new InventoryVoucherBLL<InventoryVoucherListDto>(ConnectionString, CClaim);
            var Result = await BLL.GetSerialListByInventoryVoucherItemId(Params);
            return Ok(Result);
            #endregion
        }
        [HttpPost("AddReceipt")]
        public async Task<ActionResult<ResultDto>> AddReceipt()
        {
            #region AddReceipt
            var Entity = JsonConvert.DeserializeObject<HddInventoryVoucherDto>(Request.Form["Data"]) ?? new HddInventoryVoucherDto();
            var BLL = new InventoryVoucherBLL<HddInventoryVoucherDto>(ConnectionString, CClaim);
            var Result = await BLL.AddReceipt(Entity);
            return Ok(Result);
            #endregion
        }
        [HttpPost("AddRemittance")]
        public async Task<ActionResult<ResultDto>> AddRemittance()
        {
            #region AddRemittance
            var Entity = JsonConvert.DeserializeObject<HddInventoryVoucherDto>(Request.Form["Data"]) ?? new HddInventoryVoucherDto();
            var BLL = new InventoryVoucherBLL<HddInventoryVoucherDto>(ConnectionString, CClaim);
            var Result = await BLL.AddRemittance(Entity);
            return Ok(Result);
            #endregion
        }
        [HttpPost("EditReceipt")]
        public async Task<ActionResult<ResultDto>> EditReceipt()
        {
            #region EditReceipt
            var Entity = JsonConvert.DeserializeObject<HddInventoryVoucherDto>(Request.Form["Data"]) ?? new HddInventoryVoucherDto();
            var BLL = new InventoryVoucherBLL<HddInventoryVoucherDto>(ConnectionString, CClaim);
            var Result = await BLL.EditReceipt(Entity);
            return Ok(Result);
            #endregion
        }

        [NonAction] // Custmized To be unaccessable
        public override async Task<ActionResult<ResultDto>> Add()
        {
            #region Add
            var Entity = JsonConvert.DeserializeObject<HddInventoryVoucherDto>(Request.Form["Data"]) ?? new HddInventoryVoucherDto();

            var BLL = new InventoryVoucherBLL<HddInventoryVoucherDto>(ConnectionString, CClaim);
            var Result = await BLL.Add(Entity);
            return Ok(Result);
            #endregion
        }
        [NonAction] // Custmized To be unaccessable
        public override async Task<ActionResult<ResultDto>> Edit()
        {
            #region Edit
            var Entity = JsonConvert.DeserializeObject<HddInventoryVoucherDto>(Request.Form["Data"]) ?? new HddInventoryVoucherDto();

            var BLL = new InventoryVoucherBLL<HddInventoryVoucherDto>(ConnectionString, CClaim);
            var Result = await BLL.Edit(Entity);
            return Ok(Result);
            #endregion
        }
    }
}
