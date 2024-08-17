using Common.Enum;
using Common.Common;
using FluentValidation;

namespace InventorySampleServer.Model.Part.Part
{
	public class PartValidator : AbstractValidator<PartEntity>
	{
		public PartValidator()
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

			RuleFor(e => e.Code).NotEmpty().WithMessage(ValidationMessage.NotEmptyErrorMessage("کد همکاران سیستم"))
				.MaximumLength(200).WithMessage(ValidationMessage.IsValidMaxLengthErrorMessage("کد همکاران سیستم", 200));

			RuleFor(e => e.MainCountUnitId).NotEmpty().WithMessage(ValidationMessage.NotEmptyErrorMessage("شناسه واحد شمارش اصلی"))
				.Must(e => e.IsDigit()).WithMessage(ValidationMessage.IsDigitErrorMessage());

			RuleFor(e => e.SecondaryCountUnitId)
				.Must(e => e.IsDigit()).WithMessage(ValidationMessage.IsDigitErrorMessage())
				.When(e => e.SecondaryCountUnitId != null);

			RuleFor(e => e.CategoryId).NotEmpty().WithMessage(ValidationMessage.NotEmptyErrorMessage("شناسه دسته بندی"))
				.Must(e => e.IsDigit()).WithMessage(ValidationMessage.IsDigitErrorMessage());

		}
	}
}
