using Common.Enum;
using Common;
using Microsoft.Data.SqlClient;
using Model.Custom.Other;
using Model.Custom.State;
using InventorySampleServer.BLL._Gen.Store;
using InventorySampleServer.DAL.Store.TransferVoucher;
using InventorySampleServer.Model.Store.TransferVoucher;
using FluentValidation;
using InventorySampleServer.BLL.Store.InventoryVoucher;
using InventorySampleServer.Model.Store.InventoryVoucher;
using InventorySampleServer.Model.Custom.TransferVoucher;
using InventorySampleServer.Model.Store.InventoryVoucherSpecification;
using InventorySampleServer.DAL.Store.InventoryVoucherSpecification;
using InventorySampleServer.Model.Enum;

namespace InventorySampleServer.BLL.Store.TransferVoucher
{
	public class TransferVoucherBLL<TEntity> : GTransferVoucherBLL<TEntity> where TEntity : class
	{
		public TransferVoucherBLL(string ConnectionString, GClaim Claim) : base(ConnectionString, Claim) { }
        public override async Task<ResultDto> Add(TEntity Entity)
        {
            #region Add
            using var Connection = new SqlConnection(ConnectionString);
            Connection.Open();
            using var Transaction = Connection.BeginTransaction();
            try
            {
                #region AddTransport
                var dal = new TransferVoucherDAL<TransferVoucherEntity>(Connection, Transaction);
                var Dto = Entity as TransferVoucherEntity ?? throw new Exception(MessageEnum.ثبت_رکورد_با_مشکل_مواجه_شد.EnumToString());
                var Validator = new TransferVoucherValidator();
                var Result = await Validator.ValidateAsync(Dto, op => op.IncludeRuleSets(CrudEnum.Create.ToString()).IncludeRulesNotInRuleSet());
                if (!Result.IsValid)
                    return new Return().ReturnValidation(Result.Errors);

                var IVSDal = new InventoryVoucherSpecificationDAL<InventoryVoucherSpecificationListDto>(Connection, Transaction);
                var IVS = await IVSDal.GetById(Dto.InventoryVoucherSpecificationId);
                if (IVS.IsSystemic) throw new Exception ("الگوی وارد شده سیستمی است. امکان ثبت سند وجود ندارد");
         
                //var StateMachine = new Infrastructure.StateMachineServices.StateMachineServices(Connection, Transaction).GetStateMachineByEntityId(Model.Enum.EntityEnum.TransferVoucher.EnumToInt()).Result.Data as StateMachineDto ?? throw new Exception(MessageEnum.ماشین_وضعیت_ثبت_نشده_است.EnumToString());
                //var EnumEntity = new Infrastructure.StateMachineServices.StateMachineServices(Connection, Transaction).GetEntityEnumById(Model.Enum.EntityEnum.TransferVoucher.EnumToInt()).Result.Data as EntityDto ?? throw new Exception(MessageEnum.اینام_موجودیت_ثبت_نشده_است.EnumToString());
                //var StartState = new Infrastructure.StateMachineServices.StateMachineServices(Connection, Transaction).GetStartState(StateMachine.Id).Result.Data as StateDto ?? throw new Exception(MessageEnum.وضعیت_شروع_برای_ماشین_وضعیت_ثبت_نشده_است.EnumToString());
                //var StateTransitionList = new Infrastructure.StateMachineServices.StateMachineServices(Connection, Transaction).GetStateTransitionList(StartState.Id, StateMachine.Id, Dto.UserId).Result.Data as IEnumerable<StateTransitionListDto>;

                //Dto.StateEnumId = StateTransitionList!.FirstOrDefault()?.TargetStateEnumId;

                Dto.CreatedBy = Claim.UserFullName;
                Dto.CreatedDateTime = DateTime.Now.ToShamsiDateTime();

                // Custom Assignments
                Dto.TransferVoucherNo = await dal.GenerateTransferVoucherNo(Dto.InventoryVoucherSpecificationId);
                // End Custom Assignments

                var TransferVoucherId = await dal.Add(Dto);

                if (TransferVoucherId <= 0) throw new Exception("خطا در ثبت سند انتقال");

                //var StateTransition = await new Infrastructure.StateMachineServices.StateMachineServices(Connection, Transaction).ChangeState(new EntityStateTransitionEntity()
                //{
                //    Comment = "",
                //    UserId = Dto.UserId,
                //    EntityEnumId = EnumEntity.Id,
                //    StateEnumId = Dto.StateEnumId,
                //    RecordId = TransferVoucherId,
                //    Date = DateTime.Now.ToShamsiDate(),
                //    Time = DateTime.Now.ToShamsiTime(),
                //    StateTransitionId = StateTransitionList!.First().Id,
                //});
                #endregion

                #region AddRemittance
                var DetailedEntity = Entity as TransferVoucherDetailedEntity ?? throw new Exception(MessageEnum.ثبت_رکورد_با_مشکل_مواجه_شد.EnumToString());
                
                DetailedEntity.RemittanceEntity.InventoryVoucherSpecificationId = IVS.RemittanceInventoryVoucherSpecificationId ?? throw new Exception("الگوی انتخابی شناسه الگوی حواله ندارد");
                DetailedEntity.RemittanceEntity.StoreId = DetailedEntity.SourceStoreId;
                DetailedEntity.RemittanceEntity.UserId = DetailedEntity.UserId;
                DetailedEntity.RemittanceEntity.BaseEntity = EntityEnum.TransferVoucher.EnumToInt();
                DetailedEntity.RemittanceEntity.BaseEntityRef = TransferVoucherId;

                var IVBll = new InventoryVoucherBLL<HddInventoryVoucherDto>(ConnectionString, Claim);
                var RemittanceResult = await IVBll.AddRemittance(DetailedEntity.RemittanceEntity, Connection,Transaction);
                if(!RemittanceResult.IsSucceed)
                {
                    Transaction.Rollback();
                    return RemittanceResult;
                }
                #endregion

                #region AddReceipt
                DetailedEntity.RemittanceEntity.InventoryVoucherSpecificationId = IVS.ReceiptInventoryVoucherSpecificationId ?? throw new Exception("الگوی انتخابی شناسه الگوی رسید ندارد");
                DetailedEntity.RemittanceEntity.StoreId = DetailedEntity.TargetStoreId;
                DetailedEntity.RemittanceEntity.UserId = DetailedEntity.UserId;
                DetailedEntity.RemittanceEntity.BaseEntity = EntityEnum.InventoryVoucher.EnumToInt();
                DetailedEntity.RemittanceEntity.BaseEntityRef = (int?)RemittanceResult.Data;

                var ReceiptResult = await IVBll.AddReceipt(DetailedEntity.RemittanceEntity, Connection, Transaction);
                if (!ReceiptResult.IsSucceed)
                {
                    Transaction.Rollback();
                    return RemittanceResult;
                }
                #endregion

                #region UpdateTransfer
                Dto.SourceInventoryVoucherId = (int?)RemittanceResult.Data;
                Dto.TargetInventoryVoucherId = (int?)ReceiptResult.Data;
                Dto.UpdatedBy = Claim.UserFullName;
                Dto.UpdatedDateTime = DateTime.Now.ToShamsiDateTime();
                var RowCount = await dal.Edit(Dto);
                if (RowCount == 0) throw new Exception("خطا در بروزرسانی سند انتقال");
                #endregion

                Transaction.Commit();
                return new Return().ReturnData(TransferVoucherId, StatusType.ثبت);
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
