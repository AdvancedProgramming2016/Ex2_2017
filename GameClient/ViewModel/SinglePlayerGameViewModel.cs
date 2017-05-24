using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GameClient.Model;
using MazeLib;
using SearchAlgorithmsLib;
using System.Threading;

namespace GameClient.ViewModel
{
    public class SinglePlayerGameViewModel : INotifyPropertyChanged
    {
        private ISinglePlayerGame singlePlayerModel;
        private ISettingsModel settingsModel;
        private string vm_Maze;
        private string vm_PlayerPosition;

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
                this.vm_Maze = value;
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

        public String VM_PlayerPosition
        {
            get
            {
                return this.singlePlayerModel.PlayerPosition.ToString();
            }
            set
            {
                this.vm_PlayerPosition = value;
                NotifyPropertyChanged("VM_PlayerPosition");
            }
        }

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
            Solution<String> algoSolution = this.singlePlayerModel.SolveMaze();
            String solution = "00222030"; // Need to change.
            foreach (char movement in solution) // Change so that solution holds the real solution.
            {
                int currPlayerXPosition, currPlayerYPosition;

                currPlayerXPosition = Convert.ToInt32(VM_PlayerPosition.Split(',')[0]);
                currPlayerYPosition = Convert.ToInt32(VM_PlayerPosition.Split(',')[1]);

                // Move the player to the next location.
                switch (movement)
                {
                    case '0':
                        //Move left
                        VM_PlayerPosition = (currPlayerXPosition - 1).ToString() + ',' + currPlayerYPosition.ToString();
                        break;
                    case '1':
                        //Move right
                        VM_PlayerPosition = (currPlayerXPosition + 1).ToString() + ',' + currPlayerYPosition.ToString();
                        break;
                    case '2':
                        //Move up
                        VM_PlayerPosition = currPlayerXPosition.ToString() + ',' + (currPlayerYPosition - 1).ToString();
                        break;
                    case '3':
                        //Move down
                        VM_PlayerPosition = currPlayerXPosition.ToString() + ',' + (currPlayerYPosition + 1).ToString();
                        break;
                }

                // Sleep for 500 ms.
                Thread.Sleep(500);
            }
        }

        public void StartNewGame(String numOfCols, String numOfRows,
            String nameOfMaze)
        {
            this.singlePlayerModel.GenerateGame(numOfRows, numOfCols, nameOfMaze);
        }
    }
}