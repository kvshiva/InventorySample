using Microsoft.Data.SqlClient;

namespace InventorySampleServer.DAL._Base
{
	public abstract class BaseDAL<TEntity> : IDAL<TEntity> where TEntity : class
	{
		protected SqlConnection? Connection;
		protected SqlTransaction? Transaction;

		public BaseDAL(SqlConnection? Connection, SqlTransaction? Transaction)
		{
			this.Connection = Connection;
			this.Transaction = Transaction;
		}

		public virtual Task<TEntity> GetById(int Id) { throw new NotImplementedException(); }

		public virtual Task<TEntity> GetObjectById(int Id) { throw new NotImplementedException(); }

		public virtual Task<IEnumerable<TEntity>> GetList(bool? EditMode = null) { throw new NotImplementedException(); }

		public virtual Task<IEnumerable<TEntity>> GetList(string? SearchValue = null, string? SortField = null, string? Direction = null, int? PageNumber = null, int? PageSize = null, int? Language = null, bool? EditMode = null) { throw new NotImplementedException(); }

		public virtual Task<int> Add(TEntity Entity) { throw new NotImplementedException(); }

		public virtual Task<int> Edit(TEntity Entity) { throw new NotImplementedException(); }

		public virtual Task<int> Delete(int Id) { throw new NotImplementedException(); }

		protected static int OffSet(int PageNumber, int PageSize)
		{
			#region Offset
			return (PageNumber - 1) * PageSize;
			#endregion
		}
	}
}
