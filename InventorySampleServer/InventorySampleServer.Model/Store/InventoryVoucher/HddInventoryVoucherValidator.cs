using Common.Enum;
using Common.Common;
using FluentValidation;

namespace InventorySampleServer.Model.Store.InventoryVoucher
{
	public class HddInventoryVoucherValidator : AbstractValidator<HddInventoryVoucherDto>
	{
		public HddInventoryVoucherValidator()
		{
			RuleSet(CrudEnum.Create.ToString(), () => { });

			RuleSet(CrudEnum.Read.ToString(), () => { });

			RuleSet(CrudEnum.Delete.ToString(), () => { });

			RuleSet(CrudEnum.Update.ToString(), () =>
			{
				RuleFor(e => e.Id).NotEmpty().WithMessage(ValidationMessage.NotEmptyErrorMessage("شناسه"))
					.NotEqual(0).WithMessage(ValidationMessage.NotZeroErrorMessage("شناسه"))
					.Must(e => e.IsDigit()).WithMessage(ValidationMessage.IsDigitErrorMessage());
			});

			//RuleFor(e => e.InventoryVoucherNo).NotEmpty().WithMessage(ValidationMessage.NotEmptyErrorMessage("شماره سند"))
			//	.MaximumLength(20).WithMessage(ValidationMessage.IsValidMaxLengthErrorMessage("شماره سند", 20));

			RuleFor(e => e.PersianDate).NotEmpty().WithMessage(ValidationMessage.NotEmptyErrorMessage("تاریخ شمسی"))
				.MaximumLength(10).WithMessage(ValidationMessage.IsValidPesianDateErrorMessage());

			//RuleFor(e => e.Time).NotEmpty().WithMessage(ValidationMessage.NotEmptyErrorMessage("زمان"))
			//	.MaximumLength(5).WithMessage(ValidationMessage.IsValidMaxLengthErrorMessage("زمان", 5));

			RuleFor(e => e.Comment)
				.MaximumLength(1000).WithMessage(ValidationMessage.IsValidMaxLengthErrorMessage("توضیح", 1000))
				.When(e => e.Comment != null);

			RuleFor(e => e.SystemComment)
				.MaximumLength(2000).WithMessage(ValidationMessage.IsValidMaxLengthErrorMessage("توضیح سیستم (مبنای انتقال:براساس انتقال شماره xx در تاریخ xxx)", 2000))
				.When(e => e.SystemComment != null);

			RuleFor(e => e.StoreId).NotEmpty().WithMessage(ValidationMessage.NotEmptyErrorMessage("شناسه انبار"))
				.Must(e => e.IsDigit()).WithMessage(ValidationMessage.IsDigitErrorMessage());

			RuleFor(e => e.InventoryVoucherSpecificationId).NotEmpty().WithMessage(ValidationMessage.NotEmptyErrorMessage("شناسه الگوی سند"))
				.Must(e => e.IsDigit()).WithMessage(ValidationMessage.IsDigitErrorMessage());

			RuleFor(e => e.UserId).NotEmpty().WithMessage(ValidationMessage.NotEmptyErrorMessage("شناسه کاربر"))
				.Must(e => e.IsDigit()).WithMessage(ValidationMessage.IsDigitErrorMessage());

			RuleFor(e => e.JsonField)
				.MaximumLength(4000).WithMessage(ValidationMessage.IsValidMaxLengthErrorMessage("دیتای جیسون", 4000))
				.When(e => e.JsonField != null);

			RuleFor(e => e.BaseEntity)
				.Must(e => e.IsDigit()).WithMessage(ValidationMessage.IsDigitErrorMessage())
				.When(e => e.BaseEntity != null);

			RuleFor(e => e.BaseEntityRef)
				.Must(e => e.IsDigit()).WithMessage(ValidationMessage.IsDigitErrorMessage())
				.When(e => e.BaseEntityRef != null);

			RuleFor(e => e.StateEnumId)
				.Must(e => e.IsDigit()).WithMessage(ValidationMessage.IsDigitErrorMessage())
				.When(e => e.StateEnumId != null);

			//RuleFor(e => e.InventoryVoucherItemList).NotNull().Must(e => e?.Count > 0).WithMessage(ValidationMessage.NotEmptyListErrorMessage("کالاهای سند انبار"));
			RuleForEach(e => e.InventoryVoucherItemList).SetValidator(new Model.Store.InventoryVoucherItem.HddInventoryVoucherItemListValidator());
		}
	}
}
