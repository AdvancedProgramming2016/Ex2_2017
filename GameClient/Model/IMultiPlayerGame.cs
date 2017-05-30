using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;
using MazeLib;

namespace GameClient.Model
{
    public interface IMultiPlayerGame : INotifyPropertyChanged
    {
        Maze Maze { get; set; }
        Position OpponentPosition { get; set; }
        Position PlayerPosition { get; set; }

        //void JoinGame(string gameName);

        //  void StartNewGame(string numOfRows, string numOfCols,
        //     string nameOfMaze);

        void MovePlayer(int x, int y);
        void CloseGame(string gameName);
        void StartGame(string gameName, string rows, string columns);
        void JoinGame(string gameName);
        event EventHandler ExitCalled;
        event EventHandler ConnectionLost;
        event EventHandler ReachedDestination;
       }
}