using System.Text.RegularExpressions;

namespace KristaShop.Common.Helpers
{
    public static class UrlHelper
    {
        public static string GetURL(string controller, string action)
        {
            string url = $"/{controller}/{action}";
            return url;
        }

        public static string GetURL(string controller, string action, string parameters)
        {
            string url = $"/{controller}/{action}?{parameters}";
            return url;
        }

        public static string GetURL(string url)
        {
            return url;
        }

        public static bool CompareUrls(string mainUrl, string secondUrl)
        {
            Regex rg = new Regex(mainUrl, RegexOptions.IgnoreCase);
            return rg.IsMatch(secondUrl);
        }
    }
}
