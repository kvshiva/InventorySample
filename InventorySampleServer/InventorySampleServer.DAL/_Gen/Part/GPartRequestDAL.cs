using Dapper;
using Microsoft.Data.SqlClient;
using InventorySampleServer.DAL._Base;

namespace InventorySampleServer.DAL._Gen.Part
{
	public class GPartRequestDAL<TEntity> : BaseDAL<TEntity> where TEntity : class
	{
		public GPartRequestDAL(SqlConnection? Connection, SqlTransaction? Transaction) : base(Connection, Transaction) { }

		public override async Task<TEntity> GetById(int Id)
		{
			#region GetById
			try
			{
				var Command = @"SELECT
									[PR].[Id],
									[PR].[Version],
									[PR].[CreatedBy],
									[PR].[CreatedDateTime],
									[PR].[UpdatedBy],
									[PR].[UpdatedDateTime],
									[S].[Title] [StoreTitle],
									[PR].[StoreId],
									[PR].[NeedDate],
									[PR].[DateTime],
									[U].[FullName] [UserFullName],
									[PR].[UserId],
									[PR].[PartRequestNo],
									[SE].[Title] [StateEnumTitle],
									[PR].[StateEnumId],
									[PR].[Comment],
									[PR].[SystemComment],
									[PR].[PersianDate],
									[PR].[Time],
									[PR].[JsonField],
									CAST(1 AS BIT) Editable
								FROM
									[Part].[PartRequest] [PR]
									INNER JOIN [Store].[Store] [S] ON [S].[Id] = [PR].[StoreId]
									INNER JOIN [General].[User] [U] ON [U].[Id] = [PR].[UserId]
									LEFT  JOIN [State].[StateEnum] [SE] ON [SE].[Id] = [PR].[StateEnumId]
								WHERE
									[PR].[Id] = @Id ";

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
									[PR].[Id],
									[PR].[Version],
									[PR].[CreatedBy],
									[PR].[CreatedDateTime],
									[PR].[UpdatedBy],
									[PR].[UpdatedDateTime],
									[PR].[StoreId],
									[PR].[NeedDate],
									[PR].[DateTime],
									[PR].[UserId],
									[PR].[PartRequestNo],
									[PR].[StateEnumId],
									[PR].[Comment],
									[PR].[SystemComment],
									[PR].[PersianDate],
									[PR].[Time],
									[PR].[JsonField]
								FROM
									[Part].[PartRequest] [PR] 
								WHERE
									[PR].[Id] = @Id ";

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
									[PR].[Id],
									[PR].[Version],
									[PR].[CreatedBy],
									[PR].[CreatedDateTime],
									[PR].[UpdatedBy],
									[PR].[UpdatedDateTime],
									[S].[Title] [StoreTitle],
									[PR].[StoreId],
									[PR].[NeedDate],
									[PR].[DateTime],
									[U].[FullName] [UserFullName],
									[PR].[UserId],
									[PR].[PartRequestNo],
									[SE].[Title] [StateEnumTitle],
									[PR].[StateEnumId],
									[PR].[Comment],
									[PR].[SystemComment],
									[PR].[PersianDate],
									[PR].[Time],
									[PR].[JsonField],
									CAST(1 AS BIT) Editable,
									ItemCount = COUNT(*) OVER()
								FROM
									[Part].[PartRequest] [PR]
									INNER JOIN [Store].[Store] [S] ON [S].[Id] = [PR].[StoreId]
									INNER JOIN [General].[User] [U] ON [U].[Id] = [PR].[UserId]
									LEFT  JOIN [State].[StateEnum] [SE] ON [SE].[Id] = [PR].[StateEnumId]";

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
									[PR].[Id],
									[PR].[Version],
									[PR].[CreatedBy],
									[PR].[CreatedDateTime],
									[PR].[UpdatedBy],
									[PR].[UpdatedDateTime],
									[S].[Title] [StoreTitle],
									[PR].[StoreId],
									[PR].[NeedDate],
									[PR].[DateTime],
									[U].[FullName] [UserFullName],
									[PR].[UserId],
									[PR].[PartRequestNo],
									[SE].[Title] [StateEnumTitle],
									[PR].[StateEnumId],
									[PR].[Comment],
									[PR].[SystemComment],
									[PR].[PersianDate],
									[PR].[Time],
									[PR].[JsonField],
									CAST(1 AS BIT) Editable,
									ItemCount = COUNT(*) OVER()
								FROM
									[Part].[PartRequest] [PR]
									INNER JOIN [Store].[Store] [S] ON [S].[Id] = [PR].[StoreId]
									INNER JOIN [General].[User] [U] ON [U].[Id] = [PR].[UserId]
									LEFT  JOIN [State].[StateEnum] [SE] ON [SE].[Id] = [PR].[StateEnumId]
								WHERE
									(
										@SearchValue IS NULL
										OR
										(
											[S].[Title] LIKE N'%' + @SearchValue + '%'
											OR
											[PR].[NeedDate] LIKE N'%' + @SearchValue + '%'
											OR
											[PR].[DateTime] LIKE N'%' + @SearchValue + '%'
											OR
											[U].[FullName] LIKE N'%' + @SearchValue + '%'
											OR
											[PR].[PartRequestNo] LIKE N'%' + @SearchValue + '%'
											OR
											[SE].[Title] LIKE N'%' + @SearchValue + '%'
											OR
											[PR].[Comment] LIKE N'%' + @SearchValue + '%'
											OR
											[PR].[SystemComment] LIKE N'%' + @SearchValue + '%'
											OR
											[PR].[PersianDate] LIKE N'%' + @SearchValue + '%'
											OR
											[PR].[Time] LIKE N'%' + @SearchValue + '%'
											OR
											[PR].[JsonField] LIKE N'%' + @SearchValue + '%'
											
										)
									)
								ORDER BY
									CASE WHEN @SortField IS NULL THEN [PR].[Id] END ASC,
									CASE WHEN @SortField = 'StoreTitle' AND @Direction = 'DESC' THEN [S].[Title] END DESC,
									CASE WHEN @SortField = 'StoreTitle' AND @Direction = 'ASC' THEN [S].[Title] END ASC,
									CASE WHEN @SortField = 'NeedDate' AND @Direction = 'DESC' THEN [PR].[NeedDate] END DESC,
									CASE WHEN @SortField = 'NeedDate' AND @Direction = 'ASC' THEN [PR].[NeedDate] END ASC,
									CASE WHEN @SortField = 'DateTime' AND @Direction = 'DESC' THEN [PR].[DateTime] END DESC,
									CASE WHEN @SortField = 'DateTime' AND @Direction = 'ASC' THEN [PR].[DateTime] END ASC,
									CASE WHEN @SortField = 'UserFullName' AND @Direction = 'DESC' THEN [U].[FullName] END DESC,
									CASE WHEN @SortField = 'UserFullName' AND @Direction = 'ASC' THEN [U].[FullName] END ASC,
									CASE WHEN @SortField = 'PartRequestNo' AND @Direction = 'DESC' THEN [PR].[PartRequestNo] END DESC,
									CASE WHEN @SortField = 'PartRequestNo' AND @Direction = 'ASC' THEN [PR].[PartRequestNo] END ASC,
									CASE WHEN @SortField = 'StateEnumTitle' AND @Direction = 'DESC' THEN [SE].[Title] END DESC,
									CASE WHEN @SortField = 'StateEnumTitle' AND @Direction = 'ASC' THEN [SE].[Title] END ASC,
									CASE WHEN @SortField = 'Comment' AND @Direction = 'DESC' THEN [PR].[Comment] END DESC,
									CASE WHEN @SortField = 'Comment' AND @Direction = 'ASC' THEN [PR].[Comment] END ASC,
									CASE WHEN @SortField = 'SystemComment' AND @Direction = 'DESC' THEN [PR].[SystemComment] END DESC,
									CASE WHEN @SortField = 'SystemComment' AND @Direction = 'ASC' THEN [PR].[SystemComment] END ASC,
									CASE WHEN @SortField = 'PersianDate' AND @Direction = 'DESC' THEN [PR].[PersianDate] END DESC,
									CASE WHEN @SortField = 'PersianDate' AND @Direction = 'ASC' THEN [PR].[PersianDate] END ASC,
									CASE WHEN @SortField = 'Time' AND @Direction = 'DESC' THEN [PR].[Time] END DESC,
									CASE WHEN @SortField = 'Time' AND @Direction = 'ASC' THEN [PR].[Time] END ASC,
									CASE WHEN @SortField = 'JsonField' AND @Direction = 'DESC' THEN [PR].[JsonField] END DESC,
									CASE WHEN @SortField = 'JsonField' AND @Direction = 'ASC' THEN [PR].[JsonField] END ASC
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
				var Command = @"INSERT INTO [Part].[PartRequest]
												(
													[CreatedBy],
													[CreatedDateTime],
													[StoreId],
													[NeedDate],
													[DateTime],
													[UserId],
													[PartRequestNo],
													[StateEnumId],
													[Comment],
													[SystemComment],
													[PersianDate],
													[Time],
													[JsonField]
												)
											OUTPUT INSERTED.Id
											VALUES
												(
													@CreatedBy,
													@CreatedDateTime,
													@StoreId,
													@NeedDate,
													@DateTime,
													@UserId,
													@PartRequestNo,
													@StateEnumId,
													@Comment,
													@SystemComment,
													@PersianDate,
													@Time,
													@JsonField
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
				var Command = @"UPDATE [Part].[PartRequest] SET
									[UpdatedBy] = @UpdatedBy,
									[UpdatedDateTime] = @UpdatedDateTime,
									[StoreId] = @StoreId,
									[NeedDate] = @NeedDate,
									[DateTime] = @DateTime,
									[UserId] = @UserId,
									[PartRequestNo] = @PartRequestNo,
									[StateEnumId] = @StateEnumId,
									[Comment] = @Comment,
									[SystemComment] = @SystemComment,
									[PersianDate] = @PersianDate,
									[Time] = @Time,
									[JsonField] = @JsonField
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
				var Command = @"Delete [Part].[PartRequest] WHERE [Id] = @Id ";

				return await Connection.ExecuteAsync(Command, new {Id}, transaction: Transaction);
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
									[PR].[Id],
									[PR].[Version],
									[PR].[CreatedBy],
									[PR].[CreatedDateTime],
									[PR].[UpdatedBy],
									[PR].[UpdatedDateTime],
									[S].[Title] [StoreTitle],
									[PR].[StoreId],
									[PR].[NeedDate],
									[PR].[DateTime],
									[U].[FullName] [UserFullName],
									[PR].[UserId],
									[PR].[PartRequestNo],
									[SE].[Title] [StateEnumTitle],
									[PR].[StateEnumId],
									[PR].[Comment],
									[PR].[SystemComment],
									[PR].[PersianDate],
									[PR].[Time],
									[PR].[JsonField],
									CAST(1 AS BIT) Editable,
									ItemCount = COUNT(*) OVER()
								FROM
									[Part].[PartRequest] [PR]
									INNER JOIN [Store].[Store] [S] ON [S].[Id] = [PR].[StoreId]
									INNER JOIN [General].[User] [U] ON [U].[Id] = [PR].[UserId]
									LEFT  JOIN [State].[StateEnum] [SE] ON [SE].[Id] = [PR].[StateEnumId]
								WHERE
									[PR].[StoreId] = @StoreId
									AND
									(
										@SearchValue IS NULL
										OR
										(
											[S].[Title] LIKE N'%' + @SearchValue + '%'
											OR
											[PR].[NeedDate] LIKE N'%' + @SearchValue + '%'
											OR
											[PR].[DateTime] LIKE N'%' + @SearchValue + '%'
											OR
											[U].[FullName] LIKE N'%' + @SearchValue + '%'
											OR
											[PR].[PartRequestNo] LIKE N'%' + @SearchValue + '%'
											OR
											[SE].[Title] LIKE N'%' + @SearchValue + '%'
											OR
											[PR].[Comment] LIKE N'%' + @SearchValue + '%'
											OR
											[PR].[SystemComment] LIKE N'%' + @SearchValue + '%'
											OR
											[PR].[PersianDate] LIKE N'%' + @SearchValue + '%'
											OR
											[PR].[Time] LIKE N'%' + @SearchValue + '%'
											OR
											[PR].[JsonField] LIKE N'%' + @SearchValue + '%'
											
										)
									)
								ORDER BY
									CASE WHEN @SortField IS NULL THEN [PR].[Id] END ASC,
									CASE WHEN @SortField = 'StoreTitle' AND @Direction = 'DESC' THEN [S].[Title] END DESC,
									CASE WHEN @SortField = 'StoreTitle' AND @Direction = 'ASC' THEN [S].[Title] END ASC,
									CASE WHEN @SortField = 'NeedDate' AND @Direction = 'DESC' THEN [PR].[NeedDate] END DESC,
									CASE WHEN @SortField = 'NeedDate' AND @Direction = 'ASC' THEN [PR].[NeedDate] END ASC,
									CASE WHEN @SortField = 'DateTime' AND @Direction = 'DESC' THEN [PR].[DateTime] END DESC,
									CASE WHEN @SortField = 'DateTime' AND @Direction = 'ASC' THEN [PR].[DateTime] END ASC,
									CASE WHEN @SortField = 'UserFullName' AND @Direction = 'DESC' THEN [U].[FullName] END DESC,
									CASE WHEN @SortField = 'UserFullName' AND @Direction = 'ASC' THEN [U].[FullName] END ASC,
									CASE WHEN @SortField = 'PartRequestNo' AND @Direction = 'DESC' THEN [PR].[PartRequestNo] END DESC,
									CASE WHEN @SortField = 'PartRequestNo' AND @Direction = 'ASC' THEN [PR].[PartRequestNo] END ASC,
									CASE WHEN @SortField = 'StateEnumTitle' AND @Direction = 'DESC' THEN [SE].[Title] END DESC,
									CASE WHEN @SortField = 'StateEnumTitle' AND @Direction = 'ASC' THEN [SE].[Title] END ASC,
									CASE WHEN @SortField = 'Comment' AND @Direction = 'DESC' THEN [PR].[Comment] END DESC,
									CASE WHEN @SortField = 'Comment' AND @Direction = 'ASC' THEN [PR].[Comment] END ASC,
									CASE WHEN @SortField = 'SystemComment' AND @Direction = 'DESC' THEN [PR].[SystemComment] END DESC,
									CASE WHEN @SortField = 'SystemComment' AND @Direction = 'ASC' THEN [PR].[SystemComment] END ASC,
									CASE WHEN @SortField = 'PersianDate' AND @Direction = 'DESC' THEN [PR].[PersianDate] END DESC,
									CASE WHEN @SortField = 'PersianDate' AND @Direction = 'ASC' THEN [PR].[PersianDate] END ASC,
									CASE WHEN @SortField = 'Time' AND @Direction = 'DESC' THEN [PR].[Time] END DESC,
									CASE WHEN @SortField = 'Time' AND @Direction = 'ASC' THEN [PR].[Time] END ASC,
									CASE WHEN @SortField = 'JsonField' AND @Direction = 'DESC' THEN [PR].[JsonField] END DESC,
									CASE WHEN @SortField = 'JsonField' AND @Direction = 'ASC' THEN [PR].[JsonField] END ASC
								OFFSET @Offset ROWS FETCH NEXT @Size ROWS ONLY ";

				return await Connection.QueryAsync<TEntity>(Command, new {StoreId, Offset, Size, SearchValue, SortField, Direction, Language, EditMode}, transaction: Transaction);
			}
			catch { throw; }
			#endregion
		}

		public virtual async Task<IEnumerable<TEntity>> GetListByUserId(int UserId, string? SearchValue = null, string? SortField = null, string? Direction = null, int? PageNumber = null, int? PageSize = null, int? Language = null, bool? EditMode = null)
		{
			#region GetListByUserId
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
									[PR].[Id],
									[PR].[Version],
									[PR].[CreatedBy],
									[PR].[CreatedDateTime],
									[PR].[UpdatedBy],
									[PR].[UpdatedDateTime],
									[S].[Title] [StoreTitle],
									[PR].[StoreId],
									[PR].[NeedDate],
									[PR].[DateTime],
									[U].[FullName] [UserFullName],
									[PR].[UserId],
									[PR].[PartRequestNo],
									[SE].[Title] [StateEnumTitle],
									[PR].[StateEnumId],
									[PR].[Comment],
									[PR].[SystemComment],
									[PR].[PersianDate],
									[PR].[Time],
									[PR].[JsonField],
									CAST(1 AS BIT) Editable,
									ItemCount = COUNT(*) OVER()
								FROM
									[Part].[PartRequest] [PR]
									INNER JOIN [Store].[Store] [S] ON [S].[Id] = [PR].[StoreId]
									INNER JOIN [General].[User] [U] ON [U].[Id] = [PR].[UserId]
									LEFT  JOIN [State].[StateEnum] [SE] ON [SE].[Id] = [PR].[StateEnumId]
								WHERE
									[PR].[UserId] = @UserId
									AND
									(
										@SearchValue IS NULL
										OR
										(
											[S].[Title] LIKE N'%' + @SearchValue + '%'
											OR
											[PR].[NeedDate] LIKE N'%' + @SearchValue + '%'
											OR
											[PR].[DateTime] LIKE N'%' + @SearchValue + '%'
											OR
											[U].[FullName] LIKE N'%' + @SearchValue + '%'
											OR
											[PR].[PartRequestNo] LIKE N'%' + @SearchValue + '%'
											OR
											[SE].[Title] LIKE N'%' + @SearchValue + '%'
											OR
											[PR].[Comment] LIKE N'%' + @SearchValue + '%'
											OR
											[PR].[SystemComment] LIKE N'%' + @SearchValue + '%'
											OR
											[PR].[PersianDate] LIKE N'%' + @SearchValue + '%'
											OR
											[PR].[Time] LIKE N'%' + @SearchValue + '%'
											OR
											[PR].[JsonField] LIKE N'%' + @SearchValue + '%'
											
										)
									)
								ORDER BY
									CASE WHEN @SortField IS NULL THEN [PR].[Id] END ASC,
									CASE WHEN @SortField = 'StoreTitle' AND @Direction = 'DESC' THEN [S].[Title] END DESC,
									CASE WHEN @SortField = 'StoreTitle' AND @Direction = 'ASC' THEN [S].[Title] END ASC,
									CASE WHEN @SortField = 'NeedDate' AND @Direction = 'DESC' THEN [PR].[NeedDate] END DESC,
									CASE WHEN @SortField = 'NeedDate' AND @Direction = 'ASC' THEN [PR].[NeedDate] END ASC,
									CASE WHEN @SortField = 'DateTime' AND @Direction = 'DESC' THEN [PR].[DateTime] END DESC,
									CASE WHEN @SortField = 'DateTime' AND @Direction = 'ASC' THEN [PR].[DateTime] END ASC,
									CASE WHEN @SortField = 'UserFullName' AND @Direction = 'DESC' THEN [U].[FullName] END DESC,
									CASE WHEN @SortField = 'UserFullName' AND @Direction = 'ASC' THEN [U].[FullName] END ASC,
									CASE WHEN @SortField = 'PartRequestNo' AND @Direction = 'DESC' THEN [PR].[PartRequestNo] END DESC,
									CASE WHEN @SortField = 'PartRequestNo' AND @Direction = 'ASC' THEN [PR].[PartRequestNo] END ASC,
									CASE WHEN @SortField = 'StateEnumTitle' AND @Direction = 'DESC' THEN [SE].[Title] END DESC,
									CASE WHEN @SortField = 'StateEnumTitle' AND @Direction = 'ASC' THEN [SE].[Title] END ASC,
									CASE WHEN @SortField = 'Comment' AND @Direction = 'DESC' THEN [PR].[Comment] END DESC,
									CASE WHEN @SortField = 'Comment' AND @Direction = 'ASC' THEN [PR].[Comment] END ASC,
									CASE WHEN @SortField = 'SystemComment' AND @Direction = 'DESC' THEN [PR].[SystemComment] END DESC,
									CASE WHEN @SortField = 'SystemComment' AND @Direction = 'ASC' THEN [PR].[SystemComment] END ASC,
									CASE WHEN @SortField = 'PersianDate' AND @Direction = 'DESC' THEN [PR].[PersianDate] END DESC,
									CASE WHEN @SortField = 'PersianDate' AND @Direction = 'ASC' THEN [PR].[PersianDate] END ASC,
									CASE WHEN @SortField = 'Time' AND @Direction = 'DESC' THEN [PR].[Time] END DESC,
									CASE WHEN @SortField = 'Time' AND @Direction = 'ASC' THEN [PR].[Time] END ASC,
									CASE WHEN @SortField = 'JsonField' AND @Direction = 'DESC' THEN [PR].[JsonField] END DESC,
									CASE WHEN @SortField = 'JsonField' AND @Direction = 'ASC' THEN [PR].[JsonField] END ASC
								OFFSET @Offset ROWS FETCH NEXT @Size ROWS ONLY ";

				return await Connection.QueryAsync<TEntity>(Command, new {UserId, Offset, Size, SearchValue, SortField, Direction, Language, EditMode}, transaction: Transaction);
			}
			catch { throw; }
			#endregion
		}

		public virtual async Task<IEnumerable<TEntity>> GetPartRequestStoreList(bool? EditMode = null)
		{
			#region GetPartRequestStoreList
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

		public virtual async Task<IEnumerable<TEntity>> GetPartRequestUserList(bool? EditMode = null)
		{
			#region GetPartRequestUserList
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

		public virtual async Task<IEnumerable<TEntity>> GetPartRequestStateEnumList(bool? EditMode = null)
		{
			#region GetPartRequestStateEnumList
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
									[PR].[StateEnumId],
									[SE].[Title] [StateEnumTitle]
								FROM
									[Part].[PartRequest] [PR]
									LEFT  JOIN [State].[StateEnum] [SE] ON [SE].[Id] = [PR].[StateEnumId]
								WHERE
									[PR].[Id] = @Id ";

				return await Connection.QueryFirstOrDefaultAsync<TEntity>(Command, new {Id}, transaction: Transaction);
			}
			catch { throw; }
			#endregion
		}
	}
}
