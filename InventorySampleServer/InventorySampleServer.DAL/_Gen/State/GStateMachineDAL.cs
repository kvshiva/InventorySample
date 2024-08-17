using Dapper;
using Microsoft.Data.SqlClient;
using InventorySampleServer.DAL._Base;

namespace InventorySampleServer.DAL._Gen.State
{
	public class GStateMachineDAL<TEntity> : BaseDAL<TEntity> where TEntity : class
	{
		public GStateMachineDAL(SqlConnection? Connection, SqlTransaction? Transaction) : base(Connection, Transaction) { }

		public override async Task<TEntity> GetById(int Id)
		{
			#region GetById
			try
			{
				var Command = @"SELECT
									[SM].[Id],
									[SM].[Version],
									[SM].[CreatedBy],
									[SM].[CreatedDateTime],
									[SM].[UpdatedBy],
									[SM].[UpdatedDateTime],
									[SM].[Title],
									[EntityEE].[Title] [EntityTitle],
									[SM].[EntityId],
									[SM].[StateMachineTypeEnumId],
									CAST(1 AS BIT) Editable
								FROM
									[State].[StateMachine] [SM]
									INNER JOIN [State].[EntityEnum] [EntityEE] ON [EntityEE].[Id] = [SM].[EntityId]
									LEFT  JOIN [State].[StateMachineTypeEnum] [SMTE] ON [SMTE].[Id] = [SM].[StateMachineTypeEnumId]
								WHERE
									[SM].[Id] = @Id ";

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
									[SM].[Id],
									[SM].[Version],
									[SM].[CreatedBy],
									[SM].[CreatedDateTime],
									[SM].[UpdatedBy],
									[SM].[UpdatedDateTime],
									[SM].[Title],
									[SM].[EntityId],
									[SM].[StateMachineTypeEnumId]
								FROM
									[State].[StateMachine] [SM] 
								WHERE
									[SM].[Id] = @Id ";

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
									[SM].[Id],
									[SM].[Version],
									[SM].[CreatedBy],
									[SM].[CreatedDateTime],
									[SM].[UpdatedBy],
									[SM].[UpdatedDateTime],
									[SM].[Title],
									[EntityEE].[Title] [EntityTitle],
									[SM].[EntityId],
									[SM].[StateMachineTypeEnumId],
									CAST(1 AS BIT) Editable,
									ItemCount = COUNT(*) OVER()
								FROM
									[State].[StateMachine] [SM]
									INNER JOIN [State].[EntityEnum] [EntityEE] ON [EntityEE].[Id] = [SM].[EntityId]
									LEFT  JOIN [State].[StateMachineTypeEnum] [SMTE] ON [SMTE].[Id] = [SM].[StateMachineTypeEnumId]";

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
									[SM].[Id],
									[SM].[Version],
									[SM].[CreatedBy],
									[SM].[CreatedDateTime],
									[SM].[UpdatedBy],
									[SM].[UpdatedDateTime],
									[SM].[Title],
									[EntityEE].[Title] [EntityTitle],
									[SM].[EntityId],
									[SM].[StateMachineTypeEnumId],
									CAST(1 AS BIT) Editable,
									ItemCount = COUNT(*) OVER()
								FROM
									[State].[StateMachine] [SM]
									INNER JOIN [State].[EntityEnum] [EntityEE] ON [EntityEE].[Id] = [SM].[EntityId]
									LEFT  JOIN [State].[StateMachineTypeEnum] [SMTE] ON [SMTE].[Id] = [SM].[StateMachineTypeEnumId]
								WHERE
									(
										@SearchValue IS NULL
										OR
										(
											[SM].[Title] LIKE N'%' + @SearchValue + '%'
											OR
											[EE].[Title] LIKE N'%' + @SearchValue + '%'
											
										)
									)
								ORDER BY
									CASE WHEN @SortField IS NULL THEN [SM].[Id] END ASC,
									CASE WHEN @SortField = 'Title' AND @Direction = 'DESC' THEN [SM].[Title] END DESC,
									CASE WHEN @SortField = 'Title' AND @Direction = 'ASC' THEN [SM].[Title] END ASC,
									CASE WHEN @SortField = 'EntityEnumTitle' AND @Direction = 'DESC' THEN [EE].[Title] END DESC,
									CASE WHEN @SortField = 'EntityEnumTitle' AND @Direction = 'ASC' THEN [EE].[Title] END ASC
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
				var Command = @"INSERT INTO [State].[StateMachine]
												(
													[CreatedBy],
													[CreatedDateTime],
													[Title],
													[EntityId],
													[StateMachineTypeEnumId]
												)
											OUTPUT INSERTED.Id
											VALUES
												(
													@CreatedBy,
													@CreatedDateTime,
													@Title,
													@EntityId,
													@StateMachineTypeEnumId
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
				var Command = @"UPDATE [State].[StateMachine] SET
									[UpdatedBy] = @UpdatedBy,
									[UpdatedDateTime] = @UpdatedDateTime,
									[Title] = @Title,
									[EntityId] = @EntityId,
									[StateMachineTypeEnumId] = @StateMachineTypeEnumId
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
				var Command = @"Delete [State].[StateMachine] WHERE [Id] = @Id ";

				return await Connection.ExecuteAsync(Command, new {Id}, transaction: Transaction);
			}
			catch { throw; }
			#endregion
		}

		public virtual async Task<IEnumerable<TEntity>> GetStateMachineEntityEnumList(bool? EditMode = null)
		{
			#region GetStateMachineEntityEnumList
			try
			{
				var Command = @"SELECT
									[EE].[Id],
									[EE].[Title],
									CAST(1 AS BIT) Editable,
									ItemCount = COUNT(*) OVER()
								FROM
									[State].[EntityEnum] [EE] ";

				return await Connection.QueryAsync<TEntity>(Command, new { EditMode }, transaction: Transaction);
			}
			catch { throw; }
			#endregion
		}

		public virtual async Task<IEnumerable<TEntity>> GetStateMachineStateMachineTypeEnumList(bool? EditMode = null)
		{
			#region GetStateMachineStateMachineTypeEnumList
			try
			{
				var Command = @"SELECT
									[SMTE].[Id],
									CAST(1 AS BIT) Editable,
									ItemCount = COUNT(*) OVER()
								FROM
									[State].[StateMachineTypeEnum] [SMTE] ";

				return await Connection.QueryAsync<TEntity>(Command, new { EditMode }, transaction: Transaction);
			}
			catch { throw; }
			#endregion
		}

	}
}
