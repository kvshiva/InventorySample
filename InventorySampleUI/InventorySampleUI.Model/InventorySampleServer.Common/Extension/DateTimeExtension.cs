using System.Globalization;

namespace Common
{
    public static class DateTimeExtension
    {
        public static string ToShamsiDate(this DateTime Value)
        {
            #region Shamsi Date
            var persianCalender = new PersianCalendar();
            var year = persianCalender.GetYear(Value);
            var month = persianCalender.GetMonth(Value).ToString();
            month = month.PadLeft(2, '0');
            var day = persianCalender.GetDayOfMonth(Value).ToString();
            day = day.PadLeft(2, '0');

            return $"{year}/{month}/{day}";
            #endregion
        }

        public static string ToShamsiDateTime(this DateTime Value)
        {
            #region Shamsi Date Time
            var Date = ToShamsiDate(Value);
            var Time = ToShamsiLongTime(Value);

            return $"{Date} {Time}";
            #endregion
        }

        public static string ToShamsiTime(this DateTime Value)
        {
            #region Shamsi Time
            return $"{Value.Hour.ToString().PadLeft(2, '0')}:{Value.Minute.ToString().PadLeft(2, '0')}";
            #endregion
        }

        public static string ToShamsiLongTime(this DateTime Value)
        {
            #region Shamsi Long Time
            return $"{Value.Hour.ToString().PadLeft(2, '0')}:{Value.Minute.ToString().PadLeft(2, '0')}:{Value.Second.ToString().PadLeft(2, '0')}";
            #endregion
        }

        public static string IntToTimeFormat(this int Value)
        {
            #region Int To Time Format
            var time = new TimeSpan(Value / 60, Value % 60, 0);

            return $"{time.Hours.ToString().PadLeft(2, '0')}:{time.Minutes.ToString().PadLeft(2, '0')}";
            #endregion
        }
    }
}
