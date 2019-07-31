using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SportsApiApp
{
    public static class LoadTeams
    {
        public static TeamsList allTeams;
        public static async Task GetAllTeamsAsync()
        {
            string team = "borussia";
            string action = "searchteams.php?t=";
            string url = SetUpConnection.httpClient.BaseAddress.AbsoluteUri+action+team;
            string rawJSON = await SetUpConnection.httpClient.GetStringAsync(url);

            TeamsList teamsList = JsonConvert.DeserializeObject<TeamsList>(rawJSON);
            allTeams = teamsList;
            Console.WriteLine(allTeams.Teams.Count);
        }
    }
}
