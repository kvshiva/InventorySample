using Common;
using Common.Enum;
using FluentValidation;
using Microsoft.Data.SqlClient;
using Microsoft.AspNetCore.Http;
using InventorySampleServer.BLL._Base;
using Model.Custom.Other;
using InventorySampleServer.Model.Part.Category;
using InventorySampleServer.DAL.Part.Category;

namespace InventorySampleServer.BLL._Gen.Part
{
	public class GCategoryBLL<TEntity> : BaseBLL<TEntity> where TEntity : class
	{
		public GCategoryBLL(string ConnectionString, GClaim Claim) : base(ConnectionString, Claim) { }

		public override async Task<ResultDto> GetById(int Id)
		{
			#region GetById
			using var Connection = new SqlConnection(ConnectionString);
			Connection.Open();
			using var Transaction = Connection.BeginTransaction();

			try
			{
				var dal = new CategoryDAL<CategoryListDto>(Connection, Transaction);
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

			var dal = new CategoryDAL<CategoryListDto>(Connection, Transaction);
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

			var dal = new CategoryDAL<CategoryListDto>(Connection, Transaction);
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

			var dal = new CategoryDAL<CategoryEntity>(Connection, Transaction);
			try
			{
				var Dto = Entity as CategoryEntity ?? throw new Exception(MessageEnum.ثبت_رکورد_با_مشکل_مواجه_شد.EnumToString());
				var Validator = new CategoryValidator();
				var Result = await Validator.ValidateAsync(Dto, op => op.IncludeRuleSets(CrudEnum.Create.ToString()).IncludeRulesNotInRuleSet());
				if (!Result.IsValid)
					return new Return().ReturnValidation(Result.Errors);


				Dto.CreatedBy = Claim.UserFullName;
				Dto.CreatedDateTime = DateTime.Now.ToShamsiDateTime();

				var CategoryId = await dal.Add(Dto);
				Transaction.Commit();

				return new Return().ReturnData(CategoryId, StatusType.ثبت);
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

			var dal = new CategoryDAL<CategoryEntity>(Connection, Transaction);
			try
			{
				var Dto = Entity as CategoryEntity ?? throw new Exception(MessageEnum.ثبت_رکورد_با_مشکل_مواجه_شد.EnumToString());
				var Validator = new CategoryValidator();
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

			var dal = new CategoryDAL<CategoryEntity>(Connection, Transaction);
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

	}
}
