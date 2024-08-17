using Dapper;
using Microsoft.Data.SqlClient;
using InventorySampleServer.DAL._Base;

namespace InventorySampleServer.DAL._Gen.Part
{
	public class GPartDAL<TEntity> : BaseDAL<TEntity> where TEntity : class
	{
		public GPartDAL(SqlConnection? Connection, SqlTransaction? Transaction) : base(Connection, Transaction) { }

		public override async Task<TEntity> GetById(int Id)
		{
			#region GetById
			try
			{
				var Command = @"SELECT
									[P].[Id],
									[P].[Version],
									[P].[CreatedBy],
									[P].[CreatedDateTime],
									[P].[UpdatedBy],
									[P].[UpdatedDateTime],
									[P].[Title],
									[P].[Code],
									[MainCountUnitCU].[Title] [MainCountUnitTitle],
									[P].[MainCountUnitId],
									[SecondaryCountUnitCU].[Title] [SecondaryCountUnitTitle],
									[P].[SecondaryCountUnitId],
									[C].[Title] [CategoryTitle],
									[P].[CategoryId],
									[P].[HasSerial],
									CAST(1 AS BIT) Editable
								FROM
									[Part].[Part] [P]
									INNER JOIN [Part].[CountUnit] [MainCountUnitCU] ON [MainCountUnitCU].[Id] = [P].[MainCountUnitId]
									LEFT  JOIN [Part].[CountUnit] [SecondaryCountUnitCU] ON [SecondaryCountUnitCU].[Id] = [P].[SecondaryCountUnitId]
									INNER JOIN [Part].[Category] [C] ON [C].[Id] = [P].[CategoryId]
								WHERE
									[P].[Id] = @Id ";

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
									[P].[Id],
									[P].[Version],
									[P].[CreatedBy],
									[P].[CreatedDateTime],
									[P].[UpdatedBy],
									[P].[UpdatedDateTime],
									[P].[Title],
									[P].[Code],
									[P].[MainCountUnitId],
									[P].[SecondaryCountUnitId],
									[P].[CategoryId],
									[P].[HasSerial]
								FROM
									[Part].[Part] [P] 
								WHERE
									[P].[Id] = @Id ";

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
									[P].[Id],
									[P].[Version],
									[P].[CreatedBy],
									[P].[CreatedDateTime],
									[P].[UpdatedBy],
									[P].[UpdatedDateTime],
									[P].[Title],
									[P].[Code],
									[MainCountUnitCU].[Title] [MainCountUnitTitle],
									[P].[MainCountUnitId],
									[SecondaryCountUnitCU].[Title] [SecondaryCountUnitTitle],
									[P].[SecondaryCountUnitId],
									[C].[Title] [CategoryTitle],
									[P].[CategoryId],
									[P].[HasSerial],
									CAST(1 AS BIT) Editable,
									ItemCount = COUNT(*) OVER()
								FROM
									[Part].[Part] [P]
									INNER JOIN [Part].[CountUnit] [MainCountUnitCU] ON [MainCountUnitCU].[Id] = [P].[MainCountUnitId]
									LEFT  JOIN [Part].[CountUnit] [SecondaryCountUnitCU] ON [SecondaryCountUnitCU].[Id] = [P].[SecondaryCountUnitId]
									INNER JOIN [Part].[Category] [C] ON [C].[Id] = [P].[CategoryId]";

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
									[P].[Id],
									[P].[Version],
									[P].[CreatedBy],
									[P].[CreatedDateTime],
									[P].[UpdatedBy],
									[P].[UpdatedDateTime],
									[P].[Title],
									[P].[Code],
									[MainCountUnitCU].[Title] [MainCountUnitTitle],
									[P].[MainCountUnitId],
									[SecondaryCountUnitCU].[Title] [SecondaryCountUnitTitle],
									[P].[SecondaryCountUnitId],
									[C].[Title] [CategoryTitle],
									[P].[CategoryId],
									[P].[HasSerial],
									CAST(1 AS BIT) Editable,
									ItemCount = COUNT(*) OVER()
								FROM
									[Part].[Part] [P]
									INNER JOIN [Part].[CountUnit] [MainCountUnitCU] ON [MainCountUnitCU].[Id] = [P].[MainCountUnitId]
									LEFT  JOIN [Part].[CountUnit] [SecondaryCountUnitCU] ON [SecondaryCountUnitCU].[Id] = [P].[SecondaryCountUnitId]
									INNER JOIN [Part].[Category] [C] ON [C].[Id] = [P].[CategoryId]
								WHERE
									(
										@SearchValue IS NULL
										OR
										(
											[P].[Title] LIKE N'%' + @SearchValue + '%'
											OR
											[P].[Code] LIKE N'%' + @SearchValue + '%'
											OR
											[CU].[Title] LIKE N'%' + @SearchValue + '%'
											OR
											[CU].[Title] LIKE N'%' + @SearchValue + '%'
											OR
											[C].[Title] LIKE N'%' + @SearchValue + '%'
											OR
											[P].[HasSerial] LIKE N'%' + @SearchValue + '%'
											
										)
									)
								ORDER BY
									CASE WHEN @SortField IS NULL THEN [P].[Id] END ASC,
									CASE WHEN @SortField = 'Title' AND @Direction = 'DESC' THEN [P].[Title] END DESC,
									CASE WHEN @SortField = 'Title' AND @Direction = 'ASC' THEN [P].[Title] END ASC,
									CASE WHEN @SortField = 'Code' AND @Direction = 'DESC' THEN [P].[Code] END DESC,
									CASE WHEN @SortField = 'Code' AND @Direction = 'ASC' THEN [P].[Code] END ASC,
									CASE WHEN @SortField = 'CountUnitTitle' AND @Direction = 'DESC' THEN [CU].[Title] END DESC,
									CASE WHEN @SortField = 'CountUnitTitle' AND @Direction = 'ASC' THEN [CU].[Title] END ASC,
									CASE WHEN @SortField = 'CountUnitTitle' AND @Direction = 'DESC' THEN [CU].[Title] END DESC,
									CASE WHEN @SortField = 'CountUnitTitle' AND @Direction = 'ASC' THEN [CU].[Title] END ASC,
									CASE WHEN @SortField = 'CategoryTitle' AND @Direction = 'DESC' THEN [C].[Title] END DESC,
									CASE WHEN @SortField = 'CategoryTitle' AND @Direction = 'ASC' THEN [C].[Title] END ASC,
									CASE WHEN @SortField = 'HasSerial' AND @Direction = 'DESC' THEN [P].[HasSerial] END DESC,
									CASE WHEN @SortField = 'HasSerial' AND @Direction = 'ASC' THEN [P].[HasSerial] END ASC
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
				var Command = @"INSERT INTO [Part].[Part]
												(
													[CreatedBy],
													[CreatedDateTime],
													[Title],
													[Code],
													[MainCountUnitId],
													[SecondaryCountUnitId],
													[CategoryId],
													[HasSerial]
												)
											OUTPUT INSERTED.Id
											VALUES
												(
													@CreatedBy,
													@CreatedDateTime,
													@Title,
													@Code,
													@MainCountUnitId,
													@SecondaryCountUnitId,
													@CategoryId,
													@HasSerial
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
				var Command = @"UPDATE [Part].[Part] SET
									[UpdatedBy] = @UpdatedBy,
									[UpdatedDateTime] = @UpdatedDateTime,
									[Title] = @Title,
									[Code] = @Code,
									[MainCountUnitId] = @MainCountUnitId,
									[SecondaryCountUnitId] = @SecondaryCountUnitId,
									[CategoryId] = @CategoryId,
									[HasSerial] = @HasSerial
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
				var Command = @"Delete [Part].[Part] WHERE [Id] = @Id ";

				return await Connection.ExecuteAsync(Command, new {Id}, transaction: Transaction);
			}
			catch { throw; }
			#endregion
		}

		public virtual async Task<IEnumerable<TEntity>> GetPartCountUnitList(bool? EditMode = null)
		{
			#region GetPartCountUnitList
			try
			{
				var Command = @"SELECT
									[CU].[Id],
									[CU].[Title],
									CAST(1 AS BIT) Editable,
									ItemCount = COUNT(*) OVER()
								FROM
									[Part].[CountUnit] [CU] ";

				return await Connection.QueryAsync<TEntity>(Command, new { EditMode }, transaction: Transaction);
			}
			catch { throw; }
			#endregion
		}

		public virtual async Task<IEnumerable<TEntity>> GetPartCategoryList(bool? EditMode = null)
		{
			#region GetPartCategoryList
			try
			{
				var Command = @"SELECT
									[C].[Id],
									[C].[Title],
									CAST(1 AS BIT) Editable,
									ItemCount = COUNT(*) OVER()
								FROM
									[Part].[Category] [C] ";

				return await Connection.QueryAsync<TEntity>(Command, new { EditMode }, transaction: Transaction);
			}
			catch { throw; }
			#endregion
		}

	}
}
