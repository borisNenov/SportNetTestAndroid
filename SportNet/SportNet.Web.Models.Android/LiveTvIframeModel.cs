using System;
using System.Collections.Generic;


namespace SportNet.Web.Models
{
    public class LiveTvIframeModel
    {
        public int Id { get; set; }
        public string Category { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string StartTime { get; set; }
        public List<LiveTvLinkItem> Links { get; set; }
    }
    public class LiveTvLinkItem
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Code { get; set; }
        public string Language { get; set; }
    }

}