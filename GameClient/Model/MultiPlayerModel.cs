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
        private string playerPosition;
        private string opponentPosition;
        private bool opponentExitStatus;
        private string direction;

//        private ObservableCollection<string> gamesList;

        public MultiPlayerModel(ISettingsModel settingModel,
            CommunicationClient communicationClient)
        {
            int port = settingModel.Port;
            string ip = settingModel.IpAddress;
            string serverMessage;
            this.CommunicationClient = communicationClient;
            //  this.communicationClient.Connect(port, ip);

            this.CommunicationClient.PropertyChanged +=
                delegate(Object sender, PropertyChangedEventArgs e)
                {
                    serverMessage = communicationClient.CommandFromUser;
                    if (!string.IsNullOrEmpty(serverMessage))
                    {
                        HandleServerResult(serverMessage);
                    }
                };
        }

        //public string ServerResponse { get; set; }

        public string CommandPropertyChanged { get; set; }

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

        /*  public string OpponentPosition
          {
              get { return this.opponentPosition; }
  
              set
              {
                  this.opponentPosition = value;
                  this.NotifyPropertyChanged("OpponentPosition");
              }
          }*/

        public string PlayerPosition
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

        public event EventHandler DirectionCalled;

        public string OpponentPosition
        {
            get { return this.opponentPosition; }

            set
            {
                this.opponentPosition = value;
                DirectionCalled?.Invoke(this, null);
            }
        }


        public void CloseGame(string gameName)
        {
            string command;

            //Parse into the right format.
            command = CommandParser.ParseToCloseCommand(gameName);

            this.CommandPropertyChanged = "close";

            //Send command to the server.
            this.CommunicationClient.SendToServer(command);
        }

        public void MovePlayer(string direction)
        {
            string command;

            //Parse into the right format.
            command = CommandParser.ParseToPlayCommand(direction);

            this.CommandPropertyChanged = "play";

            //Send command to the server.
            this.CommunicationClient.SendToServer(command);
        }

        public void HandleServerResult(string response)
        {
            if (response == "{}\n")
            {
                this.OpponentExitStatus = true;
            }
            else
            {
                this.OpponentPosition =
                    FromJsonConverter.PlayDirection(response);
            }
        }
    }
}