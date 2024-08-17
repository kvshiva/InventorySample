namespace Common.Common
{
    public static class Calculator
    {
        public static int PageCount(int RowCount, int PageSize)
        {
            #region PageCount
            var PageCount = 0;
            if (RowCount > 0)
            {
                if (RowCount % PageSize == 0)
                    PageCount = RowCount / PageSize;
                else
                    PageCount = RowCount / PageSize + 1;
            }
            return PageCount;
            #endregion
        }

        public static int CalculateDurationInHours(string StartDate, string StartTime, string EndDate, string EndTime)
        {
            #region Calculate Duration In Hours

            var StartDateTime = DateTime.Parse(StartDate + " " + StartTime);
            var EndDateTime = DateTime.Parse(EndDate + " " + EndTime);
            TimeSpan ts = EndDateTime - StartDateTime;

            //int differenceInDays = ts.Days; 
            //double differenceInDays = ts.TotalDays;

            //int differenceInHours = ts.Hours; 
            //double differenceInHours = ts.TotalHours; 

            //int differenceInMinutes = ts.Minutes;
            //double differenceInMinutes = ts.TotalMinutes; 

            return ts.Hours; 
            #endregion
        }
    }
}
