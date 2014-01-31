using System;
using System.Collections.Generic;

namespace SportNet.Web.Models
{
    public class GalleryArticleModel
    {
		public GalleryArticleModel()
		{
			Images = new List<string>();
		}
        public int Id { get; set; }
        public string Title { get; set; }
        public List<string> Images { get; set; }
        public string Description { get; set; }
        public string CategoryName { get; set; }
        public int CategoryId { get; set; }
        public string UploadDate { get; set; }
        public string Thumbnail { get; set; }
        public List<GalleryItemModel> MostPopular { get; set; }
    }
}