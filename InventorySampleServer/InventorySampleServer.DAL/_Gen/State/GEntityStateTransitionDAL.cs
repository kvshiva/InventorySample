using Dapper;
using Microsoft.Data.SqlClient;
using InventorySampleServer.DAL._Base;

namespace InventorySampleServer.DAL._Gen.State
{
	public class GEntityStateTransitionDAL<TEntity> : BaseDAL<TEntity> where TEntity : class
	{
		public GEntityStateTransitionDAL(SqlConnection? Connection, SqlTransaction? Transaction) : base(Connection, Transaction) { }

		public override async Task<TEntity> GetById(int Id)
		{
			#region GetById
			try
			{
				var Command = @"SELECT
									[EST].[Id],
									[EST].[Version],
									[EST].[CreatedBy],
									[EST].[CreatedDateTime],
									[EST].[UpdatedBy],
									[EST].[UpdatedDateTime],
									[EE].[Title] [EntityEnumTitle],
									[EST].[EntityEnumId],
									[ST].[Title] [StateTransitionTitle],
									[EST].[StateTransitionId],
									[U].[FullName] [UserFullName],
									[EST].[UserId],
									[EST].[RecordId],
									[EST].[Comment],
									[EST].[Date],
									[EST].[Time],
									[SE].[Title] [StateEnumTitle],
									[EST].[StateEnumId],
									[EST].[RecordDateTime],
									CAST(1 AS BIT) Editable
								FROM
									[State].[EntityStateTransition] [EST]
									INNER JOIN [State].[EntityEnum] [EE] ON [EE].[Id] = [EST].[EntityEnumId]
									INNER JOIN [State].[StateTransition] [ST] ON [ST].[Id] = [EST].[StateTransitionId]
									LEFT  JOIN [General].[User] [U] ON [U].[Id] = [EST].[UserId]
									LEFT  JOIN [State].[StateEnum] [SE] ON [SE].[Id] = [EST].[StateEnumId]
								WHERE
									[EST].[Id] = @Id ";

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
									[EST].[Id],
									[EST].[Version],
									[EST].[CreatedBy],
									[EST].[CreatedDateTime],
									[EST].[UpdatedBy],
									[EST].[UpdatedDateTime],
									[EST].[EntityEnumId],
									[EST].[StateTransitionId],
									[EST].[UserId],
									[EST].[RecordId],
									[EST].[Comment],
									[EST].[Date],
									[EST].[Time],
									[EST].[StateEnumId],
									[EST].[RecordDateTime]
								FROM
									[State].[EntityStateTransition] [EST] 
								WHERE
									[EST].[Id] = @Id ";

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
									[EST].[Id],
									[EST].[Version],
									[EST].[CreatedBy],
									[EST].[CreatedDateTime],
									[EST].[UpdatedBy],
									[EST].[UpdatedDateTime],
									[EE].[Title] [EntityEnumTitle],
									[EST].[EntityEnumId],
									[ST].[Title] [StateTransitionTitle],
									[EST].[StateTransitionId],
									[U].[FullName] [UserFullName],
									[EST].[UserId],
									[EST].[RecordId],
									[EST].[Comment],
									[EST].[Date],
									[EST].[Time],
									[SE].[Title] [StateEnumTitle],
									[EST].[StateEnumId],
									[EST].[RecordDateTime],
									CAST(1 AS BIT) Editable,
									ItemCount = COUNT(*) OVER()
								FROM
									[State].[EntityStateTransition] [EST]
									INNER JOIN [State].[EntityEnum] [EE] ON [EE].[Id] = [EST].[EntityEnumId]
									INNER JOIN [State].[StateTransition] [ST] ON [ST].[Id] = [EST].[StateTransitionId]
									LEFT  JOIN [General].[User] [U] ON [U].[Id] = [EST].[UserId]
									LEFT  JOIN [State].[StateEnum] [SE] ON [SE].[Id] = [EST].[StateEnumId]";

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
									[EST].[Id],
									[EST].[Version],
									[EST].[CreatedBy],
									[EST].[CreatedDateTime],
									[EST].[UpdatedBy],
									[EST].[UpdatedDateTime],
									[EE].[Title] [EntityEnumTitle],
									[EST].[EntityEnumId],
									[ST].[Title] [StateTransitionTitle],
									[EST].[StateTransitionId],
									[U].[FullName] [UserFullName],
									[EST].[UserId],
									[EST].[RecordId],
									[EST].[Comment],
									[EST].[Date],
									[EST].[Time],
									[SE].[Title] [StateEnumTitle],
									[EST].[StateEnumId],
									[EST].[RecordDateTime],
									CAST(1 AS BIT) Editable,
									ItemCount = COUNT(*) OVER()
								FROM
									[State].[EntityStateTransition] [EST]
									INNER JOIN [State].[EntityEnum] [EE] ON [EE].[Id] = [EST].[EntityEnumId]
									INNER JOIN [State].[StateTransition] [ST] ON [ST].[Id] = [EST].[StateTransitionId]
									LEFT  JOIN [General].[User] [U] ON [U].[Id] = [EST].[UserId]
									LEFT  JOIN [State].[StateEnum] [SE] ON [SE].[Id] = [EST].[StateEnumId]
								WHERE
									(
										@SearchValue IS NULL
										OR
										(
											[EE].[Title] LIKE N'%' + @SearchValue + '%'
											OR
											[ST].[Title] LIKE N'%' + @SearchValue + '%'
											OR
											[U].[FullName] LIKE N'%' + @SearchValue + '%'
											OR
											[EST].[RecordId] LIKE N'%' + @SearchValue + '%'
											OR
											[EST].[Comment] LIKE N'%' + @SearchValue + '%'
											OR
											[EST].[Date] LIKE N'%' + @SearchValue + '%'
											OR
											[EST].[Time] LIKE N'%' + @SearchValue + '%'
											OR
											[SE].[Title] LIKE N'%' + @SearchValue + '%'
											OR
											[EST].[RecordDateTime] LIKE N'%' + @SearchValue + '%'
											
										)
									)
								ORDER BY
									CASE WHEN @SortField IS NULL THEN [EST].[Id] END ASC,
									CASE WHEN @SortField = 'EntityEnumTitle' AND @Direction = 'DESC' THEN [EE].[Title] END DESC,
									CASE WHEN @SortField = 'EntityEnumTitle' AND @Direction = 'ASC' THEN [EE].[Title] END ASC,
									CASE WHEN @SortField = 'StateTransitionTitle' AND @Direction = 'DESC' THEN [ST].[Title] END DESC,
									CASE WHEN @SortField = 'StateTransitionTitle' AND @Direction = 'ASC' THEN [ST].[Title] END ASC,
									CASE WHEN @SortField = 'UserFullName' AND @Direction = 'DESC' THEN [U].[FullName] END DESC,
									CASE WHEN @SortField = 'UserFullName' AND @Direction = 'ASC' THEN [U].[FullName] END ASC,
									CASE WHEN @SortField = 'RecordId' AND @Direction = 'DESC' THEN [EST].[RecordId] END DESC,
									CASE WHEN @SortField = 'RecordId' AND @Direction = 'ASC' THEN [EST].[RecordId] END ASC,
									CASE WHEN @SortField = 'Comment' AND @Direction = 'DESC' THEN [EST].[Comment] END DESC,
									CASE WHEN @SortField = 'Comment' AND @Direction = 'ASC' THEN [EST].[Comment] END ASC,
									CASE WHEN @SortField = 'Date' AND @Direction = 'DESC' THEN [EST].[Date] END DESC,
									CASE WHEN @SortField = 'Date' AND @Direction = 'ASC' THEN [EST].[Date] END ASC,
									CASE WHEN @SortField = 'Time' AND @Direction = 'DESC' THEN [EST].[Time] END DESC,
									CASE WHEN @SortField = 'Time' AND @Direction = 'ASC' THEN [EST].[Time] END ASC,
									CASE WHEN @SortField = 'StateEnumTitle' AND @Direction = 'DESC' THEN [SE].[Title] END DESC,
									CASE WHEN @SortField = 'StateEnumTitle' AND @Direction = 'ASC' THEN [SE].[Title] END ASC,
									CASE WHEN @SortField = 'RecordDateTime' AND @Direction = 'DESC' THEN [EST].[RecordDateTime] END DESC,
									CASE WHEN @SortField = 'RecordDateTime' AND @Direction = 'ASC' THEN [EST].[RecordDateTime] END ASC
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
				var Command = @"INSERT INTO [State].[EntityStateTransition]
												(
													[CreatedBy],
													[CreatedDateTime],
													[EntityEnumId],
													[StateTransitionId],
													[UserId],
													[RecordId],
													[Comment],
													[Date],
													[Time],
													[StateEnumId],
													[RecordDateTime]
												)
											OUTPUT INSERTED.Id
											VALUES
												(
													@CreatedBy,
													@CreatedDateTime,
													@EntityEnumId,
													@StateTransitionId,
													@UserId,
													@RecordId,
													@Comment,
													@Date,
													@Time,
													@StateEnumId,
													@RecordDateTime
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
				var Command = @"UPDATE [State].[EntityStateTransition] SET
									[UpdatedBy] = @UpdatedBy,
									[UpdatedDateTime] = @UpdatedDateTime,
									[EntityEnumId] = @EntityEnumId,
									[StateTransitionId] = @StateTransitionId,
									[UserId] = @UserId,
									[RecordId] = @RecordId,
									[Comment] = @Comment,
									[Date] = @Date,
									[Time] = @Time,
									[StateEnumId] = @StateEnumId,
									[RecordDateTime] = @RecordDateTime
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
				var Command = @"Delete [State].[EntityStateTransition] WHERE [Id] = @Id ";

				return await Connection.ExecuteAsync(Command, new {Id}, transaction: Transaction);
			}
			catch { throw; }
			#endregion
		}

		public virtual async Task<IEnumerable<TEntity>> GetEntityStateTransitionEntityEnumList(bool? EditMode = null)
		{
			#region GetEntityStateTransitionEntityEnumList
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

		public virtual async Task<IEnumerable<TEntity>> GetEntityStateTransitionStateTransitionList(bool? EditMode = null)
		{
			#region GetEntityStateTransitionStateTransitionList
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

		public virtual async Task<IEnumerable<TEntity>> GetEntityStateTransitionUserList(bool? EditMode = null)
		{
			#region GetEntityStateTransitionUserList
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

		public virtual async Task<IEnumerable<TEntity>> GetEntityStateTransitionStateEnumList(bool? EditMode = null)
		{
			#region GetEntityStateTransitionStateEnumList
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
									[EST].[StateEnumId],
									[SE].[Title] [StateEnumTitle]
								FROM
									[State].[EntityStateTransition] [EST]
									LEFT  JOIN [State].[StateEnum] [SE] ON [SE].[Id] = [EST].[StateEnumId]
								WHERE
									[EST].[Id] = @Id ";

				return await Connection.QueryFirstOrDefaultAsync<TEntity>(Command, new {Id}, transaction: Transaction);
			}
			catch { throw; }
			#endregion
		}
	}
}
