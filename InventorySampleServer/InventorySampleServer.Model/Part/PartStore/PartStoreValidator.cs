using Common.Enum;
using Common.Common;
using FluentValidation;

namespace InventorySampleServer.Model.Part.PartStore
{
	public class PartStoreValidator : AbstractValidator<PartStoreEntity>
	{
		public PartStoreValidator()
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

			RuleFor(e => e.PartId).NotEmpty().WithMessage(ValidationMessage.NotEmptyErrorMessage("شناسه کالا"))
				.Must(e => e.IsDigit()).WithMessage(ValidationMessage.IsDigitErrorMessage());

			RuleFor(e => e.StoreId)
				.Must(e => e.IsDigit()).WithMessage(ValidationMessage.IsDigitErrorMessage())
				.When(e => e.StoreId != null);

			RuleFor(e => e.Comment)
				.MaximumLength(1000).WithMessage(ValidationMessage.IsValidMaxLengthErrorMessage("توضیح", 1000))
				.When(e => e.Comment != null);

		}
	}
}
