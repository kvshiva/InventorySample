namespace Common.Common
{
    public static class StringOperators
    {
        public static string Concatination(string Symbol, params string[] Args)
        {
            #region Concatination
            var Result = string.Empty;
            foreach (var param in Args)
            {
                if (!string.IsNullOrEmpty(param))
                    Result += param + Symbol;
            }
            return Result;
            //TODO: need to complete
            #endregion
        }
    }
}
