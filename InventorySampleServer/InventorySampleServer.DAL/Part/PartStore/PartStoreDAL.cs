using Microsoft.Data.SqlClient;
using InventorySampleServer.DAL._Gen.Part;

namespace InventorySampleServer.DAL.Part.PartStore
{
	public class PartStoreDAL<TEntity> : GPartStoreDAL<TEntity> where TEntity : class
	{
		public PartStoreDAL(SqlConnection? Connection, SqlTransaction? Transaction) : base(Connection, Transaction) {  }
	}
}
