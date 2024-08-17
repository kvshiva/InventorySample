using Common;
using Microsoft.AspNetCore.Http;

namespace InventorySampleServer.BLL._Base
{
	public interface IBLL<TEntity>
	{
		public Task<ResultDto> GetById(int Id);

		public Task<ResultDto> GetList(bool? EditMode = null);

		public Task<ResultDto> GetList(string? SearchValue = null, string? SortField = null, string? Direction = null, int? PageNumber = null, int? PageSize = null, int? Language = null, bool? EditMode = null);

		public Task<ResultDto> Add(TEntity Entity);

		public Task<ResultDto> Add(TEntity Entity, IFormFileCollection Files);

		public Task<ResultDto> Edit(TEntity Entity);

		public Task<ResultDto> Edit(TEntity Entity, IFormFileCollection Files);

		public Task<ResultDto> Delete(int Id);
	}
}
