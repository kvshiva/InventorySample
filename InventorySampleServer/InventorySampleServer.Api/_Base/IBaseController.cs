using Common;
using Microsoft.AspNetCore.Mvc;

namespace InventorySampleServer.Api._Base
{
	public interface IBaseController
	{
		public Task<ActionResult<ResultDto>> GetById(int Id);

		public Task<ActionResult<ResultDto>> GetList(string? SearchValue = null, string? SortField = null, string? Direction = null, int? PageNumber = null, int? PageSize = null, int? Language = null, bool? EditMode = null);

		public Task<ActionResult<ResultDto>> Add();

		public Task<ActionResult<ResultDto>> Edit();

		public Task<ActionResult<ResultDto>> Delete(int Id);
	}
}
