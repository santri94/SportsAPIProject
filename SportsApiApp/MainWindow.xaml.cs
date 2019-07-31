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
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            SetUpConnection.SetUp();
            Next();
        }

        public async void Next()
        {
            await LoadTeams.GetAllTeamsAsync();
            DisplayData();
        }

        public void DisplayData()
        {
            int row = 1; // leaving space for title
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
    }
}
