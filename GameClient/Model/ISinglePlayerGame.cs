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

        void GenerateGame(String numOfRows, String numOfCols,
            String nameOfMaze);

        void MovePlayer(int x, int y);

        // void MovePlayer();
        string Solution { get; set; }

        void SolveMaze(string name);

        void Restart();

        event EventHandler EnableControls;

        event EventHandler ConnectionLost;
    }
}