using Common;
using Model.Custom.Other;
using Microsoft.Data.SqlClient;
using Microsoft.AspNetCore.Http;

namespace InventorySampleServer.BLL._Base
{
	public abstract class BaseBLL<TEntity> : IBLL<TEntity> where TEntity : class
	{
		protected GClaim Claim;
		protected SqlConnection? Connection;
		protected SqlTransaction? Transaction;
		protected readonly string ConnectionString;

		public BaseBLL(string ConnectionString, GClaim Claim)
		{
			this.ConnectionString = ConnectionString;
			this.Claim = Claim;
		}

		public virtual Task<ResultDto> GetById(int Id) { throw new NotImplementedException(); }

		public virtual Task<ResultDto> GetList(bool? EditMode = null) { throw new NotImplementedException(); }

		public virtual Task<ResultDto> GetList(string? SearchValue = null, string? SortField = null, string? Direction = null, int? PageNumber = null, int? PageSize = null, int? Language = null, bool? EditMode = null) { throw new NotImplementedException(); }

		public virtual Task<ResultDto> Add(TEntity Entity) { throw new NotImplementedException(); }

		public virtual Task<ResultDto> Add(TEntity Entity, IFormFileCollection Files) { throw new NotImplementedException(); }

		public virtual Task<ResultDto> Edit(TEntity Entity) { throw new NotImplementedException(); }

		public virtual Task<ResultDto> Edit(TEntity Entity, IFormFileCollection Files) { throw new NotImplementedException(); }

		public virtual Task<ResultDto> Delete(int Id) { throw new NotImplementedException(); }
	}
}
