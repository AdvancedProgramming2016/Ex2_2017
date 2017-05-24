using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using GameServer.Controllers.AbstractCommands;
using GameServer.Controllers.Servers;
using GameServer.Models;
using GameServer.Models.Cache;
using GameServer.Views.Handlers;
using MazeLib;

namespace GameServer.Controllers.ConcreteCommands
{
    /// <summary>
    /// The class responsible for the command of joining a player to a multiplayer game.
    /// </summary>
    public class JoinCommand : ICommand
    {
        private readonly IModel model;
        private readonly Mutex joinMutex;

        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="model">Provides the logic of the game</param>
        public JoinCommand(IModel model)
        {
            this.model = model;
            this.joinMutex = new Mutex();
        }

        public string Execute(string[] args, ConnectedClient client)
        {

            //Check the number of parameters received is correct.
            if (args.Length != 1)
            {
                return "Error: wrong parameters.\n";
            }
            string gameName = args[0];

            //Lock join.
            this.joinMutex.WaitOne();

            //Search for the game.
            GameRoom room = this.model.Storage.Lobby.SearchGameRoom(gameName);

            //Check if room exists.
            if (room == null)
            {
                //Release lock.
                this.joinMutex.ReleaseMutex();
                return "Error: game doesn't exist.\n";
            }

            //Check if the room is available (not taken by other players).
            if (!room.IsGameAvailable)
            {
                //Release lock.
                this.joinMutex.ReleaseMutex();
                return "Error: game is already taken.\n";
            }

            //Check if the client is trying to play against himself.
            if (room.PlayerOne == client)
            {
                //Release lock.
                this.joinMutex.ReleaseMutex();
                return "Error: you can't play against yourself.\n";
            }

            //Release lock.
            this.joinMutex.ReleaseMutex();

            //Set the second player in the game.
            room.PlayerTwo = client;

            //Set the client as a multiplayer.
            client.IsMultiplayer = true;
            client.GameRoom = room;

            //Close the room for other players.
            room.IsGameAvailable = false;
            room.IsGameReady = true;

            //Return the maze to the client
            Maze returnMaze = room.Maze;
            string mazeInJsonFormat = returnMaze.ToJSON();

            return mazeInJsonFormat;
        }
    }
}