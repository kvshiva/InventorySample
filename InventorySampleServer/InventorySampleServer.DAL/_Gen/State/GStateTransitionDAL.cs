using Dapper;
using Microsoft.Data.SqlClient;
using InventorySampleServer.DAL._Base;

namespace InventorySampleServer.DAL._Gen.State
{
	public class GStateTransitionDAL<TEntity> : BaseDAL<TEntity> where TEntity : class
	{
		public GStateTransitionDAL(SqlConnection? Connection, SqlTransaction? Transaction) : base(Connection, Transaction) { }

		public override async Task<TEntity> GetById(int Id)
		{
			#region GetById
			try
			{
				var Command = @"SELECT
									[ST].[Id],
									[ST].[Version],
									[ST].[CreatedBy],
									[ST].[CreatedDateTime],
									[ST].[UpdatedBy],
									[ST].[UpdatedDateTime],
									[ST].[Title],
									[ST].[ConfirmMessage],
									[ST].[GetConfirm],
									[ST].[IsAutomatic],
									[ST].[CommentNeeded],
									[ST].[SourceStateId],
									[ST].[TargetStateId],
									CAST(1 AS BIT) Editable
								FROM
									[State].[StateTransition] [ST]
									INNER JOIN [State].[State] [SourceStateS] ON [SourceStateS].[Id] = [ST].[SourceStateId]
									INNER JOIN [State].[State] [TargetStateS] ON [TargetStateS].[Id] = [ST].[TargetStateId]
								WHERE
									[ST].[Id] = @Id ";

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
									[ST].[Id],
									[ST].[Version],
									[ST].[CreatedBy],
									[ST].[CreatedDateTime],
									[ST].[UpdatedBy],
									[ST].[UpdatedDateTime],
									[ST].[Title],
									[ST].[ConfirmMessage],
									[ST].[GetConfirm],
									[ST].[IsAutomatic],
									[ST].[CommentNeeded],
									[ST].[SourceStateId],
									[ST].[TargetStateId]
								FROM
									[State].[StateTransition] [ST] 
								WHERE
									[ST].[Id] = @Id ";

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
									[ST].[Id],
									[ST].[Version],
									[ST].[CreatedBy],
									[ST].[CreatedDateTime],
									[ST].[UpdatedBy],
									[ST].[UpdatedDateTime],
									[ST].[Title],
									[ST].[ConfirmMessage],
									[ST].[GetConfirm],
									[ST].[IsAutomatic],
									[ST].[CommentNeeded],
									[ST].[SourceStateId],
									[ST].[TargetStateId],
									CAST(1 AS BIT) Editable,
									ItemCount = COUNT(*) OVER()
								FROM
									[State].[StateTransition] [ST]
									INNER JOIN [State].[State] [SourceStateS] ON [SourceStateS].[Id] = [ST].[SourceStateId]
									INNER JOIN [State].[State] [TargetStateS] ON [TargetStateS].[Id] = [ST].[TargetStateId]";

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
									[ST].[Id],
									[ST].[Version],
									[ST].[CreatedBy],
									[ST].[CreatedDateTime],
									[ST].[UpdatedBy],
									[ST].[UpdatedDateTime],
									[ST].[Title],
									[ST].[ConfirmMessage],
									[ST].[GetConfirm],
									[ST].[IsAutomatic],
									[ST].[CommentNeeded],
									[ST].[SourceStateId],
									[ST].[TargetStateId],
									CAST(1 AS BIT) Editable,
									ItemCount = COUNT(*) OVER()
								FROM
									[State].[StateTransition] [ST]
									INNER JOIN [State].[State] [SourceStateS] ON [SourceStateS].[Id] = [ST].[SourceStateId]
									INNER JOIN [State].[State] [TargetStateS] ON [TargetStateS].[Id] = [ST].[TargetStateId]
								WHERE
									(
										@SearchValue IS NULL
										OR
										(
											[ST].[Title] LIKE N'%' + @SearchValue + '%'
											OR
											[ST].[ConfirmMessage] LIKE N'%' + @SearchValue + '%'
											OR
											[ST].[GetConfirm] LIKE N'%' + @SearchValue + '%'
											OR
											[ST].[IsAutomatic] LIKE N'%' + @SearchValue + '%'
											OR
											[ST].[CommentNeeded] LIKE N'%' + @SearchValue + '%'
											
										)
									)
								ORDER BY
									CASE WHEN @SortField IS NULL THEN [ST].[Id] END ASC,
									CASE WHEN @SortField = 'Title' AND @Direction = 'DESC' THEN [ST].[Title] END DESC,
									CASE WHEN @SortField = 'Title' AND @Direction = 'ASC' THEN [ST].[Title] END ASC,
									CASE WHEN @SortField = 'ConfirmMessage' AND @Direction = 'DESC' THEN [ST].[ConfirmMessage] END DESC,
									CASE WHEN @SortField = 'ConfirmMessage' AND @Direction = 'ASC' THEN [ST].[ConfirmMessage] END ASC,
									CASE WHEN @SortField = 'GetConfirm' AND @Direction = 'DESC' THEN [ST].[GetConfirm] END DESC,
									CASE WHEN @SortField = 'GetConfirm' AND @Direction = 'ASC' THEN [ST].[GetConfirm] END ASC,
									CASE WHEN @SortField = 'IsAutomatic' AND @Direction = 'DESC' THEN [ST].[IsAutomatic] END DESC,
									CASE WHEN @SortField = 'IsAutomatic' AND @Direction = 'ASC' THEN [ST].[IsAutomatic] END ASC,
									CASE WHEN @SortField = 'CommentNeeded' AND @Direction = 'DESC' THEN [ST].[CommentNeeded] END DESC,
									CASE WHEN @SortField = 'CommentNeeded' AND @Direction = 'ASC' THEN [ST].[CommentNeeded] END ASC
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
				var Command = @"INSERT INTO [State].[StateTransition]
												(
													[CreatedBy],
													[CreatedDateTime],
													[Title],
													[ConfirmMessage],
													[GetConfirm],
													[IsAutomatic],
													[CommentNeeded],
													[SourceStateId],
													[TargetStateId]
												)
											OUTPUT INSERTED.Id
											VALUES
												(
													@CreatedBy,
													@CreatedDateTime,
													@Title,
													@ConfirmMessage,
													@GetConfirm,
													@IsAutomatic,
													@CommentNeeded,
													@SourceStateId,
													@TargetStateId
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
				var Command = @"UPDATE [State].[StateTransition] SET
									[UpdatedBy] = @UpdatedBy,
									[UpdatedDateTime] = @UpdatedDateTime,
									[Title] = @Title,
									[ConfirmMessage] = @ConfirmMessage,
									[GetConfirm] = @GetConfirm,
									[IsAutomatic] = @IsAutomatic,
									[CommentNeeded] = @CommentNeeded,
									[SourceStateId] = @SourceStateId,
									[TargetStateId] = @TargetStateId
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
				var Command = @"Delete [State].[StateTransition] WHERE [Id] = @Id ";

				return await Connection.ExecuteAsync(Command, new {Id}, transaction: Transaction);
			}
			catch { throw; }
			#endregion
		}

		public virtual async Task<IEnumerable<TEntity>> GetStateTransitionStateList(bool? EditMode = null)
		{
			#region GetStateTransitionStateList
			try
			{
				var Command = @"SELECT
									[S].[Id],
									CAST(1 AS BIT) Editable,
									ItemCount = COUNT(*) OVER()
								FROM
									[State].[State] [S] ";

				return await Connection.QueryAsync<TEntity>(Command, new { EditMode }, transaction: Transaction);
			}
			catch { throw; }
			#endregion
		}

	}
}
