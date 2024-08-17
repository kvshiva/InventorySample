using Common.Enum;
using Common.Common;
using FluentValidation;

namespace InventorySampleServer.Model.Store.StoreTypeEnum
{
	public class StoreTypeEnumValidator : AbstractValidator<StoreTypeEnumEntity>
	{
		public StoreTypeEnumValidator()
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
				.MaximumLength(100).WithMessage(ValidationMessage.IsValidMaxLengthErrorMessage("عنوان", 100));

		}
	}
}
