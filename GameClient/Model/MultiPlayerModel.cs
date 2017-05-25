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
using GameClient.Model.Listeners;
using GameClient.Model.Parsers;

namespace GameClient.Model
{
    public class MultiPlayerModel : IMultiPlayerGame
    {
        private Maze maze;
        private Position playerPosition;
        private Position opponentPosition;
        private ObservableCollection<string> gamesList;
        private CommunicationClient communicationClient;

        public MultiPlayerModel(ISettingsModel settingModel)
        {
            int port = settingModel.Port;
            string ip = settingModel.IpAddress;
            communicationClient = new CommunicationClient();
            communicationClient.Connect(port, ip);
            communicationClient.PropertyChanged +=
                delegate(Object sender, PropertyChangedEventArgs e)
                {
                    ServerResponse = communicationClient.CommandFromUser;
                    HandleServerResult(ServerResponse);
                };
        }

        public string ServerResponse { get; set; }

        public string CommandPropertyChanged { get; set; }

        public event PropertyChangedEventHandler PropertyChanged;

        public void NotifyPropertyChanged(string propName)
        {
            PropertyChanged?.Invoke(this,
                new PropertyChangedEventArgs(propName));
        }

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

        public ObservableCollection<string> GamesList
        {
            get { return this.gamesList; }
            set
            {
                this.gamesList = value;
                this.NotifyPropertyChanged("GamesList");
            }
        }

        /*  private void WaitForServerInfo()
            {
                Task task = Task.Run(() =>
                {
                    NetworkStream stream;
                    //var reader = new StreamReader(stream);
                    //serverListener = new ServerListener(tcpClient, reader);
    
                    //start listener
                    //serverListener.StartListening();
                    while (true)
                    {
                        // Wait for json from server.
                        stream = this.tcpClient.GetStream();
                        //Do something.
                    }
                });
    
                task.Wait();
            }*/

        public void StartNewGame(string numOfRows, string numOfCols,
            string nameOfMaze)
        {
            string command;

            //Parse into the right format.
            command = CommandParser.ParseToStartCommand(nameOfMaze,
                numOfRows,
                numOfCols);

            CommandPropertyChanged = "start";

            //Send command to the server.
            communicationClient.SendToServer(command);

            while(ServerResponse == null) { }

           // HandleServerResult(ServerResponse);
        }

        public void JoinGame(string gameName)
        {
            string command;

            //Parse into the right format.
            command = CommandParser.ParseToJoinCommand(gameName);

            CommandPropertyChanged = "join";

            //Send command to the server.
            communicationClient.SendToServer(command);

            HandleServerResult(ServerResponse);
        }

        public void CloseGame()
        {
            throw new NotImplementedException();
        }

        public void MovePlayer(Position position)
        {
            throw new NotImplementedException();
        }

        private void HandleServerResult(string command)
        {
            if (command.StartsWith("Error"))
            {
                //TODO decide if needed
            }

            switch (CommandPropertyChanged)
            {
                case "start":
                    HandlStartCommand(command);
                    break;

                case "join":
                    HandleJoinCommand(command);
                    break;

                case "list":
                    HandleListCommand(command);
                    break;
            }
        }

        private void HandlStartCommand(string command)
        {
            Maze = Maze.FromJSON(command);
        }

        private void HandleJoinCommand(string command)
        {
            Maze = Maze.FromJSON(command);
        }

        private void HandleListCommand(string command)
        {
            this.GamesList = FromJsonConverter.GamesList(command);
        }
    }
}