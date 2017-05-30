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
    public class MultiPlayerGameViewModel : INotifyPropertyChanged
    {
        private IMultiPlayerGame mpModel;
        private ISettingsViewModel settingsViewModel;      

        public MultiPlayerGameViewModel(IMultiPlayerGame model,
            ISettingsViewModel settingsViewModel)
        {
            this.settingsViewModel = settingsViewModel;
            this.mpModel = model;
            this.mpModel.ConnectionLost += HandleConnecionLost;
            this.mpModel.ReachedDestination += HandleReachedGoal;
          //  this.mpModel.OpponentWon += HandleOpponentWon;
            model.PropertyChanged +=
                delegate(Object sender, PropertyChangedEventArgs e)
                {
                    NotifyPropertyChanged("VM_" + e.PropertyName);
                };

            model.ExitCalled += HandlExitCalled;
            // model.DirectionCalled += HandleDirectionChanged;
        }

        public event PropertyChangedEventHandler PropertyChanged;

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
            /* set
             {
                 this.vm_Maze = value;
                 NotifyPropertyChanged("VM_Maze");
             }*/
        }

        /* private string DivideMazeToCommas(string maze)
         {
             string str = maze;
             string finalString = string.Empty;
 
             finalString = str.Replace("\r\n", ",");
             finalString = finalString.Remove(finalString.Length - 1);
 
             return finalString;
         }*/

        /*   public string VM_DefaultNumRows
           {
               get
               {
                   return GameClient.Properties.Settings.Default.DefaultRows
                       .ToString();
               }
           }*/

        /* public string VM_DefaultNumCols
         {
             get
             {
                 return GameClient.Properties.Settings.Default.DefaultCols
                     .ToString();
             }
         }*/

        public event EventHandler OpponentWon;

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
            /* set
             {
                 this.vm_opponentPosition = value;
                 // NotifyPropertyChanged("VM_OpponentPosition");
                 OpponentDirectionCalled?.Invoke(this, null);
             }*/
        }

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
            /*  set
              {
                  this.vm_rows = value;
                  NotifyPropertyChanged("VM_Rows");
              }*/
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

        public String VM_MazeName
        {
            get { return this.mpModel.Maze.Name; }
            /*   set
               {
                   this.vm_mazeName = value;
                   NotifyPropertyChanged("VM_MazeName");
               }*/
        }

        public String VM_InitialPosition
        {
            get { return this.mpModel.Maze.InitialPos.ToString(); }
            /*  set
              {
                  this.vm_initialPosition = value;
                  NotifyPropertyChanged("VM_InitialPostion");
              }*/
        }

        public String VM_DestPosition
        {
            get { return this.mpModel.Maze.GoalPos.ToString(); }
            /*  set
              {
                  this.vm_destPosition = value;
                  NotifyPropertyChanged("VM_DestPosition");
              }*/
        }

        public event EventHandler OpponentExitCalled;

        private void HandlExitCalled(object sender, EventArgs e)
        {
            OpponentExitCalled?.Invoke(this, null);
        }

        public event EventHandler OpponentDirectionCalled;

        /* private void HandleDirectionChanged(object sender, EventArgs e)
         {
             VM_OpponentPosition = this.mpModel.OpponentPosition;
            }*/

       /* public void MovePlayer(String position)
        {
            this.mpModel.MovePlayer(position);
        }*/

        public void CloseGame(string gameName)
        {
            this.mpModel.CloseGame(gameName);
        }

        public void StartGame(string gameName, string rows, string columns)
        {
            this.mpModel.StartGame(gameName, rows, columns);
        }

        public void JoinGame(string gameName)
        {
            this.mpModel.JoinGame(gameName);
        }

        public event EventHandler ConnectionLost;

        public void HandleConnecionLost(object sender, EventArgs e)
        {
            this.ConnectionLost?.Invoke(this, null);
        }

        public event EventHandler ReachedGoal;

        public void HandleReachedGoal(object sender, EventArgs e)
        {
            this.ReachedGoal?.Invoke(this, null);
        }

       // public void HandleOpponentWon(object sender)
    }
}