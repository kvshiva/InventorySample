using Common;
using Common.Enum;
using FluentValidation;
using Microsoft.Data.SqlClient;
using Microsoft.AspNetCore.Http;
using InventorySampleServer.BLL._Base;
using Model.Custom.Other;
using Newtonsoft.Json;
using Model.Custom.State;
using InventorySampleServer.Model.Store.InventoryVoucher;
using InventorySampleServer.DAL.Store.InventoryVoucher;

namespace InventorySampleServer.BLL._Gen.Store
{
	public class GInventoryVoucherBLL<TEntity> : BaseBLL<TEntity> where TEntity : class
	{
		public GInventoryVoucherBLL(string ConnectionString, GClaim Claim) : base(ConnectionString, Claim) { }

		public override async Task<ResultDto> GetById(int Id)
		{
			#region GetById
			using var Connection = new SqlConnection(ConnectionString);
			Connection.Open();
			using var Transaction = Connection.BeginTransaction();

			try
			{
				var dal = new InventoryVoucherDAL<InventoryVoucherListDto>(Connection, Transaction);
				var HData = await dal.GetById(Id);

				var InventoryVoucherItemDal = new DAL.Store.InventoryVoucherItem.InventoryVoucherItemDAL<Model.Store.InventoryVoucherItem.InventoryVoucherItemListDto>(Connection, Transaction);
				var InventoryVoucherItemList = new List<Model.Store.InventoryVoucherItem.HddInventoryVoucherItemListDto>();
				var HdInventoryVoucherItemData = await InventoryVoucherItemDal.GetListByInventoryVoucherId(HData.Id);
				foreach (var DData in HdInventoryVoucherItemData)
				{
					var InventoryVoucherItemSerialDal = new DAL.Store.InventoryVoucherItemSerial.InventoryVoucherItemSerialDAL<Model.Store.InventoryVoucherItemSerial.InventoryVoucherItemSerialListDto>(Connection, Transaction);
					var InventoryVoucherItemSerialList = new List<Model.Store.InventoryVoucherItemSerial.HddlInventoryVoucherItemSerialListDto>();
					var HddInventoryVoucherItemSerialData = await InventoryVoucherItemSerialDal.GetListByInventoryVoucherItemId(DData.Id);
					foreach (var DoDData in HddInventoryVoucherItemSerialData)
					{
						InventoryVoucherItemSerialList.Add(new Model.Store.InventoryVoucherItemSerial.HddlInventoryVoucherItemSerialListDto()
						{
							Id = DoDData.Id,
							Version = DoDData.Version,
							CreatedBy = DoDData.CreatedBy,
							CreatedDateTime = DoDData.CreatedDateTime,
							UpdatedBy = DoDData.UpdatedBy,
							UpdatedDateTime = DoDData.UpdatedDateTime,
							InventoryVoucherItemId = DoDData.InventoryVoucherItemId,
							SerialNo = DoDData.SerialNo,
							Value1 = DoDData.Value1,
							Value2 = DoDData.Value2,
							Comment = DoDData.Comment,
							SystemComment = DoDData.SystemComment,
							JsonField = DoDData.JsonField,
							ItemCount = DoDData.ItemCount,
							Editable = true
						});
					}

					InventoryVoucherItemList.Add(new Model.Store.InventoryVoucherItem.HddInventoryVoucherItemListDto()
					{
						InventoryVoucherId = DData.InventoryVoucherId,
						PartTitle = DData.PartTitle,
						PartId = DData.PartId,
						Value1 = DData.Value1,
						Value2 = DData.Value2,
						Comment = DData.Comment,
						SystemComment = DData.SystemComment,
						JsonField = DData.JsonField,
						Id = DData.Id,
						Version = DData.Version,
						CreatedBy = DData.CreatedBy,
						CreatedDateTime = DData.CreatedDateTime,
						UpdatedBy = DData.UpdatedBy,
						UpdatedDateTime = DData.UpdatedDateTime,
						ItemCount = DData.ItemCount,
						Editable = true,
						InventoryVoucherItemSerialList = InventoryVoucherItemSerialList
					});
				}

				var Data = new HddInventoryVoucherDto()
				{
					Id = HData.Id,
					Version = HData.Version,
					CreatedBy = HData.CreatedBy,
					CreatedDateTime = HData.CreatedDateTime,
					UpdatedBy = HData.UpdatedBy,
					UpdatedDateTime = HData.UpdatedDateTime,
					InventoryVoucherNo = HData.InventoryVoucherNo,
					DateTime = HData.DateTime,
					PersianDate = HData.PersianDate,
					Time = HData.Time,
					Comment = HData.Comment,
					SystemComment = HData.SystemComment,
					StoreTitle = HData.StoreTitle,
					StoreId = HData.StoreId,
					InventoryVoucherSpecificationTitle = HData.InventoryVoucherSpecificationTitle,
					InventoryVoucherSpecificationId = HData.InventoryVoucherSpecificationId,
					UserFullName = HData.UserFullName,
					UserId = HData.UserId,
					JsonField = HData.JsonField,
					BaseEntityTitle = HData.BaseEntityTitle,
					BaseEntity = HData.BaseEntity,
					BaseEntityRef = HData.BaseEntityRef,
					StateEnumTitle = HData.StateEnumTitle,
					StateEnumId = HData.StateEnumId,
					InventoryVoucherItemList = InventoryVoucherItemList
				};

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

			var dal = new InventoryVoucherDAL<InventoryVoucherListDto>(Connection, Transaction);
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

			var dal = new InventoryVoucherDAL<InventoryVoucherListDto>(Connection, Transaction);
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

			var dal = new InventoryVoucherDAL<InventoryVoucherEntity>(Connection, Transaction);
			try
			{
				var Dto = Entity as HddInventoryVoucherDto ?? throw new Exception(MessageEnum.ثبت_رکورد_با_مشکل_مواجه_شد.EnumToString());
				var Validator = new HddInventoryVoucherValidator();
				var Result = await Validator.ValidateAsync(Dto, op => op.IncludeRuleSets(CrudEnum.Create.ToString()).IncludeRulesNotInRuleSet());
				if (!Result.IsValid)
					return new Return().ReturnValidation(Result.Errors);

				//var StateMachine = new Infrastructure.StateMachineServices.StateMachineServices(Connection, Transaction).GetStateMachineByEntityId(Model.Enum.EntityEnum.InventoryVoucher.EnumToInt()).Result.Data as StateMachineDto ?? throw new Exception(MessageEnum.ماشین_وضعیت_ثبت_نشده_است.EnumToString());
				//var EnumEntity = new Infrastructure.StateMachineServices.StateMachineServices(Connection, Transaction).GetEntityEnumById(Model.Enum.EntityEnum.InventoryVoucher.EnumToInt()).Result.Data as EntityDto ?? throw new Exception(MessageEnum.اینام_موجودیت_ثبت_نشده_است.EnumToString());
				//var StartState = new Infrastructure.StateMachineServices.StateMachineServices(Connection, Transaction).GetStartState(StateMachine.Id).Result.Data as StateDto ?? throw new Exception(MessageEnum.وضعیت_شروع_برای_ماشین_وضعیت_ثبت_نشده_است.EnumToString());
				//var StateTransitionList = new Infrastructure.StateMachineServices.StateMachineServices(Connection, Transaction).GetStateTransitionList(StartState.Id, StateMachine.Id, Dto.UserId).Result.Data as IEnumerable<StateTransitionListDto>;

				//Dto.StateEnumId = StateTransitionList!.FirstOrDefault()?.TargetStateEnumId;
				var InventoryVoucherId = await dal.Add(new InventoryVoucherEntity()
				{
					InventoryVoucherNo = Dto.InventoryVoucherNo,
					DateTime = Dto.DateTime,
					PersianDate = Dto.PersianDate,
					Time = Dto.Time,
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
						var InventoryVoucherItemDal = new DAL.Store.InventoryVoucherItem.InventoryVoucherItemDAL<Model.Store.InventoryVoucherItem.InventoryVoucherItemEntity>(Connection, Transaction);
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
					}

					//var StateTransition = await new Infrastructure.StateMachineServices.StateMachineServices(Connection, Transaction).ChangeState(new EntityStateTransitionEntity()
					//{
					//	Comment = "",
					//	Date = DateTime.Now.ToShamsiDate(),
					//	EntityEnumId = EnumEntity.Id,
					//	RecordId = InventoryVoucherId,
					//	StateTransitionId = StateTransitionList!.First().Id,
					//	Time = DateTime.Now.ToShamsiTime(),
					//	StateEnumId = Dto.StateEnumId,
					//	UserId = Dto.UserId
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

		public override async Task<ResultDto> Edit(TEntity Entity)
		{
			#region Edit
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

				var RowCount = await dal.Edit(new InventoryVoucherEntity()
				{
					Id = Dto.Id,
					InventoryVoucherNo = Dto.InventoryVoucherNo,
					DateTime = Dto.DateTime,
					PersianDate = Dto.PersianDate,
					Time = Dto.Time,
					Comment = Dto.Comment,
					SystemComment = Dto.SystemComment,
					StoreId = Dto.StoreId,
					InventoryVoucherSpecificationId = Dto.InventoryVoucherSpecificationId,
					UserId = Dto.UserId,
					JsonField = Dto.JsonField,
					BaseEntity = Dto.BaseEntity,
					BaseEntityRef = Dto.BaseEntityRef,
					StateEnumId = Dto.StateEnumId,

					UpdatedBy = Claim.UserFullName,
					UpdatedDateTime = DateTime.Now.ToShamsiDateTime()

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

		public override async Task<ResultDto> Delete(int Id)
		{
			#region Delete
			if (Id == 0)
				throw new Exception(MessageEnum.شناسه_مربوطه_نمی_تواند_خالی_باشد.EnumToString());

			using var Connection = new SqlConnection(ConnectionString);
			Connection.Open();
			using var Transaction = Connection.BeginTransaction();

			var dal = new InventoryVoucherDAL<InventoryVoucherEntity>(Connection, Transaction);
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

		public virtual async Task<ResultDto> GetListByInventoryVoucherSpecificationId(int InventoryVoucherSpecificationId, string? SearchValue = null, string? SortField = null, string? Direction = null, int? PageNumber = null, int? PageSize = null, int? Language = null, bool? EditMode = null)
		{
			#region GetListByInventoryVoucherSpecificationId
			using var Connection = new SqlConnection(ConnectionString);
			Connection.Open();
			using var Transaction = Connection.BeginTransaction();

			var dal = new InventoryVoucherDAL<InventoryVoucherListDto>(Connection, Transaction);
			try
			{
				var Data = await dal.GetListByInventoryVoucherSpecificationId(InventoryVoucherSpecificationId, SearchValue, SortField, Direction, PageNumber, PageSize, Language, EditMode);
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

		public virtual async Task<ResultDto> GetInventoryVoucherStoreList(bool? EditMode = null)
		{
			#region GetInventoryVoucherStoreList
			using var Connection = new SqlConnection(ConnectionString);
			Connection.Open();
			using var Transaction = Connection.BeginTransaction();

			var dal = new InventoryVoucherDAL<TEntity>(Connection, Transaction);
			try
			{
				var Data = await dal.GetInventoryVoucherStoreList(EditMode);
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

		public virtual async Task<ResultDto> GetInventoryVoucherInventoryVoucherSpecificationList(bool? EditMode = null)
		{
			#region GetInventoryVoucherInventoryVoucherSpecificationList
			using var Connection = new SqlConnection(ConnectionString);
			Connection.Open();
			using var Transaction = Connection.BeginTransaction();

			var dal = new InventoryVoucherDAL<TEntity>(Connection, Transaction);
			try
			{
				var Data = await dal.GetInventoryVoucherInventoryVoucherSpecificationList(EditMode);
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

		public virtual async Task<ResultDto> GetInventoryVoucherUserList(bool? EditMode = null)
		{
			#region GetInventoryVoucherUserList
			using var Connection = new SqlConnection(ConnectionString);
			Connection.Open();
			using var Transaction = Connection.BeginTransaction();

			var dal = new InventoryVoucherDAL<TEntity>(Connection, Transaction);
			try
			{
				var Data = await dal.GetInventoryVoucherUserList(EditMode);
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

		public virtual async Task<ResultDto> GetInventoryVoucherEntityEnumList(bool? EditMode = null)
		{
			#region GetInventoryVoucherEntityEnumList
			using var Connection = new SqlConnection(ConnectionString);
			Connection.Open();
			using var Transaction = Connection.BeginTransaction();

			var dal = new InventoryVoucherDAL<TEntity>(Connection, Transaction);
			try
			{
				var Data = await dal.GetInventoryVoucherEntityEnumList(EditMode);
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

		public virtual async Task<ResultDto> GetInventoryVoucherStateEnumList(bool? EditMode = null)
		{
			#region GetInventoryVoucherStateEnumList
			using var Connection = new SqlConnection(ConnectionString);
			Connection.Open();
			using var Transaction = Connection.BeginTransaction();

			var dal = new InventoryVoucherDAL<TEntity>(Connection, Transaction);
			try
			{
				var Data = await dal.GetInventoryVoucherStateEnumList(EditMode);
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

		public virtual async Task<ResultDto> GetInventoryVoucherItemPartList(bool? EditMode = null)
		{
			#region GetInventoryVoucherItemPartList
			using var Connection = new SqlConnection(ConnectionString);
			Connection.Open();
			using var Transaction = Connection.BeginTransaction();

			var dal = new DAL.Part.Part.PartDAL<Model.Part.Part.PartListDto>(Connection, Transaction);
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

		public virtual async Task<ResultDto> GetInventoryVoucherItemSerialInventoryVoucherItemList(bool? EditMode = null)
		{
			#region GetInventoryVoucherItemSerialInventoryVoucherItemList
			using var Connection = new SqlConnection(ConnectionString);
			Connection.Open();
			using var Transaction = Connection.BeginTransaction();

			var dal = new DAL.Store.InventoryVoucherItem.InventoryVoucherItemDAL<Model.Store.InventoryVoucherItem.InventoryVoucherItemListDto>(Connection, Transaction);
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

		public virtual async Task<ResultDto> GetLastStateById(int Id)
		{
			#region GetLastStatebyId
			using var Connection = new SqlConnection(ConnectionString);
			Connection.Open();
			using var Transaction = Connection.BeginTransaction();

			var dal = new InventoryVoucherDAL<LastStateDto>(Connection, Transaction);
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
