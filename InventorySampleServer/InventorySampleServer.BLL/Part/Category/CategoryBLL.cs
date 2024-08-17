using Model.Custom.Other;
using InventorySampleServer.BLL._Gen.Part;

namespace InventorySampleServer.BLL.Part.Category
{
	public class CategoryBLL<TEntity> : GCategoryBLL<TEntity> where TEntity : class
	{
		public CategoryBLL(string ConnectionString, GClaim Claim) : base(ConnectionString, Claim) { }
	}
}
