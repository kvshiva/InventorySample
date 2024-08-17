using Model.Custom.Other;
using InventorySampleServer.BLL._Gen.Part;

namespace InventorySampleServer.BLL.Part.CountUnit
{
	public class CountUnitBLL<TEntity> : GCountUnitBLL<TEntity> where TEntity : class
	{
		public CountUnitBLL(string ConnectionString, GClaim Claim) : base(ConnectionString, Claim) { }
	}
}
