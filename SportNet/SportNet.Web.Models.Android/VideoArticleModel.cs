using System;
using System.Collections.Generic;

namespace SportNet.Web.Models
{
    public class VideoArticleModel
    {
        public string EmbedCode { get; set; }
        public string Id { get; set; }
        public string Title { get; set; }
        public string CategoryId { get; set; }
        public string CategoryName { get; set; }
        public List<CategoriesMenuModelItem> Categories;
        public string Url { get; set; }

        public string Thumbnail { get; set; }

        public string DateUploaded { get; set; }
    }
}