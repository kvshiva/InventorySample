using Dapper;
using Microsoft.Data.SqlClient;
using InventorySampleServer.DAL._Gen.Store;
using InventorySampleServer.Model.Custom.InventoryVoucher;
using InventorySampleServer.Model.Custom.Other;

namespace InventorySampleServer.DAL.Store.InventoryVoucher
{
	public class InventoryVoucherDAL<TEntity> : GInventoryVoucherDAL<TEntity> where TEntity : class
	{
		public InventoryVoucherDAL(SqlConnection? Connection, SqlTransaction? Transaction) : base(Connection, Transaction) {  }
        public async Task<IEnumerable<TEntity>> GetListCustom(GetListParamsDto Params)
        {
            #region GetListCustom
            try
            {
                Params.Offset = OffSet(Params.PageNumber, Params.PageSize);

                var Command = @"SELECT
									[IV].[Id],
									[IV].[CreatedBy],
									[IV].[UpdatedBy],
									[IV].[UpdatedDateTime],
									[IV].[InventoryVoucherNo],
									[IV].[Comment],
									[IV].[PersianDate],
									[S].[Title] [StoreTitle],
									[IVS].[Title] [InventoryVoucherSpecificationTitle],
									[IVSTE].[Title] [InventoryVoucherSpecificationTypeEnumTitle],
									--[U].[FullName] [UserFullName],
									[SE].[Title] [StateEnumTitle],
									ItemCount = COUNT(*) OVER()
								FROM
									[Store].[InventoryVoucher] [IV]
									INNER JOIN [Store].[Store] [S] ON [S].[Id] = [IV].[StoreId]
									INNER JOIN [Store].[InventoryVoucherSpecification] [IVS] ON [IVS].[Id] = [IV].[InventoryVoucherSpecificationId]
									INNER JOIN [Store].[InventoryVoucherSpecificationTypeEnum] [IVSTE] ON [IVSTE].[Id] = [IVS].[InventoryVoucherSpecificationTypeEnumId]
									INNER JOIN [General].[User] [U] ON [U].[Id] = [IV].[UserId]
									LEFT  JOIN [State].[EntityEnum] [BaseEntityEE] ON [BaseEntityEE].[Id] = [IV].[BaseEntity]
									LEFT  JOIN [State].[StateEnum] [SE] ON [SE].[Id] = [IV].[StateEnumId]
								WHERE IV.InventoryVoucherSpecificationId =@EntityId";

                return await Connection.QueryAsync<TEntity>(Command,  Params , transaction: Transaction);
            }
            catch { throw; }
            #endregion
        }
        public async Task<InventoryVoucherListVM> GetByIdCustom(int Id)
        {
            #region GetByIdCustom
            try
            {
                var Command = @"SELECT
									[IV].[Id],
									[IV].[CreatedBy],
									[IV].[UpdatedBy],
									[IV].[UpdatedDateTime],
									[IV].[InventoryVoucherNo],
									[IV].[Comment],
									[IV].[PersianDate],
									[IV].[UserId],
									[U].FullName UserFullName,
									[S].[Id] [StoreId],
									[S].[Title] [StoreTitle],
									[IVS].[Id] [InventoryVoucherSpecificationId],
									[IVS].[Title] [InventoryVoucherSpecificationTitle],
									[IVSTE].[Id] [InventoryVoucherSpecificationTypeEnumId],
									[IVSTE].[Title] [InventoryVoucherSpecificationTypeEnumTitle],
									[SE].[Title] [StateEnumTitle],
									ItemCount = COUNT(*) OVER()
								FROM
									[Store].[InventoryVoucher] [IV]
									INNER JOIN [Store].[Store] [S] ON [S].[Id] = [IV].[StoreId]
									INNER JOIN [Store].[InventoryVoucherSpecification] [IVS] ON [IVS].[Id] = [IV].[InventoryVoucherSpecificationId]
									INNER JOIN [Store].[InventoryVoucherSpecificationTypeEnum] [IVSTE] ON [IVSTE].[Id] = [IVS].[InventoryVoucherSpecificationTypeEnumId]
									INNER JOIN [General].[User] [U] ON [U].[Id] = [IV].[UserId]
									LEFT  JOIN [State].[EntityEnum] [BaseEntityEE] ON [BaseEntityEE].[Id] = [IV].[BaseEntity]
									LEFT  JOIN [State].[StateEnum] [SE] ON [SE].[Id] = [IV].[StateEnumId]
									WHERE IV.Id = @Id ";

                return await Connection.QueryFirstOrDefaultAsync<InventoryVoucherListVM>(Command, new { Id }, transaction: Transaction);
            }
            catch { throw; }
            #endregion
        }
        public async Task<string> GenerateInventoryVoucherNo(int InventoryVoucherSpecificationId)
        {
            #region GenerateInventoryVoucherNo
            try
            {
                var Command = @"SELECT 
										MAX(InventoryVoucherNo)+1
									FROM 
										Store.InventoryVoucher
									WHERE
										InventoryVoucherSpecificationId = @InventoryVoucherSpecificationId";

                return await Connection.QueryFirstOrDefaultAsync<string>(Command, new { InventoryVoucherSpecificationId }, transaction: Transaction);
            }
            catch { throw; }
            #endregion
        }
    }
}
