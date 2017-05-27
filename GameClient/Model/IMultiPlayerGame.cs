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
        string OpponentPosition { get; set; }
        string PlayerPosition { get; set; }

        //void JoinGame(string gameName);

        //  void StartNewGame(string numOfRows, string numOfCols,
        //     string nameOfMaze);

        void MovePlayer(Position position);
        void CloseGame(string gameName);
        event EventHandler ExitCalled;
    }
}