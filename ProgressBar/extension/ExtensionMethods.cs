#region

using System;

#endregion

namespace ProgressBar.extension
{
    /// <summary>
    ///     References:
    ///     [0] http://stackoverflow.com/a/444818/752142
    ///     [1] http://msdn.microsoft.com/cs-cz/library/bb383977.aspx
    /// </summary>
    public static class ExtensionMethods
    {
        public static bool Contains(this string source, string toCheck, StringComparison comp)
        {
            return source.IndexOf(toCheck, comp) >= 0;
        }
    }
}