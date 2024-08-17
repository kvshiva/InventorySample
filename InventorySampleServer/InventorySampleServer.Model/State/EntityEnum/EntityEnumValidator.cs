using Common.Enum;
using Common.Common;
using FluentValidation;

namespace InventorySampleServer.Model.State.EntityEnum
{
	public class EntityEnumValidator : AbstractValidator<EntityEnumEntity>
	{
		public EntityEnumValidator()
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

			RuleFor(e => e.Title).NotEmpty().WithMessage(ValidationMessage.NotEmptyErrorMessage("عنوان فرم"))
				.MaximumLength(50).WithMessage(ValidationMessage.IsValidMaxLengthErrorMessage("عنوان فرم", 50));

			RuleFor(e => e.EntitySchema)
				.MaximumLength(20).WithMessage(ValidationMessage.IsValidMaxLengthErrorMessage("نام شمای فرم", 20))
				.When(e => e.EntitySchema != null);

			RuleFor(e => e.Prefix)
				.MaximumLength(10).WithMessage(ValidationMessage.IsValidMaxLengthErrorMessage("پیشوند", 10))
				.When(e => e.Prefix != null);

			RuleFor(e => e.CounterLength)
				.Must(e => e.IsDigit()).WithMessage(ValidationMessage.IsDigitErrorMessage())
				.When(e => e.CounterLength != null);

		}
	}
}
