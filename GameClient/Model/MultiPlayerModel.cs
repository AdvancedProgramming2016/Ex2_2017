using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MazeLib;
using System.Net.Sockets;
using System.Net;
using System.IO;
using System.ComponentModel;
using System.Windows;
using GameClient.Model.Listeners;
using GameClient.Model.Parsers;

namespace GameClient.Model
{
    /// <summary>
    /// Multiplayer game model.
    /// </summary>
    public class MultiPlayerModel : IMultiPlayerGame
    {
        /// <summary>
        /// Maze.
        /// </summary>
        private Maze maze;

        /// <summary>
        /// Player position.
        /// </summary>
        private Position playerPosition;

        /// <summary>
        /// Opponent position.
        /// </summary>
        private Position opponentPosition;

        /// <summary>
        /// Checks if opponent left the game.
        /// </summary>
        private bool opponentExitStatus;

        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="settingModel">Settings model.</param>
        /// <param name="communicationClient">Communication client.</param>
        public MultiPlayerModel(ISettingsModel settingModel,
            CommunicationClient communicationClient)
        {
            //Initializes variables.
            int port = settingModel.Port;
            string ip = settingModel.IpAddress;
            string serverMessage;
            this.CommunicationClient = communicationClient;
            this.CommunicationClient.ConnectionFailed += HandleConnectionFailed;

            //Sets property changed delegate.
            this.CommunicationClient.PropertyChanged +=
                delegate(Object sender, PropertyChangedEventArgs e)
                {
                    serverMessage = communicationClient.CommandFromUser;
                    if (!string.IsNullOrEmpty(serverMessage) &&
                        !IsSingleCommand)
                    {
                        HandleServerResult(serverMessage);
                    }
                    else if (serverMessage != null)
                    {
                        ServerResponse = serverMessage;
                    }
                };
        }

        /// <summary>
        /// Server response property.
        /// </summary>
        public string ServerResponse { get; set; }

        /// <summary>
        /// Is single property.
        /// </summary>
        public bool IsSingleCommand { get; set; }

        /// <summary>
        /// Property changed event.
        /// </summary>
        public event PropertyChangedEventHandler PropertyChanged;

        /// <summary>
        /// Notifiles property changed.
        /// </summary>
        /// <param name="propName">Property name.</param>
        public void NotifyPropertyChanged(string propName)
        {
            PropertyChanged?.Invoke(this,
                new PropertyChangedEventArgs(propName));
        }

        /// <summary>
        /// Communication client property.
        /// </summary>
        public CommunicationClient CommunicationClient { get; set; }

        public Maze Maze
        {
            get { return this.maze; }

            set
            {
                this.maze = value;
                this.NotifyPropertyChanged("Maze");
            }
        }

        /// <summary>
        /// Opponent position property.
        /// </summary>
        public Position OpponentPosition
        {
            get { return this.opponentPosition; }

            set
            {
                this.opponentPosition = value;
                this.NotifyPropertyChanged("OpponentPosition");
            }
        }

        /// <summary>
        /// Reached destination event.
        /// </summary>
        public event EventHandler ReachedDestination;

        /// <summary>
        /// Player position property.
        /// </summary>
        public Position PlayerPosition
        {
            get { return this.playerPosition; }

            set
            {
                this.playerPosition = value;

                if (this.playerPosition.Col == Maze.GoalPos.Col &&
                    this.playerPosition.Row == Maze.GoalPos.Row)
                {
                    this.ReachedDestination?.Invoke(this, null);
                }

                this.NotifyPropertyChanged("PlayerPosition");
            }
        }

        /// <summary>
        /// Exit called event.
        /// </summary>
        public event EventHandler ExitCalled;

        /// <summary>
        /// Opponent exit status property.
        /// </summary>
        public bool OpponentExitStatus
        {
            get { return this.opponentExitStatus; }
            set
            {
                this.opponentExitStatus = value;
                ExitCalled?.Invoke(this, null);
            }
        }

        /// <summary>
        /// Connection lost event.
        /// </summary>
        public event EventHandler ConnectionLost;

        /// <summary>
        /// Closes the game.
        /// </summary>
        /// <param name="gameName">Game name.</param>
        public void CloseGame(string gameName)
        {
            string command;

            //Parse into the right format.
            command = CommandParser.ParseToCloseCommand(gameName);

            //Send command to the server.
            try
            {
                this.CommunicationClient.SendToServer(command);
            }
            catch (ArgumentNullException)
            {
                this.ConnectionLost?.Invoke(this, null);
            }
        }

        /// <summary>
        /// Starts the game.
        /// </summary>
        /// <param name="gameName">Game name.</param>
        /// <param name="rows">Rows.</param>
        /// <param name="columns">Columns.</param>
        public void StartGame(string gameName, string rows, string columns)
        {
            string command;

            //Parse into the right format.
            command = CommandParser.ParseToStartCommand(gameName,
                rows,
                columns);

            IsSingleCommand = true;

            //Send command to the server.
            try
            {
                CommunicationClient.SendToServer(command);

                while (string.IsNullOrEmpty(ServerResponse))
                {
                    continue;
                }

                HandleMazeCommand(ServerResponse);
            }
            catch (ArgumentNullException)
            {
                this.ConnectionLost?.Invoke(this, null);
            }
        }

        /// <summary>
        /// Joins the game.
        /// </summary>
        /// <param name="gameName">Game name.</param>
        public void JoinGame(string gameName)
        {
            string command;

            //Parse into the right format.
            command = CommandParser.ParseToJoinCommand(gameName);

            IsSingleCommand = true;

            //Send command to the server.
            try
            {
                CommunicationClient.SendToServer(command);

                while (string.IsNullOrEmpty(ServerResponse))
                {
                    continue;
                }

                HandleMazeCommand(ServerResponse);
            }
            catch (ArgumentNullException)
            {
                this.ConnectionLost?.Invoke(this, null);
            }
        }

        /// <summary>
        /// Moves the player.
        /// </summary>
        /// <param name="x">X.</param>
        /// <param name="y">Y.</param>
        public void MovePlayer(int x, int y)
        {
            string command;

            string direction = ParseDirection(x, y);

            //Parse into the right format.
            command = CommandParser.ParseToPlayCommand(direction);

            //Send command to the server.
            this.CommunicationClient.SendToServer(command);

            PlayerPosition = new Position(x, y);
        }

        /// <summary>
        /// Handles server result.
        /// </summary>
        /// <param name="response">Server Result.</param>
        public void HandleServerResult(string response)
        {
            //Check if close or move command.
            if (response == "{}\n")
            {
                this.OpponentExitStatus = true;
            }
            else
            {
                string direction = FromJsonConverter.PlayDirection(response);
                this.OpponentPosition =
                    ExecuteMove(OpponentPosition, direction);
            }
        }

        /// <summary>
        /// Executes the move.
        /// </summary>
        /// <param name="position">Positon.</param>
        /// <param name="direction">Direction.</param>
        /// <returns></returns>
        public Position ExecuteMove(Position position, string direction)
        {
            switch (direction)
            {
                case "up":
                    --position.Row;
                    break;

                case "down":
                    ++position.Row;
                    break;

                case "right":
                    ++position.Col;
                    break;

                case "left":
                    --position.Col;
                    break;
            }

            return position;
        }

        /// <summary>
        /// parses the direction to a string.
        /// </summary>
        /// <param name="x">X.</param>
        /// <param name="y">Y.</param>
        /// <returns></returns>
        private string ParseDirection(int x, int y)
        {
            string direction = null;

            //Check if left direction.
            if (PlayerPosition.Col == y + 1)
            {
                direction = "left";
            }

            //Check if right direction.
            else if (PlayerPosition.Col == y - 1)
            {
                direction = "right";
            }

            //Check if uo direction.
            if (PlayerPosition.Row == x + 1)
            {
                direction = "up";
            }

            //Check if down direction.
            else if (PlayerPosition.Row == x - 1)
            {
                direction = "down";
            }

            return direction;
        }

        /// <summary>
        /// Handles maze command.
        /// </summary>
        /// <param name="command">Command.</param>
        private void HandleMazeCommand(string command)
        {
            try
            {
                //Convert maze from Json.
                Maze maze = Maze.FromJSON(command);
                this.Maze = maze;
                IsSingleCommand = false;

                //Set new position.
                PlayerPosition =
                    new Position(Maze.InitialPos.Row, Maze.InitialPos.Col);
                OpponentPosition =
                    new Position(Maze.InitialPos.Row, Maze.InitialPos.Col);
            }
            catch (Exception e)
            {
            }
        }

        /// <summary>
        /// Handles failed connection.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public void HandleConnectionFailed(object sender, EventArgs e)
        {
            this.ConnectionLost?.Invoke(this, null);
        }
    }
}