using Common.Enum;
using Common.Common;
using FluentValidation;

namespace InventorySampleServer.Model.Store.InventoryVoucherItemSerial
{
	public class InventoryVoucherItemSerialValidator : AbstractValidator<InventoryVoucherItemSerialEntity>
	{
		public InventoryVoucherItemSerialValidator()
		{
			RuleSet(CrudEnum.Create.ToString(), () => { });

			RuleSet(CrudEnum.Read.ToString(), () => { });

			RuleSet(CrudEnum.Delete.ToString(), () => { });

			RuleSet(CrudEnum.Update.ToString(), () =>
			{
				//RuleFor(e => e.Id).NotEmpty().WithMessage(ValidationMessage.NotEmptyErrorMessage("شناسه"))
				//	.NotEqual(0).WithMessage(ValidationMessage.NotZeroErrorMessage("شناسه"))
				//	.Must(e => e.IsDigit()).WithMessage(ValidationMessage.IsDigitErrorMessage());
			});

			RuleFor(e => e.InventoryVoucherItemId).NotEmpty().WithMessage(ValidationMessage.NotEmptyErrorMessage("شناسه کالای سند انبار"))
				.Must(e => e.IsDigit()).WithMessage(ValidationMessage.IsDigitErrorMessage());

			RuleFor(e => e.SerialNo).NotEmpty().WithMessage(ValidationMessage.NotEmptyErrorMessage("سریال"))
				.MaximumLength(1000).WithMessage(ValidationMessage.IsValidMaxLengthErrorMessage("سریال", 1000));

			RuleFor(e => e.Value1).NotEmpty().WithMessage(ValidationMessage.NotEmptyErrorMessage("مقدار اول"))
				.Must(e => e.IsDigit()).WithMessage(ValidationMessage.IsDigitErrorMessage());

			RuleFor(e => e.Value2)
				.Must(e => e.IsDigit()).WithMessage(ValidationMessage.IsDigitErrorMessage())
				.When(e => e.Value2 != null);

			RuleFor(e => e.Comment)
				.MaximumLength(1000).WithMessage(ValidationMessage.IsValidMaxLengthErrorMessage("توضیح", 1000))
				.When(e => e.Comment != null);

			RuleFor(e => e.SystemComment)
				.MaximumLength(4000).WithMessage(ValidationMessage.IsValidMaxLengthErrorMessage("توضیح سیستمی", 4000))
				.When(e => e.SystemComment != null);

			RuleFor(e => e.JsonField)
				.MaximumLength(4000).WithMessage(ValidationMessage.IsValidMaxLengthErrorMessage("داده های جیسون", 4000))
				.When(e => e.JsonField != null);

		}
	}
}
