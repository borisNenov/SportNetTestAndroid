using System;
using System.Collections.Generic;

namespace SportNet.Web.Models
{    
    public class VideoModelItem : ContentItem
    {
        public string Id { get; set; }
        public string Title { get; set; }
        public string Url { get; set; }
        public string Thumbnail { get; set; }
        public string Template
        {
            get
            {
                return "/Content/Templates/VideoItem";
            }
        }
        public string Category { get; set; }

        public string UploadedDate { get; set; }
    }
} 