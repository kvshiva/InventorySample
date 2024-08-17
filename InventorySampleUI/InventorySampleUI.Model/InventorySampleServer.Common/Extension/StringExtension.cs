using System.Text.RegularExpressions;

namespace Common
{
    public static class StringExtension
    {
        public static string ToFormatedText(this System.Enum Value, params string[] args)
        {
            #region To Formated Text
            var Result = Value.ToString();
            foreach (var number in Regex.Split(Result, @"\D+"))
            {
                if (!string.IsNullOrEmpty(number.ToString()))
                {
                    Result = Result.Replace(Regex.Match(Result, @"_\d{1,}_").ToString(), "_{" + number + "}_");
                }
            }

            return string.Format(Result.Replace("_", " "), args);
            #endregion
        }

        public static string AddCamma(this string Value)
        {
            #region Add Camma
            return Value + ",";
            #endregion
        }
    }
}
