namespace Common
{
    public static class EnumExtension
    {
        public static string EnumToString(this System.Enum Value)
        {
            #region EnumToString
            return Value.ToString().Replace("_", " ");
            #endregion
        }

        public static int EnumToInt(this object input)
        {
            #region EnumToInt
            return (int)input;
            #endregion
        }

    }
}
