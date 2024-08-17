using Common;
using Common.Enum;
using FluentValidation;
using Microsoft.Data.SqlClient;
using Microsoft.AspNetCore.Http;
using InventorySampleServer.BLL._Base;
using Model.Custom.Other;
using InventorySampleServer.Model.Part.Part;
using InventorySampleServer.DAL.Part.Part;

namespace InventorySampleServer.BLL._Gen.Part
{
	public class GPartBLL<TEntity> : BaseBLL<TEntity> where TEntity : class
	{
		public GPartBLL(string ConnectionString, GClaim Claim) : base(ConnectionString, Claim) { }

		public override async Task<ResultDto> GetById(int Id)
		{
			#region GetById
			using var Connection = new SqlConnection(ConnectionString);
			Connection.Open();
			using var Transaction = Connection.BeginTransaction();

			try
			{
				var dal = new PartDAL<PartListDto>(Connection, Transaction);
				var HData = await dal.GetById(Id);

				var PartStoreDal = new DAL.Part.PartStore.PartStoreDAL<Model.Part.PartStore.PartStoreListDto>(Connection, Transaction);
				var PartStoreList = new List<Model.Part.PartStore.HdPartStoreListDto>();
				var HdPartStoreData = await PartStoreDal.GetListByPartId(HData.Id);
				foreach (var DData in HdPartStoreData)
				{
					PartStoreList.Add(new Model.Part.PartStore.HdPartStoreListDto()
					{
						Id = DData.Id,
						Version = DData.Version,
						CreatedBy = DData.CreatedBy,
						CreatedDateTime = DData.CreatedDateTime,
						UpdatedBy = DData.UpdatedBy,
						UpdatedDateTime = DData.UpdatedDateTime,
						PartTitle = DData.PartTitle,
						PartId = DData.PartId,
						StoreTitle = DData.StoreTitle,
						StoreId = DData.StoreId,
						IsActive = DData.IsActive,
						IsDefault = DData.IsDefault,
						Comment = DData.Comment,
						ItemCount = DData.ItemCount,
						Editable = true
					});
				}

				var Data = new HdPartDto()
				{
					Id = HData.Id,
					Version = HData.Version,
					CreatedBy = HData.CreatedBy,
					CreatedDateTime = HData.CreatedDateTime,
					UpdatedBy = HData.UpdatedBy,
					UpdatedDateTime = HData.UpdatedDateTime,
					Title = HData.Title,
					Code = HData.Code,
					MainCountUnitTitle = HData.MainCountUnitTitle,
					MainCountUnitId = HData.MainCountUnitId,
					SecondaryCountUnitTitle = HData.SecondaryCountUnitTitle,
					SecondaryCountUnitId = HData.SecondaryCountUnitId,
					CategoryTitle = HData.CategoryTitle,
					CategoryId = HData.CategoryId,
					HasSerial = HData.HasSerial,
					PartStoreList = PartStoreList
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

			var dal = new PartDAL<PartListDto>(Connection, Transaction);
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

			var dal = new PartDAL<PartListDto>(Connection, Transaction);
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

			var dal = new PartDAL<PartEntity>(Connection, Transaction);
			try
			{
				var Dto = Entity as HdPartDto ?? throw new Exception(MessageEnum.ثبت_رکورد_با_مشکل_مواجه_شد.EnumToString());
				var Validator = new HdPartValidator();
				var Result = await Validator.ValidateAsync(Dto, op => op.IncludeRuleSets(CrudEnum.Create.ToString()).IncludeRulesNotInRuleSet());
				if (!Result.IsValid)
					return new Return().ReturnValidation(Result.Errors);

				var PartId = await dal.Add(new PartEntity()
				{
					Title = Dto.Title,
					Code = Dto.Code,
					MainCountUnitId = Dto.MainCountUnitId,
					SecondaryCountUnitId = Dto.SecondaryCountUnitId,
					CategoryId = Dto.CategoryId,
					HasSerial = Dto.HasSerial,
					CreatedBy = Claim.UserFullName,
					CreatedDateTime = DateTime.Now.ToShamsiDateTime()
				});

				if (PartId > 0)
				{
					if (Dto.PartStoreList?.Count > 0)
					{
						var PartStoreDal = new DAL.Part.PartStore.PartStoreDAL<Model.Part.PartStore.PartStoreEntity>(Connection, Transaction);
						foreach (var _PartStore in Dto.PartStoreList)
						{
							var PartStoreId = await PartStoreDal.Add(new Model.Part.PartStore.PartStoreEntity()
							{
								PartId = PartId,
								StoreId = _PartStore.StoreId,
								IsActive = _PartStore.IsActive,
								IsDefault = _PartStore.IsDefault,
								Comment = _PartStore.Comment,
								CreatedBy = Claim.UserFullName,
								CreatedDateTime = DateTime.Now.ToShamsiDateTime()
							});

						}
					}

				}
				Transaction.Commit();

				return new Return().ReturnData(PartId, StatusType.ثبت);
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

			var dal = new PartDAL<PartEntity>(Connection, Transaction);
			try
			{
				var Dto = Entity as HdPartDto ?? throw new Exception(MessageEnum.ثبت_رکورد_با_مشکل_مواجه_شد.EnumToString());
				var Validator = new HdPartValidator();
				var Result = await Validator.ValidateAsync(Dto, op => op.IncludeRuleSets(CrudEnum.Update.ToString()).IncludeRulesNotInRuleSet());
				if (!Result.IsValid)
					return new Return().ReturnValidation(Result.Errors);

				var RowCount = await dal.Edit(new PartEntity()
				{
					Id = Dto.Id,
					Title = Dto.Title,
					Code = Dto.Code,
					MainCountUnitId = Dto.MainCountUnitId,
					SecondaryCountUnitId = Dto.SecondaryCountUnitId,
					CategoryId = Dto.CategoryId,
					HasSerial = Dto.HasSerial,

					UpdatedBy = Claim.UserFullName,
					UpdatedDateTime = DateTime.Now.ToShamsiDateTime()

				});

				if (RowCount > 0)
				{
					if (Dto.PartStoreList?.Count > 0)
					{
						var PartStoreDal = new DAL.Part.PartStore.PartStoreDAL<Model.Part.PartStore.PartStoreEntity>(Connection, Transaction);
						foreach (var _PartStore in Dto.PartStoreList)
						{
							if (_PartStore.RowState == RowStateEnum.Added.EnumToInt())
							{
								var PartStoreId = await PartStoreDal.Add(new Model.Part.PartStore.PartStoreEntity()
								{
									PartId = Dto.Id,
									StoreId = _PartStore.StoreId,
									IsActive = _PartStore.IsActive,
									IsDefault = _PartStore.IsDefault,
									Comment = _PartStore.Comment,
									CreatedBy = Claim.UserFullName,
									CreatedDateTime = DateTime.Now.ToShamsiDateTime()
								});

							}
							else if (_PartStore.RowState == RowStateEnum.Modified.EnumToInt())
							{
								RowCount = await PartStoreDal.Edit(new Model.Part.PartStore.PartStoreEntity()
								{
									Id = _PartStore.Id,
									PartId = Dto.Id,
									StoreId = _PartStore.StoreId,
									IsActive = _PartStore.IsActive,
									IsDefault = _PartStore.IsDefault,
									Comment = _PartStore.Comment,
									UpdatedBy = Claim.UserFullName,
									UpdatedDateTime = DateTime.Now.ToShamsiDateTime()
								});

							}
							else if (_PartStore.RowState == RowStateEnum.Deleted.EnumToInt())
							{
								if (RowCount > 0)
								{
									RowCount = await PartStoreDal.Delete(_PartStore.Id);
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

			var dal = new PartDAL<PartEntity>(Connection, Transaction);
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

		public virtual async Task<ResultDto> GetPartCountUnitList(bool? EditMode = null)
		{
			#region GetPartCountUnitList
			using var Connection = new SqlConnection(ConnectionString);
			Connection.Open();
			using var Transaction = Connection.BeginTransaction();

			var dal = new PartDAL<TEntity>(Connection, Transaction);
			try
			{
				var Data = await dal.GetPartCountUnitList(EditMode);
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

		public virtual async Task<ResultDto> GetPartCategoryList(bool? EditMode = null)
		{
			#region GetPartCategoryList
			using var Connection = new SqlConnection(ConnectionString);
			Connection.Open();
			using var Transaction = Connection.BeginTransaction();

			var dal = new PartDAL<TEntity>(Connection, Transaction);
			try
			{
				var Data = await dal.GetPartCategoryList(EditMode);
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

		public virtual async Task<ResultDto> GetPartStoreStoreList(bool? EditMode = null)
		{
			#region GetPartStoreStoreList
			using var Connection = new SqlConnection(ConnectionString);
			Connection.Open();
			using var Transaction = Connection.BeginTransaction();

			var dal = new DAL.Store.Store.StoreDAL<Model.Store.Store.StoreListDto>(Connection, Transaction);
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

	}
}
