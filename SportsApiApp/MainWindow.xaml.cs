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
    public partial class MainWindow : Window
    {
        TeamsList teams = new TeamsList();
        PlayersList players = new PlayersList();
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
                //                                      Adding Website
                //-------------------------------------------------------------------------------------------------------
                TextBlock site = new TextBlock();
                site.Text = item.strWebsite;
                site.FontSize = 20;
                site.VerticalAlignment = VerticalAlignment.Center;
                site.HorizontalAlignment = HorizontalAlignment.Left;
                site.Foreground = System.Windows.Media.Brushes.White;
                site.FontWeight = System.Windows.FontWeights.Bold;
                site.FontStyle = System.Windows.FontStyles.Italic;

                Grid.SetRow(site, row);
                Grid.SetColumn(site, siteCol);
                Grid.Children.Add(site);
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

        private async void Button_Click(object sender, RoutedEventArgs e)
        {
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
                string playerInfo = $"{item.strTeam}\n- {item.strPlayer}\n- ({item.strPosition})";
                TextBlock info = new TextBlock();
                info.Text = playerInfo;
                info.FontSize = 25;
                info.VerticalAlignment = VerticalAlignment.Center;
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
                button.VerticalAlignment = VerticalAlignment.Bottom;
                button.HorizontalAlignment = HorizontalAlignment.Right;
                button.Visibility = Visibility.Visible;
                //button.Name = "OE";
                button.Content = item.strPlayer;
                button.Click += ShowPlayer_Click;
                Grid.SetRow(button, row);
                Grid.SetColumn(button, 1);
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
