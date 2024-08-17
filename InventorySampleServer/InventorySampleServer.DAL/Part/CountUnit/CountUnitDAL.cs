using Microsoft.Data.SqlClient;
using InventorySampleServer.DAL._Gen.Part;

namespace InventorySampleServer.DAL.Part.CountUnit
{
	public class CountUnitDAL<TEntity> : GCountUnitDAL<TEntity> where TEntity : class
	{
		public CountUnitDAL(SqlConnection? Connection, SqlTransaction? Transaction) : base(Connection, Transaction) {  }
	}
}
