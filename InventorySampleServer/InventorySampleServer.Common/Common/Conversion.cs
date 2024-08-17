using Common.Enum;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.Common
{
    public static class Conversion
    {
        public async static Task<string> GetServerShamsiDate()
        {
            #region GetServerShamsiDate
            var MiladiDateTime = DateTime.Now;
            var persianCalender = new PersianCalendar();
            var year = persianCalender.GetYear(MiladiDateTime);
            var month = persianCalender.GetMonth(MiladiDateTime).ToString();
            month = month.PadLeft(2, '0');
            var day = persianCalender.GetDayOfMonth(MiladiDateTime).ToString();
            day = day.PadLeft(2, '0');

            //return $"{year}/{month}/{day}";

            return await Task.Run(() => { return $"{year}/{month}/{day}"; });

            #endregion
        }

        public async static Task<string> GetServerDate()
        {
            #region GetServerDate  
            return await Task.Run(() => { return DateTime.Now.ToString("yyyy/MM/dd"); });
            #endregion
        }

        public async static Task<string> GetServerShamsiDateTime()
        {
            #region GetServerShamsiDateTime
            var Date = await GetServerShamsiDate();
            var Time = await GetServerLongTime();

            return await Task.Run(() => { return $"{Date} {Time}"; });
            #endregion
        }

        public async static Task<string> GetServerDateTime()
        {
            #region GetServerDateTime  
            return await Task.Run(() => { return DateTime.Now.ToString(); });
            #endregion
        }

        public async static Task<string> GetServerTime()
        {
            #region GetServerTime
            var DT = DateTime.Now;
            return await Task.Run(() => { return $"{DT.Hour.ToString().PadLeft(2, '0')}:{DT.Minute.ToString().PadLeft(2, '0')}"; });
            #endregion
        }

        public async static Task<string> GetServerLongTime()
        {
            #region Server Long Time
            var DT = DateTime.Now;
            return await Task.Run(() => { return $"{DT.Hour.ToString().PadLeft(2, '0')}:{DT.Minute.ToString().PadLeft(2, '0')}:{DT.Second.ToString().PadLeft(2, '0')}"; });
            #endregion
        }

        public static DateTime ConvertToMiladi(string PersianDate)
        {
            #region ConvertToMiladi            
            var dateTime = DateTime.Parse(PersianDate, new CultureInfo("fa-IR")); // Convert to Miladi            
            return dateTime.ToUniversalTime(); // Return Utc Date
            #endregion
        }

        public static string ConvertMinuteToHour(int Minute)
        {
            #region ConvertMinuteToHour
            var timeSpan = TimeSpan.FromMinutes(Minute);
            return ((int)timeSpan.TotalHours).ToString().PadLeft(2, '0') + ":" + timeSpan.Minutes.ToString().PadLeft(2, '0');
            #endregion
        }

        public static int ConvertHourToMinute(string Hour)
        {
            #region ConvertHourToMinute
            return (int)TimeSpan.Parse(Hour).TotalMinutes;
            #endregion
        }

        public static string ConcateHourAndMinute(int Hour, int Minute)
        {
            #region ConcateHourAndMinute
            return $"{Hour.ToString().PadLeft(2, '0')}:{Minute.ToString().PadLeft(2, '0')}";
            #endregion
        }

        public static string GetPersianMonthName(string Date)
        {
            #region Persian Month Name
            var part = Date.Split("/");
            var Month = string.Empty;
            switch (part[1])
            {
                case "01":
                    Month = $"{part[2]} {MonthEnum.فررودين.EnumToString()}";
                    break;
                case "02":
                    Month = $"{part[2]} {MonthEnum.ارديبهشت.EnumToString()}";
                    break;
                case "03":
                    Month = $"{part[2]} {MonthEnum.خرداد.EnumToString()}";
                    break;
                case "04":
                    Month = $"{part[2]} {MonthEnum.تیر.EnumToString()}";
                    break;
                case "05":
                    Month = $"{part[2]} {MonthEnum.مرداد.EnumToString()}";
                    break;
                case "06":
                    Month = $"{part[2]} {MonthEnum.شهریور.EnumToString()}";
                    break;
                case "07":
                    Month = $"{part[2]} {MonthEnum.مهر.EnumToString()}";
                    break;
                case "08":
                    Month = $"{part[2]} {MonthEnum.آبان.EnumToString()}";
                    break;
                case "09":
                    Month = $"{part[2]} {MonthEnum.آذر.EnumToString()}";
                    break;
                case "10":
                    Month = $"{part[2]} {MonthEnum.دی.EnumToString()}";
                    break;
                case "11":
                    Month = $"{part[2]} {MonthEnum.بهمن.EnumToString()}";
                    break;
                case "12":
                    Month = $"{part[2]} {MonthEnum.اسفند.EnumToString()}";
                    break;
                default:
                    break;
            }

            return Month;
            #endregion
        }
    }
}
