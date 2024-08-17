using Dapper;
using Microsoft.Data.SqlClient;
using InventorySampleServer.DAL._Base;

namespace InventorySampleServer.DAL._Gen.Store
{
	public class GInventoryVoucherItemDAL<TEntity> : BaseDAL<TEntity> where TEntity : class
	{
		public GInventoryVoucherItemDAL(SqlConnection? Connection, SqlTransaction? Transaction) : base(Connection, Transaction) { }

		public override async Task<TEntity> GetById(int Id)
		{
			#region GetById
			try
			{
				var Command = @"SELECT
									[IVI].[Id],
									[IVI].[Version],
									[IVI].[CreatedBy],
									[IVI].[CreatedDateTime],
									[IVI].[UpdatedBy],
									[IVI].[UpdatedDateTime],
									[IVI].[InventoryVoucherId],
									[P].[Title] [PartTitle],
									[IVI].[PartId],
									[IVI].[Value1],
									[IVI].[Value2],
									[IVI].[Comment],
									[IVI].[SystemComment],
									[IVI].[JsonField],
									CAST(1 AS BIT) Editable
								FROM
									[Store].[InventoryVoucherItem] [IVI]
									INNER JOIN [Store].[InventoryVoucher] [IV] ON [IV].[Id] = [IVI].[InventoryVoucherId]
									INNER JOIN [Part].[Part] [P] ON [P].[Id] = [IVI].[PartId]
								WHERE
									[IVI].[Id] = @Id ";

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
									[IVI].[Id],
									[IVI].[Version],
									[IVI].[CreatedBy],
									[IVI].[CreatedDateTime],
									[IVI].[UpdatedBy],
									[IVI].[UpdatedDateTime],
									[IVI].[InventoryVoucherId],
									[IVI].[PartId],
									[IVI].[Value1],
									[IVI].[Value2],
									[IVI].[Comment],
									[IVI].[SystemComment],
									[IVI].[JsonField]
								FROM
									[Store].[InventoryVoucherItem] [IVI] 
								WHERE
									[IVI].[Id] = @Id ";

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
									[IVI].[Id],
									[IVI].[Version],
									[IVI].[CreatedBy],
									[IVI].[CreatedDateTime],
									[IVI].[UpdatedBy],
									[IVI].[UpdatedDateTime],
									[IVI].[InventoryVoucherId],
									[P].[Title] [PartTitle],
									[IVI].[PartId],
									[IVI].[Value1],
									[IVI].[Value2],
									[IVI].[Comment],
									[IVI].[SystemComment],
									[IVI].[JsonField],
									CAST(1 AS BIT) Editable,
									ItemCount = COUNT(*) OVER()
								FROM
									[Store].[InventoryVoucherItem] [IVI]
									INNER JOIN [Store].[InventoryVoucher] [IV] ON [IV].[Id] = [IVI].[InventoryVoucherId]
									INNER JOIN [Part].[Part] [P] ON [P].[Id] = [IVI].[PartId]";

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
									[IVI].[Id],
									[IVI].[Version],
									[IVI].[CreatedBy],
									[IVI].[CreatedDateTime],
									[IVI].[UpdatedBy],
									[IVI].[UpdatedDateTime],
									[IVI].[InventoryVoucherId],
									[P].[Title] [PartTitle],
									[IVI].[PartId],
									[IVI].[Value1],
									[IVI].[Value2],
									[IVI].[Comment],
									[IVI].[SystemComment],
									[IVI].[JsonField],
									CAST(1 AS BIT) Editable,
									ItemCount = COUNT(*) OVER()
								FROM
									[Store].[InventoryVoucherItem] [IVI]
									INNER JOIN [Store].[InventoryVoucher] [IV] ON [IV].[Id] = [IVI].[InventoryVoucherId]
									INNER JOIN [Part].[Part] [P] ON [P].[Id] = [IVI].[PartId]
								WHERE
									(
										@SearchValue IS NULL
										OR
										(
											[P].[Title] LIKE N'%' + @SearchValue + '%'
											OR
											[IVI].[Value1] LIKE N'%' + @SearchValue + '%'
											OR
											[IVI].[Value2] LIKE N'%' + @SearchValue + '%'
											OR
											[IVI].[Comment] LIKE N'%' + @SearchValue + '%'
											OR
											[IVI].[SystemComment] LIKE N'%' + @SearchValue + '%'
											OR
											[IVI].[JsonField] LIKE N'%' + @SearchValue + '%'
											
										)
									)
								ORDER BY
									CASE WHEN @SortField IS NULL THEN [IVI].[Id] END ASC,
									CASE WHEN @SortField = 'PartTitle' AND @Direction = 'DESC' THEN [P].[Title] END DESC,
									CASE WHEN @SortField = 'PartTitle' AND @Direction = 'ASC' THEN [P].[Title] END ASC,
									CASE WHEN @SortField = 'Value1' AND @Direction = 'DESC' THEN [IVI].[Value1] END DESC,
									CASE WHEN @SortField = 'Value1' AND @Direction = 'ASC' THEN [IVI].[Value1] END ASC,
									CASE WHEN @SortField = 'Value2' AND @Direction = 'DESC' THEN [IVI].[Value2] END DESC,
									CASE WHEN @SortField = 'Value2' AND @Direction = 'ASC' THEN [IVI].[Value2] END ASC,
									CASE WHEN @SortField = 'Comment' AND @Direction = 'DESC' THEN [IVI].[Comment] END DESC,
									CASE WHEN @SortField = 'Comment' AND @Direction = 'ASC' THEN [IVI].[Comment] END ASC,
									CASE WHEN @SortField = 'SystemComment' AND @Direction = 'DESC' THEN [IVI].[SystemComment] END DESC,
									CASE WHEN @SortField = 'SystemComment' AND @Direction = 'ASC' THEN [IVI].[SystemComment] END ASC,
									CASE WHEN @SortField = 'JsonField' AND @Direction = 'DESC' THEN [IVI].[JsonField] END DESC,
									CASE WHEN @SortField = 'JsonField' AND @Direction = 'ASC' THEN [IVI].[JsonField] END ASC
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
				var Command = @"INSERT INTO [Store].[InventoryVoucherItem]
												(
													[CreatedBy],
													[CreatedDateTime],
													[InventoryVoucherId],
													[PartId],
													[Value1],
													[Value2],
													[Comment],
													[SystemComment],
													[JsonField]
												)
											OUTPUT INSERTED.Id
											VALUES
												(
													@CreatedBy,
													@CreatedDateTime,
													@InventoryVoucherId,
													@PartId,
													@Value1,
													@Value2,
													@Comment,
													@SystemComment,
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
				var Command = @"UPDATE [Store].[InventoryVoucherItem] SET
									[UpdatedBy] = @UpdatedBy,
									[UpdatedDateTime] = @UpdatedDateTime,
									[InventoryVoucherId] = @InventoryVoucherId,
									[PartId] = @PartId,
									[Value1] = @Value1,
									[Value2] = @Value2,
									[Comment] = @Comment,
									[SystemComment] = @SystemComment,
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
				var Command = @"Delete [Store].[InventoryVoucherItem] WHERE [Id] = @Id ";

				return await Connection.ExecuteAsync(Command, new {Id}, transaction: Transaction);
			}
			catch { throw; }
			#endregion
		}

		public virtual async Task<IEnumerable<TEntity>> GetListByInventoryVoucherId(int InventoryVoucherId, string? SearchValue = null, string? SortField = null, string? Direction = null, int? PageNumber = null, int? PageSize = null, int? Language = null, bool? EditMode = null)
		{
			#region GetListByInventoryVoucherId
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
									[IVI].[Id],
									[IVI].[Version],
									[IVI].[CreatedBy],
									[IVI].[CreatedDateTime],
									[IVI].[UpdatedBy],
									[IVI].[UpdatedDateTime],
									[IVI].[InventoryVoucherId],
									[P].[Title] [PartTitle],
									[IVI].[PartId],
									[IVI].[Value1],
									[IVI].[Value2],
									[IVI].[Comment],
									[IVI].[SystemComment],
									[IVI].[JsonField],
									CAST(1 AS BIT) Editable,
									ItemCount = COUNT(*) OVER()
								FROM
									[Store].[InventoryVoucherItem] [IVI]
									INNER JOIN [Store].[InventoryVoucher] [IV] ON [IV].[Id] = [IVI].[InventoryVoucherId]
									INNER JOIN [Part].[Part] [P] ON [P].[Id] = [IVI].[PartId]
								WHERE
									[IVI].[InventoryVoucherId] = @InventoryVoucherId
									AND
									(
										@SearchValue IS NULL
										OR
										(
											[P].[Title] LIKE N'%' + @SearchValue + '%'
											OR
											[IVI].[Value1] LIKE N'%' + @SearchValue + '%'
											OR
											[IVI].[Value2] LIKE N'%' + @SearchValue + '%'
											OR
											[IVI].[Comment] LIKE N'%' + @SearchValue + '%'
											OR
											[IVI].[SystemComment] LIKE N'%' + @SearchValue + '%'
											OR
											[IVI].[JsonField] LIKE N'%' + @SearchValue + '%'
											
										)
									)
								ORDER BY
									CASE WHEN @SortField IS NULL THEN [IVI].[Id] END ASC,
									CASE WHEN @SortField = 'PartTitle' AND @Direction = 'DESC' THEN [P].[Title] END DESC,
									CASE WHEN @SortField = 'PartTitle' AND @Direction = 'ASC' THEN [P].[Title] END ASC,
									CASE WHEN @SortField = 'Value1' AND @Direction = 'DESC' THEN [IVI].[Value1] END DESC,
									CASE WHEN @SortField = 'Value1' AND @Direction = 'ASC' THEN [IVI].[Value1] END ASC,
									CASE WHEN @SortField = 'Value2' AND @Direction = 'DESC' THEN [IVI].[Value2] END DESC,
									CASE WHEN @SortField = 'Value2' AND @Direction = 'ASC' THEN [IVI].[Value2] END ASC,
									CASE WHEN @SortField = 'Comment' AND @Direction = 'DESC' THEN [IVI].[Comment] END DESC,
									CASE WHEN @SortField = 'Comment' AND @Direction = 'ASC' THEN [IVI].[Comment] END ASC,
									CASE WHEN @SortField = 'SystemComment' AND @Direction = 'DESC' THEN [IVI].[SystemComment] END DESC,
									CASE WHEN @SortField = 'SystemComment' AND @Direction = 'ASC' THEN [IVI].[SystemComment] END ASC,
									CASE WHEN @SortField = 'JsonField' AND @Direction = 'DESC' THEN [IVI].[JsonField] END DESC,
									CASE WHEN @SortField = 'JsonField' AND @Direction = 'ASC' THEN [IVI].[JsonField] END ASC
								OFFSET @Offset ROWS FETCH NEXT @Size ROWS ONLY ";

				return await Connection.QueryAsync<TEntity>(Command, new {InventoryVoucherId, Offset, Size, SearchValue, SortField, Direction, Language, EditMode}, transaction: Transaction);
			}
			catch { throw; }
			#endregion
		}

		public virtual async Task<IEnumerable<TEntity>> GetListByPartId(int PartId, string? SearchValue = null, string? SortField = null, string? Direction = null, int? PageNumber = null, int? PageSize = null, int? Language = null, bool? EditMode = null)
		{
			#region GetListByPartId
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
									[IVI].[Id],
									[IVI].[Version],
									[IVI].[CreatedBy],
									[IVI].[CreatedDateTime],
									[IVI].[UpdatedBy],
									[IVI].[UpdatedDateTime],
									[IVI].[InventoryVoucherId],
									[P].[Title] [PartTitle],
									[IVI].[PartId],
									[IVI].[Value1],
									[IVI].[Value2],
									[IVI].[Comment],
									[IVI].[SystemComment],
									[IVI].[JsonField],
									CAST(1 AS BIT) Editable,
									ItemCount = COUNT(*) OVER()
								FROM
									[Store].[InventoryVoucherItem] [IVI]
									INNER JOIN [Store].[InventoryVoucher] [IV] ON [IV].[Id] = [IVI].[InventoryVoucherId]
									INNER JOIN [Part].[Part] [P] ON [P].[Id] = [IVI].[PartId]
								WHERE
									[IVI].[PartId] = @PartId
									AND
									(
										@SearchValue IS NULL
										OR
										(
											[P].[Title] LIKE N'%' + @SearchValue + '%'
											OR
											[IVI].[Value1] LIKE N'%' + @SearchValue + '%'
											OR
											[IVI].[Value2] LIKE N'%' + @SearchValue + '%'
											OR
											[IVI].[Comment] LIKE N'%' + @SearchValue + '%'
											OR
											[IVI].[SystemComment] LIKE N'%' + @SearchValue + '%'
											OR
											[IVI].[JsonField] LIKE N'%' + @SearchValue + '%'
											
										)
									)
								ORDER BY
									CASE WHEN @SortField IS NULL THEN [IVI].[Id] END ASC,
									CASE WHEN @SortField = 'PartTitle' AND @Direction = 'DESC' THEN [P].[Title] END DESC,
									CASE WHEN @SortField = 'PartTitle' AND @Direction = 'ASC' THEN [P].[Title] END ASC,
									CASE WHEN @SortField = 'Value1' AND @Direction = 'DESC' THEN [IVI].[Value1] END DESC,
									CASE WHEN @SortField = 'Value1' AND @Direction = 'ASC' THEN [IVI].[Value1] END ASC,
									CASE WHEN @SortField = 'Value2' AND @Direction = 'DESC' THEN [IVI].[Value2] END DESC,
									CASE WHEN @SortField = 'Value2' AND @Direction = 'ASC' THEN [IVI].[Value2] END ASC,
									CASE WHEN @SortField = 'Comment' AND @Direction = 'DESC' THEN [IVI].[Comment] END DESC,
									CASE WHEN @SortField = 'Comment' AND @Direction = 'ASC' THEN [IVI].[Comment] END ASC,
									CASE WHEN @SortField = 'SystemComment' AND @Direction = 'DESC' THEN [IVI].[SystemComment] END DESC,
									CASE WHEN @SortField = 'SystemComment' AND @Direction = 'ASC' THEN [IVI].[SystemComment] END ASC,
									CASE WHEN @SortField = 'JsonField' AND @Direction = 'DESC' THEN [IVI].[JsonField] END DESC,
									CASE WHEN @SortField = 'JsonField' AND @Direction = 'ASC' THEN [IVI].[JsonField] END ASC
								OFFSET @Offset ROWS FETCH NEXT @Size ROWS ONLY ";

				return await Connection.QueryAsync<TEntity>(Command, new {PartId, Offset, Size, SearchValue, SortField, Direction, Language, EditMode}, transaction: Transaction);
			}
			catch { throw; }
			#endregion
		}

		public virtual async Task<IEnumerable<TEntity>> GetInventoryVoucherItemInventoryVoucherList(bool? EditMode = null)
		{
			#region GetInventoryVoucherItemInventoryVoucherList
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

		public virtual async Task<IEnumerable<TEntity>> GetInventoryVoucherItemPartList(bool? EditMode = null)
		{
			#region GetInventoryVoucherItemPartList
			try
			{
				var Command = @"SELECT
									[P].[Id],
									[P].[Title],
									CAST(1 AS BIT) Editable,
									ItemCount = COUNT(*) OVER()
								FROM
									[Part].[Part] [P] ";

				return await Connection.QueryAsync<TEntity>(Command, new { EditMode }, transaction: Transaction);
			}
			catch { throw; }
			#endregion
		}

	}
}
