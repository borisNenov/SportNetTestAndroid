using System;
using System.Collections.Generic;
using System.Text;

namespace SportNet.Web.Models.LiveScore
{
    public class MatchModel
    {
        public int Id { get; set; }
        public string Result { get; set; }
        public string State { get; set; }
        public string MatchTime { get; set; }

        public string Team1 { get; set; }

        public string Team2 { get; set; }
    }
}
