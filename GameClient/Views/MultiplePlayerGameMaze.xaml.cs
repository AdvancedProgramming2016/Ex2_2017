﻿using System;
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
        private CommunicationClient communicationClient;
        private MazeGrid rightMazeGrid;
        private string gameName;

        public MultiplePlayerGameMaze(string gameName, string rows,
            string columns)
        {
            //this.gameName = maze.Name;
            this.communicationClient = new CommunicationClient();
            ISettingsModel settingsModel = new SettingsModel();
            this.communicationClient.Connect(settingsModel.Port,
                settingsModel.IpAddress);
            this.multiPlayerGameViewModel = new MultiPlayerGameViewModel
            (new MultiPlayerModel(settingsModel, communicationClient),
                new SettingsViewModel(settingsModel));

            this.multiPlayerGameViewModel.ConnectionLost +=
                HandleConnectionLost;

            try
            {
                this.multiPlayerGameViewModel.StartGame(gameName, rows, columns);
            }
            catch (Exception e)
            {
               throw new Exception();
            }
          

            /*  this.multiPlayerGameViewModel.VM_Maze = maze.ToString();
              this.multiPlayerGameViewModel.VM_Cols = maze.Cols.ToString();
              this.multiPlayerGameViewModel.VM_Rows = maze.Rows.ToString();
              this.multiPlayerGameViewModel.VM_InitialPosition =
                  maze.InitialPos.ToString();
              this.multiPlayerGameViewModel.VM_DestPosition =
                  maze.GoalPos.ToString();
              this.multiPlayerGameViewModel.VM_MazeName = maze.Name;*/

            //InitializeComponent();

            this.multiPlayerGameViewModel.OpponentExitCalled +=
                HandleExitCalled;
            this.multiPlayerGameViewModel.OpponentDirectionCalled +=
                HandleDirectionCalled;
            // LeftMaze.PlayerMoved += PlayerMovedHandler;
            rightMazeGrid = RightMaze;

            this.DataContext = this.multiPlayerGameViewModel;

            // RightMaze.MultiPlayerGameVM = this.multiPlayerGameViewModel;

            //this.StartNewGame(numOfRows, numOfCols, nameOfMaze);
        }

        public MultiplePlayerGameMaze(string gameName)
        {
            this.communicationClient = new CommunicationClient();
            ISettingsModel settingsModel = new SettingsModel();
            this.communicationClient.Connect(settingsModel.Port,
                settingsModel.IpAddress);
            this.multiPlayerGameViewModel = new MultiPlayerGameViewModel
            (new MultiPlayerModel(settingsModel, communicationClient),
                new SettingsViewModel(settingsModel));

            this.multiPlayerGameViewModel.JoinGame(gameName);

            //InitializeComponent();

            this.multiPlayerGameViewModel.OpponentExitCalled +=
                HandleExitCalled;
            /*  this.multiPlayerGameViewModel.OpponentDirectionCalled +=
                  HandleDirectionCalled;*/
            //LeftMaze.PlayerMoved += PlayerMovedHandler;
            //rightMazeGrid = RightMaze;

            this.DataContext = this.multiPlayerGameViewModel;
        }

        protected override void OnInitialized(EventArgs e)
        {
            InitializeComponent();
            base.OnInitialized(e);
        }


        /*  private void PlayerMovedHandler(object sender, EventArgs e)
          {
              this.multiPlayerGameViewModel.MovePlayer(LeftMaze.DirectionMoved);
          }*/

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

            /*  RightMaze.CalculateNewPosition(direction,
                  this.RightMaze.PlayerPosition);*/
        }

        public void HandleConnectionLost(object sender, EventArgs e)
        {
            MessageBox.Show("Connection lost.");
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