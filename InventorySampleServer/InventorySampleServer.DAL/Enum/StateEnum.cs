using Dapper;
using Microsoft.Data.SqlClient;
using InventorySampleServer.DAL._Base;

namespace InventorySampleServer.DAL.Enum
{
	public class StateEnumDAL<TEntity> : BaseDAL<TEntity> where TEntity : class
	{
		public StateEnumDAL(SqlConnection? Connection, SqlTransaction? Transaction) : base(Connection, Transaction) { }

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
									[Title]
								FROM
									[State].[StateEnum]
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
									[Title]
								FROM
									[State].[StateEnum]
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
									ItemCount = COUNT(*) OVER()
								FROM
									[State].[StateEnum]";

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
									ItemCount = COUNT(*) OVER()
								FROM
									[State].[StateEnum]
								WHERE
									(
										@SearchValue IS NULL
										OR
										(
											Title LIKE N'%' + @SearchValue + '%'
											
										)
									)
								ORDER BY
									CASE WHEN @SortField IS NULL THEN [Id] END ASC,
									CASE WHEN @SortField = 'Title' AND @Direction = 'DESC' THEN [Title] END DESC,
									CASE WHEN @SortField = 'Title' AND @Direction = 'ASC' THEN [Title] END ASC
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
				var Command = @"INSERT INTO [State].[StateEnum]
												(
													[Version],
													[CreatedBy],
													[CreatedDateTime],
													[Title]
												)
											OUTPUT INSERTED.Id
											VALUES
												(
													@Version,
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
				var Command = @"UPDATE [State].[StateEnum] SET
									[Version] = @Version,
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
				var Command = @"Delete [State].[StateEnum] WHERE [Id] = @Id ";

				return await Connection.ExecuteAsync(Command, new {Id}, transaction: Transaction);
			}
			catch { throw; }
			#endregion
		}

	}
}
