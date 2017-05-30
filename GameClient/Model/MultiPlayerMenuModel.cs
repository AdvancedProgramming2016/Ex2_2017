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
    /// <summary>
    /// Multiplayer menu model.
    /// </summary>
    class MultiPlayerMenuModel : INotifyPropertyChanged
    {
        /// <summary>
        /// Maze.
        /// </summary>
        private Maze maze;

        /// <summary>
        /// Games list.
        /// </summary>
        private ObservableCollection<String> listOfGames;

        /// <summary>
        /// Settings model reference.
        /// </summary>
        private ISettingsModel settingsModel;

        /// <summary>
        /// Constructor.
        /// </summary>
        /// <param name="settingsModel">Settings model.</param>
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
                };
        }

        /// <summary>
        /// Server response property.
        /// </summary>
        public string ServerResponse { get; set; }

        /// <summary>
        /// Command changed property.
        /// </summary>
        public string CommandPropertyChanged { get; set; }

        /// <summary>
        /// Property changed event.
        /// </summary>
        public event PropertyChangedEventHandler PropertyChanged;

        /// <summary>
        /// Notifies property has changed.
        /// </summary>
        /// <param name="propName"></param>
        public void NotifyPropertyChanged(string propName)
        {
            PropertyChanged?.Invoke(this,
                new PropertyChangedEventArgs(propName));
        }

        /// <summary>
        /// Communication client propertry.
        /// </summary>
        public CommunicationClient CommunicationClient { get; set; }

        /// <summary>
        /// Games list property.
        /// </summary>
        public ObservableCollection<String> ListOfGames
        {
            get { return this.listOfGames; }
            set
            {
                this.listOfGames = value;
                this.NotifyPropertyChanged("ListOfGames");
            }
        }

        /// <summary>
        /// Adds game to list.
        /// </summary>
        /// <param name="gameName"></param>
        public void AddGameToList(String gameName)
        {
            // Open session with the server and add the new game.
            ListOfGames.Add(gameName);
        }

        /// <summary>
        /// Connection lost event.
        /// </summary>
        public event EventHandler ConnectionLost;

        /// <summary>
        /// Requests the game list.
        /// </summary>
        public void RequestList()
        {
            string command;

            //Parse into the right format.
            command = CommandParser.ParseToListCommand();

            CommandPropertyChanged = "list";

            //Send command to the server.
            try
            {
                CommunicationClient.SendToServer(command);

                while (string.IsNullOrEmpty(ServerResponse))
                {
                    continue;
                }

                HandleServerResult(ServerResponse);
            }
            catch (ArgumentNullException)
            {
                this.ConnectionLost?.Invoke(this, null);
            }
        }

        /// <summary>
        /// Handles server response.
        /// </summary>
        /// <param name="command">Command.</param>
        private void HandleServerResult(string command)
        {
            if (command.StartsWith("Error"))
            {
                //TODO decide if needed
            }

            switch (CommandPropertyChanged)
            {
                case "start":
                    //    HandlStartCommand(command);
                    break;

                case "join":
                    //  HandleJoinCommand(command);
                    break;

                case "list":
                    HandleListCommand(command);
                    break;
            }
        }

        /// <summary>
        /// Handles list command.
        /// </summary>
        /// <param name="command">Command.</param>
        private void HandleListCommand(string command)
        {
            //Convert from Json.
            this.ListOfGames = FromJsonConverter.GamesList(command);
            ServerResponse = null;
        }
    }
}