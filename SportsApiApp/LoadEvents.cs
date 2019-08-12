using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace SportsApiApp
{
    public static class LoadEvents
    {
        public static async Task<EventsList> GetAllEventsAsync(string enteredTeam, string action)
        {
            string team = enteredTeam;
            string url = SetUpConnection.httpClient.BaseAddress.AbsoluteUri + action + team;

            using (HttpResponseMessage response = await SetUpConnection.httpClient.GetAsync(url))
            {
                if (response.IsSuccessStatusCode)
                {
                    EventsList eventsList = await response.Content.ReadAsAsync<EventsList>();
                    return eventsList;
                }
                else
                {
                    throw new Exception(response.ReasonPhrase);
                }
            }



        }
    }
}
