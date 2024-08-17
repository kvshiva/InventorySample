using Common;
using Model.Custom.Other;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.Configuration;

namespace InventorySampleServer.Api._Base
{
	//[Authorize]
	[ApiController]
	[Route("api/[controller]")]
	public abstract class BaseController : Controller, IBaseController
	{
		protected GClaim CClaim;
		protected string ConnectionString;
		protected IConfiguration Configuration;

		public BaseController(IConfiguration Configuration)
		{
			this.Configuration = Configuration;
			ConnectionString = this.Configuration.GetConnectionString("SQLConnectionString");

			CClaim = new GClaim()
			{
				UserId = User != null ? int.Parse(User.Claims.FirstOrDefault(i => i.Type == "UserId")!.Value) : 0,
				UserFullName = User != null ? User.Claims.FirstOrDefault(i => i.Type == "FullName")!.Value : string.Empty
			};
		}

		[HttpGet("GetById/{Id}")]
		public virtual Task<ActionResult<ResultDto>> GetById(int Id) { throw new NotImplementedException(); }

		[HttpGet("GetList")]
		public virtual Task<ActionResult<ResultDto>> GetList(string? SearchValue = null, string? SortField = null, string? Direction = null, int? PageNumber = null, int? PageSize = null, int? Language = null, bool? EditMode = null) { throw new NotImplementedException(); }

		[HttpPost("Add")]
		public virtual Task<ActionResult<ResultDto>> Add() { throw new NotImplementedException(); }

		[HttpPut("Edit")]
		public virtual Task<ActionResult<ResultDto>> Edit() { throw new NotImplementedException(); }

		[HttpDelete("Delete/{Id}")]
		public virtual Task<ActionResult<ResultDto>> Delete(int Id) { throw new NotImplementedException(); }
	}
}
