namespace Infrastructure.Common
{
    public static class Common
    {
        public static int OffSet(int PageNumber, int PageSize)
        {
            #region Offset
            return (PageNumber - 1) * PageSize;
            #endregion
        }
    }
}
