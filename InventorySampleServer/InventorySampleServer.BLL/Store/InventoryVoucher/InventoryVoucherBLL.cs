using Common;
using Common.Enum;
using Microsoft.Data.SqlClient;
using Model.Custom.Other;
using Model.Custom.State;
using InventorySampleServer.BLL._Gen.Store;
using InventorySampleServer.DAL._Gen.Store;
using InventorySampleServer.DAL.Store.InventoryVoucher;
using InventorySampleServer.DAL.Store.InventoryVoucherItem;
using InventorySampleServer.DAL.Store.InventoryVoucherItemSerial;
using InventorySampleServer.Model.Custom.InventoryVoucher;
using InventorySampleServer.Model.Custom.Other;
using InventorySampleServer.Model.Store.InventoryVoucher;
using FluentValidation;
using Common.Common;
using InventorySampleServer.DAL.Store.InventoryVoucherSpecification;
using InventorySampleServer.Model.Store.InventoryVoucherSpecification;
using InventorySampleServer.DAL.Part.PartStore;
using InventorySampleServer.Model.Part.PartStore;
using InventorySampleServer.DAL.Part.Part;
using InventorySampleServer.Model.Part.Part;
using static Dapper.SqlMapper;
using InventorySampleServer.Model.Store.InventoryVoucherItemSerial;

namespace InventorySampleServer.BLL.Store.InventoryVoucher
{
	public class InventoryVoucherBLL<TEntity> : GInventoryVoucherBLL<TEntity> where TEntity : class
	{
        public InventoryVoucherBLL(string ConnectionString, GClaim Claim) : base(ConnectionString, Claim) { }
        public async Task<ResultDto> GetListCustom(GetListParamsDto Params)
        {
            #region GetListCustom
            using var Connection = new SqlConnection(ConnectionString);
            Connection.Open();
            using var Transaction = Connection.BeginTransaction();

            var dal = new InventoryVoucherDAL<InventoryVoucherListVM>(Connection, Transaction);
            try
            {
                var Data = await dal.GetListCustom(Params);
                Transaction.Commit();

                return new Return().ReturnData(Data, StatusType.دریافت, Count: Data?.Count() > 0 ? Data.Count() : 0);
            }
            catch (Exception ex)
            {
                Transaction.Rollback();
                return new Return().ReturnException(ex);
            }
            #endregion
        }
        public async Task<ResultDto> GetByIdCustom(int Id)
        {
            #region GetByIdCustom
            using var Connection = new SqlConnection(ConnectionString);
            Connection.Open();
            using var Transaction = Connection.BeginTransaction();

            var dal = new InventoryVoucherDAL<InventoryVoucherListVM>(Connection, Transaction);
            var detailDAL = new InventoryVoucherItemDAL<InventoryVoucherItemListVM>(Connection, Transaction);
            var Entity = new Model.Custom.InventoryVoucher.InventoryVoucherHDVM();
            try
            {
                Entity.Voucher = await dal.GetByIdCustom(Id);
                Entity.Items = (await detailDAL.GetListByInventoryVoucherIdCustom(Id)).ToList();
                Transaction.Commit();

                return new Return().ReturnData(Entity, StatusType.دریافت, Count: Entity.Items.Count());
            }
            catch (Exception ex)
            {
                Transaction.Rollback();
                return new Return().ReturnException(ex);
            }
            #endregion
        }
        public async Task<ResultDto> GetSerialListByInventoryVoucherItemId(GetListParamsDto Params)
        {
            #region GetSerialListByInventoryVoucherItemId
            using var Connection = new SqlConnection(ConnectionString);
            Connection.Open();
            using var Transaction = Connection.BeginTransaction();

            var dal = new InventoryVoucherItemSerialDAL<InventoryVoucherItemSerialListDto>(Connection, Transaction);
            try
            {
                var Data = await dal.GetListByInventoryVoucherItemId(Params.EntityId);
                Transaction.Commit();

                return new Return().ReturnData(Data, StatusType.دریافت, Count: Data?.Count() > 0 ? Data.Count() : 0);
            }
            catch (Exception ex)
            {
                Transaction.Rollback();
                return new Return().ReturnException(ex);
            }
            #endregion
        }
        public async Task<ResultDto> AddReceipt(TEntity Entity)
        {
            #region AddReceipt
            using var Connection = new SqlConnection(ConnectionString);
            Connection.Open();
            using var Transaction = Connection.BeginTransaction();

            var dal = new InventoryVoucherDAL<InventoryVoucherEntity>(Connection, Transaction);
            try
            {
                var Dto = Entity as HddInventoryVoucherDto ?? throw new Exception(MessageEnum.ثبت_رکورد_با_مشکل_مواجه_شد.EnumToString());
                var Validator = new HddInventoryVoucherValidator();
                var Result = await Validator.ValidateAsync(Dto, op => op.IncludeRuleSets(CrudEnum.Create.ToString()).IncludeRulesNotInRuleSet());
                if (!Result.IsValid)
                    return new Return().ReturnValidation(Result.Errors);

                //Start Customization
                var CustomResult = await ValidateReceipt(Dto, Connection, Transaction);
                if (!CustomResult.IsSucceed) return CustomResult;
                //End Customization

                //var StateMachine = new Infrastructure.StateMachineServices.StateMachineServices(Connection, Transaction).GetStateMachineByEntityId(Model.Enum.EntityEnum.InventoryVoucher.EnumToInt()).Result.Data as StateMachineDto ?? throw new Exception(MessageEnum.ماشین_وضعیت_ثبت_نشده_است.EnumToString());
                //var EnumEntity = new Infrastructure.StateMachineServices.StateMachineServices(Connection, Transaction).GetEntityEnumById(Model.Enum.EntityEnum.InventoryVoucher.EnumToInt()).Result.Data as EntityDto ?? throw new Exception(MessageEnum.اینام_موجودیت_ثبت_نشده_است.EnumToString());
                //var StartState = new Infrastructure.StateMachineServices.StateMachineServices(Connection, Transaction).GetStartState(StateMachine.Id).Result.Data as StateDto ?? throw new Exception(MessageEnum.وضعیت_شروع_برای_ماشین_وضعیت_ثبت_نشده_است.EnumToString());
                //var StateTransitionList = new Infrastructure.StateMachineServices.StateMachineServices(Connection, Transaction).GetStateTransitionList(StartState.Id, StateMachine.Id, Dto.UserId).Result.Data as IEnumerable<StateTransitionListDto>;

                //Dto.StateEnumId = StateTransitionList!.FirstOrDefault()?.TargetStateEnumId;

                var InventoryVoucherId = await dal.Add(new InventoryVoucherEntity()
                {
                    // Custom Assignments
                    InventoryVoucherNo = await dal.GenerateInventoryVoucherNo(Dto.InventoryVoucherSpecificationId),
                    DateTime = DateTime.Now,
                    PersianDate = await Conversion.GetServerShamsiDate(),
                    Time = await Conversion.GetServerTime(),
                    //End Custom Assigments

                    Comment = Dto.Comment,
                    SystemComment = Dto.SystemComment,
                    StoreId = Dto.StoreId,
                    InventoryVoucherSpecificationId = Dto.InventoryVoucherSpecificationId,
                    UserId = Dto.UserId,
                    JsonField = Dto.JsonField,
                    BaseEntity = Dto.BaseEntity,
                    BaseEntityRef = Dto.BaseEntityRef,
                    StateEnumId = Dto.StateEnumId,
                    CreatedBy = Claim.UserFullName,
                    CreatedDateTime = DateTime.Now.ToShamsiDateTime()
                });

                if (InventoryVoucherId > 0)
                {
                    if (Dto.InventoryVoucherItemList?.Count > 0)
                    {
                        var InventoryVoucherItemDal = new InventoryVoucherItemDAL<Model.Store.InventoryVoucherItem.InventoryVoucherItemEntity>(Connection, Transaction);
                        
                        foreach (var _InventoryVoucherItem in Dto.InventoryVoucherItemList)
                        {
                            var InventoryVoucherItemId = await InventoryVoucherItemDal.Add(new Model.Store.InventoryVoucherItem.InventoryVoucherItemEntity()
                            {
                                InventoryVoucherId = InventoryVoucherId,
                                PartId = _InventoryVoucherItem.PartId,
                                Value1 = _InventoryVoucherItem.Value1,
                                Value2 = _InventoryVoucherItem.Value2,
                                Comment = _InventoryVoucherItem.Comment,
                                SystemComment = _InventoryVoucherItem.SystemComment,
                                JsonField = _InventoryVoucherItem.JsonField,
                                CreatedBy = Claim.UserFullName,
                                CreatedDateTime = DateTime.Now.ToShamsiDateTime()
                            });

                            if (InventoryVoucherItemId > 0)
                            {
                                if (_InventoryVoucherItem.InventoryVoucherItemSerialList?.Count > 0)
                                {
                                    //Custom Assigments
                                    _InventoryVoucherItem.Value1 = 0;
                                    _InventoryVoucherItem.Value2 = 0;
                                    //End Custom Assigments

                                    var InventoryVoucherItemSerialDal = new InventoryVoucherItemSerialDAL<Model.Store.InventoryVoucherItemSerial.InventoryVoucherItemSerialEntity>(Connection, Transaction);
                                    foreach (var _InventoryVoucherItemSerial in _InventoryVoucherItem.InventoryVoucherItemSerialList)
                                    {
                                        var InventoryVoucherItemSerialId = await InventoryVoucherItemSerialDal.Add(new Model.Store.InventoryVoucherItemSerial.InventoryVoucherItemSerialEntity()
                                        {
                                            InventoryVoucherItemId = InventoryVoucherItemId,
                                            SerialNo = _InventoryVoucherItemSerial.SerialNo,
                                            Value1 = _InventoryVoucherItemSerial.Value1,
                                            Value2 = _InventoryVoucherItemSerial.Value2,
                                            Comment = _InventoryVoucherItemSerial.Comment,
                                            SystemComment = _InventoryVoucherItemSerial.SystemComment,
                                            JsonField = _InventoryVoucherItemSerial.JsonField,
                                            CreatedBy = Claim.UserFullName,
                                            CreatedDateTime = DateTime.Now.ToShamsiDateTime()
                                        });

                                        //Custom Assigments
                                        _InventoryVoucherItem.Value1 += _InventoryVoucherItemSerial.Value1;
                                        _InventoryVoucherItem.Value2 += _InventoryVoucherItemSerial.Value2;
                                        //End Custom Assigments
                                    }
                                }
                            }
                        }
                    }

                    //var StateTransition = await new Infrastructure.StateMachineServices.StateMachineServices(Connection, Transaction).ChangeState(new EntityStateTransitionEntity()
                    //{
                    //    Comment = "",
                    //    Date = DateTime.Now.ToShamsiDate(),
                    //    EntityEnumId = EnumEntity.Id,
                    //    RecordId = InventoryVoucherId,
                    //    StateTransitionId = StateTransitionList!.First().Id,
                    //    Time = DateTime.Now.ToShamsiTime(),
                    //    StateEnumId = Dto.StateEnumId,
                    //    UserId = Dto.UserId
                    //});
                }
                Transaction.Commit();
                return new Return().ReturnData(InventoryVoucherId, StatusType.ثبت);
            }
            catch (Exception ex)
            {
                Transaction.Rollback();
                return new Return().ReturnException(ex);
            }
            #endregion
        }
        public async Task<ResultDto> AddRemittance(TEntity Entity)
        {
            #region AddRemittance
            using var Connection = new SqlConnection(ConnectionString);
            Connection.Open();
            using var Transaction = Connection.BeginTransaction();

            var dal = new InventoryVoucherDAL<InventoryVoucherEntity>(Connection, Transaction);
            var IVSDal = new InventoryVoucherSpecificationDAL<InventoryVoucherSpecificationEntity>(Connection, Transaction);
            var PSDal = new PartStoreDAL<PartStoreListDto>(Connection, Transaction);
            var PDal = new PartDAL<PartEntity>(Connection, Transaction);
            try
            {
                var Dto = Entity as HddInventoryVoucherDto ?? throw new Exception(MessageEnum.ثبت_رکورد_با_مشکل_مواجه_شد.EnumToString());

                //Validation
                var Validator = new HddInventoryVoucherValidator();
                var FluentResult = await Validator.ValidateAsync(Dto, op => op.IncludeRuleSets(CrudEnum.Create.ToString()).IncludeRulesNotInRuleSet());
                if (!FluentResult.IsValid)
                    return new Return().ReturnValidation(FluentResult.Errors);
                var CustomResult = await ValidateRemittance(Dto, Connection, Transaction);
                if (!CustomResult.IsSucceed)
                    return CustomResult;

                //var StateMachine = new Infrastructure.StateMachineServices.StateMachineServices(Connection, Transaction).GetStateMachineByEntityId(Model.Enum.EntityEnum.InventoryVoucher.EnumToInt()).Result.Data as StateMachineDto ?? throw new Exception(MessageEnum.ماشین_وضعیت_ثبت_نشده_است.EnumToString());
                //var EnumEntity = new Infrastructure.StateMachineServices.StateMachineServices(Connection, Transaction).GetEntityEnumById(Model.Enum.EntityEnum.InventoryVoucher.EnumToInt()).Result.Data as EntityDto ?? throw new Exception(MessageEnum.اینام_موجودیت_ثبت_نشده_است.EnumToString());
                //var StartState = new Infrastructure.StateMachineServices.StateMachineServices(Connection, Transaction).GetStartState(StateMachine.Id).Result.Data as StateDto ?? throw new Exception(MessageEnum.وضعیت_شروع_برای_ماشین_وضعیت_ثبت_نشده_است.EnumToString());
                //var StateTransitionList = new Infrastructure.StateMachineServices.StateMachineServices(Connection, Transaction).GetStateTransitionList(StartState.Id, StateMachine.Id, Dto.UserId).Result.Data as IEnumerable<StateTransitionListDto>;

                //Dto.StateEnumId = StateTransitionList!.FirstOrDefault()?.TargetStateEnumId;

                var InventoryVoucherId = await dal.Add(new InventoryVoucherEntity()
                {
                    // Custom Assignments
                    InventoryVoucherNo = await dal.GenerateInventoryVoucherNo(Dto.InventoryVoucherSpecificationId),
                    DateTime = DateTime.Now,
                    PersianDate = await Conversion.GetServerShamsiDate(),
                    Time = await Conversion.GetServerTime(),
                    //End Custom Assigments

                    Comment = Dto.Comment,
                    SystemComment = Dto.SystemComment,
                    StoreId = Dto.StoreId,
                    InventoryVoucherSpecificationId = Dto.InventoryVoucherSpecificationId,
                    UserId = Dto.UserId,
                    JsonField = Dto.JsonField,
                    BaseEntity = Dto.BaseEntity,
                    BaseEntityRef = Dto.BaseEntityRef,
                    StateEnumId = Dto.StateEnumId,
                    CreatedBy = Claim.UserFullName,
                    CreatedDateTime = DateTime.Now.ToShamsiDateTime()
                });

                if (InventoryVoucherId > 0)
                {
                    if (Dto.InventoryVoucherItemList?.Count > 0)
                    {
                        var InventoryVoucherItemDal = new InventoryVoucherItemDAL<Model.Store.InventoryVoucherItem.InventoryVoucherItemEntity>(Connection, Transaction);

                        foreach (var _InventoryVoucherItem in Dto.InventoryVoucherItemList)
                        {
                            var InventoryVoucherItemId = await InventoryVoucherItemDal.Add(new Model.Store.InventoryVoucherItem.InventoryVoucherItemEntity()
                            {
                                InventoryVoucherId = InventoryVoucherId,
                                PartId = _InventoryVoucherItem.PartId,
                                Value1 = _InventoryVoucherItem.Value1,
                                Value2 = _InventoryVoucherItem.Value2,
                                Comment = _InventoryVoucherItem.Comment,
                                SystemComment = _InventoryVoucherItem.SystemComment,
                                JsonField = _InventoryVoucherItem.JsonField,
                                CreatedBy = Claim.UserFullName,
                                CreatedDateTime = DateTime.Now.ToShamsiDateTime()
                            });

                            if (InventoryVoucherItemId > 0)
                            {
                                if (_InventoryVoucherItem.InventoryVoucherItemSerialList?.Count > 0)
                                {
                                    //Custom Assigments
                                    _InventoryVoucherItem.Value1 = 0;
                                    _InventoryVoucherItem.Value2 = 0;
                                    //End Custom Assigments

                                    var InventoryVoucherItemSerialDal = new InventoryVoucherItemSerialDAL<Model.Store.InventoryVoucherItemSerial.InventoryVoucherItemSerialEntity>(Connection, Transaction);
                                    foreach (var _InventoryVoucherItemSerial in _InventoryVoucherItem.InventoryVoucherItemSerialList)
                                    {
                                        var InventoryVoucherItemSerialId = await InventoryVoucherItemSerialDal.Add(new Model.Store.InventoryVoucherItemSerial.InventoryVoucherItemSerialEntity()
                                        {
                                            InventoryVoucherItemId = InventoryVoucherItemId,
                                            SerialNo = _InventoryVoucherItemSerial.SerialNo,
                                            Value1 = _InventoryVoucherItemSerial.Value1,
                                            Value2 = _InventoryVoucherItemSerial.Value2,
                                            Comment = _InventoryVoucherItemSerial.Comment,
                                            SystemComment = _InventoryVoucherItemSerial.SystemComment,
                                            JsonField = _InventoryVoucherItemSerial.JsonField,
                                            CreatedBy = Claim.UserFullName,
                                            CreatedDateTime = DateTime.Now.ToShamsiDateTime()
                                        });

                                        //Custom Assigments
                                        _InventoryVoucherItem.Value1 += _InventoryVoucherItemSerial.Value1;
                                        _InventoryVoucherItem.Value2 += _InventoryVoucherItemSerial.Value2;
                                        //End Custom Assigments
                                    }
                                }
                            }
                        }
                    }

                    //var StateTransition = await new Infrastructure.StateMachineServices.StateMachineServices(Connection, Transaction).ChangeState(new EntityStateTransitionEntity()
                    //{
                    //    Comment = "",
                    //    Date = DateTime.Now.ToShamsiDate(),
                    //    EntityEnumId = EnumEntity.Id,
                    //    RecordId = InventoryVoucherId,
                    //    StateTransitionId = StateTransitionList!.First().Id,
                    //    Time = DateTime.Now.ToShamsiTime(),
                    //    StateEnumId = Dto.StateEnumId,
                    //    UserId = Dto.UserId
                    //});
                }
                Transaction.Commit();
                return new Return().ReturnData(InventoryVoucherId, StatusType.ثبت);
            }
            catch (Exception ex)
            {
                Transaction.Rollback();
                return new Return().ReturnException(ex);
            }
            #endregion
        }
        //For_Transfer
        public async Task<ResultDto> AddReceipt(TEntity Entity, SqlConnection Connection, SqlTransaction Transaction)
        {
            #region AddReceipt
            var dal = new InventoryVoucherDAL<InventoryVoucherEntity>(Connection, Transaction);
            var IVSDal = new InventoryVoucherSpecificationDAL<InventoryVoucherSpecificationEntity>(Connection, Transaction);
            var PSDal = new PartStoreDAL<PartStoreListDto>(Connection, Transaction);
            var PDal = new PartDAL<PartEntity>(Connection, Transaction);
            try
            {
                var Dto = Entity as HddInventoryVoucherDto ?? throw new Exception(MessageEnum.ثبت_رکورد_با_مشکل_مواجه_شد.EnumToString());

                //Validation
                var Validator = new HddInventoryVoucherValidator();
                var FluentResult = await Validator.ValidateAsync(Dto, op => op.IncludeRuleSets(CrudEnum.Create.ToString()).IncludeRulesNotInRuleSet());
                if (!FluentResult.IsValid)
                    return new Return().ReturnValidation(FluentResult.Errors);
                var CustomResult = await ValidateReceipt(Dto, Connection, Transaction);
                if (!CustomResult.IsSucceed)
                {
                    Transaction.Rollback();
                    Connection.Close();
                    return CustomResult;
                }

                //var StateMachine = new Infrastructure.StateMachineServices.StateMachineServices(Connection, Transaction).GetStateMachineByEntityId(Model.Enum.EntityEnum.InventoryVoucher.EnumToInt()).Result.Data as StateMachineDto ?? throw new Exception(MessageEnum.ماشین_وضعیت_ثبت_نشده_است.EnumToString());
                //var EnumEntity = new Infrastructure.StateMachineServices.StateMachineServices(Connection, Transaction).GetEntityEnumById(Model.Enum.EntityEnum.InventoryVoucher.EnumToInt()).Result.Data as EntityDto ?? throw new Exception(MessageEnum.اینام_موجودیت_ثبت_نشده_است.EnumToString());
                //var StartState = new Infrastructure.StateMachineServices.StateMachineServices(Connection, Transaction).GetStartState(StateMachine.Id).Result.Data as StateDto ?? throw new Exception(MessageEnum.وضعیت_شروع_برای_ماشین_وضعیت_ثبت_نشده_است.EnumToString());
                //var StateTransitionList = new Infrastructure.StateMachineServices.StateMachineServices(Connection, Transaction).GetStateTransitionList(StartState.Id, StateMachine.Id, Dto.UserId).Result.Data as IEnumerable<StateTransitionListDto>;

                //Dto.StateEnumId = StateTransitionList!.FirstOrDefault()?.TargetStateEnumId;

                var InventoryVoucherId = await dal.Add(new InventoryVoucherEntity()
                {
                    // Custom Assignments
                    InventoryVoucherNo = await dal.GenerateInventoryVoucherNo(Dto.InventoryVoucherSpecificationId),
                    DateTime = DateTime.Now,
                    PersianDate = await Conversion.GetServerShamsiDate(),
                    Time = await Conversion.GetServerTime(),
                    //End Custom Assigments

                    Comment = Dto.Comment,
                    SystemComment = Dto.SystemComment,
                    StoreId = Dto.StoreId,
                    InventoryVoucherSpecificationId = Dto.InventoryVoucherSpecificationId,
                    UserId = Dto.UserId,
                    JsonField = Dto.JsonField,
                    BaseEntity = Dto.BaseEntity,
                    BaseEntityRef = Dto.BaseEntityRef,
                    StateEnumId = Dto.StateEnumId,
                    CreatedBy = Claim.UserFullName,
                    CreatedDateTime = DateTime.Now.ToShamsiDateTime()
                });

                if (InventoryVoucherId > 0)
                {
                    if (Dto.InventoryVoucherItemList?.Count > 0)
                    {
                        var InventoryVoucherItemDal = new InventoryVoucherItemDAL<Model.Store.InventoryVoucherItem.InventoryVoucherItemEntity>(Connection, Transaction);

                        foreach (var _InventoryVoucherItem in Dto.InventoryVoucherItemList)
                        {
                            var InventoryVoucherItemId = await InventoryVoucherItemDal.Add(new Model.Store.InventoryVoucherItem.InventoryVoucherItemEntity()
                            {
                                InventoryVoucherId = InventoryVoucherId,
                                PartId = _InventoryVoucherItem.PartId,
                                Value1 = _InventoryVoucherItem.Value1,
                                Value2 = _InventoryVoucherItem.Value2,
                                Comment = _InventoryVoucherItem.Comment,
                                SystemComment = _InventoryVoucherItem.SystemComment,
                                JsonField = _InventoryVoucherItem.JsonField,
                                CreatedBy = Claim.UserFullName,
                                CreatedDateTime = DateTime.Now.ToShamsiDateTime()
                            });

                            if (InventoryVoucherItemId > 0)
                            {
                                if (_InventoryVoucherItem.InventoryVoucherItemSerialList?.Count > 0)
                                {
                                    //Custom Assigments
                                    _InventoryVoucherItem.Value1 = 0;
                                    _InventoryVoucherItem.Value2 = 0;
                                    //End Custom Assigments

                                    var InventoryVoucherItemSerialDal = new InventoryVoucherItemSerialDAL<Model.Store.InventoryVoucherItemSerial.InventoryVoucherItemSerialEntity>(Connection, Transaction);
                                    foreach (var _InventoryVoucherItemSerial in _InventoryVoucherItem.InventoryVoucherItemSerialList)
                                    {
                                        var InventoryVoucherItemSerialId = await InventoryVoucherItemSerialDal.Add(new Model.Store.InventoryVoucherItemSerial.InventoryVoucherItemSerialEntity()
                                        {
                                            InventoryVoucherItemId = InventoryVoucherItemId,
                                            SerialNo = _InventoryVoucherItemSerial.SerialNo,
                                            Value1 = _InventoryVoucherItemSerial.Value1,
                                            Value2 = _InventoryVoucherItemSerial.Value2,
                                            Comment = _InventoryVoucherItemSerial.Comment,
                                            SystemComment = _InventoryVoucherItemSerial.SystemComment,
                                            JsonField = _InventoryVoucherItemSerial.JsonField,
                                            CreatedBy = Claim.UserFullName,
                                            CreatedDateTime = DateTime.Now.ToShamsiDateTime()
                                        });

                                        //Custom Assigments
                                        _InventoryVoucherItem.Value1 += _InventoryVoucherItemSerial.Value1;
                                        _InventoryVoucherItem.Value2 += _InventoryVoucherItemSerial.Value2;
                                        //End Custom Assigments
                                    }
                                }
                            }
                        }
                    }

                    //var StateTransition = await new Infrastructure.StateMachineServices.StateMachineServices(Connection, Transaction).ChangeState(new EntityStateTransitionEntity()
                    //{
                    //    Comment = "",
                    //    Date = DateTime.Now.ToShamsiDate(),
                    //    EntityEnumId = EnumEntity.Id,
                    //    RecordId = InventoryVoucherId,
                    //    StateTransitionId = StateTransitionList!.First().Id,
                    //    Time = DateTime.Now.ToShamsiTime(),
                    //    StateEnumId = Dto.StateEnumId,
                    //    UserId = Dto.UserId
                    //});
                }
                //Transaction.Commit();
                return new Return().ReturnData(InventoryVoucherId, StatusType.ثبت);
            }
            catch (Exception ex)
            {
                Transaction.Rollback();
                return new Return().ReturnException(ex);
            }
            #endregion
        }
        //For_Transfer
        public async Task<ResultDto> AddRemittance(TEntity Entity, SqlConnection Connection, SqlTransaction Transaction)
        {
            #region AddRemittance

            var dal = new InventoryVoucherDAL<InventoryVoucherEntity>(Connection, Transaction);
            var IVSDal = new InventoryVoucherSpecificationDAL<InventoryVoucherSpecificationEntity>(Connection, Transaction);
            var PSDal = new PartStoreDAL<PartStoreListDto>(Connection, Transaction);
            var PDal = new PartDAL<PartEntity>(Connection, Transaction);
            try
            {
                var Dto = Entity as HddInventoryVoucherDto ?? throw new Exception(MessageEnum.ثبت_رکورد_با_مشکل_مواجه_شد.EnumToString());

                //Validation
                var Validator = new HddInventoryVoucherValidator();
                var FluentResult = await Validator.ValidateAsync(Dto, op => op.IncludeRuleSets(CrudEnum.Create.ToString()).IncludeRulesNotInRuleSet());
                if (!FluentResult.IsValid)
                    return new Return().ReturnValidation(FluentResult.Errors);
                var CustomResult = await ValidateRemittance(Dto, Connection, Transaction);
                if (!CustomResult.IsSucceed) return CustomResult;

                //var StateMachine = new Infrastructure.StateMachineServices.StateMachineServices(Connection, Transaction).GetStateMachineByEntityId(Model.Enum.EntityEnum.InventoryVoucher.EnumToInt()).Result.Data as StateMachineDto ?? throw new Exception(MessageEnum.ماشین_وضعیت_ثبت_نشده_است.EnumToString());
                //var EnumEntity = new Infrastructure.StateMachineServices.StateMachineServices(Connection, Transaction).GetEntityEnumById(Model.Enum.EntityEnum.InventoryVoucher.EnumToInt()).Result.Data as EntityDto ?? throw new Exception(MessageEnum.اینام_موجودیت_ثبت_نشده_است.EnumToString());
                //var StartState = new Infrastructure.StateMachineServices.StateMachineServices(Connection, Transaction).GetStartState(StateMachine.Id).Result.Data as StateDto ?? throw new Exception(MessageEnum.وضعیت_شروع_برای_ماشین_وضعیت_ثبت_نشده_است.EnumToString());
                //var StateTransitionList = new Infrastructure.StateMachineServices.StateMachineServices(Connection, Transaction).GetStateTransitionList(StartState.Id, StateMachine.Id, Dto.UserId).Result.Data as IEnumerable<StateTransitionListDto>;

               // Dto.StateEnumId = StateTransitionList!.FirstOrDefault()?.TargetStateEnumId;

                var InventoryVoucherId = await dal.Add(new InventoryVoucherEntity()
                {
                    // Custom Assignments
                    InventoryVoucherNo = await dal.GenerateInventoryVoucherNo(Dto.InventoryVoucherSpecificationId),
                    DateTime = DateTime.Now,
                    PersianDate = await Conversion.GetServerShamsiDate(),
                    Time = await Conversion.GetServerTime(),
                    //End Custom Assigments

                    Comment = Dto.Comment,
                    SystemComment = Dto.SystemComment,
                    StoreId = Dto.StoreId,
                    InventoryVoucherSpecificationId = Dto.InventoryVoucherSpecificationId,
                    UserId = Dto.UserId,
                    JsonField = Dto.JsonField,
                    BaseEntity = Dto.BaseEntity,
                    BaseEntityRef = Dto.BaseEntityRef,
                    StateEnumId = Dto.StateEnumId,
                    CreatedBy = Claim.UserFullName,
                    CreatedDateTime = DateTime.Now.ToShamsiDateTime()
                });

                if (InventoryVoucherId > 0)
                {
                    if (Dto.InventoryVoucherItemList?.Count > 0)
                    {
                        var InventoryVoucherItemDal = new InventoryVoucherItemDAL<Model.Store.InventoryVoucherItem.InventoryVoucherItemEntity>(Connection, Transaction);

                        foreach (var _InventoryVoucherItem in Dto.InventoryVoucherItemList)
                        {
                            var InventoryVoucherItemId = await InventoryVoucherItemDal.Add(new Model.Store.InventoryVoucherItem.InventoryVoucherItemEntity()
                            {
                                InventoryVoucherId = InventoryVoucherId,
                                PartId = _InventoryVoucherItem.PartId,
                                Value1 = _InventoryVoucherItem.Value1,
                                Value2 = _InventoryVoucherItem.Value2,
                                Comment = _InventoryVoucherItem.Comment,
                                SystemComment = _InventoryVoucherItem.SystemComment,
                                JsonField = _InventoryVoucherItem.JsonField,
                                CreatedBy = Claim.UserFullName,
                                CreatedDateTime = DateTime.Now.ToShamsiDateTime()
                            });

                            if (InventoryVoucherItemId > 0)
                            {
                                if (_InventoryVoucherItem.InventoryVoucherItemSerialList?.Count > 0)
                                {
                                    //Custom Assigments
                                    _InventoryVoucherItem.Value1 = 0;
                                    _InventoryVoucherItem.Value2 = 0;
                                    //End Custom Assigments

                                    var InventoryVoucherItemSerialDal = new InventoryVoucherItemSerialDAL<Model.Store.InventoryVoucherItemSerial.InventoryVoucherItemSerialEntity>(Connection, Transaction);
                                    foreach (var _InventoryVoucherItemSerial in _InventoryVoucherItem.InventoryVoucherItemSerialList)
                                    {
                                        var InventoryVoucherItemSerialId = await InventoryVoucherItemSerialDal.Add(new Model.Store.InventoryVoucherItemSerial.InventoryVoucherItemSerialEntity()
                                        {
                                            InventoryVoucherItemId = InventoryVoucherItemId,
                                            SerialNo = _InventoryVoucherItemSerial.SerialNo,
                                            Value1 = _InventoryVoucherItemSerial.Value1,
                                            Value2 = _InventoryVoucherItemSerial.Value2,
                                            Comment = _InventoryVoucherItemSerial.Comment,
                                            SystemComment = _InventoryVoucherItemSerial.SystemComment,
                                            JsonField = _InventoryVoucherItemSerial.JsonField,
                                            CreatedBy = Claim.UserFullName,
                                            CreatedDateTime = DateTime.Now.ToShamsiDateTime()
                                        });

                                        //Custom Assigments
                                        _InventoryVoucherItem.Value1 += _InventoryVoucherItemSerial.Value1;
                                        _InventoryVoucherItem.Value2 += _InventoryVoucherItemSerial.Value2;
                                        //End Custom Assigments
                                    }
                                }
                            }
                        }
                    }

                    //var StateTransition = await new Infrastructure.StateMachineServices.StateMachineServices(Connection, Transaction).ChangeState(new EntityStateTransitionEntity()
                    //{
                    //    Comment = "",
                    //    Date = DateTime.Now.ToShamsiDate(),
                    //    EntityEnumId = EnumEntity.Id,
                    //    RecordId = InventoryVoucherId,
                    //    StateTransitionId = StateTransitionList!.First().Id,
                    //    Time = DateTime.Now.ToShamsiTime(),
                    //    StateEnumId = Dto.StateEnumId,
                    //    UserId = Dto.UserId
                    //});
                }
                //Transaction.Commit();
                return new Return().ReturnData(InventoryVoucherId, StatusType.ثبت);
            }
            catch (Exception ex)
            {
                Transaction.Rollback();
                return new Return().ReturnException(ex);
            }
            #endregion
        }
        public async Task<ResultDto> EditReceipt(TEntity Entity)
        {
            #region EditReceipt
			using var Connection = new SqlConnection(ConnectionString);
			Connection.Open();
			using var Transaction = Connection.BeginTransaction();

			var dal = new InventoryVoucherDAL<InventoryVoucherEntity>(Connection, Transaction);
			try
			{
				var Dto = Entity as HddInventoryVoucherDto ?? throw new Exception(MessageEnum.ثبت_رکورد_با_مشکل_مواجه_شد.EnumToString());
				var Validator = new HddInventoryVoucherValidator();
				var Result = await Validator.ValidateAsync(Dto, op => op.IncludeRuleSets(CrudEnum.Update.ToString()).IncludeRulesNotInRuleSet());
                if (!Result.IsValid)
                    return new Return().ReturnValidation(Result.Errors);
                //Start Customization : 
                var CustomResult = await ValidateReceipt(Dto, Connection, Transaction);
                if (!CustomResult.IsSucceed) return CustomResult;
                var OldEntity = await dal.GetById(Dto.Id);
                //End
                var RowCount = await dal.Edit(new InventoryVoucherEntity()
				{
                    // Customization : Assign NonEditable Fileds by OldEntity
                    InventoryVoucherNo = OldEntity.InventoryVoucherNo,
					DateTime = OldEntity.DateTime,
                    Time = OldEntity.Time,
                    SystemComment = OldEntity.SystemComment,
                    InventoryVoucherSpecificationId = OldEntity.InventoryVoucherSpecificationId,
                    UserId = OldEntity.UserId,
                    //End
                    Id = Dto.Id,
                    PersianDate = Dto.PersianDate,
					Comment = Dto.Comment,
					StoreId = Dto.StoreId,
					JsonField = Dto.JsonField,
					BaseEntity = Dto.BaseEntity,
					BaseEntityRef = Dto.BaseEntityRef,
					StateEnumId = Dto.StateEnumId,

					UpdatedBy = Claim.UserFullName,
					UpdatedDateTime = DateTime.Now.ToShamsiDateTime()
                    //End Customization
                });
				if (RowCount > 0)
				{
					if (Dto.InventoryVoucherItemList?.Count > 0)
					{
						var InventoryVoucherItemDal = new DAL.Store.InventoryVoucherItem.InventoryVoucherItemDAL<Model.Store.InventoryVoucherItem.InventoryVoucherItemEntity>(Connection, Transaction);
						foreach (var _InventoryVoucherItem in Dto.InventoryVoucherItemList)
						{
							if (_InventoryVoucherItem.RowState == RowStateEnum.Added.EnumToInt())
							{
								var InventoryVoucherItemId = await InventoryVoucherItemDal.Add(new Model.Store.InventoryVoucherItem.InventoryVoucherItemEntity()
								{
									InventoryVoucherId = Dto.Id,
									PartId = _InventoryVoucherItem.PartId,
									Value1 = _InventoryVoucherItem.Value1,
									Value2 = _InventoryVoucherItem.Value2,
									Comment = _InventoryVoucherItem.Comment,
									SystemComment = _InventoryVoucherItem.SystemComment,
									JsonField = _InventoryVoucherItem.JsonField,
									CreatedBy = Claim.UserFullName,
									CreatedDateTime = DateTime.Now.ToShamsiDateTime()
								});

								if (InventoryVoucherItemId > 0)
								{
									if (_InventoryVoucherItem.InventoryVoucherItemSerialList?.Count > 0)
									{
										var InventoryVoucherItemSerialDal = new DAL.Store.InventoryVoucherItemSerial.InventoryVoucherItemSerialDAL<Model.Store.InventoryVoucherItemSerial.InventoryVoucherItemSerialEntity>(Connection, Transaction);
										foreach (var _InventoryVoucherItemSerial in _InventoryVoucherItem.InventoryVoucherItemSerialList)
										{
											var InventoryVoucherItemSerialId = await InventoryVoucherItemSerialDal.Add(new Model.Store.InventoryVoucherItemSerial.InventoryVoucherItemSerialEntity()
											{
												InventoryVoucherItemId = InventoryVoucherItemId,
												SerialNo = _InventoryVoucherItemSerial.SerialNo,
												Value1 = _InventoryVoucherItemSerial.Value1,
												Value2 = _InventoryVoucherItemSerial.Value2,
												Comment = _InventoryVoucherItemSerial.Comment,
												SystemComment = _InventoryVoucherItemSerial.SystemComment,
												JsonField = _InventoryVoucherItemSerial.JsonField,
												CreatedBy = Claim.UserFullName,
												CreatedDateTime = DateTime.Now.ToShamsiDateTime()
											});
										}
									}

								}
							}
							else if (_InventoryVoucherItem.RowState == RowStateEnum.Modified.EnumToInt())
							{
								RowCount = await InventoryVoucherItemDal.Edit(new Model.Store.InventoryVoucherItem.InventoryVoucherItemEntity()
								{
									Id = _InventoryVoucherItem.Id,
									InventoryVoucherId = Dto.Id,
									PartId = _InventoryVoucherItem.PartId,
									Value1 = _InventoryVoucherItem.Value1,
									Value2 = _InventoryVoucherItem.Value2,
									Comment = _InventoryVoucherItem.Comment,
									SystemComment = _InventoryVoucherItem.SystemComment,
									JsonField = _InventoryVoucherItem.JsonField,
									UpdatedBy = Claim.UserFullName,
									UpdatedDateTime = DateTime.Now.ToShamsiDateTime()
								});

								if (RowCount > 0)
								{
									if (_InventoryVoucherItem.InventoryVoucherItemSerialList?.Count > 0)
									{
										var InventoryVoucherItemSerialDal = new DAL.Store.InventoryVoucherItemSerial.InventoryVoucherItemSerialDAL<Model.Store.InventoryVoucherItemSerial.InventoryVoucherItemSerialEntity>(Connection, Transaction);
										foreach (var _InventoryVoucherItemSerial in _InventoryVoucherItem.InventoryVoucherItemSerialList)
										{
											if (_InventoryVoucherItemSerial.RowState == RowStateEnum.Added.EnumToInt())
											{
												var InventoryVoucherItemSerialId  = await InventoryVoucherItemSerialDal.Add(new Model.Store.InventoryVoucherItemSerial.InventoryVoucherItemSerialEntity()
												{
													InventoryVoucherItemId = _InventoryVoucherItem.Id,
													SerialNo = _InventoryVoucherItemSerial.SerialNo,
													Value1 = _InventoryVoucherItemSerial.Value1,
													Value2 = _InventoryVoucherItemSerial.Value2,
													Comment = _InventoryVoucherItemSerial.Comment,
													SystemComment = _InventoryVoucherItemSerial.SystemComment,
													JsonField = _InventoryVoucherItemSerial.JsonField,
													CreatedBy = Claim.UserFullName,
													CreatedDateTime = DateTime.Now.ToShamsiDateTime()
												});
											}
											else if (_InventoryVoucherItemSerial.RowState == RowStateEnum.Modified.EnumToInt())
											{
												RowCount = await InventoryVoucherItemSerialDal.Edit(new Model.Store.InventoryVoucherItemSerial.InventoryVoucherItemSerialEntity()
												{
													Id = _InventoryVoucherItemSerial.Id,
													InventoryVoucherItemId = _InventoryVoucherItem.Id,
													SerialNo = _InventoryVoucherItemSerial.SerialNo,
													Value1 = _InventoryVoucherItemSerial.Value1,
													Value2 = _InventoryVoucherItemSerial.Value2,
													Comment = _InventoryVoucherItemSerial.Comment,
													SystemComment = _InventoryVoucherItemSerial.SystemComment,
													JsonField = _InventoryVoucherItemSerial.JsonField,
													UpdatedBy = Claim.UserFullName,
													UpdatedDateTime = DateTime.Now.ToShamsiDateTime()
												});
											}
											else if (_InventoryVoucherItemSerial.RowState == RowStateEnum.Deleted.EnumToInt())
											{
												RowCount = await InventoryVoucherItemSerialDal.Delete(_InventoryVoucherItemSerial.Id);
											}
										}
									}

								}
							}
							else if (_InventoryVoucherItem.RowState == RowStateEnum.Deleted.EnumToInt())
							{
								if (RowCount > 0)
								{
									if (_InventoryVoucherItem.InventoryVoucherItemSerialList.Count > 0)
									{
										var InventoryVoucherItemSerialDal = new DAL.Store.InventoryVoucherItemSerial.InventoryVoucherItemSerialDAL<Model.Store.InventoryVoucherItemSerial.InventoryVoucherItemSerialEntity>(Connection, Transaction);
										foreach (var _InventoryVoucherItemSerial in _InventoryVoucherItem.InventoryVoucherItemSerialList)
										{
											RowCount = await InventoryVoucherItemSerialDal.Delete(_InventoryVoucherItemSerial.Id);
										}
									}
								}

								if (RowCount > 0)
								{
									RowCount = await InventoryVoucherItemDal.Delete(_InventoryVoucherItem.Id);
								}
							}

						}
					}

				}
				Transaction.Commit();

				return new Return().ReturnData(RowCount, StatusType.ثبت);
			}
			catch (Exception ex)
			{
				Transaction.Rollback();
				return new Return().ReturnException(ex);
			}
            #endregion
        }
        public async Task<ResultDto> ValidateReceipt(HddInventoryVoucherDto Entity, SqlConnection Connection, SqlTransaction Transaction)
        {
            #region ValidateReceipt
            try
            {
                var Result = new ResultDto() { IsSucceed = false, ErrorList = new List<Common.DTO.ErrorDto>() };
                var IVSDal = new InventoryVoucherSpecificationDAL<InventoryVoucherSpecificationEntity>(Connection, Transaction);
                var PSDal = new PartStoreDAL<PartStoreListDto>(Connection, Transaction);
                var PDal = new PartDAL<PartEntity>(Connection, Transaction);

                // Control Systemic
                var IVS = await IVSDal.GetById(Entity.InventoryVoucherSpecificationId);
                if (IVS.IsSystemic)
                {
                    Result.ErrorList.Add(new Common.DTO.ErrorDto() { ErrorMessage = "الگوی وارد شده سیستمی است. امکان ثبت سند وجود ندارد" });
                }
                else
                {
                    if (Entity.InventoryVoucherItemList?.Count > 0)
                    {
                        //Control PartStore
                        var PartList = await PSDal.GetListByStoreId(Entity.StoreId);
                        foreach (var item in Entity.InventoryVoucherItemList)
                        {
                            var Part = await PDal.GetById(item.PartId);
                            if (!PartList.Any(p => p.PartId == item.PartId))
                            {
                                Result.ErrorList.Add(new Common.DTO.ErrorDto() { ErrorMessage = "کالای " + Part.Title + "در انبار انتخابی گردش ندارد" });
                            }
                            if (!Part.HasSerial) // Control Value2 For Parts without Serial
                            {
                                if (Part.SecondaryCountUnitId == null && item.Value2 != null)
                                {
                                    Result.ErrorList.Add(new Common.DTO.ErrorDto() { ErrorMessage = "امکان وارد کردن مقداردوم برای کالای " + Part.Title + "وجود ندارد" });
                                }
                                if (Part.SecondaryCountUnitId != null && item.Value2 == null)
                                {
                                    Result.ErrorList.Add(new Common.DTO.ErrorDto() { ErrorMessage = "مقدار دوم کالای " + Part.Title + " وارد نشده است" });
                                }
                                if (item.InventoryVoucherItemSerialList != null && item.InventoryVoucherItemSerialList.Count > 0)
                                {
                                    Result.ErrorList.Add(new Common.DTO.ErrorDto() { ErrorMessage = "امکان ثبت سریال برای کالای " + Part.Title + " وجود ندارد" });
                                }
                            }
                            else // Control Serial
                            {
                                if (item.InventoryVoucherItemSerialList == null || item.InventoryVoucherItemSerialList.Count == 0)
                                {
                                    Result.ErrorList.Add(new Common.DTO.ErrorDto() { ErrorMessage = "سریال های کالای " + Part.Title + " وارد نشده است." });
                                }
                                else
                                {
                                    foreach (var serial in item.InventoryVoucherItemSerialList)
                                    {
                                        // Control Value2 For Parts with Serial
                                        if (Part.SecondaryCountUnitId == null && serial.Value2 != null)
                                        {
                                            Result.ErrorList.Add(new Common.DTO.ErrorDto() { ErrorMessage = "امکان وارد کردن مقداردوم برای سریال " + serial.SerialNo + " در کالای " + Part.Title + "وجود ندارد" });
                                        }
                                        if (Part.SecondaryCountUnitId != null && serial.Value2 == null)
                                        {
                                            Result.ErrorList.Add(new Common.DTO.ErrorDto() { ErrorMessage = "مقدار دوم سریال " + serial.SerialNo + " در کالای " + Part.Title + " وارد نشده است" });
                                        }
                                    }
                                }
                            }
                        }
                    }
                }
                if (Result.ErrorList.Count == 0) Result.IsSucceed = true;
                return Result;
            }
            catch(Exception ex)
            {
                Transaction.Rollback();
                return new Return().ReturnException(ex);
            }
            #endregion
        }
        public async Task<ResultDto> ValidateRemittance(HddInventoryVoucherDto Entity, SqlConnection Connection, SqlTransaction Transaction)
        {
            #region ValidateRemittance
            try
            {
                var Result = new ResultDto() { IsSucceed = false, ErrorList = new List<Common.DTO.ErrorDto>() };
                var IVSDal = new InventoryVoucherSpecificationDAL<InventoryVoucherSpecificationEntity>(Connection, Transaction);
                var PSDal = new PartStoreDAL<PartStoreListDto>(Connection, Transaction);
                var PDal = new PartDAL<PartEntity>(Connection, Transaction);

                // Control Systemic
                var IVS = await IVSDal.GetById(Entity.InventoryVoucherSpecificationId);
                if (IVS.IsSystemic)
                    Result.ErrorList.Add(new Common.DTO.ErrorDto() { ErrorMessage = "الگوی وارد شده سیستمی است. امکان ثبت سند وجود ندارد" });
                else
                {
                    if (Entity.InventoryVoucherItemList?.Count > 0)
                    {
                        //Get Store's PartList
                        var PartList = await PSDal.GetListByStoreId(Entity.StoreId);
                        foreach (var item in Entity.InventoryVoucherItemList)
                        {
                            // Control PartStore
                            if (!PartList.Any(p => p.PartId == item.PartId))
                                Result.ErrorList.Add(new Common.DTO.ErrorDto() { ErrorMessage = "کالای " + item.PartTitle + "در انبار انتخابی گردش ندارد" });
                            else
                            {
                                var Part = await PDal.GetById(item.PartId);
                                var Quantity = await PDal.GetPartQuantity(item.PartId, Entity.StoreId);
                                if (!Part.HasSerial) // Control Parts without Serial
                                {
                                    // Control Part Value1 Quantity
                                    if (item.Value1 > Quantity.First().PartValue1)
                                        Result.ErrorList.Add(new Common.DTO.ErrorDto() { ErrorMessage = "مقداراول وارد شده برای کالای " + Part.Title + " بیشتر از موجودی کالا است." });

                                    // Control Part SecondaryCountUnit
                                    if (Part.SecondaryCountUnitId == null && item.Value2 != null)
                                        Result.ErrorList.Add(new Common.DTO.ErrorDto() { ErrorMessage = "امکان وارد کردن مقداردوم برای کالای " + Part.Title + "وجود ندارد" });

                                    if (Part.SecondaryCountUnitId != null)
                                    {
                                        if (item.Value2 == null)
                                            Result.ErrorList.Add(new Common.DTO.ErrorDto() { ErrorMessage = "مقدار دوم کالای " + Part.Title + " وارد نشده است" });
                                        else if (item.Value2 > Quantity.First().PartValue2) // Control Part Value2 Quantity  
                                            Result.ErrorList.Add(new Common.DTO.ErrorDto() { ErrorMessage = "مقدار دوم وارد شده برای کالای " + Part.Title + " بیشتر از موجودی کالا است." });
                                    }
                                    if (item.InventoryVoucherItemSerialList?.Count > 0)
                                        Result.ErrorList.Add(new Common.DTO.ErrorDto() { ErrorMessage = "امکان ثبت سریال برای کالای " + Part.Title + " وجود ندارد" });
                                }
                                else // Control Parts with Serial
                                {
                                    if (item.InventoryVoucherItemSerialList == null || item.InventoryVoucherItemSerialList.Count == 0)
                                        Result.ErrorList.Add(new Common.DTO.ErrorDto() { ErrorMessage = "سریال های کالای " + Part.Title + " وارد نشده است." });
                                    else
                                    {
                                        foreach (var serial in item.InventoryVoucherItemSerialList)
                                        {
                                            // Control Serial SerialNo
                                            var SerialQuantity = Quantity.Where(s => s.SerialNo == serial.SerialNo).First();
                                            if (SerialQuantity == null)
                                                Result.ErrorList.Add(new Common.DTO.ErrorDto() { ErrorMessage = "سریال " + serial.SerialNo + " واردشده برای کالای " + Part.Title + " وجود ندارد." });
                                            else
                                            {
                                                // Control Serial Value1 Equality
                                                if (serial.Value1 != SerialQuantity.SerialValue1)
                                                    Result.ErrorList.Add(new Common.DTO.ErrorDto() { ErrorMessage = "مقدار اول سریال " + serial.SerialNo + " واردشده برای کالای " + Part.Title + " غیرمجاز است." });

                                                // Control Part SecondCountUnit
                                                if (Part.SecondaryCountUnitId == null && serial.Value2 != null)
                                                    Result.ErrorList.Add(new Common.DTO.ErrorDto() { ErrorMessage = "امکان وارد کردن مقداردوم برای سریال " + serial.SerialNo + " در کالای " + Part.Title + "وجود ندارد" });

                                                if (Part.SecondaryCountUnitId != null)
                                                {
                                                    if (serial.Value2 == null)
                                                        Result.ErrorList.Add(new Common.DTO.ErrorDto() { ErrorMessage = "مقدار دوم سریال " + serial.SerialNo + " در کالای " + Part.Title + " وارد نشده است" });
                                                    // Control Serial Value2 Equality
                                                    else if (serial.Value2 != SerialQuantity.SerialValue2)
                                                        Result.ErrorList.Add(new Common.DTO.ErrorDto() { ErrorMessage = "مقدار دوم سریال " + serial.SerialNo + " واردشده برای کالای " + Part.Title + " غیرمجاز است." });
                                                }
                                            }
                                        }
                                    }
                                }
                            }
                        }
                    }
                }
                if (Result.ErrorList.Count == 0) Result.IsSucceed = true;
                return Result;
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
