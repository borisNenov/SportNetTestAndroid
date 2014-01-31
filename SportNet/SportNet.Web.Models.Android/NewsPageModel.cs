using System;
using System.Collections.Generic;

namespace SportNet.Web.Models
{
    public class NewsPageModel
    {
        public NewsPageModel()
        {
            News = new List<ContentItem>();
            Paging = new List<int>();
        }
        public CategoriesMenuModelItem CurrentCategory { get; set; }
        public int CurrentPage { get; set; }
        public CategoriesMenuModelItem ParentCategory { get; set; }
        public List<CategoriesMenuModelItem> Categories { get; set; }
        public List<ContentItem> News { get; set; }
        public List<CategoriesMenuModelItem> BradCrump { get; set; }
        public List<int> Paging { get; set; }
    }
}