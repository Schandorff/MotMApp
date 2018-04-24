using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Manofthematch.Models
{
    public class Team
    {
        public int teamId { get; set; }
        public string TeamName { get; set; }
        public string teamSport { get; set; }
        public List<Match> teamMatches { get; set; }
    }
}