using Common;
using Common.Enum;
using FluentValidation;
using Microsoft.Data.SqlClient;
using Microsoft.AspNetCore.Http;
using InventorySampleServer.BLL._Base;
using Model.Custom.Other;
using Newtonsoft.Json;
using Model.Custom.State;
using InventorySampleServer.Model.Store.TransferVoucher;
using InventorySampleServer.DAL.Store.TransferVoucher;

namespace InventorySampleServer.BLL._Gen.Store
{
	public class GTransferVoucherBLL<TEntity> : BaseBLL<TEntity> where TEntity : class
	{
		public GTransferVoucherBLL(string ConnectionString, GClaim Claim) : base(ConnectionString, Claim) { }

		public override async Task<ResultDto> GetById(int Id)
		{
			#region GetById
			using var Connection = new SqlConnection(ConnectionString);
			Connection.Open();
			using var Transaction = Connection.BeginTransaction();

			try
			{
				var dal = new TransferVoucherDAL<TransferVoucherListDto>(Connection, Transaction);
				var Data = await dal.GetById(Id) ?? throw new Exception(MessageEnum.رکورد_مورد_نظر_یافت_نشد.EnumToString());

				Transaction.Commit();

				return new Return().ReturnData(Data, StatusType.دریافت);
			}
			catch (Exception ex)
			{
				Transaction.Rollback();
				return new Return().ReturnException(ex);
			}
			#endregion
		}

		public override async Task<ResultDto> GetList(bool? EditMode = null)
		{
			#region GetList
			using var Connection = new SqlConnection(ConnectionString);
			Connection.Open();
			using var Transaction = Connection.BeginTransaction();

			var dal = new TransferVoucherDAL<TransferVoucherListDto>(Connection, Transaction);
			try
			{
				var Data = await dal.GetList(EditMode);
				Transaction.Commit();

				return new Return().ReturnData(Data, StatusType.دریافت);
			}
			catch (Exception ex)
			{
				Transaction.Rollback();
				return new Return().ReturnException(ex);
			}
			#endregion
		}

		public override async Task<ResultDto> GetList(string? SearchValue = null, string? SortField = null, string? Direction = null, int? PageNumber = null, int? PageSize = null, int? Language = null, bool? EditMode = null)
		{
			#region GetList
			using var Connection = new SqlConnection(ConnectionString);
			Connection.Open();
			using var Transaction = Connection.BeginTransaction();

			var dal = new TransferVoucherDAL<TransferVoucherListDto>(Connection, Transaction);
			try
			{
				var Data = await dal.GetList(SearchValue, SortField, Direction, PageNumber, PageSize, Language, EditMode);
				Transaction.Commit();

				return new Return().ReturnData(Data, StatusType.دریافت, Count: Data?.Count() > 0 ? Data.First().ItemCount : 0);
			}
			catch (Exception ex)
			{
				Transaction.Rollback();
				return new Return().ReturnException(ex);
			}
			#endregion
		}

		public override async Task<ResultDto> Add(TEntity Entity)
		{
			#region Add
			using var Connection = new SqlConnection(ConnectionString);
			Connection.Open();
			using var Transaction = Connection.BeginTransaction();

			var dal = new TransferVoucherDAL<TransferVoucherEntity>(Connection, Transaction);
			try
			{
				var Dto = Entity as TransferVoucherEntity ?? throw new Exception(MessageEnum.ثبت_رکورد_با_مشکل_مواجه_شد.EnumToString());
				var Validator = new TransferVoucherValidator();
				var Result = await Validator.ValidateAsync(Dto, op => op.IncludeRuleSets(CrudEnum.Create.ToString()).IncludeRulesNotInRuleSet());
				if (!Result.IsValid)
					return new Return().ReturnValidation(Result.Errors);

				//var StateMachine = new Infrastructure.StateMachineServices.StateMachineServices(Connection, Transaction).GetStateMachineByEntityId(Model.Enum.EntityEnum.TransferVoucher.EnumToInt()).Result.Data as StateMachineDto ?? throw new Exception(MessageEnum.ماشین_وضعیت_ثبت_نشده_است.EnumToString());
				//var EnumEntity = new Infrastructure.StateMachineServices.StateMachineServices(Connection, Transaction).GetEntityEnumById(Model.Enum.EntityEnum.TransferVoucher.EnumToInt()).Result.Data as EntityDto ?? throw new Exception(MessageEnum.اینام_موجودیت_ثبت_نشده_است.EnumToString());
				//var StartState = new Infrastructure.StateMachineServices.StateMachineServices(Connection, Transaction).GetStartState(StateMachine.Id).Result.Data as StateDto ?? throw new Exception(MessageEnum.وضعیت_شروع_برای_ماشین_وضعیت_ثبت_نشده_است.EnumToString());
				//var StateTransitionList = new Infrastructure.StateMachineServices.StateMachineServices(Connection, Transaction).GetStateTransitionList(StartState.Id, StateMachine.Id, Dto.UserId).Result.Data as IEnumerable<StateTransitionListDto>;

				//Dto.StateEnumId = StateTransitionList!.FirstOrDefault()?.TargetStateEnumId;

				Dto.CreatedBy = Claim.UserFullName;
				Dto.CreatedDateTime = DateTime.Now.ToShamsiDateTime();

				var TransferVoucherId = await dal.Add(Dto);

				if (TransferVoucherId > 0)
				{
					//var StateTransition = await new Infrastructure.StateMachineServices.StateMachineServices(Connection, Transaction).ChangeState(new EntityStateTransitionEntity()
					//{
					//	Comment = "",
					//	UserId = Dto.UserId,
					//	EntityEnumId = EnumEntity.Id,
					//	StateEnumId = Dto.StateEnumId,
					//	RecordId = TransferVoucherId,
					//	Date = DateTime.Now.ToShamsiDate(),
					//	Time = DateTime.Now.ToShamsiTime(),
					//	StateTransitionId = StateTransitionList!.First().Id,
					//});
				}
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

		public override async Task<ResultDto> Edit(TEntity Entity)
		{
			#region Edit
			using var Connection = new SqlConnection(ConnectionString);
			Connection.Open();
			using var Transaction = Connection.BeginTransaction();

			var dal = new TransferVoucherDAL<TransferVoucherEntity>(Connection, Transaction);
			try
			{
				var Dto = Entity as TransferVoucherEntity ?? throw new Exception(MessageEnum.ثبت_رکورد_با_مشکل_مواجه_شد.EnumToString());
				var Validator = new TransferVoucherValidator();
				var Result = await Validator.ValidateAsync(Dto, op => op.IncludeRuleSets(CrudEnum.Update.ToString()).IncludeRulesNotInRuleSet());
				if (!Result.IsValid)
					return new Return().ReturnValidation(Result.Errors);


				Dto.UpdatedBy = Claim.UserFullName;
				Dto.UpdatedDateTime = DateTime.Now.ToShamsiDateTime();

				var RowCount = await dal.Edit(Dto);
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

		public override async Task<ResultDto> Delete(int Id)
		{
			#region Delete
			if (Id == 0)
				throw new Exception(MessageEnum.شناسه_مربوطه_نمی_تواند_خالی_باشد.EnumToString());

			using var Connection = new SqlConnection(ConnectionString);
			Connection.Open();
			using var Transaction = Connection.BeginTransaction();

			var dal = new TransferVoucherDAL<TransferVoucherEntity>(Connection, Transaction);
			try
			{
				var Data = await dal.GetObjectById(Id) ?? throw new Exception(MessageEnum.رکورد_مورد_نظر_یافت_نشد.EnumToString());

				var RowCount = await dal.Delete(Id);
				Transaction.Commit();

				return new Return().ReturnData(RowCount, StatusType.حذف);
			}
			catch (Exception ex)
			{
				Transaction.Rollback();
				return new Return().ReturnException(ex);
			}
			#endregion
		}

		public virtual async Task<ResultDto> GetTransferVoucherStoreList(bool? EditMode = null)
		{
			#region GetTransferVoucherStoreList
			using var Connection = new SqlConnection(ConnectionString);
			Connection.Open();
			using var Transaction = Connection.BeginTransaction();

			var dal = new TransferVoucherDAL<TEntity>(Connection, Transaction);
			try
			{
				var Data = await dal.GetTransferVoucherStoreList(EditMode);
				Transaction.Commit();

				return new Return().ReturnData(Data, StatusType.دریافت);
			}
			catch (Exception ex)
			{
				Transaction.Rollback();
				return new Return().ReturnException(ex);
			}
			#endregion
		}

		public virtual async Task<ResultDto> GetTransferVoucherInventoryVoucherList(bool? EditMode = null)
		{
			#region GetTransferVoucherInventoryVoucherList
			using var Connection = new SqlConnection(ConnectionString);
			Connection.Open();
			using var Transaction = Connection.BeginTransaction();

			var dal = new TransferVoucherDAL<TEntity>(Connection, Transaction);
			try
			{
				var Data = await dal.GetTransferVoucherInventoryVoucherList(EditMode);
				Transaction.Commit();

				return new Return().ReturnData(Data, StatusType.دریافت);
			}
			catch (Exception ex)
			{
				Transaction.Rollback();
				return new Return().ReturnException(ex);
			}
			#endregion
		}

		public virtual async Task<ResultDto> GetTransferVoucherInventoryVoucherSpecificationList(bool? EditMode = null)
		{
			#region GetTransferVoucherInventoryVoucherSpecificationList
			using var Connection = new SqlConnection(ConnectionString);
			Connection.Open();
			using var Transaction = Connection.BeginTransaction();

			var dal = new TransferVoucherDAL<TEntity>(Connection, Transaction);
			try
			{
				var Data = await dal.GetTransferVoucherInventoryVoucherSpecificationList(EditMode);
				Transaction.Commit();

				return new Return().ReturnData(Data, StatusType.دریافت);
			}
			catch (Exception ex)
			{
				Transaction.Rollback();
				return new Return().ReturnException(ex);
			}
			#endregion
		}

		public virtual async Task<ResultDto> GetTransferVoucherUserList(bool? EditMode = null)
		{
			#region GetTransferVoucherUserList
			using var Connection = new SqlConnection(ConnectionString);
			Connection.Open();
			using var Transaction = Connection.BeginTransaction();

			var dal = new TransferVoucherDAL<TEntity>(Connection, Transaction);
			try
			{
				var Data = await dal.GetTransferVoucherUserList(EditMode);
				Transaction.Commit();

				return new Return().ReturnData(Data, StatusType.دریافت);
			}
			catch (Exception ex)
			{
				Transaction.Rollback();
				return new Return().ReturnException(ex);
			}
			#endregion
		}

		public virtual async Task<ResultDto> GetTransferVoucherStateEnumList(bool? EditMode = null)
		{
			#region GetTransferVoucherStateEnumList
			using var Connection = new SqlConnection(ConnectionString);
			Connection.Open();
			using var Transaction = Connection.BeginTransaction();

			var dal = new TransferVoucherDAL<TEntity>(Connection, Transaction);
			try
			{
				var Data = await dal.GetTransferVoucherStateEnumList(EditMode);
				Transaction.Commit();

				return new Return().ReturnData(Data, StatusType.دریافت);
			}
			catch (Exception ex)
			{
				Transaction.Rollback();
				return new Return().ReturnException(ex);
			}
			#endregion
		}

		public virtual async Task<ResultDto> GetLastStateById(int Id)
		{
			#region GetLastStatebyId
			using var Connection = new SqlConnection(ConnectionString);
			Connection.Open();
			using var Transaction = Connection.BeginTransaction();

			var dal = new TransferVoucherDAL<LastStateDto>(Connection, Transaction);
			try
			{
				var Data = await dal.GetLastStateById(Id);
				Transaction.Commit();

				return new Return().ReturnData(Data, StatusType.دریافت);
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
