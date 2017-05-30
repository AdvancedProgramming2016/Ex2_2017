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
    /// <summary>
    /// Single player model interface.
    /// </summary>
    public interface ISinglePlayerGame : INotifyPropertyChanged
    {
        /// <summary>
        /// Maze property.
        /// </summary>
        Maze Maze { get; set; }

        /// <summary>
        /// Postion property.
        /// </summary>
        Position PlayerPosition { get; set; }

        /// <summary>
        /// Command changed property.
        /// </summary>
        string CommandPropertyChanged { get; set; }

        /// <summary>
        /// Generates maze.
        /// </summary>
        /// <param name="numOfRows">Rows.</param>
        /// <param name="numOfCols">Columns.</param>
        /// <param name="nameOfMaze">Maze name.</param>
        void GenerateGame(String numOfRows, String numOfCols,
            String nameOfMaze);

        /// <summary>
        /// Moves player.
        /// </summary>
        /// <param name="x">X coordinate.</param>
        /// <param name="y">Y coordinate.</param>
        void MovePlayer(int x, int y);

        /// <summary>
        /// Solution property.
        /// </summary>
        string Solution { get; set; }

        /// <summary>
        /// Solves the maze.
        /// </summary>
        /// <param name="name">Maze name.</param>
        void SolveMaze(string name);

        /// <summary>
        /// Restarts the game.
        /// </summary>
        void Restart();

        /// <summary>
        /// Enable controls event.
        /// </summary>
        event EventHandler EnableControls;

        /// <summary>
        /// Connection lost event.
        /// </summary>
        event EventHandler ConnectionLost;
    }
}