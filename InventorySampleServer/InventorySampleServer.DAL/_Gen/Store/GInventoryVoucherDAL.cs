using Dapper;
using Microsoft.Data.SqlClient;
using InventorySampleServer.DAL._Base;

namespace InventorySampleServer.DAL._Gen.Store
{
	public class GInventoryVoucherDAL<TEntity> : BaseDAL<TEntity> where TEntity : class
	{
		public GInventoryVoucherDAL(SqlConnection? Connection, SqlTransaction? Transaction) : base(Connection, Transaction) { }

		public override async Task<TEntity> GetById(int Id)
		{
			#region GetById
			try
			{
				var Command = @"SELECT
									[IV].[Id],
									[IV].[Version],
									[IV].[CreatedBy],
									[IV].[CreatedDateTime],
									[IV].[UpdatedBy],
									[IV].[UpdatedDateTime],
									[IV].[InventoryVoucherNo],
									[IV].[DateTime],
									[IV].[PersianDate],
									[IV].[Time],
									[IV].[Comment],
									[IV].[SystemComment],
									[S].[Title] [StoreTitle],
									[IV].[StoreId],
									[IVS].[Title] [InventoryVoucherSpecificationTitle],
									[IV].[InventoryVoucherSpecificationId],
									[U].[FullName] [UserFullName],
									[IV].[UserId],
									[IV].[JsonField],
									[BaseEntityEE].[Title] [BaseEntityTitle],
									[IV].[BaseEntity],
									[IV].[BaseEntityRef],
									[SE].[Title] [StateEnumTitle],
									[IV].[StateEnumId],
									CAST(1 AS BIT) Editable
								FROM
									[Store].[InventoryVoucher] [IV]
									INNER JOIN [Store].[Store] [S] ON [S].[Id] = [IV].[StoreId]
									INNER JOIN [Store].[InventoryVoucherSpecification] [IVS] ON [IVS].[Id] = [IV].[InventoryVoucherSpecificationId]
									INNER JOIN [General].[User] [U] ON [U].[Id] = [IV].[UserId]
									LEFT  JOIN [State].[EntityEnum] [BaseEntityEE] ON [BaseEntityEE].[Id] = [IV].[BaseEntity]
									LEFT  JOIN [State].[StateEnum] [SE] ON [SE].[Id] = [IV].[StateEnumId]
								WHERE
									[IV].[Id] = @Id ";

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
									[IV].[Id],
									[IV].[Version],
									[IV].[CreatedBy],
									[IV].[CreatedDateTime],
									[IV].[UpdatedBy],
									[IV].[UpdatedDateTime],
									[IV].[InventoryVoucherNo],
									[IV].[DateTime],
									[IV].[PersianDate],
									[IV].[Time],
									[IV].[Comment],
									[IV].[SystemComment],
									[IV].[StoreId],
									[IV].[InventoryVoucherSpecificationId],
									[IV].[UserId],
									[IV].[JsonField],
									[IV].[BaseEntity],
									[IV].[BaseEntityRef],
									[IV].[StateEnumId]
								FROM
									[Store].[InventoryVoucher] [IV] 
								WHERE
									[IV].[Id] = @Id ";

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
									[IV].[Id],
									[IV].[Version],
									[IV].[CreatedBy],
									[IV].[CreatedDateTime],
									[IV].[UpdatedBy],
									[IV].[UpdatedDateTime],
									[IV].[InventoryVoucherNo],
									[IV].[DateTime],
									[IV].[PersianDate],
									[IV].[Time],
									[IV].[Comment],
									[IV].[SystemComment],
									[S].[Title] [StoreTitle],
									[IV].[StoreId],
									[IVS].[Title] [InventoryVoucherSpecificationTitle],
									[IV].[InventoryVoucherSpecificationId],
									[U].[FullName] [UserFullName],
									[IV].[UserId],
									[IV].[JsonField],
									[BaseEntityEE].[Title] [BaseEntityTitle],
									[IV].[BaseEntity],
									[IV].[BaseEntityRef],
									[SE].[Title] [StateEnumTitle],
									[IV].[StateEnumId],
									CAST(1 AS BIT) Editable,
									ItemCount = COUNT(*) OVER()
								FROM
									[Store].[InventoryVoucher] [IV]
									INNER JOIN [Store].[Store] [S] ON [S].[Id] = [IV].[StoreId]
									INNER JOIN [Store].[InventoryVoucherSpecification] [IVS] ON [IVS].[Id] = [IV].[InventoryVoucherSpecificationId]
									INNER JOIN [General].[User] [U] ON [U].[Id] = [IV].[UserId]
									LEFT  JOIN [State].[EntityEnum] [BaseEntityEE] ON [BaseEntityEE].[Id] = [IV].[BaseEntity]
									LEFT  JOIN [State].[StateEnum] [SE] ON [SE].[Id] = [IV].[StateEnumId]";

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
									[IV].[Id],
									[IV].[Version],
									[IV].[CreatedBy],
									[IV].[CreatedDateTime],
									[IV].[UpdatedBy],
									[IV].[UpdatedDateTime],
									[IV].[InventoryVoucherNo],
									[IV].[DateTime],
									[IV].[PersianDate],
									[IV].[Time],
									[IV].[Comment],
									[IV].[SystemComment],
									[S].[Title] [StoreTitle],
									[IV].[StoreId],
									[IVS].[Title] [InventoryVoucherSpecificationTitle],
									[IV].[InventoryVoucherSpecificationId],
									[U].[FullName] [UserFullName],
									[IV].[UserId],
									[IV].[JsonField],
									[BaseEntityEE].[Title] [BaseEntityTitle],
									[IV].[BaseEntity],
									[IV].[BaseEntityRef],
									[SE].[Title] [StateEnumTitle],
									[IV].[StateEnumId],
									CAST(1 AS BIT) Editable,
									ItemCount = COUNT(*) OVER()
								FROM
									[Store].[InventoryVoucher] [IV]
									INNER JOIN [Store].[Store] [S] ON [S].[Id] = [IV].[StoreId]
									INNER JOIN [Store].[InventoryVoucherSpecification] [IVS] ON [IVS].[Id] = [IV].[InventoryVoucherSpecificationId]
									INNER JOIN [General].[User] [U] ON [U].[Id] = [IV].[UserId]
									LEFT  JOIN [State].[EntityEnum] [BaseEntityEE] ON [BaseEntityEE].[Id] = [IV].[BaseEntity]
									LEFT  JOIN [State].[StateEnum] [SE] ON [SE].[Id] = [IV].[StateEnumId]
								WHERE
									(
										@SearchValue IS NULL
										OR
										(
											[IV].[InventoryVoucherNo] LIKE N'%' + @SearchValue + '%'
											OR
											[IV].[DateTime] LIKE N'%' + @SearchValue + '%'
											OR
											[IV].[PersianDate] LIKE N'%' + @SearchValue + '%'
											OR
											[IV].[Time] LIKE N'%' + @SearchValue + '%'
											OR
											[IV].[Comment] LIKE N'%' + @SearchValue + '%'
											OR
											[IV].[SystemComment] LIKE N'%' + @SearchValue + '%'
											OR
											[S].[Title] LIKE N'%' + @SearchValue + '%'
											OR
											[IVS].[Title] LIKE N'%' + @SearchValue + '%'
											OR
											[U].[FullName] LIKE N'%' + @SearchValue + '%'
											OR
											[IV].[JsonField] LIKE N'%' + @SearchValue + '%'
											OR
											[EE].[Title] LIKE N'%' + @SearchValue + '%'
											OR
											[IV].[BaseEntityRef] LIKE N'%' + @SearchValue + '%'
											OR
											[SE].[Title] LIKE N'%' + @SearchValue + '%'
											
										)
									)
								ORDER BY
									CASE WHEN @SortField IS NULL THEN [IV].[Id] END ASC,
									CASE WHEN @SortField = 'InventoryVoucherNo' AND @Direction = 'DESC' THEN [IV].[InventoryVoucherNo] END DESC,
									CASE WHEN @SortField = 'InventoryVoucherNo' AND @Direction = 'ASC' THEN [IV].[InventoryVoucherNo] END ASC,
									CASE WHEN @SortField = 'DateTime' AND @Direction = 'DESC' THEN [IV].[DateTime] END DESC,
									CASE WHEN @SortField = 'DateTime' AND @Direction = 'ASC' THEN [IV].[DateTime] END ASC,
									CASE WHEN @SortField = 'PersianDate' AND @Direction = 'DESC' THEN [IV].[PersianDate] END DESC,
									CASE WHEN @SortField = 'PersianDate' AND @Direction = 'ASC' THEN [IV].[PersianDate] END ASC,
									CASE WHEN @SortField = 'Time' AND @Direction = 'DESC' THEN [IV].[Time] END DESC,
									CASE WHEN @SortField = 'Time' AND @Direction = 'ASC' THEN [IV].[Time] END ASC,
									CASE WHEN @SortField = 'Comment' AND @Direction = 'DESC' THEN [IV].[Comment] END DESC,
									CASE WHEN @SortField = 'Comment' AND @Direction = 'ASC' THEN [IV].[Comment] END ASC,
									CASE WHEN @SortField = 'SystemComment' AND @Direction = 'DESC' THEN [IV].[SystemComment] END DESC,
									CASE WHEN @SortField = 'SystemComment' AND @Direction = 'ASC' THEN [IV].[SystemComment] END ASC,
									CASE WHEN @SortField = 'StoreTitle' AND @Direction = 'DESC' THEN [S].[Title] END DESC,
									CASE WHEN @SortField = 'StoreTitle' AND @Direction = 'ASC' THEN [S].[Title] END ASC,
									CASE WHEN @SortField = 'InventoryVoucherSpecificationTitle' AND @Direction = 'DESC' THEN [IVS].[Title] END DESC,
									CASE WHEN @SortField = 'InventoryVoucherSpecificationTitle' AND @Direction = 'ASC' THEN [IVS].[Title] END ASC,
									CASE WHEN @SortField = 'UserFullName' AND @Direction = 'DESC' THEN [U].[FullName] END DESC,
									CASE WHEN @SortField = 'UserFullName' AND @Direction = 'ASC' THEN [U].[FullName] END ASC,
									CASE WHEN @SortField = 'JsonField' AND @Direction = 'DESC' THEN [IV].[JsonField] END DESC,
									CASE WHEN @SortField = 'JsonField' AND @Direction = 'ASC' THEN [IV].[JsonField] END ASC,
									CASE WHEN @SortField = 'EntityEnumTitle' AND @Direction = 'DESC' THEN [EE].[Title] END DESC,
									CASE WHEN @SortField = 'EntityEnumTitle' AND @Direction = 'ASC' THEN [EE].[Title] END ASC,
									CASE WHEN @SortField = 'BaseEntityRef' AND @Direction = 'DESC' THEN [IV].[BaseEntityRef] END DESC,
									CASE WHEN @SortField = 'BaseEntityRef' AND @Direction = 'ASC' THEN [IV].[BaseEntityRef] END ASC,
									CASE WHEN @SortField = 'StateEnumTitle' AND @Direction = 'DESC' THEN [SE].[Title] END DESC,
									CASE WHEN @SortField = 'StateEnumTitle' AND @Direction = 'ASC' THEN [SE].[Title] END ASC
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
				var Command = @"INSERT INTO [Store].[InventoryVoucher]
												(
													[CreatedBy],
													[CreatedDateTime],
													[InventoryVoucherNo],
													[DateTime],
													[PersianDate],
													[Time],
													[Comment],
													[SystemComment],
													[StoreId],
													[InventoryVoucherSpecificationId],
													[UserId],
													[JsonField],
													[BaseEntity],
													[BaseEntityRef],
													[StateEnumId]
												)
											OUTPUT INSERTED.Id
											VALUES
												(
													@CreatedBy,
													@CreatedDateTime,
													@InventoryVoucherNo,
													@DateTime,
													@PersianDate,
													@Time,
													@Comment,
													@SystemComment,
													@StoreId,
													@InventoryVoucherSpecificationId,
													@UserId,
													@JsonField,
													@BaseEntity,
													@BaseEntityRef,
													@StateEnumId
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
				var Command = @"UPDATE [Store].[InventoryVoucher] SET
									[UpdatedBy] = @UpdatedBy,
									[UpdatedDateTime] = @UpdatedDateTime,
									[InventoryVoucherNo] = @InventoryVoucherNo,
									[DateTime] = @DateTime,
									[PersianDate] = @PersianDate,
									[Time] = @Time,
									[Comment] = @Comment,
									[SystemComment] = @SystemComment,
									[StoreId] = @StoreId,
									[InventoryVoucherSpecificationId] = @InventoryVoucherSpecificationId,
									[UserId] = @UserId,
									[JsonField] = @JsonField,
									[BaseEntity] = @BaseEntity,
									[BaseEntityRef] = @BaseEntityRef,
									[StateEnumId] = @StateEnumId
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
				var Command = @"Delete [Store].[InventoryVoucher] WHERE [Id] = @Id ";

				return await Connection.ExecuteAsync(Command, new {Id}, transaction: Transaction);
			}
			catch { throw; }
			#endregion
		}

		public virtual async Task<IEnumerable<TEntity>> GetListByInventoryVoucherSpecificationId(int InventoryVoucherSpecificationId, string? SearchValue = null, string? SortField = null, string? Direction = null, int? PageNumber = null, int? PageSize = null, int? Language = null, bool? EditMode = null)
		{
			#region GetListByInventoryVoucherSpecificationId
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
									[IV].[Id],
									[IV].[Version],
									[IV].[CreatedBy],
									[IV].[CreatedDateTime],
									[IV].[UpdatedBy],
									[IV].[UpdatedDateTime],
									[IV].[InventoryVoucherNo],
									[IV].[DateTime],
									[IV].[PersianDate],
									[IV].[Time],
									[IV].[Comment],
									[IV].[SystemComment],
									[S].[Title] [StoreTitle],
									[IV].[StoreId],
									[IVS].[Title] [InventoryVoucherSpecificationTitle],
									[IV].[InventoryVoucherSpecificationId],
									[U].[FullName] [UserFullName],
									[IV].[UserId],
									[IV].[JsonField],
									[BaseEntityEE].[Title] [BaseEntityTitle],
									[IV].[BaseEntity],
									[IV].[BaseEntityRef],
									[SE].[Title] [StateEnumTitle],
									[IV].[StateEnumId],
									CAST(1 AS BIT) Editable,
									ItemCount = COUNT(*) OVER()
								FROM
									[Store].[InventoryVoucher] [IV]
									INNER JOIN [Store].[Store] [S] ON [S].[Id] = [IV].[StoreId]
									INNER JOIN [Store].[InventoryVoucherSpecification] [IVS] ON [IVS].[Id] = [IV].[InventoryVoucherSpecificationId]
									INNER JOIN [General].[User] [U] ON [U].[Id] = [IV].[UserId]
									LEFT  JOIN [State].[EntityEnum] [BaseEntityEE] ON [BaseEntityEE].[Id] = [IV].[BaseEntity]
									LEFT  JOIN [State].[StateEnum] [SE] ON [SE].[Id] = [IV].[StateEnumId]
								WHERE
									[IV].[InventoryVoucherSpecificationId] = @InventoryVoucherSpecificationId
									AND
									(
										@SearchValue IS NULL
										OR
										(
											[IV].[InventoryVoucherNo] LIKE N'%' + @SearchValue + '%'
											OR
											[IV].[DateTime] LIKE N'%' + @SearchValue + '%'
											OR
											[IV].[PersianDate] LIKE N'%' + @SearchValue + '%'
											OR
											[IV].[Time] LIKE N'%' + @SearchValue + '%'
											OR
											[IV].[Comment] LIKE N'%' + @SearchValue + '%'
											OR
											[IV].[SystemComment] LIKE N'%' + @SearchValue + '%'
											OR
											[S].[Title] LIKE N'%' + @SearchValue + '%'
											OR
											[IVS].[Title] LIKE N'%' + @SearchValue + '%'
											OR
											[U].[FullName] LIKE N'%' + @SearchValue + '%'
											OR
											[IV].[JsonField] LIKE N'%' + @SearchValue + '%'
											OR
											[EE].[Title] LIKE N'%' + @SearchValue + '%'
											OR
											[IV].[BaseEntityRef] LIKE N'%' + @SearchValue + '%'
											OR
											[SE].[Title] LIKE N'%' + @SearchValue + '%'
											
										)
									)
								ORDER BY
									CASE WHEN @SortField IS NULL THEN [IV].[Id] END ASC,
									CASE WHEN @SortField = 'InventoryVoucherNo' AND @Direction = 'DESC' THEN [IV].[InventoryVoucherNo] END DESC,
									CASE WHEN @SortField = 'InventoryVoucherNo' AND @Direction = 'ASC' THEN [IV].[InventoryVoucherNo] END ASC,
									CASE WHEN @SortField = 'DateTime' AND @Direction = 'DESC' THEN [IV].[DateTime] END DESC,
									CASE WHEN @SortField = 'DateTime' AND @Direction = 'ASC' THEN [IV].[DateTime] END ASC,
									CASE WHEN @SortField = 'PersianDate' AND @Direction = 'DESC' THEN [IV].[PersianDate] END DESC,
									CASE WHEN @SortField = 'PersianDate' AND @Direction = 'ASC' THEN [IV].[PersianDate] END ASC,
									CASE WHEN @SortField = 'Time' AND @Direction = 'DESC' THEN [IV].[Time] END DESC,
									CASE WHEN @SortField = 'Time' AND @Direction = 'ASC' THEN [IV].[Time] END ASC,
									CASE WHEN @SortField = 'Comment' AND @Direction = 'DESC' THEN [IV].[Comment] END DESC,
									CASE WHEN @SortField = 'Comment' AND @Direction = 'ASC' THEN [IV].[Comment] END ASC,
									CASE WHEN @SortField = 'SystemComment' AND @Direction = 'DESC' THEN [IV].[SystemComment] END DESC,
									CASE WHEN @SortField = 'SystemComment' AND @Direction = 'ASC' THEN [IV].[SystemComment] END ASC,
									CASE WHEN @SortField = 'StoreTitle' AND @Direction = 'DESC' THEN [S].[Title] END DESC,
									CASE WHEN @SortField = 'StoreTitle' AND @Direction = 'ASC' THEN [S].[Title] END ASC,
									CASE WHEN @SortField = 'InventoryVoucherSpecificationTitle' AND @Direction = 'DESC' THEN [IVS].[Title] END DESC,
									CASE WHEN @SortField = 'InventoryVoucherSpecificationTitle' AND @Direction = 'ASC' THEN [IVS].[Title] END ASC,
									CASE WHEN @SortField = 'UserFullName' AND @Direction = 'DESC' THEN [U].[FullName] END DESC,
									CASE WHEN @SortField = 'UserFullName' AND @Direction = 'ASC' THEN [U].[FullName] END ASC,
									CASE WHEN @SortField = 'JsonField' AND @Direction = 'DESC' THEN [IV].[JsonField] END DESC,
									CASE WHEN @SortField = 'JsonField' AND @Direction = 'ASC' THEN [IV].[JsonField] END ASC,
									CASE WHEN @SortField = 'EntityEnumTitle' AND @Direction = 'DESC' THEN [EE].[Title] END DESC,
									CASE WHEN @SortField = 'EntityEnumTitle' AND @Direction = 'ASC' THEN [EE].[Title] END ASC,
									CASE WHEN @SortField = 'BaseEntityRef' AND @Direction = 'DESC' THEN [IV].[BaseEntityRef] END DESC,
									CASE WHEN @SortField = 'BaseEntityRef' AND @Direction = 'ASC' THEN [IV].[BaseEntityRef] END ASC,
									CASE WHEN @SortField = 'StateEnumTitle' AND @Direction = 'DESC' THEN [SE].[Title] END DESC,
									CASE WHEN @SortField = 'StateEnumTitle' AND @Direction = 'ASC' THEN [SE].[Title] END ASC
								OFFSET @Offset ROWS FETCH NEXT @Size ROWS ONLY ";

				return await Connection.QueryAsync<TEntity>(Command, new {InventoryVoucherSpecificationId, Offset, Size, SearchValue, SortField, Direction, Language, EditMode}, transaction: Transaction);
			}
			catch { throw; }
			#endregion
		}

		public virtual async Task<IEnumerable<TEntity>> GetInventoryVoucherStoreList(bool? EditMode = null)
		{
			#region GetInventoryVoucherStoreList
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

		public virtual async Task<IEnumerable<TEntity>> GetInventoryVoucherInventoryVoucherSpecificationList(bool? EditMode = null)
		{
			#region GetInventoryVoucherInventoryVoucherSpecificationList
			try
			{
				var Command = @"SELECT
									[IVS].[Id],
									[IVS].[Title],
									CAST(1 AS BIT) Editable,
									ItemCount = COUNT(*) OVER()
								FROM
									[Store].[InventoryVoucherSpecification] [IVS] ";

				return await Connection.QueryAsync<TEntity>(Command, new { EditMode }, transaction: Transaction);
			}
			catch { throw; }
			#endregion
		}

		public virtual async Task<IEnumerable<TEntity>> GetInventoryVoucherUserList(bool? EditMode = null)
		{
			#region GetInventoryVoucherUserList
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

		public virtual async Task<IEnumerable<TEntity>> GetInventoryVoucherEntityEnumList(bool? EditMode = null)
		{
			#region GetInventoryVoucherEntityEnumList
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

		public virtual async Task<IEnumerable<TEntity>> GetInventoryVoucherStateEnumList(bool? EditMode = null)
		{
			#region GetInventoryVoucherStateEnumList
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
									[IV].[StateEnumId],
									[SE].[Title] [StateEnumTitle]
								FROM
									[Store].[InventoryVoucher] [IV]
									LEFT  JOIN [State].[StateEnum] [SE] ON [SE].[Id] = [IV].[StateEnumId]
								WHERE
									[IV].[Id] = @Id ";

				return await Connection.QueryFirstOrDefaultAsync<TEntity>(Command, new {Id}, transaction: Transaction);
			}
			catch { throw; }
			#endregion
		}
	}
}
