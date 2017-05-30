using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;
using MazeLib;

namespace GameClient.Model
{
    /// <summary>
    /// Multiplayer model interface.
    /// </summary>
    public interface IMultiPlayerGame : INotifyPropertyChanged
    {
        /// <summary>
        /// Maze property.
        /// </summary>
        Maze Maze { get; set; }

        /// <summary>
        /// Opponent position property.
        /// </summary>
        Position OpponentPosition { get; set; }

        /// <summary>
        /// Player property.
        /// </summary>
        Position PlayerPosition { get; set; }

        /// <summary>
        /// Moves player.
        /// </summary>
        /// <param name="x">X coordinate.</param>
        /// <param name="y">Y coordinate.</param>
        void MovePlayer(int x, int y);

        /// <summary>
        /// Closes the game.
        /// </summary>
        /// <param name="gameName">Game name.</param>
        void CloseGame(string gameName);

        /// <summary>
        /// Starts the game.
        /// </summary>
        /// <param name="gameName">Game name.</param>
        /// <param name="rows">Rows.</param>
        /// <param name="columns">Columns.</param>
        void StartGame(string gameName, string rows, string columns);

        /// <summary>
        /// Joins to game.
        /// </summary>
        /// <param name="gameName">Game name.</param>
        void JoinGame(string gameName);

        /// <summary>
        /// Exit called event.
        /// </summary>
        event EventHandler ExitCalled;

        /// <summary>
        /// Connection lost event.
        /// </summary>
        event EventHandler ConnectionLost;

        /// <summary>
        /// Reached destination event.
        /// </summary>
        event EventHandler ReachedDestination;
    }
}