using System.Text.RegularExpressions;

namespace ESA.Shared
{
    public static class Helper
    {
        public static string GetFileExtensionFromBase64(string base64String)
        {
            var dataUriPattern = @"data:image/(?<type>.+?);base64";
            var match = Regex.Match(base64String, dataUriPattern);
            var type = match.Groups["type"].Value;
            return $".{type}";
        }

        public static string ExtractBase64Data(string base64String)
        {
            var base64DataPattern = @"base64,(?<data>.+)";
            var match = Regex.Match(base64String, base64DataPattern);
            return match.Groups["data"].Value;
        }
    }
}
