using Common.Enum;
using Common.Common;
using FluentValidation;

namespace InventorySampleServer.Model.General.User
{
	public class UserValidator : AbstractValidator<UserEntity>
	{
		public UserValidator()
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

			RuleFor(e => e.FullName).NotEmpty().WithMessage(ValidationMessage.NotEmptyErrorMessage("نام کامل"))
				.MaximumLength(400).WithMessage(ValidationMessage.IsValidMaxLengthErrorMessage("نام کامل", 400));

			RuleFor(e => e.Code).NotEmpty().WithMessage(ValidationMessage.NotEmptyErrorMessage("کد"))
				.Must(e => e.IsDigit()).WithMessage(ValidationMessage.IsDigitErrorMessage());

			RuleFor(e => e.Mobile).NotEmpty().WithMessage(ValidationMessage.NotEmptyErrorMessage("موبایل"))
				.MaximumLength(16).WithMessage(ValidationMessage.IsValidMaxLengthErrorMessage("موبایل", 16));

		}
	}
}
