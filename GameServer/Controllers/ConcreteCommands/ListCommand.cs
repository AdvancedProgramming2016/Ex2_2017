using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using GameServer.Controllers.AbstractCommands;
using GameServer.Controllers.Servers;
using GameServer.Controllers.Utilities;
using GameServer.Models;
using GameServer.Models.Cache;
using GameServer.Views.Handlers;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace GameServer.Controllers.ConcreteCommands
{
    /// <summary>
    /// The class responsible for the command of returning the availiable mazes.
    /// </summary>
    public class ListCommand : ICommand
    {
        private readonly IModel model;

        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="model">Provides the logic of the game</param>
        public ListCommand(IModel model)
        {
            this.model = model;
        }

        public string Execute(string[] args, ConnectedClient client)
        {
            string parsedList = string.Empty;

            Dictionary<string, GameRoom> gameRooms =
                this.model.Storage.Lobby.GameRooms;

            //Holds the names of the availiable games/
            IList<string> gamesList = new List<string>();

            //Add all the availiable games to the games names list.
            foreach (KeyValuePair<string, GameRoom> room in gameRooms)
            {
                if (room.Value.IsGameAvailable)
                {
                    gamesList.Add(room.Value.Name);
                }
            }

            //Convert the games names list to JSon array.
            string gamesListInJsonFormat =
                JsonConvert.SerializeObject(gamesList);

            //client.StreamWriter.Write(gamesListInJsonFormat);

            //If the client is not in a multiplayer game, close the connection.
            if (!client.IsMultiplayer)
            {
                client.IsConnected = false;
            }

            return gamesListInJsonFormat;
        }
    }
}