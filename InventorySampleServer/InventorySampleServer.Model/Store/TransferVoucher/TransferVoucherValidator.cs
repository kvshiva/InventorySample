using Common.Enum;
using Common.Common;
using FluentValidation;

namespace InventorySampleServer.Model.Store.TransferVoucher
{
	public class TransferVoucherValidator : AbstractValidator<TransferVoucherEntity>
	{
		public TransferVoucherValidator()
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

			RuleFor(e => e.SourceStoreId).NotEmpty().WithMessage(ValidationMessage.NotEmptyErrorMessage("شناسه انبار مبدا"))
				.Must(e => e.IsDigit()).WithMessage(ValidationMessage.IsDigitErrorMessage());

			RuleFor(e => e.TargetStoreId).NotEmpty().WithMessage(ValidationMessage.NotEmptyErrorMessage("شناسه انبار مقصد"))
				.Must(e => e.IsDigit()).WithMessage(ValidationMessage.IsDigitErrorMessage());

			RuleFor(e => e.SourceInventoryVoucherId)
				.Must(e => e.IsDigit()).WithMessage(ValidationMessage.IsDigitErrorMessage())
				.When(e => e.SourceInventoryVoucherId != null);

			RuleFor(e => e.TargetInventoryVoucherId)
				.Must(e => e.IsDigit()).WithMessage(ValidationMessage.IsDigitErrorMessage())
				.When(e => e.TargetInventoryVoucherId != null);

			RuleFor(e => e.InventoryVoucherSpecificationId).NotEmpty().WithMessage(ValidationMessage.NotEmptyErrorMessage("شناسه الگوی سند"))
				.Must(e => e.IsDigit()).WithMessage(ValidationMessage.IsDigitErrorMessage());

			RuleFor(e => e.TransferVoucherNo).NotEmpty().WithMessage(ValidationMessage.NotEmptyErrorMessage("شماره سند"))
				.MaximumLength(20).WithMessage(ValidationMessage.IsValidMaxLengthErrorMessage("شماره سند", 20));

			RuleFor(e => e.Comment)
				.MaximumLength(1000).WithMessage(ValidationMessage.IsValidMaxLengthErrorMessage("توضیح", 1000))
				.When(e => e.Comment != null);

			RuleFor(e => e.JsonField)
				.MaximumLength(4000).WithMessage(ValidationMessage.IsValidMaxLengthErrorMessage("داده های جیسون", 4000))
				.When(e => e.JsonField != null);

			RuleFor(e => e.UserId).NotEmpty().WithMessage(ValidationMessage.NotEmptyErrorMessage("شناسه کاربر"))
				.Must(e => e.IsDigit()).WithMessage(ValidationMessage.IsDigitErrorMessage());

			RuleFor(e => e.StateEnumId)
				.Must(e => e.IsDigit()).WithMessage(ValidationMessage.IsDigitErrorMessage())
				.When(e => e.StateEnumId != null);

		}
	}
}
