using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MazeMenu.Model;
using MazeLib;

namespace MazeMenu.ViewModel
{
    public class SinglePlayerGameViewModel : INotifyPropertyChanged
    {
        private ISinglePlayerGame singlePlayerModel;
        private ISettingsModel settingsModel;
        private string vm_Maze;

        public SinglePlayerGameViewModel(ISinglePlayerGame singlePlayerModel,
            ISettingsModel settingsModel)
        {
            this.singlePlayerModel = singlePlayerModel;
            this.settingsModel = settingsModel;

            this.singlePlayerModel.PropertyChanged +=
                delegate (Object sender, PropertyChangedEventArgs e)
                {
                    NotifyPropertyChanged("VM_" + e.PropertyName);
                };
        }

        public event PropertyChangedEventHandler PropertyChanged;

        public String VM_Maze
        {
            get
            {
                return this.singlePlayerModel.Maze.ToString()
                    .Replace("\r\n", "")
                    .Replace("*", "0");
            }

            set
            {
                vm_Maze = value;
                NotifyPropertyChanged("VM_Maze");
            }
        }

        public int VM_DefaultAlgorithm
        {
            get { return this.settingsModel.DefaultAlgo; }
        }

        public String VM_InitialPostion
        {
            get { return this.singlePlayerModel.Maze.InitialPos.ToString(); }
        }

        public String VM_DestPosition
        {
            get { return this.singlePlayerModel.Maze.GoalPos.ToString(); }
        }

       /* public String VM_PlayerPosition
        {
            get { return this.singlePlayerModel.PlayerPosition.ToString(); }
        }*/

        public int VM_Rows
        {
            get { return this.singlePlayerModel.Maze.Rows; }
        }

        public int VM_Cols
        {
            get { return this.singlePlayerModel.Maze.Cols; }
        }

        public String VM_MazeName
        {
            get { return this.singlePlayerModel.Maze.Name; }
        }

        public void NotifyPropertyChanged(string propName)
        {
            if (this.PropertyChanged != null)
                this.PropertyChanged?.Invoke(this,
                    new PropertyChangedEventArgs(propName));
        }

       /* public void MovePlayer()
        {
            this.singlePlayerModel.MovePlayer();
        }*/

        public void Restart()
        {
            this.singlePlayerModel.Restart();
        }

        public void SolveMaze()
        {
            this.singlePlayerModel.SolveMaze();
        }

        public void StartNewGame(String numOfCols, String numOfRows,
            String nameOfMaze)
        {
            this.singlePlayerModel.GenerateGame(numOfRows, numOfCols, nameOfMaze);
        }
    }
}