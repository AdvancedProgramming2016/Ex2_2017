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
using GameClient.Model;
using GameClient.ViewModel;
using System.Collections.ObjectModel;
using MazeLib;

namespace GameClient.Views
{
    /// <summary>
    /// Interaction logic for MultiplePlayerMenu.xaml
    /// </summary>
    public partial class MultiplePlayerMenu : Window
    {

        private MultiPlayerMenuViewModel multiPlayerMenuViewModel;
        private ISettingsModel settingsModel;
        private ISettingsViewModel settingsViewModel;
        private ObservableCollection<string> listOfGames;
        private Maze maze;

        public MultiplePlayerMenu()
        {
            InitializeComponent();
            //this.listOfGames = ObservableCollection<string>(); Might need to initialize the collection
            this.multiPlayerMenuViewModel = new MultiPlayerMenuViewModel();
            //this.settingsModel = new SettingsModel();
            //this.multiPlayerGameViewModel = new MultiPlayerGameViewModel
            //    (new MultiPlayerModel(settingsModel), new SettingsViewModel(settingsModel));
            this.DataContext = this.multiPlayerMenuViewModel;
        }

        private void startGame_Click(object sender, RoutedEventArgs e)
        {
            // Open maze window.
            // this.multiPlayerMenuViewModel.AddGameToList(mazeNameBox.Text);
            this.multiPlayerMenuViewModel.StartNewGame(mazeNameBox.Text,
                mazeRowsBox.Text, mazeColsBox.Text);

            Maze maze = this.multiPlayerMenuViewModel.MultiPlayerMenuModel.Maze;
             new MultiplePlayerGameMaze(maze, this.multiPlayerMenuViewModel.MultiPlayerMenuModel.CommunicationClient).Show();
          
            this.Close();
        }
        
        private void joinGameButton_Click(object sender, RoutedEventArgs e)
        {
            string nameOfGame = listOfGameComboBox.SelectionBoxItem.ToString();
            this.multiPlayerMenuViewModel.JoinGame(nameOfGame);

            Maze maze = this.multiPlayerMenuViewModel.MultiPlayerMenuModel.Maze;
            new MultiplePlayerGameMaze(maze, this.multiPlayerMenuViewModel.MultiPlayerMenuModel.CommunicationClient).Show();

            this.Close();
        }

        private void listOfGameComboBox_DropDownOpened(object sender, EventArgs e)
        {
            this.multiPlayerMenuViewModel.GetGameList();
        }
    }
}
