using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;
using MazeLib;
using SearchAlgorithmsLib;

namespace GameClient.Model
{
    public interface ISinglePlayerGame : INotifyPropertyChanged
    {
        Maze Maze { get; set; }
        Position PlayerPosition { get; set; }

        string CommandPropertyChanged { get; set; }
        void GenerateGame(String numOfRows, String numOfCols, String nameOfMaze);
       // void MovePlayer();
        Solution<String> SolveMaze();
        void Restart();

    }
}
