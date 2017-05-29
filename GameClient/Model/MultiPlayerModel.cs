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
    public class MultiPlayerModel : IMultiPlayerGame
    {
        private Maze maze;
        private Position playerPosition;
        private Position opponentPosition;
        private bool opponentExitStatus;
      
        public MultiPlayerModel(ISettingsModel settingModel,
            CommunicationClient communicationClient)
        {
            int port = settingModel.Port;
            string ip = settingModel.IpAddress;
            string serverMessage;
            this.CommunicationClient = communicationClient;
            this.CommunicationClient.ConnectionFailed += HandleConnectionFailed;
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

        public string ServerResponse { get; set; }

        public bool IsSingleCommand { get; set; }

        public event PropertyChangedEventHandler PropertyChanged;

        public void NotifyPropertyChanged(string propName)
        {
            PropertyChanged?.Invoke(this,
                new PropertyChangedEventArgs(propName));
        }

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

        public Position OpponentPosition
        {
            get { return this.opponentPosition; }

            set
            {
                this.opponentPosition = value;
                this.NotifyPropertyChanged("OpponentPosition");
            }
        }

        public Position PlayerPosition
        {
            get { return this.playerPosition; }

            set
            {
                this.playerPosition = value;
                this.NotifyPropertyChanged("PlayerPosition");
            }
        }

        public event EventHandler ExitCalled;
     
        public bool OpponentExitStatus
        {
            get { return this.opponentExitStatus; }
            set
            {
                this.opponentExitStatus = value;
                ExitCalled?.Invoke(this, null);
            }
        }

        public event EventHandler ConnectionLost;

      public void CloseGame(string gameName)
        {
            string command;

            //Parse into the right format.
            command = CommandParser.ParseToCloseCommand(gameName);

            //this.IsSingleCommand = "close";

            //Send command to the server.
            try
            {
                this.CommunicationClient.SendToServer(command);
            }
            catch (Exception)
            {
              this.ConnectionLost?.Invoke(this, null);
            }
           
        }

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
            catch (Exception e)
            {
              throw new Exception();
            }
           
        }

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
            catch (Exception e)
            {
              this.ConnectionLost?.Invoke(this, null);
            }
          
        }

       public void MovePlayer(int x,  int y)
        {
            string command;

            string direction = ParseDirection(x, y);

            //Parse into the right format.
            command = CommandParser.ParseToPlayCommand(direction);

            //this.IsSingleCommand = "play";

            //Send command to the server.
            this.CommunicationClient.SendToServer(command);

            PlayerPosition = new Position(x, y);
        }

        public void HandleServerResult(string response)
        {
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

        private string ParseDirection(int x, int y)
        {
            string direction = null;

            if (PlayerPosition.Col == y + 1)
            {
                direction = "left";
            }
            else if (PlayerPosition.Col == y - 1)
            {
                direction = "right";
            }

            if (PlayerPosition.Row == x + 1)
            {
                direction = "up";
            }
            else if (PlayerPosition.Row == x - 1)
            {
                direction = "down";
            }

            return direction;
        }

        private void HandleMazeCommand(string command)
        {
            try
            {
                Maze maze = Maze.FromJSON(command);
                this.Maze = maze;
                IsSingleCommand = false;
                PlayerPosition = new Position(Maze.InitialPos.Row, Maze.InitialPos.Col);
                OpponentPosition = new Position(Maze.InitialPos.Row, Maze.InitialPos.Col);
            }
            catch (Exception e)
            {
                MessageBox.Show("A maze with the same name already exists");
            }
         
            // ServerResponse = null;
        }

        public void HandleConnectionFailed(object sender, EventArgs e)
        {
            this.ConnectionLost?.Invoke(this, null);
        }
    }
}