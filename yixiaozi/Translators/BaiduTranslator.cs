using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace yixiaozi.Translators
{
    public static class BaiduTranslator
    {
        private const string AppId = "20210707000882577";
        private const string Secret = "D2lkxxjXthckEqQzOFtV";
        private static readonly HttpClient HttpClient = new HttpClient()
        {
            DefaultRequestHeaders = {
        ExpectContinue = new bool?(false)
      }
        };
        private static readonly Random Random = new Random();

        public static async Task<string> DetectLanguageAsync(string query)
        {
            if (string.IsNullOrWhiteSpace(query))
                return string.Empty;
            string salt = BaiduTranslator.CreateSalt();
            SortedDictionary<string, string> sortedDictionary = new SortedDictionary<string, string>()
      {
        {
          "q",
          query
        },
        {
          "appid",
          "20210707000882577"
        },
        {
          "salt",
          salt
        },
        {
          "sign",
          BaiduTranslator.EncryptString("20210707000882577" + query + salt + "D2lkxxjXthckEqQzOFtV")
        }
      };
            HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Post, "https://fanyi-api.baidu.com/api/trans/vip/language")
            {
                Content = (HttpContent)new FormUrlEncodedContent((IEnumerable<KeyValuePair<string, string>>)sortedDictionary)
            };
            HttpResponseMessage httpResponseMessage = await BaiduTranslator.HttpClient.SendAsync(request);
            httpResponseMessage.EnsureSuccessStatusCode();
            BaiduTranslator.DetectLanguageResponse languageResponse = JsonConvert.DeserializeObject<BaiduTranslator.DetectLanguageResponse>(await httpResponseMessage.Content.ReadAsStringAsync());
            if (languageResponse == null)
                throw new BaiduTranslator.BaiduTranslateException("返回结果解析失败");
            if (languageResponse.ErrorCode != 0)
                throw new BaiduTranslator.BaiduTranslateException(string.Format("语种识别接口调用失败： {0} - {1}", (object)languageResponse.ErrorCode, (object)languageResponse.ErrorMessage));
            return languageResponse.Data?.Src;
        }

        public static async Task<string> TransalteAsync(string query, string from, string to)
        {
            if (string.IsNullOrWhiteSpace(from))
                throw new ArgumentNullException(nameof(from));
            if (string.IsNullOrWhiteSpace(to))
                throw new ArgumentNullException(nameof(to));
            if (string.IsNullOrWhiteSpace(query))
                return string.Empty;
            if (string.Equals(from, to, StringComparison.InvariantCultureIgnoreCase))
                return query;
            List<string> output = new List<string>();
            StringBuilder builder = new StringBuilder();
            string str = query;
            char[] separator = new char[2] { '\n', '\r' };
            foreach (string line in ((IEnumerable<string>)str.Split(separator, StringSplitOptions.RemoveEmptyEntries)).Select<string, string>((Func<string, string>)(i => i.Trim())).ToList<string>())
            {
                if (builder.Length >= 1500)
                    await TransalteAsync();
                builder.AppendLine(line);
            }
            if (builder.Length > 0)
                await TransalteAsync();
            return string.Join(Environment.NewLine + Environment.NewLine, (IEnumerable<string>)output);

            async Task TransalteAsync()
            {
                str = builder.ToString();
                string salt = BaiduTranslator.CreateSalt();
                SortedDictionary<string, string> sortedDictionary = new SortedDictionary<string, string>()
        {
          {
            "q",
            str
          },
          {
            "from",
            from
          },
          {
            "to",
            to
          },
          {
            "appid",
            "20210707000882577"
          },
          {
            "salt",
            salt
          },
          {
            "sign",
            BaiduTranslator.EncryptString("20210707000882577" + str + salt + "D2lkxxjXthckEqQzOFtV")
          }
        };
                HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Post, "http://api.fanyi.baidu.com/api/trans/vip/translate")
                {
                    Content = (HttpContent)new FormUrlEncodedContent((IEnumerable<KeyValuePair<string, string>>)sortedDictionary)
                };
                HttpResponseMessage httpResponseMessage = await BaiduTranslator.HttpClient.SendAsync(request);
                httpResponseMessage.EnsureSuccessStatusCode();
                BaiduTranslator.FanYiResponse fanYiResponse = JsonConvert.DeserializeObject<BaiduTranslator.FanYiResponse>(await httpResponseMessage.Content.ReadAsStringAsync());
                if (fanYiResponse == null)
                    throw new BaiduTranslator.BaiduTranslateException("返回结果解析失败");
                if (fanYiResponse.ErrorCode != 0)
                    throw new BaiduTranslator.BaiduTranslateException(string.Format("翻译接口调用失败： {0} - {1}", (object)fanYiResponse.ErrorCode, (object)fanYiResponse.ErrorMessage));
                builder.Clear();
                foreach (BaiduTranslator.FanYiResult result in fanYiResponse.Results)
                    output.Add(result.Dst);
            }
        }

        private static string CreateSalt() => BaiduTranslator.Random.Next(100000).ToString();

        private static string EncryptString(string str)
        {
            using (MD5 md5 = MD5.Create())
            {
                byte[] bytes = Encoding.UTF8.GetBytes(str);
                byte[] hash = md5.ComputeHash(bytes);
                StringBuilder stringBuilder = new StringBuilder();
                foreach (byte num in hash)
                    stringBuilder.Append(num.ToString("x2"));
                return stringBuilder.ToString();
            }
        }

        public class BaiduTranslateException : Exception
        {
            public BaiduTranslateException(string message)
              : base(message)
            {
            }
        }

        public class DetectLanguageResponse
        {
            [JsonProperty("error_code")]
            public int ErrorCode { get; set; }

            [JsonProperty("error_msg")]
            public string ErrorMessage { get; set; }

            [JsonProperty("data")]
            public BaiduTranslator.DetectLanguageResult Data { get; set; }
        }

        public class DetectLanguageResult
        {
            [JsonProperty("src")]
            public string Src { get; set; }
        }

        public class FanYiResponse
        {
            [JsonProperty("from")]
            public string From { get; set; }

            [JsonProperty("to")]
            public string To { get; set; }

            [JsonProperty("trans_result")]
            public List<BaiduTranslator.FanYiResult> Results { get; set; }

            [JsonProperty("error_code")]
            public int ErrorCode { get; set; }

            [JsonProperty("error_msg")]
            public string ErrorMessage { get; set; }
        }

        public class FanYiResult
        {
            [JsonProperty("src")]
            public string Src { get; set; }

            [JsonProperty("dst")]
            public string Dst { get; set; }
        }
    }
}
