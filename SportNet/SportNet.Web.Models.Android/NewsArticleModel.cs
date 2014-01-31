using System;
using System.Collections.Generic;

namespace SportNet.Web.Models
{
    public class NewsArticleModel
    {
        public string Id { get; set; }
        public string RealId { get; set; } 
        public string Title { get; set; }
        public string MainPic { get; set; }
        public string Content { get; set; }
        public string Source { get; set; }
        public string SourceUrl { get; set; }
        public string PublishedDate { get; set; }
        public CategoriesMenuModelItem CurrentCategory { get; set; }
        public List<CategoriesMenuModelItem> Categories;

    }
}