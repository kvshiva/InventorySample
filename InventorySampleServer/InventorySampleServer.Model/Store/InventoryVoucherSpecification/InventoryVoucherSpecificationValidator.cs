using Common.Enum;
using Common.Common;
using FluentValidation;

namespace InventorySampleServer.Model.Store.InventoryVoucherSpecification
{
	public class InventoryVoucherSpecificationValidator : AbstractValidator<InventoryVoucherSpecificationEntity>
	{
		public InventoryVoucherSpecificationValidator()
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

			RuleFor(e => e.Title).NotEmpty().WithMessage(ValidationMessage.NotEmptyErrorMessage("عنوان"))
				.MaximumLength(200).WithMessage(ValidationMessage.IsValidMaxLengthErrorMessage("عنوان", 200));

			RuleFor(e => e.Comment)
				.MaximumLength(1000).WithMessage(ValidationMessage.IsValidMaxLengthErrorMessage("توضیح", 1000))
				.When(e => e.Comment != null);

			RuleFor(e => e.InventoryVoucherSpecificationTypeEnumId).NotEmpty().WithMessage(ValidationMessage.NotEmptyErrorMessage("شناسه نوع الگو"))
				.Must(e => e.IsDigit()).WithMessage(ValidationMessage.IsDigitErrorMessage());

			RuleFor(e => e.ReceiptInventoryVoucherSpecificationId)
				.Must(e => e.IsDigit()).WithMessage(ValidationMessage.IsDigitErrorMessage())
				.When(e => e.ReceiptInventoryVoucherSpecificationId != null);

			RuleFor(e => e.RemittanceInventoryVoucherSpecificationId)
				.Must(e => e.IsDigit()).WithMessage(ValidationMessage.IsDigitErrorMessage())
				.When(e => e.RemittanceInventoryVoucherSpecificationId != null);

			RuleFor(e => e.Jsonfield)
				.MaximumLength(4000).WithMessage(ValidationMessage.IsValidMaxLengthErrorMessage("دیتای جیسون", 4000))
				.When(e => e.Jsonfield != null);

		}
	}
}
