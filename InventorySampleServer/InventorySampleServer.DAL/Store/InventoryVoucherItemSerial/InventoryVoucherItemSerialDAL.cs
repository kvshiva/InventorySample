using Microsoft.Data.SqlClient;
using InventorySampleServer.DAL._Gen.Store;

namespace InventorySampleServer.DAL.Store.InventoryVoucherItemSerial
{
	public class InventoryVoucherItemSerialDAL<TEntity> : GInventoryVoucherItemSerialDAL<TEntity> where TEntity : class
	{
		public InventoryVoucherItemSerialDAL(SqlConnection? Connection, SqlTransaction? Transaction) : base(Connection, Transaction) {  }
	}
}
