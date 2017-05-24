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
    public class SinglePlayerGameViewModel : INotifyPropertyChanged
    {
        private ISinglePlayerGame singlePlayerModel;
        private ISettingsModel settingsModel;
        private Maze maze;
        private string vm_Maze;
        private string vm_PlayerPosition;
        private string vm_mazeName;
        private string vm_algorithmType;
        private string vm_initialPosition;
        private string vm_destPosition;
        private string vm_rows;
        private string vm_cols;

        public SinglePlayerGameViewModel(ISinglePlayerGame singlePlayerModel,
            ISettingsModel settingsModel)
        {
            this.singlePlayerModel = singlePlayerModel;
            this.settingsModel = settingsModel;

            this.singlePlayerModel.PropertyChanged +=
                delegate(Object sender, PropertyChangedEventArgs e)
                {
                    NotifyPropertyChanged("VM_" + e.PropertyName);
                };
        }

        public event PropertyChangedEventHandler PropertyChanged;

        public Maze VM_FullMaze
        {
            get { return maze; }
            set
            {
                maze = value;
                VM_MazeName = maze.Name;
                VM_Maze = maze.ToString();
                VM_InitialPostion = maze.InitialPos.ToString();
                VM_DestPosition = maze.GoalPos.ToString();
                VM_Rows = maze.Rows.ToString();
                VM_Cols = maze.Cols.ToString();
            }
        }

        public String VM_Maze
        {
            get { return DivideMazeToCommas(this.singlePlayerModel.Maze); }

            set
            {
                this.vm_Maze = value;
                NotifyPropertyChanged("VM_Maze");
            }
        }

        private IEnumerable<string> Split(string str, int chunkSize)
        {
            return Enumerable.Range(0, str.Length / chunkSize)
                .Select(i => str.Substring(i * chunkSize, chunkSize));
        }


        private string DivideMazeToCommas(Maze maze)
        {
            string str = maze.ToString();
            int chunkSize = maze.Rows;
            string finalString = string.Empty;
            int stringLength = str.Length;
            for (int i = 0; i < stringLength; i += chunkSize)
            {
                if (i + chunkSize > stringLength) { chunkSize = stringLength - i;}
                finalString += str.Substring(i, chunkSize) + ",";
            }

            return finalString;
        }

        public string VM_DefaultAlgorithm
        {
            get { return this.settingsModel.DefaultAlgo.ToString(); }
            set
            {
                this.vm_algorithmType = value;
                NotifyPropertyChanged("VM_DefaultAlgorithm");
            }
        }

        public String VM_InitialPostion
        {
            get { return this.singlePlayerModel.Maze.InitialPos.ToString(); }
            set
            {
                this.vm_initialPosition = value;
                NotifyPropertyChanged("VM_InitialPostion");
            }
        }

        public String VM_DestPosition
        {
            get { return this.singlePlayerModel.Maze.GoalPos.ToString(); }
            set
            {
                this.vm_destPosition = value;
                NotifyPropertyChanged("VM_DestPosition");
            }
        }

        public String VM_PlayerPosition
        {
            get { return this.singlePlayerModel.PlayerPosition.ToString(); }
            set
            {
                this.vm_PlayerPosition = value;
                NotifyPropertyChanged("VM_PlayerPosition");
            }
        }

        public string VM_Rows
        {
            get { return this.singlePlayerModel.Maze.Rows.ToString(); }
            set
            {
                this.vm_rows = value;
                NotifyPropertyChanged("VM_Rows");
            }
        }

        public string VM_Cols
        {
            get { return this.singlePlayerModel.Maze.Cols.ToString(); }
            set
            {
                this.vm_cols = value;
                NotifyPropertyChanged("VM_Cols");
            }
        }

        public String VM_MazeName
        {
            get { return this.singlePlayerModel.Maze.Name; }
            set
            {
                this.vm_mazeName = value;
                NotifyPropertyChanged("VM_MazeName");
            }
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
            foreach (char movement in solution
            ) // Change so that solution holds the real solution.
            {
                int currPlayerXPosition, currPlayerYPosition;

                currPlayerXPosition =
                    Convert.ToInt32(VM_PlayerPosition.Split(',')[0]);
                currPlayerYPosition =
                    Convert.ToInt32(VM_PlayerPosition.Split(',')[1]);

                // Move the player to the next location.
                switch (movement)
                {
                    case '0':
                        //Move left
                        VM_PlayerPosition =
                            (currPlayerXPosition - 1).ToString() + ',' +
                            currPlayerYPosition.ToString();
                        break;
                    case '1':
                        //Move right
                        VM_PlayerPosition =
                            (currPlayerXPosition + 1).ToString() + ',' +
                            currPlayerYPosition.ToString();
                        break;
                    case '2':
                        //Move up
                        VM_PlayerPosition =
                            currPlayerXPosition.ToString() + ',' +
                            (currPlayerYPosition - 1).ToString();
                        break;
                    case '3':
                        //Move down
                        VM_PlayerPosition =
                            currPlayerXPosition.ToString() + ',' +
                            (currPlayerYPosition + 1).ToString();
                        break;
                }

                // Sleep for 500 ms.
                Thread.Sleep(500);
            }
        }

        public void StartNewGame(String numOfCols, String numOfRows,
            String nameOfMaze)
        {
            this.singlePlayerModel.GenerateGame(numOfRows, numOfCols,
                nameOfMaze);
        }
    }
}