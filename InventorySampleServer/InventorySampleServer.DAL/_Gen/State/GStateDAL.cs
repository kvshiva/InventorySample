using Dapper;
using Microsoft.Data.SqlClient;
using InventorySampleServer.DAL._Base;

namespace InventorySampleServer.DAL._Gen.State
{
	public class GStateDAL<TEntity> : BaseDAL<TEntity> where TEntity : class
	{
		public GStateDAL(SqlConnection? Connection, SqlTransaction? Transaction) : base(Connection, Transaction) { }

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
									[SM].[Title] [StateMachineTitle],
									[S].[StateMachineId],
									[S].[CanEdit],
									[S].[CanDelete],
									[S].[IsStartState],
									[S].[IsFinishState],
									[SE].[Title] [StateEnumTitle],
									[S].[StateEnumId],
									[S].[StateOrder],
									CAST(1 AS BIT) Editable
								FROM
									[State].[State] [S]
									INNER JOIN [State].[StateMachine] [SM] ON [SM].[Id] = [S].[StateMachineId]
									INNER JOIN [State].[StateEnum] [SE] ON [SE].[Id] = [S].[StateEnumId]
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
									[S].[StateMachineId],
									[S].[CanEdit],
									[S].[CanDelete],
									[S].[IsStartState],
									[S].[IsFinishState],
									[S].[StateEnumId],
									[S].[StateOrder]
								FROM
									[State].[State] [S] 
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
									[SM].[Title] [StateMachineTitle],
									[S].[StateMachineId],
									[S].[CanEdit],
									[S].[CanDelete],
									[S].[IsStartState],
									[S].[IsFinishState],
									[SE].[Title] [StateEnumTitle],
									[S].[StateEnumId],
									[S].[StateOrder],
									CAST(1 AS BIT) Editable,
									ItemCount = COUNT(*) OVER()
								FROM
									[State].[State] [S]
									INNER JOIN [State].[StateMachine] [SM] ON [SM].[Id] = [S].[StateMachineId]
									INNER JOIN [State].[StateEnum] [SE] ON [SE].[Id] = [S].[StateEnumId]";

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
									[SM].[Title] [StateMachineTitle],
									[S].[StateMachineId],
									[S].[CanEdit],
									[S].[CanDelete],
									[S].[IsStartState],
									[S].[IsFinishState],
									[SE].[Title] [StateEnumTitle],
									[S].[StateEnumId],
									[S].[StateOrder],
									CAST(1 AS BIT) Editable,
									ItemCount = COUNT(*) OVER()
								FROM
									[State].[State] [S]
									INNER JOIN [State].[StateMachine] [SM] ON [SM].[Id] = [S].[StateMachineId]
									INNER JOIN [State].[StateEnum] [SE] ON [SE].[Id] = [S].[StateEnumId]
								WHERE
									(
										@SearchValue IS NULL
										OR
										(
											[SM].[Title] LIKE N'%' + @SearchValue + '%'
											OR
											[S].[CanEdit] LIKE N'%' + @SearchValue + '%'
											OR
											[S].[CanDelete] LIKE N'%' + @SearchValue + '%'
											OR
											[S].[IsStartState] LIKE N'%' + @SearchValue + '%'
											OR
											[S].[IsFinishState] LIKE N'%' + @SearchValue + '%'
											OR
											[SE].[Title] LIKE N'%' + @SearchValue + '%'
											OR
											[S].[StateOrder] LIKE N'%' + @SearchValue + '%'
											
										)
									)
								ORDER BY
									CASE WHEN @SortField IS NULL THEN [S].[Id] END ASC,
									CASE WHEN @SortField = 'StateMachineTitle' AND @Direction = 'DESC' THEN [SM].[Title] END DESC,
									CASE WHEN @SortField = 'StateMachineTitle' AND @Direction = 'ASC' THEN [SM].[Title] END ASC,
									CASE WHEN @SortField = 'CanEdit' AND @Direction = 'DESC' THEN [S].[CanEdit] END DESC,
									CASE WHEN @SortField = 'CanEdit' AND @Direction = 'ASC' THEN [S].[CanEdit] END ASC,
									CASE WHEN @SortField = 'CanDelete' AND @Direction = 'DESC' THEN [S].[CanDelete] END DESC,
									CASE WHEN @SortField = 'CanDelete' AND @Direction = 'ASC' THEN [S].[CanDelete] END ASC,
									CASE WHEN @SortField = 'IsStartState' AND @Direction = 'DESC' THEN [S].[IsStartState] END DESC,
									CASE WHEN @SortField = 'IsStartState' AND @Direction = 'ASC' THEN [S].[IsStartState] END ASC,
									CASE WHEN @SortField = 'IsFinishState' AND @Direction = 'DESC' THEN [S].[IsFinishState] END DESC,
									CASE WHEN @SortField = 'IsFinishState' AND @Direction = 'ASC' THEN [S].[IsFinishState] END ASC,
									CASE WHEN @SortField = 'StateEnumTitle' AND @Direction = 'DESC' THEN [SE].[Title] END DESC,
									CASE WHEN @SortField = 'StateEnumTitle' AND @Direction = 'ASC' THEN [SE].[Title] END ASC,
									CASE WHEN @SortField = 'StateOrder' AND @Direction = 'DESC' THEN [S].[StateOrder] END DESC,
									CASE WHEN @SortField = 'StateOrder' AND @Direction = 'ASC' THEN [S].[StateOrder] END ASC
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
				var Command = @"INSERT INTO [State].[State]
												(
													[CreatedBy],
													[CreatedDateTime],
													[StateMachineId],
													[CanEdit],
													[CanDelete],
													[IsStartState],
													[IsFinishState],
													[StateEnumId],
													[StateOrder]
												)
											OUTPUT INSERTED.Id
											VALUES
												(
													@CreatedBy,
													@CreatedDateTime,
													@StateMachineId,
													@CanEdit,
													@CanDelete,
													@IsStartState,
													@IsFinishState,
													@StateEnumId,
													@StateOrder
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
				var Command = @"UPDATE [State].[State] SET
									[UpdatedBy] = @UpdatedBy,
									[UpdatedDateTime] = @UpdatedDateTime,
									[StateMachineId] = @StateMachineId,
									[CanEdit] = @CanEdit,
									[CanDelete] = @CanDelete,
									[IsStartState] = @IsStartState,
									[IsFinishState] = @IsFinishState,
									[StateEnumId] = @StateEnumId,
									[StateOrder] = @StateOrder
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
				var Command = @"Delete [State].[State] WHERE [Id] = @Id ";

				return await Connection.ExecuteAsync(Command, new {Id}, transaction: Transaction);
			}
			catch { throw; }
			#endregion
		}

		public virtual async Task<IEnumerable<TEntity>> GetStateStateMachineList(bool? EditMode = null)
		{
			#region GetStateStateMachineList
			try
			{
				var Command = @"SELECT
									[SM].[Id],
									[SM].[Title],
									CAST(1 AS BIT) Editable,
									ItemCount = COUNT(*) OVER()
								FROM
									[State].[StateMachine] [SM] ";

				return await Connection.QueryAsync<TEntity>(Command, new { EditMode }, transaction: Transaction);
			}
			catch { throw; }
			#endregion
		}

		public virtual async Task<IEnumerable<TEntity>> GetStateStateEnumList(bool? EditMode = null)
		{
			#region GetStateStateEnumList
			try
			{
				var Command = @"SELECT
									[SE].[Id],
									[SE].[Title],
									CAST(1 AS BIT) Editable,
									ItemCount = COUNT(*) OVER()
								FROM
									[State].[StateEnum] [SE] ";

				return await Connection.QueryAsync<TEntity>(Command, new { EditMode }, transaction: Transaction);
			}
			catch { throw; }
			#endregion
		}

		public virtual async Task<TEntity> GetLastStateById(int Id)
		{
			#region GetLastStateById
			try
			{
				var Command = @"SELECT
									[S].[StateEnumId],
									[SE].[Title] [StateEnumTitle]
								FROM
									[State].[State] [S]
									INNER JOIN [State].[StateEnum] [SE] ON [SE].[Id] = [S].[StateEnumId]
								WHERE
									[S].[Id] = @Id ";

				return await Connection.QueryFirstOrDefaultAsync<TEntity>(Command, new {Id}, transaction: Transaction);
			}
			catch { throw; }
			#endregion
		}
	}
}
