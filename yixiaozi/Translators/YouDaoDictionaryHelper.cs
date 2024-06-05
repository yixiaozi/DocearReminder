using HtmlAgilityPack;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web;
using System.Xml.Linq;
using System.Xml.XPath;
using yixiaozi.Net.HttpHelp;

namespace yixiaozi.Translators
{
    public static class YouDaoDictionaryHelper
    {
        public static async Task<IReadOnlyList<YouDaoSuggestItem>> SuggestAsync(
          string input)
        {
            if (string.IsNullOrWhiteSpace(input))
                throw new ArgumentNullException(nameof(input));
            IReadOnlyList<YouDaoSuggestItem> youDaoSuggestItemList1;
            using (HttpResponseMessage response = await HttpClientHelper.HttpClient.GetAsync("https://dict.youdao.com/suggest?type=DESKDICT&num=4&ver=2.0&le=eng&q=" + HttpUtility.UrlEncode(input)))
            {
                response.EnsureSuccessStatusCode();
                string text = await response.ReadAsHtmlAsync();
                List<YouDaoSuggestItem> youDaoSuggestItemList2 = new List<YouDaoSuggestItem>();
                foreach (XElement xpathSelectElement in XDocument.Parse(text).XPathSelectElements("//item"))
                {
                    XElement xelement1 = xpathSelectElement.Element((XName)"title");
                    XElement xelement2 = xpathSelectElement.Element((XName)"explain");
                    XElement xelement3 = xpathSelectElement.Element((XName)"result_num");
                    if (xelement1 != null && xelement2 != null)
                    {
                        YouDaoSuggestItem youDaoSuggestItem = new YouDaoSuggestItem()
                        {
                            Title = xelement1.Value,
                            Explain = xelement2.Value
                        };
                        int result;
                        if (xelement3 != null && int.TryParse(xelement3.Value, out result))
                            youDaoSuggestItem.ResultNum = result;
                        youDaoSuggestItemList2.Add(youDaoSuggestItem);
                    }
                }
                youDaoSuggestItemList1 = (IReadOnlyList<YouDaoSuggestItem>)youDaoSuggestItemList2;
            }
            return youDaoSuggestItemList1;
        }

        public static async Task<YouDaoWord> QueryAsync(string input)
        {
            if (string.IsNullOrWhiteSpace(input))
                throw new ArgumentNullException(nameof(input));
            YouDaoWord youDaoWord1;
            using (HttpResponseMessage response = await HttpClientHelper.HttpClient.GetAsync("https://m.youdao.com/dict?le=eng&q=" + HttpUtility.UrlEncode(input.Trim())))
            {
                response.EnsureSuccessStatusCode();
                string html = await response.ReadAsHtmlAsync();
                HtmlDocument htmlDocument = new HtmlDocument();
                htmlDocument.LoadHtml(html);
                HtmlNode htmlNode1 = htmlDocument.DocumentNode.SelectSingleNode("//p[@class=\"empty-content\"]");
                if (htmlNode1 != null)
                    throw new Exception(HtmlEntity.DeEntitize(htmlNode1.InnerText).Trim());
                YouDaoWord youDaoWord2 = new YouDaoWord(input.Trim());
                HtmlNodeCollection htmlNodeCollection1 = htmlDocument.DocumentNode.SelectNodes("//*[@class=\"phonetic\"]");
                if (htmlNodeCollection1 != null)
                {
                    foreach (HtmlNode htmlNode2 in (IEnumerable<HtmlNode>)htmlNodeCollection1)
                    {
                        string str = (htmlNode2.ParentNode.SelectSingleNode("text()")?.InnerText ?? string.Empty).Trim();
                        HtmlNode htmlNode3 = htmlNode2.ParentNode.SelectSingleNode("a");
                        if (!string.IsNullOrWhiteSpace(str) && htmlNode3 != null)
                            youDaoWord2.Phonetic.Add(new YouDaoPhonetic()
                            {
                                Type = str,
                                Text = htmlNode2.InnerText.Trim(),
                                Source = htmlNode3.GetAttributeValue("data-rel", string.Empty)
                            });
                    }
                }
                HtmlNodeCollection htmlNodeCollection2 = htmlDocument.DocumentNode.SelectNodes("//*[@id=\"ec\"]/ul/li");
                if (htmlNodeCollection2 != null)
                {
                    foreach (HtmlNode htmlNode4 in (IEnumerable<HtmlNode>)htmlNodeCollection2)
                        youDaoWord2.Paraphrase.Add(HtmlEntity.DeEntitize(htmlNode4.InnerText).Trim());
                }
                HtmlNodeCollection htmlNodeCollection3 = htmlDocument.DocumentNode.SelectNodes("//div[@class=\"sub\"]/p");
                if (htmlNodeCollection3 != null)
                {
                    foreach (HtmlNode htmlNode5 in (IEnumerable<HtmlNode>)htmlNodeCollection3)
                        youDaoWord2.Variant.Add(HtmlEntity.DeEntitize(htmlNode5.InnerText).Trim());
                }
                youDaoWord1 = !youDaoWord2.IsEmpty ? youDaoWord2 : throw new Exception("查询结果为空");
            }
            return youDaoWord1;
        }
    }
}
