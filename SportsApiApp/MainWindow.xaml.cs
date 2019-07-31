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
        public MainWindow()
        {
            InitializeComponent();
            SetUpConnection.SetUp();
            //Next();
        }

        public async void Next()
        {
            await LoadTeams.GetAllTeamsAsync(Team.Text);
            DisplayData();
        }

        public void DisplayData()
        {
            int row = 0; // leaving space for title
            int col = 1; // center column
            foreach (var item in LoadTeams.allTeams.Teams)
            {
                if (item.strTeamBadge == null)
                {
                    continue;
                }
                RowDefinition x = new RowDefinition();
                Grid.RowDefinitions.Add(x);
                x.Height = new GridLength(200);

                //-------------------------------------------------------------------------------------------------------
                //                                      Adding Image     
                //-------------------------------------------------------------------------------------------------------
                Image image = new Image();
                image.Source = new BitmapImage(new Uri(item.strTeamBadge));
                image.Height = 130;
                image.Width = 130;
                Grid.SetRow(image, row);
                Grid.SetColumn(image, col);
                Grid.Children.Add(image);
                row++;
                //-------------------------------------------------------------------------------------------------------
            }
        }

        private async void Button_Click(object sender, RoutedEventArgs e)
        {
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
                await LoadTeams.GetAllTeamsAsync(Team.Text);
                if (LoadTeams.allTeams.Teams == null)
                {
                    MessageBox.Show("Team Does not Exist on API");
                }
                else
                {
                    DisplayData();
                }
            }
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
    }
}
