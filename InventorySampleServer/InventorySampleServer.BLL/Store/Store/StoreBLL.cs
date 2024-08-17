using Model.Custom.Other;
using InventorySampleServer.BLL._Gen.Store;

namespace InventorySampleServer.BLL.Store.Store
{
	public class StoreBLL<TEntity> : GStoreBLL<TEntity> where TEntity : class
	{
		public StoreBLL(string ConnectionString, GClaim Claim) : base(ConnectionString, Claim) { }
	}
}
