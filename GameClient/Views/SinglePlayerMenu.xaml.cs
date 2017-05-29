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
using GameClient.ViewModel;
using GameClient.Model;

namespace GameClient.Views
{
    /// <summary>
    /// Interaction logic for SinglePlayerMenu.xaml
    /// </summary>
    public partial class SinglePlayerMenu : Window
    {
        private SinglePlayerGameViewModel singlePlayerGameViewModel;
        private ISettingsModel settingsModel;
        private ISettingsViewModel settingsViewModel;


        public SinglePlayerMenu()
        {
            InitializeComponent();
            settingsModel = new SettingsModel();
            this.singlePlayerGameViewModel = new SinglePlayerGameViewModel
            (new SinglePlayerGameModel(settingsModel),
                new SettingsViewModel(settingsModel));

            this.DataContext = this.singlePlayerGameViewModel;
        }

        private void okButton_Click(object sender, RoutedEventArgs e)
        {
            // Open maze window.
            try
            {
                new SinglePlayerGameMaze(numOfRowsBox.Text, numOfCols.Text,
                    MazeNameBox.Text).Show();
            }
            catch (Exception exception)
            {
                new MainWindow().Show();
            }
            this.Close();

        }
    }
}