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

            await LoadTeams.GetAllTeamsAsync(team, action);

            Assert.NotNull(LoadTeams.allTeams.Teams);

        }
    }
}