using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace SportsApiApp
{
    public static class LoadPlayers
    {

        public static async Task<PlayersList> GetAllPlayersAsync(string enteredTeam, string action)
        {
            string team = enteredTeam;
            string url = SetUpConnection.httpClient.BaseAddress.AbsoluteUri + action + team;

            using (HttpResponseMessage response = await SetUpConnection.httpClient.GetAsync(url))
            {
                if (response.IsSuccessStatusCode)
                {
                    PlayersList playersList = await response.Content.ReadAsAsync<PlayersList>();
                    return playersList;
                }
                else
                {
                    throw new Exception(response.ReasonPhrase);
                }
            }



        }
    }
}
