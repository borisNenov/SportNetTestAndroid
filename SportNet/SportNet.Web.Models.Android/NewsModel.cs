using System;
using System.Collections.Generic;

namespace SportNet.Web.Models
{       
    public class NewsModelItem : ContentItem
    {
        public string Title { get; set; }
        public string Img { get; set; }
        public string Category { get; set; }
        public string Template
        {
            get
            {
                if (string.IsNullOrEmpty(Img))
                {
                    return "/Content/Templates/NewsItemNoPic";
                }
                return "/Content/Templates/NewsItemWithPic";
            }
        }

        public string Id { get; set; }

        public string PublishedDate { get; set; }

        public int SmallId { get; set; }

        public string Url { get; set; }
    }
}