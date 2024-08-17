using Dapper;
using Microsoft.Data.SqlClient;
using InventorySampleServer.DAL._Gen.Store;

namespace InventorySampleServer.DAL.Store.TransferVoucher
{
	public class TransferVoucherDAL<TEntity> : GTransferVoucherDAL<TEntity> where TEntity : class
	{
		public TransferVoucherDAL(SqlConnection? Connection, SqlTransaction? Transaction) : base(Connection, Transaction) {  }

        public async Task<string> GenerateTransferVoucherNo(int InventoryVoucherSpecificationId)
        {
            #region GenerateTransferVoucherNo
            try
            {
                var Command = @"SELECT 
										MAX(TransferVoucherNo)+1
									FROM 
										Store.TransferVoucher
									WHERE
										InventoryVoucherSpecificationId = @InventoryVoucherSpecificationId";

                return await Connection.QueryFirstOrDefaultAsync<string>(Command, new { InventoryVoucherSpecificationId }, transaction: Transaction);
            }
            catch { throw; }
            #endregion
        }
    }
}
