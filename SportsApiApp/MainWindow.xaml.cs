using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace SportsApiApp
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    /// images : https://wallpaperplay.com/board/cool-soccer-wallpapers
    /// images : https://www.konfest.com/product/free-png-ball-6/
    /// API: https://www.thesportsdb.com/api.php
    public partial class MainWindow : Window
    {
        TeamsList teams = new TeamsList();
        PlayersList players = new PlayersList();
        EventsList events = new EventsList();
        public MainWindow()
        {
            InitializeComponent();
            SetUpConnection.SetUp();

        }


        public async void DisplayData()
        {
            int row = 0; // leaving space for title
            int teamCol = 1;
            int imgCol = 2; // center column
            int jerseyCol = 3;
            int siteCol = 4;
            
            foreach (var item in teams.Teams)
            {
                if (item.strTeamBadge == null || item.strTeamBadge == "")
                {
                    item.strTeamBadge = "C:\\Sports Project API\\SportsApiApp\\null.png";
                }
                if (item.strTeamJersey == null || item.strTeamJersey == "")
                {
                    item.strTeamJersey = "C:\\Sports Project API\\SportsApiApp\\null.png";
                }
                if (item.strWebsite == null || item.strWebsite == "")
                {
                    item.strWebsite = "None";
                }
                RowDefinition x = new RowDefinition();
                Grid.RowDefinitions.Add(x);
                x.Height = new GridLength(200);
                //-------------------------------------------------------------------------------------------------------
                //                                      Adding Name
                //-------------------------------------------------------------------------------------------------------
                TextBlock info = new TextBlock();
                info.Text = item.strTeam;
                if (item.strTeam.Length > 12)
                {
                    info.FontSize = 17;
                }
                else
                {

                    info.FontSize = 25;
                }
                info.VerticalAlignment = VerticalAlignment.Center;
                info.HorizontalAlignment = HorizontalAlignment.Left;
                info.Foreground = System.Windows.Media.Brushes.PaleVioletRed;
                info.FontWeight = System.Windows.FontWeights.Bold;
                info.FontStyle = System.Windows.FontStyles.Italic;

                Grid.SetRow(info, row);
                Grid.SetColumn(info, teamCol);
                Grid.Children.Add(info);
                //-------------------------------------------------------------------------------------------------------
                //-------------------------------------------------------------------------------------------------------
                //                                      Adding Button
                //-------------------------------------------------------------------------------------------------------
                Button button = new Button();
                button.Height = 30;
                button.Width = 150;
                button.VerticalAlignment = VerticalAlignment.Center;
                button.HorizontalAlignment = HorizontalAlignment.Left;
                button.Visibility = Visibility.Visible;
                button.Name = "O"+item.idTeam;
                button.Content = "Show Next 5 Games";
                button.Click += ShowGames_Click;
                Grid.SetRow(button, row);
                Grid.SetColumn(button, 4);
                Grid.Children.Add(button);

                //-------------------------------------------------------------------------------------------------------

                //-------------------------------------------------------------------------------------------------------
                //                                      Adding Image     
                //-------------------------------------------------------------------------------------------------------
                Image image = new Image();
                image.Source = new BitmapImage(new Uri(item.strTeamBadge));
                image.Height = 130;
                image.Width = 130;
                Grid.SetRow(image, row);
                Grid.SetColumn(image, imgCol);
                Grid.Children.Add(image);
                //-------------------------------------------------------------------------------------------------------
                //-------------------------------------------------------------------------------------------------------
                //                                      Adding Image     
                //-------------------------------------------------------------------------------------------------------
                Image jersey = new Image();
                jersey.Source = new BitmapImage(new Uri(item.strTeamJersey));
                jersey.Height = 130;
                jersey.Width = 130;
                Grid.SetRow(jersey, row);
                Grid.SetColumn(jersey, jerseyCol);
                Grid.Children.Add(jersey);
                row++;
                //-------------------------------------------------------------------------------------------------------
            }
        }

        private async void ShowGames_Click(object sender, RoutedEventArgs e)
        {
            
            string choosedTeam = ((System.Windows.FrameworkElement)sender).Name.Trim('O');

            //Send request
            string action = "eventsnext.php?id=";
            events = await LoadEvents.GetAllEventsAsync(choosedTeam, action);
            if (events.Events == null)
            {
                MessageBox.Show("No Games To Show");
            }
            else
            {
                EmptyGrid();
                DisplayEvents(choosedTeam);
            }
        }

        private void DisplayEvents(string chooseTeam)
        {
            int row = 0;

            //            Player player = new Player();
            //player = players.Player.Where(x => x.strPlayer == playerName).ToList()[0];
            Team team = new Team();
            team = teams.Teams.Where(x => x.idTeam == chooseTeam).ToList()[0];

            Logo.Source = new BitmapImage(new Uri(team.strTeamJersey));

            foreach (var item in events.Events)
            {
                RowDefinition x = new RowDefinition();
                Grid.RowDefinitions.Add(x);
                x.Height = new GridLength(50);
                //-------------------------------------------------------------------------------------------------------
                //                                      Adding Name
                //-------------------------------------------------------------------------------------------------------
                TextBlock info = new TextBlock();
                info.Text = item.strEvent;
                info.FontSize = 30;
                info.VerticalAlignment = VerticalAlignment.Center;
                info.HorizontalAlignment = HorizontalAlignment.Center;
                info.Foreground = System.Windows.Media.Brushes.PaleVioletRed;
                info.FontWeight = System.Windows.FontWeights.Bold;
                info.FontStyle = System.Windows.FontStyles.Italic;
                info.TextDecorations = TextDecorations.Underline;

                Grid.SetRow(info, row);
                Grid.SetColumn(info, 1);
                Grid.SetColumnSpan(info, 4);
                Grid.Children.Add(info);
                //-------------------------------------------------------------------------------------------------------
                row++;
                //-------------------------------------------------------------------------------------------------------
                RowDefinition y = new RowDefinition();
                Grid.RowDefinitions.Add(y);
                y.Height = new GridLength(100);
                //-------------------------------------------------------------------------------------------------------
                //-------------------------------------------------------------------------------------------------------
                //                                      Adding More Info
                //-------------------------------------------------------------------------------------------------------
                TextBlock league = new TextBlock();
                league.Text = item.strLeague;
                league.FontSize = 25;
                league.VerticalAlignment = VerticalAlignment.Center;
                league.HorizontalAlignment = HorizontalAlignment.Center;
                league.Foreground = System.Windows.Media.Brushes.White;
                league.FontWeight = System.Windows.FontWeights.Bold;
                league.FontStyle = System.Windows.FontStyles.Italic;

                Grid.SetRow(league, row);
                Grid.SetColumn(league, 1);
                Grid.SetColumnSpan(league, 4);
                Grid.Children.Add(league);

                //-------------------------------------------------------------------------------------------------------
                row++;
                //-------------------------------------------------------------------------------------------------------
                RowDefinition i = new RowDefinition();
                Grid.RowDefinitions.Add(i);
                i.Height = new GridLength(50);
                //-------------------------------------------------------------------------------------------------------
                //                                      Adding More Info
                //-------------------------------------------------------------------------------------------------------
                if (item.strDate == null)
                {
                    item.strDate = "Not Specified";
                }
                TextBlock time = new TextBlock();
                time.Text = $"Date: {item.strDate} Time: {item.strTime}";
                time.FontSize = 15;
                time.VerticalAlignment = VerticalAlignment.Center;
                time.HorizontalAlignment = HorizontalAlignment.Center;
                time.Foreground = System.Windows.Media.Brushes.White;
                time.FontWeight = System.Windows.FontWeights.Bold;
                time.FontStyle = System.Windows.FontStyles.Italic;

                Grid.SetRow(time, row);
                Grid.SetColumn(time, 1);
                Grid.SetColumnSpan(time, 4);
                Grid.Children.Add(time);
                //-------------------------------------------------------------------------------------------------------
                row++;



            }


        }

        private async void Button_Click(object sender, RoutedEventArgs e)
        {
            Logo.Source = new BitmapImage(new Uri("C:\\Sports Project API\\SportsApiApp\\logo.png")); // empty image logo in recent search
            RecentSearch.Text = Team.Text;
            if (RecentSearch.Text.Length > 12)
            {
                RecentSearch.FontSize = 17;
            }
            else
            {
                RecentSearch.FontSize = 25;
            }
            string action = "searchteams.php?t=";
            EmptyGrid();
            //------------------------------------------------------------------------------------------------------------
            //                                      Checking To See if Search Bar is empty
            //                                          If so, don't send the request
            //------------------------------------------------------------------------------------------------------------
            if (Team.Text == "")
            {
                // Don't Do anything
            }
            else
            {
                //EmptyGrid();
                teams = await LoadTeams.GetAllTeamsAsync(Team.Text, action);
                if (teams.Teams == null)
                {
                    MessageBox.Show("Team Does not Exist on API");
                }
                else
                {
                    DisplayData();
                }
            }
            Team.Text = "";
            //------------------------------------------------------------------------------------------------------------
        }

        public void EmptyGrid()
        {
            var numberOfChildren = Grid.Children.Count;
            var numberOfRows = Grid.RowDefinitions.Count;
            //-----------------------------------------------------------------------------------------------
            //                                  Delete everything  from grid    
            //-----------------------------------------------------------------------------------------------
            if (numberOfRows == 0)
            {
                //Don't do Anything about to start creating Rows;
            }
            else
            {
                Grid.Children.RemoveRange(0, numberOfChildren);
                Grid.RowDefinitions.RemoveRange(0, numberOfRows);

            }

            //-----------------------------------------------------------------------------------------------
        }

        private async void ShowPlayers_Click(object sender, RoutedEventArgs e)
        {
            Logo.Source = new BitmapImage(new Uri("C:\\Sports Project API\\SportsApiApp\\logo.png"));
            RecentSearch.Text = TeamPlayers.Text;
            if (RecentSearch.Text.Length > 12)
            {
                RecentSearch.FontSize = 17;
            }
            else
            {
                RecentSearch.FontSize = 25;
            }
            string action = "searchplayers.php?t=";
            EmptyGrid();
            //------------------------------------------------------------------------------------------------------------
            //                                      Checking To See if Search Bar is empty
            //                                          If so, don't send the request
            //------------------------------------------------------------------------------------------------------------
            if (TeamPlayers.Text == "")
            {
                // Don't Do anything
            }
            else
            {
                players = await LoadPlayers.GetAllPlayersAsync(TeamPlayers.Text, action);
                if (players.Player == null)
                {
                    MessageBox.Show("Team Does not Exist on API");
                }
                else
                {
                    DisplayPlayers();
                }
            }
            TeamPlayers.Text = "";
        }

        public void DisplayPlayers()
        {
            int row = 0; // leaving space for title
            int playerCol = 1;
            int playerImgCol = 3; // center column
            int positionCol = 4;

            foreach (var item in players.Player)
            {

                if (item.strThumb == null || item.strThumb == "")
                {
                    item.strThumb = "C:\\Sports Project API\\SportsApiApp\\null.png"; // default img 
                }
                if (item.strPosition == null || item.strPosition == "")
                {
                    item.strPosition = "None";
                }

                RowDefinition x = new RowDefinition();
                Grid.RowDefinitions.Add(x);
                x.Height = new GridLength(200);
                //-------------------------------------------------------------------------------------------------------
                //                                      Adding Name
                //-------------------------------------------------------------------------------------------------------
                string playerInfo = $"{item.strTeam}\n- {item.strPlayer}";
                TextBlock info = new TextBlock();
                info.Text = playerInfo;
                info.FontSize = 25;
                info.VerticalAlignment = VerticalAlignment.Top;
                info.HorizontalAlignment = HorizontalAlignment.Left;
                info.Foreground = System.Windows.Media.Brushes.White;
                info.FontWeight = System.Windows.FontWeights.Bold;
                info.FontStyle = System.Windows.FontStyles.Italic;

                Grid.SetRow(info, row);
                Grid.SetColumn(info, playerCol);
                Grid.SetColumnSpan(info, 2);
                Grid.Children.Add(info);
                //-------------------------------------------------------------------------------------------------------
                //                                      Adding Button
                //-------------------------------------------------------------------------------------------------------
                Button button = new Button();
                button.Height = 30;
                button.Width = 100;
                button.VerticalAlignment = VerticalAlignment.Center;
                button.HorizontalAlignment = HorizontalAlignment.Left;
                button.Visibility = Visibility.Visible;
                button.Name = "Oe";
                button.Content = item.strPlayer;
                button.Click += ShowPlayer_Click;
                Grid.SetRow(button, row);
                Grid.SetColumn(button, 2);
                Grid.Children.Add(button);

                //-------------------------------------------------------------------------------------------------------

                //-------------------------------------------------------------------------------------------------------
                //                                      Adding Image     
                //-------------------------------------------------------------------------------------------------------
                Image image = new Image();
                image.Source = new BitmapImage(new Uri(item.strThumb));
                image.Height = 180;
                image.Width = 250;
                Grid.SetRow(image, row);
                Grid.SetColumn(image, playerImgCol);
                Grid.SetColumnSpan(image, 2);
                Grid.Children.Add(image);
                //-------------------------------------------------------------------------------------------------------
                row++;
            }

        }

        void ShowPlayer_Click(object sender, RoutedEventArgs e)
        {
            //MessageBox.Show($"Now Show player : {sender.ToString().Remove(0,32)}");
            string playerName = sender.ToString().Remove(0, 32);

            Player player = new Player();
            player = players.Player.Where(x => x.strPlayer == playerName).ToList()[0];

            PlayerInfo info = new PlayerInfo(player);
            info.Show();
        }

    }
}
