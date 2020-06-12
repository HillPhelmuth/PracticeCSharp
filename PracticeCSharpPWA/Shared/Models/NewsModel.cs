using System.Collections.Generic;

namespace PracticeCSharpPWA.Shared.Models
{
    public class NewsModel
    {
        public IEnumerable<NewsItem> Articles { get; set; }
    }
    public class NewsItem
    {
        public string Title { get; set; }
        public string Content { get; set; }
        public string Author { get; set; }
        public string UrlToImage { get; set; }
        public string Url { get; set; }
    }

}
