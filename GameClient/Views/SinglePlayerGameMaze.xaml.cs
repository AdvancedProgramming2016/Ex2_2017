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

        public SinglePlayerGameMaze(String numOfRows, String numOfCols, String nameOfMaze)
        {
            InitializeComponent();
            this.gameName = nameOfMaze;
            ISettingsModel settingsModel = new SettingsModel();
            this.spViewModel = new SinglePlayerGameViewModel
                (new SinglePlayerGameModel(settingsModel), new SettingsViewModel(settingsModel));
            this.DataContext = this.spViewModel;
            this.spViewModel.StartNewGame(numOfRows, numOfCols, nameOfMaze);
            this.spViewModel.SolutionCall += AnimationCaller;
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

        private void MazeBoard_Loaded(object sender, RoutedEventArgs e)
        {

        }

        private void solveButton_Click(object sender, RoutedEventArgs e)
        {
            this.spViewModel.SolveMaze(gameName);
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            MazeBoard.Focus();
        }

        private void AnimationCaller(object sender, EventArgs e)
        {
            MazeBoard.RunAnimation(this.spViewModel.VM_Solution);
        }
    }
}
