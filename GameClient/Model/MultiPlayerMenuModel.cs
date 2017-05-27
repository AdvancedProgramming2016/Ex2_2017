using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;
using MazeLib;
using System.Collections.ObjectModel;
using GameClient.Model.Listeners;
using GameClient.Model.Parsers;

namespace GameClient.Model
{
    class MultiPlayerMenuModel : INotifyPropertyChanged
    {
        private Maze maze;
        private ObservableCollection<String> listOfGames;
        private ISettingsModel settingsModel;

        public MultiPlayerMenuModel(ISettingsModel settingsModel)
        {
            //Set settings.
            this.settingsModel = settingsModel;
            int port = this.settingsModel.Port;
            string ip = this.settingsModel.IpAddress;

            //Connect to server.
            this.CommunicationClient = new CommunicationClient();
            this.CommunicationClient.Connect(port, ip);

            this.CommunicationClient.PropertyChanged +=
                delegate(Object sender, PropertyChangedEventArgs e)
                {
                    ServerResponse = CommunicationClient.CommandFromUser;
                    //HandleServerResult(ServerResponse);
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

        public CommunicationClient CommunicationClient { get; set; }

        public ObservableCollection<String> ListOfGames
        {
            get { return this.listOfGames; }
            set
            {
                this.listOfGames = value;
                this.NotifyPropertyChanged("ListOfGames");
            }
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

        public void AddGameToList(String gameName)
        {
            // Open session with the server and add the new game.
            ListOfGames.Add(gameName);
        }

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
            CommunicationClient.SendToServer(command);

            while (string.IsNullOrEmpty(ServerResponse))
            {
                continue;
            }

            HandleServerResult(ServerResponse);
        }

        public void JoinGame(string gameName)
        {
            string command;

            //Parse into the right format.
            command = CommandParser.ParseToJoinCommand(gameName);

            CommandPropertyChanged = "join";

            //Send command to the server.
            CommunicationClient.SendToServer(command);

            while (string.IsNullOrEmpty(ServerResponse))
            {
                continue;
            }

            HandleServerResult(ServerResponse);
        }

        public void RequestList()
        {
            string command;

            //Parse into the right format.
            command = CommandParser.ParseToListCommand();

            CommandPropertyChanged = "list";

            //Send command to the server.
            CommunicationClient.SendToServer(command);

            while (string.IsNullOrEmpty(ServerResponse))
            {
                continue;
            }

            HandleServerResult(ServerResponse);
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
            Maze maze = Maze.FromJSON(command);
            this.Maze = maze;
            // ServerResponse = null;
        }

        private void HandleJoinCommand(string command)
        {
            Maze maze = Maze.FromJSON(command);
            this.Maze = maze;
            //ServerResponse = null;
        }

        private void HandleListCommand(string command)
        {
            this.ListOfGames = FromJsonConverter.GamesList(command);
            ServerResponse = null;
        }
    }
}