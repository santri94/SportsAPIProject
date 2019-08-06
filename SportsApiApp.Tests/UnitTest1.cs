using NUnit.Framework;
using SportsApiApp;
using System.Threading.Tasks;

namespace Tests
{
    public class Tests
    {
        [SetUp]
        public void Setup()
        {
            //------------------------------------------------------------------------------
            SetUpConnection.SetUp();
        }


        //-----------------------------------------------------------------------------------
        //                      When Passing a valid action and team name
        //                          Our Teams list SHOULD NOT BE NULL
        //-----------------------------------------------------------------------------------
        [Test]
        public async Task GetAllTeamsAsyncNotNullList()
        {
            string action = "searchteams.php?t="; // Search teams with this name

            string team = "Colombia";

            TeamsList teams = await LoadTeams.GetAllTeamsAsync(team, action);

            Assert.NotNull(teams.Teams);

        }

        //-----------------------------------------------------------------------------------
        //                      When Passing action (searchplayers) and a team name WITH PLAYERS
        //                          Our Player List SHOULD NOT BE NULL
        //-----------------------------------------------------------------------------------
        [Test]
        public async Task GetAllPlayersAsyncNotNull()
        {
            string action = "searchplayers.php?t="; // Search teams with this name

            string team = "Everton";

            await LoadPlayers.GetAllPlayersAsync(team, action);

            Assert.NotNull(LoadPlayers.allPlayers.Player);

        }

    }
}