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
using MazeLib;

namespace GameClient.Views
{
    /// <summary>
    /// Interaction logic for MultiplePlayerGameMaze.xaml
    /// </summary>
    public partial class MultiplePlayerGameMaze : Window
    {
        private MultiPlayerGameViewModel multiPlayerGameViewModel;
        private MazeGrid rightMazeGrid;
        private string gameName;

        public MultiplePlayerGameMaze(Maze maze,
            CommunicationClient communicationClient)
        {
            
           
           
            this.gameName = maze.Name;
            ISettingsModel settingsModel = new SettingsModel();
            this.multiPlayerGameViewModel = new MultiPlayerGameViewModel
            (new MultiPlayerModel(settingsModel, communicationClient),
                new SettingsViewModel(settingsModel));

            this.multiPlayerGameViewModel.VM_Maze = maze.ToString();
            this.multiPlayerGameViewModel.VM_Cols = maze.Cols.ToString();
            this.multiPlayerGameViewModel.VM_Rows = maze.Rows.ToString();
            this.multiPlayerGameViewModel.VM_InitialPosition =
                maze.InitialPos.ToString();
            this.multiPlayerGameViewModel.VM_DestPosition =
                maze.GoalPos.ToString();
            this.multiPlayerGameViewModel.VM_MazeName = maze.Name;

            InitializeComponent();

            this.multiPlayerGameViewModel.OpponentExitCalled +=
                HandleExitCalled;
            this.multiPlayerGameViewModel.OpponentDirectionCalled +=
                HandleDirectionCalled;
            LeftMaze.PlayerMoved += PlayerMovedHandler;
            rightMazeGrid = RightMaze;

            this.DataContext = this.multiPlayerGameViewModel;
            /*
            while(!RightMaze.IsLoaded)
            {
                continue;
            }
            */
            // RightMaze.MultiPlayerGameVM = this.multiPlayerGameViewModel;


            //this.StartNewGame(numOfRows, numOfCols, nameOfMaze);
        }

        private void PlayerMovedHandler(object sender, EventArgs e)
        {
            this.multiPlayerGameViewModel.MovePlayer(LeftMaze.DirectionMoved);
        }

        /// <summary>
        /// Go back to main menu.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BackMainMenuButton_Click(object sender, RoutedEventArgs e)
        {
            this.multiPlayerGameViewModel.CloseGame(gameName);
            MainWindow mw = new MainWindow();
            mw.Show();
            this.Close();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            LeftMaze.Focus();
        }

        private void HandleExitCalled(object sender, EventArgs e)
        {
            MessageBox.Show("Opponent exited the game.", "Game ended");
            /* MainWindow mw = new MainWindow();
             mw.Show();
             this.Close();*/
        }

        private void HandleDirectionCalled(object sender, EventArgs e)
        {
            string direction = this.multiPlayerGameViewModel
                .VM_OpponentPosition;

            RightMaze.CalculateNewPosition(direction,
                this.RightMaze.PlayerPosition);
        }
    }
}