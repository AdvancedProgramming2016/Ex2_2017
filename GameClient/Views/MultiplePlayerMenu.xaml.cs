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
        /// <summary>
        /// Multiplayer menu viewModel.
        /// </summary>
        private MultiPlayerMenuViewModel multiPlayerMenuViewModel;

        /// <summary>
        /// Settings model.
        /// </summary>
        private ISettingsModel settingsModel;

        /// <summary>
        /// Settings viewModel.
        /// </summary>
        private ISettingsViewModel settingsViewModel;

        /// <summary>
        /// Games list.
        /// </summary>
        private ObservableCollection<string> listOfGames;

        /// <summary>
        /// Maze.
        /// </summary>
        private Maze maze;

        /// <summary>
        /// Checks if list was requested.
        /// </summary>
        private bool listPressed;

        /// <summary>
        /// Constructor.
        /// </summary>
        public MultiplePlayerMenu()
        {
            InitializeComponent();

            //Initialize members.
            this.multiPlayerMenuViewModel = new MultiPlayerMenuViewModel();

            this.multiPlayerMenuViewModel.ConnectionLost +=
                HandleConnectionLost;

            //Set data context.
            this.DataContext = this.multiPlayerMenuViewModel;
            listPressed = false;
        }

        /// <summary>
        /// Start game click.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void startGame_Click(object sender, RoutedEventArgs e)
        {
            //Start loading screen.
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

        /// <summary>
        /// Handles lost connection.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public void HandleConnectionLost(object sender, EventArgs e)
        {
            if (listPressed)
            {
                MessageBox.Show("Connection lost.", "Error",
                    MessageBoxButton.OK,
                    MessageBoxImage.Information);

                new MainWindow().Show();
                this.Close();
            }
        }

        /// <summary>
        /// Join click.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void joinGameButton_Click(object sender, RoutedEventArgs e)
        {
            string gameName = listOfGameComboBox.SelectionBoxItem.ToString();

            new MultiplePlayerGameMaze(gameName).Show();

            this.Close();
        }

        /// <summary>
        /// Games list droped dowm.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void listOfGameComboBox_DropDownOpened(object sender,
            EventArgs e)
        {
            listPressed = true;
            this.multiPlayerMenuViewModel.GetGameList();
        }

        /// <summary>
        /// Cancel click.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void CancelBtn_Click(object sender, RoutedEventArgs e)
        {
            MainWindow mainWindow = new MainWindow();
            mainWindow.Show();
            this.Close();
        }
    }
}