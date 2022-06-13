using AngleSharp.Html.Parser;
using System;
using System.Net.Http;

namespace TLSScrape.console
{
    class Program
    {
        static void Main(string[] args)
        {

                var html = GetTLSHtml();
            ParseTLSHtml(html);

            }

        private static void ParseTLSHtml(string html)
        {
            var parser = new HtmlParser();
            var document = parser.ParseDocument(html);
            var resultDivs = document.QuerySelectorAll(".post");
            foreach (var div in resultDivs)
            {
                var titleSpan = div.QuerySelector("h2");
                if (titleSpan == null)
                {
                    continue;
                }
                Console.WriteLine(titleSpan.TextContent);
                var comments = div.QuerySelector(".backtotop");
                if (comments == null)
                {
                    continue;
                }
                Console.WriteLine(comments.TextContent);

                var imageTag = div.QuerySelector(".aligncenter");
                if (imageTag == null)
                {
                    continue;
                }
                Console.WriteLine(imageTag.TextContent);
                var linkTag = div.QuerySelector("a");
                if (linkTag == null)
                {
                    continue;
                }
                Console.WriteLine(linkTag.TextContent);
            
                //var priceSpan = div.QuerySelector("span.a-offscreen");
                //if (priceSpan != null)
                //{
                //    Console.WriteLine(priceSpan.TextContent);
                //}
            }
        }

        static string GetTLSHtml()
            {
                var handler = new HttpClientHandler
                {
                    AutomaticDecompression = System.Net.DecompressionMethods.GZip | System.Net.DecompressionMethods.Deflate
                };
                using var client = new HttpClient(handler);
                var url = "https://www.thelakewoodscoop.com";
                var html = client.GetStringAsync(url).Result;
                return html;
            }
        }
    }