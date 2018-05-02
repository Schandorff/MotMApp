using System;
using System.Collections.Generic;
using System.Text;

namespace Manofthematch.Models
{
    public class Favourites
    {
        public List<Player> FavPlayers { get; set; }
        public List<Team> FavTeams { get; set; }
        public List<Club> FavClubs { get; set; }
    }
}
