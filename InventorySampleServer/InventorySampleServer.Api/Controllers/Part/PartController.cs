using InventorySampleServer.Api._Gen.Part;
using Microsoft.Extensions.Configuration;
using Common;
using Microsoft.AspNetCore.Mvc;
using InventorySampleServer.BLL.Part.Part;
using InventorySampleServer.Model.Part.Part;
using InventorySampleServer.Model.Custom.Part;
using InventorySampleServer.Model.Custom.Other;

namespace InventorySampleServer.Api.Controllers.Part
{
	public class PartController : GPartController
	{
		public PartController(IConfiguration Configuration) : base(Configuration) { }

        [HttpGet("GetPartQuantity/{PartId}/{StoreId}")]
        public async Task<ActionResult<ResultDto>> GetPartQuantity(int PartId, int StoreId)
        {
            #region GetPartQuantity
            var BLL = new PartBLL<PartQuantityDto>(ConnectionString, CClaim);
            var Result = await BLL.GetPartQuantity(PartId, StoreId);
            return Ok(Result);
            #endregion
        }

        [HttpGet("GetListByStoreId")]
        public async Task<ActionResult<ResultDto>> GetListByStoreId([FromQuery] GetListParamsDto Params)
        {
            #region GetListByStoreId
            var BLL = new PartBLL<PartListDto>(ConnectionString, CClaim);
            var Result = await BLL.GetListByStoreId(Params);
            return Ok(Result);
            #endregion
        }

    }
}
