using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace SportsApiApp
{
    public static class LoadTeams
    {
        public static async Task<TeamsList> GetAllTeamsAsync(string enteredTeam, string action)
        {
            string team = enteredTeam;
            //string action = "searchteams.php?t=";
            string url = SetUpConnection.httpClient.BaseAddress.AbsoluteUri+action+team;

            using (HttpResponseMessage response = await SetUpConnection.httpClient.GetAsync(url))
            {
                if (response.IsSuccessStatusCode)
                {
                    TeamsList teams = await response.Content.ReadAsAsync<TeamsList>();
                    return teams;
                }
                else
                {
                    throw new Exception(response.ReasonPhrase);
                }
            }




            //string rawJSON = await SetUpConnection.httpClient.GetStringAsync(url);

            //TeamsList teamsList = JsonConvert.DeserializeObject<TeamsList>(rawJSON);
            //allTeams = teamsList;
            //Console.WriteLine(allTeams.Teams.Count);
        }
    }
}
