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
using Newtonsoft.Json.Linq;

namespace GameServer.Controllers.ConcreteCommands
{
    /// <summary>
    /// The class responsible for the command of performing a move in the game.
    /// </summary>
    public class PlayCommand : ICommand
    {
        private IModel model;
        private IList<string> legalMoves;
        private Mutex moveMutex;

        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="model">Provides the logic of the game</param>
        public PlayCommand(IModel model)
        {
            this.model = model;
            legalMoves = new List<string>();
            this.moveMutex = new Mutex();

            //Initializes the legal moves.
            AddLegalMoves();
        }

        public string Execute(string[] args, ConnectedClient client)
        {
            //Check the number of parameters received is correct.
            if (args.Length != 1)
            {
                return "Wrong parameters.\n";
            }

            //Initialize game members.
            GameRoom room = client.GameRoom;
            ConnectedClient rivalPlayer;

            string move = args[0];

            //Check if the move is legal.
            if (!ContainsMove(move))
            {
                return "Illegal move\n";
            }

            //Set players.
            if (room.PlayerOne == client)
            {
                rivalPlayer = room.PlayerTwo;
            }
            else
            {
                rivalPlayer = room.PlayerOne;
            }

            //Convert to Json.
            JObject jMove = new JObject();
            jMove["Name"] = room.Name;
            jMove["Direction"] = move;
            
            //Sends the move to the rival.
            this.moveMutex.WaitOne();
            rivalPlayer.Send(jMove.ToString());
            this.moveMutex.ReleaseMutex();

            return string.Empty;
        }

        /// <summary>
        /// Sets the legal moves into the list.
        /// </summary>
        private void AddLegalMoves()
        {
            //Adds moves.
            this.legalMoves.Add("up");
            this.legalMoves.Add("down");
            this.legalMoves.Add("left");
            this.legalMoves.Add("right");
        }

        /// <summary>
        /// Checks if the move received is a legal move.
        /// </summary>
        /// <param name="move">Desired move.</param>
        /// <returns>Is the move legal.</returns>
        private bool ContainsMove(string move)
        {
            //Search for the received move in the list.
            foreach (string legalMove in this.legalMoves)
            {
                if (legalMove == move)
                {
                    return true;
                }
            }

            return false;
        }
    }
}