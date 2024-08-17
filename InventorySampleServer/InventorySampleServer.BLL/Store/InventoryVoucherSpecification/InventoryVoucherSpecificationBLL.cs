using Model.Custom.Other;
using InventorySampleServer.BLL._Gen.Store;

namespace InventorySampleServer.BLL.Store.InventoryVoucherSpecification
{
	public class InventoryVoucherSpecificationBLL<TEntity> : GInventoryVoucherSpecificationBLL<TEntity> where TEntity : class
	{
		public InventoryVoucherSpecificationBLL(string ConnectionString, GClaim Claim) : base(ConnectionString, Claim) { }
	}
}
