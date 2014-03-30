namespace ProgressBar.Helper
{
    public static class TagNameHelper
    {
        public const string MainTagKey = "5XQ8HZCIiAVwnvP7QDECuRd1ygcAHb";

        internal static string MakeKey(string key)
        {
            return string.Format("{0}{0}", MainTagKey, key);
        }
    }
}