using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;
using GameClient.Model;
using MazeLib;

namespace GameClient.ViewModel
{
    /// <summary>
    /// Multiplayer viewModel.
    /// </summary>
    public class MultiPlayerGameViewModel : INotifyPropertyChanged
    {
        /// <summary>
        /// Multiplayer model reference.
        /// </summary>
        private readonly IMultiPlayerGame mpModel;

        /// <summary>
        /// Constructor 
        /// </summary>
        /// <param name="model">Model.</param>
        /// <param name="settingsViewModel">Settings viewModel.</param>
        public MultiPlayerGameViewModel(IMultiPlayerGame model,
            ISettingsViewModel settingsViewModel)
        {
            //Set members.
            this.mpModel = model;
            this.mpModel.ConnectionLost += HandleConnecionLost;
            this.mpModel.ReachedDestination += HandleReachedGoal;

            //Set property changed delefate.
            model.PropertyChanged +=
                delegate(Object sender, PropertyChangedEventArgs e)
                {
                    NotifyPropertyChanged("VM_" + e.PropertyName);
                };

            model.ExitCalled += HandlExitCalled;
        }

        /// <summary>
        /// Property changed event.
        /// </summary>
        public event PropertyChangedEventHandler PropertyChanged;

        /// <summary>
        /// Notifies property changed.
        /// </summary>
        /// <param name="propertyName"></param>
        public void NotifyPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this,
                new PropertyChangedEventArgs(propertyName));
        }

        public String VM_Maze
        {
            get
            {
                return mpModel.Maze.ToString()
                    .Replace("*", "0")
                    .Replace("#", "0")
                    .Replace("\r\n", "");
            }
        }

        /// <summary>
        /// Opponent won event.
        /// </summary>
        public event EventHandler OpponentWon;

        /// <summary>
        /// Opponent position property.
        /// </summary>
        public String VM_OpponentPosition
        {
            get
            {
                if (mpModel.OpponentPosition.Col == mpModel.Maze.GoalPos.Col &&
                    mpModel.OpponentPosition.Row == mpModel.Maze.GoalPos.Row)
                {
                    this.OpponentWon?.Invoke(this, null);
                }
                return this.mpModel.OpponentPosition.ToString();
            }
        }

        /// <summary>
        /// Player position property.
        /// </summary>
        public String VM_PlayerPosition
        {
            get { return this.mpModel.PlayerPosition.ToString(); }
            set
            {
                this.mpModel.MovePlayer(
                    int.Parse(value.Split(',')[0].Replace("(", "")),
                    int.Parse(value.Split(',')[1].Replace(")", "")));
            }
        }

        public int VM_Rows
        {
            get { return this.mpModel.Maze.Rows; }
        }

        public int VM_Cols
        {
            get { return this.mpModel.Maze.Cols; }
            /*  set
              {
                  this.vm_cols = value;
                  NotifyPropertyChanged("VM_Cols");
              }*/
        }

        /// <summary>
        /// Maze name property.
        /// </summary>
        public String VM_MazeName
        {
            get { return this.mpModel.Maze.Name; }
        }

        /// <summary>
        /// Initial position property.
        /// </summary>
        public String VM_InitialPosition
        {
            get { return this.mpModel.Maze.InitialPos.ToString(); }
        }

        /// <summary>
        /// Destination property.
        /// </summary>
        public String VM_DestPosition
        {
            get { return this.mpModel.Maze.GoalPos.ToString(); }
        }

        /// <summary>
        /// Opponent exit called event.
        /// </summary>
        public event EventHandler OpponentExitCalled;

        /// <summary>
        /// Handles exit called.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void HandlExitCalled(object sender, EventArgs e)
        {
            OpponentExitCalled?.Invoke(this, null);
        }

        /// <summary>
        /// Opponent direction called event.
        /// </summary>
        public event EventHandler OpponentDirectionCalled;

        /// <summary>
        /// Closes the game.
        /// </summary>
        /// <param name="gameName">Game name.</param>
        public void CloseGame(string gameName)
        {
            this.mpModel.CloseGame(gameName);
        }

        /// <summary>
        /// Starts the game.
        /// </summary>
        /// <param name="gameName">Game name.</param>
        /// <param name="rows">Rows.</param>
        /// <param name="columns">Columns.</param>
        public void StartGame(string gameName, string rows, string columns)
        {
            this.mpModel.StartGame(gameName, rows, columns);
        }

        /// <summary>
        /// Joins the game.
        /// </summary>
        /// <param name="gameName">Game name.</param>
        public void JoinGame(string gameName)
        {
            this.mpModel.JoinGame(gameName);
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
        public void HandleConnecionLost(object sender, EventArgs e)
        {
            this.ConnectionLost?.Invoke(this, null);
        }

        /// <summary>
        /// Reached goal event.
        /// </summary>
        public event EventHandler ReachedGoal;

        /// <summary>
        /// Handles reached goal.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public void HandleReachedGoal(object sender, EventArgs e)
        {
            this.ReachedGoal?.Invoke(this, null);
        }
    }
}