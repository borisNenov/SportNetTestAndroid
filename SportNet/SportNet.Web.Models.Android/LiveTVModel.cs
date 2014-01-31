
using System;
using System.Collections.Generic;

namespace SportNet.Web.Models
{
    public class LiveTVModel
    {
        public List<KeyValuePair<string, List<LiveTVItemModel>>> Items { get; set; }
        public CategoriesMenuModelItem Parent { get; set; }
    }
    public class LiveTVItemModel
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Start { get; set; }
        public bool IsLive { get; set; }
        public string Url
        {
            get;
            set;
        }
        public string Description { get; set; }
    }
}