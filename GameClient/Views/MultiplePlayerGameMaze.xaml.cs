using System;
using System.Collections.Generic;
using System.ComponentModel;
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
using MazeLib;

namespace GameClient.Views
{
    /// <summary>
    /// Interaction logic for MultiplePlayerGameMaze.xaml
    /// </summary>
    public partial class MultiplePlayerGameMaze : Window
    {
        /// <summary>
        /// Multiplayer viewModel reference.
        /// </summary>
        private MultiPlayerGameViewModel multiPlayerGameViewModel;

        /// <summary>
        /// Communication client.
        /// </summary>
        private CommunicationClient communicationClient;

        /// <summary>
        /// Game name.
        /// </summary>
        private string gameName;

        /// <summary>
        /// Checks if game is on.
        /// </summary>
        private bool isGameOn;

        /// <summary>
        /// Constructor,
        /// </summary>
        /// <param name="gameName">Game name.</param>
        /// <param name="rows">Rows.</param>
        /// <param name="columns">Columns.</param>
        public MultiplePlayerGameMaze(string gameName, string rows,
            string columns)
        {
            //Initialize members.
            this.communicationClient = new CommunicationClient();
            ISettingsModel settingsModel = new SettingsModel();
            this.communicationClient.Connect(settingsModel.Port,
                settingsModel.IpAddress);
            this.multiPlayerGameViewModel = new MultiPlayerGameViewModel
            (new MultiPlayerModel(settingsModel, communicationClient),
                new SettingsViewModel(settingsModel));

            this.multiPlayerGameViewModel.ConnectionLost +=
                HandleConnectionLost;

            this.multiPlayerGameViewModel.OpponentWon += HandleOpponentWon;

            this.multiPlayerGameViewModel.ReachedGoal += HandleReachedGoal;

            try
            {
                this.multiPlayerGameViewModel.StartGame(gameName, rows,
                    columns);
            }
            catch (Exception e)
            {
                throw new Exception();
            }

            this.multiPlayerGameViewModel.OpponentExitCalled +=
                HandleExitCalled;
            this.multiPlayerGameViewModel.OpponentDirectionCalled +=
                HandleDirectionCalled;

            //Set data context.
            this.DataContext = this.multiPlayerGameViewModel;
        }

        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="gameName">Game name.</param>
        public MultiplePlayerGameMaze(string gameName)
        {
            //Initialize members.
            this.communicationClient = new CommunicationClient();
            ISettingsModel settingsModel = new SettingsModel();
            this.communicationClient.Connect(settingsModel.Port,
                settingsModel.IpAddress);
            this.multiPlayerGameViewModel = new MultiPlayerGameViewModel
            (new MultiPlayerModel(settingsModel, communicationClient),
                new SettingsViewModel(settingsModel));

            this.multiPlayerGameViewModel.JoinGame(gameName);

            this.multiPlayerGameViewModel.OpponentExitCalled +=
                HandleExitCalled;

            this.multiPlayerGameViewModel.ReachedGoal += HandleReachedGoal;

            this.multiPlayerGameViewModel.OpponentWon += HandleOpponentWon;

            this.DataContext = this.multiPlayerGameViewModel;
        }

        /// <summary>
        /// On initialize event.
        /// </summary>
        /// <param name="e"></param>
        protected override void OnInitialized(EventArgs e)
        {
            this.isGameOn = true;
            InitializeComponent();
            base.OnInitialized(e);
        }

        /// <summary>
        /// Go back to main menu.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BackMainMenuButton_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        /// <summary>
        /// Window loaded event.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            LeftMaze.Focus();
        }

        /// <summary>
        /// Handles exit called.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void HandleExitCalled(object sender, EventArgs e)
        {
            isGameOn = false;

            MessageBox.Show("Opponent exited the game.", "Game ended",
                MessageBoxButton.OK,
                MessageBoxImage.Information);
            try
            {
                this.Close();
            }
            catch (Exception exception)
            {
                Console.WriteLine(exception.InnerException);
            }
        }

        /// <summary>
        /// Handles direction called.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void HandleDirectionCalled(object sender, EventArgs e)
        {
            string direction = this.multiPlayerGameViewModel
                .VM_OpponentPosition;
        }

        /// <summary>
        /// Handles opponent won.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public void HandleOpponentWon(object sender, EventArgs e)
        {
            //  MessageBox.Show("You lost.");
        }

        /// <summary>
        /// Handles connection lost.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public void HandleConnectionLost(object sender, EventArgs e)
        {
            MessageBox.Show("Connection lost.", "Error", MessageBoxButton.OK,
                MessageBoxImage.Information);
            try
            {
                this.Close();
                new MainWindow().Show();
            }
            catch (Exception exception)
            {
            }
        }

        /// <summary>
        /// Handles goal reached.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public void HandleReachedGoal(object sender, EventArgs e)
        {
            //  MessageBox.Show("You won.");
        }

        /// <summary>
        /// On closing event.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void MultiplePlayerGameMaze_OnClosing(object sender,
            CancelEventArgs e)
        {
            if (isGameOn)
            {
                this.multiPlayerGameViewModel.CloseGame(gameName);
            }

            MainWindow mw = new MainWindow();
            mw.Show();
        }
    }
}