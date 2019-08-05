﻿using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SportsApiApp
{
    public static class LoadPlayers
    {
        public static PlayersList allPlayers;

        public static async Task GetAllTeamsAsync(string enteredTeam, string action)
        {
            string team = enteredTeam;
            //string action = "searchteams.php?t=";
            string url = SetUpConnection.httpClient.BaseAddress.AbsoluteUri + action + team;
            string rawJSON = await SetUpConnection.httpClient.GetStringAsync(url);

            PlayersList playersList = JsonConvert.DeserializeObject<PlayersList>(rawJSON);
            allPlayers = playersList;
            //Console.WriteLine(allTeams.Teams.Count);
        }
    }
}
