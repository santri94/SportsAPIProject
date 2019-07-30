using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SportsApiApp
{
    public class TeamsList
    {
        private List<Team> teams;

        public List<Team> Teams { get => teams; set => teams = value; }
    }
}
