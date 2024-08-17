using InventorySampleServer.Api._Gen.Store;
using Microsoft.Extensions.Configuration;
using Common;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using InventorySampleServer.BLL.Store.TransferVoucher;
using InventorySampleServer.Model.Store.TransferVoucher;
using InventorySampleServer.Model.Custom.TransferVoucher;

namespace InventorySampleServer.Api.Controllers.Store
{
	public class TransferVoucherController : GTransferVoucherController
	{
		public TransferVoucherController(IConfiguration Configuration) : base(Configuration) { }

        public override async Task<ActionResult<ResultDto>> Add() // Customized For Different Entity
        {
            #region Add
            var Entity = JsonConvert.DeserializeObject<TransferVoucherDetailedEntity>(Request.Form["Data"]) ?? new TransferVoucherDetailedEntity();

            var BLL = new TransferVoucherBLL<TransferVoucherDetailedEntity>(ConnectionString, CClaim);
            var Result = await BLL.Add(Entity);
            return Ok(Result);
            #endregion
        }
        
    }
}
