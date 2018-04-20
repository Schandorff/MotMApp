﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Manofthematch.Models
{
    public class Player
    {
        public int playerId { get; set; }
        public string playerFirstName { get; set; }
        public string playerLastName { get; set; }
        public int playerNumber { get; set; }
        public int teamId { get; set; }
        public string teamName { get; set; }
    }
}