using Dapper;
using Microsoft.Data.SqlClient;
using InventorySampleServer.DAL._Base;

namespace InventorySampleServer.DAL._Gen.Store
{
	public class GStoreDAL<TEntity> : BaseDAL<TEntity> where TEntity : class
	{
		public GStoreDAL(SqlConnection? Connection, SqlTransaction? Transaction) : base(Connection, Transaction) { }

		public override async Task<TEntity> GetById(int Id)
		{
			#region GetById
			try
			{
				var Command = @"SELECT
									[S].[Id],
									[S].[Version],
									[S].[CreatedBy],
									[S].[CreatedDateTime],
									[S].[UpdatedBy],
									[S].[UpdatedDateTime],
									[S].[Title],
									[S].[Code],
									[S].[Comment],
									[S].[Disabled],
									[S].[Jsonfield],
									[STE].[Title] [StoreTypeEnumTitle],
									[S].[StoreTypeEnumId],
									CAST(1 AS BIT) Editable
								FROM
									[Store].[Store] [S]
									INNER JOIN [Store].[StoreTypeEnum] [STE] ON [STE].[Id] = [S].[StoreTypeEnumId]
								WHERE
									[S].[Id] = @Id ";

				return await Connection.QueryFirstOrDefaultAsync<TEntity>(Command, new {Id}, transaction: Transaction);
			}
			catch { throw; }
			#endregion
		}

		public override async Task<TEntity> GetObjectById(int Id)
		{
			#region GetObjectById
			try
			{
				var Command = @"SELECT
									[S].[Id],
									[S].[Version],
									[S].[CreatedBy],
									[S].[CreatedDateTime],
									[S].[UpdatedBy],
									[S].[UpdatedDateTime],
									[S].[Title],
									[S].[Code],
									[S].[Comment],
									[S].[Disabled],
									[S].[Jsonfield],
									[S].[StoreTypeEnumId]
								FROM
									[Store].[Store] [S] 
								WHERE
									[S].[Id] = @Id ";

				return await Connection.QueryFirstOrDefaultAsync<TEntity>(Command, new {Id}, transaction: Transaction);
			}
			catch { throw; }
			#endregion
		}

		public override async Task<IEnumerable<TEntity>> GetList(bool? EditMode = null)
		{
			#region GetList
			try
			{
				var Command = @"SELECT
									[S].[Id],
									[S].[Version],
									[S].[CreatedBy],
									[S].[CreatedDateTime],
									[S].[UpdatedBy],
									[S].[UpdatedDateTime],
									[S].[Title],
									[S].[Code],
									[S].[Comment],
									[S].[Disabled],
									[S].[Jsonfield],
									[STE].[Title] [StoreTypeEnumTitle],
									[S].[StoreTypeEnumId],
									CAST(1 AS BIT) Editable,
									ItemCount = COUNT(*) OVER()
								FROM
									[Store].[Store] [S]
									INNER JOIN [Store].[StoreTypeEnum] [STE] ON [STE].[Id] = [S].[StoreTypeEnumId]";

				return await Connection.QueryAsync<TEntity>(Command, new { EditMode }, transaction: Transaction);
			}
			catch { throw; }
			#endregion
		}

		public override async Task<IEnumerable<TEntity>> GetList(string? SearchValue = null, string? SortField = null, string? Direction = null, int? PageNumber = null, int? PageSize = null, int? Language = null, bool? EditMode = null)
		{
			#region GetList
			try
			{
				var Offset = 0;
				var Size = int.MaxValue;
				if (PageNumber != null && PageSize != null)
				{
					Size = PageSize.Value;
					Offset = OffSet(PageNumber.Value, PageSize.Value);
				}

				var Command = @"SELECT
									[S].[Id],
									[S].[Version],
									[S].[CreatedBy],
									[S].[CreatedDateTime],
									[S].[UpdatedBy],
									[S].[UpdatedDateTime],
									[S].[Title],
									[S].[Code],
									[S].[Comment],
									[S].[Disabled],
									[S].[Jsonfield],
									[STE].[Title] [StoreTypeEnumTitle],
									[S].[StoreTypeEnumId],
									CAST(1 AS BIT) Editable,
									ItemCount = COUNT(*) OVER()
								FROM
									[Store].[Store] [S]
									INNER JOIN [Store].[StoreTypeEnum] [STE] ON [STE].[Id] = [S].[StoreTypeEnumId]
								WHERE
									(
										@SearchValue IS NULL
										OR
										(
											[S].[Title] LIKE N'%' + @SearchValue + '%'
											OR
											[S].[Code] LIKE N'%' + @SearchValue + '%'
											OR
											[S].[Comment] LIKE N'%' + @SearchValue + '%'
											OR
											[S].[Disabled] LIKE N'%' + @SearchValue + '%'
											OR
											[S].[Jsonfield] LIKE N'%' + @SearchValue + '%'
											OR
											[STE].[Title] LIKE N'%' + @SearchValue + '%'
											
										)
									)
								ORDER BY
									CASE WHEN @SortField IS NULL THEN [S].[Id] END ASC,
									CASE WHEN @SortField = 'Title' AND @Direction = 'DESC' THEN [S].[Title] END DESC,
									CASE WHEN @SortField = 'Title' AND @Direction = 'ASC' THEN [S].[Title] END ASC,
									CASE WHEN @SortField = 'Code' AND @Direction = 'DESC' THEN [S].[Code] END DESC,
									CASE WHEN @SortField = 'Code' AND @Direction = 'ASC' THEN [S].[Code] END ASC,
									CASE WHEN @SortField = 'Comment' AND @Direction = 'DESC' THEN [S].[Comment] END DESC,
									CASE WHEN @SortField = 'Comment' AND @Direction = 'ASC' THEN [S].[Comment] END ASC,
									CASE WHEN @SortField = 'Disabled' AND @Direction = 'DESC' THEN [S].[Disabled] END DESC,
									CASE WHEN @SortField = 'Disabled' AND @Direction = 'ASC' THEN [S].[Disabled] END ASC,
									CASE WHEN @SortField = 'Jsonfield' AND @Direction = 'DESC' THEN [S].[Jsonfield] END DESC,
									CASE WHEN @SortField = 'Jsonfield' AND @Direction = 'ASC' THEN [S].[Jsonfield] END ASC,
									CASE WHEN @SortField = 'StoreTypeEnumTitle' AND @Direction = 'DESC' THEN [STE].[Title] END DESC,
									CASE WHEN @SortField = 'StoreTypeEnumTitle' AND @Direction = 'ASC' THEN [STE].[Title] END ASC
								OFFSET @Offset ROWS FETCH NEXT @Size ROWS ONLY ";

				return await Connection.QueryAsync<TEntity>(Command, new {Offset, Size, SearchValue, SortField, Direction, Language, EditMode}, transaction: Transaction);
			}
			catch { throw; }
			#endregion
		}

		public override async Task<int> Add(TEntity Entity)
		{
			#region Add
			try
			{
				var Command = @"INSERT INTO [Store].[Store]
												(
													[CreatedBy],
													[CreatedDateTime],
													[Title],
													[Code],
													[Comment],
													[Disabled],
													[Jsonfield],
													[StoreTypeEnumId]
												)
											OUTPUT INSERTED.Id
											VALUES
												(
													@CreatedBy,
													@CreatedDateTime,
													@Title,
													@Code,
													@Comment,
													@Disabled,
													@Jsonfield,
													@StoreTypeEnumId
												) ";

				return await Connection.ExecuteScalarAsync<int>(Command, Entity, transaction: Transaction);
			}
			catch { throw; }
			#endregion
		}

		public override async Task<int> Edit(TEntity Entity)
		{
			#region Edit
			try
			{
				var Command = @"UPDATE [Store].[Store] SET
									[UpdatedBy] = @UpdatedBy,
									[UpdatedDateTime] = @UpdatedDateTime,
									[Title] = @Title,
									[Code] = @Code,
									[Comment] = @Comment,
									[Disabled] = @Disabled,
									[Jsonfield] = @Jsonfield,
									[StoreTypeEnumId] = @StoreTypeEnumId
								WHERE
									[Id] = @Id ";

				return await Connection.ExecuteAsync(Command, Entity, transaction: Transaction);
			}
			catch { throw; }
			#endregion
		}

		public override async Task<int> Delete(int Id)
		{
			#region Delete
			try
			{
				var Command = @"Delete [Store].[Store] WHERE [Id] = @Id ";

				return await Connection.ExecuteAsync(Command, new {Id}, transaction: Transaction);
			}
			catch { throw; }
			#endregion
		}

		public virtual async Task<IEnumerable<TEntity>> GetStoreStoreTypeEnumList(bool? EditMode = null)
		{
			#region GetStoreStoreTypeEnumList
			try
			{
				var Command = @"SELECT
									[STE].[Id],
									[STE].[Title],
									CAST(1 AS BIT) Editable,
									ItemCount = COUNT(*) OVER()
								FROM
									[Store].[StoreTypeEnum] [STE] ";

				return await Connection.QueryAsync<TEntity>(Command, new { EditMode }, transaction: Transaction);
			}
			catch { throw; }
			#endregion
		}

	}
}
