using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace GameServer.Models.Cache
{
    /// <summary>
    /// Contains and manages the game rooms.
    /// </summary>
    public class Lobby
    {
        /// <summary>
        /// Constructor.
        /// </summary>
        public Lobby()
        {
           this.GameRooms = new Dictionary<string, GameRoom>();
        }

        /// <summary>
        /// Game rooms property.
        /// </summary>
        /// <value>Dictionary</value>
        public Dictionary<string, GameRoom> GameRooms { get; private set; }

        /// <summary>
        /// Adds a new game room to the storage.
        /// </summary>
        /// <param name="name">Game name</param>
        /// <returns>New game room</returns>
        public GameRoom CreateNewRoom(string name)
        {
            //Check if the dictionary contains the given name.
            if (this.GameRooms.ContainsKey(name))
            {
                return null;
            }

            //Create a new game room.
            GameRoom newRoom = new GameRoom(name);

            //Add the new room to the dictionary.
            this.GameRooms.Add(name, newRoom);

            return newRoom;
        }

        /// <summary>
        /// Finds the room with the given name.
        /// </summary>
        /// <param name="name">string</param>
        /// <returns>Game room</returns>
        public GameRoom SearchGameRoom(string name)
        {
            //Check if game room exists in storage.
            if (!GameRooms.ContainsKey(name))
            {
                return null;
            }

            //Sets the return value to requested game room.
            var gameRoom = GameRooms[name];

            return gameRoom;
        }

        /// <summary>
        /// Removes the game room with the fiven name from the list
        /// </summary>
        /// <param name="name">Maze name.</param>
        public void DeleteGameRoom(string name)
        {
            //Check if game room exists in storage.
            if (!GameRooms.ContainsKey(name))
            {
                return;
            }

            //Removes the game room from storage.
            GameRooms.Remove(name);
        }
    }
}