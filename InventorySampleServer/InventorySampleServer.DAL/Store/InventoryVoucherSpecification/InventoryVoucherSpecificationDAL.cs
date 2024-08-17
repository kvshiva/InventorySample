using Dapper;
using Microsoft.Data.SqlClient;
using InventorySampleServer.DAL._Gen.Store;

namespace InventorySampleServer.DAL.Store.InventoryVoucherSpecification
{
	public class InventoryVoucherSpecificationDAL<TEntity> : GInventoryVoucherSpecificationDAL<TEntity> where TEntity : class
	{
		public InventoryVoucherSpecificationDAL(SqlConnection? Connection, SqlTransaction? Transaction) : base(Connection, Transaction) {  }

        // Customized to fix error : A column has been specified more than once in the order by list. Columns in the order by list must be unique
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
									CASE WHEN @SortField = 'IsSystemic' AND @Direction = 'DESC' THEN [IVS].[IsSystemic] END DESC,
									CASE WHEN @SortField = 'IsSystemic' AND @Direction = 'ASC' THEN [IVS].[IsSystemic] END ASC,
									CASE WHEN @SortField = 'Jsonfield' AND @Direction = 'DESC' THEN [IVS].[Jsonfield] END DESC,
									CASE WHEN @SortField = 'Jsonfield' AND @Direction = 'ASC' THEN [IVS].[Jsonfield] END ASC
								OFFSET @Offset ROWS FETCH NEXT @Size ROWS ONLY ";

                return await Connection.QueryAsync<TEntity>(Command, new { Offset, Size, SearchValue, SortField, Direction, Language, EditMode }, transaction: Transaction);
            }
            catch { throw; }
            #endregion
        }
    }
}
