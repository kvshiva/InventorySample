using Microsoft.Data.SqlClient;
using InventorySampleServer.DAL._Gen.Store;

namespace InventorySampleServer.DAL.Store.Store
{
	public class StoreDAL<TEntity> : GStoreDAL<TEntity> where TEntity : class
	{
		public StoreDAL(SqlConnection? Connection, SqlTransaction? Transaction) : base(Connection, Transaction) {  }
	}
}
