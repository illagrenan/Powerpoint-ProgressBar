namespace ProgressBar.Helper
{
    public static class TagNameHelper
    {
        public const string MainTagKey = "5XQ8HZCIiAVwnvP7QDECuRd1ygcAHb";
        private const string InternalContainerKey = "container";
        private static string _containerKeyCache;

        public static string ContainerKey
        {
            get { return _containerKeyCache ?? (_containerKeyCache = MakeKey(InternalContainerKey)); }
        }

        private static string MakeKey(string key)
        {
            return string.Format("{0}_{0}", MainTagKey, key);
        }
    }
}