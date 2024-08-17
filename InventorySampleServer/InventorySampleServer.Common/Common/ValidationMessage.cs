namespace Common.Common
{
    public static class ValidationMessage
    {
        public static string NotEmptyErrorMessage(string fieldName)
        {
            return $"{fieldName} را وارد نمایید";
        }

        public static string NotEmptyListErrorMessage(string listName)
        {
            return $"لیست {listName} نمی تواند خالی باشد";
        }

        public static string NotZeroErrorMessage(string fieldName)
        {
            return $"{fieldName} نمیتواند برابر صفر باشد";
        }

        public static string IsValidMaxLengthErrorMessage(string fieldName, int max)
        {
            return $"تعداد کاراکترهای {fieldName} نمیتواند بیشتر از {max} کاراکتر باشد";
        }

        public static string IsValidMinLengthErrorMessage(string fieldName, int min)
        {
            return $"تعداد کاراکترهای {min} نمیتواند کمتر از {fieldName} کاراکتر باشد";
        }

        public static string IsValidMobileErrorMessage()
        {
            return "شماره موبایل صحیح نیست";
        }

        public static string IsValidNationalCodeErrorMessage()
        {
            return "کد ملی نامعتبر است";
        }
        public static string IsDigitErrorMessage()
        {
            return "فقط ورودی عدد مجاز است";
        }

        public static string IsDigitErrorMessage(string fieldName)
        {
            return $"فیلد {fieldName} باید یک عدد باشد";
        }

        public static string IsValidPesianDateErrorMessage()
        {
            return "فرمت تاریخ شمسی درست نیست";
        }

        public static string IsValidTimeErrorMessage()
        {
            return "فرمت ساعت درست نیست";
        }

        public static string IsEmailAddressErrorMessage()
        {
            return "پست الکترونیکی وارد شده معتبر نمی باشد";
        }
    }
}
