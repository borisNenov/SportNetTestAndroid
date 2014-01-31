using System;
using System.Collections.Generic;

namespace SportNet.Web.Models
{
    public interface ContentItem {
        string Category { get; set; }
        string Title { get; set; }
    }
    public class CategoryModel
    {
        public CategoryModel()
        {
            News = new List<NewsModelItem>();
        }
        public int CategoryId { get; set; }
        public int CurrentPage { get; set; }
        public CategoriesMenuModelItem Parent { get; set; }
        public List<CategoriesMenuModelItem> Categories { get; set; }
        public List<NewsModelItem> News { get; set; }
        public List<CategoriesMenuModelItem> BradCrump { get; set; }
    }
	public class CategoryVideoModel
	{
		public CategoryVideoModel()
		{
			News = new List<VideoModelItem>();
		}
		public int CategoryId { get; set; }
		public int CurrentPage { get; set; }
		public CategoriesMenuModelItem Parent { get; set; }
		public List<CategoriesMenuModelItem> Categories { get; set; }
		public List<VideoModelItem> News { get; set; }
		public List<CategoriesMenuModelItem> BradCrump { get; set; }
	}
	public class CategoryPicturesModel
	{
		public CategoryPicturesModel()
		{
			News = new List<GalleryItemModel>();
		}
		public int CategoryId { get; set; }
		public int CurrentPage { get; set; }
		public CategoriesMenuModelItem Parent { get; set; }
		public List<CategoriesMenuModelItem> Categories { get; set; }
		public List<GalleryItemModel> News { get; set; }
		public List<CategoriesMenuModelItem> BradCrump { get; set; }
	}
    public class CategoriesMenuModelItem
    {
        public string Name { get; set; }
        public int Link { get; set; }
    }
}