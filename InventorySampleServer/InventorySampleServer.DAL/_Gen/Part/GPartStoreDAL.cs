using Dapper;
using Microsoft.Data.SqlClient;
using InventorySampleServer.DAL._Base;

namespace InventorySampleServer.DAL._Gen.Part
{
	public class GPartStoreDAL<TEntity> : BaseDAL<TEntity> where TEntity : class
	{
		public GPartStoreDAL(SqlConnection? Connection, SqlTransaction? Transaction) : base(Connection, Transaction) { }

		public override async Task<TEntity> GetById(int Id)
		{
			#region GetById
			try
			{
				var Command = @"SELECT
									[PS].[Id],
									[PS].[Version],
									[PS].[CreatedBy],
									[PS].[CreatedDateTime],
									[PS].[UpdatedBy],
									[PS].[UpdatedDateTime],
									[P].[Title] [PartTitle],
									[PS].[PartId],
									[S].[Title] [StoreTitle],
									[PS].[StoreId],
									[PS].[IsActive],
									[PS].[IsDefault],
									[PS].[Comment],
									CAST(1 AS BIT) Editable
								FROM
									[Part].[PartStore] [PS]
									INNER JOIN [Part].[Part] [P] ON [P].[Id] = [PS].[PartId]
									LEFT  JOIN [Store].[Store] [S] ON [S].[Id] = [PS].[StoreId]
								WHERE
									[PS].[Id] = @Id ";

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
									[PS].[Id],
									[PS].[Version],
									[PS].[CreatedBy],
									[PS].[CreatedDateTime],
									[PS].[UpdatedBy],
									[PS].[UpdatedDateTime],
									[PS].[PartId],
									[PS].[StoreId],
									[PS].[IsActive],
									[PS].[IsDefault],
									[PS].[Comment]
								FROM
									[Part].[PartStore] [PS] 
								WHERE
									[PS].[Id] = @Id ";

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
									[PS].[Id],
									[PS].[Version],
									[PS].[CreatedBy],
									[PS].[CreatedDateTime],
									[PS].[UpdatedBy],
									[PS].[UpdatedDateTime],
									[P].[Title] [PartTitle],
									[PS].[PartId],
									[S].[Title] [StoreTitle],
									[PS].[StoreId],
									[PS].[IsActive],
									[PS].[IsDefault],
									[PS].[Comment],
									CAST(1 AS BIT) Editable,
									ItemCount = COUNT(*) OVER()
								FROM
									[Part].[PartStore] [PS]
									INNER JOIN [Part].[Part] [P] ON [P].[Id] = [PS].[PartId]
									LEFT  JOIN [Store].[Store] [S] ON [S].[Id] = [PS].[StoreId]";

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
									[PS].[Id],
									[PS].[Version],
									[PS].[CreatedBy],
									[PS].[CreatedDateTime],
									[PS].[UpdatedBy],
									[PS].[UpdatedDateTime],
									[P].[Title] [PartTitle],
									[PS].[PartId],
									[S].[Title] [StoreTitle],
									[PS].[StoreId],
									[PS].[IsActive],
									[PS].[IsDefault],
									[PS].[Comment],
									CAST(1 AS BIT) Editable,
									ItemCount = COUNT(*) OVER()
								FROM
									[Part].[PartStore] [PS]
									INNER JOIN [Part].[Part] [P] ON [P].[Id] = [PS].[PartId]
									LEFT  JOIN [Store].[Store] [S] ON [S].[Id] = [PS].[StoreId]
								WHERE
									(
										@SearchValue IS NULL
										OR
										(
											[P].[Title] LIKE N'%' + @SearchValue + '%'
											OR
											[S].[Title] LIKE N'%' + @SearchValue + '%'
											OR
											[PS].[IsActive] LIKE N'%' + @SearchValue + '%'
											OR
											[PS].[IsDefault] LIKE N'%' + @SearchValue + '%'
											OR
											[PS].[Comment] LIKE N'%' + @SearchValue + '%'
											
										)
									)
								ORDER BY
									CASE WHEN @SortField IS NULL THEN [PS].[Id] END ASC,
									CASE WHEN @SortField = 'PartTitle' AND @Direction = 'DESC' THEN [P].[Title] END DESC,
									CASE WHEN @SortField = 'PartTitle' AND @Direction = 'ASC' THEN [P].[Title] END ASC,
									CASE WHEN @SortField = 'StoreTitle' AND @Direction = 'DESC' THEN [S].[Title] END DESC,
									CASE WHEN @SortField = 'StoreTitle' AND @Direction = 'ASC' THEN [S].[Title] END ASC,
									CASE WHEN @SortField = 'IsActive' AND @Direction = 'DESC' THEN [PS].[IsActive] END DESC,
									CASE WHEN @SortField = 'IsActive' AND @Direction = 'ASC' THEN [PS].[IsActive] END ASC,
									CASE WHEN @SortField = 'IsDefault' AND @Direction = 'DESC' THEN [PS].[IsDefault] END DESC,
									CASE WHEN @SortField = 'IsDefault' AND @Direction = 'ASC' THEN [PS].[IsDefault] END ASC,
									CASE WHEN @SortField = 'Comment' AND @Direction = 'DESC' THEN [PS].[Comment] END DESC,
									CASE WHEN @SortField = 'Comment' AND @Direction = 'ASC' THEN [PS].[Comment] END ASC
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
				var Command = @"INSERT INTO [Part].[PartStore]
												(
													[CreatedBy],
													[CreatedDateTime],
													[PartId],
													[StoreId],
													[IsActive],
													[IsDefault],
													[Comment]
												)
											OUTPUT INSERTED.Id
											VALUES
												(
													@CreatedBy,
													@CreatedDateTime,
													@PartId,
													@StoreId,
													@IsActive,
													@IsDefault,
													@Comment
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
				var Command = @"UPDATE [Part].[PartStore] SET
									[UpdatedBy] = @UpdatedBy,
									[UpdatedDateTime] = @UpdatedDateTime,
									[PartId] = @PartId,
									[StoreId] = @StoreId,
									[IsActive] = @IsActive,
									[IsDefault] = @IsDefault,
									[Comment] = @Comment
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
				var Command = @"Delete [Part].[PartStore] WHERE [Id] = @Id ";

				return await Connection.ExecuteAsync(Command, new {Id}, transaction: Transaction);
			}
			catch { throw; }
			#endregion
		}

		public virtual async Task<IEnumerable<TEntity>> GetListByPartId(int PartId, string? SearchValue = null, string? SortField = null, string? Direction = null, int? PageNumber = null, int? PageSize = null, int? Language = null, bool? EditMode = null)
		{
			#region GetListByPartId
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
									[PS].[Id],
									[PS].[Version],
									[PS].[CreatedBy],
									[PS].[CreatedDateTime],
									[PS].[UpdatedBy],
									[PS].[UpdatedDateTime],
									[P].[Title] [PartTitle],
									[PS].[PartId],
									[S].[Title] [StoreTitle],
									[PS].[StoreId],
									[PS].[IsActive],
									[PS].[IsDefault],
									[PS].[Comment],
									CAST(1 AS BIT) Editable,
									ItemCount = COUNT(*) OVER()
								FROM
									[Part].[PartStore] [PS]
									INNER JOIN [Part].[Part] [P] ON [P].[Id] = [PS].[PartId]
									LEFT  JOIN [Store].[Store] [S] ON [S].[Id] = [PS].[StoreId]
								WHERE
									[PS].[PartId] = @PartId
									AND
									(
										@SearchValue IS NULL
										OR
										(
											[P].[Title] LIKE N'%' + @SearchValue + '%'
											OR
											[S].[Title] LIKE N'%' + @SearchValue + '%'
											OR
											[PS].[IsActive] LIKE N'%' + @SearchValue + '%'
											OR
											[PS].[IsDefault] LIKE N'%' + @SearchValue + '%'
											OR
											[PS].[Comment] LIKE N'%' + @SearchValue + '%'
											
										)
									)
								ORDER BY
									CASE WHEN @SortField IS NULL THEN [PS].[Id] END ASC,
									CASE WHEN @SortField = 'PartTitle' AND @Direction = 'DESC' THEN [P].[Title] END DESC,
									CASE WHEN @SortField = 'PartTitle' AND @Direction = 'ASC' THEN [P].[Title] END ASC,
									CASE WHEN @SortField = 'StoreTitle' AND @Direction = 'DESC' THEN [S].[Title] END DESC,
									CASE WHEN @SortField = 'StoreTitle' AND @Direction = 'ASC' THEN [S].[Title] END ASC,
									CASE WHEN @SortField = 'IsActive' AND @Direction = 'DESC' THEN [PS].[IsActive] END DESC,
									CASE WHEN @SortField = 'IsActive' AND @Direction = 'ASC' THEN [PS].[IsActive] END ASC,
									CASE WHEN @SortField = 'IsDefault' AND @Direction = 'DESC' THEN [PS].[IsDefault] END DESC,
									CASE WHEN @SortField = 'IsDefault' AND @Direction = 'ASC' THEN [PS].[IsDefault] END ASC,
									CASE WHEN @SortField = 'Comment' AND @Direction = 'DESC' THEN [PS].[Comment] END DESC,
									CASE WHEN @SortField = 'Comment' AND @Direction = 'ASC' THEN [PS].[Comment] END ASC
								OFFSET @Offset ROWS FETCH NEXT @Size ROWS ONLY ";

				return await Connection.QueryAsync<TEntity>(Command, new {PartId, Offset, Size, SearchValue, SortField, Direction, Language, EditMode}, transaction: Transaction);
			}
			catch { throw; }
			#endregion
		}

		public virtual async Task<IEnumerable<TEntity>> GetListByStoreId(int StoreId, string? SearchValue = null, string? SortField = null, string? Direction = null, int? PageNumber = null, int? PageSize = null, int? Language = null, bool? EditMode = null)
		{
			#region GetListByStoreId
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
									[PS].[Id],
									[PS].[Version],
									[PS].[CreatedBy],
									[PS].[CreatedDateTime],
									[PS].[UpdatedBy],
									[PS].[UpdatedDateTime],
									[P].[Title] [PartTitle],
									[PS].[PartId],
									[S].[Title] [StoreTitle],
									[PS].[StoreId],
									[PS].[IsActive],
									[PS].[IsDefault],
									[PS].[Comment],
									CAST(1 AS BIT) Editable,
									ItemCount = COUNT(*) OVER()
								FROM
									[Part].[PartStore] [PS]
									INNER JOIN [Part].[Part] [P] ON [P].[Id] = [PS].[PartId]
									LEFT  JOIN [Store].[Store] [S] ON [S].[Id] = [PS].[StoreId]
								WHERE
									[PS].[StoreId] = @StoreId
									AND
									(
										@SearchValue IS NULL
										OR
										(
											[P].[Title] LIKE N'%' + @SearchValue + '%'
											OR
											[S].[Title] LIKE N'%' + @SearchValue + '%'
											OR
											[PS].[IsActive] LIKE N'%' + @SearchValue + '%'
											OR
											[PS].[IsDefault] LIKE N'%' + @SearchValue + '%'
											OR
											[PS].[Comment] LIKE N'%' + @SearchValue + '%'
											
										)
									)
								ORDER BY
									CASE WHEN @SortField IS NULL THEN [PS].[Id] END ASC,
									CASE WHEN @SortField = 'PartTitle' AND @Direction = 'DESC' THEN [P].[Title] END DESC,
									CASE WHEN @SortField = 'PartTitle' AND @Direction = 'ASC' THEN [P].[Title] END ASC,
									CASE WHEN @SortField = 'StoreTitle' AND @Direction = 'DESC' THEN [S].[Title] END DESC,
									CASE WHEN @SortField = 'StoreTitle' AND @Direction = 'ASC' THEN [S].[Title] END ASC,
									CASE WHEN @SortField = 'IsActive' AND @Direction = 'DESC' THEN [PS].[IsActive] END DESC,
									CASE WHEN @SortField = 'IsActive' AND @Direction = 'ASC' THEN [PS].[IsActive] END ASC,
									CASE WHEN @SortField = 'IsDefault' AND @Direction = 'DESC' THEN [PS].[IsDefault] END DESC,
									CASE WHEN @SortField = 'IsDefault' AND @Direction = 'ASC' THEN [PS].[IsDefault] END ASC,
									CASE WHEN @SortField = 'Comment' AND @Direction = 'DESC' THEN [PS].[Comment] END DESC,
									CASE WHEN @SortField = 'Comment' AND @Direction = 'ASC' THEN [PS].[Comment] END ASC
								OFFSET @Offset ROWS FETCH NEXT @Size ROWS ONLY ";

				return await Connection.QueryAsync<TEntity>(Command, new {StoreId, Offset, Size, SearchValue, SortField, Direction, Language, EditMode}, transaction: Transaction);
			}
			catch { throw; }
			#endregion
		}

		public virtual async Task<IEnumerable<TEntity>> GetPartStorePartList(bool? EditMode = null)
		{
			#region GetPartStorePartList
			try
			{
				var Command = @"SELECT
									[P].[Id],
									[P].[Title],
									CAST(1 AS BIT) Editable,
									ItemCount = COUNT(*) OVER()
								FROM
									[Part].[Part] [P] ";

				return await Connection.QueryAsync<TEntity>(Command, new { EditMode }, transaction: Transaction);
			}
			catch { throw; }
			#endregion
		}

		public virtual async Task<IEnumerable<TEntity>> GetPartStoreStoreList(bool? EditMode = null)
		{
			#region GetPartStoreStoreList
			try
			{
				var Command = @"SELECT
									[S].[Id],
									[S].[Title],
									CAST(1 AS BIT) Editable,
									ItemCount = COUNT(*) OVER()
								FROM
									[Store].[Store] [S] ";

				return await Connection.QueryAsync<TEntity>(Command, new { EditMode }, transaction: Transaction);
			}
			catch { throw; }
			#endregion
		}

	}
}
