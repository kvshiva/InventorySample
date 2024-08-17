using Dapper;
using Microsoft.Data.SqlClient;
using InventorySampleServer.DAL._Base;

namespace InventorySampleServer.DAL._Gen.Part
{
	public class GCountUnitDAL<TEntity> : BaseDAL<TEntity> where TEntity : class
	{
		public GCountUnitDAL(SqlConnection? Connection, SqlTransaction? Transaction) : base(Connection, Transaction) { }

		public override async Task<TEntity> GetById(int Id)
		{
			#region GetById
			try
			{
				var Command = @"SELECT
									[CU].[Id],
									[CU].[Version],
									[CU].[CreatedBy],
									[CU].[CreatedDateTime],
									[CU].[UpdatedBy],
									[CU].[UpdatedDateTime],
									[CU].[Title],
									CAST(1 AS BIT) Editable
								FROM
									[Part].[CountUnit] [CU]
								WHERE
									[CU].[Id] = @Id ";

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
									[CU].[Id],
									[CU].[Version],
									[CU].[CreatedBy],
									[CU].[CreatedDateTime],
									[CU].[UpdatedBy],
									[CU].[UpdatedDateTime],
									[CU].[Title]
								FROM
									[Part].[CountUnit] [CU] 
								WHERE
									[CU].[Id] = @Id ";

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
									[CU].[Id],
									[CU].[Version],
									[CU].[CreatedBy],
									[CU].[CreatedDateTime],
									[CU].[UpdatedBy],
									[CU].[UpdatedDateTime],
									[CU].[Title],
									CAST(1 AS BIT) Editable,
									ItemCount = COUNT(*) OVER()
								FROM
									[Part].[CountUnit] [CU]";

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
									[CU].[Id],
									[CU].[Version],
									[CU].[CreatedBy],
									[CU].[CreatedDateTime],
									[CU].[UpdatedBy],
									[CU].[UpdatedDateTime],
									[CU].[Title],
									CAST(1 AS BIT) Editable,
									ItemCount = COUNT(*) OVER()
								FROM
									[Part].[CountUnit] [CU]
								WHERE
									(
										@SearchValue IS NULL
										OR
										(
											[CU].[Title] LIKE N'%' + @SearchValue + '%'
											
										)
									)
								ORDER BY
									CASE WHEN @SortField IS NULL THEN [CU].[Id] END ASC,
									CASE WHEN @SortField = 'Title' AND @Direction = 'DESC' THEN [CU].[Title] END DESC,
									CASE WHEN @SortField = 'Title' AND @Direction = 'ASC' THEN [CU].[Title] END ASC
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
				var Command = @"INSERT INTO [Part].[CountUnit]
												(
													[CreatedBy],
													[CreatedDateTime],
													[Title]
												)
											OUTPUT INSERTED.Id
											VALUES
												(
													@CreatedBy,
													@CreatedDateTime,
													@Title
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
				var Command = @"UPDATE [Part].[CountUnit] SET
									[UpdatedBy] = @UpdatedBy,
									[UpdatedDateTime] = @UpdatedDateTime,
									[Title] = @Title
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
				var Command = @"Delete [Part].[CountUnit] WHERE [Id] = @Id ";

				return await Connection.ExecuteAsync(Command, new {Id}, transaction: Transaction);
			}
			catch { throw; }
			#endregion
		}

	}
}
