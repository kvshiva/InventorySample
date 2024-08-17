using Common;
using Microsoft.Data.SqlClient;
using Model.Custom.Other;
using InventorySampleServer.BLL._Gen.Part;
using InventorySampleServer.DAL.Part.Part;
using InventorySampleServer.Model.Custom.Other;
using InventorySampleServer.Model.Custom.Part;
using InventorySampleServer.Model.Part.Part;

namespace InventorySampleServer.BLL.Part.Part
{
	public class PartBLL<TEntity> : GPartBLL<TEntity> where TEntity : class
	{
		public PartBLL(string ConnectionString, GClaim Claim) : base(ConnectionString, Claim) { }

        public async Task<ResultDto> GetPartQuantity(int PartId, int StoreId)
        {
            #region GetPartQuantity
            using var Connection = new SqlConnection(ConnectionString);
            Connection.Open();
            using var Transaction = Connection.BeginTransaction();

            try
            {
                var dal = new PartDAL<PartQuantityDto>(Connection, Transaction);
                var Data = await dal.GetPartQuantity(PartId,StoreId);
                Transaction.Commit();
                return new Return().ReturnData(Data, StatusType.دریافت, Count: Data.Count());
            }
            catch (Exception ex)
            {
                Transaction.Rollback();
                return new Return().ReturnException(ex);
            }
            #endregion
        }
        public async Task<ResultDto> GetListByStoreId(GetListParamsDto Params)
        {
            #region GetPartQuantity
            using var Connection = new SqlConnection(ConnectionString);
            Connection.Open();
            using var Transaction = Connection.BeginTransaction();

            try
            {
                var dal = new PartDAL<PartListDto>(Connection, Transaction);
                var Data = await dal.GetListByStoreId(Params);
                Transaction.Commit();
                return new Return().ReturnData(Data, StatusType.دریافت, Count: Data.Count());
            }
            catch (Exception ex)
            {
                Transaction.Rollback();
                return new Return().ReturnException(ex);
            }
            #endregion
        }

    }
}
