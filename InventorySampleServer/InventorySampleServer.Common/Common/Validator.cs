using System.Globalization;

namespace Common.Common
{
    public static class Validator
    {
        public static bool IsValidMobile(this string Input)
        {
            #region Validate Mobile
            if (!string.IsNullOrEmpty(Input) && Input.Length == 11)
            {
                if (Input.IsDigit())
                {
                    if (Input.StartsWith("09"))
                    {
                        return true;
                    }
                }
            }
            return false;
            #endregion
        }

        public static bool IsValidNationalCode(this string Input)
        {
            #region Validate National Code
            if (!string.IsNullOrEmpty(Input) && Input.Length == 10)
            {
                var Sum = 0;
                int[] Digits = new int[10];

                for (var i = 0; i < 10; i++)
                {
                    Digits[i] = (int)char.GetNumericValue(Input[i]);
                    Sum += Digits[i] * (10 - i);
                }

                Sum -= Digits[9];
                int r = Sum % 11;

                if (r <= 2)
                {
                    if (r == Digits[9])
                        return true;
                }
                else // if (r > 2)
                {
                    r = 11 - r;
                    if (r == Digits[9])
                        return true;
                }
            }

            return false;
            #endregion
        }

        public static bool IsDigit(this string Input)
        {
            #region Is Digit
            char[] Digits = new char[10] { '0', '1', '2', '3', '4', '5', '6', '7', '8', '9' };

            foreach (char ch in Input)
            {
                if (!Digits.Contains(ch))
                    return false;
            }
            return true;
            #endregion
        }

        public static bool IsDigit(this int Input)
        {
            #region Is Digit
            char[] Digits = new char[10] { '0', '1', '2', '3', '4', '5', '6', '7', '8', '9' };
            string StringInput = Input.ToString() ?? "";
            foreach (char ch in StringInput)
            {
                if (!Digits.Contains(ch))
                    return false;
            }
            return true;
            #endregion
        }

        public static bool IsDigit(this int? Input)
        {
            #region Is Digit
            char[] Digits = new char[10] { '0', '1', '2', '3', '4', '5', '6', '7', '8', '9' };
            string StringInput = Input.ToString() ?? "";
            foreach (char ch in StringInput)
            {
                if (!Digits.Contains(ch))
                    return false;
            }
            return true;
            #endregion
        }

        public static bool IsDigit(this long Input)
        {
            #region Is Digit
            char[] Digits = new char[10] { '0', '1', '2', '3', '4', '5', '6', '7', '8', '9' };
            string StringInput = Input.ToString() ?? "";
            foreach (char ch in StringInput)
            {
                if (!Digits.Contains(ch))
                    return false;
            }
            return true;
            #endregion
        }

        public static bool IsDigit(this long? Input)
        {
            #region Is Digit
            char[] Digits = new char[10] { '0', '1', '2', '3', '4', '5', '6', '7', '8', '9' };
            string StringInput = Input.ToString() ?? "";
            foreach (char ch in StringInput)
            {
                if (!Digits.Contains(ch))
                    return false;
            }
            return true;
            #endregion
        }

        public static bool IsDigit(this decimal Input)
        {
            #region Is Digit
            char[] Digits = new char[11] { '0', '1', '2', '3', '4', '5', '6', '7', '8', '9', '.' };
            string StringInput = Input.ToString() ?? "";
            foreach (char ch in StringInput)
            {
                if (!Digits.Contains(ch))
                    return false;
            }
            return true;
            #endregion
        }

        public static bool IsDigit(this decimal? Input)
        {
            #region Is Digit
            char[] Digits = new char[11] { '0', '1', '2', '3', '4', '5', '6', '7', '8', '9', '.' };
            string StringInput = Input.ToString() ?? "";
            foreach (char ch in StringInput)
            {
                if (!Digits.Contains(ch))
                    return false;
            }
            return true;
            #endregion
        }

        public static bool IsValidPesianDate(this string Input)
        {
            #region Validate Pesian Date
            if (!string.IsNullOrEmpty(Input) && Input.Length == 10)
            {
                if (Input[4] == '/' && Input[7] == '/')
                {
                    int Year = Convert.ToInt32(Input.Substring(0, 4));
                    int Month = Convert.ToInt32(Input.Substring(5, 2));
                    int Day = Convert.ToInt32(Input.Substring(8, 2));

                    if (Year > 1300 && Year < 1500)
                    {
                        if (Month >= 1 && Month <= 6)
                        {
                            if (Day >= 1 && Day <= 31)
                                return true;
                        }
                        else if (Month >= 7 && Month <= 11)
                        {
                            if (Day >= 1 && Day <= 30)
                                return true;
                        }
                        else if (Month == 12)
                        {
                            if (Day >= 1 && Day <= 29)
                                return true;

                            if (Day == 30)
                            {
                                var persiancal = new PersianCalendar();
                                if (persiancal.IsLeapYear(Year))
                                    return true;
                            }
                        }
                    }
                }
            }
            return false;
            #endregion
        }
        public static bool IsValidTime(this string Input)
        {
            #region Validate Time 
            if (!string.IsNullOrEmpty(Input) && Input.Length == 5)
            {
                if (Input[2] == ':')
                {
                    int Hour = int.Parse(Input.Substring(0, 2));
                    int Minute = int.Parse(Input.Substring(3, 2));
                    if (Hour > 0 && Hour < 24 && Minute > 0 && Minute < 60) 
                        return true;
                }
            }
            return false;
            #endregion
        }
    }
}
