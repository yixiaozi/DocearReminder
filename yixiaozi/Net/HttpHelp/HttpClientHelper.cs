using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace yixiaozi.Net.HttpHelp
{
    public static class HttpClientHelper
    {
        private static Regex CharSetRegex = new Regex("charset=(?<encoding>['\"\\w-]+)", RegexOptions.IgnoreCase | RegexOptions.Compiled);

        static HttpClientHelper() => HttpClientHelper.HttpClient = new HttpClient();

        public static HttpClient HttpClient { get; }

        public static async Task<string> ReadAsHtmlAsync(this HttpResponseMessage response)
        {
            if (response == null)
                throw new ArgumentNullException(nameof(response));
            Encoding encoding = (Encoding)null;
            MediaTypeHeaderValue contentType = response.Content.Headers.ContentType;
            if (contentType != null && !string.IsNullOrWhiteSpace(contentType.CharSet))
                encoding = EncodingHelper.GetEncoding(contentType.CharSet);
            byte[] bytes = await response.Content.ReadAsByteArrayAsync();
            string input = Encoding.ASCII.GetString(bytes, 0, 512);
            Match match = HttpClientHelper.CharSetRegex.Match(input);
            if (match.Success)
                encoding = EncodingHelper.GetEncoding(match.Groups["encoding"].Value.Trim(' ', '\'', '"'));
            string str = (encoding ?? Encoding.UTF8).GetString(bytes);
            encoding = (Encoding)null;
            return str;
        }
    }
    public static class EncodingHelper
    {
        public static Encoding GetEncoding(string name) => name.ToLowerInvariant() == "utf8" ? Encoding.UTF8 : Encoding.GetEncoding(name);
    }
}
