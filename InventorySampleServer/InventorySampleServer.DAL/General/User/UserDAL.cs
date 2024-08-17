using Microsoft.Data.SqlClient;
using InventorySampleServer.DAL._Gen.General;

namespace InventorySampleServer.DAL.General.User
{
	public class UserDAL<TEntity> : GUserDAL<TEntity> where TEntity : class
	{
		public UserDAL(SqlConnection? Connection, SqlTransaction? Transaction) : base(Connection, Transaction) {  }
	}
}
