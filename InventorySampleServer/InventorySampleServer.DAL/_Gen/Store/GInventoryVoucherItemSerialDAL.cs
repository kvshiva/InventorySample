using Dapper;
using Microsoft.Data.SqlClient;
using InventorySampleServer.DAL._Base;

namespace InventorySampleServer.DAL._Gen.Store
{
	public class GInventoryVoucherItemSerialDAL<TEntity> : BaseDAL<TEntity> where TEntity : class
	{
		public GInventoryVoucherItemSerialDAL(SqlConnection? Connection, SqlTransaction? Transaction) : base(Connection, Transaction) { }

		public override async Task<TEntity> GetById(int Id)
		{
			#region GetById
			try
			{
				var Command = @"SELECT
									[IVIS].[Id],
									[IVIS].[Version],
									[IVIS].[CreatedBy],
									[IVIS].[CreatedDateTime],
									[IVIS].[UpdatedBy],
									[IVIS].[UpdatedDateTime],
									[IVIS].[InventoryVoucherItemId],
									[IVIS].[SerialNo],
									[IVIS].[Value1],
									[IVIS].[Value2],
									[IVIS].[Comment],
									[IVIS].[SystemComment],
									[IVIS].[JsonField],
									CAST(1 AS BIT) Editable
								FROM
									[Store].[InventoryVoucherItemSerial] [IVIS]
									INNER JOIN [Store].[InventoryVoucherItem] [IVI] ON [IVI].[Id] = [IVIS].[InventoryVoucherItemId]
								WHERE
									[IVIS].[Id] = @Id ";

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
									[IVIS].[Id],
									[IVIS].[Version],
									[IVIS].[CreatedBy],
									[IVIS].[CreatedDateTime],
									[IVIS].[UpdatedBy],
									[IVIS].[UpdatedDateTime],
									[IVIS].[InventoryVoucherItemId],
									[IVIS].[SerialNo],
									[IVIS].[Value1],
									[IVIS].[Value2],
									[IVIS].[Comment],
									[IVIS].[SystemComment],
									[IVIS].[JsonField]
								FROM
									[Store].[InventoryVoucherItemSerial] [IVIS] 
								WHERE
									[IVIS].[Id] = @Id ";

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
									[IVIS].[Id],
									[IVIS].[Version],
									[IVIS].[CreatedBy],
									[IVIS].[CreatedDateTime],
									[IVIS].[UpdatedBy],
									[IVIS].[UpdatedDateTime],
									[IVIS].[InventoryVoucherItemId],
									[IVIS].[SerialNo],
									[IVIS].[Value1],
									[IVIS].[Value2],
									[IVIS].[Comment],
									[IVIS].[SystemComment],
									[IVIS].[JsonField],
									CAST(1 AS BIT) Editable,
									ItemCount = COUNT(*) OVER()
								FROM
									[Store].[InventoryVoucherItemSerial] [IVIS]
									INNER JOIN [Store].[InventoryVoucherItem] [IVI] ON [IVI].[Id] = [IVIS].[InventoryVoucherItemId]";

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
									[IVIS].[Id],
									[IVIS].[Version],
									[IVIS].[CreatedBy],
									[IVIS].[CreatedDateTime],
									[IVIS].[UpdatedBy],
									[IVIS].[UpdatedDateTime],
									[IVIS].[InventoryVoucherItemId],
									[IVIS].[SerialNo],
									[IVIS].[Value1],
									[IVIS].[Value2],
									[IVIS].[Comment],
									[IVIS].[SystemComment],
									[IVIS].[JsonField],
									CAST(1 AS BIT) Editable,
									ItemCount = COUNT(*) OVER()
								FROM
									[Store].[InventoryVoucherItemSerial] [IVIS]
									INNER JOIN [Store].[InventoryVoucherItem] [IVI] ON [IVI].[Id] = [IVIS].[InventoryVoucherItemId]
								WHERE
									(
										@SearchValue IS NULL
										OR
										(
											[IVIS].[SerialNo] LIKE N'%' + @SearchValue + '%'
											OR
											[IVIS].[Value1] LIKE N'%' + @SearchValue + '%'
											OR
											[IVIS].[Value2] LIKE N'%' + @SearchValue + '%'
											OR
											[IVIS].[Comment] LIKE N'%' + @SearchValue + '%'
											OR
											[IVIS].[SystemComment] LIKE N'%' + @SearchValue + '%'
											OR
											[IVIS].[JsonField] LIKE N'%' + @SearchValue + '%'
											
										)
									)
								ORDER BY
									CASE WHEN @SortField IS NULL THEN [IVIS].[Id] END ASC,
									CASE WHEN @SortField = 'SerialNo' AND @Direction = 'DESC' THEN [IVIS].[SerialNo] END DESC,
									CASE WHEN @SortField = 'SerialNo' AND @Direction = 'ASC' THEN [IVIS].[SerialNo] END ASC,
									CASE WHEN @SortField = 'Value1' AND @Direction = 'DESC' THEN [IVIS].[Value1] END DESC,
									CASE WHEN @SortField = 'Value1' AND @Direction = 'ASC' THEN [IVIS].[Value1] END ASC,
									CASE WHEN @SortField = 'Value2' AND @Direction = 'DESC' THEN [IVIS].[Value2] END DESC,
									CASE WHEN @SortField = 'Value2' AND @Direction = 'ASC' THEN [IVIS].[Value2] END ASC,
									CASE WHEN @SortField = 'Comment' AND @Direction = 'DESC' THEN [IVIS].[Comment] END DESC,
									CASE WHEN @SortField = 'Comment' AND @Direction = 'ASC' THEN [IVIS].[Comment] END ASC,
									CASE WHEN @SortField = 'SystemComment' AND @Direction = 'DESC' THEN [IVIS].[SystemComment] END DESC,
									CASE WHEN @SortField = 'SystemComment' AND @Direction = 'ASC' THEN [IVIS].[SystemComment] END ASC,
									CASE WHEN @SortField = 'JsonField' AND @Direction = 'DESC' THEN [IVIS].[JsonField] END DESC,
									CASE WHEN @SortField = 'JsonField' AND @Direction = 'ASC' THEN [IVIS].[JsonField] END ASC
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
				var Command = @"INSERT INTO [Store].[InventoryVoucherItemSerial]
												(
													[CreatedBy],
													[CreatedDateTime],
													[InventoryVoucherItemId],
													[SerialNo],
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
													@InventoryVoucherItemId,
													@SerialNo,
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
				var Command = @"UPDATE [Store].[InventoryVoucherItemSerial] SET
									[UpdatedBy] = @UpdatedBy,
									[UpdatedDateTime] = @UpdatedDateTime,
									[InventoryVoucherItemId] = @InventoryVoucherItemId,
									[SerialNo] = @SerialNo,
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
				var Command = @"Delete [Store].[InventoryVoucherItemSerial] WHERE [Id] = @Id ";

				return await Connection.ExecuteAsync(Command, new {Id}, transaction: Transaction);
			}
			catch { throw; }
			#endregion
		}

		public virtual async Task<IEnumerable<TEntity>> GetListByInventoryVoucherItemId(int InventoryVoucherItemId, string? SearchValue = null, string? SortField = null, string? Direction = null, int? PageNumber = null, int? PageSize = null, int? Language = null, bool? EditMode = null)
		{
			#region GetListByInventoryVoucherItemId
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
									[IVIS].[Id],
									[IVIS].[Version],
									[IVIS].[CreatedBy],
									[IVIS].[CreatedDateTime],
									[IVIS].[UpdatedBy],
									[IVIS].[UpdatedDateTime],
									[IVIS].[InventoryVoucherItemId],
									[IVIS].[SerialNo],
									[IVIS].[Value1],
									[IVIS].[Value2],
									[IVIS].[Comment],
									[IVIS].[SystemComment],
									[IVIS].[JsonField],
									CAST(1 AS BIT) Editable,
									ItemCount = COUNT(*) OVER()
								FROM
									[Store].[InventoryVoucherItemSerial] [IVIS]
									INNER JOIN [Store].[InventoryVoucherItem] [IVI] ON [IVI].[Id] = [IVIS].[InventoryVoucherItemId]
								WHERE
									[IVIS].[InventoryVoucherItemId] = @InventoryVoucherItemId
									AND
									(
										@SearchValue IS NULL
										OR
										(
											[IVIS].[SerialNo] LIKE N'%' + @SearchValue + '%'
											OR
											[IVIS].[Value1] LIKE N'%' + @SearchValue + '%'
											OR
											[IVIS].[Value2] LIKE N'%' + @SearchValue + '%'
											OR
											[IVIS].[Comment] LIKE N'%' + @SearchValue + '%'
											OR
											[IVIS].[SystemComment] LIKE N'%' + @SearchValue + '%'
											OR
											[IVIS].[JsonField] LIKE N'%' + @SearchValue + '%'
											
										)
									)
								ORDER BY
									CASE WHEN @SortField IS NULL THEN [IVIS].[Id] END ASC,
									CASE WHEN @SortField = 'SerialNo' AND @Direction = 'DESC' THEN [IVIS].[SerialNo] END DESC,
									CASE WHEN @SortField = 'SerialNo' AND @Direction = 'ASC' THEN [IVIS].[SerialNo] END ASC,
									CASE WHEN @SortField = 'Value1' AND @Direction = 'DESC' THEN [IVIS].[Value1] END DESC,
									CASE WHEN @SortField = 'Value1' AND @Direction = 'ASC' THEN [IVIS].[Value1] END ASC,
									CASE WHEN @SortField = 'Value2' AND @Direction = 'DESC' THEN [IVIS].[Value2] END DESC,
									CASE WHEN @SortField = 'Value2' AND @Direction = 'ASC' THEN [IVIS].[Value2] END ASC,
									CASE WHEN @SortField = 'Comment' AND @Direction = 'DESC' THEN [IVIS].[Comment] END DESC,
									CASE WHEN @SortField = 'Comment' AND @Direction = 'ASC' THEN [IVIS].[Comment] END ASC,
									CASE WHEN @SortField = 'SystemComment' AND @Direction = 'DESC' THEN [IVIS].[SystemComment] END DESC,
									CASE WHEN @SortField = 'SystemComment' AND @Direction = 'ASC' THEN [IVIS].[SystemComment] END ASC,
									CASE WHEN @SortField = 'JsonField' AND @Direction = 'DESC' THEN [IVIS].[JsonField] END DESC,
									CASE WHEN @SortField = 'JsonField' AND @Direction = 'ASC' THEN [IVIS].[JsonField] END ASC
								OFFSET @Offset ROWS FETCH NEXT @Size ROWS ONLY ";

				return await Connection.QueryAsync<TEntity>(Command, new {InventoryVoucherItemId, Offset, Size, SearchValue, SortField, Direction, Language, EditMode}, transaction: Transaction);
			}
			catch { throw; }
			#endregion
		}

		public virtual async Task<IEnumerable<TEntity>> GetInventoryVoucherItemSerialInventoryVoucherItemList(bool? EditMode = null)
		{
			#region GetInventoryVoucherItemSerialInventoryVoucherItemList
			try
			{
				var Command = @"SELECT
									[IVI].[Id],
									CAST(1 AS BIT) Editable,
									ItemCount = COUNT(*) OVER()
								FROM
									[Store].[InventoryVoucherItem] [IVI] ";

				return await Connection.QueryAsync<TEntity>(Command, new { EditMode }, transaction: Transaction);
			}
			catch { throw; }
			#endregion
		}

	}
}
