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
using GameClient.Model.Listeners;
using SearchAlgorithmsLib;

namespace GameClient.Views
{
    /// <summary>
    /// Interaction logic for SinglePlayerGameMaze.xaml
    /// </summary>
    public partial class SinglePlayerGameMaze : Window
    {
        private SinglePlayerGameViewModel spViewModel;
        private string gameName;
        private CommunicationClient communicationClient;

        public SinglePlayerGameMaze(String numOfRows, String numOfCols,
            String nameOfMaze)
        {
            //InitializeComponent();
            this.gameName = nameOfMaze;
            this.communicationClient = new CommunicationClient();
            ISettingsModel settingsModel = new SettingsModel();
            this.communicationClient.Connect(settingsModel.Port,
                settingsModel.IpAddress);
            this.spViewModel = new SinglePlayerGameViewModel
            (new SinglePlayerGameModel(settingsModel),
                new SettingsViewModel(settingsModel));
            this.spViewModel.ConnectionLost +=
                HandleConnectionLost;

            this.spViewModel.StartNewGame(numOfRows, numOfCols, nameOfMaze);
            this.DataContext = this.spViewModel;
            this.spViewModel.EnableCalled += HandleEnable;
            // this.spViewModel.SolutionCall += AnimationCaller;
        }

        protected override void OnInitialized(EventArgs e)
        {
            InitializeComponent();
            base.OnInitialized(e);
        }

        /// <summary>
        /// Opens the Main menu when clicked.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void menuButton_Click(object sender, RoutedEventArgs e)
        {
            MainWindow mw = new MainWindow();
            mw.Show();
            this.Close();
        }

        private void solveButton_Click(object sender, RoutedEventArgs e)
        {
            this.spViewModel.SolveMaze(gameName);
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            MazeBoard.Focus();
        }

        private void restartButton_Click(object sender, RoutedEventArgs e)
        {
            this.spViewModel.Restart();
        }

        public void HandleEnable(object sender, EventArgs e)
        {
            this.Dispatcher.Invoke(() =>
            {
                this.restartButton.IsEnabled = !this.restartButton.IsEnabled;

                this.solveButton.IsEnabled = !this.solveButton.IsEnabled;

                this.menuButton.IsEnabled = !this.menuButton.IsEnabled;
            });
        }

        public void HandleConnectionLost(object sender, EventArgs e)
        {
            MessageBox.Show("Connection lost.", "Error", MessageBoxButton.OK,
                MessageBoxImage.Information);
            try
            {
                this.Close();
            }
            catch (Exception exception)
            {
            }
        }
    }
}