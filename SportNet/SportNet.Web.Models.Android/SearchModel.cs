using System;
using System.Collections.Generic;
using System.Text;

namespace SportNet.Web.Models
{
    public class SearchModel
    {
        public List<ContentItem> News { get; set; }
        public List<CategoriesMenuModelItem> BradCrump { get; set; }
        public int Index { get; set; }
        public string Query { get; set; }

        public string Type { get; set; }
    }
}
