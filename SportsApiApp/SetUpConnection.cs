using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace SportsApiApp
{
    public static class SetUpConnection
    {
        public static HttpClient httpClient { get; set; }

        public static void SetUp()
        {
            httpClient = new HttpClient();
            httpClient.BaseAddress = new Uri("https://www.thesportsdb.com/api/v1/json/1/");

            // Key For Testing = 1
        }
    }
}
