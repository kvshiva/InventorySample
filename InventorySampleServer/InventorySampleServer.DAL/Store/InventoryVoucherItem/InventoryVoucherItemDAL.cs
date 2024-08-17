using Dapper;
using Microsoft.Data.SqlClient;
using InventorySampleServer.DAL._Gen.Store;
using InventorySampleServer.Model.Custom.InventoryVoucher;

namespace InventorySampleServer.DAL.Store.InventoryVoucherItem
{
	public class InventoryVoucherItemDAL<TEntity> : GInventoryVoucherItemDAL<TEntity> where TEntity : class
	{
		public InventoryVoucherItemDAL(SqlConnection? Connection, SqlTransaction? Transaction) : base(Connection, Transaction) {  }

        public async Task<IEnumerable<InventoryVoucherItemListVM>> GetListByInventoryVoucherIdCustom(int InventoryVoucherId)
        {
            #region GetListByInventoryVoucherIdCustom
            try
            {
                var Command = @"SELECT
									[IVI].[Id],
									[IVI].[InventoryVoucherId],
									[P].[Id] PartId,
									[P].Title PartTitle,
									[IVI].Comment,
									[IVI].Value1,
									[IVI].Value2,
									[MCU].Title Unit1,
									[SCU].Title Unit2,
									[P].HasSerial,
									[P].Code PartCode
								FROM
									[Store].[InventoryVoucher] [IV]
									INNER JOIN [Store].[InventoryVoucherItem] [IVI] ON [IVI].[InventoryVoucherId] = [IV].[Id]
									INNER JOIN [Part].[Part] P ON P.Id = IVI.PartId 
									INNER JOIN Part.CountUnit MCU ON MCU.Id = P.MainCountUnitId
									LEFT JOIN Part.CountUnit SCU ON SCU.Id = P.SecondaryCountUnitId

								WHERE
									[IV].[Id] = @InventoryVoucherId ";

                return await Connection.QueryAsync<InventoryVoucherItemListVM>(Command, new { InventoryVoucherId }, transaction: Transaction);
            }
            catch { throw; }
            #endregion
        }
    }
}
