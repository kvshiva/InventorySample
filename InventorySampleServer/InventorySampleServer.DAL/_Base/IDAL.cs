namespace InventorySampleServer.DAL._Base
{
	public interface IDAL<TEntity>
	{
		public Task<TEntity> GetById(int Id);

		public Task<TEntity> GetObjectById(int Id);

		public Task<IEnumerable<TEntity>> GetList(bool? EditMode = null);

		public Task<IEnumerable<TEntity>> GetList(string? SearchValue = null, string? SortField = null, string? Direction = null, int? PageNumber = null, int? PageSize = null, int? Language = null, bool? EditMode = null);

		public Task<int> Add(TEntity Entity);

		public Task<int> Edit(TEntity Entity);

		public Task<int> Delete(int Id);
	}
}
