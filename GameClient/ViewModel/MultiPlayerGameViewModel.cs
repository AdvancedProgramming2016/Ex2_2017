using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;
using MazeMenu.Model;
using MazeLib;

namespace MazeMenu.ViewModel
{
    public class MultiPlayerGameViewModel : INotifyPropertyChanged
    {
        
        private IMultiPlayerGame mpModel;
        private ISettingsModel settingsModel;

        public event PropertyChangedEventHandler PropertyChanged;

        public MultiPlayerGameViewModel(IMultiPlayerGame model)
        {
            this.mpModel = model;
            model.PropertyChanged += delegate(Object sender, PropertyChangedEventArgs e)
            {
                OnPropertyChanged("VM_" + e.PropertyName);
            };
        }

        protected void OnPropertyChanged(string propertyName)
        {
            if (PropertyChanged != null) // if there is any subscribers 
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
        }

        public String VM_Maze
        {
            get
            {
                return this.mpModel.Maze.ToString().Replace("\r\n", "").Replace("*", "0");
            }
        }

        public String VM_OpponentPosition
        {
            get
            {
                return this.mpModel.OpponentPosition.ToString();
            }
        }

        public String VM_PlayerPosition
        {
            get
            {
                return this.mpModel.PlayerPosition.ToString();
            }
        }

        public int VM_Rows
        {
            get
            {
                return this.mpModel.Maze.Rows;
            }
         }

        public int VM_Cols
        {
            get
            {
                return this.mpModel.Maze.Cols;
            }
        }

        public String VM_MazeName
        {
            get
            {
                return this.mpModel.Maze.Name;
            }
        }

        public String VM_InitialPosition
        {
            get
            {
                return this.mpModel.Maze.InitialPos.ToString();
            }
        }

        public String VM_DestPosition
        {
            get
            {
                return this.mpModel.Maze.GoalPos.ToString();
            }
        }

        public void MovePlayer(Position position)
        {
            this.mpModel.MovePlayer(position);
        }

        public void JoinGame()
        {
            this.mpModel.JoinGame();
        }

        public void StartNewGame()
        {
            this.mpModel.StartNewGame();
        }
        
        public void CloseGame()
        {
            this.mpModel.CloseGame();
        }
    }
}
