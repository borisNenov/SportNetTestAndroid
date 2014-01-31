using System;
using System.Collections.Generic;
using System.Text;

namespace SportNet.Web.Models.LiveScore
{
    public class TournamentModel
    {
        public string Name { get; set; }
        public string Category { get; set; }
        public List<MatchModel> Matches { get; set; }
    }
}
