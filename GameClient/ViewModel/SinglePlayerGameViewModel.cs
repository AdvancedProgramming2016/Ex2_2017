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


        public SinglePlayerGameViewModel(ISinglePlayerGame singlePlayerModel,
            ISettingsViewModel settingsViewModel)
        {
            this.singlePlayerModel = singlePlayerModel;
            this.settingsViewModel = settingsViewModel;

            this.singlePlayerModel.ConnectionLost += HandleConnectionLost;
            this.singlePlayerModel.PropertyChanged +=
                delegate(Object sender, PropertyChangedEventArgs e)
                {
                    NotifyPropertyChanged("VM_" + e.PropertyName);

                   /* if (e.PropertyName == "Solution")
                    {
                        SolutionCall?.Invoke(this, null);
                        //RunAnimation(this.singlePlayerModel.Solution);
                    }*/
                };

            this.singlePlayerModel.EnableControls += HandleEnable;
        }

        public event PropertyChangedEventHandler PropertyChanged;

        public event EventHandler SolutionCall;

        /* public Maze VM_FullMaze
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
         }*/

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

        public string VM_Solution
        {
            get { return this.singlePlayerModel.Solution; }
           /* set
            {
                this.vm_solution = value;
                NotifyPropertyChanged("VM_Solution");
                //RunAnimation(vm_solution);
            }*/
        }

        /* private string DivideMazeToCommas(Maze maze)
         {
             string str = maze.ToString();
             string finalString = string.Empty;
 
             finalString = str.Replace("\r\n", ",");
             finalString = finalString.Remove(finalString.Length - 1);
 
             return finalString;
         }*/

        public string VM_DefaultAlgorithm
        {
            get { return this.settingsViewModel.VM_SelectedAlgo.ToString(); }
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
            get { return this.singlePlayerModel.PlayerPosition.ToString(); }
            set
            {
                singlePlayerModel.MovePlayer(
                    int.Parse(value.Split(',')[0].Replace("(", "")),
                    int.Parse(value.Split(',')[1].Replace(")", "")));
            }
        }

        public string VM_Rows
        {
            get { return this.singlePlayerModel.Maze.Rows.ToString(); }
        }

        public string VM_Cols
        {
            get { return this.singlePlayerModel.Maze.Cols.ToString(); }
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

        public event EventHandler EnableCalled;

        public void HandleEnable(object sender, EventArgs e)
        {
            EnableCalled?.Invoke(this, null);
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

        public event EventHandler ConnectionLost;

        public void HandleConnectionLost(object sender, EventArgs e)
        {
            this.ConnectionLost?.Invoke(this, null);
        }
    }
}