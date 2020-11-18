using System;
using System.Collections.Generic;
using HtmlAgilityPack;
using ScrapySharp.Extensions;
using ScrapySharp.Network;

namespace scraper
{
    class Program
    {
        static ScrapingBrowser _browser = new ScrapingBrowser();
        static void Main(string[] args)
        {
            var mainPageLinks = GetMainPageLinks("https://newyork.craigslist.org/d/computer-gigs/search/cpg");
            Console.WriteLine(mainPageLinks.Count);
        }
        static List<string> GetMainPageLinks(string url)
        {
            var homePageLinks = new List<string>();
            var html = GetHtml(url);
            var links = html.CssSelect("a");

            foreach (var link in links)
            {
                if (link.Attributes["href"].Value.Contains(".html"))
                {
                    homePageLinks.Add(link.Attributes["href"].Value);
                }
            }
            return homePageLinks;
        }
        static HtmlNode GetHtml(string url)
        {
            WebPage webPage = _browser.NavigateToPage(new Uri(url));
            return webPage.Html;
        }
    }
}
