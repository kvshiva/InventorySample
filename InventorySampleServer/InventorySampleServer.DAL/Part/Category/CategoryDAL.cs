using Microsoft.Data.SqlClient;
using InventorySampleServer.DAL._Gen.Part;

namespace InventorySampleServer.DAL.Part.Category
{
	public class CategoryDAL<TEntity> : GCategoryDAL<TEntity> where TEntity : class
	{
		public CategoryDAL(SqlConnection? Connection, SqlTransaction? Transaction) : base(Connection, Transaction) {  }
	}
}
