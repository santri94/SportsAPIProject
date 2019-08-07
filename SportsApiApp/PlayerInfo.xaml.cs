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
    /// Interaction logic for PlayerInfo.xaml
    /// </summary>
    public partial class PlayerInfo : Window
    {
        public Player player = new Player();
        public PlayerInfo(Player passedPlayer)
        {
            InitializeComponent();
            this.player = passedPlayer;
            ShowOnWindow();
            
        }

        public void ShowOnWindow()
        {
            PlayerName.Text = player.strPlayer;
            PlayerImage.Source = new BitmapImage(new Uri(player.strThumb));
            Description.Text = player.strDescriptionEN;
            if (player.strWage == null || player.strWage == "")
            {
                player.strWage = "Not Defined";
            }
            MoreInfo.Text = $"- {player.strTeam} (Team)\n- {player.strNationality} (Nationality)\n- {player.strPosition} (Position)\n- {player.strHeight} (Height)\n- {player.dateBorn}\n- {player.strWage} (Salary)";
        }
    }
}
