using Common.Enum;
using Common.Common;
using FluentValidation;

namespace InventorySampleServer.Model.Store.InventoryVoucherSpecificationTypeEnum
{
	public class InventoryVoucherSpecificationTypeEnumValidator : AbstractValidator<InventoryVoucherSpecificationTypeEnumEntity>
	{
		public InventoryVoucherSpecificationTypeEnumValidator()
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
				.MaximumLength(50).WithMessage(ValidationMessage.IsValidMaxLengthErrorMessage("عنوان", 50));

			RuleFor(e => e.Ratio)
				.Must(e => e.IsDigit()).WithMessage(ValidationMessage.IsDigitErrorMessage())
				.When(e => e.Ratio != null);

		}
	}
}
