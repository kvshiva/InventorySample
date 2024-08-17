using Dapper;
using Microsoft.Data.SqlClient;
using InventorySampleServer.DAL._Base;

namespace InventorySampleServer.DAL._Gen.State
{
	public class GStateMachineUserAccessDAL<TEntity> : BaseDAL<TEntity> where TEntity : class
	{
		public GStateMachineUserAccessDAL(SqlConnection? Connection, SqlTransaction? Transaction) : base(Connection, Transaction) { }

		public override async Task<TEntity> GetById(int Id)
		{
			#region GetById
			try
			{
				var Command = @"SELECT
									[SMUA].[Id],
									[SMUA].[Version],
									[SMUA].[CreatedBy],
									[SMUA].[CreatedDateTime],
									[SMUA].[UpdatedBy],
									[SMUA].[UpdatedDateTime],
									[ST].[Title] [StateTransitionTitle],
									[SMUA].[StateTransitionId],
									[EE].[Title] [EntityEnumTitle],
									[SMUA].[EntityEnumId],
									[U].[FullName] [UserFullName],
									[SMUA].[UserId],
									[ORTE].[Title] [OperationRoleTypeEnumTitle],
									[SMUA].[OperationRoleTypeEnumId],
									CAST(1 AS BIT) Editable
								FROM
									[State].[StateMachineUserAccess] [SMUA]
									INNER JOIN [State].[StateTransition] [ST] ON [ST].[Id] = [SMUA].[StateTransitionId]
									INNER JOIN [State].[EntityEnum] [EE] ON [EE].[Id] = [SMUA].[EntityEnumId]
									LEFT  JOIN [General].[User] [U] ON [U].[Id] = [SMUA].[UserId]
									LEFT  JOIN [General].[OperationRoleTypeEnum] [ORTE] ON [ORTE].[Id] = [SMUA].[OperationRoleTypeEnumId]
								WHERE
									[SMUA].[Id] = @Id ";

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
									[SMUA].[Id],
									[SMUA].[Version],
									[SMUA].[CreatedBy],
									[SMUA].[CreatedDateTime],
									[SMUA].[UpdatedBy],
									[SMUA].[UpdatedDateTime],
									[SMUA].[StateTransitionId],
									[SMUA].[EntityEnumId],
									[SMUA].[UserId],
									[SMUA].[OperationRoleTypeEnumId]
								FROM
									[State].[StateMachineUserAccess] [SMUA] 
								WHERE
									[SMUA].[Id] = @Id ";

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
									[SMUA].[Id],
									[SMUA].[Version],
									[SMUA].[CreatedBy],
									[SMUA].[CreatedDateTime],
									[SMUA].[UpdatedBy],
									[SMUA].[UpdatedDateTime],
									[ST].[Title] [StateTransitionTitle],
									[SMUA].[StateTransitionId],
									[EE].[Title] [EntityEnumTitle],
									[SMUA].[EntityEnumId],
									[U].[FullName] [UserFullName],
									[SMUA].[UserId],
									[ORTE].[Title] [OperationRoleTypeEnumTitle],
									[SMUA].[OperationRoleTypeEnumId],
									CAST(1 AS BIT) Editable,
									ItemCount = COUNT(*) OVER()
								FROM
									[State].[StateMachineUserAccess] [SMUA]
									INNER JOIN [State].[StateTransition] [ST] ON [ST].[Id] = [SMUA].[StateTransitionId]
									INNER JOIN [State].[EntityEnum] [EE] ON [EE].[Id] = [SMUA].[EntityEnumId]
									LEFT  JOIN [General].[User] [U] ON [U].[Id] = [SMUA].[UserId]
									LEFT  JOIN [General].[OperationRoleTypeEnum] [ORTE] ON [ORTE].[Id] = [SMUA].[OperationRoleTypeEnumId]";

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
									[SMUA].[Id],
									[SMUA].[Version],
									[SMUA].[CreatedBy],
									[SMUA].[CreatedDateTime],
									[SMUA].[UpdatedBy],
									[SMUA].[UpdatedDateTime],
									[ST].[Title] [StateTransitionTitle],
									[SMUA].[StateTransitionId],
									[EE].[Title] [EntityEnumTitle],
									[SMUA].[EntityEnumId],
									[U].[FullName] [UserFullName],
									[SMUA].[UserId],
									[ORTE].[Title] [OperationRoleTypeEnumTitle],
									[SMUA].[OperationRoleTypeEnumId],
									CAST(1 AS BIT) Editable,
									ItemCount = COUNT(*) OVER()
								FROM
									[State].[StateMachineUserAccess] [SMUA]
									INNER JOIN [State].[StateTransition] [ST] ON [ST].[Id] = [SMUA].[StateTransitionId]
									INNER JOIN [State].[EntityEnum] [EE] ON [EE].[Id] = [SMUA].[EntityEnumId]
									LEFT  JOIN [General].[User] [U] ON [U].[Id] = [SMUA].[UserId]
									LEFT  JOIN [General].[OperationRoleTypeEnum] [ORTE] ON [ORTE].[Id] = [SMUA].[OperationRoleTypeEnumId]
								WHERE
									(
										@SearchValue IS NULL
										OR
										(
											[ST].[Title] LIKE N'%' + @SearchValue + '%'
											OR
											[EE].[Title] LIKE N'%' + @SearchValue + '%'
											OR
											[U].[FullName] LIKE N'%' + @SearchValue + '%'
											OR
											[ORTE].[Title] LIKE N'%' + @SearchValue + '%'
											
										)
									)
								ORDER BY
									CASE WHEN @SortField IS NULL THEN [SMUA].[Id] END ASC,
									CASE WHEN @SortField = 'StateTransitionTitle' AND @Direction = 'DESC' THEN [ST].[Title] END DESC,
									CASE WHEN @SortField = 'StateTransitionTitle' AND @Direction = 'ASC' THEN [ST].[Title] END ASC,
									CASE WHEN @SortField = 'EntityEnumTitle' AND @Direction = 'DESC' THEN [EE].[Title] END DESC,
									CASE WHEN @SortField = 'EntityEnumTitle' AND @Direction = 'ASC' THEN [EE].[Title] END ASC,
									CASE WHEN @SortField = 'UserFullName' AND @Direction = 'DESC' THEN [U].[FullName] END DESC,
									CASE WHEN @SortField = 'UserFullName' AND @Direction = 'ASC' THEN [U].[FullName] END ASC,
									CASE WHEN @SortField = 'OperationRoleTypeEnumTitle' AND @Direction = 'DESC' THEN [ORTE].[Title] END DESC,
									CASE WHEN @SortField = 'OperationRoleTypeEnumTitle' AND @Direction = 'ASC' THEN [ORTE].[Title] END ASC
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
				var Command = @"INSERT INTO [State].[StateMachineUserAccess]
												(
													[CreatedBy],
													[CreatedDateTime],
													[StateTransitionId],
													[EntityEnumId],
													[UserId],
													[OperationRoleTypeEnumId]
												)
											OUTPUT INSERTED.Id
											VALUES
												(
													@CreatedBy,
													@CreatedDateTime,
													@StateTransitionId,
													@EntityEnumId,
													@UserId,
													@OperationRoleTypeEnumId
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
				var Command = @"UPDATE [State].[StateMachineUserAccess] SET
									[UpdatedBy] = @UpdatedBy,
									[UpdatedDateTime] = @UpdatedDateTime,
									[StateTransitionId] = @StateTransitionId,
									[EntityEnumId] = @EntityEnumId,
									[UserId] = @UserId,
									[OperationRoleTypeEnumId] = @OperationRoleTypeEnumId
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
				var Command = @"Delete [State].[StateMachineUserAccess] WHERE [Id] = @Id ";

				return await Connection.ExecuteAsync(Command, new {Id}, transaction: Transaction);
			}
			catch { throw; }
			#endregion
		}

		public virtual async Task<IEnumerable<TEntity>> GetStateMachineUserAccessStateTransitionList(bool? EditMode = null)
		{
			#region GetStateMachineUserAccessStateTransitionList
			try
			{
				var Command = @"SELECT
									[ST].[Id],
									[ST].[Title],
									CAST(1 AS BIT) Editable,
									ItemCount = COUNT(*) OVER()
								FROM
									[State].[StateTransition] [ST] ";

				return await Connection.QueryAsync<TEntity>(Command, new { EditMode }, transaction: Transaction);
			}
			catch { throw; }
			#endregion
		}

		public virtual async Task<IEnumerable<TEntity>> GetStateMachineUserAccessEntityEnumList(bool? EditMode = null)
		{
			#region GetStateMachineUserAccessEntityEnumList
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

		public virtual async Task<IEnumerable<TEntity>> GetStateMachineUserAccessUserList(bool? EditMode = null)
		{
			#region GetStateMachineUserAccessUserList
			try
			{
				var Command = @"SELECT
									[U].[Id],
									[U].[FullName],
									CAST(1 AS BIT) Editable,
									ItemCount = COUNT(*) OVER()
								FROM
									[General].[User] [U] ";

				return await Connection.QueryAsync<TEntity>(Command, new { EditMode }, transaction: Transaction);
			}
			catch { throw; }
			#endregion
		}

		public virtual async Task<IEnumerable<TEntity>> GetStateMachineUserAccessOperationRoleTypeEnumList(bool? EditMode = null)
		{
			#region GetStateMachineUserAccessOperationRoleTypeEnumList
			try
			{
				var Command = @"SELECT
									[ORTE].[Id],
									[ORTE].[Title],
									CAST(1 AS BIT) Editable,
									ItemCount = COUNT(*) OVER()
								FROM
									[General].[OperationRoleTypeEnum] [ORTE] ";

				return await Connection.QueryAsync<TEntity>(Command, new { EditMode }, transaction: Transaction);
			}
			catch { throw; }
			#endregion
		}

	}
}
