using System;
using System.Collections.Generic;
using System.Text;

namespace NCAA_FB_Data
{
    class BettingInfo
    {
        public string sportsbook { get; set; }

        public string homeTeam { get; set; }

        public decimal homeTeamSpread { get; set; }

        public int homeTeamSpreadOdds { get; set; }

        public int homeTeamMoneyLineOdds { get; set; }

        public string awayTeam { get; set; }

        public decimal awayTeamSpread { get; set; }

        public int awayTeamSpreadOdds { get; set; }
        
        public string awayTeamMoneyLineOdds { get; set; }

        public decimal pointTotal { get; set; }

        public int overOdds { get; set; }

        public int underOdds { get; set; }
    }
}
