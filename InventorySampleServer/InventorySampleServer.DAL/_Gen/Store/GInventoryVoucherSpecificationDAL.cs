using Dapper;
using Microsoft.Data.SqlClient;
using InventorySampleServer.DAL._Base;

namespace InventorySampleServer.DAL._Gen.Store
{
	public class GInventoryVoucherSpecificationDAL<TEntity> : BaseDAL<TEntity> where TEntity : class
	{
		public GInventoryVoucherSpecificationDAL(SqlConnection? Connection, SqlTransaction? Transaction) : base(Connection, Transaction) { }

		public override async Task<TEntity> GetById(int Id)
		{
			#region GetById
			try
			{
				var Command = @"SELECT
									[IVS].[Id],
									[IVS].[Version],
									[IVS].[CreatedBy],
									[IVS].[CreatedDateTime],
									[IVS].[UpdatedBy],
									[IVS].[UpdatedDateTime],
									[IVS].[Title],
									[IVS].[Comment],
									[IVSTE].[Title] [InventoryVoucherSpecificationTypeEnumTitle],
									[IVS].[InventoryVoucherSpecificationTypeEnumId],
									[ReceiptInventoryVoucherSpecificationIVS].[Title] [ReceiptInventoryVoucherSpecificationTitle],
									[IVS].[ReceiptInventoryVoucherSpecificationId],
									[RemittanceInventoryVoucherSpecificationIVS].[Title] [RemittanceInventoryVoucherSpecificationTitle],
									[IVS].[RemittanceInventoryVoucherSpecificationId],
									[IVS].[IsSystemic],
									[IVS].[Jsonfield],
									CAST(1 AS BIT) Editable
								FROM
									[Store].[InventoryVoucherSpecification] [IVS]
									INNER JOIN [Store].[InventoryVoucherSpecificationTypeEnum] [IVSTE] ON [IVSTE].[Id] = [IVS].[InventoryVoucherSpecificationTypeEnumId]
									LEFT  JOIN [Store].[InventoryVoucherSpecification] [ReceiptInventoryVoucherSpecificationIVS] ON [ReceiptInventoryVoucherSpecificationIVS].[Id] = [IVS].[ReceiptInventoryVoucherSpecificationId]
									LEFT  JOIN [Store].[InventoryVoucherSpecification] [RemittanceInventoryVoucherSpecificationIVS] ON [RemittanceInventoryVoucherSpecificationIVS].[Id] = [IVS].[RemittanceInventoryVoucherSpecificationId]
								WHERE
									[IVS].[Id] = @Id ";

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
									[IVS].[Id],
									[IVS].[Version],
									[IVS].[CreatedBy],
									[IVS].[CreatedDateTime],
									[IVS].[UpdatedBy],
									[IVS].[UpdatedDateTime],
									[IVS].[Title],
									[IVS].[Comment],
									[IVS].[InventoryVoucherSpecificationTypeEnumId],
									[IVS].[ReceiptInventoryVoucherSpecificationId],
									[IVS].[RemittanceInventoryVoucherSpecificationId],
									[IVS].[IsSystemic],
									[IVS].[Jsonfield]
								FROM
									[Store].[InventoryVoucherSpecification] [IVS] 
								WHERE
									[IVS].[Id] = @Id ";

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
									[IVS].[Id],
									[IVS].[Version],
									[IVS].[CreatedBy],
									[IVS].[CreatedDateTime],
									[IVS].[UpdatedBy],
									[IVS].[UpdatedDateTime],
									[IVS].[Title],
									[IVS].[Comment],
									[IVSTE].[Title] [InventoryVoucherSpecificationTypeEnumTitle],
									[IVS].[InventoryVoucherSpecificationTypeEnumId],
									[ReceiptInventoryVoucherSpecificationIVS].[Title] [ReceiptInventoryVoucherSpecificationTitle],
									[IVS].[ReceiptInventoryVoucherSpecificationId],
									[RemittanceInventoryVoucherSpecificationIVS].[Title] [RemittanceInventoryVoucherSpecificationTitle],
									[IVS].[RemittanceInventoryVoucherSpecificationId],
									[IVS].[IsSystemic],
									[IVS].[Jsonfield],
									CAST(1 AS BIT) Editable,
									ItemCount = COUNT(*) OVER()
								FROM
									[Store].[InventoryVoucherSpecification] [IVS]
									INNER JOIN [Store].[InventoryVoucherSpecificationTypeEnum] [IVSTE] ON [IVSTE].[Id] = [IVS].[InventoryVoucherSpecificationTypeEnumId]
									LEFT  JOIN [Store].[InventoryVoucherSpecification] [ReceiptInventoryVoucherSpecificationIVS] ON [ReceiptInventoryVoucherSpecificationIVS].[Id] = [IVS].[ReceiptInventoryVoucherSpecificationId]
									LEFT  JOIN [Store].[InventoryVoucherSpecification] [RemittanceInventoryVoucherSpecificationIVS] ON [RemittanceInventoryVoucherSpecificationIVS].[Id] = [IVS].[RemittanceInventoryVoucherSpecificationId]";

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
									[IVS].[Id],
									[IVS].[Version],
									[IVS].[CreatedBy],
									[IVS].[CreatedDateTime],
									[IVS].[UpdatedBy],
									[IVS].[UpdatedDateTime],
									[IVS].[Title],
									[IVS].[Comment],
									[IVSTE].[Title] [InventoryVoucherSpecificationTypeEnumTitle],
									[IVS].[InventoryVoucherSpecificationTypeEnumId],
									[ReceiptInventoryVoucherSpecificationIVS].[Title] [ReceiptInventoryVoucherSpecificationTitle],
									[IVS].[ReceiptInventoryVoucherSpecificationId],
									[RemittanceInventoryVoucherSpecificationIVS].[Title] [RemittanceInventoryVoucherSpecificationTitle],
									[IVS].[RemittanceInventoryVoucherSpecificationId],
									[IVS].[IsSystemic],
									[IVS].[Jsonfield],
									CAST(1 AS BIT) Editable,
									ItemCount = COUNT(*) OVER()
								FROM
									[Store].[InventoryVoucherSpecification] [IVS]
									INNER JOIN [Store].[InventoryVoucherSpecificationTypeEnum] [IVSTE] ON [IVSTE].[Id] = [IVS].[InventoryVoucherSpecificationTypeEnumId]
									LEFT  JOIN [Store].[InventoryVoucherSpecification] [ReceiptInventoryVoucherSpecificationIVS] ON [ReceiptInventoryVoucherSpecificationIVS].[Id] = [IVS].[ReceiptInventoryVoucherSpecificationId]
									LEFT  JOIN [Store].[InventoryVoucherSpecification] [RemittanceInventoryVoucherSpecificationIVS] ON [RemittanceInventoryVoucherSpecificationIVS].[Id] = [IVS].[RemittanceInventoryVoucherSpecificationId]
								WHERE
									(
										@SearchValue IS NULL
										OR
										(
											[IVS].[Title] LIKE N'%' + @SearchValue + '%'
											OR
											[IVS].[Comment] LIKE N'%' + @SearchValue + '%'
											OR
											[IVSTE].[Title] LIKE N'%' + @SearchValue + '%'
											OR
											[IVS].[Title] LIKE N'%' + @SearchValue + '%'
											OR
											[IVS].[Title] LIKE N'%' + @SearchValue + '%'
											OR
											[IVS].[IsSystemic] LIKE N'%' + @SearchValue + '%'
											OR
											[IVS].[Jsonfield] LIKE N'%' + @SearchValue + '%'
											
										)
									)
								ORDER BY
									CASE WHEN @SortField IS NULL THEN [IVS].[Id] END ASC,
									CASE WHEN @SortField = 'Title' AND @Direction = 'DESC' THEN [IVS].[Title] END DESC,
									CASE WHEN @SortField = 'Title' AND @Direction = 'ASC' THEN [IVS].[Title] END ASC,
									CASE WHEN @SortField = 'Comment' AND @Direction = 'DESC' THEN [IVS].[Comment] END DESC,
									CASE WHEN @SortField = 'Comment' AND @Direction = 'ASC' THEN [IVS].[Comment] END ASC,
									CASE WHEN @SortField = 'InventoryVoucherSpecificationTypeEnumTitle' AND @Direction = 'DESC' THEN [IVSTE].[Title] END DESC,
									CASE WHEN @SortField = 'InventoryVoucherSpecificationTypeEnumTitle' AND @Direction = 'ASC' THEN [IVSTE].[Title] END ASC,
									CASE WHEN @SortField = 'InventoryVoucherSpecificationTitle' AND @Direction = 'DESC' THEN [IVS].[Title] END DESC,
									CASE WHEN @SortField = 'InventoryVoucherSpecificationTitle' AND @Direction = 'ASC' THEN [IVS].[Title] END ASC,
									CASE WHEN @SortField = 'InventoryVoucherSpecificationTitle' AND @Direction = 'DESC' THEN [IVS].[Title] END DESC,
									CASE WHEN @SortField = 'InventoryVoucherSpecificationTitle' AND @Direction = 'ASC' THEN [IVS].[Title] END ASC,
									CASE WHEN @SortField = 'IsSystemic' AND @Direction = 'DESC' THEN [IVS].[IsSystemic] END DESC,
									CASE WHEN @SortField = 'IsSystemic' AND @Direction = 'ASC' THEN [IVS].[IsSystemic] END ASC,
									CASE WHEN @SortField = 'Jsonfield' AND @Direction = 'DESC' THEN [IVS].[Jsonfield] END DESC,
									CASE WHEN @SortField = 'Jsonfield' AND @Direction = 'ASC' THEN [IVS].[Jsonfield] END ASC
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
				var Command = @"INSERT INTO [Store].[InventoryVoucherSpecification]
												(
													[CreatedBy],
													[CreatedDateTime],
													[Title],
													[Comment],
													[InventoryVoucherSpecificationTypeEnumId],
													[ReceiptInventoryVoucherSpecificationId],
													[RemittanceInventoryVoucherSpecificationId],
													[IsSystemic],
													[Jsonfield]
												)
											OUTPUT INSERTED.Id
											VALUES
												(
													@CreatedBy,
													@CreatedDateTime,
													@Title,
													@Comment,
													@InventoryVoucherSpecificationTypeEnumId,
													@ReceiptInventoryVoucherSpecificationId,
													@RemittanceInventoryVoucherSpecificationId,
													@IsSystemic,
													@Jsonfield
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
				var Command = @"UPDATE [Store].[InventoryVoucherSpecification] SET
									[UpdatedBy] = @UpdatedBy,
									[UpdatedDateTime] = @UpdatedDateTime,
									[Title] = @Title,
									[Comment] = @Comment,
									[InventoryVoucherSpecificationTypeEnumId] = @InventoryVoucherSpecificationTypeEnumId,
									[ReceiptInventoryVoucherSpecificationId] = @ReceiptInventoryVoucherSpecificationId,
									[RemittanceInventoryVoucherSpecificationId] = @RemittanceInventoryVoucherSpecificationId,
									[IsSystemic] = @IsSystemic,
									[Jsonfield] = @Jsonfield
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
				var Command = @"Delete [Store].[InventoryVoucherSpecification] WHERE [Id] = @Id ";

				return await Connection.ExecuteAsync(Command, new {Id}, transaction: Transaction);
			}
			catch { throw; }
			#endregion
		}

		public virtual async Task<IEnumerable<TEntity>> GetListByInventoryVoucherSpecificationTypeEnumId(int InventoryVoucherSpecificationTypeEnumId, string? SearchValue = null, string? SortField = null, string? Direction = null, int? PageNumber = null, int? PageSize = null, int? Language = null, bool? EditMode = null)
		{
			#region GetListByInventoryVoucherSpecificationTypeEnumId
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
									[IVS].[Id],
									[IVS].[Version],
									[IVS].[CreatedBy],
									[IVS].[CreatedDateTime],
									[IVS].[UpdatedBy],
									[IVS].[UpdatedDateTime],
									[IVS].[Title],
									[IVS].[Comment],
									[IVSTE].[Title] [InventoryVoucherSpecificationTypeEnumTitle],
									[IVS].[InventoryVoucherSpecificationTypeEnumId],
									[ReceiptInventoryVoucherSpecificationIVS].[Title] [ReceiptInventoryVoucherSpecificationTitle],
									[IVS].[ReceiptInventoryVoucherSpecificationId],
									[RemittanceInventoryVoucherSpecificationIVS].[Title] [RemittanceInventoryVoucherSpecificationTitle],
									[IVS].[RemittanceInventoryVoucherSpecificationId],
									[IVS].[IsSystemic],
									[IVS].[Jsonfield],
									CAST(1 AS BIT) Editable,
									ItemCount = COUNT(*) OVER()
								FROM
									[Store].[InventoryVoucherSpecification] [IVS]
									INNER JOIN [Store].[InventoryVoucherSpecificationTypeEnum] [IVSTE] ON [IVSTE].[Id] = [IVS].[InventoryVoucherSpecificationTypeEnumId]
									LEFT  JOIN [Store].[InventoryVoucherSpecification] [ReceiptInventoryVoucherSpecificationIVS] ON [ReceiptInventoryVoucherSpecificationIVS].[Id] = [IVS].[ReceiptInventoryVoucherSpecificationId]
									LEFT  JOIN [Store].[InventoryVoucherSpecification] [RemittanceInventoryVoucherSpecificationIVS] ON [RemittanceInventoryVoucherSpecificationIVS].[Id] = [IVS].[RemittanceInventoryVoucherSpecificationId]
								WHERE
									[IVS].[InventoryVoucherSpecificationTypeEnumId] = @InventoryVoucherSpecificationTypeEnumId
									AND
									(
										@SearchValue IS NULL
										OR
										(
											[IVS].[Title] LIKE N'%' + @SearchValue + '%'
											OR
											[IVS].[Comment] LIKE N'%' + @SearchValue + '%'
											OR
											[IVSTE].[Title] LIKE N'%' + @SearchValue + '%'
											OR
											[IVS].[Title] LIKE N'%' + @SearchValue + '%'
											OR
											[IVS].[Title] LIKE N'%' + @SearchValue + '%'
											OR
											[IVS].[IsSystemic] LIKE N'%' + @SearchValue + '%'
											OR
											[IVS].[Jsonfield] LIKE N'%' + @SearchValue + '%'
											
										)
									)
								ORDER BY
									CASE WHEN @SortField IS NULL THEN [IVS].[Id] END ASC,
									CASE WHEN @SortField = 'Title' AND @Direction = 'DESC' THEN [IVS].[Title] END DESC,
									CASE WHEN @SortField = 'Title' AND @Direction = 'ASC' THEN [IVS].[Title] END ASC,
									CASE WHEN @SortField = 'Comment' AND @Direction = 'DESC' THEN [IVS].[Comment] END DESC,
									CASE WHEN @SortField = 'Comment' AND @Direction = 'ASC' THEN [IVS].[Comment] END ASC,
									CASE WHEN @SortField = 'InventoryVoucherSpecificationTypeEnumTitle' AND @Direction = 'DESC' THEN [IVSTE].[Title] END DESC,
									CASE WHEN @SortField = 'InventoryVoucherSpecificationTypeEnumTitle' AND @Direction = 'ASC' THEN [IVSTE].[Title] END ASC,
									CASE WHEN @SortField = 'InventoryVoucherSpecificationTitle' AND @Direction = 'DESC' THEN [IVS].[Title] END DESC,
									CASE WHEN @SortField = 'InventoryVoucherSpecificationTitle' AND @Direction = 'ASC' THEN [IVS].[Title] END ASC,
									CASE WHEN @SortField = 'InventoryVoucherSpecificationTitle' AND @Direction = 'DESC' THEN [IVS].[Title] END DESC,
									CASE WHEN @SortField = 'InventoryVoucherSpecificationTitle' AND @Direction = 'ASC' THEN [IVS].[Title] END ASC,
									CASE WHEN @SortField = 'IsSystemic' AND @Direction = 'DESC' THEN [IVS].[IsSystemic] END DESC,
									CASE WHEN @SortField = 'IsSystemic' AND @Direction = 'ASC' THEN [IVS].[IsSystemic] END ASC,
									CASE WHEN @SortField = 'Jsonfield' AND @Direction = 'DESC' THEN [IVS].[Jsonfield] END DESC,
									CASE WHEN @SortField = 'Jsonfield' AND @Direction = 'ASC' THEN [IVS].[Jsonfield] END ASC
								OFFSET @Offset ROWS FETCH NEXT @Size ROWS ONLY ";

				return await Connection.QueryAsync<TEntity>(Command, new {InventoryVoucherSpecificationTypeEnumId, Offset, Size, SearchValue, SortField, Direction, Language, EditMode}, transaction: Transaction);
			}
			catch { throw; }
			#endregion
		}

		public virtual async Task<IEnumerable<TEntity>> GetInventoryVoucherSpecificationInventoryVoucherSpecificationTypeEnumList(bool? EditMode = null)
		{
			#region GetInventoryVoucherSpecificationInventoryVoucherSpecificationTypeEnumList
			try
			{
				var Command = @"SELECT
									[IVSTE].[Id],
									[IVSTE].[Title],
									CAST(1 AS BIT) Editable,
									ItemCount = COUNT(*) OVER()
								FROM
									[Store].[InventoryVoucherSpecificationTypeEnum] [IVSTE] ";

				return await Connection.QueryAsync<TEntity>(Command, new { EditMode }, transaction: Transaction);
			}
			catch { throw; }
			#endregion
		}

		public virtual async Task<IEnumerable<TEntity>> GetInventoryVoucherSpecificationInventoryVoucherSpecificationList(bool? EditMode = null)
		{
			#region GetInventoryVoucherSpecificationInventoryVoucherSpecificationList
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

	}
}
