using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GameClient.Model;
using MazeLib;
using SearchAlgorithmsLib;
using System.Threading;

namespace GameClient.ViewModel
{
    /// <summary>
    /// Single player viewModel.
    /// </summary>
    public class SinglePlayerGameViewModel : INotifyPropertyChanged
    {
        /// <summary>
        /// Single player model reference.
        /// </summary>
        private ISinglePlayerGame singlePlayerModel;

        /// <summary>
        /// Settings model reference.
        /// </summary>
        private ISettingsViewModel settingsViewModel;

        /// <summary>
        /// Maze
        /// </summary>
        private Maze maze;

        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="singlePlayerModel">Model.</param>
        /// <param name="settingsViewModel">Settings viewModel.</param>
        public SinglePlayerGameViewModel(ISinglePlayerGame singlePlayerModel,
            ISettingsViewModel settingsViewModel)
        {
            this.singlePlayerModel = singlePlayerModel;
            this.settingsViewModel = settingsViewModel;

            this.singlePlayerModel.ConnectionLost += HandleConnectionLost;

            //Set property changed delegate.
            this.singlePlayerModel.PropertyChanged +=
                delegate(Object sender, PropertyChangedEventArgs e)
                {
                    NotifyPropertyChanged("VM_" + e.PropertyName);
                };

            this.singlePlayerModel.EnableControls += HandleEnable;
        }

        /// <summary>
        /// Property changed event.
        /// </summary>
        public event PropertyChangedEventHandler PropertyChanged;

        /// <summary>
        /// Solution call event.
        /// </summary>
        public event EventHandler SolutionCall;

        /// <summary>
        /// Maze property.
        /// </summary>
        public String VM_Maze
        {
            get
            {
                return this.singlePlayerModel.Maze.ToString()
                    .Replace("*", "0")
                    .Replace("#", "0")
                    .Replace("\r\n", "");
            }
        }

        /// <summary>
        /// Solution property.
        /// </summary>
        public string VM_Solution
        {
            get { return this.singlePlayerModel.Solution; }
        }

        /// <summary>
        /// Default algorithm property.
        /// </summary>
        public string VM_DefaultAlgorithm
        {
            get { return this.settingsViewModel.VM_SelectedAlgo.ToString(); }
        }

        /// <summary>
        /// Initial position property.
        /// </summary>
        public String VM_InitialPostion
        {
            get { return this.singlePlayerModel.Maze.InitialPos.ToString(); }
        }

        /// <summary>
        /// Destination position property.
        /// </summary>
        public String VM_DestPosition
        {
            get { return this.singlePlayerModel.Maze.GoalPos.ToString(); }
        }

        /// <summary>
        /// Player position property.
        /// </summary>
        public String VM_PlayerPosition
        {
            get { return this.singlePlayerModel.PlayerPosition.ToString(); }
            set
            {
                singlePlayerModel.MovePlayer(
                    int.Parse(value.Split(',')[0].Replace("(", "")),
                    int.Parse(value.Split(',')[1].Replace(")", "")));
            }
        }

        /// <summary>
        /// Rows property.
        /// </summary>
        public string VM_Rows
        {
            get { return this.singlePlayerModel.Maze.Rows.ToString(); }
        }

        /// <summary>
        /// Columns property.
        /// </summary>
        public string VM_Cols
        {
            get { return this.singlePlayerModel.Maze.Cols.ToString(); }
        }

        /// <summary>
        /// Default columns property.
        /// </summary>
        public string VM_DefaultNumberCols
        {
            get
            {
                return GameClient.Properties.Settings.Default.DefaultCols
                    .ToString();
            }
        }

        /// <summary>
        /// Default rows property.
        /// </summary>
        public string VM_DefaultNumberRows
        {
            get
            {
                return GameClient.Properties.Settings.Default.DefaultRows
                    .ToString();
            }
        }

        /// <summary>
        /// Maze name property.
        /// </summary>
        public String VM_MazeName
        {
            get { return this.singlePlayerModel.Maze.Name; }
        }

        /// <summary>
        /// Notifies property changed.
        /// </summary>
        /// <param name="propName"></param>
        public void NotifyPropertyChanged(string propName)
        {
            if (this.PropertyChanged != null)
                this.PropertyChanged?.Invoke(this,
                    new PropertyChangedEventArgs(propName));
        }

        /// <summary>
        /// Enable called event.
        /// </summary>
        public event EventHandler EnableCalled;

        /// <summary>
        /// Handels enable called.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public void HandleEnable(object sender, EventArgs e)
        {
            EnableCalled?.Invoke(this, null);
        }

        /// <summary>
        /// Restarts the game.
        /// </summary>
        public void Restart()
        {
            this.singlePlayerModel.Restart();
        }

        /// <summary>
        /// Solves the maze.
        /// </summary>
        /// <param name="name"></param>
        public void SolveMaze(string name)
        {
            this.singlePlayerModel.SolveMaze(name);
        }

        /// <summary>
        /// Creates a new maze.
        /// </summary>
        /// <param name="numOfRows">Rows.</param>
        /// <param name="numOfCols">Columns.</param>
        /// <param name="nameOfMaze">Maze name.</param>
        public void StartNewGame(String numOfRows, String numOfCols,
            String nameOfMaze)
        {
            this.singlePlayerModel.GenerateGame(numOfRows, numOfCols,
                nameOfMaze);
        }

        /// <summary>
        /// Connection lost event.
        /// </summary>
        public event EventHandler ConnectionLost;

        /// <summary>
        /// Handles lost connection.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public void HandleConnectionLost(object sender, EventArgs e)
        {
            this.ConnectionLost?.Invoke(this, null);
        }
    }
}