using Dapper;
using Microsoft.Data.SqlClient;
using InventorySampleServer.DAL._Base;

namespace InventorySampleServer.DAL._Gen.General
{
	public class GUserOperationRoleDAL<TEntity> : BaseDAL<TEntity> where TEntity : class
	{
		public GUserOperationRoleDAL(SqlConnection? Connection, SqlTransaction? Transaction) : base(Connection, Transaction) { }

		public override async Task<TEntity> GetById(int Id)
		{
			#region GetById
			try
			{
				var Command = @"SELECT
									[UOR].[Id],
									[UOR].[Version],
									[UOR].[CreatedBy],
									[UOR].[CreatedDateTime],
									[UOR].[UpdatedBy],
									[UOR].[UpdatedDateTime],
									[UOR].[IsActive],
									[U].[FullName] [UserFullName],
									[UOR].[UserId],
									[ORTE].[Title] [OperationRoleTypeEnumTitle],
									[UOR].[OperationRoleTypeEnumId],
									CAST(1 AS BIT) Editable
								FROM
									[General].[UserOperationRole] [UOR]
									INNER JOIN [General].[User] [U] ON [U].[Id] = [UOR].[UserId]
									INNER JOIN [General].[OperationRoleTypeEnum] [ORTE] ON [ORTE].[Id] = [UOR].[OperationRoleTypeEnumId]
								WHERE
									[UOR].[Id] = @Id ";

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
									[UOR].[Id],
									[UOR].[Version],
									[UOR].[CreatedBy],
									[UOR].[CreatedDateTime],
									[UOR].[UpdatedBy],
									[UOR].[UpdatedDateTime],
									[UOR].[IsActive],
									[UOR].[UserId],
									[UOR].[OperationRoleTypeEnumId]
								FROM
									[General].[UserOperationRole] [UOR] 
								WHERE
									[UOR].[Id] = @Id ";

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
									[UOR].[Id],
									[UOR].[Version],
									[UOR].[CreatedBy],
									[UOR].[CreatedDateTime],
									[UOR].[UpdatedBy],
									[UOR].[UpdatedDateTime],
									[UOR].[IsActive],
									[U].[FullName] [UserFullName],
									[UOR].[UserId],
									[ORTE].[Title] [OperationRoleTypeEnumTitle],
									[UOR].[OperationRoleTypeEnumId],
									CAST(1 AS BIT) Editable,
									ItemCount = COUNT(*) OVER()
								FROM
									[General].[UserOperationRole] [UOR]
									INNER JOIN [General].[User] [U] ON [U].[Id] = [UOR].[UserId]
									INNER JOIN [General].[OperationRoleTypeEnum] [ORTE] ON [ORTE].[Id] = [UOR].[OperationRoleTypeEnumId]";

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
									[UOR].[Id],
									[UOR].[Version],
									[UOR].[CreatedBy],
									[UOR].[CreatedDateTime],
									[UOR].[UpdatedBy],
									[UOR].[UpdatedDateTime],
									[UOR].[IsActive],
									[U].[FullName] [UserFullName],
									[UOR].[UserId],
									[ORTE].[Title] [OperationRoleTypeEnumTitle],
									[UOR].[OperationRoleTypeEnumId],
									CAST(1 AS BIT) Editable,
									ItemCount = COUNT(*) OVER()
								FROM
									[General].[UserOperationRole] [UOR]
									INNER JOIN [General].[User] [U] ON [U].[Id] = [UOR].[UserId]
									INNER JOIN [General].[OperationRoleTypeEnum] [ORTE] ON [ORTE].[Id] = [UOR].[OperationRoleTypeEnumId]
								WHERE
									(
										@SearchValue IS NULL
										OR
										(
											[UOR].[IsActive] LIKE N'%' + @SearchValue + '%'
											OR
											[U].[FullName] LIKE N'%' + @SearchValue + '%'
											OR
											[ORTE].[Title] LIKE N'%' + @SearchValue + '%'
											
										)
									)
								ORDER BY
									CASE WHEN @SortField IS NULL THEN [UOR].[Id] END ASC,
									CASE WHEN @SortField = 'IsActive' AND @Direction = 'DESC' THEN [UOR].[IsActive] END DESC,
									CASE WHEN @SortField = 'IsActive' AND @Direction = 'ASC' THEN [UOR].[IsActive] END ASC,
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
				var Command = @"INSERT INTO [General].[UserOperationRole]
												(
													[CreatedBy],
													[CreatedDateTime],
													[IsActive],
													[UserId],
													[OperationRoleTypeEnumId]
												)
											OUTPUT INSERTED.Id
											VALUES
												(
													@CreatedBy,
													@CreatedDateTime,
													@IsActive,
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
				var Command = @"UPDATE [General].[UserOperationRole] SET
									[UpdatedBy] = @UpdatedBy,
									[UpdatedDateTime] = @UpdatedDateTime,
									[IsActive] = @IsActive,
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
				var Command = @"Delete [General].[UserOperationRole] WHERE [Id] = @Id ";

				return await Connection.ExecuteAsync(Command, new {Id}, transaction: Transaction);
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
									[UOR].[Id],
									[UOR].[Version],
									[UOR].[CreatedBy],
									[UOR].[CreatedDateTime],
									[UOR].[UpdatedBy],
									[UOR].[UpdatedDateTime],
									[UOR].[IsActive],
									[U].[FullName] [UserFullName],
									[UOR].[UserId],
									[ORTE].[Title] [OperationRoleTypeEnumTitle],
									[UOR].[OperationRoleTypeEnumId],
									CAST(1 AS BIT) Editable,
									ItemCount = COUNT(*) OVER()
								FROM
									[General].[UserOperationRole] [UOR]
									INNER JOIN [General].[User] [U] ON [U].[Id] = [UOR].[UserId]
									INNER JOIN [General].[OperationRoleTypeEnum] [ORTE] ON [ORTE].[Id] = [UOR].[OperationRoleTypeEnumId]
								WHERE
									[UOR].[UserId] = @UserId
									AND
									(
										@SearchValue IS NULL
										OR
										(
											[UOR].[IsActive] LIKE N'%' + @SearchValue + '%'
											OR
											[U].[FullName] LIKE N'%' + @SearchValue + '%'
											OR
											[ORTE].[Title] LIKE N'%' + @SearchValue + '%'
											
										)
									)
								ORDER BY
									CASE WHEN @SortField IS NULL THEN [UOR].[Id] END ASC,
									CASE WHEN @SortField = 'IsActive' AND @Direction = 'DESC' THEN [UOR].[IsActive] END DESC,
									CASE WHEN @SortField = 'IsActive' AND @Direction = 'ASC' THEN [UOR].[IsActive] END ASC,
									CASE WHEN @SortField = 'UserFullName' AND @Direction = 'DESC' THEN [U].[FullName] END DESC,
									CASE WHEN @SortField = 'UserFullName' AND @Direction = 'ASC' THEN [U].[FullName] END ASC,
									CASE WHEN @SortField = 'OperationRoleTypeEnumTitle' AND @Direction = 'DESC' THEN [ORTE].[Title] END DESC,
									CASE WHEN @SortField = 'OperationRoleTypeEnumTitle' AND @Direction = 'ASC' THEN [ORTE].[Title] END ASC
								OFFSET @Offset ROWS FETCH NEXT @Size ROWS ONLY ";

				return await Connection.QueryAsync<TEntity>(Command, new {UserId, Offset, Size, SearchValue, SortField, Direction, Language, EditMode}, transaction: Transaction);
			}
			catch { throw; }
			#endregion
		}

		public virtual async Task<IEnumerable<TEntity>> GetListByOperationRoleTypeEnumId(int OperationRoleTypeEnumId, string? SearchValue = null, string? SortField = null, string? Direction = null, int? PageNumber = null, int? PageSize = null, int? Language = null, bool? EditMode = null)
		{
			#region GetListByOperationRoleTypeEnumId
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
									[UOR].[Id],
									[UOR].[Version],
									[UOR].[CreatedBy],
									[UOR].[CreatedDateTime],
									[UOR].[UpdatedBy],
									[UOR].[UpdatedDateTime],
									[UOR].[IsActive],
									[U].[FullName] [UserFullName],
									[UOR].[UserId],
									[ORTE].[Title] [OperationRoleTypeEnumTitle],
									[UOR].[OperationRoleTypeEnumId],
									CAST(1 AS BIT) Editable,
									ItemCount = COUNT(*) OVER()
								FROM
									[General].[UserOperationRole] [UOR]
									INNER JOIN [General].[User] [U] ON [U].[Id] = [UOR].[UserId]
									INNER JOIN [General].[OperationRoleTypeEnum] [ORTE] ON [ORTE].[Id] = [UOR].[OperationRoleTypeEnumId]
								WHERE
									[UOR].[OperationRoleTypeEnumId] = @OperationRoleTypeEnumId
									AND
									(
										@SearchValue IS NULL
										OR
										(
											[UOR].[IsActive] LIKE N'%' + @SearchValue + '%'
											OR
											[U].[FullName] LIKE N'%' + @SearchValue + '%'
											OR
											[ORTE].[Title] LIKE N'%' + @SearchValue + '%'
											
										)
									)
								ORDER BY
									CASE WHEN @SortField IS NULL THEN [UOR].[Id] END ASC,
									CASE WHEN @SortField = 'IsActive' AND @Direction = 'DESC' THEN [UOR].[IsActive] END DESC,
									CASE WHEN @SortField = 'IsActive' AND @Direction = 'ASC' THEN [UOR].[IsActive] END ASC,
									CASE WHEN @SortField = 'UserFullName' AND @Direction = 'DESC' THEN [U].[FullName] END DESC,
									CASE WHEN @SortField = 'UserFullName' AND @Direction = 'ASC' THEN [U].[FullName] END ASC,
									CASE WHEN @SortField = 'OperationRoleTypeEnumTitle' AND @Direction = 'DESC' THEN [ORTE].[Title] END DESC,
									CASE WHEN @SortField = 'OperationRoleTypeEnumTitle' AND @Direction = 'ASC' THEN [ORTE].[Title] END ASC
								OFFSET @Offset ROWS FETCH NEXT @Size ROWS ONLY ";

				return await Connection.QueryAsync<TEntity>(Command, new {OperationRoleTypeEnumId, Offset, Size, SearchValue, SortField, Direction, Language, EditMode}, transaction: Transaction);
			}
			catch { throw; }
			#endregion
		}

		public virtual async Task<IEnumerable<TEntity>> GetUserOperationRoleUserList(bool? EditMode = null)
		{
			#region GetUserOperationRoleUserList
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

		public virtual async Task<IEnumerable<TEntity>> GetUserOperationRoleOperationRoleTypeEnumList(bool? EditMode = null)
		{
			#region GetUserOperationRoleOperationRoleTypeEnumList
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
