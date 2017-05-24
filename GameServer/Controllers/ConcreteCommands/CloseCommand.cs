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
using Newtonsoft.Json.Linq;

namespace GameServer.Controllers.ConcreteCommands
{
    /// <summary>
    /// The class responsible for the command of closing a multiplayer game.
    /// </summary>
    public class CloseCommand : ICommand
    {
        private readonly IModel model;
        private readonly Mutex closeMutex;

        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="model">Provides the logic of the game</param>
        public CloseCommand(IModel model)
        {
            this.model = model;
            this.closeMutex = new Mutex();
        }

        public string Execute(string[] args, ConnectedClient client)
        {
            //Check the number of parameters received is correct.
            if (args.Length != 1)
            {
                return "Wrong parameters.\n";
            }

            //Lock close.
            this.closeMutex.WaitOne();

            //Check if room wasn't closed already.
            if (!client.IsConnected)
            {
                //Lock close.
                this.closeMutex.WaitOne();
                return string.Empty;
            }

            //Get the game room members.
            GameRoom room = client.GameRoom;
            ConnectedClient rivalPlayer;

            //Set the players accordingly.
            if (room.PlayerOne == client)
            {
                rivalPlayer = room.PlayerTwo;
            }
            else
            {
                rivalPlayer = room.PlayerOne;
            }

            //Disconnect players from multiplayer.
            client.IsMultiplayer = false;
            rivalPlayer.IsMultiplayer = false;
            client.GameRoom = null;
            rivalPlayer.GameRoom = null;

            //Delete the maze the players played upon.
            Maze maze = room.Maze;
            this.model.Storage.Mazes.StartedMazes.Remove(maze.Name);

            //Delete game room.
            room.IsGameClosed = true;
            this.model.Storage.Lobby.DeleteGameRoom(room.Name);

            JObject emptyJObject = new JObject();

            //send empty JSon to player two to notify the game is closed.
            rivalPlayer.IsConnected = false;
            rivalPlayer.Send(emptyJObject.ToString());
            client.IsConnected = false;

            //Release close.
            this.closeMutex.ReleaseMutex();

            return string.Empty;
        }
    }
}