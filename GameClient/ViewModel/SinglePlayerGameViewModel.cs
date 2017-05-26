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
        private ISettingsViewModel settingsViewModel;
        private Maze maze;
        private string vm_Maze;
        private string vm_PlayerPosition;
        private string vm_mazeName;
        private string vm_algorithmType;
        private string vm_initialPosition;
        private string vm_destPosition;
        private string vm_rows;
        private string vm_cols;
        private string vm_solution;

        public SinglePlayerGameViewModel(ISinglePlayerGame singlePlayerModel,
            ISettingsViewModel settingsViewModel)
        {
            this.singlePlayerModel = singlePlayerModel;
            this.settingsViewModel = settingsViewModel;

            this.singlePlayerModel.PropertyChanged +=
                delegate(Object sender, PropertyChangedEventArgs e)
                {
                    NotifyPropertyChanged("VM_" + e.PropertyName);

                    if (e.PropertyName == "Solution")
                    {
                        //RunAnimation(this.singlePlayerModel.Solution);
                    }
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

        public string VM_Solution
        {
            get { return this.singlePlayerModel.Solution; }
            set
            {
                this.vm_solution = value;
                NotifyPropertyChanged("VM_Solution");
                //RunAnimation(vm_solution);
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

        public string VM_DefaultAlgorithm
        {
            get { return this.settingsViewModel.VM_SelectedAlgo.ToString(); }
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
            get { return this.vm_PlayerPosition; }
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

        public string VM_DefaultNumberCols
        {
            get
            {
                return GameClient.Properties.Settings.Default.DefaultCols
                    .ToString();
            }
        }

        public string VM_DefaultNumberRows
        {
            get
            {
                return GameClient.Properties.Settings.Default.DefaultRows
                    .ToString();
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

        /*
        private void RunAnimation(string solution)
        {

            int currPlayerXPosition, currPlayerYPosition;
            Task t = Task.Run( () =>
            {
                // Reverse the solution.
                char[] reverseSolution = solution.ToCharArray();
                Array.Reverse(reverseSolution);

                // Get player initial position.
                string position = VM_InitialPostion;
                position = position.Trim(new Char[] { '(', ')' });

                currPlayerXPosition =
                    Convert.ToInt32(position.Split(',')[0]);
                currPlayerYPosition =
                    Convert.ToInt32(position.Split(',')[1]);

                foreach (char movement in reverseSolution)
                {
                    string temp;
                    string newPosition;

                    // Move the player to the next location.
                    switch (movement)
                    {
                        case '0': //Move right
                            temp = (currPlayerXPosition + 1).ToString() + ',' +
                                                currPlayerYPosition.ToString();
                            newPosition = "(" + temp + ")";
                            VM_PlayerPosition = newPosition;

                            break;
                        case '1': //Move left
                            temp =
                               (currPlayerXPosition - 1).ToString() + ',' +
                               currPlayerYPosition.ToString();
                            newPosition = "(" + temp + ")";
                            VM_PlayerPosition = newPosition;
                            break;
                        case '2': //Move up
                            temp =
                                currPlayerXPosition.ToString() + ',' +
                                (currPlayerYPosition - 1).ToString();
                            newPosition = "(" + temp + ")";
                            VM_PlayerPosition = newPosition;
                            break;
                        case '3': //Move down
                            temp =
                                currPlayerXPosition.ToString() + ',' +
                                (currPlayerYPosition + 1).ToString();
                            newPosition = "(" + temp + ")";
                            VM_PlayerPosition = newPosition;
                            break;
                    }

                    // Sleep for 500 ms.
                    Thread.Sleep(500);
                }
            });
            t.Wait();
            
        }
        */

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

        public void SolveMaze(string name)
        {
            this.singlePlayerModel.SolveMaze(name);
        }

        public void StartNewGame(String numOfRows, String numOfCols,
            String nameOfMaze)
        {
            this.singlePlayerModel.GenerateGame(numOfRows, numOfCols,
                nameOfMaze);
        }
    }
}