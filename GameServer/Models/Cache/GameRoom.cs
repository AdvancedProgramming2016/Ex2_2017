using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GameServer.Controllers.Servers;
using GameServer.Views.Handlers;
using MazeLib;

namespace GameServer.Models.Cache
{
    /// <summary>
    /// Contains two players that play togather.
    /// </summary>
    public class GameRoom
    {
       /// <summary>
       /// Constructor.
       /// </summary>
       /// <param name="name">Game name.</param>
        public GameRoom(string name)
        {
            this.Name = name;
            this.IsGameClosed = false;
            this.IsGameReady = false;
            this.IsGameAvailable = true;
        }

        /// <summary>
        /// Maze name property.
        /// </summary>
        /// <value>Maze name.</value>
        public string Name { get; private set; }

        /// <summary>
        /// Is the game closed property.
        /// </summary>
        /// <value>Bool is the game closed.</value>
        public bool IsGameClosed { get; set; }

        /// <summary>
        /// Is the game ready property.
        /// </summary>
        /// <value>Bool is the game ready.</value>
        public bool IsGameReady { get; set; }

        /// <summary>
        /// Is the game availiable property.
        /// </summary>
        /// <value>Bool is the game availiable.</value>
        public bool IsGameAvailable { get; set; }

        /// <summary>
        /// player one property.
        /// </summary>
        /// <value>ConnectedPlayer.</value>
        public ConnectedClient PlayerOne { get; set; }

        /// <summary>
        /// player two property.
        /// </summary>
        /// <value>ConnectedPlayer.</value>
        public ConnectedClient PlayerTwo { get; set; }

        /// <summary>
        /// Maze property.
        /// </summary>
        /// <value>Maze.</value>
        public Maze Maze { get; set; }
    }
}