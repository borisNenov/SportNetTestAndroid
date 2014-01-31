using System;
using System.Collections.Generic;
using System.Text;

namespace SportNet.Web.Models.LiveScore
{
    public class LiveScoreViewModel
    {
        public int MatchesCode { get; set; }
        public LiveScoreSportModel CurrentSport { get; set; }
        public LiveScoreCategoryModel CurrentCountry { get; set; }
        public List<TournamentModel> Tournaments { get; set; }
    }
}
