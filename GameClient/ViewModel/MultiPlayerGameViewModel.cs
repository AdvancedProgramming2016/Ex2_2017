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
        private Maze maze;
        private string vm_Maze;
        private string vm_PlayerPosition;
        private string vm_opponentPosition;
        private string vm_mazeName;
        private string vm_algorithmType;
        private string vm_initialPosition;
        private string vm_destPosition;
        private string vm_rows;
        private string vm_cols;

        public MultiPlayerGameViewModel(IMultiPlayerGame model,
            ISettingsViewModel settingsViewModel)
        {
            this.settingsViewModel = settingsViewModel;
            this.mpModel = model;
            model.PropertyChanged +=
                delegate(Object sender, PropertyChangedEventArgs e)
                {
                    NotifyPropertyChanged("VM_" + e.PropertyName);
                };
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
                return DivideMazeToCommas(this.mpModel.Maze);
            }
            set
            {
                this.vm_Maze = value;
                NotifyPropertyChanged("VM_Maze");
            }
        }

        private string DivideMazeToCommas(Maze maze)
        {
            string str = maze.ToString();
            string finalString = string.Empty;

            finalString = str.Replace("\r\n", ",");
            finalString = finalString.Remove(finalString.Length - 1);

            return finalString;
        }

        public string VM_DefaultNumRows
        {
            get
            {
                return GameClient.Properties.Settings.Default.DefaultRows.ToString();
            }
        }

        public string VM_DefaultNumCols
        {
            get
            {
                return GameClient.Properties.Settings.Default.DefaultCols.ToString();
            }
        }

       public String VM_OpponentPosition
        {
            get { return this.mpModel.OpponentPosition.ToString(); }
           set
           {
               this.vm_opponentPosition = value;
               NotifyPropertyChanged("VM_OpponentPosition");
           }
        }

        public String VM_PlayerPosition
        {
            get { return this.mpModel.PlayerPosition.ToString(); }
            set
            {
                this.vm_PlayerPosition = value;
                NotifyPropertyChanged("VM_PlayerPosition");
            }
        }

        public string VM_Rows
        {
            get { return this.mpModel.Maze.Rows.ToString(); }
            set
            {
                this.vm_rows = value;
                NotifyPropertyChanged("VM_Rows");
            }
        }

        public string VM_Cols
        {
            get { return this.mpModel.Maze.Cols.ToString(); }
            set
            {
                this.vm_cols = value;
                NotifyPropertyChanged("VM_Cols");
            }
        }

        public String VM_MazeName
        {
            get { return this.mpModel.Maze.Name; }
            set
            {
                this.vm_mazeName = value;
                NotifyPropertyChanged("VM_MazeName");
            }
        }

        public String VM_InitialPosition
        {
            get { return this.mpModel.Maze.InitialPos.ToString(); }
            set
            {
                this.vm_initialPosition = value;
                NotifyPropertyChanged("VM_InitialPostion");
            }
        }

        public String VM_DestPosition
        {
            get { return this.mpModel.Maze.GoalPos.ToString(); }
            set
            {
                this.vm_destPosition = value;
                NotifyPropertyChanged("VM_DestPosition");
            }
        }

        public void MovePlayer(Position position)
        {
            this.mpModel.MovePlayer(position);
        }

        public void JoinGame(string gameName)
        {
            this.mpModel.JoinGame(gameName);
        }

        public void StartNewGame(string numOfRows, string numOfCols,
            string nameOfMaze)
        {
            this.mpModel.StartNewGame(numOfRows, numOfCols,
                nameOfMaze);
        }

        public void CloseGame()
        {
            this.mpModel.CloseGame();
        }
    }
}