using System;
using System.Collections.Generic;

namespace SportNet.Web.Models
{
    public class GalleryItemModel : ContentItem
    {
        public string Id { get; set; }
        public string Title { get; set; }
        public string Url { get; set; }
        public string Thumbnail { get; set; }
        public string Template
        {
            get
            {
                return "/Content/Templates/GalleryItem";
            }
        }
        public string Category { get; set; }
        public int PicturesCount { get; set; }
        public string UploadedDate { get; set; }
    }
}