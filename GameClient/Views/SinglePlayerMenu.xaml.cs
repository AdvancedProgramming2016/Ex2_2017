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
using System.Windows.Shapes;

namespace GameClient.Views
{
    /// <summary>
    /// Interaction logic for SinglePlayerMenu.xaml
    /// </summary>
    public partial class SinglePlayerMenu : Window
    {
        public SinglePlayerMenu()
        {
            InitializeComponent();
            /*
            this.singlePlayerGameViewModel = new SinglePlayerGameViewModel
                (new SinglePlayerGameModel(), new SettingsModel());
            this.DataContext = this.singlePlayerGameViewModel;
            */
        }
        
        //private SinglePlayerGameViewModel singlePlayerGameViewModel;
        
        private void okButton_Click(object sender, RoutedEventArgs e)
        {
            /*
            // Open maze window.
            new SinglePlayerGameMaze(NumOfRows.Text, numOfCols.Text, MazeNameBox.Text).Show();
            this.Close();
            */
        }
    }
}

