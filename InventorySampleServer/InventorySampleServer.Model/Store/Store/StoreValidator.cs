using Common.Enum;
using Common.Common;
using FluentValidation;

namespace InventorySampleServer.Model.Store.Store
{
	public class StoreValidator : AbstractValidator<StoreEntity>
	{
		public StoreValidator()
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

			RuleFor(e => e.Code).NotEmpty().WithMessage(ValidationMessage.NotEmptyErrorMessage("کد"))
				.MaximumLength(200).WithMessage(ValidationMessage.IsValidMaxLengthErrorMessage("کد", 200));

			RuleFor(e => e.Comment)
				.MaximumLength(1000).WithMessage(ValidationMessage.IsValidMaxLengthErrorMessage("توضیحات", 1000))
				.When(e => e.Comment != null);

			RuleFor(e => e.Jsonfield)
				.MaximumLength(4000).WithMessage(ValidationMessage.IsValidMaxLengthErrorMessage("داده های جیسون", 4000))
				.When(e => e.Jsonfield != null);

			RuleFor(e => e.StoreTypeEnumId).NotEmpty().WithMessage(ValidationMessage.NotEmptyErrorMessage("شناسه گروه انبار"))
				.Must(e => e.IsDigit()).WithMessage(ValidationMessage.IsDigitErrorMessage());

		}
	}
}
