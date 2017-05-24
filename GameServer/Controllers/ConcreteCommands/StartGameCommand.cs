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
using MazeLib;

namespace GameServer.Controllers.ConcreteCommands
{
    /// <summary>
    /// The class responsible for the command of starting a multiplayer game.
    /// </summary>
    public class StartGameCommand : ICommand
    {
        private readonly IModel model;
        private readonly Mutex roomMutex;
        private readonly Mutex startedMazesMutex;

        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="model">Provides the logic of the game</param>
        public StartGameCommand(IModel model)
        {
            this.model = model;
            this.roomMutex = new Mutex();
            this.startedMazesMutex = new Mutex();
        }

        public string Execute(string[] args, ConnectedClient client)
        {
            //Check the number of parameters received is correct.
            if (args.Length != 3)
            {
                return "Error: wrong parameters.\n";
            }

            string gameName = args[0];

            //Create new room.
            this.roomMutex.WaitOne();
            GameRoom room = this.model.Storage.Lobby.CreateNewRoom(gameName);
            this.roomMutex.ReleaseMutex();

            //Check that the game doesn't exist.
            if (room == null)
            {
                return ("Error: game already exists.\n");
            }

            //Creates the requested maze.
            int rows = int.Parse(args[1]);
            int cols = int.Parse(args[2]);
            Maze maze = model.GenerateMaze(gameName, rows, cols);

            //Saves the maze in the started mazes storage.
            this.startedMazesMutex.WaitOne();
            this.model.Storage.Mazes.StartedMazes.Add(maze.Name, maze);
            this.startedMazesMutex.ReleaseMutex();

            //Set the room maze.
            room.Maze = maze;

            room.PlayerOne = client;
            client.IsMultiplayer = true;
            client.GameRoom = room;

            //Waits for the second player to join the game.
            while (!room.IsGameReady)
            {
                Thread.Sleep(250);
            }

            //Sends the maze to the client.
            string mazeInJsonFormat = maze.ToJSON();

            return mazeInJsonFormat;
        }
    }
}