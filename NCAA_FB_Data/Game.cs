using System;
using System.Collections.Generic;
using System.Text;

namespace NCAA_FB_Data
{
    class Game
    {
        public string homeTeam { get; set; }
        public string awayTeam { get; set; }
        public DateTime gameDateTime { get; set; }
        public int gameWeek { get; set; }
    }
}
