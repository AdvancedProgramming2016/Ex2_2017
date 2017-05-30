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
        private bool listPressed;

        public MultiplePlayerMenu()
        {
            InitializeComponent();
            //this.listOfGames = ObservableCollection<string>(); Might need to initialize the collection
            this.multiPlayerMenuViewModel = new MultiPlayerMenuViewModel();

            this.multiPlayerMenuViewModel.ConnectionLost +=
                HandleConnectionLost;
           //this.settingsModel = new SettingsModel();
            //this.multiPlayerGameViewModel = new MultiPlayerGameViewModel
            //    (new MultiPlayerModel(settingsModel), new SettingsViewModel(settingsModel));
            this.DataContext = this.multiPlayerMenuViewModel;
            listPressed = false;
        }

        private void startGame_Click(object sender, RoutedEventArgs e)
        {
            // Open maze window.
            // this.multiPlayerMenuViewModel.AddGameToList(mazeNameBox.Text);

            LoadingWindow loadingWindow = new LoadingWindow();
            Application.Current.Dispatcher.Invoke((Action) delegate
            {
                loadingWindow.Show();
            });

            this.Hide();

            try
            {
                new MultiplePlayerGameMaze(mazeNameBox.Text, mazeRowsBox.Text,
                    mazeColsBox.Text).Show();
            }
            catch (Exception exception)
            {
              
            }
         

            loadingWindow.Close();

            this.Close();
        }

        public void HandleConnectionLost(object sender, EventArgs e)
        {
            if (listPressed)
            {
                MessageBox.Show("Connection lost.", "Error", MessageBoxButton.OK,
                    MessageBoxImage.Information);

                new MainWindow().Show();
                this.Close();
            }
          
        }

        private void joinGameButton_Click(object sender, RoutedEventArgs e)
        {
            string gameName = listOfGameComboBox.SelectionBoxItem.ToString();
            //  this.multiPlayerMenuViewModel.JoinGame(gameName);

            // Maze maze = this.multiPlayerMenuViewModel.MultiPlayerMenuModel.Maze;
            new MultiplePlayerGameMaze(gameName).Show();

            this.Close();
        }

        private void listOfGameComboBox_DropDownOpened(object sender,
            EventArgs e)
        {
            listPressed = true;
            this.multiPlayerMenuViewModel.GetGameList();
        }

        private void CancelBtn_Click(object sender, RoutedEventArgs e)
        {
            MainWindow mainWindow = new MainWindow();
            mainWindow.Show();
            this.Close();
        }
    }
}