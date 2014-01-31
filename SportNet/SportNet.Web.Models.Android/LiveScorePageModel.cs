using System;
using System.Collections.Generic;
using System.Text;

namespace SportNet.Web.Models.LiveScore
{
    public class LiveScorePageModel
    {
        public List<LiveScoreSportModel> Sports { get; set; }
        public int CurrentSport { get; set; }
        public List<LiveScoreCategoryModel> Categories { get; set; }
        public LiveScoreViewModel Matches { get; set; }
    }
}
