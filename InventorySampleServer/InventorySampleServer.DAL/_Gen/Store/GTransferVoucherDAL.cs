using Dapper;
using Microsoft.Data.SqlClient;
using InventorySampleServer.DAL._Base;

namespace InventorySampleServer.DAL._Gen.Store
{
	public class GTransferVoucherDAL<TEntity> : BaseDAL<TEntity> where TEntity : class
	{
		public GTransferVoucherDAL(SqlConnection? Connection, SqlTransaction? Transaction) : base(Connection, Transaction) { }

		public override async Task<TEntity> GetById(int Id)
		{
			#region GetById
			try
			{
				var Command = @"SELECT
									[TV].[Id],
									[TV].[Version],
									[TV].[CreatedBy],
									[TV].[CreatedDateTime],
									[TV].[UpdatedBy],
									[TV].[UpdatedDateTime],
									[SourceStoreS].[Title] [SourceStoreTitle],
									[TV].[SourceStoreId],
									[TargetStoreS].[Title] [TargetStoreTitle],
									[TV].[TargetStoreId],
									[TV].[SourceInventoryVoucherId],
									[TV].[TargetInventoryVoucherId],
									[IVS].[Title] [InventoryVoucherSpecificationTitle],
									[TV].[InventoryVoucherSpecificationId],
									[TV].[TransferVoucherNo],
									[TV].[Comment],
									[TV].[JsonField],
									[U].[FullName] [UserFullName],
									[TV].[UserId],
									[SE].[Title] [StateEnumTitle],
									[TV].[StateEnumId],
									CAST(1 AS BIT) Editable
								FROM
									[Store].[TransferVoucher] [TV]
									INNER JOIN [Store].[Store] [SourceStoreS] ON [SourceStoreS].[Id] = [TV].[SourceStoreId]
									INNER JOIN [Store].[Store] [TargetStoreS] ON [TargetStoreS].[Id] = [TV].[TargetStoreId]
									LEFT  JOIN [Store].[InventoryVoucher] [SourceInventoryVoucherIV] ON [SourceInventoryVoucherIV].[Id] = [TV].[SourceInventoryVoucherId]
									LEFT  JOIN [Store].[InventoryVoucher] [TargetInventoryVoucherIV] ON [TargetInventoryVoucherIV].[Id] = [TV].[TargetInventoryVoucherId]
									INNER JOIN [Store].[InventoryVoucherSpecification] [IVS] ON [IVS].[Id] = [TV].[InventoryVoucherSpecificationId]
									INNER JOIN [General].[User] [U] ON [U].[Id] = [TV].[UserId]
									LEFT  JOIN [State].[StateEnum] [SE] ON [SE].[Id] = [TV].[StateEnumId]
								WHERE
									[TV].[Id] = @Id ";

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
									[TV].[Id],
									[TV].[Version],
									[TV].[CreatedBy],
									[TV].[CreatedDateTime],
									[TV].[UpdatedBy],
									[TV].[UpdatedDateTime],
									[TV].[SourceStoreId],
									[TV].[TargetStoreId],
									[TV].[SourceInventoryVoucherId],
									[TV].[TargetInventoryVoucherId],
									[TV].[InventoryVoucherSpecificationId],
									[TV].[TransferVoucherNo],
									[TV].[Comment],
									[TV].[JsonField],
									[TV].[UserId],
									[TV].[StateEnumId]
								FROM
									[Store].[TransferVoucher] [TV] 
								WHERE
									[TV].[Id] = @Id ";

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
									[TV].[Id],
									[TV].[Version],
									[TV].[CreatedBy],
									[TV].[CreatedDateTime],
									[TV].[UpdatedBy],
									[TV].[UpdatedDateTime],
									[SourceStoreS].[Title] [SourceStoreTitle],
									[TV].[SourceStoreId],
									[TargetStoreS].[Title] [TargetStoreTitle],
									[TV].[TargetStoreId],
									[TV].[SourceInventoryVoucherId],
									[TV].[TargetInventoryVoucherId],
									[IVS].[Title] [InventoryVoucherSpecificationTitle],
									[TV].[InventoryVoucherSpecificationId],
									[TV].[TransferVoucherNo],
									[TV].[Comment],
									[TV].[JsonField],
									[U].[FullName] [UserFullName],
									[TV].[UserId],
									[SE].[Title] [StateEnumTitle],
									[TV].[StateEnumId],
									CAST(1 AS BIT) Editable,
									ItemCount = COUNT(*) OVER()
								FROM
									[Store].[TransferVoucher] [TV]
									INNER JOIN [Store].[Store] [SourceStoreS] ON [SourceStoreS].[Id] = [TV].[SourceStoreId]
									INNER JOIN [Store].[Store] [TargetStoreS] ON [TargetStoreS].[Id] = [TV].[TargetStoreId]
									LEFT  JOIN [Store].[InventoryVoucher] [SourceInventoryVoucherIV] ON [SourceInventoryVoucherIV].[Id] = [TV].[SourceInventoryVoucherId]
									LEFT  JOIN [Store].[InventoryVoucher] [TargetInventoryVoucherIV] ON [TargetInventoryVoucherIV].[Id] = [TV].[TargetInventoryVoucherId]
									INNER JOIN [Store].[InventoryVoucherSpecification] [IVS] ON [IVS].[Id] = [TV].[InventoryVoucherSpecificationId]
									INNER JOIN [General].[User] [U] ON [U].[Id] = [TV].[UserId]
									LEFT  JOIN [State].[StateEnum] [SE] ON [SE].[Id] = [TV].[StateEnumId]";

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
									[TV].[Id],
									[TV].[Version],
									[TV].[CreatedBy],
									[TV].[CreatedDateTime],
									[TV].[UpdatedBy],
									[TV].[UpdatedDateTime],
									[SourceStoreS].[Title] [SourceStoreTitle],
									[TV].[SourceStoreId],
									[TargetStoreS].[Title] [TargetStoreTitle],
									[TV].[TargetStoreId],
									[TV].[SourceInventoryVoucherId],
									[TV].[TargetInventoryVoucherId],
									[IVS].[Title] [InventoryVoucherSpecificationTitle],
									[TV].[InventoryVoucherSpecificationId],
									[TV].[TransferVoucherNo],
									[TV].[Comment],
									[TV].[JsonField],
									[U].[FullName] [UserFullName],
									[TV].[UserId],
									[SE].[Title] [StateEnumTitle],
									[TV].[StateEnumId],
									CAST(1 AS BIT) Editable,
									ItemCount = COUNT(*) OVER()
								FROM
									[Store].[TransferVoucher] [TV]
									INNER JOIN [Store].[Store] [SourceStoreS] ON [SourceStoreS].[Id] = [TV].[SourceStoreId]
									INNER JOIN [Store].[Store] [TargetStoreS] ON [TargetStoreS].[Id] = [TV].[TargetStoreId]
									LEFT  JOIN [Store].[InventoryVoucher] [SourceInventoryVoucherIV] ON [SourceInventoryVoucherIV].[Id] = [TV].[SourceInventoryVoucherId]
									LEFT  JOIN [Store].[InventoryVoucher] [TargetInventoryVoucherIV] ON [TargetInventoryVoucherIV].[Id] = [TV].[TargetInventoryVoucherId]
									INNER JOIN [Store].[InventoryVoucherSpecification] [IVS] ON [IVS].[Id] = [TV].[InventoryVoucherSpecificationId]
									INNER JOIN [General].[User] [U] ON [U].[Id] = [TV].[UserId]
									LEFT  JOIN [State].[StateEnum] [SE] ON [SE].[Id] = [TV].[StateEnumId]
								WHERE
									(
										@SearchValue IS NULL
										OR
										(
											[S].[Title] LIKE N'%' + @SearchValue + '%'
											OR
											[S].[Title] LIKE N'%' + @SearchValue + '%'
											OR
											[IVS].[Title] LIKE N'%' + @SearchValue + '%'
											OR
											[TV].[TransferVoucherNo] LIKE N'%' + @SearchValue + '%'
											OR
											[TV].[Comment] LIKE N'%' + @SearchValue + '%'
											OR
											[TV].[JsonField] LIKE N'%' + @SearchValue + '%'
											OR
											[U].[FullName] LIKE N'%' + @SearchValue + '%'
											OR
											[SE].[Title] LIKE N'%' + @SearchValue + '%'
											
										)
									)
								ORDER BY
									CASE WHEN @SortField IS NULL THEN [TV].[Id] END ASC,
									CASE WHEN @SortField = 'StoreTitle' AND @Direction = 'DESC' THEN [S].[Title] END DESC,
									CASE WHEN @SortField = 'StoreTitle' AND @Direction = 'ASC' THEN [S].[Title] END ASC,
									CASE WHEN @SortField = 'StoreTitle' AND @Direction = 'DESC' THEN [S].[Title] END DESC,
									CASE WHEN @SortField = 'StoreTitle' AND @Direction = 'ASC' THEN [S].[Title] END ASC,
									CASE WHEN @SortField = 'InventoryVoucherSpecificationTitle' AND @Direction = 'DESC' THEN [IVS].[Title] END DESC,
									CASE WHEN @SortField = 'InventoryVoucherSpecificationTitle' AND @Direction = 'ASC' THEN [IVS].[Title] END ASC,
									CASE WHEN @SortField = 'TransferVoucherNo' AND @Direction = 'DESC' THEN [TV].[TransferVoucherNo] END DESC,
									CASE WHEN @SortField = 'TransferVoucherNo' AND @Direction = 'ASC' THEN [TV].[TransferVoucherNo] END ASC,
									CASE WHEN @SortField = 'Comment' AND @Direction = 'DESC' THEN [TV].[Comment] END DESC,
									CASE WHEN @SortField = 'Comment' AND @Direction = 'ASC' THEN [TV].[Comment] END ASC,
									CASE WHEN @SortField = 'JsonField' AND @Direction = 'DESC' THEN [TV].[JsonField] END DESC,
									CASE WHEN @SortField = 'JsonField' AND @Direction = 'ASC' THEN [TV].[JsonField] END ASC,
									CASE WHEN @SortField = 'UserFullName' AND @Direction = 'DESC' THEN [U].[FullName] END DESC,
									CASE WHEN @SortField = 'UserFullName' AND @Direction = 'ASC' THEN [U].[FullName] END ASC,
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
				var Command = @"INSERT INTO [Store].[TransferVoucher]
												(
													[CreatedBy],
													[CreatedDateTime],
													[SourceStoreId],
													[TargetStoreId],
													[SourceInventoryVoucherId],
													[TargetInventoryVoucherId],
													[InventoryVoucherSpecificationId],
													[TransferVoucherNo],
													[Comment],
													[JsonField],
													[UserId],
													[StateEnumId]
												)
											OUTPUT INSERTED.Id
											VALUES
												(
													@CreatedBy,
													@CreatedDateTime,
													@SourceStoreId,
													@TargetStoreId,
													@SourceInventoryVoucherId,
													@TargetInventoryVoucherId,
													@InventoryVoucherSpecificationId,
													@TransferVoucherNo,
													@Comment,
													@JsonField,
													@UserId,
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
				var Command = @"UPDATE [Store].[TransferVoucher] SET
									[UpdatedBy] = @UpdatedBy,
									[UpdatedDateTime] = @UpdatedDateTime,
									[SourceStoreId] = @SourceStoreId,
									[TargetStoreId] = @TargetStoreId,
									[SourceInventoryVoucherId] = @SourceInventoryVoucherId,
									[TargetInventoryVoucherId] = @TargetInventoryVoucherId,
									[InventoryVoucherSpecificationId] = @InventoryVoucherSpecificationId,
									[TransferVoucherNo] = @TransferVoucherNo,
									[Comment] = @Comment,
									[JsonField] = @JsonField,
									[UserId] = @UserId,
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
				var Command = @"Delete [Store].[TransferVoucher] WHERE [Id] = @Id ";

				return await Connection.ExecuteAsync(Command, new {Id}, transaction: Transaction);
			}
			catch { throw; }
			#endregion
		}

		public virtual async Task<IEnumerable<TEntity>> GetTransferVoucherStoreList(bool? EditMode = null)
		{
			#region GetTransferVoucherStoreList
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

		public virtual async Task<IEnumerable<TEntity>> GetTransferVoucherInventoryVoucherList(bool? EditMode = null)
		{
			#region GetTransferVoucherInventoryVoucherList
			try
			{
				var Command = @"SELECT
									[IV].[Id],
									CAST(1 AS BIT) Editable,
									ItemCount = COUNT(*) OVER()
								FROM
									[Store].[InventoryVoucher] [IV] ";

				return await Connection.QueryAsync<TEntity>(Command, new { EditMode }, transaction: Transaction);
			}
			catch { throw; }
			#endregion
		}

		public virtual async Task<IEnumerable<TEntity>> GetTransferVoucherInventoryVoucherSpecificationList(bool? EditMode = null)
		{
			#region GetTransferVoucherInventoryVoucherSpecificationList
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

		public virtual async Task<IEnumerable<TEntity>> GetTransferVoucherUserList(bool? EditMode = null)
		{
			#region GetTransferVoucherUserList
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

		public virtual async Task<IEnumerable<TEntity>> GetTransferVoucherStateEnumList(bool? EditMode = null)
		{
			#region GetTransferVoucherStateEnumList
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
									[TV].[StateEnumId],
									[SE].[Title] [StateEnumTitle]
								FROM
									[Store].[TransferVoucher] [TV]
									LEFT  JOIN [State].[StateEnum] [SE] ON [SE].[Id] = [TV].[StateEnumId]
								WHERE
									[TV].[Id] = @Id ";

				return await Connection.QueryFirstOrDefaultAsync<TEntity>(Command, new {Id}, transaction: Transaction);
			}
			catch { throw; }
			#endregion
		}
	}
}
