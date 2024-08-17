using Dapper;
using Microsoft.Data.SqlClient;
using InventorySampleServer.DAL._Base;

namespace InventorySampleServer.DAL.Enum
{
	public class EntityEnumDAL<TEntity> : BaseDAL<TEntity> where TEntity : class
	{
		public EntityEnumDAL(SqlConnection? Connection, SqlTransaction? Transaction) : base(Connection, Transaction) { }

		public override async Task<TEntity> GetById(int Id)
		{
			#region GetById
			try
			{
				var Command = @"SELECT
									[Id],
									[Version],
									[CreatedBy],
									[CreatedDateTime],
									[UpdatedBy],
									[UpdatedDateTime],
									[Title],
									[EntitySchema],
									[Prefix],
									[CounterLength]
								FROM
									[State].[EntityEnum]
								WHERE
									[Id] = @Id ";

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
									[Id],
									[Version],
									[CreatedBy],
									[CreatedDateTime],
									[UpdatedBy],
									[UpdatedDateTime],
									[Title],
									[EntitySchema],
									[Prefix],
									[CounterLength]
								FROM
									[State].[EntityEnum]
								WHERE
									[Id] = @Id ";

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
									[Id],
									[Version],
									[CreatedBy],
									[CreatedDateTime],
									[UpdatedBy],
									[UpdatedDateTime],
									[Title],
									[EntitySchema],
									[Prefix],
									[CounterLength],
									ItemCount = COUNT(*) OVER()
								FROM
									[State].[EntityEnum]";

				return await Connection.QueryAsync<TEntity>(Command, new { EditMode }, transaction: Transaction);
			}
			catch { throw; }
			#endregion
		}

		public override async Task<IEnumerable<TEntity>> GetList(string? SearchValue = null, string? SortField = null, string? Direction = null, int? PageNumber = null, int? PageSize = null, int? Language = null, bool? EditMode = null)
		{
			#region GetList By Parameters
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
									[Id],
									[Version],
									[CreatedBy],
									[CreatedDateTime],
									[UpdatedBy],
									[UpdatedDateTime],
									[Title],
									[EntitySchema],
									[Prefix],
									[CounterLength],
									ItemCount = COUNT(*) OVER()
								FROM
									[State].[EntityEnum]
								WHERE
									(
										@SearchValue IS NULL
										OR
										(
											Title LIKE N'%' + @SearchValue + '%'
											OR
											EntitySchema LIKE N'%' + @SearchValue + '%'
											OR
											Prefix LIKE N'%' + @SearchValue + '%'
											OR
											CounterLength LIKE N'%' + @SearchValue + '%'
											
										)
									)
								ORDER BY
									CASE WHEN @SortField IS NULL THEN [Id] END ASC,
									CASE WHEN @SortField = 'Title' AND @Direction = 'DESC' THEN [Title] END DESC,
									CASE WHEN @SortField = 'Title' AND @Direction = 'ASC' THEN [Title] END ASC,
									CASE WHEN @SortField = 'EntitySchema' AND @Direction = 'DESC' THEN [EntitySchema] END DESC,
									CASE WHEN @SortField = 'EntitySchema' AND @Direction = 'ASC' THEN [EntitySchema] END ASC,
									CASE WHEN @SortField = 'Prefix' AND @Direction = 'DESC' THEN [Prefix] END DESC,
									CASE WHEN @SortField = 'Prefix' AND @Direction = 'ASC' THEN [Prefix] END ASC,
									CASE WHEN @SortField = 'CounterLength' AND @Direction = 'DESC' THEN [CounterLength] END DESC,
									CASE WHEN @SortField = 'CounterLength' AND @Direction = 'ASC' THEN [CounterLength] END ASC
								OFFSET @Offset ROWS FETCH NEXT @Size ROWS ONLY ";

				return await Connection.QueryAsync<TEntity>(Command, new {Offset, Size, SearchValue, SortField, Direction, Language, EditMode }, transaction: Transaction);
			}
			catch { throw; }
			#endregion
		}

		public override async Task<int> Add(TEntity Entity)
		{
			#region Add
			try
			{
				var Command = @"INSERT INTO [State].[EntityEnum]
												(
													[Version],
													[CreatedBy],
													[CreatedDateTime],
													[Title],
													[EntitySchema],
													[Prefix],
													[CounterLength]
												)
											OUTPUT INSERTED.Id
											VALUES
												(
													@Version,
													@CreatedBy,
													@CreatedDateTime,
													@Title,
													@EntitySchema,
													@Prefix,
													@CounterLength
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
				var Command = @"UPDATE [State].[EntityEnum] SET
									[Version] = @Version,
									[UpdatedBy] = @UpdatedBy,
									[UpdatedDateTime] = @UpdatedDateTime,
									[Title] = @Title,
									[EntitySchema] = @EntitySchema,
									[Prefix] = @Prefix,
									[CounterLength] = @CounterLength
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
				var Command = @"Delete [State].[EntityEnum] WHERE [Id] = @Id ";

				return await Connection.ExecuteAsync(Command, new {Id}, transaction: Transaction);
			}
			catch { throw; }
			#endregion
		}

	}
}
