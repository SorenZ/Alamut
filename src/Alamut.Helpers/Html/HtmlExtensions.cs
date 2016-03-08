using System.Text.RegularExpressions;

namespace Alamut.Helpers.Html
{
    /// <summary>
    /// sets of tools and utility to work on Html text
    /// </summary>
    public static class HtmlExtensions
    {
        private static readonly Regex HtmlRegext = new Regex(@"<[^>]*(>|$)|&nbsp;|&zwnj;|&raquo;|&laquo;|&quot;|&shy;|&rlm;",
            RegexOptions.Compiled);

        /// <summary>
        /// remove html elements
        /// </summary>
        /// <param name="inputHtml">input html text</param>
        /// <param name="length">length of result to return back (default = null)</param>
        /// <param name="endWith">the restul end with "..." or nothing</param>
        /// <returns></returns>
        public static string StripHtmlElements(this string inputHtml, int? length = null, string endWith = " ...")
        {
            var result = HtmlRegext.Replace(inputHtml, string.Empty);

            return length == null || result.Length < length.Value
                ? result
                : result.Substring(0, length.Value) + endWith;
        }
    }
}
