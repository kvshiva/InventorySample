using Dapper;
using Microsoft.Data.SqlClient;
using InventorySampleServer.DAL._Gen.Part;
using InventorySampleServer.Model.Custom.Other;
using InventorySampleServer.Model.Custom.Part;
using InventorySampleServer.Model.Part.Part;

namespace InventorySampleServer.DAL.Part.Part
{
	public class PartDAL<TEntity> : GPartDAL<TEntity> where TEntity : class
	{
		public PartDAL(SqlConnection? Connection, SqlTransaction? Transaction) : base(Connection, Transaction) {  }

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
									[P].[Id],
									[P].[Version],
									[P].[CreatedBy],
									[P].[CreatedDateTime],
									[P].[UpdatedBy],
									[P].[UpdatedDateTime],
									[P].[Title],
									[P].[Code],
									[MainCountUnitCU].[Title] [MainCountUnitTitle],
									[P].[MainCountUnitId],
									[SecondaryCountUnitCU].[Title] [SecondaryCountUnitTitle],
									[P].[SecondaryCountUnitId],
									[C].[Title] [CategoryTitle],
									[P].[CategoryId],
									[P].[HasSerial],
									CAST(1 AS BIT) Editable,
									ItemCount = COUNT(*) OVER()
								FROM
									[Part].[Part] [P]
									INNER JOIN [Part].[CountUnit] [MainCountUnitCU] ON [MainCountUnitCU].[Id] = [P].[MainCountUnitId]
									LEFT  JOIN [Part].[CountUnit] [SecondaryCountUnitCU] ON [SecondaryCountUnitCU].[Id] = [P].[SecondaryCountUnitId]
									INNER JOIN [Part].[Category] [C] ON [C].[Id] = [P].[CategoryId]
								WHERE
									(
										@SearchValue IS NULL
										OR
										(
											[P].[Title] LIKE N'%' + @SearchValue + '%'
											OR
											[P].[Code] LIKE N'%' + @SearchValue + '%'
											OR
											--Start Customization******* To Fix Error : The multi-part identifier \""CU.Title\"" could not be bound
											[MainCountUnitCU].[Title] LIKE N'%' + @SearchValue + '%'
											OR
											[SecondaryCountUnitCU].[Title] LIKE N'%' + @SearchValue + '%'
											--End Customization******
											OR
											[C].[Title] LIKE N'%' + @SearchValue + '%'
											OR
											[P].[HasSerial] LIKE N'%' + @SearchValue + '%'
											
										)
									)
								ORDER BY
									CASE WHEN @SortField IS NULL THEN [P].[Id] END ASC,
									CASE WHEN @SortField = 'Title' AND @Direction = 'DESC' THEN [P].[Title] END DESC,
									CASE WHEN @SortField = 'Title' AND @Direction = 'ASC' THEN [P].[Title] END ASC,
									CASE WHEN @SortField = 'Code' AND @Direction = 'DESC' THEN [P].[Code] END DESC,
									CASE WHEN @SortField = 'Code' AND @Direction = 'ASC' THEN [P].[Code] END ASC,
									--Start Customization***************
									CASE WHEN @SortField = 'MainCountUnitTitle' AND @Direction = 'DESC' THEN [MainCountUnitCU].[Title] END DESC,
									CASE WHEN @SortField = 'MainCountUnitTitle' AND @Direction = 'ASC' THEN [MainCountUnitCU].[Title] END ASC,
									CASE WHEN @SortField = 'SecondaryCountUnitTitle' AND @Direction = 'DESC' THEN [SecondaryCountUnitCU].[Title] END DESC,
									CASE WHEN @SortField = 'SecondaryCountUnitTitle' AND @Direction = 'ASC' THEN [SecondaryCountUnitCU].[Title] END ASC,
									--End Customization****************
									CASE WHEN @SortField = 'CategoryTitle' AND @Direction = 'DESC' THEN [C].[Title] END DESC,
									CASE WHEN @SortField = 'CategoryTitle' AND @Direction = 'ASC' THEN [C].[Title] END ASC,
									CASE WHEN @SortField = 'HasSerial' AND @Direction = 'DESC' THEN [P].[HasSerial] END DESC,
									CASE WHEN @SortField = 'HasSerial' AND @Direction = 'ASC' THEN [P].[HasSerial] END ASC
								OFFSET @Offset ROWS FETCH NEXT @Size ROWS ONLY ";

                return await Connection.QueryAsync<TEntity>(Command, new { Offset, Size, SearchValue, SortField, Direction, Language, EditMode }, transaction: Transaction);
            }
            catch { throw; }
            #endregion
        }
        public async Task<IEnumerable<PartQuantityDto>> GetPartQuantity(int PartId,int StoreId)
        {
            #region GetPartQuantity
            try
            {
                var Command = @"SELECT 
									P.Title,
									P.HasSerial,
									MCU.Title MainCountUnitTitle,
									SCU.Title SecondaryCountUnitTitle,
									SUM(IVI.Value1*Ratio) PartValue1,
									SUM(IVI.Value2*Ratio)PartValue2,
									SerialNo,
									SUM(IVIS.Value1*Ratio) SerialValue1,
									SUM(IVIS.Value2*Ratio) SerialValue2
								FROM 
									Store.InventoryVoucherItem IVI
									INNER JOIN Store.InventoryVoucher IV ON IV.Id = IVI.InventoryVoucherId
									INNER JOIN Store.InventoryVoucherSpecification IVS ON IVS.Id = IV.InventoryVoucherSpecificationId
									INNER JOIN Store.InventoryVoucherSpecificationTypeEnum IVSTE ON IVSTE.Id = IVS.InventoryVoucherSpecificationTypeEnumId
									INNER JOIN Part.Part P ON P.Id = IVI.PartId
									INNER JOIN Part.CountUnit MCU ON P.MainCountUnitId = MCU.Id
									LEFT JOIN Part.CountUnit SCU ON P.SecondaryCountUnitId = SCU.Id
									LEFT JOIN Store.InventoryVoucherItemSerial IVIS ON IVIS.InventoryVoucherItemId = IVI.Id
								WHERE 
									PartId = @PartId
									AND
									StoreId = @StoreId
									GROUP BY P.Title,HasSerial,MCU.Title,SCU.Title,SerialNo ";

                return await Connection.QueryAsync<PartQuantityDto>(Command, new { PartId, StoreId}, transaction: Transaction);
            }
            catch { throw; }
            #endregion
        }
        public async Task<IEnumerable<PartListDto>> GetListByStoreId(GetListParamsDto Params)
        {
            #region GetListByStoreId
            try
            {
                Params.Offset = OffSet(Params.PageNumber, Params.PageSize);

                var Command = @"SELECT
									[P].[Id],
									[P].[Version],
									[P].[CreatedBy],
									[P].[CreatedDateTime],
									[P].[UpdatedBy],
									[P].[UpdatedDateTime],
									[P].[Title],
									[P].[Code],
									[MainCountUnitCU].[Title] [MainCountUnitTitle],
									[P].[MainCountUnitId],
									[SecondaryCountUnitCU].[Title] [SecondaryCountUnitTitle],
									[P].[SecondaryCountUnitId],
									[C].[Title] [CategoryTitle],
									[P].[CategoryId],
									[P].[HasSerial],
									CAST(1 AS BIT) Editable,
									ItemCount = COUNT(*) OVER()
								FROM
									[Part].[Part] [P]
									INNER JOIN [Part].[CountUnit] [MainCountUnitCU] ON [MainCountUnitCU].[Id] = [P].[MainCountUnitId]
									LEFT  JOIN [Part].[CountUnit] [SecondaryCountUnitCU] ON [SecondaryCountUnitCU].[Id] = [P].[SecondaryCountUnitId]
									INNER JOIN [Part].[Category] [C] ON [C].[Id] = [P].[CategoryId]
									INNER JOIN Part.PartStore PS ON PS.PartId = P.Id
								WHERE PS.StoreId = @EntityId
								AND PS.IsActive = 1
								AND
									(
										@SearchValue IS NULL
										OR
										(
											[P].[Title] LIKE N'%' + @SearchValue + '%'
											OR
											[P].[Code] LIKE N'%' + @SearchValue + '%'
											OR
											--Start Customization******* To Fix Error : The multi-part identifier \""CU.Title\"" could not be bound
											[MainCountUnitCU].[Title] LIKE N'%' + @SearchValue + '%'
											OR
											[SecondaryCountUnitCU].[Title] LIKE N'%' + @SearchValue + '%'
											--End Customization******
											OR
											[C].[Title] LIKE N'%' + @SearchValue + '%'
											OR
											[P].[HasSerial] LIKE N'%' + @SearchValue + '%'
											
										)
									)
								ORDER BY
									CASE WHEN @SortField IS NULL THEN [P].[Id] END ASC,
									CASE WHEN @SortField = 'Title' AND @Direction = 'DESC' THEN [P].[Title] END DESC,
									CASE WHEN @SortField = 'Title' AND @Direction = 'ASC' THEN [P].[Title] END ASC,
									CASE WHEN @SortField = 'Code' AND @Direction = 'DESC' THEN [P].[Code] END DESC,
									CASE WHEN @SortField = 'Code' AND @Direction = 'ASC' THEN [P].[Code] END ASC,
									--Start Customization***************
									CASE WHEN @SortField = 'MainCountUnitTitle' AND @Direction = 'DESC' THEN [MainCountUnitCU].[Title] END DESC,
									CASE WHEN @SortField = 'MainCountUnitTitle' AND @Direction = 'ASC' THEN [MainCountUnitCU].[Title] END ASC,
									CASE WHEN @SortField = 'SecondaryCountUnitTitle' AND @Direction = 'DESC' THEN [SecondaryCountUnitCU].[Title] END DESC,
									CASE WHEN @SortField = 'SecondaryCountUnitTitle' AND @Direction = 'ASC' THEN [SecondaryCountUnitCU].[Title] END ASC,
									--End Customization****************
									CASE WHEN @SortField = 'CategoryTitle' AND @Direction = 'DESC' THEN [C].[Title] END DESC,
									CASE WHEN @SortField = 'CategoryTitle' AND @Direction = 'ASC' THEN [C].[Title] END ASC,
									CASE WHEN @SortField = 'HasSerial' AND @Direction = 'DESC' THEN [P].[HasSerial] END DESC,
									CASE WHEN @SortField = 'HasSerial' AND @Direction = 'ASC' THEN [P].[HasSerial] END ASC
								OFFSET @Offset ROWS FETCH NEXT @PageSize ROWS ONLY ";

                return await Connection.QueryAsync<PartListDto>(Command, Params, transaction: Transaction);
            }
            catch { throw; }
            #endregion
        }
    }
}
